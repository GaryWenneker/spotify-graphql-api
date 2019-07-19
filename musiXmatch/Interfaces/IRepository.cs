using FluentSpotifyApi.Model.Browse;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace musiXmatch.Interfaces
{
    public interface IRepository
    {
        Task<List<FluentSpotifyApi.Model.FullArtist>> Search(string query, int limit = 50);
        Task<List<FluentSpotifyApi.Model.SimpleAlbum>> GetNewReleasesAsync();
        Task<List<FluentSpotifyApi.Model.FullArtist>> ArtistSearch(string query, int limit = 50);
        Task<FluentSpotifyApi.Model.FullArtist> GetArtist(string id);
        Task<List<FluentSpotifyApi.Model.SimpleAlbum>> GetAlbums(string artistId, int limit = 50);
        Task<List<FluentSpotifyApi.Model.FullArtist>> GetRelatedArtist(string id, int limit = 50);
        Task<List<FluentSpotifyApi.Model.SimpleTrack>> GetTracks(string id, int limit = 50);
    }
}
