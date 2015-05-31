
using System.ComponentModel;

namespace MusicPlayerWithStars
{
	public class MainPageViewModel : INotifyPropertyChanged
	{

		#region Ctor
		public MainPageViewModel()
		{
			PlayerViewModel = new PlayerViewModel ();
			RatingsViewModel = new RatingsViewModel ();
		}
		#endregion

		#region Properties

		public PlayerViewModel PlayerViewModel { get; private set; }

		public RatingsViewModel RatingsViewModel { get; private set;}

		#endregion

		#region INotifyPropertyChanged implementation
		public event PropertyChangedEventHandler PropertyChanged = (o,e) => {};
		#endregion
	}


}

