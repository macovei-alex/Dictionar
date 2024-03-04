using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionar
{
	public interface IDataSource<T> where T : IEntry
	{
		void CreateEntry(T entry);

		T ReadEntry(T entry);
		T ReadEntry(string key);

		void UpdateEntry(T entry);

		void DeleteEntry(T entry);
		void DeleteEntry(string key);

		T RandomEntry();
	}
}
