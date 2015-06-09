using System;
using MusicPlayerWithStars.iOS;
using MusicPlayerWithStars.Domain;
using Microsoft.AspNet.SignalR.Client;


[assembly: Xamarin.Forms.Dependency (typeof (iOSRatingService))]
namespace MusicPlayerWithStars.iOS
{
	public class iOSRatingService : IRatingService
	{
		public iOSRatingService ()
		{
		}

		#region IRatingService implementation

		public async System.Threading.Tasks.Task SendRating (MusicRating rating)
		{
			var hubConnection = new HubConnection("http://192.168.0.140:8080");

			var hubProxy = hubConnection.CreateHubProxy("musicRatingHub");

			hubConnection.Start().ContinueWith(t =>
				{
					if (t.IsFaulted)
						Console.WriteLine("An error has ocurred");
				}).Wait();

			await hubProxy.Invoke("RateSong", rating);
		}


		public System.Threading.Tasks.Task GetRatings ()
		{
			throw new NotImplementedException ();
		}

		public System.Threading.Tasks.Task GetRatings (DateTime startTime, DateTime endTime)
		{
			throw new NotImplementedException ();
		}
		#endregion
	}
}

