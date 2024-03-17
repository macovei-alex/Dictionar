using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Dictionar.Converters
{
	public class StringEqualityColorConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values != null
				&& values.Length == 2
				&& values[0] is string string1
				&& values[1] is string string2)
			{
				if (string.Equals(string1, string2)) { return Brushes.Green; }
				else { return Brushes.Red; }
			}

			return Brushes.Red;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
