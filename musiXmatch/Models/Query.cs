using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using musiXmatch.Interfaces;

namespace musiXmatch.Models
{
    public class Query : ObjectGraphType
    {
        public Query(IRepository repository)
        {
            Field<GraphTypes.Artist>(
                "artist",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "id" }),
                resolve: context => repository.GetArtist(context.GetArgument<string>("id")));



            Field<ListGraphType<GraphTypes.SimpleAlbum>>(
                "newReleases",
                resolve: context => repository.GetNewReleasesAsync());


            Field<ListGraphType<GraphTypes.SimpleTrack>>(
                "tracks",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "id" },
                    new QueryArgument<IntGraphType> { Name = "limit" }
                    ),
                resolve: context => repository.GetTracks(
                    context.GetArgument<string>("id"), 
                    context.GetArgument<int>("limit", 50)
            ));


            Field<ListGraphType<GraphTypes.Artist>>(
                "artistSearch",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "query" },
                    new QueryArgument<IntGraphType> { Name = "limit" }
                    ),
                resolve: context => repository.Search(
                    context.GetArgument<string>("query"), 
                    context.GetArgument<int>("limit", 100)
            ));
        }
    }
}
