using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Plugin.Core.Utility;

public class MainForm : Form, IMessageFilter
{
	[CompilerGenerated]
	private sealed class Class14 : IEnumerable<Control>, IEnumerable, IEnumerator<Control>, IDisposable, IEnumerator
	{
		private int int_0;

		private Control control_0;

		private int int_1;

		private Control control_1;

		public Control control_2;

		private Stack<Control> stack_0;

		Control IEnumerator<Control>.Current
		{
			[DebuggerHidden]
			get
			{
				return control_0;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return control_0;
			}
		}

		[DebuggerHidden]
		public Class14(int int_2)
		{
			int_0 = int_2;
			int_1 = Environment.CurrentManagedThreadId;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			stack_0 = null;
			int_0 = -2;
		}

		private bool MoveNext()
		{
			switch (int_0)
			{
			default:
				return false;
			case 0:
				int_0 = -1;
				stack_0 = new Stack<Control>();
				stack_0.Push(control_1);
				break;
			case 1:
				int_0 = -1;
				break;
			}
			if (stack_0.Any())
			{
				Control control = stack_0.Pop();
				foreach (Control control2 in control.Controls)
				{
					stack_0.Push(control2);
				}
				control_0 = control;
				int_0 = 1;
				return true;
			}
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		[DebuggerHidden]
		IEnumerator<Control> IEnumerable<Control>.GetEnumerator()
		{
			Class14 @class;
			if (int_0 == -2 && int_1 == Environment.CurrentManagedThreadId)
			{
				int_0 = 0;
				@class = this;
			}
			else
			{
				@class = new Class14(0);
			}
			@class.control_1 = control_2;
			return @class;
		}

		[DebuggerHidden]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<Control>)this).GetEnumerator();
		}
	}

	public const int int_0 = 161;

	public const int int_1 = 2;

	public const int int_2 = 513;

	private readonly HashSet<Control> hashSet_0 = new HashSet<Control>();

	private readonly int int_3;

	private readonly DirectoryInfo directoryInfo_0;

	private readonly Color color_0 = Color.FromArgb(20, 30, 54);

	private readonly Color color_1 = Color.FromArgb(46, 51, 73);

	private IContainer icontainer_0;

	private VerticalProgressBar verticalProgressBar_0;

	private Label RamPercent;

	private Label label47;

	private Label label48;

	private PictureBox MonitorLogo;

	private Label MonitorName;

	private GroupBox groupBox3;

	private Button button_0;

	private GroupBox groupBox2;

	private Button button_1;

	private Button CloseBTN;

	private Label AppTitle;

	private Button button_2;

	private Button AdditionalBTN;

	private Button button_3;

	private GroupBox groupBox4;

	private Label label51;

	private Label LogFileSize;

	private Label AppStatus;

	private Timer timer_0;

	private Panel FormLoaderPNL;

	private ToolTip toolTip_0;

	private Panel AdditionalPanelN;

	private Panel NavPNL;

	private Panel MonitorPanelN;

	private Panel ConfigPanelN;

	private Panel FMonitorPanel;

	private Panel FAdditionalPanel;

	private Panel FConfigPanel;

	[DllImport("user32.dll")]
	public static extern int SendMessage(IntPtr intptr_0, int int_4, int int_5, int int_6);

	[DllImport("user32.dll")]
	public static extern bool ReleaseCapture();

	[DllImport("Gdi32.dll")]
	private static extern IntPtr CreateRoundRectRgn(int int_4, int int_5, int int_6, int int_7, int int_8, int int_9);

	public MainForm(int int_4, DirectoryInfo directoryInfo_1)
	{
		InitializeComponent();
		int_3 = int_4;
		directoryInfo_0 = directoryInfo_1;
		Application.AddMessageFilter(this);
		hashSet_0.Add(this);
		hashSet_0.Add(MonitorLogo);
		hashSet_0.Add(MonitorName);
	}

	private void method_0()
	{
		AppTitle.Text = "Version ****";
		AppStatus.Text = "PLEASE WAIT...";
		verticalProgressBar_0.Int32_0 = GClass6.smethod_1();
		RamPercent.Text = "--";
		LogFileSize.Text = "--";
	}

	public bool PreFilterMessage(ref Message Msg)
	{
		if (Msg.Msg == 513 && hashSet_0.Contains(Control.FromHandle(Msg.HWnd)))
		{
			ReleaseCapture();
			SendMessage(base.Handle, 161, 2, 0);
			return true;
		}
		return false;
	}

	[IteratorStateMachine(typeof(Class14))]
	private IEnumerable<Control> method_1(Control control_0)
	{
		//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
		return new Class14(-2)
		{
			control_2 = control_0
		};
	}

	private void method_2(string[] string_0, PrivateFontCollection privateFontCollection_0)
	{
		foreach (string filename in string_0)
		{
			privateFontCollection_0.AddFontFile(filename);
		}
	}

	private string method_3(FontFamily[] fontFamily_0)
	{
		foreach (FontFamily fontFamily in fontFamily_0)
		{
			if (fontFamily.Name == method_4())
			{
				return fontFamily.Name;
			}
		}
		return "Consolas";
	}

	private string method_4()
	{
		string result = "";
		try
		{
			string text = "Config/FontSet.ini";
			if (!File.Exists(text))
			{
				MessageBox.Show("File Not Found! " + text, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return result;
			}
			string[] array = File.ReadAllLines(text, Encoding.UTF8);
			string[] array2 = array;
			foreach (string text2 in array2)
			{
				if (!text2.StartsWith(";") && !text2.StartsWith("["))
				{
					result = text2;
					return result;
				}
			}
			return result;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return result;
		}
	}

	private void MainForm_Load(object sender, EventArgs e)
	{
		Rectangle workingArea = Screen.GetWorkingArea(this);
		base.Location = new Point(workingArea.Right - base.Size.Width, workingArea.Bottom - base.Size.Height);
		FormLoaderPNL.BackColor = color_1;
		MonitorPanelN.BackColor = color_1;
		AdditionalPanelN.BackColor = color_1;
		ConfigPanelN.BackColor = color_1;
		method_0();
		using (PrivateFontCollection privateFontCollection = new PrivateFontCollection())
		{
			string[] files = Directory.GetFiles("Font/");
			if (files.Length != 0)
			{
				method_2(files, privateFontCollection);
				string familyName = method_3(privateFontCollection.Families);
				foreach (Control item in method_1(this))
				{
					item.Font = new Font(familyName, item.Font.Size, item.Font.Style);
				}
			}
			else
			{
				MessageBox.Show("The Font was not found. try again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				GClass11.smethod_3(int_3);
			}
		}
		FormLoaderPNL.BringToFront();
		NavPNL.Width = FMonitorPanel.Width;
		NavPNL.Top = FMonitorPanel.Top;
		NavPNL.Left = FMonitorPanel.Left;
		button_2.BackColor = color_1;
		AdditionalBTN.BackColor = color_0;
		button_3.BackColor = color_0;
		FormLoaderPNL.Controls.Clear();
		FormMonitor formMonitor = new FormMonitor
		{
			Dock = DockStyle.Fill,
			TopLevel = false,
			TopMost = true
		};
		FormLoaderPNL.Controls.Add(formMonitor);
		formMonitor.Show();
		timer_0.Start();
	}

	private void button_0_Click(object sender, EventArgs e)
	{
		Refresh();
	}

	private void CloseBTN_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void button_1_Click(object sender, EventArgs e)
	{
		base.WindowState = FormWindowState.Minimized;
	}

	private void MainForm_Paint(object sender, PaintEventArgs e)
	{
		Rectangle clientRectangle = base.ClientRectangle;
		clientRectangle.Inflate(0, 0);
		ControlPaint.DrawBorder(e.Graphics, clientRectangle, Color.FromArgb(255, 54, 54, 164), ButtonBorderStyle.Solid);
	}

	private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		DialogResult dialogResult = MessageBox.Show("Are you sure want to quit?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		if (dialogResult == DialogResult.Yes)
		{
			GClass11.smethod_3(int_3);
		}
		e.Cancel = dialogResult == DialogResult.No;
	}

	private void timer_0_Tick(object sender, EventArgs e)
	{
		AppTitle.Text = "Version " + GClass9.string_0;
		AppStatus.ForeColor = (GClass9.string_1.Equals("SERVER ONLINE") ? ColorUtil.Green : (GClass9.string_1.Equals("SERVER OFFLINE") ? ColorUtil.Red : ColorUtil.White));
		AppStatus.Text = GClass9.string_1;
		verticalProgressBar_0.Int32_3 = int.Parse(GClass9.string_2);
		RamPercent.Text = GClass9.string_3;
		LogFileSize.Text = GClass9.string_4;
	}

	private void button_2_Click(object sender, EventArgs e)
	{
		NavPNL.Width = FMonitorPanel.Width;
		NavPNL.Top = FMonitorPanel.Top;
		NavPNL.Left = FMonitorPanel.Left;
		button_2.BackColor = color_1;
		AdditionalBTN.BackColor = color_0;
		button_3.BackColor = color_0;
		FormLoaderPNL.Controls.Clear();
		FormMonitor formMonitor = new FormMonitor
		{
			Dock = DockStyle.Fill,
			TopLevel = false,
			TopMost = true
		};
		FormLoaderPNL.Controls.Add(formMonitor);
		formMonitor.Show();
	}

	private void AdditionalBTN_Click(object sender, EventArgs e)
	{
		NavPNL.Width = FAdditionalPanel.Width;
		NavPNL.Top = FAdditionalPanel.Top;
		NavPNL.Left = FAdditionalPanel.Left;
		AdditionalBTN.BackColor = color_1;
		button_2.BackColor = color_0;
		button_3.BackColor = color_0;
		FormLoaderPNL.Controls.Clear();
		FormAdditional formAdditional = new FormAdditional
		{
			Dock = DockStyle.Fill,
			TopLevel = false,
			TopMost = true
		};
		FormLoaderPNL.Controls.Add(formAdditional);
		formAdditional.Show();
	}

	private void button_3_Click(object sender, EventArgs e)
	{
		NavPNL.Width = FConfigPanel.Width;
		NavPNL.Top = FConfigPanel.Top;
		NavPNL.Left = FConfigPanel.Left;
		button_3.BackColor = color_1;
		button_2.BackColor = color_0;
		AdditionalBTN.BackColor = color_0;
		FormLoaderPNL.Controls.Clear();
		FormConfig formConfig = new FormConfig(directoryInfo_0)
		{
			Dock = DockStyle.Fill,
			TopLevel = false,
			TopMost = true
		};
		FormLoaderPNL.Controls.Add(formConfig);
		formConfig.Show();
	}

	void Form.Dispose(bool disposing)
	{
		if (disposing && icontainer_0 != null)
		{
			icontainer_0.Dispose();
		}
		Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.icontainer_0 = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
		this.RamPercent = new System.Windows.Forms.Label();
		this.label47 = new System.Windows.Forms.Label();
		this.label48 = new System.Windows.Forms.Label();
		this.MonitorName = new System.Windows.Forms.Label();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.button_0 = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.button_1 = new System.Windows.Forms.Button();
		this.CloseBTN = new System.Windows.Forms.Button();
		this.AppTitle = new System.Windows.Forms.Label();
		this.button_2 = new System.Windows.Forms.Button();
		this.AdditionalBTN = new System.Windows.Forms.Button();
		this.button_3 = new System.Windows.Forms.Button();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.label51 = new System.Windows.Forms.Label();
		this.LogFileSize = new System.Windows.Forms.Label();
		this.AppStatus = new System.Windows.Forms.Label();
		this.timer_0 = new System.Windows.Forms.Timer(this.icontainer_0);
		this.FormLoaderPNL = new System.Windows.Forms.Panel();
		this.MonitorLogo = new System.Windows.Forms.PictureBox();
		this.toolTip_0 = new System.Windows.Forms.ToolTip(this.icontainer_0);
		this.AdditionalPanelN = new System.Windows.Forms.Panel();
		this.NavPNL = new System.Windows.Forms.Panel();
		this.MonitorPanelN = new System.Windows.Forms.Panel();
		this.ConfigPanelN = new System.Windows.Forms.Panel();
		this.FMonitorPanel = new System.Windows.Forms.Panel();
		this.FAdditionalPanel = new System.Windows.Forms.Panel();
		this.FConfigPanel = new System.Windows.Forms.Panel();
		this.verticalProgressBar_0 = new VerticalProgressBar();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.MonitorLogo).BeginInit();
		base.SuspendLayout();
		this.RamPercent.BackColor = System.Drawing.Color.Transparent;
		this.RamPercent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.RamPercent.Font = new System.Drawing.Font("Roboto Slab", 18f);
		this.RamPercent.ForeColor = System.Drawing.Color.White;
		this.RamPercent.Location = new System.Drawing.Point(33, 205);
		this.RamPercent.Name = "RamPercent";
		this.RamPercent.Size = new System.Drawing.Size(125, 31);
		this.RamPercent.TabIndex = 91;
		this.RamPercent.Text = "99.9%";
		this.RamPercent.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
		this.label47.BackColor = System.Drawing.Color.Transparent;
		this.label47.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.label47.Font = new System.Drawing.Font("Roboto Slab", 10f);
		this.label47.ForeColor = System.Drawing.Color.White;
		this.label47.Location = new System.Drawing.Point(33, 182);
		this.label47.Name = "label47";
		this.label47.Size = new System.Drawing.Size(125, 23);
		this.label47.TabIndex = 92;
		this.label47.Text = "Memory Usage";
		this.label48.BackColor = System.Drawing.Color.Transparent;
		this.label48.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.label48.Font = new System.Drawing.Font("Roboto Slab SemiBold", 14f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
		this.label48.ForeColor = System.Drawing.Color.White;
		this.label48.Location = new System.Drawing.Point(12, 84);
		this.label48.Name = "label48";
		this.label48.Size = new System.Drawing.Size(394, 29);
		this.label48.TabIndex = 93;
		this.label48.Text = "Please Monitor Your Server";
		this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.MonitorName.BackColor = System.Drawing.Color.Transparent;
		this.MonitorName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.MonitorName.Font = new System.Drawing.Font("Roboto Slab", 10f, System.Drawing.FontStyle.Bold);
		this.MonitorName.ForeColor = System.Drawing.Color.White;
		this.MonitorName.Location = new System.Drawing.Point(56, 12);
		this.MonitorName.Name = "MonitorName";
		this.MonitorName.Size = new System.Drawing.Size(149, 38);
		this.MonitorName.TabIndex = 95;
		this.MonitorName.Text = "OSM - Monitor";
		this.MonitorName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.groupBox3.Controls.Add(this.verticalProgressBar_0);
		this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.groupBox3.Font = new System.Drawing.Font("Roboto Slab", 6f);
		this.groupBox3.ForeColor = System.Drawing.Color.White;
		this.groupBox3.Location = new System.Drawing.Point(16, 178);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(11, 58);
		this.groupBox3.TabIndex = 96;
		this.groupBox3.TabStop = false;
		this.button_0.BackColor = System.Drawing.Color.FromArgb(0, 126, 249);
		this.button_0.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_0.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
		this.button_0.FlatAppearance.BorderSize = 0;
		this.button_0.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkBlue;
		this.button_0.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
		this.button_0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_0.Font = new System.Drawing.Font("Roboto Slab", 10f);
		this.button_0.ForeColor = System.Drawing.Color.White;
		this.button_0.Location = new System.Drawing.Point(313, 182);
		this.button_0.Name = "RefreshBTN";
		this.button_0.Size = new System.Drawing.Size(93, 54);
		this.button_0.TabIndex = 1;
		this.button_0.Text = "Refresh";
		this.button_0.UseVisualStyleBackColor = false;
		this.button_0.Click += new System.EventHandler(button_0_Click);
		this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.groupBox2.Font = new System.Drawing.Font("Roboto Slab", 6f);
		this.groupBox2.ForeColor = System.Drawing.Color.White;
		this.groupBox2.Location = new System.Drawing.Point(16, 163);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(390, 1);
		this.groupBox2.TabIndex = 97;
		this.groupBox2.TabStop = false;
		this.button_1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(20, 30, 54);
		this.button_1.FlatAppearance.BorderSize = 0;
		this.button_1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_1.Font = new System.Drawing.Font("Roboto Slab", 14f);
		this.button_1.ForeColor = System.Drawing.Color.White;
		this.button_1.Location = new System.Drawing.Point(328, 1);
		this.button_1.Name = "MinimizeBTN";
		this.button_1.Size = new System.Drawing.Size(38, 48);
		this.button_1.TabIndex = 99;
		this.button_1.Text = "_";
		this.button_1.UseVisualStyleBackColor = true;
		this.button_1.Click += new System.EventHandler(button_1_Click);
		this.CloseBTN.Cursor = System.Windows.Forms.Cursors.Hand;
		this.CloseBTN.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(20, 30, 54);
		this.CloseBTN.FlatAppearance.BorderSize = 0;
		this.CloseBTN.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
		this.CloseBTN.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
		this.CloseBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.CloseBTN.Font = new System.Drawing.Font("Roboto Slab", 15f);
		this.CloseBTN.ForeColor = System.Drawing.Color.White;
		this.CloseBTN.Location = new System.Drawing.Point(369, 1);
		this.CloseBTN.Name = "CloseBTN";
		this.CloseBTN.Size = new System.Drawing.Size(52, 48);
		this.CloseBTN.TabIndex = 98;
		this.CloseBTN.Text = "X";
		this.CloseBTN.UseVisualStyleBackColor = true;
		this.CloseBTN.Click += new System.EventHandler(CloseBTN_Click);
		this.AppTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.AppTitle.Font = new System.Drawing.Font("Roboto Slab", 8f);
		this.AppTitle.ForeColor = System.Drawing.Color.Silver;
		this.AppTitle.Location = new System.Drawing.Point(14, 113);
		this.AppTitle.Name = "AppTitle";
		this.AppTitle.Size = new System.Drawing.Size(392, 17);
		this.AppTitle.TabIndex = 85;
		this.AppTitle.Text = "Version 1.2.34567.890";
		this.AppTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.button_2.BackColor = System.Drawing.Color.FromArgb(20, 30, 54);
		this.button_2.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_2.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
		this.button_2.FlatAppearance.BorderSize = 0;
		this.button_2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkBlue;
		this.button_2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
		this.button_2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_2.Font = new System.Drawing.Font("Roboto Slab", 9f);
		this.button_2.ForeColor = System.Drawing.Color.White;
		this.button_2.Location = new System.Drawing.Point(1, 561);
		this.button_2.Name = "MonitorBTN";
		this.button_2.Size = new System.Drawing.Size(146, 54);
		this.button_2.TabIndex = 106;
		this.button_2.Text = "Monitor";
		this.button_2.UseVisualStyleBackColor = false;
		this.button_2.Click += new System.EventHandler(button_2_Click);
		this.AdditionalBTN.BackColor = System.Drawing.Color.FromArgb(20, 30, 54);
		this.AdditionalBTN.Cursor = System.Windows.Forms.Cursors.Hand;
		this.AdditionalBTN.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
		this.AdditionalBTN.FlatAppearance.BorderSize = 0;
		this.AdditionalBTN.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkBlue;
		this.AdditionalBTN.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
		this.AdditionalBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.AdditionalBTN.Font = new System.Drawing.Font("Roboto Slab", 9f);
		this.AdditionalBTN.ForeColor = System.Drawing.Color.White;
		this.AdditionalBTN.Location = new System.Drawing.Point(146, 561);
		this.AdditionalBTN.Name = "AdditionalBTN";
		this.AdditionalBTN.Size = new System.Drawing.Size(130, 54);
		this.AdditionalBTN.TabIndex = 107;
		this.AdditionalBTN.Text = "Additional";
		this.AdditionalBTN.UseVisualStyleBackColor = false;
		this.AdditionalBTN.Click += new System.EventHandler(AdditionalBTN_Click);
		this.button_3.BackColor = System.Drawing.Color.FromArgb(20, 30, 54);
		this.button_3.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_3.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
		this.button_3.FlatAppearance.BorderSize = 0;
		this.button_3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkBlue;
		this.button_3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
		this.button_3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_3.Font = new System.Drawing.Font("Roboto Slab", 9f);
		this.button_3.ForeColor = System.Drawing.Color.White;
		this.button_3.Location = new System.Drawing.Point(275, 561);
		this.button_3.Name = "ConfigBTN";
		this.button_3.Size = new System.Drawing.Size(146, 54);
		this.button_3.TabIndex = 108;
		this.button_3.Text = "Config";
		this.button_3.UseVisualStyleBackColor = false;
		this.button_3.Click += new System.EventHandler(button_3_Click);
		this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.groupBox4.Font = new System.Drawing.Font("Roboto Slab", 6f);
		this.groupBox4.ForeColor = System.Drawing.Color.White;
		this.groupBox4.Location = new System.Drawing.Point(167, 178);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(1, 58);
		this.groupBox4.TabIndex = 111;
		this.groupBox4.TabStop = false;
		this.label51.BackColor = System.Drawing.Color.Transparent;
		this.label51.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.label51.Font = new System.Drawing.Font("Roboto Slab", 10f);
		this.label51.ForeColor = System.Drawing.Color.White;
		this.label51.Location = new System.Drawing.Point(179, 182);
		this.label51.Name = "label51";
		this.label51.Size = new System.Drawing.Size(125, 23);
		this.label51.TabIndex = 110;
		this.label51.Text = "Log Files";
		this.LogFileSize.BackColor = System.Drawing.Color.Transparent;
		this.LogFileSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.LogFileSize.Font = new System.Drawing.Font("Roboto Slab", 18f);
		this.LogFileSize.ForeColor = System.Drawing.Color.White;
		this.LogFileSize.Location = new System.Drawing.Point(179, 205);
		this.LogFileSize.Name = "LogFileSize";
		this.LogFileSize.Size = new System.Drawing.Size(125, 31);
		this.LogFileSize.TabIndex = 109;
		this.LogFileSize.Text = "100MB";
		this.LogFileSize.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
		this.AppStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.AppStatus.Font = new System.Drawing.Font("Roboto Slab", 8f);
		this.AppStatus.ForeColor = System.Drawing.Color.Silver;
		this.AppStatus.Location = new System.Drawing.Point(14, 143);
		this.AppStatus.Name = "AppStatus";
		this.AppStatus.Size = new System.Drawing.Size(392, 17);
		this.AppStatus.TabIndex = 112;
		this.AppStatus.Text = "Server Status";
		this.AppStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.timer_0.Interval = 1000;
		this.timer_0.Tick += new System.EventHandler(timer_0_Tick);
		this.FormLoaderPNL.BackColor = System.Drawing.Color.FromArgb(46, 51, 73);
		this.FormLoaderPNL.Location = new System.Drawing.Point(1, 242);
		this.FormLoaderPNL.Name = "FormLoaderPNL";
		this.FormLoaderPNL.Size = new System.Drawing.Size(420, 313);
		this.FormLoaderPNL.TabIndex = 120;
		this.MonitorLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.MonitorLogo.Image = Class11.Bitmap_0;
		this.MonitorLogo.Location = new System.Drawing.Point(12, 12);
		this.MonitorLogo.Name = "MonitorLogo";
		this.MonitorLogo.Size = new System.Drawing.Size(38, 38);
		this.MonitorLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.MonitorLogo.TabIndex = 94;
		this.MonitorLogo.TabStop = false;
		this.toolTip_0.ToolTipTitle = "Monitor Tips";
		this.AdditionalPanelN.BackColor = System.Drawing.Color.FromArgb(46, 51, 73);
		this.AdditionalPanelN.Location = new System.Drawing.Point(146, 555);
		this.AdditionalPanelN.Name = "AdditionalPanelN";
		this.AdditionalPanelN.Size = new System.Drawing.Size(130, 6);
		this.AdditionalPanelN.TabIndex = 128;
		this.NavPNL.BackColor = System.Drawing.Color.FromArgb(0, 126, 249);
		this.NavPNL.Location = new System.Drawing.Point(146, 612);
		this.NavPNL.Name = "NavPNL";
		this.NavPNL.Size = new System.Drawing.Size(130, 2);
		this.NavPNL.TabIndex = 129;
		this.MonitorPanelN.BackColor = System.Drawing.Color.FromArgb(46, 51, 73);
		this.MonitorPanelN.Location = new System.Drawing.Point(1, 555);
		this.MonitorPanelN.Name = "MonitorPanelN";
		this.MonitorPanelN.Size = new System.Drawing.Size(146, 6);
		this.MonitorPanelN.TabIndex = 129;
		this.ConfigPanelN.BackColor = System.Drawing.Color.FromArgb(46, 51, 73);
		this.ConfigPanelN.Location = new System.Drawing.Point(275, 555);
		this.ConfigPanelN.Name = "ConfigPanelN";
		this.ConfigPanelN.Size = new System.Drawing.Size(146, 6);
		this.ConfigPanelN.TabIndex = 129;
		this.FMonitorPanel.BackColor = System.Drawing.Color.FromArgb(20, 30, 54);
		this.FMonitorPanel.Location = new System.Drawing.Point(1, 616);
		this.FMonitorPanel.Name = "FMonitorPanel";
		this.FMonitorPanel.Size = new System.Drawing.Size(146, 3);
		this.FMonitorPanel.TabIndex = 130;
		this.FAdditionalPanel.BackColor = System.Drawing.Color.FromArgb(20, 30, 54);
		this.FAdditionalPanel.Location = new System.Drawing.Point(146, 616);
		this.FAdditionalPanel.Name = "FAdditionalPanel";
		this.FAdditionalPanel.Size = new System.Drawing.Size(130, 3);
		this.FAdditionalPanel.TabIndex = 131;
		this.FConfigPanel.BackColor = System.Drawing.Color.FromArgb(20, 30, 54);
		this.FConfigPanel.Location = new System.Drawing.Point(275, 616);
		this.FConfigPanel.Name = "FConfigPanel";
		this.FConfigPanel.Size = new System.Drawing.Size(146, 3);
		this.FConfigPanel.TabIndex = 131;
		this.verticalProgressBar_0.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.verticalProgressBar_0.GEnum2_0 = GEnum2.None;
		this.verticalProgressBar_0.Color_0 = System.Drawing.Color.DarkGray;
		this.verticalProgressBar_0.ForeColor = System.Drawing.Color.Gray;
		this.verticalProgressBar_0.Location = new System.Drawing.Point(3, 8);
		this.verticalProgressBar_0.Int32_0 = 2000;
		this.verticalProgressBar_0.Int32_1 = 0;
		this.verticalProgressBar_0.Name = "MemoryVPB";
		this.verticalProgressBar_0.Size = new System.Drawing.Size(5, 46);
		this.verticalProgressBar_0.Int32_2 = 100;
		this.verticalProgressBar_0.GEnum1_0 = GEnum1.Solid;
		this.verticalProgressBar_0.TabIndex = 90;
		this.verticalProgressBar_0.Int32_3 = 1000;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(20, 30, 54);
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		base.ClientSize = new System.Drawing.Size(422, 620);
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
		this.Font = new System.Drawing.Font("Roboto Slab", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "MainForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "OSM-Monitor";
		base.TopMost = true;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(MainForm_FormClosing);
		base.Load += new System.EventHandler(MainForm_Load);
		base.Paint += new System.Windows.Forms.PaintEventHandler(MainForm_Paint);
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.MonitorLogo).EndInit();
		base.ResumeLayout(false);
	}
}
