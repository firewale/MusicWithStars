using MusicPlayerWithStars.Domain.SignalR;
using System;
using Microsoft.AspNet.SignalR.Client;
using MusicPlayerWithStars.Domain;

namespace MusicServices.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var hubConnection = new HubConnection("http://192.168.1.106:8080/");


            var hubProxy = hubConnection.CreateHubProxy<IMusicRatingHub, IMusicRatingHubClient>("musicRatingHub");

            hubConnection.Start().ContinueWith(t =>
            {
                if (t.IsFaulted)
                    Console.WriteLine("An error has ocurred");
            }).Wait();



            hubProxy.SubscribeOn<string>(h => h.OnMusicRatingError, (error) => Console.WriteLine(error));


            Console.WriteLine("Click a key to send a rating!");
            Console.ReadLine();


            hubProxy.Call(h => h.RateSong(new MusicRating(Rating.TwoStars, new SongData("title 1", "album 1", "artist 1", "genre 1", 0))));



            Console.WriteLine("Press a key to retrieve the rating.");
            Console.ReadLine();

            var rating = hubProxy.Call(h => h.GetSongRating(new SongData("title 1", "album 1", "artist 1", "genre 1", 0)));

        }
    }
}
