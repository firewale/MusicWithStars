using System;
using Microsoft.AspNet.SignalR.Client.Hubs;
using System.Threading.Tasks;
using MusicPlayerWithStars.Domain;
using MusicPlayerWithStars.Droid;
using Microsoft.AspNet.SignalR.Client;

[assembly: Xamarin.Forms.Dependency (typeof (AndroidRatingService))]
namespace MusicPlayerWithStars.Droid
{
	public class AndroidRatingService : IRatingService
	{
		 private IHubProxy _hubProxy;

		public AndroidRatingService ()
		{
			
		}

		#region IRatingService implementation

		public async Task SendRating (MusicRating rating)
		{
			
			var hubConnection = new HubConnection("http://192.168.1.106:8080");

			_hubProxy = hubConnection.CreateHubProxy("musicRatingHub");

			hubConnection.Start().ContinueWith(t =>
				{
					if (t.IsFaulted)
						Console.WriteLine("An error has ocurred");
				}).Wait();

			await _hubProxy.Invoke("RateSong", rating);
			
		}
			
		public Task GetRatings ()
		{
			throw new NotImplementedException ();
		}

		public Task GetRatings (DateTime startTime, DateTime endTime)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}

