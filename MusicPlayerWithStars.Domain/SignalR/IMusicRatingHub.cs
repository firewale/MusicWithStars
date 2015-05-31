using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerWithStars.Domain.SignalR
{
    public interface IMusicRatingHub : ISignalRHub
    {
        void RateSong(MusicRating rating);
        MusicRating GetSongRating(SongData key);
    }
}
