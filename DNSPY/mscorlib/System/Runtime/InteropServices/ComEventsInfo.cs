using System;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x020009AF RID: 2479
	[SecurityCritical]
	internal class ComEventsInfo
	{
		// Token: 0x06006319 RID: 25369 RVA: 0x00151A50 File Offset: 0x0014FC50
		private ComEventsInfo(object rcw)
		{
			this._rcw = rcw;
		}

		// Token: 0x0600631A RID: 25370 RVA: 0x00151A60 File Offset: 0x0014FC60
		[SecuritySafeCritical]
		~ComEventsInfo()
		{
			this._sinks = ComEventsSink.RemoveAll(this._sinks);
		}

		// Token: 0x0600631B RID: 25371 RVA: 0x00151A98 File Offset: 0x0014FC98
		[SecurityCritical]
		internal static ComEventsInfo Find(object rcw)
		{
			return (ComEventsInfo)Marshal.GetComObjectData(rcw, typeof(ComEventsInfo));
		}

		// Token: 0x0600631C RID: 25372 RVA: 0x00151AB0 File Offset: 0x0014FCB0
		[SecurityCritical]
		internal static ComEventsInfo FromObject(object rcw)
		{
			ComEventsInfo comEventsInfo = ComEventsInfo.Find(rcw);
			if (comEventsInfo == null)
			{
				comEventsInfo = new ComEventsInfo(rcw);
				Marshal.SetComObjectData(rcw, typeof(ComEventsInfo), comEventsInfo);
			}
			return comEventsInfo;
		}

		// Token: 0x0600631D RID: 25373 RVA: 0x00151AE1 File Offset: 0x0014FCE1
		internal ComEventsSink FindSink(ref Guid iid)
		{
			return ComEventsSink.Find(this._sinks, ref iid);
		}

		// Token: 0x0600631E RID: 25374 RVA: 0x00151AF0 File Offset: 0x0014FCF0
		internal ComEventsSink AddSink(ref Guid iid)
		{
			ComEventsSink comEventsSink = new ComEventsSink(this._rcw, iid);
			this._sinks = ComEventsSink.Add(this._sinks, comEventsSink);
			return this._sinks;
		}

		// Token: 0x0600631F RID: 25375 RVA: 0x00151B27 File Offset: 0x0014FD27
		[SecurityCritical]
		internal ComEventsSink RemoveSink(ComEventsSink sink)
		{
			this._sinks = ComEventsSink.Remove(this._sinks, sink);
			return this._sinks;
		}

		// Token: 0x04002CBD RID: 11453
		private ComEventsSink _sinks;

		// Token: 0x04002CBE RID: 11454
		private object _rcw;
	}
}
