using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007A8 RID: 1960
	[Serializable]
	internal sealed class BinaryMethodCallMessage
	{
		// Token: 0x060054F7 RID: 21751 RVA: 0x0012DEB8 File Offset: 0x0012C0B8
		[SecurityCritical]
		internal BinaryMethodCallMessage(string uri, string methodName, string typeName, Type[] instArgs, object[] args, object methodSignature, LogicalCallContext callContext, object[] properties)
		{
			this._methodName = methodName;
			this._typeName = typeName;
			if (args == null)
			{
				args = new object[0];
			}
			this._inargs = args;
			this._args = args;
			this._instArgs = instArgs;
			this._methodSignature = methodSignature;
			if (callContext == null)
			{
				this._logicalCallContext = new LogicalCallContext();
			}
			else
			{
				this._logicalCallContext = callContext;
			}
			this._properties = properties;
		}

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x060054F8 RID: 21752 RVA: 0x0012DF26 File Offset: 0x0012C126
		public string MethodName
		{
			get
			{
				return this._methodName;
			}
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x060054F9 RID: 21753 RVA: 0x0012DF2E File Offset: 0x0012C12E
		public string TypeName
		{
			get
			{
				return this._typeName;
			}
		}

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x060054FA RID: 21754 RVA: 0x0012DF36 File Offset: 0x0012C136
		public Type[] InstantiationArgs
		{
			get
			{
				return this._instArgs;
			}
		}

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x060054FB RID: 21755 RVA: 0x0012DF3E File Offset: 0x0012C13E
		public object MethodSignature
		{
			get
			{
				return this._methodSignature;
			}
		}

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x060054FC RID: 21756 RVA: 0x0012DF46 File Offset: 0x0012C146
		public object[] Args
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x060054FD RID: 21757 RVA: 0x0012DF4E File Offset: 0x0012C14E
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return this._logicalCallContext;
			}
		}

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x060054FE RID: 21758 RVA: 0x0012DF56 File Offset: 0x0012C156
		public bool HasProperties
		{
			get
			{
				return this._properties != null;
			}
		}

		// Token: 0x060054FF RID: 21759 RVA: 0x0012DF64 File Offset: 0x0012C164
		internal void PopulateMessageProperties(IDictionary dict)
		{
			foreach (DictionaryEntry dictionaryEntry in this._properties)
			{
				dict[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
		}

		// Token: 0x04002719 RID: 10009
		private object[] _inargs;

		// Token: 0x0400271A RID: 10010
		private string _methodName;

		// Token: 0x0400271B RID: 10011
		private string _typeName;

		// Token: 0x0400271C RID: 10012
		private object _methodSignature;

		// Token: 0x0400271D RID: 10013
		private Type[] _instArgs;

		// Token: 0x0400271E RID: 10014
		private object[] _args;

		// Token: 0x0400271F RID: 10015
		[SecurityCritical]
		private LogicalCallContext _logicalCallContext;

		// Token: 0x04002720 RID: 10016
		private object[] _properties;
	}
}
