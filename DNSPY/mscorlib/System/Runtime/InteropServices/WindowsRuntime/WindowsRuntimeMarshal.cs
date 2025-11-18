using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009F8 RID: 2552
	public static class WindowsRuntimeMarshal
	{
		// Token: 0x060064CF RID: 25807 RVA: 0x00157158 File Offset: 0x00155358
		[SecurityCritical]
		public static void AddEventHandler<T>(Func<T, EventRegistrationToken> addMethod, Action<EventRegistrationToken> removeMethod, T handler)
		{
			if (addMethod == null)
			{
				throw new ArgumentNullException("addMethod");
			}
			if (removeMethod == null)
			{
				throw new ArgumentNullException("removeMethod");
			}
			if (handler == null)
			{
				return;
			}
			object target = removeMethod.Target;
			if (target == null || Marshal.IsComObject(target))
			{
				WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.AddEventHandler<T>(addMethod, removeMethod, handler);
				return;
			}
			WindowsRuntimeMarshal.ManagedEventRegistrationImpl.AddEventHandler<T>(addMethod, removeMethod, handler);
		}

		// Token: 0x060064D0 RID: 25808 RVA: 0x001571B0 File Offset: 0x001553B0
		[SecurityCritical]
		public static void RemoveEventHandler<T>(Action<EventRegistrationToken> removeMethod, T handler)
		{
			if (removeMethod == null)
			{
				throw new ArgumentNullException("removeMethod");
			}
			if (handler == null)
			{
				return;
			}
			object target = removeMethod.Target;
			if (target == null || Marshal.IsComObject(target))
			{
				WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.RemoveEventHandler<T>(removeMethod, handler);
				return;
			}
			WindowsRuntimeMarshal.ManagedEventRegistrationImpl.RemoveEventHandler<T>(removeMethod, handler);
		}

		// Token: 0x060064D1 RID: 25809 RVA: 0x001571F8 File Offset: 0x001553F8
		[SecurityCritical]
		public static void RemoveAllEventHandlers(Action<EventRegistrationToken> removeMethod)
		{
			if (removeMethod == null)
			{
				throw new ArgumentNullException("removeMethod");
			}
			object target = removeMethod.Target;
			if (target == null || Marshal.IsComObject(target))
			{
				WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.RemoveAllEventHandlers(removeMethod);
				return;
			}
			WindowsRuntimeMarshal.ManagedEventRegistrationImpl.RemoveAllEventHandlers(removeMethod);
		}

		// Token: 0x060064D2 RID: 25810 RVA: 0x00157234 File Offset: 0x00155434
		internal static int GetRegistrationTokenCacheSize()
		{
			int num = 0;
			if (WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations != null)
			{
				ConditionalWeakTable<object, Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>> s_eventRegistrations = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations;
				lock (s_eventRegistrations)
				{
					num += WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations.Keys.Count;
				}
			}
			if (WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations != null)
			{
				Dictionary<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry> s_eventRegistrations2 = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations;
				lock (s_eventRegistrations2)
				{
					num += WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations.Count;
				}
			}
			return num;
		}

		// Token: 0x060064D3 RID: 25811 RVA: 0x001572D4 File Offset: 0x001554D4
		internal static void CallRemoveMethods(Action<EventRegistrationToken> removeMethod, List<EventRegistrationToken> tokensToRemove)
		{
			List<Exception> list = new List<Exception>();
			foreach (EventRegistrationToken eventRegistrationToken in tokensToRemove)
			{
				try
				{
					removeMethod(eventRegistrationToken);
				}
				catch (Exception ex)
				{
					list.Add(ex);
				}
			}
			if (list.Count > 0)
			{
				throw new AggregateException(list.ToArray());
			}
		}

		// Token: 0x060064D4 RID: 25812 RVA: 0x00157358 File Offset: 0x00155558
		[SecurityCritical]
		internal unsafe static string HStringToString(IntPtr hstring)
		{
			if (hstring == IntPtr.Zero)
			{
				return string.Empty;
			}
			uint num;
			char* ptr = UnsafeNativeMethods.WindowsGetStringRawBuffer(hstring, &num);
			return new string(ptr, 0, checked((int)num));
		}

		// Token: 0x060064D5 RID: 25813 RVA: 0x0015738C File Offset: 0x0015558C
		internal static Exception GetExceptionForHR(int hresult, Exception innerException, string messageResource)
		{
			Exception ex;
			if (innerException != null)
			{
				string text = innerException.Message;
				if (text == null && messageResource != null)
				{
					text = Environment.GetResourceString(messageResource);
				}
				ex = new Exception(text, innerException);
			}
			else
			{
				string text2 = ((messageResource != null) ? Environment.GetResourceString(messageResource) : null);
				ex = new Exception(text2);
			}
			ex.SetErrorCode(hresult);
			return ex;
		}

		// Token: 0x060064D6 RID: 25814 RVA: 0x001573D8 File Offset: 0x001555D8
		internal static Exception GetExceptionForHR(int hresult, Exception innerException)
		{
			return WindowsRuntimeMarshal.GetExceptionForHR(hresult, innerException, null);
		}

		// Token: 0x060064D7 RID: 25815 RVA: 0x001573E4 File Offset: 0x001555E4
		[SecurityCritical]
		private static bool RoOriginateLanguageException(int error, string message, IntPtr languageException)
		{
			if (WindowsRuntimeMarshal.s_haveBlueErrorApis)
			{
				try
				{
					return UnsafeNativeMethods.RoOriginateLanguageException(error, message, languageException);
				}
				catch (EntryPointNotFoundException)
				{
					WindowsRuntimeMarshal.s_haveBlueErrorApis = false;
				}
				return false;
			}
			return false;
		}

		// Token: 0x060064D8 RID: 25816 RVA: 0x00157420 File Offset: 0x00155620
		[SecurityCritical]
		private static void RoReportUnhandledError(IRestrictedErrorInfo error)
		{
			if (WindowsRuntimeMarshal.s_haveBlueErrorApis)
			{
				try
				{
					UnsafeNativeMethods.RoReportUnhandledError(error);
				}
				catch (EntryPointNotFoundException)
				{
					WindowsRuntimeMarshal.s_haveBlueErrorApis = false;
				}
			}
		}

		// Token: 0x060064D9 RID: 25817 RVA: 0x00157458 File Offset: 0x00155658
		[FriendAccessAllowed]
		[SecuritySafeCritical]
		internal static bool ReportUnhandledError(Exception e)
		{
			if (!AppDomain.IsAppXModel())
			{
				return false;
			}
			if (!WindowsRuntimeMarshal.s_haveBlueErrorApis)
			{
				return false;
			}
			if (e != null)
			{
				IntPtr intPtr = IntPtr.Zero;
				IntPtr zero = IntPtr.Zero;
				try
				{
					intPtr = Marshal.GetIUnknownForObject(e);
					if (intPtr != IntPtr.Zero)
					{
						Marshal.QueryInterface(intPtr, ref WindowsRuntimeMarshal.s_iidIErrorInfo, out zero);
						if (zero != IntPtr.Zero && WindowsRuntimeMarshal.RoOriginateLanguageException(Marshal.GetHRForException_WinRT(e), e.Message, zero))
						{
							IRestrictedErrorInfo restrictedErrorInfo = UnsafeNativeMethods.GetRestrictedErrorInfo();
							if (restrictedErrorInfo != null)
							{
								WindowsRuntimeMarshal.RoReportUnhandledError(restrictedErrorInfo);
								return true;
							}
						}
					}
				}
				finally
				{
					if (zero != IntPtr.Zero)
					{
						Marshal.Release(zero);
					}
					if (intPtr != IntPtr.Zero)
					{
						Marshal.Release(intPtr);
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060064DA RID: 25818 RVA: 0x00157520 File Offset: 0x00155720
		[SecurityCritical]
		internal static IntPtr GetActivationFactoryForType(Type type)
		{
			ManagedActivationFactory managedActivationFactory = WindowsRuntimeMarshal.GetManagedActivationFactory(type);
			return Marshal.GetComInterfaceForObject(managedActivationFactory, typeof(IActivationFactory));
		}

		// Token: 0x060064DB RID: 25819 RVA: 0x00157544 File Offset: 0x00155744
		[SecurityCritical]
		internal static ManagedActivationFactory GetManagedActivationFactory(Type type)
		{
			ManagedActivationFactory managedActivationFactory = new ManagedActivationFactory(type);
			Marshal.InitializeManagedWinRTFactoryObject(managedActivationFactory, (RuntimeType)type);
			return managedActivationFactory;
		}

		// Token: 0x060064DC RID: 25820 RVA: 0x00157568 File Offset: 0x00155768
		[SecurityCritical]
		internal static IntPtr GetClassActivatorForApplication(string appBase)
		{
			if (WindowsRuntimeMarshal.s_pClassActivator == IntPtr.Zero)
			{
				AppDomainSetup appDomainSetup = new AppDomainSetup
				{
					ApplicationBase = appBase
				};
				AppDomain appDomain = AppDomain.CreateDomain(Environment.GetResourceString("WinRTHostDomainName", new object[] { appBase }), null, appDomainSetup);
				WinRTClassActivator winRTClassActivator = (WinRTClassActivator)appDomain.CreateInstanceAndUnwrap(typeof(WinRTClassActivator).Assembly.FullName, typeof(WinRTClassActivator).FullName);
				IntPtr iwinRTClassActivator = winRTClassActivator.GetIWinRTClassActivator();
				if (Interlocked.CompareExchange(ref WindowsRuntimeMarshal.s_pClassActivator, iwinRTClassActivator, IntPtr.Zero) != IntPtr.Zero)
				{
					Marshal.Release(iwinRTClassActivator);
					try
					{
						AppDomain.Unload(appDomain);
					}
					catch (CannotUnloadAppDomainException)
					{
					}
				}
			}
			Marshal.AddRef(WindowsRuntimeMarshal.s_pClassActivator);
			return WindowsRuntimeMarshal.s_pClassActivator;
		}

		// Token: 0x060064DD RID: 25821 RVA: 0x00157638 File Offset: 0x00155838
		[SecurityCritical]
		public static IActivationFactory GetActivationFactory(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type.IsWindowsRuntimeObject && type.IsImport)
			{
				return (IActivationFactory)Marshal.GetNativeActivationFactory(type);
			}
			return WindowsRuntimeMarshal.GetManagedActivationFactory(type);
		}

		// Token: 0x060064DE RID: 25822 RVA: 0x00157670 File Offset: 0x00155870
		[SecurityCritical]
		public unsafe static IntPtr StringToHString(string s)
		{
			if (!Environment.IsWinRTSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
			}
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			IntPtr intPtr;
			int num = UnsafeNativeMethods.WindowsCreateString(s, s.Length, &intPtr);
			Marshal.ThrowExceptionForHR(num, new IntPtr(-1));
			return intPtr;
		}

		// Token: 0x060064DF RID: 25823 RVA: 0x001576BF File Offset: 0x001558BF
		[SecurityCritical]
		public static string PtrToStringHString(IntPtr ptr)
		{
			if (!Environment.IsWinRTSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
			}
			return WindowsRuntimeMarshal.HStringToString(ptr);
		}

		// Token: 0x060064E0 RID: 25824 RVA: 0x001576DE File Offset: 0x001558DE
		[SecurityCritical]
		public static void FreeHString(IntPtr ptr)
		{
			if (!Environment.IsWinRTSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
			}
			if (ptr != IntPtr.Zero)
			{
				UnsafeNativeMethods.WindowsDeleteString(ptr);
			}
		}

		// Token: 0x060064E1 RID: 25825 RVA: 0x0015770C File Offset: 0x0015590C
		// Note: this type is marked as 'beforefieldinit'.
		static WindowsRuntimeMarshal()
		{
		}

		// Token: 0x04002D2F RID: 11567
		private static bool s_haveBlueErrorApis = true;

		// Token: 0x04002D30 RID: 11568
		private static Guid s_iidIErrorInfo = new Guid(485667104, 21629, 4123, 142, 101, 8, 0, 43, 43, 209, 25);

		// Token: 0x04002D31 RID: 11569
		private static IntPtr s_pClassActivator = IntPtr.Zero;

		// Token: 0x02000CA7 RID: 3239
		internal struct EventRegistrationTokenList
		{
			// Token: 0x06007142 RID: 28994 RVA: 0x00185692 File Offset: 0x00183892
			internal EventRegistrationTokenList(EventRegistrationToken token)
			{
				this.firstToken = token;
				this.restTokens = null;
			}

			// Token: 0x06007143 RID: 28995 RVA: 0x001856A2 File Offset: 0x001838A2
			internal EventRegistrationTokenList(WindowsRuntimeMarshal.EventRegistrationTokenList list)
			{
				this.firstToken = list.firstToken;
				this.restTokens = list.restTokens;
			}

			// Token: 0x06007144 RID: 28996 RVA: 0x001856BC File Offset: 0x001838BC
			public bool Push(EventRegistrationToken token)
			{
				bool flag = false;
				if (this.restTokens == null)
				{
					this.restTokens = new List<EventRegistrationToken>();
					flag = true;
				}
				this.restTokens.Add(token);
				return flag;
			}

			// Token: 0x06007145 RID: 28997 RVA: 0x001856F0 File Offset: 0x001838F0
			public bool Pop(out EventRegistrationToken token)
			{
				if (this.restTokens == null || this.restTokens.Count == 0)
				{
					token = this.firstToken;
					return false;
				}
				int num = this.restTokens.Count - 1;
				token = this.restTokens[num];
				this.restTokens.RemoveAt(num);
				return true;
			}

			// Token: 0x06007146 RID: 28998 RVA: 0x0018574D File Offset: 0x0018394D
			public void CopyTo(List<EventRegistrationToken> tokens)
			{
				tokens.Add(this.firstToken);
				if (this.restTokens != null)
				{
					tokens.AddRange(this.restTokens);
				}
			}

			// Token: 0x0400388D RID: 14477
			private EventRegistrationToken firstToken;

			// Token: 0x0400388E RID: 14478
			private List<EventRegistrationToken> restTokens;
		}

		// Token: 0x02000CA8 RID: 3240
		internal static class ManagedEventRegistrationImpl
		{
			// Token: 0x06007147 RID: 28999 RVA: 0x00185770 File Offset: 0x00183970
			[SecurityCritical]
			internal static void AddEventHandler<T>(Func<T, EventRegistrationToken> addMethod, Action<EventRegistrationToken> removeMethod, T handler)
			{
				object target = removeMethod.Target;
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> eventRegistrationTokenTable = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.GetEventRegistrationTokenTable(target, removeMethod);
				EventRegistrationToken eventRegistrationToken = addMethod(handler);
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> dictionary = eventRegistrationTokenTable;
				lock (dictionary)
				{
					WindowsRuntimeMarshal.EventRegistrationTokenList eventRegistrationTokenList;
					if (!eventRegistrationTokenTable.TryGetValue(handler, out eventRegistrationTokenList))
					{
						eventRegistrationTokenList = new WindowsRuntimeMarshal.EventRegistrationTokenList(eventRegistrationToken);
						eventRegistrationTokenTable[handler] = eventRegistrationTokenList;
					}
					else
					{
						bool flag2 = eventRegistrationTokenList.Push(eventRegistrationToken);
						if (flag2)
						{
							eventRegistrationTokenTable[handler] = eventRegistrationTokenList;
						}
					}
				}
			}

			// Token: 0x06007148 RID: 29000 RVA: 0x00185804 File Offset: 0x00183A04
			private static Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> GetEventRegistrationTokenTable(object instance, Action<EventRegistrationToken> removeMethod)
			{
				ConditionalWeakTable<object, Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>> conditionalWeakTable = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations;
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> dictionary3;
				lock (conditionalWeakTable)
				{
					Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>> dictionary = null;
					if (!WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations.TryGetValue(instance, out dictionary))
					{
						dictionary = new Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>();
						WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations.Add(instance, dictionary);
					}
					Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> dictionary2 = null;
					if (!dictionary.TryGetValue(removeMethod.Method, out dictionary2))
					{
						dictionary2 = new Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>();
						dictionary.Add(removeMethod.Method, dictionary2);
					}
					dictionary3 = dictionary2;
				}
				return dictionary3;
			}

			// Token: 0x06007149 RID: 29001 RVA: 0x00185890 File Offset: 0x00183A90
			[SecurityCritical]
			internal static void RemoveEventHandler<T>(Action<EventRegistrationToken> removeMethod, T handler)
			{
				object target = removeMethod.Target;
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> eventRegistrationTokenTable = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.GetEventRegistrationTokenTable(target, removeMethod);
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> dictionary = eventRegistrationTokenTable;
				EventRegistrationToken eventRegistrationToken;
				lock (dictionary)
				{
					WindowsRuntimeMarshal.EventRegistrationTokenList eventRegistrationTokenList;
					if (!eventRegistrationTokenTable.TryGetValue(handler, out eventRegistrationTokenList))
					{
						return;
					}
					if (!eventRegistrationTokenList.Pop(out eventRegistrationToken))
					{
						eventRegistrationTokenTable.Remove(handler);
					}
				}
				removeMethod(eventRegistrationToken);
			}

			// Token: 0x0600714A RID: 29002 RVA: 0x0018590C File Offset: 0x00183B0C
			[SecurityCritical]
			internal static void RemoveAllEventHandlers(Action<EventRegistrationToken> removeMethod)
			{
				object target = removeMethod.Target;
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> eventRegistrationTokenTable = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.GetEventRegistrationTokenTable(target, removeMethod);
				List<EventRegistrationToken> list = new List<EventRegistrationToken>();
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> dictionary = eventRegistrationTokenTable;
				lock (dictionary)
				{
					foreach (WindowsRuntimeMarshal.EventRegistrationTokenList eventRegistrationTokenList in eventRegistrationTokenTable.Values)
					{
						eventRegistrationTokenList.CopyTo(list);
					}
					eventRegistrationTokenTable.Clear();
				}
				WindowsRuntimeMarshal.CallRemoveMethods(removeMethod, list);
			}

			// Token: 0x0600714B RID: 29003 RVA: 0x001859AC File Offset: 0x00183BAC
			// Note: this type is marked as 'beforefieldinit'.
			static ManagedEventRegistrationImpl()
			{
			}

			// Token: 0x0400388F RID: 14479
			internal static volatile ConditionalWeakTable<object, Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>> s_eventRegistrations = new ConditionalWeakTable<object, Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>>();
		}

		// Token: 0x02000CA9 RID: 3241
		internal static class NativeOrStaticEventRegistrationImpl
		{
			// Token: 0x0600714C RID: 29004 RVA: 0x001859BC File Offset: 0x00183BBC
			[SecuritySafeCritical]
			private static object GetInstanceKey(Action<EventRegistrationToken> removeMethod)
			{
				object target = removeMethod.Target;
				if (target == null)
				{
					return removeMethod.Method.DeclaringType;
				}
				return Marshal.GetRawIUnknownForComObjectNoAddRef(target);
			}

			// Token: 0x0600714D RID: 29005 RVA: 0x001859EC File Offset: 0x00183BEC
			[SecurityCritical]
			internal static void AddEventHandler<T>(Func<T, EventRegistrationToken> addMethod, Action<EventRegistrationToken> removeMethod, T handler)
			{
				object instanceKey = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetInstanceKey(removeMethod);
				EventRegistrationToken eventRegistrationToken = addMethod(handler);
				bool flag = false;
				try
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.AcquireReaderLock(-1);
					try
					{
						WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount;
						ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> orCreateEventRegistrationTokenTable = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetOrCreateEventRegistrationTokenTable(instanceKey, removeMethod, out tokenListCount);
						ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> conditionalWeakTable = orCreateEventRegistrationTokenTable;
						lock (conditionalWeakTable)
						{
							WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount eventRegistrationTokenListWithCount;
							if (orCreateEventRegistrationTokenTable.FindEquivalentKeyUnsafe(handler, out eventRegistrationTokenListWithCount) == null)
							{
								eventRegistrationTokenListWithCount = new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount(tokenListCount, eventRegistrationToken);
								orCreateEventRegistrationTokenTable.Add(handler, eventRegistrationTokenListWithCount);
							}
							else
							{
								eventRegistrationTokenListWithCount.Push(eventRegistrationToken);
							}
							flag = true;
						}
					}
					finally
					{
						WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.ReleaseReaderLock();
					}
				}
				catch (Exception)
				{
					if (!flag)
					{
						removeMethod(eventRegistrationToken);
					}
					throw;
				}
			}

			// Token: 0x0600714E RID: 29006 RVA: 0x00185AC0 File Offset: 0x00183CC0
			private static ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> GetEventRegistrationTokenTableNoCreate(object instance, Action<EventRegistrationToken> removeMethod, out WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount)
			{
				return WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetEventRegistrationTokenTableInternal(instance, removeMethod, out tokenListCount, false);
			}

			// Token: 0x0600714F RID: 29007 RVA: 0x00185ACB File Offset: 0x00183CCB
			private static ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> GetOrCreateEventRegistrationTokenTable(object instance, Action<EventRegistrationToken> removeMethod, out WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount)
			{
				return WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetEventRegistrationTokenTableInternal(instance, removeMethod, out tokenListCount, true);
			}

			// Token: 0x06007150 RID: 29008 RVA: 0x00185AD8 File Offset: 0x00183CD8
			private static ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> GetEventRegistrationTokenTableInternal(object instance, Action<EventRegistrationToken> removeMethod, out WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount, bool createIfNotFound)
			{
				WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey eventCacheKey;
				eventCacheKey.target = instance;
				eventCacheKey.method = removeMethod.Method;
				Dictionary<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry> dictionary = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations;
				ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> registrationTable;
				lock (dictionary)
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry eventCacheEntry;
					if (!WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations.TryGetValue(eventCacheKey, out eventCacheEntry))
					{
						if (!createIfNotFound)
						{
							tokenListCount = null;
							return null;
						}
						eventCacheEntry = default(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry);
						eventCacheEntry.registrationTable = new ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount>();
						eventCacheEntry.tokenListCount = new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount(eventCacheKey);
						WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations.Add(eventCacheKey, eventCacheEntry);
					}
					tokenListCount = eventCacheEntry.tokenListCount;
					registrationTable = eventCacheEntry.registrationTable;
				}
				return registrationTable;
			}

			// Token: 0x06007151 RID: 29009 RVA: 0x00185B88 File Offset: 0x00183D88
			[SecurityCritical]
			internal static void RemoveEventHandler<T>(Action<EventRegistrationToken> removeMethod, T handler)
			{
				object instanceKey = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetInstanceKey(removeMethod);
				WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.AcquireReaderLock(-1);
				EventRegistrationToken eventRegistrationToken;
				try
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount;
					ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> eventRegistrationTokenTableNoCreate = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetEventRegistrationTokenTableNoCreate(instanceKey, removeMethod, out tokenListCount);
					if (eventRegistrationTokenTableNoCreate == null)
					{
						return;
					}
					ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> conditionalWeakTable = eventRegistrationTokenTableNoCreate;
					lock (conditionalWeakTable)
					{
						WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount eventRegistrationTokenListWithCount;
						object obj = eventRegistrationTokenTableNoCreate.FindEquivalentKeyUnsafe(handler, out eventRegistrationTokenListWithCount);
						if (eventRegistrationTokenListWithCount == null)
						{
							return;
						}
						if (!eventRegistrationTokenListWithCount.Pop(out eventRegistrationToken))
						{
							eventRegistrationTokenTableNoCreate.Remove(obj);
						}
					}
				}
				finally
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.ReleaseReaderLock();
				}
				removeMethod(eventRegistrationToken);
			}

			// Token: 0x06007152 RID: 29010 RVA: 0x00185C34 File Offset: 0x00183E34
			[SecurityCritical]
			internal static void RemoveAllEventHandlers(Action<EventRegistrationToken> removeMethod)
			{
				object instanceKey = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetInstanceKey(removeMethod);
				List<EventRegistrationToken> list = new List<EventRegistrationToken>();
				WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.AcquireReaderLock(-1);
				try
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount;
					ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> eventRegistrationTokenTableNoCreate = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetEventRegistrationTokenTableNoCreate(instanceKey, removeMethod, out tokenListCount);
					if (eventRegistrationTokenTableNoCreate == null)
					{
						return;
					}
					ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> conditionalWeakTable = eventRegistrationTokenTableNoCreate;
					lock (conditionalWeakTable)
					{
						foreach (WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount eventRegistrationTokenListWithCount in eventRegistrationTokenTableNoCreate.Values)
						{
							eventRegistrationTokenListWithCount.CopyTo(list);
						}
						eventRegistrationTokenTableNoCreate.Clear();
					}
				}
				finally
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.ReleaseReaderLock();
				}
				WindowsRuntimeMarshal.CallRemoveMethods(removeMethod, list);
			}

			// Token: 0x06007153 RID: 29011 RVA: 0x00185D00 File Offset: 0x00183F00
			// Note: this type is marked as 'beforefieldinit'.
			static NativeOrStaticEventRegistrationImpl()
			{
			}

			// Token: 0x04003890 RID: 14480
			internal static volatile Dictionary<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry> s_eventRegistrations = new Dictionary<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry>(new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKeyEqualityComparer());

			// Token: 0x04003891 RID: 14481
			private static volatile WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.MyReaderWriterLock s_eventCacheRWLock = new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.MyReaderWriterLock();

			// Token: 0x02000D17 RID: 3351
			internal struct EventCacheKey
			{
				// Token: 0x06007235 RID: 29237 RVA: 0x001895C4 File Offset: 0x001877C4
				public override string ToString()
				{
					string[] array = new string[5];
					array[0] = "(";
					int num = 1;
					object obj = this.target;
					array[num] = ((obj != null) ? obj.ToString() : null);
					array[2] = ", ";
					int num2 = 3;
					MethodInfo methodInfo = this.method;
					array[num2] = ((methodInfo != null) ? methodInfo.ToString() : null);
					array[4] = ")";
					return string.Concat(array);
				}

				// Token: 0x04003977 RID: 14711
				internal object target;

				// Token: 0x04003978 RID: 14712
				internal MethodInfo method;
			}

			// Token: 0x02000D18 RID: 3352
			internal class EventCacheKeyEqualityComparer : IEqualityComparer<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey>
			{
				// Token: 0x06007236 RID: 29238 RVA: 0x0018961E File Offset: 0x0018781E
				public bool Equals(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey lhs, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey rhs)
				{
					return object.Equals(lhs.target, rhs.target) && object.Equals(lhs.method, rhs.method);
				}

				// Token: 0x06007237 RID: 29239 RVA: 0x00189646 File Offset: 0x00187846
				public int GetHashCode(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey key)
				{
					return key.target.GetHashCode() ^ key.method.GetHashCode();
				}

				// Token: 0x06007238 RID: 29240 RVA: 0x0018965F File Offset: 0x0018785F
				public EventCacheKeyEqualityComparer()
				{
				}
			}

			// Token: 0x02000D19 RID: 3353
			internal class EventRegistrationTokenListWithCount
			{
				// Token: 0x06007239 RID: 29241 RVA: 0x00189667 File Offset: 0x00187867
				internal EventRegistrationTokenListWithCount(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount, EventRegistrationToken token)
				{
					this._tokenListCount = tokenListCount;
					this._tokenListCount.Inc();
					this._tokenList = new WindowsRuntimeMarshal.EventRegistrationTokenList(token);
				}

				// Token: 0x0600723A RID: 29242 RVA: 0x00189690 File Offset: 0x00187890
				~EventRegistrationTokenListWithCount()
				{
					this._tokenListCount.Dec();
				}

				// Token: 0x0600723B RID: 29243 RVA: 0x001896C4 File Offset: 0x001878C4
				public void Push(EventRegistrationToken token)
				{
					this._tokenList.Push(token);
				}

				// Token: 0x0600723C RID: 29244 RVA: 0x001896D3 File Offset: 0x001878D3
				public bool Pop(out EventRegistrationToken token)
				{
					return this._tokenList.Pop(out token);
				}

				// Token: 0x0600723D RID: 29245 RVA: 0x001896E1 File Offset: 0x001878E1
				public void CopyTo(List<EventRegistrationToken> tokens)
				{
					this._tokenList.CopyTo(tokens);
				}

				// Token: 0x04003979 RID: 14713
				private WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount _tokenListCount;

				// Token: 0x0400397A RID: 14714
				private WindowsRuntimeMarshal.EventRegistrationTokenList _tokenList;
			}

			// Token: 0x02000D1A RID: 3354
			internal class TokenListCount
			{
				// Token: 0x0600723E RID: 29246 RVA: 0x001896EF File Offset: 0x001878EF
				internal TokenListCount(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey key)
				{
					this._key = key;
				}

				// Token: 0x17001393 RID: 5011
				// (get) Token: 0x0600723F RID: 29247 RVA: 0x001896FE File Offset: 0x001878FE
				internal WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey Key
				{
					get
					{
						return this._key;
					}
				}

				// Token: 0x06007240 RID: 29248 RVA: 0x00189708 File Offset: 0x00187908
				internal void Inc()
				{
					int num = Interlocked.Increment(ref this._count);
				}

				// Token: 0x06007241 RID: 29249 RVA: 0x00189724 File Offset: 0x00187924
				internal void Dec()
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.AcquireWriterLock(-1);
					try
					{
						if (Interlocked.Decrement(ref this._count) == 0)
						{
							this.CleanupCache();
						}
					}
					finally
					{
						WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.ReleaseWriterLock();
					}
				}

				// Token: 0x06007242 RID: 29250 RVA: 0x00189774 File Offset: 0x00187974
				private void CleanupCache()
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations.Remove(this._key);
				}

				// Token: 0x0400397B RID: 14715
				private int _count;

				// Token: 0x0400397C RID: 14716
				private WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey _key;
			}

			// Token: 0x02000D1B RID: 3355
			internal struct EventCacheEntry
			{
				// Token: 0x0400397D RID: 14717
				internal ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> registrationTable;

				// Token: 0x0400397E RID: 14718
				internal WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount;
			}

			// Token: 0x02000D1C RID: 3356
			internal class ReaderWriterLockTimedOutException : ApplicationException
			{
				// Token: 0x06007243 RID: 29251 RVA: 0x00189789 File Offset: 0x00187989
				public ReaderWriterLockTimedOutException()
				{
				}
			}

			// Token: 0x02000D1D RID: 3357
			internal class MyReaderWriterLock
			{
				// Token: 0x06007244 RID: 29252 RVA: 0x00189791 File Offset: 0x00187991
				internal MyReaderWriterLock()
				{
				}

				// Token: 0x06007245 RID: 29253 RVA: 0x0018979C File Offset: 0x0018799C
				internal void AcquireReaderLock(int millisecondsTimeout)
				{
					this.EnterMyLock();
					while (this.owners < 0 || this.numWriteWaiters != 0U)
					{
						if (this.readEvent == null)
						{
							this.LazyCreateEvent(ref this.readEvent, false);
						}
						else
						{
							this.WaitOnEvent(this.readEvent, ref this.numReadWaiters, millisecondsTimeout);
						}
					}
					this.owners++;
					this.ExitMyLock();
				}

				// Token: 0x06007246 RID: 29254 RVA: 0x00189804 File Offset: 0x00187A04
				internal void AcquireWriterLock(int millisecondsTimeout)
				{
					this.EnterMyLock();
					while (this.owners != 0)
					{
						if (this.writeEvent == null)
						{
							this.LazyCreateEvent(ref this.writeEvent, true);
						}
						else
						{
							this.WaitOnEvent(this.writeEvent, ref this.numWriteWaiters, millisecondsTimeout);
						}
					}
					this.owners = -1;
					this.ExitMyLock();
				}

				// Token: 0x06007247 RID: 29255 RVA: 0x0018985A File Offset: 0x00187A5A
				internal void ReleaseReaderLock()
				{
					this.EnterMyLock();
					this.owners--;
					this.ExitAndWakeUpAppropriateWaiters();
				}

				// Token: 0x06007248 RID: 29256 RVA: 0x00189876 File Offset: 0x00187A76
				internal void ReleaseWriterLock()
				{
					this.EnterMyLock();
					this.owners++;
					this.ExitAndWakeUpAppropriateWaiters();
				}

				// Token: 0x06007249 RID: 29257 RVA: 0x00189894 File Offset: 0x00187A94
				private void LazyCreateEvent(ref EventWaitHandle waitEvent, bool makeAutoResetEvent)
				{
					this.ExitMyLock();
					EventWaitHandle eventWaitHandle;
					if (makeAutoResetEvent)
					{
						eventWaitHandle = new AutoResetEvent(false);
					}
					else
					{
						eventWaitHandle = new ManualResetEvent(false);
					}
					this.EnterMyLock();
					if (waitEvent == null)
					{
						waitEvent = eventWaitHandle;
					}
				}

				// Token: 0x0600724A RID: 29258 RVA: 0x001898C8 File Offset: 0x00187AC8
				private void WaitOnEvent(EventWaitHandle waitEvent, ref uint numWaiters, int millisecondsTimeout)
				{
					waitEvent.Reset();
					numWaiters += 1U;
					bool flag = false;
					this.ExitMyLock();
					try
					{
						if (!waitEvent.WaitOne(millisecondsTimeout, false))
						{
							throw new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.ReaderWriterLockTimedOutException();
						}
						flag = true;
					}
					finally
					{
						this.EnterMyLock();
						numWaiters -= 1U;
						if (!flag)
						{
							this.ExitMyLock();
						}
					}
				}

				// Token: 0x0600724B RID: 29259 RVA: 0x00189924 File Offset: 0x00187B24
				private void ExitAndWakeUpAppropriateWaiters()
				{
					if (this.owners == 0 && this.numWriteWaiters > 0U)
					{
						this.ExitMyLock();
						this.writeEvent.Set();
						return;
					}
					if (this.owners >= 0 && this.numReadWaiters != 0U)
					{
						this.ExitMyLock();
						this.readEvent.Set();
						return;
					}
					this.ExitMyLock();
				}

				// Token: 0x0600724C RID: 29260 RVA: 0x0018997F File Offset: 0x00187B7F
				private void EnterMyLock()
				{
					if (Interlocked.CompareExchange(ref this.myLock, 1, 0) != 0)
					{
						this.EnterMyLockSpin();
					}
				}

				// Token: 0x0600724D RID: 29261 RVA: 0x00189998 File Offset: 0x00187B98
				private void EnterMyLockSpin()
				{
					int num = 0;
					for (;;)
					{
						if (num < 3 && Environment.ProcessorCount > 1)
						{
							Thread.SpinWait(20);
						}
						else
						{
							Thread.Sleep(0);
						}
						if (Interlocked.CompareExchange(ref this.myLock, 1, 0) == 0)
						{
							break;
						}
						num++;
					}
				}

				// Token: 0x0600724E RID: 29262 RVA: 0x001899D7 File Offset: 0x00187BD7
				private void ExitMyLock()
				{
					this.myLock = 0;
				}

				// Token: 0x0400397F RID: 14719
				private int myLock;

				// Token: 0x04003980 RID: 14720
				private int owners;

				// Token: 0x04003981 RID: 14721
				private uint numWriteWaiters;

				// Token: 0x04003982 RID: 14722
				private uint numReadWaiters;

				// Token: 0x04003983 RID: 14723
				private EventWaitHandle writeEvent;

				// Token: 0x04003984 RID: 14724
				private EventWaitHandle readEvent;
			}
		}
	}
}
