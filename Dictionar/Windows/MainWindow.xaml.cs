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
		public Dictionary Dictionary { get; private set; }

		public MainWindow()
		{
			InitializeComponent();
			SwapPage(Utils.Pages.MainPage);
			SizeToContent = SizeToContent.WidthAndHeight;

			Dictionary = new Dictionary(new FileSystemDataSource<DictionaryEntry>(Utils.FullWordsDirectory));
		}

		internal void SwapPage(Utils.Pages page)
		{
			SwapPage(Utils.GetPageName(page));
		}

		public void SwapPage(string pageName)
		{
			MainFrame.Navigate(new Uri($"Pages\\{pageName}.xaml", UriKind.Relative));
		}
	}
}
