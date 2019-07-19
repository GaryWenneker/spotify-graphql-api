using GraphQL.Types;
using musiXmatch.Interfaces;


namespace musiXmatch.Models.GraphTypes
{
    public class Image: ObjectGraphType<FluentSpotifyApi.Core.Model.Image>
    {
        public Image(IRepository repository)
        {
            Name = "Image";

            Field<StringGraphType>("Width", resolve: context => context.Source.Width);
            Field<StringGraphType>("Height", resolve: context => context.Source.Height);
            Field<StringGraphType>("Url", resolve: context => context.Source.Url);
        }
    }
}
