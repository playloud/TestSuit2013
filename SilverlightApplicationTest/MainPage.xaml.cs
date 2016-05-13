using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightApplicationTest
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			
			tBox.Text = string.Empty;

			HtmlPage.RegisterScriptableObject("Page", this);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			this.label.Content = tBox.Text;
		}

		[ScriptableMember]
		public string LeaveRequested()
		{
			return "Please dont leave the page..";
		}

		[ScriptableMember]
		public bool PreventLeave()
		{
			return (bool)PreventLeaveCheckBox.IsChecked;
		}

		[ScriptableMember]
		public void PostAction()
		{
			Thread.Sleep(1);
		}

		
		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			CLChildWindow win = new CLChildWindow();

			win.Show();
		}

		private void OpenPopupPage_Click(object sender, RoutedEventArgs e)
		{
			HtmlPopupWindowOptions options = new HtmlPopupWindowOptions()
			{   
				Directories = false,
				Location = false,
				Menubar = false,
				Status = false,
				Toolbar = false,
			};
			HtmlPage.PopupWindow(new Uri("http://localhost:53904/SilverlightApplicationTestTestPage.html"), "_blank", options);
		}

		private void txt_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			popUp.IsOpen = true;
		}

		private void popUp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			popUp.IsOpen = false;
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			
		}
	}
}
