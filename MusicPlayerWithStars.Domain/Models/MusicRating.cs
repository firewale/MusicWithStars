
namespace MusicPlayerWithStars.Domain
{
	public class MusicRating
	{
		public MusicRating (Rating rating, SongData songData)
		{
			Rating = rating;
			SongData = songData;
		}

		public Rating Rating { get; private set; }
		public SongData SongData { get; private set; }
	}
}

