using GraphQL.Types;
using musiXmatch.Interfaces;


namespace musiXmatch.Models.GraphTypes
{
    public class Followers: ObjectGraphType<FluentSpotifyApi.Core.Model.Followers>
    {
        public Followers()
        {
            Field(x => x.Total);
        }
    }
}
