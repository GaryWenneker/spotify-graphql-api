using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using musiXmatch.Models;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace musiXmatch.Controllers
{
    [ResponseCache(Duration = 6000)]
    [ApiVersion("1.0")]
    [ExcludeFromCodeCoverage]
    [Route("v{api-version:apiVersion}/[controller]")]
    public class GraphQLController : Controller
    {
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ISchema _schema;

        public GraphQLController(ISchema schema, IDocumentExecuter documentExecuter)
        {
            _schema = schema;
            _documentExecuter = documentExecuter;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {
            if (query == null) { throw new ArgumentNullException(nameof(query)); }
            var inputs = query.Variables.ToInputs();
            var executionOptions = new ExecutionOptions
            {
                Schema = _schema,
                Query = query.Query,
                Inputs = inputs
            };

            var result = await _documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
