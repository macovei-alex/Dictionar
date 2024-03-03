using System;
using System.Collections.Generic;
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

namespace Dictionar
{
	/// <summary>
	/// Interaction logic for DictionaryModePage.xaml
	/// </summary>
	public partial class DictionaryModePage : Page
	{
		private MainWindow ParentWindow => Window.GetWindow(this) as MainWindow;

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
				var dictionaryEntry = ParentWindow.Search(wordTextBox.Text.Trim());
				if (dictionaryEntry != null)
				{
					definitionAnswerLabel.Content = dictionaryEntry.Definition;

					try
					{
						imageImage.Source = Utils.GetImageFromBase64(dictionaryEntry.Image);
					}
					catch (Exception)
					{
						imageImage.Source = new BitmapImage(new Uri(Properties.Settings.Default.DefaultImage));
					}
				}
				else
				{
					definitionAnswerLabel.Content = $"The word \"{wordTextBox.Text.Trim()}\" could not be found in the dictionary";
					imageImage.Source = null;
				}
			}
		}
	}
}
