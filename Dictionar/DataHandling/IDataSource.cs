using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionar
{
	internal interface IDataSource<T> where T : IEntry
	{
		void CreateEntry(T entry);

		T ReadEntry(T entry);

		void UpdateEntry(T entry);

		void DeleteEntry(T entry);
	}
}
