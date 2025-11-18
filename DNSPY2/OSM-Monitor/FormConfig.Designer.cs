// Token: 0x0200002A RID: 42
public partial class FormConfig : global::System.Windows.Forms.Form
{
	// Token: 0x060000CF RID: 207 RVA: 0x00006FE4 File Offset: 0x000051E4
	private void InitializeComponent()
	{
		global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::FormConfig));
		this.HighRB = new global::System.Windows.Forms.RadioButton();
		this.DefaultRB = new global::System.Windows.Forms.RadioButton();
		this.button_0 = new global::System.Windows.Forms.Button();
		this.ChangeLogBTN = new global::System.Windows.Forms.Button();
		this.button_1 = new global::System.Windows.Forms.Button();
		this.button_2 = new global::System.Windows.Forms.Button();
		this.button_3 = new global::System.Windows.Forms.Button();
		this.button_4 = new global::System.Windows.Forms.Button();
		this.button_5 = new global::System.Windows.Forms.Button();
		this.OpenConfigBTN = new global::System.Windows.Forms.Button();
		base.SuspendLayout();
		this.HighRB.AutoCheck = false;
		this.HighRB.AutoSize = true;
		this.HighRB.ForeColor = global::System.Drawing.Color.Silver;
		this.HighRB.Location = new global::System.Drawing.Point(105, 41);
		this.HighRB.Name = "HighRB";
		this.HighRB.Size = new global::System.Drawing.Size(71, 19);
		this.HighRB.TabIndex = 136;
		this.HighRB.Text = "High Log";
		this.HighRB.UseVisualStyleBackColor = true;
		this.DefaultRB.AutoCheck = false;
		this.DefaultRB.AutoSize = true;
		this.DefaultRB.Checked = true;
		this.DefaultRB.Cursor = global::System.Windows.Forms.Cursors.Default;
		this.DefaultRB.ForeColor = global::System.Drawing.Color.Silver;
		this.DefaultRB.Location = new global::System.Drawing.Point(15, 41);
		this.DefaultRB.Name = "DefaultRB";
		this.DefaultRB.Size = new global::System.Drawing.Size(84, 19);
		this.DefaultRB.TabIndex = 135;
		this.DefaultRB.TabStop = true;
		this.DefaultRB.Text = "Default Log";
		this.DefaultRB.UseVisualStyleBackColor = true;
		this.button_0.Cursor = global::System.Windows.Forms.Cursors.Hand;
		this.button_0.FlatAppearance.BorderColor = global::System.Drawing.Color.Gold;
		this.button_0.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
		this.button_0.Font = new global::System.Drawing.Font("Roboto Slab", 8f);
		this.button_0.ForeColor = global::System.Drawing.Color.Gold;
		this.button_0.Location = new global::System.Drawing.Point(15, 160);
		this.button_0.Name = "ClearLogBTN";
		this.button_0.Size = new global::System.Drawing.Size(161, 69);
		this.button_0.TabIndex = 134;
		this.button_0.Text = "Clear Log Files";
		this.button_0.UseVisualStyleBackColor = true;
		this.button_0.Click += new global::System.EventHandler(this.button_0_Click);
		this.ChangeLogBTN.Cursor = global::System.Windows.Forms.Cursors.Hand;
		this.ChangeLogBTN.FlatAppearance.BorderColor = global::System.Drawing.Color.Gold;
		this.ChangeLogBTN.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
		this.ChangeLogBTN.Font = new global::System.Drawing.Font("Roboto Slab", 8f);
		this.ChangeLogBTN.ForeColor = global::System.Drawing.Color.Gold;
		this.ChangeLogBTN.Location = new global::System.Drawing.Point(15, 74);
		this.ChangeLogBTN.Name = "ChangeLogBTN";
		this.ChangeLogBTN.Size = new global::System.Drawing.Size(161, 70);
		this.ChangeLogBTN.TabIndex = 133;
		this.ChangeLogBTN.Text = "Change Log Mode";
		this.ChangeLogBTN.UseVisualStyleBackColor = true;
		this.ChangeLogBTN.Click += new global::System.EventHandler(this.ChangeLogBTN_Click);
		this.button_1.Cursor = global::System.Windows.Forms.Cursors.Hand;
		this.button_1.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
		this.button_1.Font = new global::System.Drawing.Font("Roboto Slab", 8f);
		this.button_1.ForeColor = global::System.Drawing.Color.Silver;
		this.button_1.Location = new global::System.Drawing.Point(182, 74);
		this.button_1.Name = "Reload1BTN";
		this.button_1.Size = new global::System.Drawing.Size(223, 26);
		this.button_1.TabIndex = 128;
		this.button_1.Text = "Reload Config";
		this.button_1.UseVisualStyleBackColor = true;
		this.button_1.Click += new global::System.EventHandler(this.button_1_Click);
		this.button_2.Cursor = global::System.Windows.Forms.Cursors.Hand;
		this.button_2.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
		this.button_2.Font = new global::System.Drawing.Font("Roboto Slab", 8f);
		this.button_2.ForeColor = global::System.Drawing.Color.Silver;
		this.button_2.Location = new global::System.Drawing.Point(182, 117);
		this.button_2.Name = "Reload3BTN";
		this.button_2.Size = new global::System.Drawing.Size(223, 26);
		this.button_2.TabIndex = 130;
		this.button_2.Text = "Reload Events";
		this.button_2.UseVisualStyleBackColor = true;
		this.button_2.Click += new global::System.EventHandler(this.button_2_Click);
		this.button_3.Cursor = global::System.Windows.Forms.Cursors.Hand;
		this.button_3.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
		this.button_3.Font = new global::System.Drawing.Font("Roboto Slab", 8f);
		this.button_3.ForeColor = global::System.Drawing.Color.Silver;
		this.button_3.Location = new global::System.Drawing.Point(15, 246);
		this.button_3.Name = "Reload5BTN";
		this.button_3.Size = new global::System.Drawing.Size(358, 26);
		this.button_3.TabIndex = 132;
		this.button_3.Text = "Reload Attachments";
		this.button_3.UseVisualStyleBackColor = true;
		this.button_3.Click += new global::System.EventHandler(this.button_3_Click);
		this.button_4.Cursor = global::System.Windows.Forms.Cursors.Hand;
		this.button_4.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
		this.button_4.Font = new global::System.Drawing.Font("Roboto Slab", 8f);
		this.button_4.ForeColor = global::System.Drawing.Color.Silver;
		this.button_4.Location = new global::System.Drawing.Point(182, 203);
		this.button_4.Name = "Reload4BTN";
		this.button_4.Size = new global::System.Drawing.Size(223, 26);
		this.button_4.TabIndex = 131;
		this.button_4.Text = "Reload Item Rules";
		this.button_4.UseVisualStyleBackColor = true;
		this.button_4.Click += new global::System.EventHandler(this.button_4_Click);
		this.button_5.Cursor = global::System.Windows.Forms.Cursors.Hand;
		this.button_5.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
		this.button_5.Font = new global::System.Drawing.Font("Roboto Slab", 8f);
		this.button_5.ForeColor = global::System.Drawing.Color.Silver;
		this.button_5.Location = new global::System.Drawing.Point(182, 160);
		this.button_5.Name = "Reload2BTN";
		this.button_5.Size = new global::System.Drawing.Size(223, 26);
		this.button_5.TabIndex = 129;
		this.button_5.Text = "Reload Shop";
		this.button_5.UseVisualStyleBackColor = true;
		this.button_5.Click += new global::System.EventHandler(this.button_5_Click);
		this.OpenConfigBTN.BackgroundImage = (global::System.Drawing.Image)componentResourceManager.GetObject("OpenConfigBTN.BackgroundImage");
		this.OpenConfigBTN.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Stretch;
		this.OpenConfigBTN.Cursor = global::System.Windows.Forms.Cursors.Hand;
		this.OpenConfigBTN.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
		this.OpenConfigBTN.Font = new global::System.Drawing.Font("Roboto Slab", 8f);
		this.OpenConfigBTN.ForeColor = global::System.Drawing.Color.Silver;
		this.OpenConfigBTN.Location = new global::System.Drawing.Point(379, 246);
		this.OpenConfigBTN.Name = "OpenConfigBTN";
		this.OpenConfigBTN.Size = new global::System.Drawing.Size(26, 26);
		this.OpenConfigBTN.TabIndex = 137;
		this.OpenConfigBTN.UseVisualStyleBackColor = true;
		this.OpenConfigBTN.Click += new global::System.EventHandler(this.OpenConfigBTN_Click);
		base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 15f);
		base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = global::System.Drawing.Color.FromArgb(46, 51, 73);
		base.ClientSize = new global::System.Drawing.Size(420, 313);
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
		this.Font = new global::System.Drawing.Font("Roboto Slab", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.None;
		base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
		base.Name = "FormConfig";
		this.Text = "FormMonitor";
		base.Load += new global::System.EventHandler(this.FormConfig_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	// Token: 0x040000CA RID: 202
	private global::System.Windows.Forms.Button OpenConfigBTN;

	// Token: 0x040000CB RID: 203
	private global::System.Windows.Forms.RadioButton HighRB;

	// Token: 0x040000CC RID: 204
	private global::System.Windows.Forms.RadioButton DefaultRB;

	// Token: 0x040000CD RID: 205
	private global::System.Windows.Forms.Button button_0;

	// Token: 0x040000CE RID: 206
	private global::System.Windows.Forms.Button ChangeLogBTN;

	// Token: 0x040000CF RID: 207
	private global::System.Windows.Forms.Button button_1;

	// Token: 0x040000D0 RID: 208
	private global::System.Windows.Forms.Button button_2;

	// Token: 0x040000D1 RID: 209
	private global::System.Windows.Forms.Button button_3;

	// Token: 0x040000D2 RID: 210
	private global::System.Windows.Forms.Button button_4;

	// Token: 0x040000D3 RID: 211
	private global::System.Windows.Forms.Button button_5;
}
