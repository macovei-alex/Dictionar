using System;
using System.ComponentModel;
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
using Dictionar.DataHandling;

namespace Dictionar
{
	/// <summary>
	/// Interaction logic for DictionaryModePage.xaml
	/// </summary>
	public partial class DictionaryModePage : Page
	{
		private MainWindow ParentWindow => Window.GetWindow(this) as MainWindow;
		private Dictionary Dictionary => ParentWindow.Dictionary;

		public DictionaryModePage()
		{
			InitializeComponent();
		}

		private void mainPageButton_Click(object sender, RoutedEventArgs e)
		{
			ParentWindow.SwapPage(Utils.Pages.MainPage);
		}

		private void wordTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				var dictionaryEntry = Dictionary.Search(wordTextBox.Text.Trim());
				if (dictionaryEntry != null)
				{
					definitionAnswerLabel.Content = dictionaryEntry.Definition;

					try
					{
						imageImage.Source = Utils.GetImageFromBase64(dictionaryEntry.Image);
					}
					catch (Exception)
					{
						imageImage.Source = new BitmapImage(Utils.DefaultImageUri);
					}
				}
				else
				{
					definitionAnswerLabel.Content = $"The word \"{wordTextBox.Text.Trim()}\" could not be found in the dictionary";
					imageImage.Source = null;
				}

				wordTextBox.SelectionStart = wordTextBox.Text.Length;
			}
		}
	}
}
