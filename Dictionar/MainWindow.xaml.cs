using Dictionar.DataHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dictionar
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			Dictionary dictionary = new Dictionary(
				new FileSystemDataSource(Properties.Settings.Default.WordsDirectory));

			dictionary.CreateEntry(new DictionaryEntry("Apple", "Red tasty fruit", "[insert image here]"));

			Console.WriteLine(dictionary.ReadEntry(new DictionaryEntry("Apple")) as DictionaryEntry);
		}
	}
}
