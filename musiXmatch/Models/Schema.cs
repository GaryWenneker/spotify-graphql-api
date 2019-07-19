using GraphQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace musiXmatch.Models
{
    public class Schema : GraphQL.Types.Schema
    {
        public Schema(IDependencyResolver resolver): base(resolver)
        {
            Query = resolver.Resolve<Query>();
        }
    }
}
