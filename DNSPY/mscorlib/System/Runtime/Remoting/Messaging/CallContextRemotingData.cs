using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000892 RID: 2194
	[Serializable]
	internal class CallContextRemotingData : ICloneable
	{
		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x06005CE7 RID: 23783 RVA: 0x00145798 File Offset: 0x00143998
		// (set) Token: 0x06005CE8 RID: 23784 RVA: 0x001457A0 File Offset: 0x001439A0
		internal string LogicalCallID
		{
			get
			{
				return this._logicalCallID;
			}
			set
			{
				this._logicalCallID = value;
			}
		}

		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x06005CE9 RID: 23785 RVA: 0x001457A9 File Offset: 0x001439A9
		internal bool HasInfo
		{
			get
			{
				return this._logicalCallID != null;
			}
		}

		// Token: 0x06005CEA RID: 23786 RVA: 0x001457B4 File Offset: 0x001439B4
		public object Clone()
		{
			return new CallContextRemotingData
			{
				LogicalCallID = this.LogicalCallID
			};
		}

		// Token: 0x06005CEB RID: 23787 RVA: 0x001457D4 File Offset: 0x001439D4
		public CallContextRemotingData()
		{
		}

		// Token: 0x040029EB RID: 10731
		private string _logicalCallID;
	}
}
