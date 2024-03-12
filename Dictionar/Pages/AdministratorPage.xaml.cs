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
using System.ComponentModel;

namespace Dictionar.Pages
{
	/// <summary>
	/// Interaction logic for AdministratorPage.xaml
	/// </summary>
	public partial class AdministratorPage : Page
	{
		public BitmapImage DefaultImage { get; } = new BitmapImage(Utils.DefaultImageUri);
		private MainWindow ParentWindow => Window.GetWindow(this) as MainWindow;
		private Dictionary Dictionary => ParentWindow.Dictionary;
		private DictionaryEntry CurrentEntry
		{
			get
			{
				return (DataContext as DictionaryEntryContext).DictionaryEntry;
			}
			set
			{
				(DataContext as DictionaryEntryContext).DictionaryEntry = value;
			}
		}

		public AdministratorPage()
		{
			InitializeComponent();
			CurrentEntry = new DictionaryEntry();
		}

		private void mainPageButton_Click(object sender, RoutedEventArgs e)
		{
			ParentWindow.SwapPage(Utils.Pages.MainPage);
		}

		private void wordTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				string word = string.Copy(wordTextBox.Text);

				if ((CurrentEntry = SafeSearch()) != DictionaryEntry.Empty)
				{
					Debug(Utils.Debug.Good, "Entry found.");

					try
					{
						if (CurrentEntry.Image == null
							|| CurrentEntry.Image == string.Empty
							|| CurrentEntry.Image == DictionaryEntry.DefaultImageString)
						{
							imageImage.Source = DefaultImage;
							CurrentEntry.Image = Utils.GetBase64FromImage(DefaultImage);

							Debug(Utils.Debug.Bad, "No image found.", true);
						}
						else
						{
							imageImage.Source = Utils.GetImageFromBase64(CurrentEntry.Image);

							Debug(Utils.Debug.Good, "Image found.", true);
						}
					}
					catch (Exception)
					{
						imageImage.Source = DefaultImage;
						CurrentEntry.Image = Utils.GetBase64FromImage(DefaultImage);

						Debug(Utils.Debug.Bad, "Error loading image.", true);
					}
				}
				else
				{
					CurrentEntry = new DictionaryEntry(word);
					imageImage.Source = null;

					Debug(Utils.Debug.Bad, "Entry not found.");
				}

				wordTextBox.SelectionStart = wordTextBox.Text.Length;
			}
		}

		private void changeImageButton_Click(object sender, RoutedEventArgs e)
		{
			FileDialog fileDialog = new OpenFileDialog
			{
				Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg",
				InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
			};

			if (fileDialog.ShowDialog() == true)
			{
				CurrentEntry = new DictionaryEntry(CurrentEntry.Word);
				imageImage.Source = Utils.GetImageFromBase64(CurrentEntry.Image);

				Debug(Utils.Debug.Good, "Image uploaded.");
			}
		}

		private void saveButton_Click(object sender, RoutedEventArgs e)
		{
			if (SafeSearch() == DictionaryEntry.Empty)
			{
				CurrentEntry = new DictionaryEntry()
				{
					Word = wordTextBox.Text.Trim(),
					Definition = definitionAnswerTextBox.Text.Trim(),
					Image = Utils.GetBase64FromImage(imageImage.Source as BitmapImage)
				};
				Dictionary.CreateEntry(CurrentEntry);

				Debug(Utils.Debug.Good, "Entry created.");
			}
			else
			{
				Dictionary.UpdateEntry(CurrentEntry);

				Debug(Utils.Debug.Good, "Entry updated.");
			}
		}

		private void deleteButton_Click(object sender, RoutedEventArgs e)
		{
			if (SafeSearch() != DictionaryEntry.Empty)
			{
				Dictionary.DeleteEntry(CurrentEntry);
				CurrentEntry = new DictionaryEntry();
				imageImage.Source = null;

				Debug(Utils.Debug.Good, "Entry deleted.");
			}
		}

		private void Debug(Utils.Debug kind, string info, bool append = false)
		{
			if (append)
			{
				debugLabel.Content = debugLabel.Content + " " + info;
			}
			else
			{
				debugLabel.Content = info;
			}

			switch (kind)
			{
				case Utils.Debug.Good:
					debugSymbol.Text = Utils.tick;
					debugSymbol.Foreground = Brushes.Green;
					break;

				case Utils.Debug.Bad:
					debugSymbol.Text = Utils.x;
					debugSymbol.Foreground = Brushes.Red;
					break;

				default:
					debugSymbol.Text = Utils.x;
					debugSymbol.Foreground = Brushes.Blue;
					break;
			}
		}

		private DictionaryEntry SafeSearch()
		{
			return Dictionary.Search(CurrentEntry.Word) ?? DictionaryEntry.Empty;
		}
	}
}
