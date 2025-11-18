// Token: 0x0200002E RID: 46
public partial class MainForm : global::System.Windows.Forms.Form, global::System.Windows.Forms.IMessageFilter
{
	// Token: 0x060000F5 RID: 245 RVA: 0x000098EC File Offset: 0x00007AEC
	private void InitializeComponent()
	{
		this.icontainer_0 = new global::System.ComponentModel.Container();
		global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MainForm));
		this.RamPercent = new global::System.Windows.Forms.Label();
		this.label47 = new global::System.Windows.Forms.Label();
		this.label48 = new global::System.Windows.Forms.Label();
		this.MonitorName = new global::System.Windows.Forms.Label();
		this.groupBox3 = new global::System.Windows.Forms.GroupBox();
		this.button_0 = new global::System.Windows.Forms.Button();
		this.groupBox2 = new global::System.Windows.Forms.GroupBox();
		this.button_1 = new global::System.Windows.Forms.Button();
		this.CloseBTN = new global::System.Windows.Forms.Button();
		this.AppTitle = new global::System.Windows.Forms.Label();
		this.button_2 = new global::System.Windows.Forms.Button();
		this.AdditionalBTN = new global::System.Windows.Forms.Button();
		this.button_3 = new global::System.Windows.Forms.Button();
		this.groupBox4 = new global::System.Windows.Forms.GroupBox();
		this.label51 = new global::System.Windows.Forms.Label();
		this.LogFileSize = new global::System.Windows.Forms.Label();
		this.AppStatus = new global::System.Windows.Forms.Label();
		this.timer_0 = new global::System.Windows.Forms.Timer(this.icontainer_0);
		this.FormLoaderPNL = new global::System.Windows.Forms.Panel();
		this.MonitorLogo = new global::System.Windows.Forms.PictureBox();
		this.toolTip_0 = new global::System.Windows.Forms.ToolTip(this.icontainer_0);
		this.AdditionalPanelN = new global::System.Windows.Forms.Panel();
		this.NavPNL = new global::System.Windows.Forms.Panel();
		this.MonitorPanelN = new global::System.Windows.Forms.Panel();
		this.ConfigPanelN = new global::System.Windows.Forms.Panel();
		this.FMonitorPanel = new global::System.Windows.Forms.Panel();
		this.FAdditionalPanel = new global::System.Windows.Forms.Panel();
		this.FConfigPanel = new global::System.Windows.Forms.Panel();
		this.verticalProgressBar_0 = new global::VerticalProgressBar();
		this.groupBox3.SuspendLayout();
		((global::System.ComponentModel.ISupportInitialize)this.MonitorLogo).BeginInit();
		base.SuspendLayout();
		this.RamPercent.BackColor = global::System.Drawing.Color.Transparent;
		this.RamPercent.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
		this.RamPercent.Font = new global::System.Drawing.Font("Roboto Slab", 18f);
		this.RamPercent.ForeColor = global::System.Drawing.Color.White;
		this.RamPercent.Location = new global::System.Drawing.Point(33, 205);
		this.RamPercent.Name = "RamPercent";
		this.RamPercent.Size = new global::System.Drawing.Size(125, 31);
		this.RamPercent.TabIndex = 91;
		this.RamPercent.Text = "99.9%";
		this.RamPercent.TextAlign = global::System.Drawing.ContentAlignment.BottomLeft;
		this.label47.BackColor = global::System.Drawing.Color.Transparent;
		this.label47.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
		this.label47.Font = new global::System.Drawing.Font("Roboto Slab", 10f);
		this.label47.ForeColor = global::System.Drawing.Color.White;
		this.label47.Location = new global::System.Drawing.Point(33, 182);
		this.label47.Name = "label47";
		this.label47.Size = new global::System.Drawing.Size(125, 23);
		this.label47.TabIndex = 92;
		this.label47.Text = "Memory Usage";
		this.label48.BackColor = global::System.Drawing.Color.Transparent;
		this.label48.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
		this.label48.Font = new global::System.Drawing.Font("Roboto Slab SemiBold", 14f, global::System.Drawing.FontStyle.Bold | global::System.Drawing.FontStyle.Italic);
		this.label48.ForeColor = global::System.Drawing.Color.White;
		this.label48.Location = new global::System.Drawing.Point(12, 84);
		this.label48.Name = "label48";
		this.label48.Size = new global::System.Drawing.Size(394, 29);
		this.label48.TabIndex = 93;
		this.label48.Text = "Please Monitor Your Server";
		this.label48.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
		this.MonitorName.BackColor = global::System.Drawing.Color.Transparent;
		this.MonitorName.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
		this.MonitorName.Font = new global::System.Drawing.Font("Roboto Slab", 10f, global::System.Drawing.FontStyle.Bold);
		this.MonitorName.ForeColor = global::System.Drawing.Color.White;
		this.MonitorName.Location = new global::System.Drawing.Point(56, 12);
		this.MonitorName.Name = "MonitorName";
		this.MonitorName.Size = new global::System.Drawing.Size(149, 38);
		this.MonitorName.TabIndex = 95;
		this.MonitorName.Text = "OSM - Monitor";
		this.MonitorName.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
		this.groupBox3.Controls.Add(this.verticalProgressBar_0);
		this.groupBox3.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
		this.groupBox3.Font = new global::System.Drawing.Font("Roboto Slab", 6f);
		this.groupBox3.ForeColor = global::System.Drawing.Color.White;
		this.groupBox3.Location = new global::System.Drawing.Point(16, 178);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new global::System.Drawing.Size(11, 58);
		this.groupBox3.TabIndex = 96;
		this.groupBox3.TabStop = false;
		this.button_0.BackColor = global::System.Drawing.Color.FromArgb(0, 126, 249);
		this.button_0.Cursor = global::System.Windows.Forms.Cursors.Hand;
		this.button_0.FlatAppearance.BorderColor = global::System.Drawing.Color.Silver;
		this.button_0.FlatAppearance.BorderSize = 0;
		this.button_0.FlatAppearance.MouseDownBackColor = global::System.Drawing.Color.DarkBlue;
		this.button_0.FlatAppearance.MouseOverBackColor = global::System.Drawing.Color.Blue;
		this.button_0.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
		this.button_0.Font = new global::System.Drawing.Font("Roboto Slab", 10f);
		this.button_0.ForeColor = global::System.Drawing.Color.White;
		this.button_0.Location = new global::System.Drawing.Point(313, 182);
		this.button_0.Name = "RefreshBTN";
		this.button_0.Size = new global::System.Drawing.Size(93, 54);
		this.button_0.TabIndex = 1;
		this.button_0.Text = "Refresh";
		this.button_0.UseVisualStyleBackColor = false;
		this.button_0.Click += new global::System.EventHandler(this.button_0_Click);
		this.groupBox2.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
		this.groupBox2.Font = new global::System.Drawing.Font("Roboto Slab", 6f);
		this.groupBox2.ForeColor = global::System.Drawing.Color.White;
		this.groupBox2.Location = new global::System.Drawing.Point(16, 163);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new global::System.Drawing.Size(390, 1);
		this.groupBox2.TabIndex = 97;
		this.groupBox2.TabStop = false;
		this.button_1.Cursor = global::System.Windows.Forms.Cursors.Hand;
		this.button_1.FlatAppearance.BorderColor = global::System.Drawing.Color.FromArgb(20, 30, 54);
		this.button_1.FlatAppearance.BorderSize = 0;
		this.button_1.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
		this.button_1.Font = new global::System.Drawing.Font("Roboto Slab", 14f);
		this.button_1.ForeColor = global::System.Drawing.Color.White;
		this.button_1.Location = new global::System.Drawing.Point(328, 1);
		this.button_1.Name = "MinimizeBTN";
		this.button_1.Size = new global::System.Drawing.Size(38, 48);
		this.button_1.TabIndex = 99;
		this.button_1.Text = "_";
		this.button_1.UseVisualStyleBackColor = true;
		this.button_1.Click += new global::System.EventHandler(this.button_1_Click);
		this.CloseBTN.Cursor = global::System.Windows.Forms.Cursors.Hand;
		this.CloseBTN.FlatAppearance.BorderColor = global::System.Drawing.Color.FromArgb(20, 30, 54);
		this.CloseBTN.FlatAppearance.BorderSize = 0;
		this.CloseBTN.FlatAppearance.MouseDownBackColor = global::System.Drawing.Color.Maroon;
		this.CloseBTN.FlatAppearance.MouseOverBackColor = global::System.Drawing.Color.Red;
		this.CloseBTN.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
		this.CloseBTN.Font = new global::System.Drawing.Font("Roboto Slab", 15f);
		this.CloseBTN.ForeColor = global::System.Drawing.Color.White;
		this.CloseBTN.Location = new global::System.Drawing.Point(369, 1);
		this.CloseBTN.Name = "CloseBTN";
		this.CloseBTN.Size = new global::System.Drawing.Size(52, 48);
		this.CloseBTN.TabIndex = 98;
		this.CloseBTN.Text = "X";
		this.CloseBTN.UseVisualStyleBackColor = true;
		this.CloseBTN.Click += new global::System.EventHandler(this.CloseBTN_Click);
		this.AppTitle.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
		this.AppTitle.Font = new global::System.Drawing.Font("Roboto Slab", 8f);
		this.AppTitle.ForeColor = global::System.Drawing.Color.Silver;
		this.AppTitle.Location = new global::System.Drawing.Point(14, 113);
		this.AppTitle.Name = "AppTitle";
		this.AppTitle.Size = new global::System.Drawing.Size(392, 17);
		this.AppTitle.TabIndex = 85;
		this.AppTitle.Text = "Version 1.2.34567.890";
		this.AppTitle.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
		this.button_2.BackColor = global::System.Drawing.Color.FromArgb(20, 30, 54);
		this.button_2.Cursor = global::System.Windows.Forms.Cursors.Hand;
		this.button_2.FlatAppearance.BorderColor = global::System.Drawing.Color.Silver;
		this.button_2.FlatAppearance.BorderSize = 0;
		this.button_2.FlatAppearance.MouseDownBackColor = global::System.Drawing.Color.DarkBlue;
		this.button_2.FlatAppearance.MouseOverBackColor = global::System.Drawing.Color.Blue;
		this.button_2.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
		this.button_2.Font = new global::System.Drawing.Font("Roboto Slab", 9f);
		this.button_2.ForeColor = global::System.Drawing.Color.White;
		this.button_2.Location = new global::System.Drawing.Point(1, 561);
		this.button_2.Name = "MonitorBTN";
		this.button_2.Size = new global::System.Drawing.Size(146, 54);
		this.button_2.TabIndex = 106;
		this.button_2.Text = "Monitor";
		this.button_2.UseVisualStyleBackColor = false;
		this.button_2.Click += new global::System.EventHandler(this.button_2_Click);
		this.AdditionalBTN.BackColor = global::System.Drawing.Color.FromArgb(20, 30, 54);
		this.AdditionalBTN.Cursor = global::System.Windows.Forms.Cursors.Hand;
		this.AdditionalBTN.FlatAppearance.BorderColor = global::System.Drawing.Color.Silver;
		this.AdditionalBTN.FlatAppearance.BorderSize = 0;
		this.AdditionalBTN.FlatAppearance.MouseDownBackColor = global::System.Drawing.Color.DarkBlue;
		this.AdditionalBTN.FlatAppearance.MouseOverBackColor = global::System.Drawing.Color.Blue;
		this.AdditionalBTN.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
		this.AdditionalBTN.Font = new global::System.Drawing.Font("Roboto Slab", 9f);
		this.AdditionalBTN.ForeColor = global::System.Drawing.Color.White;
		this.AdditionalBTN.Location = new global::System.Drawing.Point(146, 561);
		this.AdditionalBTN.Name = "AdditionalBTN";
		this.AdditionalBTN.Size = new global::System.Drawing.Size(130, 54);
		this.AdditionalBTN.TabIndex = 107;
		this.AdditionalBTN.Text = "Additional";
		this.AdditionalBTN.UseVisualStyleBackColor = false;
		this.AdditionalBTN.Click += new global::System.EventHandler(this.AdditionalBTN_Click);
		this.button_3.BackColor = global::System.Drawing.Color.FromArgb(20, 30, 54);
		this.button_3.Cursor = global::System.Windows.Forms.Cursors.Hand;
		this.button_3.FlatAppearance.BorderColor = global::System.Drawing.Color.Silver;
		this.button_3.FlatAppearance.BorderSize = 0;
		this.button_3.FlatAppearance.MouseDownBackColor = global::System.Drawing.Color.DarkBlue;
		this.button_3.FlatAppearance.MouseOverBackColor = global::System.Drawing.Color.Blue;
		this.button_3.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
		this.button_3.Font = new global::System.Drawing.Font("Roboto Slab", 9f);
		this.button_3.ForeColor = global::System.Drawing.Color.White;
		this.button_3.Location = new global::System.Drawing.Point(275, 561);
		this.button_3.Name = "ConfigBTN";
		this.button_3.Size = new global::System.Drawing.Size(146, 54);
		this.button_3.TabIndex = 108;
		this.button_3.Text = "Config";
		this.button_3.UseVisualStyleBackColor = false;
		this.button_3.Click += new global::System.EventHandler(this.button_3_Click);
		this.groupBox4.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
		this.groupBox4.Font = new global::System.Drawing.Font("Roboto Slab", 6f);
		this.groupBox4.ForeColor = global::System.Drawing.Color.White;
		this.groupBox4.Location = new global::System.Drawing.Point(167, 178);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new global::System.Drawing.Size(1, 58);
		this.groupBox4.TabIndex = 111;
		this.groupBox4.TabStop = false;
		this.label51.BackColor = global::System.Drawing.Color.Transparent;
		this.label51.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
		this.label51.Font = new global::System.Drawing.Font("Roboto Slab", 10f);
		this.label51.ForeColor = global::System.Drawing.Color.White;
		this.label51.Location = new global::System.Drawing.Point(179, 182);
		this.label51.Name = "label51";
		this.label51.Size = new global::System.Drawing.Size(125, 23);
		this.label51.TabIndex = 110;
		this.label51.Text = "Log Files";
		this.LogFileSize.BackColor = global::System.Drawing.Color.Transparent;
		this.LogFileSize.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
		this.LogFileSize.Font = new global::System.Drawing.Font("Roboto Slab", 18f);
		this.LogFileSize.ForeColor = global::System.Drawing.Color.White;
		this.LogFileSize.Location = new global::System.Drawing.Point(179, 205);
		this.LogFileSize.Name = "LogFileSize";
		this.LogFileSize.Size = new global::System.Drawing.Size(125, 31);
		this.LogFileSize.TabIndex = 109;
		this.LogFileSize.Text = "100MB";
		this.LogFileSize.TextAlign = global::System.Drawing.ContentAlignment.BottomLeft;
		this.AppStatus.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
		this.AppStatus.Font = new global::System.Drawing.Font("Roboto Slab", 8f);
		this.AppStatus.ForeColor = global::System.Drawing.Color.Silver;
		this.AppStatus.Location = new global::System.Drawing.Point(14, 143);
		this.AppStatus.Name = "AppStatus";
		this.AppStatus.Size = new global::System.Drawing.Size(392, 17);
		this.AppStatus.TabIndex = 112;
		this.AppStatus.Text = "Server Status";
		this.AppStatus.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
		this.timer_0.Interval = 1000;
		this.timer_0.Tick += new global::System.EventHandler(this.timer_0_Tick);
		this.FormLoaderPNL.BackColor = global::System.Drawing.Color.FromArgb(46, 51, 73);
		this.FormLoaderPNL.Location = new global::System.Drawing.Point(1, 242);
		this.FormLoaderPNL.Name = "FormLoaderPNL";
		this.FormLoaderPNL.Size = new global::System.Drawing.Size(420, 313);
		this.FormLoaderPNL.TabIndex = 120;
		this.MonitorLogo.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Stretch;
		this.MonitorLogo.Image = global::Class11.Bitmap_0;
		this.MonitorLogo.Location = new global::System.Drawing.Point(12, 12);
		this.MonitorLogo.Name = "MonitorLogo";
		this.MonitorLogo.Size = new global::System.Drawing.Size(38, 38);
		this.MonitorLogo.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.MonitorLogo.TabIndex = 94;
		this.MonitorLogo.TabStop = false;
		this.toolTip_0.ToolTipTitle = "Monitor Tips";
		this.AdditionalPanelN.BackColor = global::System.Drawing.Color.FromArgb(46, 51, 73);
		this.AdditionalPanelN.Location = new global::System.Drawing.Point(146, 555);
		this.AdditionalPanelN.Name = "AdditionalPanelN";
		this.AdditionalPanelN.Size = new global::System.Drawing.Size(130, 6);
		this.AdditionalPanelN.TabIndex = 128;
		this.NavPNL.BackColor = global::System.Drawing.Color.FromArgb(0, 126, 249);
		this.NavPNL.Location = new global::System.Drawing.Point(146, 612);
		this.NavPNL.Name = "NavPNL";
		this.NavPNL.Size = new global::System.Drawing.Size(130, 2);
		this.NavPNL.TabIndex = 129;
		this.MonitorPanelN.BackColor = global::System.Drawing.Color.FromArgb(46, 51, 73);
		this.MonitorPanelN.Location = new global::System.Drawing.Point(1, 555);
		this.MonitorPanelN.Name = "MonitorPanelN";
		this.MonitorPanelN.Size = new global::System.Drawing.Size(146, 6);
		this.MonitorPanelN.TabIndex = 129;
		this.ConfigPanelN.BackColor = global::System.Drawing.Color.FromArgb(46, 51, 73);
		this.ConfigPanelN.Location = new global::System.Drawing.Point(275, 555);
		this.ConfigPanelN.Name = "ConfigPanelN";
		this.ConfigPanelN.Size = new global::System.Drawing.Size(146, 6);
		this.ConfigPanelN.TabIndex = 129;
		this.FMonitorPanel.BackColor = global::System.Drawing.Color.FromArgb(20, 30, 54);
		this.FMonitorPanel.Location = new global::System.Drawing.Point(1, 616);
		this.FMonitorPanel.Name = "FMonitorPanel";
		this.FMonitorPanel.Size = new global::System.Drawing.Size(146, 3);
		this.FMonitorPanel.TabIndex = 130;
		this.FAdditionalPanel.BackColor = global::System.Drawing.Color.FromArgb(20, 30, 54);
		this.FAdditionalPanel.Location = new global::System.Drawing.Point(146, 616);
		this.FAdditionalPanel.Name = "FAdditionalPanel";
		this.FAdditionalPanel.Size = new global::System.Drawing.Size(130, 3);
		this.FAdditionalPanel.TabIndex = 131;
		this.FConfigPanel.BackColor = global::System.Drawing.Color.FromArgb(20, 30, 54);
		this.FConfigPanel.Location = new global::System.Drawing.Point(275, 616);
		this.FConfigPanel.Name = "FConfigPanel";
		this.FConfigPanel.Size = new global::System.Drawing.Size(146, 3);
		this.FConfigPanel.TabIndex = 131;
		this.verticalProgressBar_0.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left;
		this.verticalProgressBar_0.GEnum2_0 = global::GEnum2.None;
		this.verticalProgressBar_0.Color_0 = global::System.Drawing.Color.DarkGray;
		this.verticalProgressBar_0.ForeColor = global::System.Drawing.Color.Gray;
		this.verticalProgressBar_0.Location = new global::System.Drawing.Point(3, 8);
		this.verticalProgressBar_0.Int32_0 = 2000;
		this.verticalProgressBar_0.Int32_1 = 0;
		this.verticalProgressBar_0.Name = "MemoryVPB";
		this.verticalProgressBar_0.Size = new global::System.Drawing.Size(5, 46);
		this.verticalProgressBar_0.Int32_2 = 100;
		this.verticalProgressBar_0.GEnum1_0 = global::GEnum1.Solid;
		this.verticalProgressBar_0.TabIndex = 90;
		this.verticalProgressBar_0.Int32_3 = 1000;
		base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 15f);
		base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = global::System.Drawing.Color.FromArgb(20, 30, 54);
		this.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Center;
		base.ClientSize = new global::System.Drawing.Size(422, 620);
		base.Controls.Add(this.NavPNL);
		base.Controls.Add(this.ConfigPanelN);
		base.Controls.Add(this.AdditionalPanelN);
		base.Controls.Add(this.MonitorPanelN);
		base.Controls.Add(this.FConfigPanel);
		base.Controls.Add(this.FAdditionalPanel);
		base.Controls.Add(this.FMonitorPanel);
		base.Controls.Add(this.FormLoaderPNL);
		base.Controls.Add(this.AdditionalBTN);
		base.Controls.Add(this.AppStatus);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.label51);
		base.Controls.Add(this.LogFileSize);
		base.Controls.Add(this.button_3);
		base.Controls.Add(this.button_2);
		base.Controls.Add(this.AppTitle);
		base.Controls.Add(this.button_1);
		base.Controls.Add(this.CloseBTN);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.button_0);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.MonitorName);
		base.Controls.Add(this.MonitorLogo);
		base.Controls.Add(this.label48);
		base.Controls.Add(this.label47);
		base.Controls.Add(this.RamPercent);
		this.Font = new global::System.Drawing.Font("Roboto Slab", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.None;
		base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
		base.Name = "MainForm";
		base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "OSM-Monitor";
		base.TopMost = true;
		base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
		base.Load += new global::System.EventHandler(this.MainForm_Load);
		base.Paint += new global::System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
		this.groupBox3.ResumeLayout(false);
		((global::System.ComponentModel.ISupportInitialize)this.MonitorLogo).EndInit();
		base.ResumeLayout(false);
	}

	// Token: 0x04000102 RID: 258
	private global::System.ComponentModel.IContainer icontainer_0;

	// Token: 0x04000103 RID: 259
	private global::VerticalProgressBar verticalProgressBar_0;

	// Token: 0x04000104 RID: 260
	private global::System.Windows.Forms.Label RamPercent;

	// Token: 0x04000105 RID: 261
	private global::System.Windows.Forms.Label label47;

	// Token: 0x04000106 RID: 262
	private global::System.Windows.Forms.Label label48;

	// Token: 0x04000107 RID: 263
	private global::System.Windows.Forms.PictureBox MonitorLogo;

	// Token: 0x04000108 RID: 264
	private global::System.Windows.Forms.Label MonitorName;

	// Token: 0x04000109 RID: 265
	private global::System.Windows.Forms.GroupBox groupBox3;

	// Token: 0x0400010A RID: 266
	private global::System.Windows.Forms.Button button_0;

	// Token: 0x0400010B RID: 267
	private global::System.Windows.Forms.GroupBox groupBox2;

	// Token: 0x0400010C RID: 268
	private global::System.Windows.Forms.Button button_1;

	// Token: 0x0400010D RID: 269
	private global::System.Windows.Forms.Button CloseBTN;

	// Token: 0x0400010E RID: 270
	private global::System.Windows.Forms.Label AppTitle;

	// Token: 0x0400010F RID: 271
	private global::System.Windows.Forms.Button button_2;

	// Token: 0x04000110 RID: 272
	private global::System.Windows.Forms.Button AdditionalBTN;

	// Token: 0x04000111 RID: 273
	private global::System.Windows.Forms.Button button_3;

	// Token: 0x04000112 RID: 274
	private global::System.Windows.Forms.GroupBox groupBox4;

	// Token: 0x04000113 RID: 275
	private global::System.Windows.Forms.Label label51;

	// Token: 0x04000114 RID: 276
	private global::System.Windows.Forms.Label LogFileSize;

	// Token: 0x04000115 RID: 277
	private global::System.Windows.Forms.Label AppStatus;

	// Token: 0x04000116 RID: 278
	private global::System.Windows.Forms.Timer timer_0;

	// Token: 0x04000117 RID: 279
	private global::System.Windows.Forms.Panel FormLoaderPNL;

	// Token: 0x04000118 RID: 280
	private global::System.Windows.Forms.ToolTip toolTip_0;

	// Token: 0x04000119 RID: 281
	private global::System.Windows.Forms.Panel AdditionalPanelN;

	// Token: 0x0400011A RID: 282
	private global::System.Windows.Forms.Panel NavPNL;

	// Token: 0x0400011B RID: 283
	private global::System.Windows.Forms.Panel MonitorPanelN;

	// Token: 0x0400011C RID: 284
	private global::System.Windows.Forms.Panel ConfigPanelN;

	// Token: 0x0400011D RID: 285
	private global::System.Windows.Forms.Panel FMonitorPanel;

	// Token: 0x0400011E RID: 286
	private global::System.Windows.Forms.Panel FAdditionalPanel;

	// Token: 0x0400011F RID: 287
	private global::System.Windows.Forms.Panel FConfigPanel;
}
