using System;
using System.Collections.ObjectModel;
using MusicPlayerWithStars.Domain;

namespace MusicPlayerWithStars
{
	public class RatingsViewModel
	{
		public RatingsViewModel ()
		{
			Ratings = new ObservableCollection<MusicRating> ();
			Ratings.Add(new MusicRating(Rating.FourStars, new SongData("Song 1", "Album 1", "Artist 1", "Genre 1", 0))); 
			Ratings.Add(new MusicRating(Rating.ThreeStars, new SongData("Song 2", "Album 1", "Artist 1", "Genre 1", 0))); 
			Ratings.Add(new MusicRating(Rating.TwoStars, new SongData("Song 3", "Album 1", "Artist 1", "Genre 1", 0))); 
			Ratings.Add(new MusicRating(Rating.FourStars, new SongData("Song 4", "Album 1", "Artist 1", "Genre 1", 0))); 
		}


		public ObservableCollection<MusicRating> Ratings { get; private set; }
	}
}

