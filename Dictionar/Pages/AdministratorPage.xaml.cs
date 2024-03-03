﻿using Microsoft.Win32;
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
		private MainWindow ParentWindow => Window.GetWindow(this) as MainWindow;
		private Dictionary Dictionary => ParentWindow.Dictionary;
		private DictionaryEntry CurrentEntry { get; set; }

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
				CurrentEntry = ParentWindow.Search(wordTextBox.Text.Trim());
				if (CurrentEntry != null)
				{
					// remove
					definitionAnswerTextBox.Text = CurrentEntry.Definition;

					try
					{
						if (CurrentEntry.Image == DictionaryEntry.DefaultImageString)
						{
							CurrentEntry.Image = Utils.GetBase64FromImage(new BitmapImage(new Uri(Properties.Settings.Default.DefaultImage)));
						}

						imageImage.Source = Utils.GetImageFromBase64(CurrentEntry.Image);
					}
					catch (Exception)
					{
						var bitmap = new BitmapImage(new Uri(Properties.Settings.Default.DefaultImage));

						imageImage.Source = bitmap;
						CurrentEntry.Image = Utils.GetBase64FromImage(bitmap);
						MessageBox.Show("Failed to load image");
					}
				}
				else
				{
					CurrentEntry = new DictionaryEntry(wordTextBox.Text.Trim());
					definitionAnswerTextBox.Text = string.Empty;
					imageImage.Source = null;
				}
			}
		}

		/*private void changeDefinitionButton_Click(object sender, RoutedEventArgs e)
		{
			if (CurrentEntry == null)
			{
				MessageBox.Show("The word does not exist in the dictionary", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			var textInputWindow = new TextInputWindow("Insert the new definition here");
			textInputWindow.Closing += this.TextInputWindowClosing;

			textInputWindow.Show();
			IsTextInputWindowOpen = true;
		}*/

		private void changeImageButton_Click(object sender, RoutedEventArgs e)
		{
			FileDialog fileDialog = new OpenFileDialog
			{
				Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg",
				InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
			};

			if (fileDialog.ShowDialog() == true)
			{
				if (CurrentEntry == null)
				{
					CurrentEntry = new DictionaryEntry(wordTextBox.Text.Trim());
				}

				CurrentEntry.Image = Convert.ToBase64String(File.ReadAllBytes(fileDialog.FileName));
				imageImage.Source = Utils.GetImageFromBase64(CurrentEntry.Image);
			}
		}

		private void saveButton_Click(object sender, RoutedEventArgs e)
		{
			if (CurrentEntry == null)
			{
				CurrentEntry = new DictionaryEntry()
				{
					Word = wordTextBox.Text.Trim(),
					Definition = definitionAnswerTextBox.Text.Trim(),
					Image = Utils.GetBase64FromImage(imageImage.Source as BitmapImage)
				};
				Dictionary.CreateEntry(CurrentEntry);
			}
			else
			{
				CurrentEntry.Definition = definitionAnswerTextBox.Text.Trim();
				Dictionary.UpdateEntry(CurrentEntry);
			}
		}

		public void TextInputWindowClosing(object sender, EventArgs e)
		{
			var inputWindow = sender as TextInputWindow;

			if (inputWindow.buttonClicked == TextInputWindow.Button.Accept)
			{
				CurrentEntry.Definition = inputWindow.answerTextBox.Text.Trim();
				ParentWindow.Dictionary.UpdateEntry(CurrentEntry);
				definitionAnswerTextBox.Text = CurrentEntry.Definition;
			}
		}
	}
}