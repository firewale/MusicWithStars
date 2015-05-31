using System;
using Xamarin.Forms;
using MusicPlayerWithStars.Domain;

namespace MusicPlayerWithStars
{
	public class RatingToStarsConverter : IValueConverter
	{
		#region IValueConverter implementation

		public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var desiredValue = (Rating)parameter;
			var actualValue = (Rating)value;

			return desiredValue <= actualValue ? "star_gold_48.png" : "star_white_48.png";
		}

		public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}

