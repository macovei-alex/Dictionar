﻿using System;
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
	/// Interaction logic for DictionaryModePage.xaml
	/// </summary>
	public partial class DictionaryModePage : Page
	{
		public DictionaryModePage()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (Window.GetWindow(this) is MainWindow window)
			{
				window.NavigateToPage(Utils.GetPageName(Utils.Pages.MainPage));
			}
		}
	}
}
