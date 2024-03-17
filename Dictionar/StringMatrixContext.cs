using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionar
{
	public class StringMatrixContext : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string[][] _matrix;

		public string[][] Matrix
		{
			get { return _matrix; }
			set
			{
				_matrix = value;
				RaisePropertyChanged(nameof(Matrix));
			}
		}

		public StringMatrixContext() : this(5, 2)
		{
			// empty
		}

		public StringMatrixContext(uint rowCount, uint columnCount)
		{
			Matrix = new string[rowCount][];

			for (uint i = 0; i < rowCount; i++)
			{
				Matrix[i] = new string[columnCount];
			}
		}

		private void RaisePropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public string[] this[int index]
		{
			get { return Matrix[index]; }
			set { Matrix[index] = value; }
		}
	}
}
