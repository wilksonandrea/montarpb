using System;
using System.Collections;
using System.Reflection;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000869 RID: 2153
	internal class StackBasedReturnMessage : IMethodReturnMessage, IMethodMessage, IMessage, IInternalMessage
	{
		// Token: 0x06005B33 RID: 23347 RVA: 0x0013FB70 File Offset: 0x0013DD70
		internal StackBasedReturnMessage()
		{
		}

		// Token: 0x06005B34 RID: 23348 RVA: 0x0013FB78 File Offset: 0x0013DD78
		internal void InitFields(Message m)
		{
			this._m = m;
			if (this._h != null)
			{
				this._h.Clear();
			}
			if (this._d != null)
			{
				this._d.Clear();
			}
		}

		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x06005B35 RID: 23349 RVA: 0x0013FBA7 File Offset: 0x0013DDA7
		public string Uri
		{
			[SecurityCritical]
			get
			{
				return this._m.Uri;
			}
		}

		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x06005B36 RID: 23350 RVA: 0x0013FBB4 File Offset: 0x0013DDB4
		public string MethodName
		{
			[SecurityCritical]
			get
			{
				return this._m.MethodName;
			}
		}

		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x06005B37 RID: 23351 RVA: 0x0013FBC1 File Offset: 0x0013DDC1
		public string TypeName
		{
			[SecurityCritical]
			get
			{
				return this._m.TypeName;
			}
		}

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x06005B38 RID: 23352 RVA: 0x0013FBCE File Offset: 0x0013DDCE
		public object MethodSignature
		{
			[SecurityCritical]
			get
			{
				return this._m.MethodSignature;
			}
		}

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x06005B39 RID: 23353 RVA: 0x0013FBDB File Offset: 0x0013DDDB
		public MethodBase MethodBase
		{
			[SecurityCritical]
			get
			{
				return this._m.MethodBase;
			}
		}

		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x06005B3A RID: 23354 RVA: 0x0013FBE8 File Offset: 0x0013DDE8
		public bool HasVarArgs
		{
			[SecurityCritical]
			get
			{
				return this._m.HasVarArgs;
			}
		}

		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x06005B3B RID: 23355 RVA: 0x0013FBF5 File Offset: 0x0013DDF5
		public int ArgCount
		{
			[SecurityCritical]
			get
			{
				return this._m.ArgCount;
			}
		}

		// Token: 0x06005B3C RID: 23356 RVA: 0x0013FC02 File Offset: 0x0013DE02
		[SecurityCritical]
		public object GetArg(int argNum)
		{
			return this._m.GetArg(argNum);
		}

		// Token: 0x06005B3D RID: 23357 RVA: 0x0013FC10 File Offset: 0x0013DE10
		[SecurityCritical]
		public string GetArgName(int index)
		{
			return this._m.GetArgName(index);
		}

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x06005B3E RID: 23358 RVA: 0x0013FC1E File Offset: 0x0013DE1E
		public object[] Args
		{
			[SecurityCritical]
			get
			{
				return this._m.Args;
			}
		}

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x06005B3F RID: 23359 RVA: 0x0013FC2B File Offset: 0x0013DE2B
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return this._m.GetLogicalCallContext();
			}
		}

		// Token: 0x06005B40 RID: 23360 RVA: 0x0013FC38 File Offset: 0x0013DE38
		[SecurityCritical]
		internal LogicalCallContext GetLogicalCallContext()
		{
			return this._m.GetLogicalCallContext();
		}

		// Token: 0x06005B41 RID: 23361 RVA: 0x0013FC45 File Offset: 0x0013DE45
		[SecurityCritical]
		internal LogicalCallContext SetLogicalCallContext(LogicalCallContext callCtx)
		{
			return this._m.SetLogicalCallContext(callCtx);
		}

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x06005B42 RID: 23362 RVA: 0x0013FC53 File Offset: 0x0013DE53
		public int OutArgCount
		{
			[SecurityCritical]
			get
			{
				if (this._argMapper == null)
				{
					this._argMapper = new ArgMapper(this, true);
				}
				return this._argMapper.ArgCount;
			}
		}

		// Token: 0x06005B43 RID: 23363 RVA: 0x0013FC75 File Offset: 0x0013DE75
		[SecurityCritical]
		public object GetOutArg(int argNum)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, true);
			}
			return this._argMapper.GetArg(argNum);
		}

		// Token: 0x06005B44 RID: 23364 RVA: 0x0013FC98 File Offset: 0x0013DE98
		[SecurityCritical]
		public string GetOutArgName(int index)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, true);
			}
			return this._argMapper.GetArgName(index);
		}

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x06005B45 RID: 23365 RVA: 0x0013FCBB File Offset: 0x0013DEBB
		public object[] OutArgs
		{
			[SecurityCritical]
			get
			{
				if (this._argMapper == null)
				{
					this._argMapper = new ArgMapper(this, true);
				}
				return this._argMapper.Args;
			}
		}

		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x06005B46 RID: 23366 RVA: 0x0013FCDD File Offset: 0x0013DEDD
		public Exception Exception
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x06005B47 RID: 23367 RVA: 0x0013FCE0 File Offset: 0x0013DEE0
		public object ReturnValue
		{
			[SecurityCritical]
			get
			{
				return this._m.GetReturnValue();
			}
		}

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x06005B48 RID: 23368 RVA: 0x0013FCF0 File Offset: 0x0013DEF0
		public IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				IDictionary d;
				lock (this)
				{
					if (this._h == null)
					{
						this._h = new Hashtable();
					}
					if (this._d == null)
					{
						this._d = new MRMDictionary(this, this._h);
					}
					d = this._d;
				}
				return d;
			}
		}

		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x06005B49 RID: 23369 RVA: 0x0013FD5C File Offset: 0x0013DF5C
		// (set) Token: 0x06005B4A RID: 23370 RVA: 0x0013FD5F File Offset: 0x0013DF5F
		ServerIdentity IInternalMessage.ServerIdentityObject
		{
			[SecurityCritical]
			get
			{
				return null;
			}
			[SecurityCritical]
			set
			{
			}
		}

		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06005B4B RID: 23371 RVA: 0x0013FD61 File Offset: 0x0013DF61
		// (set) Token: 0x06005B4C RID: 23372 RVA: 0x0013FD64 File Offset: 0x0013DF64
		Identity IInternalMessage.IdentityObject
		{
			[SecurityCritical]
			get
			{
				return null;
			}
			[SecurityCritical]
			set
			{
			}
		}

		// Token: 0x06005B4D RID: 23373 RVA: 0x0013FD66 File Offset: 0x0013DF66
		[SecurityCritical]
		void IInternalMessage.SetURI(string val)
		{
			this._m.Uri = val;
		}

		// Token: 0x06005B4E RID: 23374 RVA: 0x0013FD74 File Offset: 0x0013DF74
		[SecurityCritical]
		void IInternalMessage.SetCallContext(LogicalCallContext newCallContext)
		{
			this._m.SetLogicalCallContext(newCallContext);
		}

		// Token: 0x06005B4F RID: 23375 RVA: 0x0013FD83 File Offset: 0x0013DF83
		[SecurityCritical]
		bool IInternalMessage.HasProperties()
		{
			return this._h != null;
		}

		// Token: 0x04002951 RID: 10577
		private Message _m;

		// Token: 0x04002952 RID: 10578
		private Hashtable _h;

		// Token: 0x04002953 RID: 10579
		private MRMDictionary _d;

		// Token: 0x04002954 RID: 10580
		private ArgMapper _argMapper;
	}
}
