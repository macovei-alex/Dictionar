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

		private StringMatrixContext MatrixContext
		{
			get { return DataContext as StringMatrixContext; }
			set { DataContext = value; }
		}

		public GameResultsPage(string[][] informationMatrix)
		{
			InitializeComponent();

			MatrixContext.Matrix = informationMatrix;
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			int score = 0;

			for (int i = 0; i < MatrixContext.Matrix.Length; i++)
			{
				var guessTextBlock = (TextBlock)FindName($"answer{i}");
				var expectedTextBlock = (TextBlock)FindName($"expected{i}");

				guessTextBlock.Text = MatrixContext[i][0];
				expectedTextBlock.Text = MatrixContext[i][1];

				if (MatrixContext[i][0] == MatrixContext[i][1])
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
