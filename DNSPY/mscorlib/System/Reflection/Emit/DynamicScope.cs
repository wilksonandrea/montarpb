using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security;

namespace System.Reflection.Emit
{
	// Token: 0x02000636 RID: 1590
	internal class DynamicScope
	{
		// Token: 0x06004A27 RID: 18983 RVA: 0x0010C27F File Offset: 0x0010A47F
		internal DynamicScope()
		{
			this.m_tokens = new List<object>();
			this.m_tokens.Add(null);
		}

		// Token: 0x17000B90 RID: 2960
		internal object this[int token]
		{
			get
			{
				token &= 16777215;
				if (token < 0 || token > this.m_tokens.Count)
				{
					return null;
				}
				return this.m_tokens[token];
			}
		}

		// Token: 0x06004A29 RID: 18985 RVA: 0x0010C2C9 File Offset: 0x0010A4C9
		internal int GetTokenFor(VarArgMethod varArgMethod)
		{
			this.m_tokens.Add(varArgMethod);
			return (this.m_tokens.Count - 1) | 167772160;
		}

		// Token: 0x06004A2A RID: 18986 RVA: 0x0010C2EA File Offset: 0x0010A4EA
		internal string GetString(int token)
		{
			return this[token] as string;
		}

		// Token: 0x06004A2B RID: 18987 RVA: 0x0010C2F8 File Offset: 0x0010A4F8
		internal byte[] ResolveSignature(int token, int fromMethod)
		{
			if (fromMethod == 0)
			{
				return (byte[])this[token];
			}
			VarArgMethod varArgMethod = this[token] as VarArgMethod;
			if (varArgMethod == null)
			{
				return null;
			}
			return varArgMethod.m_signature.GetSignature(true);
		}

		// Token: 0x06004A2C RID: 18988 RVA: 0x0010C334 File Offset: 0x0010A534
		[SecuritySafeCritical]
		public int GetTokenFor(RuntimeMethodHandle method)
		{
			IRuntimeMethodInfo methodInfo = method.GetMethodInfo();
			RuntimeMethodHandleInternal value = methodInfo.Value;
			if (methodInfo != null && !RuntimeMethodHandle.IsDynamicMethod(value))
			{
				RuntimeType declaringType = RuntimeMethodHandle.GetDeclaringType(value);
				if (declaringType != null && RuntimeTypeHandle.HasInstantiation(declaringType))
				{
					MethodBase methodBase = RuntimeType.GetMethodBase(methodInfo);
					Type genericTypeDefinition = methodBase.DeclaringType.GetGenericTypeDefinition();
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_MethodDeclaringTypeGenericLcg"), methodBase, genericTypeDefinition));
				}
			}
			this.m_tokens.Add(method);
			return (this.m_tokens.Count - 1) | 100663296;
		}

		// Token: 0x06004A2D RID: 18989 RVA: 0x0010C3C8 File Offset: 0x0010A5C8
		public int GetTokenFor(RuntimeMethodHandle method, RuntimeTypeHandle typeContext)
		{
			this.m_tokens.Add(new GenericMethodInfo(method, typeContext));
			return (this.m_tokens.Count - 1) | 100663296;
		}

		// Token: 0x06004A2E RID: 18990 RVA: 0x0010C3EF File Offset: 0x0010A5EF
		public int GetTokenFor(DynamicMethod method)
		{
			this.m_tokens.Add(method);
			return (this.m_tokens.Count - 1) | 100663296;
		}

		// Token: 0x06004A2F RID: 18991 RVA: 0x0010C410 File Offset: 0x0010A610
		public int GetTokenFor(RuntimeFieldHandle field)
		{
			this.m_tokens.Add(field);
			return (this.m_tokens.Count - 1) | 67108864;
		}

		// Token: 0x06004A30 RID: 18992 RVA: 0x0010C436 File Offset: 0x0010A636
		public int GetTokenFor(RuntimeFieldHandle field, RuntimeTypeHandle typeContext)
		{
			this.m_tokens.Add(new GenericFieldInfo(field, typeContext));
			return (this.m_tokens.Count - 1) | 67108864;
		}

		// Token: 0x06004A31 RID: 18993 RVA: 0x0010C45D File Offset: 0x0010A65D
		public int GetTokenFor(RuntimeTypeHandle type)
		{
			this.m_tokens.Add(type);
			return (this.m_tokens.Count - 1) | 33554432;
		}

		// Token: 0x06004A32 RID: 18994 RVA: 0x0010C483 File Offset: 0x0010A683
		public int GetTokenFor(string literal)
		{
			this.m_tokens.Add(literal);
			return (this.m_tokens.Count - 1) | 1879048192;
		}

		// Token: 0x06004A33 RID: 18995 RVA: 0x0010C4A4 File Offset: 0x0010A6A4
		public int GetTokenFor(byte[] signature)
		{
			this.m_tokens.Add(signature);
			return (this.m_tokens.Count - 1) | 285212672;
		}

		// Token: 0x04001E9D RID: 7837
		internal List<object> m_tokens;
	}
}
