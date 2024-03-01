using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionar
{
	internal interface IDataSource
	{
		void CreateEntry(IEntry entry);

		IEntry ReadRandomEntry();
		IEntry ReadEntry(string name);
		IEntry ReadByPreffix(string preffix);

		void UpdateEntry(IEntry entry);

		void DeleteEntry(IEntry entry);
		void DeleteEntry(string name);
	}
}
