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
	public partial class GamePage : Page
	{
		private enum HintType
		{
			Definition,
			Image
		}

		private MainWindow ParentWindow => Window.GetWindow(this) as MainWindow;
		private Dictionary Dictionary => ParentWindow.Dictionary;
		private Random Random { get; }

		/*private Tuple<DictionaryEntry, HintType>[] Entries { get; }
		private string[] Guesses { get; }*/

		public GamePage()
		{
			InitializeComponent();
			Random = new Random();
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			GenerateNewHint();
		}

		private void leaveGameButton_Click(object sender, RoutedEventArgs e)
		{
			ParentWindow.SwapPage(Utils.Pages.MainPage);
		}

		private void wordTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				GenerateNewHint();
			}
		}

		private void previousQuestionButton_Click(object sender, RoutedEventArgs e)
		{
		}

		private void nextQuestionButton_Click(object sender, RoutedEventArgs e)
		{
		}

		private void GenerateNewHint()
		{
			var entry = Dictionary.Random();
			definitionAnswerTextBox.Text = entry.Definition;

			try
			{
				if (entry.Image == null
					|| entry.Image == string.Empty
					|| entry.Image == DictionaryEntry.DefaultImageString)
				{
					SetHint(entry, HintType.Definition);
				}
				else
				{
					if (Random.Next(0, 2) == 0)
					{
						SetHint(entry, HintType.Definition);
					}
					else
					{
						SetHint(entry, HintType.Image);
					}
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}

		private void SetHint(DictionaryEntry entry, HintType type)
		{
			if (type == HintType.Definition)
			{
				definitionAnswerTextBox.Visibility = Visibility.Visible;
				definitionAnswerTextBox.Text = entry.Definition;

				imageImage.Visibility = Visibility.Hidden;
			}
			else if (type == HintType.Image)
			{
				definitionAnswerTextBox.Visibility = Visibility.Hidden;

				imageImage.Visibility = Visibility.Visible;
				imageImage.Source = Utils.GetImageFromBase64(entry.Image);
			}
		}
	}
}
