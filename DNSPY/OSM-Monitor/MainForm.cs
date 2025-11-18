using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Plugin.Core.Utility;

// Token: 0x0200002E RID: 46
public partial class MainForm : Form, IMessageFilter
{
	// Token: 0x060000E0 RID: 224
	[DllImport("user32.dll")]
	public static extern int SendMessage(IntPtr intptr_0, int int_4, int int_5, int int_6);

	// Token: 0x060000E1 RID: 225
	[DllImport("user32.dll")]
	public static extern bool ReleaseCapture();

	// Token: 0x060000E2 RID: 226
	[DllImport("Gdi32.dll")]
	private static extern IntPtr CreateRoundRectRgn(int int_4, int int_5, int int_6, int int_7, int int_8, int int_9);

	// Token: 0x060000E3 RID: 227 RVA: 0x000090E8 File Offset: 0x000072E8
	public MainForm(int int_4, DirectoryInfo directoryInfo_1)
	{
		this.InitializeComponent();
		this.int_3 = int_4;
		this.directoryInfo_0 = directoryInfo_1;
		Application.AddMessageFilter(this);
		this.hashSet_0.Add(this);
		this.hashSet_0.Add(this.MonitorLogo);
		this.hashSet_0.Add(this.MonitorName);
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x00009174 File Offset: 0x00007374
	private void method_0()
	{
		this.AppTitle.Text = "Version ****";
		this.AppStatus.Text = "PLEASE WAIT...";
		this.verticalProgressBar_0.Int32_0 = GClass6.smethod_1();
		this.RamPercent.Text = "--";
		this.LogFileSize.Text = "--";
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x000091D4 File Offset: 0x000073D4
	public bool PreFilterMessage(ref Message Msg)
	{
		if (Msg.Msg == 513 && this.hashSet_0.Contains(Control.FromHandle(Msg.HWnd)))
		{
			MainForm.ReleaseCapture();
			MainForm.SendMessage(base.Handle, 161, 2, 0);
			return true;
		}
		return false;
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x00002779 File Offset: 0x00000979
	private IEnumerable<Control> method_1(Control control_0)
	{
		MainForm.Class14 @class = new MainForm.Class14(-2);
		@class.control_2 = control_0;
		return @class;
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x00009224 File Offset: 0x00007424
	private void method_2(string[] string_0, PrivateFontCollection privateFontCollection_0)
	{
		foreach (string text in string_0)
		{
			privateFontCollection_0.AddFontFile(text);
		}
	}

	// Token: 0x060000E8 RID: 232 RVA: 0x0000924C File Offset: 0x0000744C
	private string method_3(FontFamily[] fontFamily_0)
	{
		foreach (FontFamily fontFamily in fontFamily_0)
		{
			if (fontFamily.Name == this.method_4())
			{
				return fontFamily.Name;
			}
		}
		return "Consolas";
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x0000928C File Offset: 0x0000748C
	private string method_4()
	{
		string text = "";
		try
		{
			string text2 = "Config/FontSet.ini";
			if (!File.Exists(text2))
			{
				MessageBox.Show("File Not Found! " + text2, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return text;
			}
			string[] array = File.ReadAllLines(text2, Encoding.UTF8);
			foreach (string text3 in array)
			{
				if (!text3.StartsWith(";") && !text3.StartsWith("["))
				{
					text = text3;
					break;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		return text;
	}

	// Token: 0x060000EA RID: 234 RVA: 0x00009344 File Offset: 0x00007544
	private void MainForm_Load(object sender, EventArgs e)
	{
		Rectangle workingArea = Screen.GetWorkingArea(this);
		base.Location = new Point(workingArea.Right - base.Size.Width, workingArea.Bottom - base.Size.Height);
		this.FormLoaderPNL.BackColor = this.color_1;
		this.MonitorPanelN.BackColor = this.color_1;
		this.AdditionalPanelN.BackColor = this.color_1;
		this.ConfigPanelN.BackColor = this.color_1;
		this.method_0();
		using (PrivateFontCollection privateFontCollection = new PrivateFontCollection())
		{
			string[] files = Directory.GetFiles("Font/");
			if (files.Length != 0)
			{
				this.method_2(files, privateFontCollection);
				string text = this.method_3(privateFontCollection.Families);
				using (IEnumerator<Control> enumerator = this.method_1(this).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Control control = enumerator.Current;
						control.Font = new Font(text, control.Font.Size, control.Font.Style);
					}
					goto IL_136;
				}
			}
			MessageBox.Show("The Font was not found. try again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			GClass11.smethod_3(this.int_3);
		}
		IL_136:
		this.FormLoaderPNL.BringToFront();
		this.NavPNL.Width = this.FMonitorPanel.Width;
		this.NavPNL.Top = this.FMonitorPanel.Top;
		this.NavPNL.Left = this.FMonitorPanel.Left;
		this.button_2.BackColor = this.color_1;
		this.AdditionalBTN.BackColor = this.color_0;
		this.button_3.BackColor = this.color_0;
		this.FormLoaderPNL.Controls.Clear();
		FormMonitor formMonitor = new FormMonitor
		{
			Dock = DockStyle.Fill,
			TopLevel = false,
			TopMost = true
		};
		this.FormLoaderPNL.Controls.Add(formMonitor);
		formMonitor.Show();
		this.timer_0.Start();
	}

	// Token: 0x060000EB RID: 235 RVA: 0x00002789 File Offset: 0x00000989
	private void button_0_Click(object sender, EventArgs e)
	{
		this.Refresh();
	}

	// Token: 0x060000EC RID: 236 RVA: 0x00002791 File Offset: 0x00000991
	private void CloseBTN_Click(object sender, EventArgs e)
	{
		base.Close();
	}

	// Token: 0x060000ED RID: 237 RVA: 0x00002799 File Offset: 0x00000999
	private void button_1_Click(object sender, EventArgs e)
	{
		base.WindowState = FormWindowState.Minimized;
	}

	// Token: 0x060000EE RID: 238 RVA: 0x00009570 File Offset: 0x00007770
	private void MainForm_Paint(object sender, PaintEventArgs e)
	{
		Rectangle clientRectangle = base.ClientRectangle;
		clientRectangle.Inflate(0, 0);
		ControlPaint.DrawBorder(e.Graphics, clientRectangle, Color.FromArgb(255, 54, 54, 164), ButtonBorderStyle.Solid);
	}

	// Token: 0x060000EF RID: 239 RVA: 0x000095B0 File Offset: 0x000077B0
	private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		DialogResult dialogResult = MessageBox.Show("Are you sure want to quit?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		if (dialogResult == DialogResult.Yes)
		{
			GClass11.smethod_3(this.int_3);
		}
		e.Cancel = dialogResult == DialogResult.No;
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x000095EC File Offset: 0x000077EC
	private void timer_0_Tick(object sender, EventArgs e)
	{
		this.AppTitle.Text = "Version " + GClass9.string_0;
		this.AppStatus.ForeColor = (GClass9.string_1.Equals("SERVER ONLINE") ? ColorUtil.Green : (GClass9.string_1.Equals("SERVER OFFLINE") ? ColorUtil.Red : ColorUtil.White));
		this.AppStatus.Text = GClass9.string_1;
		this.verticalProgressBar_0.Int32_3 = int.Parse(GClass9.string_2);
		this.RamPercent.Text = GClass9.string_3;
		this.LogFileSize.Text = GClass9.string_4;
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x00009698 File Offset: 0x00007898
	private void button_2_Click(object sender, EventArgs e)
	{
		this.NavPNL.Width = this.FMonitorPanel.Width;
		this.NavPNL.Top = this.FMonitorPanel.Top;
		this.NavPNL.Left = this.FMonitorPanel.Left;
		this.button_2.BackColor = this.color_1;
		this.AdditionalBTN.BackColor = this.color_0;
		this.button_3.BackColor = this.color_0;
		this.FormLoaderPNL.Controls.Clear();
		FormMonitor formMonitor = new FormMonitor
		{
			Dock = DockStyle.Fill,
			TopLevel = false,
			TopMost = true
		};
		this.FormLoaderPNL.Controls.Add(formMonitor);
		formMonitor.Show();
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x0000975C File Offset: 0x0000795C
	private void AdditionalBTN_Click(object sender, EventArgs e)
	{
		this.NavPNL.Width = this.FAdditionalPanel.Width;
		this.NavPNL.Top = this.FAdditionalPanel.Top;
		this.NavPNL.Left = this.FAdditionalPanel.Left;
		this.AdditionalBTN.BackColor = this.color_1;
		this.button_2.BackColor = this.color_0;
		this.button_3.BackColor = this.color_0;
		this.FormLoaderPNL.Controls.Clear();
		FormAdditional formAdditional = new FormAdditional
		{
			Dock = DockStyle.Fill,
			TopLevel = false,
			TopMost = true
		};
		this.FormLoaderPNL.Controls.Add(formAdditional);
		formAdditional.Show();
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x00009820 File Offset: 0x00007A20
	private void button_3_Click(object sender, EventArgs e)
	{
		this.NavPNL.Width = this.FConfigPanel.Width;
		this.NavPNL.Top = this.FConfigPanel.Top;
		this.NavPNL.Left = this.FConfigPanel.Left;
		this.button_3.BackColor = this.color_1;
		this.button_2.BackColor = this.color_0;
		this.AdditionalBTN.BackColor = this.color_0;
		this.FormLoaderPNL.Controls.Clear();
		FormConfig formConfig = new FormConfig(this.directoryInfo_0)
		{
			Dock = DockStyle.Fill,
			TopLevel = false,
			TopMost = true
		};
		this.FormLoaderPNL.Controls.Add(formConfig);
		formConfig.Show();
	}

	// Token: 0x060000F4 RID: 244 RVA: 0x000027A2 File Offset: 0x000009A2
	protected virtual void Dispose(bool disposing)
	{
		if (disposing && this.icontainer_0 != null)
		{
			this.icontainer_0.Dispose();
		}
		base.Dispose(disposing);
	}

	// Token: 0x040000FA RID: 250
	public const int int_0 = 161;

	// Token: 0x040000FB RID: 251
	public const int int_1 = 2;

	// Token: 0x040000FC RID: 252
	public const int int_2 = 513;

	// Token: 0x040000FD RID: 253
	private readonly HashSet<Control> hashSet_0 = new HashSet<Control>();

	// Token: 0x040000FE RID: 254
	private readonly int int_3;

	// Token: 0x040000FF RID: 255
	private readonly DirectoryInfo directoryInfo_0;

	// Token: 0x04000100 RID: 256
	private readonly Color color_0 = Color.FromArgb(20, 30, 54);

	// Token: 0x04000101 RID: 257
	private readonly Color color_1 = Color.FromArgb(46, 51, 73);
}
