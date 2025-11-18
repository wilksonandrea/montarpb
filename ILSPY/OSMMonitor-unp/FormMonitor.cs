using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public class FormMonitor : Form
{
	private IContainer icontainer_0;

	private GroupBox groupBox_0;

	private Label label109;

	private Label label60;

	private Label label108;

	private Label RepairableItems;

	private Label label57;

	private Label ShopCafeItems;

	private Label label107;

	private Label label54;

	private Label RegisteredShopItems;

	private Label VipUsers;

	private Label label101;

	private Label label2;

	private Label label102;

	private Label label104;

	private Label lebl106;

	private Label label105;

	private Label label103;

	private Label label18;

	private Label label19;

	private Label label21;

	private Label TotalClans;

	private Label label24;

	private Label UnknownUsers;

	private Label label27;

	private Label RegisteredUsers;

	private Label OnlinePlayers;

	private Label TotalBannedPlayers;

	private Timer timer_0;

	public FormMonitor()
	{
		InitializeComponent();
	}

	private void FormMonitor_Load(object sender, EventArgs e)
	{
		float float_ = float.Parse("10") / 100f;
		BackgroundImage = GClass5.smethod_0().method_0(Class11.Bitmap_0, float_);
		BackgroundImageLayout = ImageLayout.Center;
		RegisteredUsers.Text = "Loading...";
		OnlinePlayers.Text = "Loading...";
		TotalClans.Text = "Loading...";
		VipUsers.Text = "Loading...";
		UnknownUsers.Text = "Loading...";
		TotalBannedPlayers.Text = "Loading...";
		RegisteredShopItems.Text = "Loading...";
		ShopCafeItems.Text = "Loading...";
		RepairableItems.Text = "Loading...";
		timer_0.Start();
	}

	private void timer_0_Tick(object sender, EventArgs e)
	{
		RegisteredUsers.Text = GClass9.string_5;
		OnlinePlayers.Text = GClass9.string_6;
		TotalClans.Text = GClass9.string_7;
		VipUsers.Text = GClass9.string_8;
		UnknownUsers.Text = GClass9.string_9;
		TotalBannedPlayers.Text = GClass9.string_10;
		RegisteredShopItems.Text = GClass9.string_11;
		ShopCafeItems.Text = GClass9.string_12;
		RepairableItems.Text = GClass9.string_13;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMonitor));
		this.groupBox_0 = new System.Windows.Forms.GroupBox();
		this.label109 = new System.Windows.Forms.Label();
		this.label60 = new System.Windows.Forms.Label();
		this.label108 = new System.Windows.Forms.Label();
		this.RepairableItems = new System.Windows.Forms.Label();
		this.label57 = new System.Windows.Forms.Label();
		this.ShopCafeItems = new System.Windows.Forms.Label();
		this.label107 = new System.Windows.Forms.Label();
		this.label54 = new System.Windows.Forms.Label();
		this.RegisteredShopItems = new System.Windows.Forms.Label();
		this.VipUsers = new System.Windows.Forms.Label();
		this.label101 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label102 = new System.Windows.Forms.Label();
		this.label104 = new System.Windows.Forms.Label();
		this.lebl106 = new System.Windows.Forms.Label();
		this.label105 = new System.Windows.Forms.Label();
		this.label103 = new System.Windows.Forms.Label();
		this.label18 = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.label21 = new System.Windows.Forms.Label();
		this.TotalClans = new System.Windows.Forms.Label();
		this.label24 = new System.Windows.Forms.Label();
		this.UnknownUsers = new System.Windows.Forms.Label();
		this.label27 = new System.Windows.Forms.Label();
		this.RegisteredUsers = new System.Windows.Forms.Label();
		this.OnlinePlayers = new System.Windows.Forms.Label();
		this.TotalBannedPlayers = new System.Windows.Forms.Label();
		this.timer_0 = new System.Windows.Forms.Timer(this.icontainer_0);
		this.groupBox_0.SuspendLayout();
		base.SuspendLayout();
		this.groupBox_0.BackColor = System.Drawing.Color.Transparent;
		this.groupBox_0.Controls.Add(this.label109);
		this.groupBox_0.Controls.Add(this.label60);
		this.groupBox_0.Controls.Add(this.label108);
		this.groupBox_0.Controls.Add(this.RepairableItems);
		this.groupBox_0.Controls.Add(this.label57);
		this.groupBox_0.Controls.Add(this.ShopCafeItems);
		this.groupBox_0.Controls.Add(this.label107);
		this.groupBox_0.Controls.Add(this.label54);
		this.groupBox_0.Controls.Add(this.RegisteredShopItems);
		this.groupBox_0.Controls.Add(this.VipUsers);
		this.groupBox_0.Controls.Add(this.label101);
		this.groupBox_0.Controls.Add(this.label2);
		this.groupBox_0.Controls.Add(this.label102);
		this.groupBox_0.Controls.Add(this.label104);
		this.groupBox_0.Controls.Add(this.lebl106);
		this.groupBox_0.Controls.Add(this.label105);
		this.groupBox_0.Controls.Add(this.label103);
		this.groupBox_0.Controls.Add(this.label18);
		this.groupBox_0.Controls.Add(this.label19);
		this.groupBox_0.Controls.Add(this.label21);
		this.groupBox_0.Controls.Add(this.TotalClans);
		this.groupBox_0.Controls.Add(this.label24);
		this.groupBox_0.Controls.Add(this.UnknownUsers);
		this.groupBox_0.Controls.Add(this.label27);
		this.groupBox_0.Controls.Add(this.RegisteredUsers);
		this.groupBox_0.Controls.Add(this.OnlinePlayers);
		this.groupBox_0.Controls.Add(this.TotalBannedPlayers);
		this.groupBox_0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.groupBox_0.Font = new System.Drawing.Font("Roboto Slab", 8f);
		this.groupBox_0.ForeColor = System.Drawing.Color.White;
		this.groupBox_0.Location = new System.Drawing.Point(15, 9);
		this.groupBox_0.Name = "SrvInfoGB";
		this.groupBox_0.Size = new System.Drawing.Size(390, 294);
		this.groupBox_0.TabIndex = 89;
		this.groupBox_0.TabStop = false;
		this.groupBox_0.Text = "Database Monitor";
		this.label109.AutoSize = true;
		this.label109.Font = new System.Drawing.Font("Roboto Slab", 10f);
		this.label109.ForeColor = System.Drawing.Color.Silver;
		this.label109.Location = new System.Drawing.Point(17, 253);
		this.label109.Name = "label109";
		this.label109.Size = new System.Drawing.Size(122, 19);
		this.label109.TabIndex = 112;
		this.label109.Text = "Repairable Items";
		this.label60.AutoSize = true;
		this.label60.Font = new System.Drawing.Font("Roboto Slab", 10f);
		this.label60.ForeColor = System.Drawing.Color.Silver;
		this.label60.Location = new System.Drawing.Point(202, 253);
		this.label60.Name = "label60";
		this.label60.Size = new System.Drawing.Size(12, 19);
		this.label60.TabIndex = 113;
		this.label60.Text = ":";
		this.label108.AutoSize = true;
		this.label108.Font = new System.Drawing.Font("Roboto Slab", 10f);
		this.label108.ForeColor = System.Drawing.Color.Silver;
		this.label108.Location = new System.Drawing.Point(17, 225);
		this.label108.Name = "label108";
		this.label108.Size = new System.Drawing.Size(117, 19);
		this.label108.TabIndex = 91;
		this.label108.Text = "Shop Cafe Items";
		this.RepairableItems.AutoSize = true;
		this.RepairableItems.Font = new System.Drawing.Font("Roboto Slab", 10f);
		this.RepairableItems.ForeColor = System.Drawing.Color.Silver;
		this.RepairableItems.Location = new System.Drawing.Point(218, 253);
		this.RepairableItems.Name = "RepairableItems";
		this.RepairableItems.Size = new System.Drawing.Size(86, 19);
		this.RepairableItems.TabIndex = 114;
		this.RepairableItems.Text = "1234567890";
		this.label57.AutoSize = true;
		this.label57.Font = new System.Drawing.Font("Roboto Slab", 10f);
		this.label57.ForeColor = System.Drawing.Color.Silver;
		this.label57.Location = new System.Drawing.Point(202, 225);
		this.label57.Name = "label57";
		this.label57.Size = new System.Drawing.Size(12, 19);
		this.label57.TabIndex = 92;
		this.label57.Text = ":";
		this.ShopCafeItems.AutoSize = true;
		this.ShopCafeItems.Font = new System.Drawing.Font("Roboto Slab", 10f);
		this.ShopCafeItems.ForeColor = System.Drawing.Color.Silver;
		this.ShopCafeItems.Location = new System.Drawing.Point(218, 225);
		this.ShopCafeItems.Name = "ShopCafeItems";
		this.ShopCafeItems.Size = new System.Drawing.Size(86, 19);
		this.ShopCafeItems.TabIndex = 93;
		this.ShopCafeItems.Text = "1234567890";
		this.label107.AutoSize = true;
		this.label107.Font = new System.Drawing.Font("Roboto Slab", 10f);
		this.label107.ForeColor = System.Drawing.Color.Silver;
		this.label107.Location = new System.Drawing.Point(17, 197);
		this.label107.Name = "label107";
		this.label107.Size = new System.Drawing.Size(159, 19);
		this.label107.TabIndex = 88;
		this.label107.Text = "Registered Shop Items";
		this.label54.AutoSize = true;
		this.label54.Font = new System.Drawing.Font("Roboto Slab", 10f);
		this.label54.ForeColor = System.Drawing.Color.Silver;
		this.label54.Location = new System.Drawing.Point(202, 197);
		this.label54.Name = "label54";
		this.label54.Size = new System.Drawing.Size(12, 19);
		this.label54.TabIndex = 89;
		this.label54.Text = ":";
		this.RegisteredShopItems.AutoSize = true;
		this.RegisteredShopItems.Font = new System.Drawing.Font("Roboto Slab", 10f);
		this.RegisteredShopItems.ForeColor = System.Drawing.Color.Silver;
		this.RegisteredShopItems.Location = new System.Drawing.Point(218, 197);
		this.RegisteredShopItems.Name = "RegisteredShopItems";
		this.RegisteredShopItems.Size = new System.Drawing.Size(86, 19);
		this.RegisteredShopItems.TabIndex = 90;
		this.RegisteredShopItems.Text = "1234567890";
		this.VipUsers.AutoSize = true;
		this.VipUsers.Font = new System.Drawing.Font("Roboto Slab", 10f);
		this.VipUsers.ForeColor = System.Drawing.Color.Silver;
		this.VipUsers.Location = new System.Drawing.Point(218, 113);
		this.VipUsers.Name = "VipUsers";
		this.VipUsers.Size = new System.Drawing.Size(86, 19);
		this.VipUsers.TabIndex = 87;
		this.VipUsers.Text = "1234567890";
		this.label101.AutoSize = true;
		this.label101.Font = new System.Drawing.Font("Roboto Slab", 10f);
		this.label101.ForeColor = System.Drawing.Color.Silver;
		this.label101.Location = new System.Drawing.Point(17, 29);
		this.label101.Name = "label101";
		this.label101.Size = new System.Drawing.Size(123, 19);
		this.label101.TabIndex = 44;
		this.label101.Text = "Registered Users";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Roboto Slab", 10f);
		this.label2.ForeColor = System.Drawing.Color.Silver;
		this.label2.Location = new System.Drawing.Point(202, 113);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(12, 19);
		this.label2.TabIndex = 86;
		this.label2.Text = ":";
		this.label102.AutoSize = true;
		this.label102.Font = new System.Drawing.Font("Roboto Slab", 10f);
		this.label102.ForeColor = System.Drawing.Color.Silver;
		this.label102.Location = new System.Drawing.Point(17, 57);
		this.label102.Name = "label102";
		this.label102.Size = new System.Drawing.Size(107, 19);
		this.label102.TabIndex = 45;
		this.label102.Text = "Online Players";
		this.label104.AutoSize = true;
		this.label104.Font = new System.Drawing.Font("Roboto Slab", 10f);
		this.label104.ForeColor = System.Drawing.Color.Silver;
		this.label104.Location = new System.Drawing.Point(17, 113);
		this.label104.Name = "label104";
		this.label104.Size = new System.Drawing.Size(75, 19);
		this.label104.TabIndex = 85;
		this.label104.Text = "VIP Users";
		this.lebl106.AutoSize = true;
		this.lebl106.Font = new System.Drawing.Font("Roboto Slab", 10f);
		this.lebl106.ForeColor = System.Drawing.Color.Silver;
		this.lebl106.Location = new System.Drawing.Point(17, 169);
		this.lebl106.Name = "lebl106";
		this.lebl106.Size = new System.Drawing.Size(150, 19);
		this.lebl106.TabIndex = 47;
		this.lebl106.Text = "Total Banned Players";
		this.label105.AutoSize = true;
		this.label105.Font = new System.Drawing.Font("Roboto Slab", 10f);
		this.label105.ForeColor = System.Drawing.Color.Silver;
		this.label105.Location = new System.Drawing.Point(17, 141);
		this.label105.Name = "label105";
		this.label105.Size = new System.Drawing.Size(118, 19);
		this.label105.TabIndex = 51;
		this.label105.Text = "Unknown Users";
		this.label103.AutoSize = true;
		this.label103.Font = new System.Drawing.Font("Roboto Slab", 10f);
		this.label103.ForeColor = System.Drawing.Color.Silver;
		this.label103.Location = new System.Drawing.Point(17, 85);
		this.label103.Name = "label103";
		this.label103.Size = new System.Drawing.Size(83, 19);
		this.label103.TabIndex = 53;
		this.label103.Text = "Total Clans";
		this.label18.AutoSize = true;
		this.label18.Font = new System.Drawing.Font("Roboto Slab", 10f);
		this.label18.ForeColor = System.Drawing.Color.Silver;
		this.label18.Location = new System.Drawing.Point(202, 29);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(12, 19);
		this.label18.TabIndex = 56;
		this.label18.Text = ":";
		this.label19.AutoSize = true;
		this.label19.Font = new System.Drawing.Font("Roboto Slab", 10f);
		this.label19.ForeColor = System.Drawing.Color.Silver;
		this.label19.Location = new System.Drawing.Point(202, 57);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(12, 19);
		this.label19.TabIndex = 57;
		this.label19.Text = ":";
		this.label21.AutoSize = true;
		this.label21.Font = new System.Drawing.Font("Roboto Slab", 10f);
		this.label21.ForeColor = System.Drawing.Color.Silver;
		this.label21.Location = new System.Drawing.Point(202, 169);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(12, 19);
		this.label21.TabIndex = 59;
		this.label21.Text = ":";
		this.TotalClans.AutoSize = true;
		this.TotalClans.Font = new System.Drawing.Font("Roboto Slab", 10f);
		this.TotalClans.ForeColor = System.Drawing.Color.Silver;
		this.TotalClans.Location = new System.Drawing.Point(218, 85);
		this.TotalClans.Name = "TotalClans";
		this.TotalClans.Size = new System.Drawing.Size(86, 19);
		this.TotalClans.TabIndex = 77;
		this.TotalClans.Text = "1234567890";
		this.label24.AutoSize = true;
		this.label24.Font = new System.Drawing.Font("Roboto Slab", 10f);
		this.label24.ForeColor = System.Drawing.Color.Silver;
		this.label24.Location = new System.Drawing.Point(202, 141);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(12, 19);
		this.label24.TabIndex = 62;
		this.label24.Text = ":";
		this.UnknownUsers.AutoSize = true;
		this.UnknownUsers.Font = new System.Drawing.Font("Roboto Slab", 10f);
		this.UnknownUsers.ForeColor = System.Drawing.Color.Silver;
		this.UnknownUsers.Location = new System.Drawing.Point(218, 141);
		this.UnknownUsers.Name = "UnknownUsers";
		this.UnknownUsers.Size = new System.Drawing.Size(86, 19);
		this.UnknownUsers.TabIndex = 75;
		this.UnknownUsers.Text = "1234567890";
		this.label27.AutoSize = true;
		this.label27.Font = new System.Drawing.Font("Roboto Slab", 10f);
		this.label27.ForeColor = System.Drawing.Color.Silver;
		this.label27.Location = new System.Drawing.Point(202, 85);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(12, 19);
		this.label27.TabIndex = 65;
		this.label27.Text = ":";
		this.RegisteredUsers.AutoSize = true;
		this.RegisteredUsers.Font = new System.Drawing.Font("Roboto Slab", 10f);
		this.RegisteredUsers.ForeColor = System.Drawing.Color.Silver;
		this.RegisteredUsers.Location = new System.Drawing.Point(218, 29);
		this.RegisteredUsers.Name = "RegisteredUsers";
		this.RegisteredUsers.Size = new System.Drawing.Size(86, 19);
		this.RegisteredUsers.TabIndex = 68;
		this.RegisteredUsers.Text = "1234567890";
		this.OnlinePlayers.AutoSize = true;
		this.OnlinePlayers.Font = new System.Drawing.Font("Roboto Slab", 10f);
		this.OnlinePlayers.ForeColor = System.Drawing.Color.Silver;
		this.OnlinePlayers.Location = new System.Drawing.Point(218, 57);
		this.OnlinePlayers.Name = "OnlinePlayers";
		this.OnlinePlayers.Size = new System.Drawing.Size(86, 19);
		this.OnlinePlayers.TabIndex = 69;
		this.OnlinePlayers.Text = "1234567890";
		this.TotalBannedPlayers.AutoSize = true;
		this.TotalBannedPlayers.Font = new System.Drawing.Font("Roboto Slab", 10f);
		this.TotalBannedPlayers.ForeColor = System.Drawing.Color.Silver;
		this.TotalBannedPlayers.Location = new System.Drawing.Point(218, 169);
		this.TotalBannedPlayers.Name = "TotalBannedPlayers";
		this.TotalBannedPlayers.Size = new System.Drawing.Size(86, 19);
		this.TotalBannedPlayers.TabIndex = 71;
		this.TotalBannedPlayers.Text = "1234567890";
		this.timer_0.Interval = 1000;
		this.timer_0.Tick += new System.EventHandler(timer_0_Tick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(46, 51, 73);
		base.ClientSize = new System.Drawing.Size(420, 313);
		base.Controls.Add(this.groupBox_0);
		this.Font = new System.Drawing.Font("Roboto Slab", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormMonitor";
		this.Text = "FormMonitor";
		base.Load += new System.EventHandler(FormMonitor_Load);
		this.groupBox_0.ResumeLayout(false);
		this.groupBox_0.PerformLayout();
		base.ResumeLayout(false);
	}
}
