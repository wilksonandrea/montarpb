using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System
{
	// Token: 0x020000F8 RID: 248
	[ComVisible(false)]
	[DebuggerTypeProxy(typeof(System_LazyDebugView<>))]
	[DebuggerDisplay("ThreadSafetyMode={Mode}, IsValueCreated={IsValueCreated}, IsValueFaulted={IsValueFaulted}, Value={ValueForDebugDisplay}")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	[Serializable]
	public class Lazy<T>
	{
		// Token: 0x06000F13 RID: 3859 RVA: 0x0002EF15 File Offset: 0x0002D115
		[__DynamicallyInvokable]
		public Lazy()
			: this(LazyThreadSafetyMode.ExecutionAndPublication)
		{
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0002EF1E File Offset: 0x0002D11E
		[__DynamicallyInvokable]
		public Lazy(Func<T> valueFactory)
			: this(valueFactory, LazyThreadSafetyMode.ExecutionAndPublication)
		{
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0002EF28 File Offset: 0x0002D128
		[__DynamicallyInvokable]
		public Lazy(bool isThreadSafe)
			: this(isThreadSafe ? LazyThreadSafetyMode.ExecutionAndPublication : LazyThreadSafetyMode.None)
		{
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0002EF37 File Offset: 0x0002D137
		[__DynamicallyInvokable]
		public Lazy(LazyThreadSafetyMode mode)
		{
			this.m_threadSafeObj = Lazy<T>.GetObjectFromMode(mode);
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0002EF4B File Offset: 0x0002D14B
		[__DynamicallyInvokable]
		public Lazy(Func<T> valueFactory, bool isThreadSafe)
			: this(valueFactory, isThreadSafe ? LazyThreadSafetyMode.ExecutionAndPublication : LazyThreadSafetyMode.None)
		{
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x0002EF5B File Offset: 0x0002D15B
		[__DynamicallyInvokable]
		public Lazy(Func<T> valueFactory, LazyThreadSafetyMode mode)
		{
			if (valueFactory == null)
			{
				throw new ArgumentNullException("valueFactory");
			}
			this.m_threadSafeObj = Lazy<T>.GetObjectFromMode(mode);
			this.m_valueFactory = valueFactory;
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0002EF84 File Offset: 0x0002D184
		private static object GetObjectFromMode(LazyThreadSafetyMode mode)
		{
			if (mode == LazyThreadSafetyMode.ExecutionAndPublication)
			{
				return new object();
			}
			if (mode == LazyThreadSafetyMode.PublicationOnly)
			{
				return LazyHelpers.PUBLICATION_ONLY_SENTINEL;
			}
			if (mode != LazyThreadSafetyMode.None)
			{
				throw new ArgumentOutOfRangeException("mode", Environment.GetResourceString("Lazy_ctor_ModeInvalid"));
			}
			return null;
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0002EFB4 File Offset: 0x0002D1B4
		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			T value = this.Value;
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0002EFC8 File Offset: 0x0002D1C8
		[__DynamicallyInvokable]
		public override string ToString()
		{
			if (!this.IsValueCreated)
			{
				return Environment.GetResourceString("Lazy_ToString_ValueNotCreated");
			}
			T value = this.Value;
			return value.ToString();
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x0002EFFC File Offset: 0x0002D1FC
		internal T ValueForDebugDisplay
		{
			get
			{
				if (!this.IsValueCreated)
				{
					return default(T);
				}
				return ((Lazy<T>.Boxed)this.m_boxed).m_value;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x0002F02B File Offset: 0x0002D22B
		internal LazyThreadSafetyMode Mode
		{
			get
			{
				if (this.m_threadSafeObj == null)
				{
					return LazyThreadSafetyMode.None;
				}
				if (this.m_threadSafeObj == LazyHelpers.PUBLICATION_ONLY_SENTINEL)
				{
					return LazyThreadSafetyMode.PublicationOnly;
				}
				return LazyThreadSafetyMode.ExecutionAndPublication;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x0002F047 File Offset: 0x0002D247
		internal bool IsValueFaulted
		{
			get
			{
				return this.m_boxed is Lazy<T>.LazyInternalExceptionHolder;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x0002F057 File Offset: 0x0002D257
		[__DynamicallyInvokable]
		public bool IsValueCreated
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_boxed != null && this.m_boxed is Lazy<T>.Boxed;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x0002F074 File Offset: 0x0002D274
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[__DynamicallyInvokable]
		public T Value
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_boxed != null)
				{
					Lazy<T>.Boxed boxed = this.m_boxed as Lazy<T>.Boxed;
					if (boxed != null)
					{
						return boxed.m_value;
					}
					Lazy<T>.LazyInternalExceptionHolder lazyInternalExceptionHolder = this.m_boxed as Lazy<T>.LazyInternalExceptionHolder;
					lazyInternalExceptionHolder.m_edi.Throw();
				}
				Debugger.NotifyOfCrossThreadDependency();
				return this.LazyInitValue();
			}
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x0002F0C4 File Offset: 0x0002D2C4
		private T LazyInitValue()
		{
			Lazy<T>.Boxed boxed = null;
			LazyThreadSafetyMode mode = this.Mode;
			if (mode == LazyThreadSafetyMode.None)
			{
				boxed = this.CreateValue();
				this.m_boxed = boxed;
			}
			else if (mode == LazyThreadSafetyMode.PublicationOnly)
			{
				boxed = this.CreateValue();
				if (boxed == null || Interlocked.CompareExchange(ref this.m_boxed, boxed, null) != null)
				{
					boxed = (Lazy<T>.Boxed)this.m_boxed;
				}
				else
				{
					this.m_valueFactory = Lazy<T>.ALREADY_INVOKED_SENTINEL;
				}
			}
			else
			{
				object obj = Volatile.Read<object>(ref this.m_threadSafeObj);
				bool flag = false;
				try
				{
					if (obj != Lazy<T>.ALREADY_INVOKED_SENTINEL)
					{
						Monitor.Enter(obj, ref flag);
					}
					if (this.m_boxed == null)
					{
						boxed = this.CreateValue();
						this.m_boxed = boxed;
						Volatile.Write<object>(ref this.m_threadSafeObj, Lazy<T>.ALREADY_INVOKED_SENTINEL);
					}
					else
					{
						boxed = this.m_boxed as Lazy<T>.Boxed;
						if (boxed == null)
						{
							Lazy<T>.LazyInternalExceptionHolder lazyInternalExceptionHolder = this.m_boxed as Lazy<T>.LazyInternalExceptionHolder;
							lazyInternalExceptionHolder.m_edi.Throw();
						}
					}
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(obj);
					}
				}
			}
			return boxed.m_value;
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x0002F1BC File Offset: 0x0002D3BC
		private Lazy<T>.Boxed CreateValue()
		{
			Lazy<T>.Boxed boxed = null;
			LazyThreadSafetyMode mode = this.Mode;
			if (this.m_valueFactory != null)
			{
				try
				{
					if (mode != LazyThreadSafetyMode.PublicationOnly && this.m_valueFactory == Lazy<T>.ALREADY_INVOKED_SENTINEL)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Lazy_Value_RecursiveCallsToValue"));
					}
					Func<T> valueFactory = this.m_valueFactory;
					if (mode != LazyThreadSafetyMode.PublicationOnly)
					{
						this.m_valueFactory = Lazy<T>.ALREADY_INVOKED_SENTINEL;
					}
					else if (valueFactory == Lazy<T>.ALREADY_INVOKED_SENTINEL)
					{
						return null;
					}
					return new Lazy<T>.Boxed(valueFactory());
				}
				catch (Exception ex)
				{
					if (mode != LazyThreadSafetyMode.PublicationOnly)
					{
						this.m_boxed = new Lazy<T>.LazyInternalExceptionHolder(ex);
					}
					throw;
				}
			}
			try
			{
				boxed = new Lazy<T>.Boxed((T)((object)Activator.CreateInstance(typeof(T))));
			}
			catch (MissingMethodException)
			{
				Exception ex2 = new MissingMemberException(Environment.GetResourceString("Lazy_CreateValue_NoParameterlessCtorForT"));
				if (mode != LazyThreadSafetyMode.PublicationOnly)
				{
					this.m_boxed = new Lazy<T>.LazyInternalExceptionHolder(ex2);
				}
				throw ex2;
			}
			return boxed;
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x0002F2B0 File Offset: 0x0002D4B0
		// Note: this type is marked as 'beforefieldinit'.
		static Lazy()
		{
		}

		// Token: 0x0400059A RID: 1434
		private static readonly Func<T> ALREADY_INVOKED_SENTINEL = () => default(T);

		// Token: 0x0400059B RID: 1435
		private object m_boxed;

		// Token: 0x0400059C RID: 1436
		[NonSerialized]
		private Func<T> m_valueFactory;

		// Token: 0x0400059D RID: 1437
		[NonSerialized]
		private object m_threadSafeObj;

		// Token: 0x02000AF0 RID: 2800
		[Serializable]
		private class Boxed
		{
			// Token: 0x06006A12 RID: 27154 RVA: 0x0016D1A7 File Offset: 0x0016B3A7
			internal Boxed(T value)
			{
				this.m_value = value;
			}

			// Token: 0x040031B0 RID: 12720
			internal T m_value;
		}

		// Token: 0x02000AF1 RID: 2801
		private class LazyInternalExceptionHolder
		{
			// Token: 0x06006A13 RID: 27155 RVA: 0x0016D1B6 File Offset: 0x0016B3B6
			internal LazyInternalExceptionHolder(Exception ex)
			{
				this.m_edi = ExceptionDispatchInfo.Capture(ex);
			}

			// Token: 0x040031B1 RID: 12721
			internal ExceptionDispatchInfo m_edi;
		}

		// Token: 0x02000AF2 RID: 2802
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06006A14 RID: 27156 RVA: 0x0016D1CA File Offset: 0x0016B3CA
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06006A15 RID: 27157 RVA: 0x0016D1D6 File Offset: 0x0016B3D6
			public <>c()
			{
			}

			// Token: 0x06006A16 RID: 27158 RVA: 0x0016D1E0 File Offset: 0x0016B3E0
			internal T <.cctor>b__27_0()
			{
				return default(T);
			}

			// Token: 0x040031B2 RID: 12722
			public static readonly Lazy<T>.<>c <>9 = new Lazy<T>.<>c();
		}
	}
}
