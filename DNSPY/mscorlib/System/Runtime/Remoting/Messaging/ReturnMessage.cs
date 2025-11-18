using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200086A RID: 2154
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public class ReturnMessage : IMethodReturnMessage, IMethodMessage, IMessage
	{
		// Token: 0x06005B50 RID: 23376 RVA: 0x0013FD90 File Offset: 0x0013DF90
		[SecurityCritical]
		public ReturnMessage(object ret, object[] outArgs, int outArgsCount, LogicalCallContext callCtx, IMethodCallMessage mcm)
		{
			this._ret = ret;
			this._outArgs = outArgs;
			this._outArgsCount = outArgsCount;
			if (callCtx != null)
			{
				this._callContext = callCtx;
			}
			else
			{
				this._callContext = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
			}
			if (mcm != null)
			{
				this._URI = mcm.Uri;
				this._methodName = mcm.MethodName;
				this._methodSignature = null;
				this._typeName = mcm.TypeName;
				this._hasVarArgs = mcm.HasVarArgs;
				this._methodBase = mcm.MethodBase;
			}
		}

		// Token: 0x06005B51 RID: 23377 RVA: 0x0013FE28 File Offset: 0x0013E028
		[SecurityCritical]
		public ReturnMessage(Exception e, IMethodCallMessage mcm)
		{
			this._e = (ReturnMessage.IsCustomErrorEnabled() ? new RemotingException(Environment.GetResourceString("Remoting_InternalError")) : e);
			this._callContext = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
			if (mcm != null)
			{
				this._URI = mcm.Uri;
				this._methodName = mcm.MethodName;
				this._methodSignature = null;
				this._typeName = mcm.TypeName;
				this._hasVarArgs = mcm.HasVarArgs;
				this._methodBase = mcm.MethodBase;
			}
		}

		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x06005B52 RID: 23378 RVA: 0x0013FEB5 File Offset: 0x0013E0B5
		// (set) Token: 0x06005B53 RID: 23379 RVA: 0x0013FEBD File Offset: 0x0013E0BD
		public string Uri
		{
			[SecurityCritical]
			get
			{
				return this._URI;
			}
			set
			{
				this._URI = value;
			}
		}

		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x06005B54 RID: 23380 RVA: 0x0013FEC6 File Offset: 0x0013E0C6
		public string MethodName
		{
			[SecurityCritical]
			get
			{
				return this._methodName;
			}
		}

		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x06005B55 RID: 23381 RVA: 0x0013FECE File Offset: 0x0013E0CE
		public string TypeName
		{
			[SecurityCritical]
			get
			{
				return this._typeName;
			}
		}

		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x06005B56 RID: 23382 RVA: 0x0013FED6 File Offset: 0x0013E0D6
		public object MethodSignature
		{
			[SecurityCritical]
			get
			{
				if (this._methodSignature == null && this._methodBase != null)
				{
					this._methodSignature = Message.GenerateMethodSignature(this._methodBase);
				}
				return this._methodSignature;
			}
		}

		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x06005B57 RID: 23383 RVA: 0x0013FF05 File Offset: 0x0013E105
		public MethodBase MethodBase
		{
			[SecurityCritical]
			get
			{
				return this._methodBase;
			}
		}

		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x06005B58 RID: 23384 RVA: 0x0013FF0D File Offset: 0x0013E10D
		public bool HasVarArgs
		{
			[SecurityCritical]
			get
			{
				return this._hasVarArgs;
			}
		}

		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x06005B59 RID: 23385 RVA: 0x0013FF15 File Offset: 0x0013E115
		public int ArgCount
		{
			[SecurityCritical]
			get
			{
				if (this._outArgs == null)
				{
					return this._outArgsCount;
				}
				return this._outArgs.Length;
			}
		}

		// Token: 0x06005B5A RID: 23386 RVA: 0x0013FF30 File Offset: 0x0013E130
		[SecurityCritical]
		public object GetArg(int argNum)
		{
			if (this._outArgs == null)
			{
				if (argNum < 0 || argNum >= this._outArgsCount)
				{
					throw new ArgumentOutOfRangeException("argNum");
				}
				return null;
			}
			else
			{
				if (argNum < 0 || argNum >= this._outArgs.Length)
				{
					throw new ArgumentOutOfRangeException("argNum");
				}
				return this._outArgs[argNum];
			}
		}

		// Token: 0x06005B5B RID: 23387 RVA: 0x0013FF84 File Offset: 0x0013E184
		[SecurityCritical]
		public string GetArgName(int index)
		{
			if (this._outArgs == null)
			{
				if (index < 0 || index >= this._outArgsCount)
				{
					throw new ArgumentOutOfRangeException("index");
				}
			}
			else if (index < 0 || index >= this._outArgs.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (this._methodBase != null)
			{
				RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(this._methodBase);
				return reflectionCachedData.Parameters[index].Name;
			}
			return "__param" + index.ToString();
		}

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06005B5C RID: 23388 RVA: 0x00140004 File Offset: 0x0013E204
		public object[] Args
		{
			[SecurityCritical]
			get
			{
				if (this._outArgs == null)
				{
					return new object[this._outArgsCount];
				}
				return this._outArgs;
			}
		}

		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x06005B5D RID: 23389 RVA: 0x00140020 File Offset: 0x0013E220
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

		// Token: 0x06005B5E RID: 23390 RVA: 0x00140042 File Offset: 0x0013E242
		[SecurityCritical]
		public object GetOutArg(int argNum)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, true);
			}
			return this._argMapper.GetArg(argNum);
		}

		// Token: 0x06005B5F RID: 23391 RVA: 0x00140065 File Offset: 0x0013E265
		[SecurityCritical]
		public string GetOutArgName(int index)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, true);
			}
			return this._argMapper.GetArgName(index);
		}

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x06005B60 RID: 23392 RVA: 0x00140088 File Offset: 0x0013E288
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

		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x06005B61 RID: 23393 RVA: 0x001400AA File Offset: 0x0013E2AA
		public Exception Exception
		{
			[SecurityCritical]
			get
			{
				return this._e;
			}
		}

		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x06005B62 RID: 23394 RVA: 0x001400B2 File Offset: 0x0013E2B2
		public virtual object ReturnValue
		{
			[SecurityCritical]
			get
			{
				return this._ret;
			}
		}

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x06005B63 RID: 23395 RVA: 0x001400BA File Offset: 0x0013E2BA
		public virtual IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				if (this._properties == null)
				{
					this._properties = new MRMDictionary(this, null);
				}
				return (MRMDictionary)this._properties;
			}
		}

		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x06005B64 RID: 23396 RVA: 0x001400DC File Offset: 0x0013E2DC
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return this.GetLogicalCallContext();
			}
		}

		// Token: 0x06005B65 RID: 23397 RVA: 0x001400E4 File Offset: 0x0013E2E4
		[SecurityCritical]
		internal LogicalCallContext GetLogicalCallContext()
		{
			if (this._callContext == null)
			{
				this._callContext = new LogicalCallContext();
			}
			return this._callContext;
		}

		// Token: 0x06005B66 RID: 23398 RVA: 0x00140100 File Offset: 0x0013E300
		internal LogicalCallContext SetLogicalCallContext(LogicalCallContext ctx)
		{
			LogicalCallContext callContext = this._callContext;
			this._callContext = ctx;
			return callContext;
		}

		// Token: 0x06005B67 RID: 23399 RVA: 0x0014011C File Offset: 0x0013E31C
		internal bool HasProperties()
		{
			return this._properties != null;
		}

		// Token: 0x06005B68 RID: 23400 RVA: 0x00140128 File Offset: 0x0013E328
		[SecurityCritical]
		internal static bool IsCustomErrorEnabled()
		{
			object data = CallContext.GetData("__CustomErrorsEnabled");
			return data != null && (bool)data;
		}

		// Token: 0x04002955 RID: 10581
		internal object _ret;

		// Token: 0x04002956 RID: 10582
		internal object _properties;

		// Token: 0x04002957 RID: 10583
		internal string _URI;

		// Token: 0x04002958 RID: 10584
		internal Exception _e;

		// Token: 0x04002959 RID: 10585
		internal object[] _outArgs;

		// Token: 0x0400295A RID: 10586
		internal int _outArgsCount;

		// Token: 0x0400295B RID: 10587
		internal string _methodName;

		// Token: 0x0400295C RID: 10588
		internal string _typeName;

		// Token: 0x0400295D RID: 10589
		internal Type[] _methodSignature;

		// Token: 0x0400295E RID: 10590
		internal bool _hasVarArgs;

		// Token: 0x0400295F RID: 10591
		internal LogicalCallContext _callContext;

		// Token: 0x04002960 RID: 10592
		internal ArgMapper _argMapper;

		// Token: 0x04002961 RID: 10593
		internal MethodBase _methodBase;
	}
}
