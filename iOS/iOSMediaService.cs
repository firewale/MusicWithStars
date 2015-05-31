using System;
using MusicPlayerWithStars.iOS;
using MusicPlayerWithStars.Domain;

[assembly: Xamarin.Forms.Dependency (typeof (iOSMediaService))]
namespace MusicPlayerWithStars.iOS
{
	public class iOSMediaService : IMediaService
	{
		public iOSMediaService ()
		{
		}

		#region IMediaService implementation

		public void Next ()
		{
			throw new NotImplementedException ();
		}

		public void Pause ()
		{
			throw new NotImplementedException ();
		}

		public void Play ()
		{
			throw new NotImplementedException ();
		}

		public void Previous ()
		{
			throw new NotImplementedException ();
		}

		public void SetOnSongDataRetrieved (Action<SongData> callback)
		{
			
		}

		public void SetOnSongProgess (Action<double> callback)
		{
			
		}

		#endregion
	}
}

