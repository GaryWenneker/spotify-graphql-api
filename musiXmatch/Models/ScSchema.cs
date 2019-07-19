using GraphQL.Types;
using Sitecore.Services.GraphQL.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;
using musiXmatch.Interfaces;
using Sitecore.DependencyInjection;
using musiXmatch.Models.GraphTypes;

namespace musiXmatch.Models
{

    public class ScSchema : SchemaProviderBase
    {
        private IRepository _repository;

        public override IEnumerable<FieldType> CreateRootQueries()
        {
            _repository = (IRepository)ServiceLocator.ServiceProvider.GetService(typeof(Repository));

            yield return new ArtistQuery();
        }


        protected class ArtistQuery : RootFieldType<Artist, FullArtist>
        {
            public ArtistQuery() : base(name: "artistQuery", description: "Gets some artist data")
            {
                QueryArgument[] queryArgumentArray = new QueryArgument[1];
                int index1 = 0;
                QueryArgument<StringGraphType> queryArgument1 = new QueryArgument<StringGraphType>();
                queryArgument1.Name = "movieId";
                queryArgument1.Description = "The movie ID to get";
                queryArgumentArray[index1] = (QueryArgument)queryArgument1;

                this.Arguments = new QueryArguments(queryArgumentArray);
            }

            

            protected override FullArtist Resolve(ResolveFieldContext context)
            {
                var repos = (IRepository)ServiceLocator.ServiceProvider.GetService(typeof(Repository));
                return repos.GetArtist(context.GetArgument<string>("id")).Result;
            }

        }
    }
}
