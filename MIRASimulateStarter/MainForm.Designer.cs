namespace MIRASimulateStarter
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.listBoxUsers = new System.Windows.Forms.ListBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.buttonStartMIRA = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.listBoxUsers);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(379, 314);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Users";
			// 
			// listBoxUsers
			// 
			this.listBoxUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listBoxUsers.FormattingEnabled = true;
			this.listBoxUsers.ItemHeight = 20;
			this.listBoxUsers.Location = new System.Drawing.Point(6, 19);
			this.listBoxUsers.Name = "listBoxUsers";
			this.listBoxUsers.Size = new System.Drawing.Size(367, 284);
			this.listBoxUsers.TabIndex = 0;
			this.listBoxUsers.DoubleClick += new System.EventHandler(this.listBoxUsers_DoubleClick);
			this.listBoxUsers.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listBoxUsers_KeyPress);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.buttonStartMIRA);
			this.groupBox2.Location = new System.Drawing.Point(12, 332);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(379, 87);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Control";
			// 
			// buttonStartMIRA
			// 
			this.buttonStartMIRA.Location = new System.Drawing.Point(7, 20);
			this.buttonStartMIRA.Name = "buttonStartMIRA";
			this.buttonStartMIRA.Size = new System.Drawing.Size(357, 57);
			this.buttonStartMIRA.TabIndex = 0;
			this.buttonStartMIRA.Text = "Start MIRA";
			this.buttonStartMIRA.UseVisualStyleBackColor = true;
			this.buttonStartMIRA.Click += new System.EventHandler(this.buttonStartMIRA_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(403, 421);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "MIRA Simulate starter";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ListBox listBoxUsers;
		private System.Windows.Forms.Button buttonStartMIRA;
	}
}

