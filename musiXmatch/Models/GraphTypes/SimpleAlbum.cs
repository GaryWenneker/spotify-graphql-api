using GraphQL.Types;
using musiXmatch.Interfaces;


namespace musiXmatch.Models.GraphTypes
{
    public class SimpleAlbum: ObjectGraphType<FluentSpotifyApi.Model.SimpleAlbum>
    {
        public SimpleAlbum(IRepository repository)
        {
            Name = "SimpleAlbum";
    
            Field<StringGraphType>("Id", resolve: context => context.Source.Id);
            Field<StringGraphType>("Href", resolve: context => context.Source.Href);
            Field<StringGraphType>("Name", resolve: context => context.Source.Name);
            Field<ListGraphType<Image>>("Images", resolve: context => context.Source.Images);

            Field<ListGraphType<SimpleTrack>>(
                "tracks",
                resolve: context => repository.GetTracks(
                    context.Source.Id));
        }
    }
}
