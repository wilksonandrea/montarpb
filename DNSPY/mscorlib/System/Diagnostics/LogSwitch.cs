using System;
using System.Security;

namespace System.Diagnostics
{
	// Token: 0x020003F6 RID: 1014
	[Serializable]
	internal class LogSwitch
	{
		// Token: 0x06003356 RID: 13142 RVA: 0x000C501B File Offset: 0x000C321B
		private LogSwitch()
		{
		}

		// Token: 0x06003357 RID: 13143 RVA: 0x000C5024 File Offset: 0x000C3224
		[SecuritySafeCritical]
		public LogSwitch(string name, string description, LogSwitch parent)
		{
			if (name != null && name.Length == 0)
			{
				throw new ArgumentOutOfRangeException("Name", Environment.GetResourceString("Argument_StringZeroLength"));
			}
			if (name != null && parent != null)
			{
				this.strName = name;
				this.strDescription = description;
				this.iLevel = LoggingLevels.ErrorLevel;
				this.iOldLevel = this.iLevel;
				this.ParentSwitch = parent;
				Log.m_Hashtable.Add(this.strName, this);
				Log.AddLogSwitch(this);
				return;
			}
			throw new ArgumentNullException((name == null) ? "name" : "parent");
		}

		// Token: 0x06003358 RID: 13144 RVA: 0x000C50B8 File Offset: 0x000C32B8
		[SecuritySafeCritical]
		internal LogSwitch(string name, string description)
		{
			this.strName = name;
			this.strDescription = description;
			this.iLevel = LoggingLevels.ErrorLevel;
			this.iOldLevel = this.iLevel;
			this.ParentSwitch = null;
			Log.m_Hashtable.Add(this.strName, this);
			Log.AddLogSwitch(this);
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06003359 RID: 13145 RVA: 0x000C5111 File Offset: 0x000C3311
		public virtual string Name
		{
			get
			{
				return this.strName;
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x0600335A RID: 13146 RVA: 0x000C5119 File Offset: 0x000C3319
		public virtual string Description
		{
			get
			{
				return this.strDescription;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x0600335B RID: 13147 RVA: 0x000C5121 File Offset: 0x000C3321
		public virtual LogSwitch Parent
		{
			get
			{
				return this.ParentSwitch;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x0600335C RID: 13148 RVA: 0x000C5129 File Offset: 0x000C3329
		// (set) Token: 0x0600335D RID: 13149 RVA: 0x000C5134 File Offset: 0x000C3334
		public virtual LoggingLevels MinimumLevel
		{
			get
			{
				return this.iLevel;
			}
			[SecuritySafeCritical]
			set
			{
				this.iLevel = value;
				this.iOldLevel = value;
				string text = ((this.ParentSwitch != null) ? this.ParentSwitch.Name : "");
				if (Debugger.IsAttached)
				{
					Log.ModifyLogSwitch((int)this.iLevel, this.strName, text);
				}
				Log.InvokeLogSwitchLevelHandlers(this, this.iLevel);
			}
		}

		// Token: 0x0600335E RID: 13150 RVA: 0x000C5197 File Offset: 0x000C3397
		public virtual bool CheckLevel(LoggingLevels level)
		{
			return this.iLevel <= level || (this.ParentSwitch != null && this.ParentSwitch.CheckLevel(level));
		}

		// Token: 0x0600335F RID: 13151 RVA: 0x000C51BC File Offset: 0x000C33BC
		public static LogSwitch GetSwitch(string name)
		{
			return (LogSwitch)Log.m_Hashtable[name];
		}

		// Token: 0x040016CA RID: 5834
		internal string strName;

		// Token: 0x040016CB RID: 5835
		internal string strDescription;

		// Token: 0x040016CC RID: 5836
		private LogSwitch ParentSwitch;

		// Token: 0x040016CD RID: 5837
		internal volatile LoggingLevels iLevel;

		// Token: 0x040016CE RID: 5838
		internal volatile LoggingLevels iOldLevel;
	}
}
