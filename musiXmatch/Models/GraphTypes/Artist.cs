using GraphQL.Types;
using musiXmatch.Interfaces;

namespace musiXmatch.Models.GraphTypes
{
    public class Artist: ObjectGraphType<FluentSpotifyApi.Model.FullArtist>
    {
        public Artist(IRepository repository)
        {
            Field<StringGraphType>("Id", resolve: context => context.Source.Id);
            Field<StringGraphType>("Href", resolve: context => context.Source.Href);
            Field<StringGraphType>("Name", resolve: context => context.Source.Name);
            Field<ListGraphType<Image>>("Images", resolve: context => context.Source.Images);
            Field<ListGraphType<StringGraphType>>("Genres", resolve: context => context.Source.Genres);
            Field<StringGraphType>("Popularity", resolve: context => context.Source.Popularity);
            Field<StringGraphType>("Followers", resolve: context => context.Source.Followers);

            Field<ListGraphType<SimpleAlbum>>(
                "albums",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "limit" }
                ),
                resolve: context => repository.GetAlbums(
                    context.Source.Id,
                    context.GetArgument<int>("limit", 20)));

            Field<ListGraphType<Artist>>(
                "relatedArtists",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "limit" }
                ),
                resolve: context => repository.GetRelatedArtist(
                    context.Source.Id,
                    context.GetArgument<int>("limit", 50)));
        }
    }
}
