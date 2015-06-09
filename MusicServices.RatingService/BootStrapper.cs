
using Microsoft.Owin.Hosting;
using System;


namespace MusicServices.RatingService
{
    internal class BootStrapper
    {
        internal void Start()
        { 
            var url = "http://*:8080";
            using (WebApp.Start(url))
            {
                Console.WriteLine("Server started ...");
                Console.ReadLine();
            }
        }

        internal void Stop()
        {

        }
    }
}
