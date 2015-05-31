using System;
using Android.Media;
using MusicPlayerWithStars.Droid;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MusicPlayerWithStars.Domain;

[assembly: Xamarin.Forms.Dependency (typeof (AndroidMediaService))]
namespace MusicPlayerWithStars.Droid
{
	public class AndroidMediaService : IMediaService
	{
		private Random _random;
		private MediaPlayer _mediaPlayer;
		private List<string> _playList;
		private List<string> _played;
		private Action<SongData> _onSongDataRetrievedCallback;
		private Action<double> _onSongProgressCallback;
		private int _previousStep;
		private Timer _progressTimer;

		public AndroidMediaService ()
		{
			_progressTimer = new Timer (OnProgressTimerTick, null, TimeSpan.Zero, TimeSpan.FromSeconds (0));
			_played = new List<string> ();
			_random = new Random ();
			InitializeMusicCollection ();

		}

		private void OnProgressTimerTick(object state)
		{
			if (_mediaPlayer == null)
				return;

			if (_onSongProgressCallback == null)
				return;

			_onSongProgressCallback (((double)_mediaPlayer.CurrentPosition / (double)_mediaPlayer.Duration) * 100);
		}

		private void InitializeMusicCollection()
		{
			var path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryMusic);
			_playList = new List<string> (Directory.GetFiles(path.AbsolutePath));
		}

		public void OnCompletion (MediaPlayer mp)
		{
			mp.Stop();
			mp.Reset();
			PlayRandomSong();
		}

		public void SetOnSongDataRetrieved (Action<SongData> callback)
		{
			_onSongDataRetrievedCallback = callback;
		}

		public void SetOnSongProgess(Action<double> callback)
		{
			_onSongProgressCallback = callback;
		}

		private SongData GetSongInfo(string path)
		{
			var retriever = new MediaMetadataRetriever();
			retriever.SetDataSource(path);

			String[] tokens = path.Split(new []{'/'});

			String title = retriever.ExtractMetadata(Android.Media.MetadataKey.Title);
			String album = retriever.ExtractMetadata(Android.Media.MetadataKey.Album);
			String artist = retriever.ExtractMetadata(Android.Media.MetadataKey.Artist);
			String genre = retriever.ExtractMetadata (Android.Media.MetadataKey.Genre);
			String durationStr = retriever.ExtractMetadata (Android.Media.MetadataKey.Duration);

			double duration;
			double.TryParse (durationStr, out duration);

			if(tokens.Length > 0 && string.IsNullOrWhiteSpace(title))
				title = tokens[tokens.Length - 1];

			if(tokens.Length > 1 && string.IsNullOrWhiteSpace(album))
				album = tokens[tokens.Length - 2];

			if(tokens.Length > 2 && string.IsNullOrWhiteSpace(artist))
				artist = tokens[tokens.Length - 3];


			var songData = new SongData (title, album, artist, genre, duration);

			return songData;
		}

		private void StartTimer()
		{
			_progressTimer.Change (TimeSpan.FromSeconds (1), TimeSpan.FromSeconds (1));
		}

		private void StopTimer()
		{
			_progressTimer.Change (TimeSpan.FromSeconds (0), TimeSpan.FromSeconds (0));
		}

		private void PlaySong(string filePath)
		{
			try
			{
				if(_mediaPlayer == null)
					_mediaPlayer = new MediaPlayer();

				try
				{
					var songData = GetSongInfo(filePath);
					if(_onSongDataRetrievedCallback != null)
						_onSongDataRetrievedCallback(songData);
				}
				catch(Exception ex2)
				{

				}
					
				_mediaPlayer.Reset();
				_mediaPlayer.SetDataSource(filePath);
				_mediaPlayer.Prepare();
				_mediaPlayer.Start();

				_played.Add(filePath);

				//start the progress timer to send progress updates to the UI layer
				StartTimer();

				_mediaPlayer.Error += (sender, e) => {throw new Exception(e.ToString());};

				_mediaPlayer.Completion += (sender, e) => OnCompletion(sender as MediaPlayer);

			}
			catch(Exception ex)
			{
				_mediaPlayer.Stop();
				_mediaPlayer.Release();
				_mediaPlayer = null;
				//PlayRandomSong();
			}
		}

		private void PauseSong()
		{
			if (_mediaPlayer == null)
				return;

			_mediaPlayer.Pause();
			StopTimer ();
		}

		private void PlayRandomSong()
		{
			if (!_playList.Any ())
				return;

			var randomSong = _playList [_random.Next (0, _playList.Count)];
			PlaySong (randomSong);
		}

		private void ResumeSong()
		{
			if (_mediaPlayer == null) {
				PlayRandomSong ();
			} else {
				_mediaPlayer.Start ();
				StartTimer ();
			}
		}

		#region IMediaService implementation

		public void Next ()
		{
			PlayRandomSong ();
		}

		public void Previous ()
		{
			if (_played.Count == 0)
				return;
			
			var played = _played.Distinct().ToArray();

			var idx = played.Length - (1 + _previousStep++);

			if (idx < 0)
				return;

			var previous = played[idx];

			if (previous == null)
				return;

			PlaySong (previous);
		}

		public void Pause()
		{
			PauseSong ();
		}

		public void Play ()
		{
			if (_mediaPlayer == null) {
				PlayRandomSong ();
			} else {
				ResumeSong ();
			}

		}

		#endregion
	}
}

