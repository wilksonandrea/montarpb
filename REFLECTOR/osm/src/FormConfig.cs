using Plugin.Core;
using Plugin.Core.Filters;
using Plugin.Core.JSON;
using Plugin.Core.Managers;
using Plugin.Core.Settings;
using Plugin.Core.XML;
using Server.Auth.Data.XML;
using Server.Game;
using Server.Game.Data.XML;
using Server.Match.Data.XML;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

public class FormConfig : Form
{
    private readonly DirectoryInfo directoryInfo_0;
    private IContainer icontainer_0;
    private Button OpenConfigBTN;
    private RadioButton HighRB;
    private RadioButton DefaultRB;
    private Button button_0;
    private Button ChangeLogBTN;
    private Button button_1;
    private Button button_2;
    private Button button_3;
    private Button button_4;
    private Button button_5;

    public FormConfig(DirectoryInfo directoryInfo_1)
    {
        this.InitializeComponent();
        this.directoryInfo_0 = directoryInfo_1;
    }

    private void button_0_Click(object sender, EventArgs e)
    {
        new Thread(new ThreadStart(this.method_1)).Start();
        MessageBox.Show("Logs has been cleared!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void button_1_Click(object sender, EventArgs e)
    {
        new Thread(Class12.<>9__8_0 ??= new ThreadStart(Class12.<>9.method_1)).Start();
        MessageBox.Show("Config successfully Reloaded!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void button_2_Click(object sender, EventArgs e)
    {
        new Thread(Class12.<>9__10_0 ??= new ThreadStart(Class12.<>9.method_3)).Start();
        MessageBox.Show("Events Sucessfully reloaded.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void button_3_Click(object sender, EventArgs e)
    {
        new Thread(Class12.<>9__12_0 ??= new ThreadStart(Class12.<>9.method_5)).Start();
        MessageBox.Show("Server Data Sucessfully reloaded.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void button_4_Click(object sender, EventArgs e)
    {
        new Thread(Class12.<>9__11_0 ??= new ThreadStart(Class12.<>9.method_4)).Start();
        MessageBox.Show("Tournament Rule Sucessfully reloaded.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void button_5_Click(object sender, EventArgs e)
    {
        new Thread(Class12.<>9__9_0 ??= new ThreadStart(Class12.<>9.method_2)).Start();
        MessageBox.Show("Shop Sucessfully reloaded.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void ChangeLogBTN_Click(object sender, EventArgs e)
    {
        Class13 class2 = new Class13 {
            configEngine_0 = new ConfigEngine("Config/Settings.ini", FileAccess.ReadWrite)
        };
        if (!ConfigLoader.ShowMoreInfo)
        {
            ConfigLoader.ShowMoreInfo = true;
            this.method_0(true, false);
        }
        else
        {
            ConfigLoader.ShowMoreInfo = false;
            this.method_0(false, true);
        }
        new Thread(new ThreadStart(class2.method_0)).Start();
        MessageBox.Show("Logs mode changed to " + (ConfigLoader.ShowMoreInfo ? "High." : "Default."), "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void FormConfig_Load(object sender, EventArgs e)
    {
        float num = float.Parse("10") / 100f;
        this.BackgroundImage = GClass5.smethod_0().method_0(Class11.Bitmap_0, num);
        this.BackgroundImageLayout = ImageLayout.Center;
        if (ConfigLoader.ShowMoreInfo)
        {
            this.method_0(true, false);
        }
        else
        {
            this.method_0(false, true);
        }
    }

    private void InitializeComponent()
    {
        ComponentResourceManager manager = new ComponentResourceManager(typeof(FormConfig));
        this.HighRB = new RadioButton();
        this.DefaultRB = new RadioButton();
        this.button_0 = new Button();
        this.ChangeLogBTN = new Button();
        this.button_1 = new Button();
        this.button_2 = new Button();
        this.button_3 = new Button();
        this.button_4 = new Button();
        this.button_5 = new Button();
        this.OpenConfigBTN = new Button();
        base.SuspendLayout();
        this.HighRB.AutoCheck = false;
        this.HighRB.AutoSize = true;
        this.HighRB.ForeColor = Color.Silver;
        this.HighRB.Location = new Point(0x69, 0x29);
        this.HighRB.Name = "HighRB";
        this.HighRB.Size = new Size(0x47, 0x13);
        this.HighRB.TabIndex = 0x88;
        this.HighRB.Text = "High Log";
        this.HighRB.UseVisualStyleBackColor = true;
        this.DefaultRB.AutoCheck = false;
        this.DefaultRB.AutoSize = true;
        this.DefaultRB.Checked = true;
        this.DefaultRB.Cursor = Cursors.Default;
        this.DefaultRB.ForeColor = Color.Silver;
        this.DefaultRB.Location = new Point(15, 0x29);
        this.DefaultRB.Name = "DefaultRB";
        this.DefaultRB.Size = new Size(0x54, 0x13);
        this.DefaultRB.TabIndex = 0x87;
        this.DefaultRB.TabStop = true;
        this.DefaultRB.Text = "Default Log";
        this.DefaultRB.UseVisualStyleBackColor = true;
        this.button_0.Cursor = Cursors.Hand;
        this.button_0.FlatAppearance.BorderColor = Color.Gold;
        this.button_0.FlatStyle = FlatStyle.Flat;
        this.button_0.Font = new Font("Roboto Slab", 8f);
        this.button_0.ForeColor = Color.Gold;
        this.button_0.Location = new Point(15, 160);
        this.button_0.Name = "ClearLogBTN";
        this.button_0.Size = new Size(0xa1, 0x45);
        this.button_0.TabIndex = 0x86;
        this.button_0.Text = "Clear Log Files";
        this.button_0.UseVisualStyleBackColor = true;
        this.button_0.Click += new EventHandler(this.button_0_Click);
        this.ChangeLogBTN.Cursor = Cursors.Hand;
        this.ChangeLogBTN.FlatAppearance.BorderColor = Color.Gold;
        this.ChangeLogBTN.FlatStyle = FlatStyle.Flat;
        this.ChangeLogBTN.Font = new Font("Roboto Slab", 8f);
        this.ChangeLogBTN.ForeColor = Color.Gold;
        this.ChangeLogBTN.Location = new Point(15, 0x4a);
        this.ChangeLogBTN.Name = "ChangeLogBTN";
        this.ChangeLogBTN.Size = new Size(0xa1, 70);
        this.ChangeLogBTN.TabIndex = 0x85;
        this.ChangeLogBTN.Text = "Change Log Mode";
        this.ChangeLogBTN.UseVisualStyleBackColor = true;
        this.ChangeLogBTN.Click += new EventHandler(this.ChangeLogBTN_Click);
        this.button_1.Cursor = Cursors.Hand;
        this.button_1.FlatStyle = FlatStyle.Flat;
        this.button_1.Font = new Font("Roboto Slab", 8f);
        this.button_1.ForeColor = Color.Silver;
        this.button_1.Location = new Point(0xb6, 0x4a);
        this.button_1.Name = "Reload1BTN";
        this.button_1.Size = new Size(0xdf, 0x1a);
        this.button_1.TabIndex = 0x80;
        this.button_1.Text = "Reload Config";
        this.button_1.UseVisualStyleBackColor = true;
        this.button_1.Click += new EventHandler(this.button_1_Click);
        this.button_2.Cursor = Cursors.Hand;
        this.button_2.FlatStyle = FlatStyle.Flat;
        this.button_2.Font = new Font("Roboto Slab", 8f);
        this.button_2.ForeColor = Color.Silver;
        this.button_2.Location = new Point(0xb6, 0x75);
        this.button_2.Name = "Reload3BTN";
        this.button_2.Size = new Size(0xdf, 0x1a);
        this.button_2.TabIndex = 130;
        this.button_2.Text = "Reload Events";
        this.button_2.UseVisualStyleBackColor = true;
        this.button_2.Click += new EventHandler(this.button_2_Click);
        this.button_3.Cursor = Cursors.Hand;
        this.button_3.FlatStyle = FlatStyle.Flat;
        this.button_3.Font = new Font("Roboto Slab", 8f);
        this.button_3.ForeColor = Color.Silver;
        this.button_3.Location = new Point(15, 0xf6);
        this.button_3.Name = "Reload5BTN";
        this.button_3.Size = new Size(0x166, 0x1a);
        this.button_3.TabIndex = 0x84;
        this.button_3.Text = "Reload Attachments";
        this.button_3.UseVisualStyleBackColor = true;
        this.button_3.Click += new EventHandler(this.button_3_Click);
        this.button_4.Cursor = Cursors.Hand;
        this.button_4.FlatStyle = FlatStyle.Flat;
        this.button_4.Font = new Font("Roboto Slab", 8f);
        this.button_4.ForeColor = Color.Silver;
        this.button_4.Location = new Point(0xb6, 0xcb);
        this.button_4.Name = "Reload4BTN";
        this.button_4.Size = new Size(0xdf, 0x1a);
        this.button_4.TabIndex = 0x83;
        this.button_4.Text = "Reload Item Rules";
        this.button_4.UseVisualStyleBackColor = true;
        this.button_4.Click += new EventHandler(this.button_4_Click);
        this.button_5.Cursor = Cursors.Hand;
        this.button_5.FlatStyle = FlatStyle.Flat;
        this.button_5.Font = new Font("Roboto Slab", 8f);
        this.button_5.ForeColor = Color.Silver;
        this.button_5.Location = new Point(0xb6, 160);
        this.button_5.Name = "Reload2BTN";
        this.button_5.Size = new Size(0xdf, 0x1a);
        this.button_5.TabIndex = 0x81;
        this.button_5.Text = "Reload Shop";
        this.button_5.UseVisualStyleBackColor = true;
        this.button_5.Click += new EventHandler(this.button_5_Click);
        this.OpenConfigBTN.BackgroundImage = (Image) manager.GetObject("OpenConfigBTN.BackgroundImage");
        this.OpenConfigBTN.BackgroundImageLayout = ImageLayout.Stretch;
        this.OpenConfigBTN.Cursor = Cursors.Hand;
        this.OpenConfigBTN.FlatStyle = FlatStyle.Flat;
        this.OpenConfigBTN.Font = new Font("Roboto Slab", 8f);
        this.OpenConfigBTN.ForeColor = Color.Silver;
        this.OpenConfigBTN.Location = new Point(0x17b, 0xf6);
        this.OpenConfigBTN.Name = "OpenConfigBTN";
        this.OpenConfigBTN.Size = new Size(0x1a, 0x1a);
        this.OpenConfigBTN.TabIndex = 0x89;
        this.OpenConfigBTN.UseVisualStyleBackColor = true;
        this.OpenConfigBTN.Click += new EventHandler(this.OpenConfigBTN_Click);
        base.AutoScaleDimensions = new SizeF(7f, 15f);
        base.AutoScaleMode = AutoScaleMode.Font;
        this.BackColor = Color.FromArgb(0x2e, 0x33, 0x49);
        base.ClientSize = new Size(420, 0x139);
        base.Controls.Add(this.OpenConfigBTN);
        base.Controls.Add(this.HighRB);
        base.Controls.Add(this.DefaultRB);
        base.Controls.Add(this.button_0);
        base.Controls.Add(this.ChangeLogBTN);
        base.Controls.Add(this.button_1);
        base.Controls.Add(this.button_2);
        base.Controls.Add(this.button_3);
        base.Controls.Add(this.button_4);
        base.Controls.Add(this.button_5);
        this.Font = new Font("Roboto Slab", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
        base.FormBorderStyle = FormBorderStyle.None;
        base.Icon = (Icon) manager.GetObject("$this.Icon");
        base.Name = "FormConfig";
        this.Text = "FormMonitor";
        base.Load += new EventHandler(this.FormConfig_Load);
        base.ResumeLayout(false);
        base.PerformLayout();
    }

    private void method_0(bool bool_0, bool bool_1)
    {
        this.HighRB.Checked = bool_0;
        this.DefaultRB.Checked = bool_1;
    }

    [CompilerGenerated]
    private void method_1()
    {
        GClass6.smethod_5(this.directoryInfo_0);
    }

    private void OpenConfigBTN_Click(object sender, EventArgs e)
    {
        new Thread(Class12.<>9__5_0 ??= new ThreadStart(Class12.<>9.method_0)).Start();
    }

    private static void smethod_0(string string_0, string string_1)
    {
        StringBuilder builder = new StringBuilder(80);
        if (string_0 != null)
        {
            builder.Append("---[").Append(string_0).Append(']');
        }
        string str = (string_1 == null) ? "" : ("[" + string_1 + "]---");
        int num = 0x4f - str.Length;
        while (builder.Length != num)
        {
            builder.Append('-');
        }
        builder.Append(str);
        string local1 = string_1.Equals("Ended") ? $"{builder}
" : $"
{builder}";
        string text1 = local1;
        if (local1 == null)
        {
            string local2 = local1;
            text1 = "";
        }
        Console.WriteLine(text1);
    }

    override void Form.Dispose(bool disposing)
    {
        if (disposing && (this.icontainer_0 != null))
        {
            this.icontainer_0.Dispose();
        }
        base.Dispose(disposing);
    }

    [Serializable, CompilerGenerated]
    private sealed class Class12
    {
        public static readonly FormConfig.Class12 <>9 = new FormConfig.Class12();
        public static ThreadStart <>9__5_0;
        public static ThreadStart <>9__8_0;
        public static ThreadStart <>9__9_0;
        public static ThreadStart <>9__10_0;
        public static ThreadStart <>9__11_0;
        public static ThreadStart <>9__12_0;

        internal void method_0()
        {
            GClass6.smethod_6("Config/Settings.ini", "notepad.exe", "open");
        }

        internal void method_1()
        {
            FormConfig.smethod_0("Config", "Begin");
            ServerConfigJSON.Reload();
            CommandHelperJSON.Reload();
            ResolutionJSON.Reload();
            FormConfig.smethod_0("Config", "Ended");
        }

        internal void method_2()
        {
            FormConfig.smethod_0("Shop Data", "Begin");
            ShopManager.Reset();
            ShopManager.Load(1);
            ShopManager.Load(2);
            FormConfig.smethod_0("Shop Data", "Ended");
            GameXender.UpdateShop();
        }

        internal void method_3()
        {
            FormConfig.smethod_0("Events Data", "Begin");
            EventLoginXML.Reload();
            EventBoostXML.Reload();
            EventPlaytimeXML.Reload();
            EventQuestXML.Reload();
            EventRankUpXML.Reload();
            EventVisitXML.Reload();
            EventXmasXML.Reload();
            FormConfig.smethod_0("Events Data", "Ended");
            GameXender.UpdateEvents();
        }

        internal void method_4()
        {
            FormConfig.smethod_0("Classic Mode", "Begin");
            GameRuleXML.Reload();
            FormConfig.smethod_0("Classic Mode", "Ended");
        }

        internal void method_5()
        {
            FormConfig.smethod_0("Server Data", "Begin");
            TemplatePackXML.Reload();
            TitleSystemXML.Reload();
            TitleAwardXML.Reload();
            MissionAwardXML.Reload();
            MissionConfigXML.Reload();
            SChannelXML.Reload();
            SynchronizeXML.Reload();
            SystemMapXML.Reload();
            ClanRankXML.Reload();
            PlayerRankXML.Reload();
            CouponEffectXML.Reload();
            PermissionXML.Reload();
            RandomBoxXML.Reload();
            DirectLibraryXML.Reload();
            InternetCafeXML.Reload();
            RedeemCodeXML.Reload();
            NickFilter.Reload();
            Server.Auth.Data.XML.ChannelsXML.Reload();
            Server.Game.Data.XML.ChannelsXML.Reload();
            MapStructureXML.Reload();
            CharaStructureXML.Reload();
            ItemStatisticXML.Reload();
            FormConfig.smethod_0("Server Data", "Ended");
        }
    }

    [CompilerGenerated]
    private sealed class Class13
    {
        public ConfigEngine configEngine_0;

        internal void method_0()
        {
            string key = "MoreInfo";
            string section = "Server";
            if (this.configEngine_0.KeyExists(key, section))
            {
                this.configEngine_0.WriteX(key, ConfigLoader.ShowMoreInfo, section);
            }
            else
            {
                string[] textArray1 = new string[] { "Key: '", key, "' on Section '", section, "' doesn't exist!" };
                MessageBox.Show(string.Concat(textArray1), "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}

