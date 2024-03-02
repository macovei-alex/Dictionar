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

			Dictionary dictionary = new Dictionary(new FileSystemDataSource<DictionaryEntry>(Properties.Settings.Default.WordsDirectory));

			dictionary.ReadEntryOrNull(new DictionaryEntry("Apple", "Red tasty fruit", "[insert image here]"));
			dictionary.ReadEntryOrNull(new DictionaryEntry("Bomb", "Explosive device", "[insert image here]"));
			dictionary.ReadEntryOrNull(new DictionaryEntry("Carrot", "Orange", "[insert image here]"));

			Console.WriteLine(dictionary.ReadEntry(new DictionaryEntry("Apple")) as DictionaryEntry);
			Console.WriteLine(dictionary.ReadEntry(new DictionaryEntry("Bomb")) as DictionaryEntry);
			Console.WriteLine(dictionary.ReadEntry(new DictionaryEntry("Carrot")) as DictionaryEntry);
		}
	}
}
