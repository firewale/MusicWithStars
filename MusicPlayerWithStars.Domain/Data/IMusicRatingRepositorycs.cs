using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerWithStars.Domain.Data
{
    public interface IMusicRatingRepository
    {
        Task<MusicRating> GetOne(SongData key);

        IEnumerable<MusicRating> GetMany(IEnumerable<SongData> keys);

        Task<IEnumerable<MusicRating>> GetAll();

        Task AddOrUpdate(MusicRating rating);

        void AddOrUpdate(IEnumerable<MusicRating> ratings);
    }
}
