using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000614 RID: 1556
	[ComVisible(true)]
	public class ExceptionHandlingClause
	{
		// Token: 0x06004802 RID: 18434 RVA: 0x00105C27 File Offset: 0x00103E27
		protected ExceptionHandlingClause()
		{
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06004803 RID: 18435 RVA: 0x00105C2F File Offset: 0x00103E2F
		public virtual ExceptionHandlingClauseOptions Flags
		{
			get
			{
				return this.m_flags;
			}
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06004804 RID: 18436 RVA: 0x00105C37 File Offset: 0x00103E37
		public virtual int TryOffset
		{
			get
			{
				return this.m_tryOffset;
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06004805 RID: 18437 RVA: 0x00105C3F File Offset: 0x00103E3F
		public virtual int TryLength
		{
			get
			{
				return this.m_tryLength;
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06004806 RID: 18438 RVA: 0x00105C47 File Offset: 0x00103E47
		public virtual int HandlerOffset
		{
			get
			{
				return this.m_handlerOffset;
			}
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06004807 RID: 18439 RVA: 0x00105C4F File Offset: 0x00103E4F
		public virtual int HandlerLength
		{
			get
			{
				return this.m_handlerLength;
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06004808 RID: 18440 RVA: 0x00105C57 File Offset: 0x00103E57
		public virtual int FilterOffset
		{
			get
			{
				if (this.m_flags != ExceptionHandlingClauseOptions.Filter)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Arg_EHClauseNotFilter"));
				}
				return this.m_filterOffset;
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06004809 RID: 18441 RVA: 0x00105C78 File Offset: 0x00103E78
		public virtual Type CatchType
		{
			get
			{
				if (this.m_flags != ExceptionHandlingClauseOptions.Clause)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Arg_EHClauseNotClause"));
				}
				Type type = null;
				if (!MetadataToken.IsNullToken(this.m_catchMetadataToken))
				{
					Type declaringType = this.m_methodBody.m_methodBase.DeclaringType;
					Module module = ((declaringType == null) ? this.m_methodBody.m_methodBase.Module : declaringType.Module);
					type = module.ResolveType(this.m_catchMetadataToken, (declaringType == null) ? null : declaringType.GetGenericArguments(), (this.m_methodBody.m_methodBase is MethodInfo) ? this.m_methodBody.m_methodBase.GetGenericArguments() : null);
				}
				return type;
			}
		}

		// Token: 0x0600480A RID: 18442 RVA: 0x00105D24 File Offset: 0x00103F24
		public override string ToString()
		{
			if (this.Flags == ExceptionHandlingClauseOptions.Clause)
			{
				return string.Format(CultureInfo.CurrentUICulture, "Flags={0}, TryOffset={1}, TryLength={2}, HandlerOffset={3}, HandlerLength={4}, CatchType={5}", new object[] { this.Flags, this.TryOffset, this.TryLength, this.HandlerOffset, this.HandlerLength, this.CatchType });
			}
			if (this.Flags == ExceptionHandlingClauseOptions.Filter)
			{
				return string.Format(CultureInfo.CurrentUICulture, "Flags={0}, TryOffset={1}, TryLength={2}, HandlerOffset={3}, HandlerLength={4}, FilterOffset={5}", new object[] { this.Flags, this.TryOffset, this.TryLength, this.HandlerOffset, this.HandlerLength, this.FilterOffset });
			}
			return string.Format(CultureInfo.CurrentUICulture, "Flags={0}, TryOffset={1}, TryLength={2}, HandlerOffset={3}, HandlerLength={4}", new object[] { this.Flags, this.TryOffset, this.TryLength, this.HandlerOffset, this.HandlerLength });
		}

		// Token: 0x04001DD6 RID: 7638
		private MethodBody m_methodBody;

		// Token: 0x04001DD7 RID: 7639
		private ExceptionHandlingClauseOptions m_flags;

		// Token: 0x04001DD8 RID: 7640
		private int m_tryOffset;

		// Token: 0x04001DD9 RID: 7641
		private int m_tryLength;

		// Token: 0x04001DDA RID: 7642
		private int m_handlerOffset;

		// Token: 0x04001DDB RID: 7643
		private int m_handlerLength;

		// Token: 0x04001DDC RID: 7644
		private int m_catchMetadataToken;

		// Token: 0x04001DDD RID: 7645
		private int m_filterOffset;
	}
}
