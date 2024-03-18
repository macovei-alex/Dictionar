using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Dictionar.Pages
{
	/// <summary>
	/// Interaction logic for GameResultsPage.xaml
	/// </summary>
	public partial class GameResultsPage : Page
	{
		private MainWindow ParentWindow => Window.GetWindow(this) as MainWindow;

		private Tuple<string, string>[] QuizResults { get; set; }

		public GameResultsPage(Tuple<string, string>[] quizResults)
		{
			InitializeComponent();

			QuizResults = quizResults;
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			int score = 0;

			for (int i = 0; i < QuizResults.Length; i++)
			{
				var guessTextBlock = (TextBlock)FindName($"answer{i}");
				var expectedTextBlock = (TextBlock)FindName($"expected{i}");

				guessTextBlock.Text = QuizResults[i].Item1;
				expectedTextBlock.Text = QuizResults[i].Item2;

				if (QuizResults[i].Item1 == QuizResults[i].Item2)
				{
					score++;
					guessTextBlock.Background = Brushes.LightGreen;
				}
				else
				{
					guessTextBlock.Background = Brushes.OrangeRed;
				}
			}

			quizScoreLabel.Content += score.ToString();
		}

		private void mainPageButton_Click(object sender, RoutedEventArgs e)
		{
			ParentWindow.SwapPage(Utils.Pages.MainPage);
		}
	}
}
