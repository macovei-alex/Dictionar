using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Dictionar.DataHandling
{
	internal class XMLReader : IFileReader
	{
		public string Path { get; }
		private XmlDocument Document { get; set; }

		public XMLReader(string path)
		{
			Path = path;
			Document = null;
		}

		public void LoadContents()
		{
			Document = new XmlDocument();
			Document.Load(Path);
		}

		public void UnloadContents()
		{
			Document = null;
		}

		public void PrintContents()
		{
			if (Document == null)
			{
				LoadContents();
			}

			PrintXmlNode(Document.DocumentElement);
		}

		private void PrintXmlNode(XmlNode node, string indent = "")
		{
			if (node != null)
			{
				switch (node.NodeType)
				{
					case XmlNodeType.Element:
						Console.WriteLine($"{indent}<{node.Name}>");
						foreach (XmlAttribute attribute in node.Attributes)
						{
							Console.WriteLine($"{indent}\t{attribute.Name} = '{attribute.Value}'");
						}
						break;
					case XmlNodeType.Text:
						Console.WriteLine($"{indent}{node.InnerText}");
						break;
				}

				// Recursively print child nodes
				foreach (XmlNode childNode in node.ChildNodes)
				{
					PrintXmlNode(childNode, indent + '\t');
				}

				if (node.NodeType == XmlNodeType.Element)
				{
					Console.WriteLine($"{indent}</{node.Name}>");
				}
			}
		}

		public IEntry GetRandomEntry()
		{
			throw new NotImplementedException();
		}

		public IEntry GetEntry(string name)
		{
			throw new NotImplementedException();
		}

		public IEntry GetByPreffix(string preffix)
		{
			throw new NotImplementedException();
		}
	}
}
