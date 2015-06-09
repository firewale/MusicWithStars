using MongoDB.Bson;
using MusicPlayerWithStars.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicServices.Data
{
    internal class MusicRatingEntity
    {
        public MusicRatingEntity(MusicRating rating)
        {
            Rating = rating;
        }

        public ObjectId Id { get; private set; }
        public MusicRating Rating { get; private set; }
    }
}
