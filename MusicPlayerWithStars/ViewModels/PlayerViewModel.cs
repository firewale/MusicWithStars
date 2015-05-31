using System;
using MusicPlayerWithStars.Domain;
using Xamarin.Forms;
using System.Windows.Input;
using System.ComponentModel;

namespace MusicPlayerWithStars
{
	public class PlayerViewModel
	{
		private readonly IRatingService _ratingService;
		private readonly IMediaService _mediaService;
		private bool _isPlaying;
		private SongData _songInfo;
		private Rating _rating;
		private double _progress;
		private bool _isSongLoaded;

		public PlayerViewModel ()
		{
			_mediaService = DependencyService.Get<IMediaService> ();
			_ratingService = DependencyService.Get<IRatingService> ();

			_mediaService.SetOnSongDataRetrieved (OnSongDataReceived);
			_mediaService.SetOnSongProgess (OnSongProgress);

			PlayCommand = new Command (Play);
			PauseCommand = new Command (Pause);
			PreviousCommand = new Command (Previous);
			NextCommand = new Command (Next);

			OneStarCommand   = new Command (() => RateSong(Rating.OneStar),    CanRateSong);
			TwoStarCommand   = new Command (() => RateSong(Rating.TwoStars),   CanRateSong);
			ThreeStarCommand = new Command (() => RateSong(Rating.ThreeStars), CanRateSong);
			FourStarCommand  = new Command (() => RateSong(Rating.FourStars),  CanRateSong);
			FiveStarCommand  = new Command (() => RateSong(Rating.FiveStars),  CanRateSong);

		}

		private void OnSongDataReceived(SongData data)
		{
			SongInfo = data;
			_isSongLoaded = data != null;
		}

		private void OnSongProgress(double progress)
		{
			Progress = progress;
		}

		private void Play()
		{
			IsPlaying = true;
			_mediaService.Play ();
		}

		private void Pause()
		{
			IsPlaying = false;
			_mediaService.Pause ();
		}

		private void Previous()
		{
			_mediaService.Previous ();
		}

		private void Next()
		{
			_mediaService.Next ();
		}

		private void RateSong(Rating rating)
		{
			Rating = rating;
			_ratingService.SendRating (BuildMusicRating (rating));
		}

		private MusicRating BuildMusicRating(Rating rating)
		{
			return new MusicRating (rating, SongInfo);
		}

		private bool CanRateSong()
		{
			return true;
			//return _isSongLoaded;
		}

		#region Properties

		public Rating Rating { 
			get { return _rating; } 
			private set {
				if (value == _rating)
					return;

				_rating = value;
				PropertyChanged(this, new PropertyChangedEventArgs("Rating"));
			} }

		public ICommand PlayCommand { get; private set;}
		public ICommand PauseCommand { get; private set;}
		public ICommand PreviousCommand { get; private set;}
		public ICommand NextCommand { get; private set;}

		public ICommand OneStarCommand { get; private set; }
		public ICommand TwoStarCommand { get; private set; }
		public ICommand ThreeStarCommand { get; private set; }
		public ICommand FourStarCommand { get; private set; }
		public ICommand FiveStarCommand { get; private set; }


		public bool IsPlaying
		{
			get{ return _isPlaying;}
			set 
			{
				_isPlaying = value;
				PropertyChanged(this, new PropertyChangedEventArgs("IsPlaying"));
				PropertyChanged(this, new PropertyChangedEventArgs("IsNotPlaying"));
			}
		}

		public bool IsNotPlaying
		{
			get{ return !_isPlaying;}
			set 
			{

				_isPlaying = !value;
				PropertyChanged(this, new PropertyChangedEventArgs("IsPlaying"));
				PropertyChanged(this, new PropertyChangedEventArgs("IsNotPlaying"));
			}
		}


		public double Progress 
		{
			get { return _progress;}
			set 
			{
				if (value == _progress)
					return;

				_progress = value;
				PropertyChanged(this, new PropertyChangedEventArgs("Progress"));
			}
		}

		public SongData SongInfo
		{
			get{ return _songInfo; }
			set
			{
				_songInfo = value;
				PropertyChanged(this, new PropertyChangedEventArgs("SongInfo"));
				PropertyChanged(this, new PropertyChangedEventArgs("Duration"));
			}
		}

		public string Duration 
		{
			get
			{
				if (_songInfo == null)
					return string.Empty;

				return TimeSpan.FromMilliseconds(_songInfo.Duration).ToString("g");
			}
		}

		#endregion

		#region INotifyPropertyChanged implementation
		public event PropertyChangedEventHandler PropertyChanged = (o,e) => {};
		#endregion
	}
}

