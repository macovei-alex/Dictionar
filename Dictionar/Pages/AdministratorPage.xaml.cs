using Microsoft.Win32;
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
using Dictionar.DataHandling;
using System.IO;

namespace Dictionar.Pages
{
	/// <summary>
	/// Interaction logic for AdministratorPage.xaml
	/// </summary>
	public partial class AdministratorPage : Page
	{
		private MainWindow ParentWindow => Window.GetWindow(this) as MainWindow;

		public AdministratorPage()
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

		private void changeDefinitionButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private void changeImageButton_Click(object sender, RoutedEventArgs e)
		{
			if (ParentWindow.Dictionary.ReadEntryOrNull(wordTextBox.Text.Trim()) == null)
			{
				MessageBox.Show("The word does not exist in the dictionary", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			FileDialog fileDialog = new OpenFileDialog
			{
				Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg",
				InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
			};

			var result = fileDialog.ShowDialog();
			if (result == true)
			{
				var imageBase64 = Convert.ToBase64String(File.ReadAllBytes(fileDialog.FileName));

				ParentWindow.Dictionary.UpdateEntry(new DictionaryEntry(wordTextBox.Text.Trim(), definitionAnswerLabel.Content.ToString().Trim(), imageBase64));

				imageImage.Source = Utils.GetImageFromBase64(imageBase64);
			}
		}
	}
}
