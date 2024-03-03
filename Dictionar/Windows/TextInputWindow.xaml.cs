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
using System.Windows.Shapes;

namespace Dictionar
{
	/// <summary>
	/// Interaction logic for TextInputWindow.xaml
	/// </summary>
	public partial class TextInputWindow : Window
	{
		public enum Button
		{
			Invalid,
			Accept,
			Cancel
		}

		private event EventHandler WindowClosing;
		public Button buttonClicked = Button.Invalid;

		public TextInputWindow(string question)
		{
			InitializeComponent();
			questionLabel.Content = question;
		}

		private void Close_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			WindowClosing?.Invoke(this, EventArgs.Empty);
		}

		private void acceptButton_Click(object sender, RoutedEventArgs e)
		{
			buttonClicked = Button.Accept;
			Close();
		}

		private void cancelButton_Click(object sender, RoutedEventArgs e)
		{
			buttonClicked = Button.Cancel;
			Close();
		}
	}
}
