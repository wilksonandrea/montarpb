using System;
using System.Collections;
using System.Reflection;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000873 RID: 2163
	internal class ErrorMessage : IMethodCallMessage, IMethodMessage, IMessage
	{
		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x06005BF1 RID: 23537 RVA: 0x00142941 File Offset: 0x00140B41
		public IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x06005BF2 RID: 23538 RVA: 0x00142944 File Offset: 0x00140B44
		public string Uri
		{
			[SecurityCritical]
			get
			{
				return this.m_URI;
			}
		}

		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x06005BF3 RID: 23539 RVA: 0x0014294C File Offset: 0x00140B4C
		public string MethodName
		{
			[SecurityCritical]
			get
			{
				return this.m_MethodName;
			}
		}

		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x06005BF4 RID: 23540 RVA: 0x00142954 File Offset: 0x00140B54
		public string TypeName
		{
			[SecurityCritical]
			get
			{
				return this.m_TypeName;
			}
		}

		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x06005BF5 RID: 23541 RVA: 0x0014295C File Offset: 0x00140B5C
		public object MethodSignature
		{
			[SecurityCritical]
			get
			{
				return this.m_MethodSignature;
			}
		}

		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x06005BF6 RID: 23542 RVA: 0x00142964 File Offset: 0x00140B64
		public MethodBase MethodBase
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x06005BF7 RID: 23543 RVA: 0x00142967 File Offset: 0x00140B67
		public int ArgCount
		{
			[SecurityCritical]
			get
			{
				return this.m_ArgCount;
			}
		}

		// Token: 0x06005BF8 RID: 23544 RVA: 0x0014296F File Offset: 0x00140B6F
		[SecurityCritical]
		public string GetArgName(int index)
		{
			return this.m_ArgName;
		}

		// Token: 0x06005BF9 RID: 23545 RVA: 0x00142977 File Offset: 0x00140B77
		[SecurityCritical]
		public object GetArg(int argNum)
		{
			return null;
		}

		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x06005BFA RID: 23546 RVA: 0x0014297A File Offset: 0x00140B7A
		public object[] Args
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x06005BFB RID: 23547 RVA: 0x0014297D File Offset: 0x00140B7D
		public bool HasVarArgs
		{
			[SecurityCritical]
			get
			{
				return false;
			}
		}

		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x06005BFC RID: 23548 RVA: 0x00142980 File Offset: 0x00140B80
		public int InArgCount
		{
			[SecurityCritical]
			get
			{
				return this.m_ArgCount;
			}
		}

		// Token: 0x06005BFD RID: 23549 RVA: 0x00142988 File Offset: 0x00140B88
		[SecurityCritical]
		public string GetInArgName(int index)
		{
			return null;
		}

		// Token: 0x06005BFE RID: 23550 RVA: 0x0014298B File Offset: 0x00140B8B
		[SecurityCritical]
		public object GetInArg(int argNum)
		{
			return null;
		}

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x06005BFF RID: 23551 RVA: 0x0014298E File Offset: 0x00140B8E
		public object[] InArgs
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x06005C00 RID: 23552 RVA: 0x00142991 File Offset: 0x00140B91
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x06005C01 RID: 23553 RVA: 0x00142994 File Offset: 0x00140B94
		public ErrorMessage()
		{
		}

		// Token: 0x04002995 RID: 10645
		private string m_URI = "Exception";

		// Token: 0x04002996 RID: 10646
		private string m_MethodName = "Unknown";

		// Token: 0x04002997 RID: 10647
		private string m_TypeName = "Unknown";

		// Token: 0x04002998 RID: 10648
		private object m_MethodSignature;

		// Token: 0x04002999 RID: 10649
		private int m_ArgCount;

		// Token: 0x0400299A RID: 10650
		private string m_ArgName = "Unknown";
	}
}
