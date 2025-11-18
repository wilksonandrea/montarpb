using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200064A RID: 1610
	[ComVisible(false)]
	public struct ExceptionHandler : IEquatable<ExceptionHandler>
	{
		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06004B79 RID: 19321 RVA: 0x001114E7 File Offset: 0x0010F6E7
		public int ExceptionTypeToken
		{
			get
			{
				return this.m_exceptionClass;
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06004B7A RID: 19322 RVA: 0x001114EF File Offset: 0x0010F6EF
		public int TryOffset
		{
			get
			{
				return this.m_tryStartOffset;
			}
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06004B7B RID: 19323 RVA: 0x001114F7 File Offset: 0x0010F6F7
		public int TryLength
		{
			get
			{
				return this.m_tryEndOffset - this.m_tryStartOffset;
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06004B7C RID: 19324 RVA: 0x00111506 File Offset: 0x0010F706
		public int FilterOffset
		{
			get
			{
				return this.m_filterOffset;
			}
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06004B7D RID: 19325 RVA: 0x0011150E File Offset: 0x0010F70E
		public int HandlerOffset
		{
			get
			{
				return this.m_handlerStartOffset;
			}
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06004B7E RID: 19326 RVA: 0x00111516 File Offset: 0x0010F716
		public int HandlerLength
		{
			get
			{
				return this.m_handlerEndOffset - this.m_handlerStartOffset;
			}
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06004B7F RID: 19327 RVA: 0x00111525 File Offset: 0x0010F725
		public ExceptionHandlingClauseOptions Kind
		{
			get
			{
				return this.m_kind;
			}
		}

		// Token: 0x06004B80 RID: 19328 RVA: 0x00111530 File Offset: 0x0010F730
		public ExceptionHandler(int tryOffset, int tryLength, int filterOffset, int handlerOffset, int handlerLength, ExceptionHandlingClauseOptions kind, int exceptionTypeToken)
		{
			if (tryOffset < 0)
			{
				throw new ArgumentOutOfRangeException("tryOffset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (tryLength < 0)
			{
				throw new ArgumentOutOfRangeException("tryLength", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (filterOffset < 0)
			{
				throw new ArgumentOutOfRangeException("filterOffset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (handlerOffset < 0)
			{
				throw new ArgumentOutOfRangeException("handlerOffset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (handlerLength < 0)
			{
				throw new ArgumentOutOfRangeException("handlerLength", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if ((long)tryOffset + (long)tryLength > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("tryLength", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					0,
					int.MaxValue - tryOffset
				}));
			}
			if ((long)handlerOffset + (long)handlerLength > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("handlerLength", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					0,
					int.MaxValue - handlerOffset
				}));
			}
			if (kind == ExceptionHandlingClauseOptions.Clause && (exceptionTypeToken & 16777215) == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidTypeToken", new object[] { exceptionTypeToken }), "exceptionTypeToken");
			}
			if (!ExceptionHandler.IsValidKind(kind))
			{
				throw new ArgumentOutOfRangeException("kind", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
			}
			this.m_tryStartOffset = tryOffset;
			this.m_tryEndOffset = tryOffset + tryLength;
			this.m_filterOffset = filterOffset;
			this.m_handlerStartOffset = handlerOffset;
			this.m_handlerEndOffset = handlerOffset + handlerLength;
			this.m_kind = kind;
			this.m_exceptionClass = exceptionTypeToken;
		}

		// Token: 0x06004B81 RID: 19329 RVA: 0x001116CA File Offset: 0x0010F8CA
		internal ExceptionHandler(int tryStartOffset, int tryEndOffset, int filterOffset, int handlerStartOffset, int handlerEndOffset, int kind, int exceptionTypeToken)
		{
			this.m_tryStartOffset = tryStartOffset;
			this.m_tryEndOffset = tryEndOffset;
			this.m_filterOffset = filterOffset;
			this.m_handlerStartOffset = handlerStartOffset;
			this.m_handlerEndOffset = handlerEndOffset;
			this.m_kind = (ExceptionHandlingClauseOptions)kind;
			this.m_exceptionClass = exceptionTypeToken;
		}

		// Token: 0x06004B82 RID: 19330 RVA: 0x00111701 File Offset: 0x0010F901
		private static bool IsValidKind(ExceptionHandlingClauseOptions kind)
		{
			return kind <= ExceptionHandlingClauseOptions.Finally || kind == ExceptionHandlingClauseOptions.Fault;
		}

		// Token: 0x06004B83 RID: 19331 RVA: 0x0011170E File Offset: 0x0010F90E
		public override int GetHashCode()
		{
			return this.m_exceptionClass ^ this.m_tryStartOffset ^ this.m_tryEndOffset ^ this.m_filterOffset ^ this.m_handlerStartOffset ^ this.m_handlerEndOffset ^ (int)this.m_kind;
		}

		// Token: 0x06004B84 RID: 19332 RVA: 0x00111740 File Offset: 0x0010F940
		public override bool Equals(object obj)
		{
			return obj is ExceptionHandler && this.Equals((ExceptionHandler)obj);
		}

		// Token: 0x06004B85 RID: 19333 RVA: 0x00111758 File Offset: 0x0010F958
		public bool Equals(ExceptionHandler other)
		{
			return other.m_exceptionClass == this.m_exceptionClass && other.m_tryStartOffset == this.m_tryStartOffset && other.m_tryEndOffset == this.m_tryEndOffset && other.m_filterOffset == this.m_filterOffset && other.m_handlerStartOffset == this.m_handlerStartOffset && other.m_handlerEndOffset == this.m_handlerEndOffset && other.m_kind == this.m_kind;
		}

		// Token: 0x06004B86 RID: 19334 RVA: 0x001117C9 File Offset: 0x0010F9C9
		public static bool operator ==(ExceptionHandler left, ExceptionHandler right)
		{
			return left.Equals(right);
		}

		// Token: 0x06004B87 RID: 19335 RVA: 0x001117D3 File Offset: 0x0010F9D3
		public static bool operator !=(ExceptionHandler left, ExceptionHandler right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04001F3A RID: 7994
		internal readonly int m_exceptionClass;

		// Token: 0x04001F3B RID: 7995
		internal readonly int m_tryStartOffset;

		// Token: 0x04001F3C RID: 7996
		internal readonly int m_tryEndOffset;

		// Token: 0x04001F3D RID: 7997
		internal readonly int m_filterOffset;

		// Token: 0x04001F3E RID: 7998
		internal readonly int m_handlerStartOffset;

		// Token: 0x04001F3F RID: 7999
		internal readonly int m_handlerEndOffset;

		// Token: 0x04001F40 RID: 8000
		internal readonly ExceptionHandlingClauseOptions m_kind;
	}
}
