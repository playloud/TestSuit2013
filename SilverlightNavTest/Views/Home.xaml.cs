using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SilverlightNavTest
{
	public partial class Home : Page
	{
		public Home()
		{
			InitializeComponent();
		}

		// Executes when the user navigates to this page.
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			string absURI = HtmlPage.Document.DocumentUri.AbsoluteUri;
			string frag = HtmlPage.Document.DocumentUri.Fragment;
			absURI = absURI.Replace(frag, "#/P");
			
			HtmlPopupWindowOptions options = new HtmlPopupWindowOptions()
			{
				Directories = false,
				Location = false,
				Menubar = false,
				Status = false,
				Toolbar = false,
			};
			HtmlPage.PopupWindow(new Uri(absURI), "_blank", options);
			
		}

		private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
		{
			Uri uriQueryPage = new Uri("PopupWebForm.aspx", UriKind.RelativeOrAbsolute);
			string features = String.Format("directories=yes,location=no,menubar=no,status=no,toolbar=no,height=100,width=600,resizable=false");
			HtmlPage.Window.Navigate(uriQueryPage, "_blank", features);
		}
	}
}