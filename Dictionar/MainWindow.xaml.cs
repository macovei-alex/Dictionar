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
			MainFrame.Navigate(new Uri("Pages\\DictionaryModePage.xaml", UriKind.Relative));
			SizeToContent = SizeToContent.WidthAndHeight;

			/*Dictionary dictionary = new Dictionary(new FileSystemDataSource<DictionaryEntry>(Properties.Settings.Default.WordsDirectory));

			dictionary.UpdateEntry(new DictionaryEntry("Apple", "Red fruit", "[insert image here]"));
			dictionary.UpdateEntry(new DictionaryEntry("Carrot", "Orange vegetable", "[insert image here]"));
			dictionary.UpdateEntry(new DictionaryEntry("Bomb", "Explosive device", "[insert image here]"));*/
		}

		public void NavigateToPage(string pageName)
		{
			MainFrame.Navigate(new Uri($"Pages\\{pageName}.xaml", UriKind.Relative));
		}
	}
}
