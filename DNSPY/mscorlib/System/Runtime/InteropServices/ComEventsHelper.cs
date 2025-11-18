using System;
using System.Runtime.Remoting;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x020009AE RID: 2478
	[__DynamicallyInvokable]
	public static class ComEventsHelper
	{
		// Token: 0x06006316 RID: 25366 RVA: 0x001518E0 File Offset: 0x0014FAE0
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static void Combine(object rcw, Guid iid, int dispid, Delegate d)
		{
			rcw = ComEventsHelper.UnwrapIfTransparentProxy(rcw);
			object obj = rcw;
			lock (obj)
			{
				ComEventsInfo comEventsInfo = ComEventsInfo.FromObject(rcw);
				ComEventsSink comEventsSink = comEventsInfo.FindSink(ref iid);
				if (comEventsSink == null)
				{
					comEventsSink = comEventsInfo.AddSink(ref iid);
				}
				ComEventsMethod comEventsMethod = comEventsSink.FindMethod(dispid);
				if (comEventsMethod == null)
				{
					comEventsMethod = comEventsSink.AddMethod(dispid);
				}
				comEventsMethod.AddDelegate(d);
			}
		}

		// Token: 0x06006317 RID: 25367 RVA: 0x00151958 File Offset: 0x0014FB58
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static Delegate Remove(object rcw, Guid iid, int dispid, Delegate d)
		{
			rcw = ComEventsHelper.UnwrapIfTransparentProxy(rcw);
			object obj = rcw;
			Delegate @delegate;
			lock (obj)
			{
				ComEventsInfo comEventsInfo = ComEventsInfo.Find(rcw);
				if (comEventsInfo == null)
				{
					@delegate = null;
				}
				else
				{
					ComEventsSink comEventsSink = comEventsInfo.FindSink(ref iid);
					if (comEventsSink == null)
					{
						@delegate = null;
					}
					else
					{
						ComEventsMethod comEventsMethod = comEventsSink.FindMethod(dispid);
						if (comEventsMethod == null)
						{
							@delegate = null;
						}
						else
						{
							comEventsMethod.RemoveDelegate(d);
							if (comEventsMethod.Empty)
							{
								comEventsMethod = comEventsSink.RemoveMethod(comEventsMethod);
							}
							if (comEventsMethod == null)
							{
								comEventsSink = comEventsInfo.RemoveSink(comEventsSink);
							}
							if (comEventsSink == null)
							{
								Marshal.SetComObjectData(rcw, typeof(ComEventsInfo), null);
								GC.SuppressFinalize(comEventsInfo);
							}
							@delegate = d;
						}
					}
				}
			}
			return @delegate;
		}

		// Token: 0x06006318 RID: 25368 RVA: 0x00151A10 File Offset: 0x0014FC10
		[SecurityCritical]
		internal static object UnwrapIfTransparentProxy(object rcw)
		{
			if (RemotingServices.IsTransparentProxy(rcw))
			{
				IntPtr iunknownForObject = Marshal.GetIUnknownForObject(rcw);
				try
				{
					rcw = Marshal.GetObjectForIUnknown(iunknownForObject);
				}
				finally
				{
					Marshal.Release(iunknownForObject);
				}
			}
			return rcw;
		}
	}
}
