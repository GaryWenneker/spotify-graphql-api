using GraphQL.Types;
using musiXmatch.Interfaces;


namespace musiXmatch.Models.GraphTypes
{
    public class SimpleTrack: ObjectGraphType<FluentSpotifyApi.Model.SimpleTrack>
    {
        public SimpleTrack(IRepository repository)
        {
            Name = "SimpleTrack";
            Field<StringGraphType>("Id", resolve: context => context.Source.Id);
            Field<StringGraphType>("Href", resolve: context => context.Source.Href);
            Field<StringGraphType>("Name", resolve: context => context.Source.Name);
            Field<StringGraphType>("DiscNumber", resolve: context => context.Source.DiscNumber);
            Field<StringGraphType>("DurationMs", resolve: context => context.Source.DurationMs);
            Field<StringGraphType>("IsExplicit", resolve: context => context.Source.IsExplicit);
            Field<StringGraphType>("IsPlayable", resolve: context => context.Source.IsPlayable);
        }
    }
}
