using System;
using System.Threading.Tasks;

namespace MusicPlayerWithStars.Domain
{
	public interface IRatingService
	{
		Task SendRating(MusicRating rating);
		Task GetRatings ();
		Task GetRatings (DateTime startTime, DateTime endTime);
	}
}

