using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007A9 RID: 1961
	[Serializable]
	internal class BinaryMethodReturnMessage
	{
		// Token: 0x06005500 RID: 21760 RVA: 0x0012DFA4 File Offset: 0x0012C1A4
		[SecurityCritical]
		internal BinaryMethodReturnMessage(object returnValue, object[] args, Exception e, LogicalCallContext callContext, object[] properties)
		{
			this._returnValue = returnValue;
			if (args == null)
			{
				args = new object[0];
			}
			this._outargs = args;
			this._args = args;
			this._exception = e;
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

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x06005501 RID: 21761 RVA: 0x0012DFFF File Offset: 0x0012C1FF
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x06005502 RID: 21762 RVA: 0x0012E007 File Offset: 0x0012C207
		public object ReturnValue
		{
			get
			{
				return this._returnValue;
			}
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x06005503 RID: 21763 RVA: 0x0012E00F File Offset: 0x0012C20F
		public object[] Args
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x06005504 RID: 21764 RVA: 0x0012E017 File Offset: 0x0012C217
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return this._logicalCallContext;
			}
		}

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x06005505 RID: 21765 RVA: 0x0012E01F File Offset: 0x0012C21F
		public bool HasProperties
		{
			get
			{
				return this._properties != null;
			}
		}

		// Token: 0x06005506 RID: 21766 RVA: 0x0012E02C File Offset: 0x0012C22C
		internal void PopulateMessageProperties(IDictionary dict)
		{
			foreach (DictionaryEntry dictionaryEntry in this._properties)
			{
				dict[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
		}

		// Token: 0x04002721 RID: 10017
		private object[] _outargs;

		// Token: 0x04002722 RID: 10018
		private Exception _exception;

		// Token: 0x04002723 RID: 10019
		private object _returnValue;

		// Token: 0x04002724 RID: 10020
		private object[] _args;

		// Token: 0x04002725 RID: 10021
		[SecurityCritical]
		private LogicalCallContext _logicalCallContext;

		// Token: 0x04002726 RID: 10022
		private object[] _properties;
	}
}
