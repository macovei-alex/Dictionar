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
		private Random RNG { get; }

		private const int QUESTION_COUNT = 5;

		private Tuple<DictionaryEntry, HintType>[] Entries { get; }
		private string[] Guesses { get; }
		private int QuestionCounter { get; set; }

		public GamePage()
		{
			InitializeComponent();

			RNG = new Random();

			Entries = new Tuple<DictionaryEntry, HintType>[QUESTION_COUNT];
			Guesses = new string[QUESTION_COUNT];
		}

		private void GenerateRandomEntries()
		{
			for (int i = 0; i < QUESTION_COUNT; i++)
			{
				var entry = GenerateUniqueRandomEntry(i);

				if (entry.Image != null
					&& entry.Image != string.Empty
					&& entry.Image != DictionaryEntry.DefaultImageString)
				{
					if (RNG.Next(0, 2) == 0)
					{
						Entries[i] = new Tuple<DictionaryEntry, HintType>(entry, HintType.Image);
					}
					else
					{
						Entries[i] = new Tuple<DictionaryEntry, HintType>(entry, HintType.Definition);
					}
				}
				else
				{
					Entries[i] = new Tuple<DictionaryEntry, HintType>(entry, HintType.Definition);
				}
			}
		}

		private DictionaryEntry GenerateUniqueRandomEntry(int lastCheck)
		{
			DictionaryEntry entry = null;
			bool entryFound = true;

			while (entryFound)
			{
				entryFound = false;

				entry = Dictionary.Random();
				for (int i = 0; i < lastCheck; i++)
				{
					if (Entries[i].Item1.Word == entry.Word)
					{
						entryFound = true;
						break;
					}
				}
			}

			return entry;
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			GenerateRandomEntries();

			previousQuestionButton.IsEnabled = false;
			QuestionCounter = 0;
			SetHint(Entries[QuestionCounter]);
		}

		private void leaveGameButton_Click(object sender, RoutedEventArgs e)
		{
			ParentWindow.SwapPage(Utils.Pages.MainPage);
		}

		private void wordTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				nextQuestionButton_Click(null, null);
			}
			else if (e.Key == Key.Tab)
			{
				previousQuestionButton_Click(null, null);
				wordTextBox.Focus();
				e.Handled = true;
				wordTextBox.SelectionStart = wordTextBox.Text.Length;
			}
		}

		private void previousQuestionButton_Click(object sender, RoutedEventArgs e)
		{
			if (QuestionCounter == 0)
			{
				return;
			}
			else if (QuestionCounter == QUESTION_COUNT - 1)
			{
				nextQuestionButton.Content = "Next question";
			}

			QuestionCounter--;
			wordTextBox.Text = Guesses[QuestionCounter];

			if (QuestionCounter == 0)
			{
				previousQuestionButton.IsEnabled = false;
			}

			SetHint(Entries[QuestionCounter]);
		}

		private void nextQuestionButton_Click(object sender, RoutedEventArgs e)
		{
			Guesses[QuestionCounter] = wordTextBox.Text;

			QuestionCounter++;
			wordTextBox.Text = string.Empty;
			previousQuestionButton.IsEnabled = true;

			if (QuestionCounter == QUESTION_COUNT)
			{
				string[][] informationMatrix = new string[QuestionCounter][];
				for (int i = 0; i < QuestionCounter; i++)
				{
					informationMatrix[i] = new string[2];
					informationMatrix[i][0] = Guesses[i].ToLower();
					informationMatrix[i][1] = Entries[i].Item1.Word.ToLower();
				}

				GameResultsPage gameResultsPage = new GameResultsPage(informationMatrix);
				ParentWindow.MainFrame.Navigate(gameResultsPage);

				return;
			}

			SetHint(Entries[QuestionCounter]);

			if (QuestionCounter == QUESTION_COUNT - 1)
			{
				nextQuestionButton.Content = "Finish";
			}
		}

		private void SetHint(Tuple<DictionaryEntry, HintType> entry)
		{
			if (entry.Item2 == HintType.Definition)
			{
				definitionAnswerTextBox.Visibility = Visibility.Visible;
				definitionAnswerTextBox.Text = entry.Item1.Definition;

				imageImage.Visibility = Visibility.Hidden;
			}
			else if (entry.Item2 == HintType.Image)
			{
				definitionAnswerTextBox.Visibility = Visibility.Hidden;

				imageImage.Visibility = Visibility.Visible;
				imageImage.Source = Utils.GetImageFromBase64(entry.Item1.Image);
			}
		}
	}
}
