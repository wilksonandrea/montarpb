using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x020001A6 RID: 422
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class TextReader : MarshalByRefObject, IDisposable
	{
		// Token: 0x06001A2C RID: 6700 RVA: 0x000578CE File Offset: 0x00055ACE
		[__DynamicallyInvokable]
		protected TextReader()
		{
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x000578D6 File Offset: 0x00055AD6
		public virtual void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x000578E5 File Offset: 0x00055AE5
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x000578F4 File Offset: 0x00055AF4
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x000578F6 File Offset: 0x00055AF6
		[__DynamicallyInvokable]
		public virtual int Peek()
		{
			return -1;
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x000578F9 File Offset: 0x00055AF9
		[__DynamicallyInvokable]
		public virtual int Read()
		{
			return -1;
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x000578FC File Offset: 0x00055AFC
		[__DynamicallyInvokable]
		public virtual int Read([In] [Out] char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			int num = 0;
			do
			{
				int num2 = this.Read();
				if (num2 == -1)
				{
					break;
				}
				buffer[index + num++] = (char)num2;
			}
			while (num < count);
			return num;
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x00057988 File Offset: 0x00055B88
		[__DynamicallyInvokable]
		public virtual string ReadToEnd()
		{
			char[] array = new char[4096];
			StringBuilder stringBuilder = new StringBuilder(4096);
			int num;
			while ((num = this.Read(array, 0, array.Length)) != 0)
			{
				stringBuilder.Append(array, 0, num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x000579CC File Offset: 0x00055BCC
		[__DynamicallyInvokable]
		public virtual int ReadBlock([In] [Out] char[] buffer, int index, int count)
		{
			int num = 0;
			int num2;
			do
			{
				num += (num2 = this.Read(buffer, index + num, count - num));
			}
			while (num2 > 0 && num < count);
			return num;
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x000579F8 File Offset: 0x00055BF8
		[__DynamicallyInvokable]
		public virtual string ReadLine()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num;
			for (;;)
			{
				num = this.Read();
				if (num == -1)
				{
					goto IL_43;
				}
				if (num == 13 || num == 10)
				{
					break;
				}
				stringBuilder.Append((char)num);
			}
			if (num == 13 && this.Peek() == 10)
			{
				this.Read();
			}
			return stringBuilder.ToString();
			IL_43:
			if (stringBuilder.Length > 0)
			{
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x00057A59 File Offset: 0x00055C59
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task<string> ReadLineAsync()
		{
			return Task<string>.Factory.StartNew(TextReader._ReadLineDelegate, this, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x00057A78 File Offset: 0x00055C78
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual async Task<string> ReadToEndAsync()
		{
			char[] chars = new char[4096];
			StringBuilder sb = new StringBuilder(4096);
			for (;;)
			{
				int num = await this.ReadAsyncInternal(chars, 0, chars.Length).ConfigureAwait(false);
				int num2;
				if ((num2 = num) == 0)
				{
					break;
				}
				sb.Append(chars, 0, num2);
			}
			return sb.ToString();
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x00057ABC File Offset: 0x00055CBC
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task<int> ReadAsync(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			return this.ReadAsyncInternal(buffer, index, count);
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x00057B2C File Offset: 0x00055D2C
		internal virtual Task<int> ReadAsyncInternal(char[] buffer, int index, int count)
		{
			Tuple<TextReader, char[], int, int> tuple = new Tuple<TextReader, char[], int, int>(this, buffer, index, count);
			return Task<int>.Factory.StartNew(TextReader._ReadDelegate, tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x00057B60 File Offset: 0x00055D60
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task<int> ReadBlockAsync(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			return this.ReadBlockAsyncInternal(buffer, index, count);
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x00057BD0 File Offset: 0x00055DD0
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		private async Task<int> ReadBlockAsyncInternal(char[] buffer, int index, int count)
		{
			int i = 0;
			int num2;
			do
			{
				int num = await this.ReadAsyncInternal(buffer, index + i, count - i).ConfigureAwait(false);
				num2 = num;
				i += num2;
			}
			while (num2 > 0 && i < count);
			return i;
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x00057C2B File Offset: 0x00055E2B
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static TextReader Synchronized(TextReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (reader is TextReader.SyncTextReader)
			{
				return reader;
			}
			return new TextReader.SyncTextReader(reader);
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x00057C4B File Offset: 0x00055E4B
		// Note: this type is marked as 'beforefieldinit'.
		static TextReader()
		{
		}

		// Token: 0x04000923 RID: 2339
		[NonSerialized]
		private static Func<object, string> _ReadLineDelegate = (object state) => ((TextReader)state).ReadLine();

		// Token: 0x04000924 RID: 2340
		[NonSerialized]
		private static Func<object, int> _ReadDelegate = delegate(object state)
		{
			Tuple<TextReader, char[], int, int> tuple = (Tuple<TextReader, char[], int, int>)state;
			return tuple.Item1.Read(tuple.Item2, tuple.Item3, tuple.Item4);
		};

		// Token: 0x04000925 RID: 2341
		[__DynamicallyInvokable]
		public static readonly TextReader Null = new TextReader.NullTextReader();

		// Token: 0x02000B25 RID: 2853
		[Serializable]
		private sealed class NullTextReader : TextReader
		{
			// Token: 0x06006B09 RID: 27401 RVA: 0x00172C76 File Offset: 0x00170E76
			public NullTextReader()
			{
			}

			// Token: 0x06006B0A RID: 27402 RVA: 0x00172C7E File Offset: 0x00170E7E
			public override int Read(char[] buffer, int index, int count)
			{
				return 0;
			}

			// Token: 0x06006B0B RID: 27403 RVA: 0x00172C81 File Offset: 0x00170E81
			public override string ReadLine()
			{
				return null;
			}
		}

		// Token: 0x02000B26 RID: 2854
		[Serializable]
		internal sealed class SyncTextReader : TextReader
		{
			// Token: 0x06006B0C RID: 27404 RVA: 0x00172C84 File Offset: 0x00170E84
			internal SyncTextReader(TextReader t)
			{
				this._in = t;
			}

			// Token: 0x06006B0D RID: 27405 RVA: 0x00172C93 File Offset: 0x00170E93
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Close()
			{
				this._in.Close();
			}

			// Token: 0x06006B0E RID: 27406 RVA: 0x00172CA0 File Offset: 0x00170EA0
			[MethodImpl(MethodImplOptions.Synchronized)]
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					((IDisposable)this._in).Dispose();
				}
			}

			// Token: 0x06006B0F RID: 27407 RVA: 0x00172CB0 File Offset: 0x00170EB0
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override int Peek()
			{
				return this._in.Peek();
			}

			// Token: 0x06006B10 RID: 27408 RVA: 0x00172CBD File Offset: 0x00170EBD
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override int Read()
			{
				return this._in.Read();
			}

			// Token: 0x06006B11 RID: 27409 RVA: 0x00172CCA File Offset: 0x00170ECA
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override int Read([In] [Out] char[] buffer, int index, int count)
			{
				return this._in.Read(buffer, index, count);
			}

			// Token: 0x06006B12 RID: 27410 RVA: 0x00172CDA File Offset: 0x00170EDA
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override int ReadBlock([In] [Out] char[] buffer, int index, int count)
			{
				return this._in.ReadBlock(buffer, index, count);
			}

			// Token: 0x06006B13 RID: 27411 RVA: 0x00172CEA File Offset: 0x00170EEA
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override string ReadLine()
			{
				return this._in.ReadLine();
			}

			// Token: 0x06006B14 RID: 27412 RVA: 0x00172CF7 File Offset: 0x00170EF7
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override string ReadToEnd()
			{
				return this._in.ReadToEnd();
			}

			// Token: 0x06006B15 RID: 27413 RVA: 0x00172D04 File Offset: 0x00170F04
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task<string> ReadLineAsync()
			{
				return Task.FromResult<string>(this.ReadLine());
			}

			// Token: 0x06006B16 RID: 27414 RVA: 0x00172D11 File Offset: 0x00170F11
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task<string> ReadToEndAsync()
			{
				return Task.FromResult<string>(this.ReadToEnd());
			}

			// Token: 0x06006B17 RID: 27415 RVA: 0x00172D20 File Offset: 0x00170F20
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task<int> ReadBlockAsync(char[] buffer, int index, int count)
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
				}
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (buffer.Length - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				return Task.FromResult<int>(this.ReadBlock(buffer, index, count));
			}

			// Token: 0x06006B18 RID: 27416 RVA: 0x00172D94 File Offset: 0x00170F94
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task<int> ReadAsync(char[] buffer, int index, int count)
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
				}
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (buffer.Length - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				return Task.FromResult<int>(this.Read(buffer, index, count));
			}

			// Token: 0x04003330 RID: 13104
			internal TextReader _in;
		}

		// Token: 0x02000B27 RID: 2855
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadToEndAsync>d__14 : IAsyncStateMachine
		{
			// Token: 0x06006B19 RID: 27417 RVA: 0x00172E08 File Offset: 0x00171008
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				TextReader textReader = this;
				string text;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter;
					if (num == 0)
					{
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						num2 = -1;
						goto IL_BA;
					}
					chars = new char[4096];
					sb = new StringBuilder(4096);
					IL_4A:
					configuredTaskAwaiter = textReader.ReadAsyncInternal(chars, 0, chars.Length).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						num2 = 0;
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, TextReader.<ReadToEndAsync>d__14>(ref configuredTaskAwaiter, ref this);
						return;
					}
					IL_BA:
					int result = configuredTaskAwaiter.GetResult();
					int num3;
					if ((num3 = result) != 0)
					{
						sb.Append(chars, 0, num3);
						goto IL_4A;
					}
					text = sb.ToString();
				}
				catch (Exception ex)
				{
					num2 = -2;
					chars = null;
					sb = null;
					this.<>t__builder.SetException(ex);
					return;
				}
				num2 = -2;
				chars = null;
				sb = null;
				this.<>t__builder.SetResult(text);
			}

			// Token: 0x06006B1A RID: 27418 RVA: 0x00172F48 File Offset: 0x00171148
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003331 RID: 13105
			public int <>1__state;

			// Token: 0x04003332 RID: 13106
			public AsyncTaskMethodBuilder<string> <>t__builder;

			// Token: 0x04003333 RID: 13107
			public TextReader <>4__this;

			// Token: 0x04003334 RID: 13108
			private char[] <chars>5__2;

			// Token: 0x04003335 RID: 13109
			private StringBuilder <sb>5__3;

			// Token: 0x04003336 RID: 13110
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000B28 RID: 2856
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadBlockAsyncInternal>d__18 : IAsyncStateMachine
		{
			// Token: 0x06006B1B RID: 27419 RVA: 0x00172F58 File Offset: 0x00171158
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				TextReader textReader = this;
				int num4;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter;
					if (num == 0)
					{
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						num2 = -1;
						goto IL_99;
					}
					i = 0;
					IL_18:
					configuredTaskAwaiter = textReader.ReadAsyncInternal(buffer, index + i, count - i).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						num2 = 0;
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, TextReader.<ReadBlockAsyncInternal>d__18>(ref configuredTaskAwaiter, ref this);
						return;
					}
					IL_99:
					int result = configuredTaskAwaiter.GetResult();
					int num3 = result;
					i += num3;
					if (num3 > 0 && i < count)
					{
						goto IL_18;
					}
					num4 = i;
				}
				catch (Exception ex)
				{
					num2 = -2;
					this.<>t__builder.SetException(ex);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(num4);
			}

			// Token: 0x06006B1C RID: 27420 RVA: 0x00173074 File Offset: 0x00171274
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003337 RID: 13111
			public int <>1__state;

			// Token: 0x04003338 RID: 13112
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04003339 RID: 13113
			public TextReader <>4__this;

			// Token: 0x0400333A RID: 13114
			public char[] buffer;

			// Token: 0x0400333B RID: 13115
			public int index;

			// Token: 0x0400333C RID: 13116
			public int count;

			// Token: 0x0400333D RID: 13117
			private int <n>5__2;

			// Token: 0x0400333E RID: 13118
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000B29 RID: 2857
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06006B1D RID: 27421 RVA: 0x00173082 File Offset: 0x00171282
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06006B1E RID: 27422 RVA: 0x0017308E File Offset: 0x0017128E
			public <>c()
			{
			}

			// Token: 0x06006B1F RID: 27423 RVA: 0x00173096 File Offset: 0x00171296
			internal string <.cctor>b__22_0(object state)
			{
				return ((TextReader)state).ReadLine();
			}

			// Token: 0x06006B20 RID: 27424 RVA: 0x001730A4 File Offset: 0x001712A4
			internal int <.cctor>b__22_1(object state)
			{
				Tuple<TextReader, char[], int, int> tuple = (Tuple<TextReader, char[], int, int>)state;
				return tuple.Item1.Read(tuple.Item2, tuple.Item3, tuple.Item4);
			}

			// Token: 0x0400333F RID: 13119
			public static readonly TextReader.<>c <>9 = new TextReader.<>c();
		}
	}
}
