using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Dictionar.Converters
{
	public class MultipleNegativeValuesConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values.Length == 4 && values[0] is double && values[1] is double && values[2] is double && values[3] is double)
			{
				double left = -(double)values[0];
				double top = -(double)values[1];
				double right = -(double)values[2];
				double bottom = -(double)values[3];

				return new Thickness(left, top, right, bottom);
			}
			else if (values.Length == 4 && values[0] is double && values[1] is double)
			{
				return new Thickness(-(double)values[0], -(double)values[1], 0, 0);
			}
			return DependencyProperty.UnsetValue;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
