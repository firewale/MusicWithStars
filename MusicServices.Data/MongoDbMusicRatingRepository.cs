using MusicPlayerWithStars.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPlayerWithStars.Domain;
using MongoDB.Driver;
using System.Diagnostics;
using MongoDB.Bson;

namespace MusicServices.Data
{
    public class MongoDbMusicRatingRepository : IMusicRatingRepository
    {
        private const string ConnectionString = "mongodb://localhost";
        private const string DatabaseName = "MusicWithStars";
        private const string MongoCollectionName = "MusicRatings";

        public void AddOrUpdate(IEnumerable<MusicRating> ratings)
        {
            throw new NotImplementedException();
        }

        public async Task AddOrUpdate(MusicRating rating)
        {
            var ratingCollection = GetRatingCollection();

            var existingCount = await ratingCollection.Find(BuildFilter(rating)).CountAsync();
            Debug.WriteLine("Record Inserted");

            if (existingCount == 0)
            {
                await ratingCollection.InsertOneAsync(new MusicRatingEntity(rating));
                return;
            }

            var builder = Builders<MusicRatingEntity>.Update;
            var update = builder.Set(x => x.Rating.Rating, rating.Rating);

            //Updates the first one that matches criteria
            var result = await ratingCollection.UpdateOneAsync<MusicRatingEntity>(
               re => re.Rating.SongData.Album == rating.SongData.Album
            && re.Rating.SongData.Artist == rating.SongData.Artist
            && re.Rating.SongData.Genre == rating.SongData.Genre
            && re.Rating.SongData.Title == rating.SongData.Title,
            update);

            if (result.IsAcknowledged)
            {
                Debug.WriteLine("Update modified " + result.ModifiedCount);
            }
        }

        public async Task<IEnumerable<MusicRating>> GetAll()
        {
            var ratingCollection = GetRatingCollection();
            
            //Equivalent of a select all
            var filter = new BsonDocument();
            var cursor = await ratingCollection.FindAsync(filter);

            var ratings = new List<MusicRating>();

            while (await cursor.MoveNextAsync())
            {
                var batch = cursor.Current;
                foreach (MusicRatingEntity entity in batch)
                {
                    ratings.Add(entity.Rating);
                }
            }

            return ratings;
        }

        public IEnumerable<MusicRating> GetMany(IEnumerable<SongData> keys)
        {
            throw new NotImplementedException();
        }

        public async Task<MusicRating> GetOne(SongData key)
        {
            var ratingCollection = GetRatingCollection();

            var rating = await ratingCollection.Find(BuildFilter(key)).FirstOrDefaultAsync();
            return rating.Rating;
        }

       

        private IMongoCollection<MusicRatingEntity> GetRatingCollection()
        {
            // Get a thread-safe client object by using a connection string
            var mongoClient = new MongoClient(ConnectionString);
            var db = mongoClient.GetDatabase(DatabaseName);

            //Retrieve any existing tests in the test DB
            var ratings = db.GetCollection<MusicRatingEntity>(MongoCollectionName);
            return ratings;
        }

        private FilterDefinition<MusicRatingEntity> BuildFilter(MusicRating rating)
        {
            return BuildFilter(rating.SongData);
        }

        private FilterDefinition<MusicRatingEntity> BuildFilter(SongData songData)
        {
            var builder = Builders<MusicRatingEntity>.Filter;

            var filter = builder.Eq(re => re.Rating.SongData.Album, songData.Album)
                & builder.Eq(re => re.Rating.SongData.Artist, songData.Artist)
                & builder.Eq(re => re.Rating.SongData.Genre, songData.Genre)
                & builder.Eq(re => re.Rating.SongData.Title, songData.Title);

            return filter;
        }
    }
}
