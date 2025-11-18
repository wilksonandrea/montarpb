using System;
using System.Security;
using System.Threading;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008D9 RID: 2265
	[FriendAccessAllowed]
	internal static class JitHelpers
	{
		// Token: 0x06005DCE RID: 24014 RVA: 0x00149740 File Offset: 0x00147940
		[SecurityCritical]
		internal static StringHandleOnStack GetStringHandleOnStack(ref string s)
		{
			return new StringHandleOnStack(JitHelpers.UnsafeCastToStackPointer<string>(ref s));
		}

		// Token: 0x06005DCF RID: 24015 RVA: 0x0014974D File Offset: 0x0014794D
		[SecurityCritical]
		internal static ObjectHandleOnStack GetObjectHandleOnStack<T>(ref T o) where T : class
		{
			return new ObjectHandleOnStack(JitHelpers.UnsafeCastToStackPointer<T>(ref o));
		}

		// Token: 0x06005DD0 RID: 24016 RVA: 0x0014975A File Offset: 0x0014795A
		[SecurityCritical]
		internal static StackCrawlMarkHandle GetStackCrawlMarkHandle(ref StackCrawlMark stackMark)
		{
			return new StackCrawlMarkHandle(JitHelpers.UnsafeCastToStackPointer<StackCrawlMark>(ref stackMark));
		}

		// Token: 0x06005DD1 RID: 24017 RVA: 0x00149767 File Offset: 0x00147967
		[SecurityCritical]
		[FriendAccessAllowed]
		internal static T UnsafeCast<T>(object o) where T : class
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06005DD2 RID: 24018 RVA: 0x0014976E File Offset: 0x0014796E
		internal static int UnsafeEnumCast<T>(T val) where T : struct
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06005DD3 RID: 24019 RVA: 0x00149775 File Offset: 0x00147975
		internal static long UnsafeEnumCastLong<T>(T val) where T : struct
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06005DD4 RID: 24020 RVA: 0x0014977C File Offset: 0x0014797C
		[SecurityCritical]
		internal static IntPtr UnsafeCastToStackPointer<T>(ref T val)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06005DD5 RID: 24021
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void UnsafeSetArrayElement(object[] target, int index, object element);

		// Token: 0x06005DD6 RID: 24022 RVA: 0x00149783 File Offset: 0x00147983
		[SecurityCritical]
		internal static PinningHelper GetPinningHelper(object o)
		{
			return JitHelpers.UnsafeCast<PinningHelper>(o);
		}

		// Token: 0x04002A40 RID: 10816
		internal const string QCall = "QCall";
	}
}
