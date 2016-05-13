using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MIRASimulateStarter
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			AddUsers();
		}

		public void AddUsers()
		{
			listBoxUsers.Items.Add("SePark");
			listBoxUsers.Items.Add("SeParkReader1");
			listBoxUsers.Items.Add("SeParkReader2");
			listBoxUsers.Items.Add("SeParkReader3");
			listBoxUsers.Items.Add("SeParkAdj");
			listBoxUsers.Items.Add("SeParkMonitor");
			listBoxUsers.Items.Add("CEE1TestAdj1");
			listBoxUsers.Items.Add("CEE1TestReader1");
			listBoxUsers.Items.Add("CEE1TestReader2");
			listBoxUsers.Items.Add("CEE1TestReader3");
			listBoxUsers.Items.Add("CEE1TestReader4");
			listBoxUsers.Items.Add("CEE1TestReader5");
			listBoxUsers.Items.Add("CEE1TestAdj1");
			listBoxUsers.Items.Add("CEE1TestAdj2");
			listBoxUsers.Items.Add("CEE1TestAdj3");
			listBoxUsers.Items.Add("CEE1ReaderU1");
			listBoxUsers.Items.Add("CEE1ReaderU2");
			listBoxUsers.Items.Add("CEE1ReaderU3");
			listBoxUsers.Items.Add("CEE1ReaderU4");
			listBoxUsers.Items.Add("CEE1ReaderVal1");
			listBoxUsers.Items.Add("CEE1ReaderVal2");
			listBoxUsers.Items.Add("CEE1ReaderVal3");
			listBoxUsers.Items.Add("CEE1ReaderVal4");
			listBoxUsers.Items.Add("");
		}

		private void buttonStartMIRA_Click(object sender, EventArgs e)
		{
			StartMira();
		}

		private void listBoxUsers_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char) Keys.Return)
			{
				StartMira();
			}
		}

		private void listBoxUsers_DoubleClick(object sender, EventArgs e)
		{
			StartMira();
		}

		public void StartMira()
		{
			if (listBoxUsers.SelectedItem == null || string.IsNullOrEmpty(listBoxUsers.SelectedItem.ToString()))
				return;

			string path = @"C:\Program Files (x86)\Beacon Bioscience, Inc\MIRA\GeneralViewer.exe";
			string arguments = string.Format("-SIMULATEUSER={0}", listBoxUsers.SelectedItem.ToString());
			Process.Start(path, arguments);
		}
	}
}
