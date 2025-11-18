using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Services
{
	// Token: 0x02000809 RID: 2057
	[SecurityCritical]
	[ComVisible(true)]
	public class TrackingServices
	{
		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x0600587C RID: 22652 RVA: 0x00137D54 File Offset: 0x00135F54
		private static object TrackingServicesSyncObject
		{
			get
			{
				if (TrackingServices.s_TrackingServicesSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref TrackingServices.s_TrackingServicesSyncObject, obj, null);
				}
				return TrackingServices.s_TrackingServicesSyncObject;
			}
		}

		// Token: 0x0600587D RID: 22653 RVA: 0x00137D80 File Offset: 0x00135F80
		[SecurityCritical]
		public static void RegisterTrackingHandler(ITrackingHandler handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			object trackingServicesSyncObject = TrackingServices.TrackingServicesSyncObject;
			lock (trackingServicesSyncObject)
			{
				if (-1 != TrackingServices.Match(handler))
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_TrackingHandlerAlreadyRegistered", new object[] { "handler" }));
				}
				if (TrackingServices._Handlers == null || TrackingServices._Size == TrackingServices._Handlers.Length)
				{
					ITrackingHandler[] array = new ITrackingHandler[TrackingServices._Size * 2 + 4];
					if (TrackingServices._Handlers != null)
					{
						Array.Copy(TrackingServices._Handlers, array, TrackingServices._Size);
					}
					TrackingServices._Handlers = array;
				}
				Volatile.Write<ITrackingHandler>(ref TrackingServices._Handlers[TrackingServices._Size++], handler);
			}
		}

		// Token: 0x0600587E RID: 22654 RVA: 0x00137E64 File Offset: 0x00136064
		[SecurityCritical]
		public static void UnregisterTrackingHandler(ITrackingHandler handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			object trackingServicesSyncObject = TrackingServices.TrackingServicesSyncObject;
			lock (trackingServicesSyncObject)
			{
				int num = TrackingServices.Match(handler);
				if (-1 == num)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_HandlerNotRegistered", new object[] { handler }));
				}
				Array.Copy(TrackingServices._Handlers, num + 1, TrackingServices._Handlers, num, TrackingServices._Size - num - 1);
				TrackingServices._Size--;
			}
		}

		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x0600587F RID: 22655 RVA: 0x00137F04 File Offset: 0x00136104
		public static ITrackingHandler[] RegisteredHandlers
		{
			[SecurityCritical]
			get
			{
				object trackingServicesSyncObject = TrackingServices.TrackingServicesSyncObject;
				ITrackingHandler[] array;
				lock (trackingServicesSyncObject)
				{
					if (TrackingServices._Size == 0)
					{
						array = new ITrackingHandler[0];
					}
					else
					{
						ITrackingHandler[] array2 = new ITrackingHandler[TrackingServices._Size];
						for (int i = 0; i < TrackingServices._Size; i++)
						{
							array2[i] = TrackingServices._Handlers[i];
						}
						array = array2;
					}
				}
				return array;
			}
		}

		// Token: 0x06005880 RID: 22656 RVA: 0x00137F84 File Offset: 0x00136184
		[SecurityCritical]
		internal static void MarshaledObject(object obj, ObjRef or)
		{
			try
			{
				ITrackingHandler[] handlers = TrackingServices._Handlers;
				for (int i = 0; i < TrackingServices._Size; i++)
				{
					Volatile.Read<ITrackingHandler>(ref handlers[i]).MarshaledObject(obj, or);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06005881 RID: 22657 RVA: 0x00137FD4 File Offset: 0x001361D4
		[SecurityCritical]
		internal static void UnmarshaledObject(object obj, ObjRef or)
		{
			try
			{
				ITrackingHandler[] handlers = TrackingServices._Handlers;
				for (int i = 0; i < TrackingServices._Size; i++)
				{
					Volatile.Read<ITrackingHandler>(ref handlers[i]).UnmarshaledObject(obj, or);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06005882 RID: 22658 RVA: 0x00138024 File Offset: 0x00136224
		[SecurityCritical]
		internal static void DisconnectedObject(object obj)
		{
			try
			{
				ITrackingHandler[] handlers = TrackingServices._Handlers;
				for (int i = 0; i < TrackingServices._Size; i++)
				{
					Volatile.Read<ITrackingHandler>(ref handlers[i]).DisconnectedObject(obj);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06005883 RID: 22659 RVA: 0x00138074 File Offset: 0x00136274
		private static int Match(ITrackingHandler handler)
		{
			int num = -1;
			for (int i = 0; i < TrackingServices._Size; i++)
			{
				if (TrackingServices._Handlers[i] == handler)
				{
					num = i;
					break;
				}
			}
			return num;
		}

		// Token: 0x06005884 RID: 22660 RVA: 0x001380A6 File Offset: 0x001362A6
		public TrackingServices()
		{
		}

		// Token: 0x06005885 RID: 22661 RVA: 0x001380AE File Offset: 0x001362AE
		// Note: this type is marked as 'beforefieldinit'.
		static TrackingServices()
		{
		}

		// Token: 0x0400285E RID: 10334
		private static volatile ITrackingHandler[] _Handlers = new ITrackingHandler[0];

		// Token: 0x0400285F RID: 10335
		private static volatile int _Size = 0;

		// Token: 0x04002860 RID: 10336
		private static object s_TrackingServicesSyncObject = null;
	}
}
