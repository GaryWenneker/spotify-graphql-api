using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using musiXmatch.Interfaces;
using Microsoft.Extensions.Configuration;
using musiXmatch.Models.GraphTypes;
using FluentSpotifyApi;
using FluentSpotifyApi.Builder.Artists;
using FluentSpotifyApi.Model.Browse;
using NewReleases = FluentSpotifyApi.Model.Browse.NewReleases;
using FluentSpotifyApi.Model;

namespace musiXmatch.Models
{
    public class Repository : IRepository
    {
        private IConfiguration _configuration { get; set; }
        private readonly IFluentSpotifyClient _fluentSpotifyClient;

        public Repository(IConfiguration configuration, IFluentSpotifyClient fluentSpotifyClient)
        {
            _configuration = configuration;
            _fluentSpotifyClient = fluentSpotifyClient;
        }

        public async Task<List<FluentSpotifyApi.Model.SimpleAlbum>> GetNewReleasesAsync()
        {
            var data = await _fluentSpotifyClient.Browse.NewReleases.GetAsync(limit: 50);
            return data.Albums.Items.ToList();
        }

        public async Task<List<FluentSpotifyApi.Model.FullArtist>> ArtistSearch(string query, int limit = 50)
        {
            var data = await _fluentSpotifyClient.Search.Artists.Matching(query) .GetAsync(limit: limit);
            return data.Page.Items.ToList();
        }

        public async Task<List<FluentSpotifyApi.Model.FullArtist>> Search(string query, int limit = 50)
        {
            var data = await _fluentSpotifyClient.Search.Artists.Matching(query).GetAsync();
            return data.Page.Items.ToList();
        }

        public async Task<FluentSpotifyApi.Model.FullArtist> GetArtist(string id)
        {
            var data = await _fluentSpotifyClient.Artist(id).GetAsync();
            return data;
        }

        public async Task<List<FluentSpotifyApi.Model.SimpleAlbum>> GetAlbums(string artistId, int limit)
        {            
            var data = await _fluentSpotifyClient.Artist(artistId).Albums.GetAsync(
                null,
                null,
                null,
                limit, 0, CancellationToken.None
            );
            return data.Items.ToList();
        }

        public async Task<List<FluentSpotifyApi.Model.FullArtist>> GetRelatedArtist(string id, int limit = 50)
        {
            var data = await _fluentSpotifyClient.Artist(id).RelatedArtists.GetAsync();
            return data.Items.Take(limit).ToList();
        }

        public async Task<List<FluentSpotifyApi.Model.SimpleTrack>> GetTracks(string id, int limit = 50)
        {
            var data = await _fluentSpotifyClient.Album(id).Tracks.GetAsync();
            return data.Items.Take(limit).ToList();
        }
    }
}
