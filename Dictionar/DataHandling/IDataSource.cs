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

		IEntry ReadEntry(IEntry entry);

		void UpdateEntry(IEntry entry);

		void DeleteEntry(IEntry entry);
		void DeleteEntry(string name);
	}
}
