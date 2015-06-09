using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPlayerWithStars.Domain.SignalR;
using Microsoft.AspNet.SignalR;
using MusicPlayerWithStars.Domain;
using MusicPlayerWithStars.Domain.Data;

namespace MusicPlayerWithStars.Services.SignalR.Hubs
{
    public class MusicRatingHub : Hub<IMusicRatingHubClient>, IMusicRatingHub
    {
        IMusicRatingRepository _repository;

        public MusicRatingHub(IMusicRatingRepository repository)
        {
            _repository = repository;
        }

        public void RateSong(MusicRating rating)
        {
            try
            {
                _repository.AddOrUpdate(rating).Wait();
            }
            catch (Exception ex)
            {
                Clients.Caller.OnMusicRatingError(string.Format("Error submitting rating {0}", ex.ToString()));
            }

        }

        public MusicRating GetSongRating(SongData key)
        {
            try
            {
                var task = _repository.GetOne(key);
                task.Wait();
                return task.Result;
            }
            catch (Exception ex)
            {
                Clients.Caller.OnMusicRatingError(string.Format("Error getting rating {0}", ex.ToString()));
                return null;
            }

        }

        public IEnumerable<MusicRating> GetRatings()
        {
            try
            {
                var task = _repository.GetAll();
                task.Wait();
                return task.Result;
            }
            catch (Exception ex)
            {
                Clients.Caller.OnMusicRatingError(string.Format("Error getting rating {0}", ex.ToString()));
                return null;
            }

        }


    }
}
