using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x020001A7 RID: 423
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class TextWriter : MarshalByRefObject, IDisposable
	{
		// Token: 0x06001A3E RID: 6718 RVA: 0x00057C81 File Offset: 0x00055E81
		[__DynamicallyInvokable]
		protected TextWriter()
		{
			this.InternalFormatProvider = null;
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x00057CA6 File Offset: 0x00055EA6
		[__DynamicallyInvokable]
		protected TextWriter(IFormatProvider formatProvider)
		{
			this.InternalFormatProvider = formatProvider;
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06001A40 RID: 6720 RVA: 0x00057CCB File Offset: 0x00055ECB
		[__DynamicallyInvokable]
		public virtual IFormatProvider FormatProvider
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.InternalFormatProvider == null)
				{
					return Thread.CurrentThread.CurrentCulture;
				}
				return this.InternalFormatProvider;
			}
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x00057CE6 File Offset: 0x00055EE6
		public virtual void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x00057CF5 File Offset: 0x00055EF5
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x00057CF7 File Offset: 0x00055EF7
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x00057D06 File Offset: 0x00055F06
		[__DynamicallyInvokable]
		public virtual void Flush()
		{
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06001A45 RID: 6725
		[__DynamicallyInvokable]
		public abstract Encoding Encoding
		{
			[__DynamicallyInvokable]
			get;
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06001A46 RID: 6726 RVA: 0x00057D08 File Offset: 0x00055F08
		// (set) Token: 0x06001A47 RID: 6727 RVA: 0x00057D15 File Offset: 0x00055F15
		[__DynamicallyInvokable]
		public virtual string NewLine
		{
			[__DynamicallyInvokable]
			get
			{
				return new string(this.CoreNewLine);
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					value = "\r\n";
				}
				this.CoreNewLine = value.ToCharArray();
			}
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x00057D2D File Offset: 0x00055F2D
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static TextWriter Synchronized(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (writer is TextWriter.SyncTextWriter)
			{
				return writer;
			}
			return new TextWriter.SyncTextWriter(writer);
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x00057D4D File Offset: 0x00055F4D
		[__DynamicallyInvokable]
		public virtual void Write(char value)
		{
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x00057D4F File Offset: 0x00055F4F
		[__DynamicallyInvokable]
		public virtual void Write(char[] buffer)
		{
			if (buffer != null)
			{
				this.Write(buffer, 0, buffer.Length);
			}
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x00057D60 File Offset: 0x00055F60
		[__DynamicallyInvokable]
		public virtual void Write(char[] buffer, int index, int count)
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
			for (int i = 0; i < count; i++)
			{
				this.Write(buffer[index + i]);
			}
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x00057DE6 File Offset: 0x00055FE6
		[__DynamicallyInvokable]
		public virtual void Write(bool value)
		{
			this.Write(value ? "True" : "False");
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x00057DFD File Offset: 0x00055FFD
		[__DynamicallyInvokable]
		public virtual void Write(int value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x00057E12 File Offset: 0x00056012
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public virtual void Write(uint value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x00057E27 File Offset: 0x00056027
		[__DynamicallyInvokable]
		public virtual void Write(long value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x00057E3C File Offset: 0x0005603C
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public virtual void Write(ulong value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x00057E51 File Offset: 0x00056051
		[__DynamicallyInvokable]
		public virtual void Write(float value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x00057E66 File Offset: 0x00056066
		[__DynamicallyInvokable]
		public virtual void Write(double value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x00057E7B File Offset: 0x0005607B
		[__DynamicallyInvokable]
		public virtual void Write(decimal value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x00057E90 File Offset: 0x00056090
		[__DynamicallyInvokable]
		public virtual void Write(string value)
		{
			if (value != null)
			{
				this.Write(value.ToCharArray());
			}
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x00057EA4 File Offset: 0x000560A4
		[__DynamicallyInvokable]
		public virtual void Write(object value)
		{
			if (value != null)
			{
				IFormattable formattable = value as IFormattable;
				if (formattable != null)
				{
					this.Write(formattable.ToString(null, this.FormatProvider));
					return;
				}
				this.Write(value.ToString());
			}
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x00057EDE File Offset: 0x000560DE
		[__DynamicallyInvokable]
		public virtual void Write(string format, object arg0)
		{
			this.Write(string.Format(this.FormatProvider, format, arg0));
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x00057EF3 File Offset: 0x000560F3
		[__DynamicallyInvokable]
		public virtual void Write(string format, object arg0, object arg1)
		{
			this.Write(string.Format(this.FormatProvider, format, arg0, arg1));
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x00057F09 File Offset: 0x00056109
		[__DynamicallyInvokable]
		public virtual void Write(string format, object arg0, object arg1, object arg2)
		{
			this.Write(string.Format(this.FormatProvider, format, arg0, arg1, arg2));
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x00057F21 File Offset: 0x00056121
		[__DynamicallyInvokable]
		public virtual void Write(string format, params object[] arg)
		{
			this.Write(string.Format(this.FormatProvider, format, arg));
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x00057F36 File Offset: 0x00056136
		[__DynamicallyInvokable]
		public virtual void WriteLine()
		{
			this.Write(this.CoreNewLine);
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x00057F44 File Offset: 0x00056144
		[__DynamicallyInvokable]
		public virtual void WriteLine(char value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x00057F53 File Offset: 0x00056153
		[__DynamicallyInvokable]
		public virtual void WriteLine(char[] buffer)
		{
			this.Write(buffer);
			this.WriteLine();
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x00057F62 File Offset: 0x00056162
		[__DynamicallyInvokable]
		public virtual void WriteLine(char[] buffer, int index, int count)
		{
			this.Write(buffer, index, count);
			this.WriteLine();
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x00057F73 File Offset: 0x00056173
		[__DynamicallyInvokable]
		public virtual void WriteLine(bool value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x00057F82 File Offset: 0x00056182
		[__DynamicallyInvokable]
		public virtual void WriteLine(int value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x00057F91 File Offset: 0x00056191
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public virtual void WriteLine(uint value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x00057FA0 File Offset: 0x000561A0
		[__DynamicallyInvokable]
		public virtual void WriteLine(long value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x00057FAF File Offset: 0x000561AF
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public virtual void WriteLine(ulong value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x00057FBE File Offset: 0x000561BE
		[__DynamicallyInvokable]
		public virtual void WriteLine(float value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x00057FCD File Offset: 0x000561CD
		[__DynamicallyInvokable]
		public virtual void WriteLine(double value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x00057FDC File Offset: 0x000561DC
		[__DynamicallyInvokable]
		public virtual void WriteLine(decimal value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x00057FEC File Offset: 0x000561EC
		[__DynamicallyInvokable]
		public virtual void WriteLine(string value)
		{
			if (value == null)
			{
				this.WriteLine();
				return;
			}
			int length = value.Length;
			int num = this.CoreNewLine.Length;
			char[] array = new char[length + num];
			value.CopyTo(0, array, 0, length);
			if (num == 2)
			{
				array[length] = this.CoreNewLine[0];
				array[length + 1] = this.CoreNewLine[1];
			}
			else if (num == 1)
			{
				array[length] = this.CoreNewLine[0];
			}
			else
			{
				Buffer.InternalBlockCopy(this.CoreNewLine, 0, array, length * 2, num * 2);
			}
			this.Write(array, 0, length + num);
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x00058074 File Offset: 0x00056274
		[__DynamicallyInvokable]
		public virtual void WriteLine(object value)
		{
			if (value == null)
			{
				this.WriteLine();
				return;
			}
			IFormattable formattable = value as IFormattable;
			if (formattable != null)
			{
				this.WriteLine(formattable.ToString(null, this.FormatProvider));
				return;
			}
			this.WriteLine(value.ToString());
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x000580B5 File Offset: 0x000562B5
		[__DynamicallyInvokable]
		public virtual void WriteLine(string format, object arg0)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, arg0));
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x000580CA File Offset: 0x000562CA
		[__DynamicallyInvokable]
		public virtual void WriteLine(string format, object arg0, object arg1)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, arg0, arg1));
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x000580E0 File Offset: 0x000562E0
		[__DynamicallyInvokable]
		public virtual void WriteLine(string format, object arg0, object arg1, object arg2)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, arg0, arg1, arg2));
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x000580F8 File Offset: 0x000562F8
		[__DynamicallyInvokable]
		public virtual void WriteLine(string format, params object[] arg)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, arg));
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x00058110 File Offset: 0x00056310
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteAsync(char value)
		{
			Tuple<TextWriter, char> tuple = new Tuple<TextWriter, char>(this, value);
			return Task.Factory.StartNew(TextWriter._WriteCharDelegate, tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x00058140 File Offset: 0x00056340
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteAsync(string value)
		{
			Tuple<TextWriter, string> tuple = new Tuple<TextWriter, string>(this, value);
			return Task.Factory.StartNew(TextWriter._WriteStringDelegate, tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x00058170 File Offset: 0x00056370
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task WriteAsync(char[] buffer)
		{
			if (buffer == null)
			{
				return Task.CompletedTask;
			}
			return this.WriteAsync(buffer, 0, buffer.Length);
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x00058188 File Offset: 0x00056388
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteAsync(char[] buffer, int index, int count)
		{
			Tuple<TextWriter, char[], int, int> tuple = new Tuple<TextWriter, char[], int, int>(this, buffer, index, count);
			return Task.Factory.StartNew(TextWriter._WriteCharArrayRangeDelegate, tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x000581BC File Offset: 0x000563BC
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteLineAsync(char value)
		{
			Tuple<TextWriter, char> tuple = new Tuple<TextWriter, char>(this, value);
			return Task.Factory.StartNew(TextWriter._WriteLineCharDelegate, tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x000581EC File Offset: 0x000563EC
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteLineAsync(string value)
		{
			Tuple<TextWriter, string> tuple = new Tuple<TextWriter, string>(this, value);
			return Task.Factory.StartNew(TextWriter._WriteLineStringDelegate, tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x0005821C File Offset: 0x0005641C
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task WriteLineAsync(char[] buffer)
		{
			if (buffer == null)
			{
				return Task.CompletedTask;
			}
			return this.WriteLineAsync(buffer, 0, buffer.Length);
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x00058234 File Offset: 0x00056434
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteLineAsync(char[] buffer, int index, int count)
		{
			Tuple<TextWriter, char[], int, int> tuple = new Tuple<TextWriter, char[], int, int>(this, buffer, index, count);
			return Task.Factory.StartNew(TextWriter._WriteLineCharArrayRangeDelegate, tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x00058266 File Offset: 0x00056466
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteLineAsync()
		{
			return this.WriteAsync(this.CoreNewLine);
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x00058274 File Offset: 0x00056474
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task FlushAsync()
		{
			return Task.Factory.StartNew(TextWriter._FlushDelegate, this, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x00058294 File Offset: 0x00056494
		// Note: this type is marked as 'beforefieldinit'.
		static TextWriter()
		{
		}

		// Token: 0x04000926 RID: 2342
		[__DynamicallyInvokable]
		public static readonly TextWriter Null = new TextWriter.NullTextWriter();

		// Token: 0x04000927 RID: 2343
		[NonSerialized]
		private static Action<object> _WriteCharDelegate = delegate(object state)
		{
			Tuple<TextWriter, char> tuple = (Tuple<TextWriter, char>)state;
			tuple.Item1.Write(tuple.Item2);
		};

		// Token: 0x04000928 RID: 2344
		[NonSerialized]
		private static Action<object> _WriteStringDelegate = delegate(object state)
		{
			Tuple<TextWriter, string> tuple = (Tuple<TextWriter, string>)state;
			tuple.Item1.Write(tuple.Item2);
		};

		// Token: 0x04000929 RID: 2345
		[NonSerialized]
		private static Action<object> _WriteCharArrayRangeDelegate = delegate(object state)
		{
			Tuple<TextWriter, char[], int, int> tuple = (Tuple<TextWriter, char[], int, int>)state;
			tuple.Item1.Write(tuple.Item2, tuple.Item3, tuple.Item4);
		};

		// Token: 0x0400092A RID: 2346
		[NonSerialized]
		private static Action<object> _WriteLineCharDelegate = delegate(object state)
		{
			Tuple<TextWriter, char> tuple = (Tuple<TextWriter, char>)state;
			tuple.Item1.WriteLine(tuple.Item2);
		};

		// Token: 0x0400092B RID: 2347
		[NonSerialized]
		private static Action<object> _WriteLineStringDelegate = delegate(object state)
		{
			Tuple<TextWriter, string> tuple = (Tuple<TextWriter, string>)state;
			tuple.Item1.WriteLine(tuple.Item2);
		};

		// Token: 0x0400092C RID: 2348
		[NonSerialized]
		private static Action<object> _WriteLineCharArrayRangeDelegate = delegate(object state)
		{
			Tuple<TextWriter, char[], int, int> tuple = (Tuple<TextWriter, char[], int, int>)state;
			tuple.Item1.WriteLine(tuple.Item2, tuple.Item3, tuple.Item4);
		};

		// Token: 0x0400092D RID: 2349
		[NonSerialized]
		private static Action<object> _FlushDelegate = delegate(object state)
		{
			((TextWriter)state).Flush();
		};

		// Token: 0x0400092E RID: 2350
		private const string InitialNewLine = "\r\n";

		// Token: 0x0400092F RID: 2351
		[__DynamicallyInvokable]
		protected char[] CoreNewLine = new char[] { '\r', '\n' };

		// Token: 0x04000930 RID: 2352
		private IFormatProvider InternalFormatProvider;

		// Token: 0x02000B2A RID: 2858
		[Serializable]
		private sealed class NullTextWriter : TextWriter
		{
			// Token: 0x06006B21 RID: 27425 RVA: 0x001730D5 File Offset: 0x001712D5
			internal NullTextWriter()
				: base(CultureInfo.InvariantCulture)
			{
			}

			// Token: 0x1700121A RID: 4634
			// (get) Token: 0x06006B22 RID: 27426 RVA: 0x001730E2 File Offset: 0x001712E2
			public override Encoding Encoding
			{
				get
				{
					return Encoding.Default;
				}
			}

			// Token: 0x06006B23 RID: 27427 RVA: 0x001730E9 File Offset: 0x001712E9
			public override void Write(char[] buffer, int index, int count)
			{
			}

			// Token: 0x06006B24 RID: 27428 RVA: 0x001730EB File Offset: 0x001712EB
			public override void Write(string value)
			{
			}

			// Token: 0x06006B25 RID: 27429 RVA: 0x001730ED File Offset: 0x001712ED
			public override void WriteLine()
			{
			}

			// Token: 0x06006B26 RID: 27430 RVA: 0x001730EF File Offset: 0x001712EF
			public override void WriteLine(string value)
			{
			}

			// Token: 0x06006B27 RID: 27431 RVA: 0x001730F1 File Offset: 0x001712F1
			public override void WriteLine(object value)
			{
			}
		}

		// Token: 0x02000B2B RID: 2859
		[Serializable]
		internal sealed class SyncTextWriter : TextWriter, IDisposable
		{
			// Token: 0x06006B28 RID: 27432 RVA: 0x001730F3 File Offset: 0x001712F3
			internal SyncTextWriter(TextWriter t)
				: base(t.FormatProvider)
			{
				this._out = t;
			}

			// Token: 0x1700121B RID: 4635
			// (get) Token: 0x06006B29 RID: 27433 RVA: 0x00173108 File Offset: 0x00171308
			public override Encoding Encoding
			{
				get
				{
					return this._out.Encoding;
				}
			}

			// Token: 0x1700121C RID: 4636
			// (get) Token: 0x06006B2A RID: 27434 RVA: 0x00173115 File Offset: 0x00171315
			public override IFormatProvider FormatProvider
			{
				get
				{
					return this._out.FormatProvider;
				}
			}

			// Token: 0x1700121D RID: 4637
			// (get) Token: 0x06006B2B RID: 27435 RVA: 0x00173122 File Offset: 0x00171322
			// (set) Token: 0x06006B2C RID: 27436 RVA: 0x0017312F File Offset: 0x0017132F
			public override string NewLine
			{
				[MethodImpl(MethodImplOptions.Synchronized)]
				get
				{
					return this._out.NewLine;
				}
				[MethodImpl(MethodImplOptions.Synchronized)]
				set
				{
					this._out.NewLine = value;
				}
			}

			// Token: 0x06006B2D RID: 27437 RVA: 0x0017313D File Offset: 0x0017133D
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Close()
			{
				this._out.Close();
			}

			// Token: 0x06006B2E RID: 27438 RVA: 0x0017314A File Offset: 0x0017134A
			[MethodImpl(MethodImplOptions.Synchronized)]
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					((IDisposable)this._out).Dispose();
				}
			}

			// Token: 0x06006B2F RID: 27439 RVA: 0x0017315A File Offset: 0x0017135A
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Flush()
			{
				this._out.Flush();
			}

			// Token: 0x06006B30 RID: 27440 RVA: 0x00173167 File Offset: 0x00171367
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(char value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006B31 RID: 27441 RVA: 0x00173175 File Offset: 0x00171375
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(char[] buffer)
			{
				this._out.Write(buffer);
			}

			// Token: 0x06006B32 RID: 27442 RVA: 0x00173183 File Offset: 0x00171383
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(char[] buffer, int index, int count)
			{
				this._out.Write(buffer, index, count);
			}

			// Token: 0x06006B33 RID: 27443 RVA: 0x00173193 File Offset: 0x00171393
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(bool value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006B34 RID: 27444 RVA: 0x001731A1 File Offset: 0x001713A1
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(int value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006B35 RID: 27445 RVA: 0x001731AF File Offset: 0x001713AF
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(uint value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006B36 RID: 27446 RVA: 0x001731BD File Offset: 0x001713BD
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(long value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006B37 RID: 27447 RVA: 0x001731CB File Offset: 0x001713CB
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(ulong value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006B38 RID: 27448 RVA: 0x001731D9 File Offset: 0x001713D9
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(float value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006B39 RID: 27449 RVA: 0x001731E7 File Offset: 0x001713E7
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(double value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006B3A RID: 27450 RVA: 0x001731F5 File Offset: 0x001713F5
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(decimal value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006B3B RID: 27451 RVA: 0x00173203 File Offset: 0x00171403
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006B3C RID: 27452 RVA: 0x00173211 File Offset: 0x00171411
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(object value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006B3D RID: 27453 RVA: 0x0017321F File Offset: 0x0017141F
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string format, object arg0)
			{
				this._out.Write(format, arg0);
			}

			// Token: 0x06006B3E RID: 27454 RVA: 0x0017322E File Offset: 0x0017142E
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string format, object arg0, object arg1)
			{
				this._out.Write(format, arg0, arg1);
			}

			// Token: 0x06006B3F RID: 27455 RVA: 0x0017323E File Offset: 0x0017143E
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string format, object arg0, object arg1, object arg2)
			{
				this._out.Write(format, arg0, arg1, arg2);
			}

			// Token: 0x06006B40 RID: 27456 RVA: 0x00173250 File Offset: 0x00171450
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string format, params object[] arg)
			{
				this._out.Write(format, arg);
			}

			// Token: 0x06006B41 RID: 27457 RVA: 0x0017325F File Offset: 0x0017145F
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine()
			{
				this._out.WriteLine();
			}

			// Token: 0x06006B42 RID: 27458 RVA: 0x0017326C File Offset: 0x0017146C
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(char value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006B43 RID: 27459 RVA: 0x0017327A File Offset: 0x0017147A
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(decimal value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006B44 RID: 27460 RVA: 0x00173288 File Offset: 0x00171488
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(char[] buffer)
			{
				this._out.WriteLine(buffer);
			}

			// Token: 0x06006B45 RID: 27461 RVA: 0x00173296 File Offset: 0x00171496
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(char[] buffer, int index, int count)
			{
				this._out.WriteLine(buffer, index, count);
			}

			// Token: 0x06006B46 RID: 27462 RVA: 0x001732A6 File Offset: 0x001714A6
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(bool value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006B47 RID: 27463 RVA: 0x001732B4 File Offset: 0x001714B4
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(int value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006B48 RID: 27464 RVA: 0x001732C2 File Offset: 0x001714C2
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(uint value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006B49 RID: 27465 RVA: 0x001732D0 File Offset: 0x001714D0
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(long value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006B4A RID: 27466 RVA: 0x001732DE File Offset: 0x001714DE
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(ulong value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006B4B RID: 27467 RVA: 0x001732EC File Offset: 0x001714EC
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(float value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006B4C RID: 27468 RVA: 0x001732FA File Offset: 0x001714FA
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(double value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006B4D RID: 27469 RVA: 0x00173308 File Offset: 0x00171508
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006B4E RID: 27470 RVA: 0x00173316 File Offset: 0x00171516
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(object value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006B4F RID: 27471 RVA: 0x00173324 File Offset: 0x00171524
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, object arg0)
			{
				this._out.WriteLine(format, arg0);
			}

			// Token: 0x06006B50 RID: 27472 RVA: 0x00173333 File Offset: 0x00171533
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, object arg0, object arg1)
			{
				this._out.WriteLine(format, arg0, arg1);
			}

			// Token: 0x06006B51 RID: 27473 RVA: 0x00173343 File Offset: 0x00171543
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, object arg0, object arg1, object arg2)
			{
				this._out.WriteLine(format, arg0, arg1, arg2);
			}

			// Token: 0x06006B52 RID: 27474 RVA: 0x00173355 File Offset: 0x00171555
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, params object[] arg)
			{
				this._out.WriteLine(format, arg);
			}

			// Token: 0x06006B53 RID: 27475 RVA: 0x00173364 File Offset: 0x00171564
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteAsync(char value)
			{
				this.Write(value);
				return Task.CompletedTask;
			}

			// Token: 0x06006B54 RID: 27476 RVA: 0x00173372 File Offset: 0x00171572
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteAsync(string value)
			{
				this.Write(value);
				return Task.CompletedTask;
			}

			// Token: 0x06006B55 RID: 27477 RVA: 0x00173380 File Offset: 0x00171580
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteAsync(char[] buffer, int index, int count)
			{
				this.Write(buffer, index, count);
				return Task.CompletedTask;
			}

			// Token: 0x06006B56 RID: 27478 RVA: 0x00173390 File Offset: 0x00171590
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteLineAsync(char value)
			{
				this.WriteLine(value);
				return Task.CompletedTask;
			}

			// Token: 0x06006B57 RID: 27479 RVA: 0x0017339E File Offset: 0x0017159E
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteLineAsync(string value)
			{
				this.WriteLine(value);
				return Task.CompletedTask;
			}

			// Token: 0x06006B58 RID: 27480 RVA: 0x001733AC File Offset: 0x001715AC
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteLineAsync(char[] buffer, int index, int count)
			{
				this.WriteLine(buffer, index, count);
				return Task.CompletedTask;
			}

			// Token: 0x06006B59 RID: 27481 RVA: 0x001733BC File Offset: 0x001715BC
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task FlushAsync()
			{
				this.Flush();
				return Task.CompletedTask;
			}

			// Token: 0x04003340 RID: 13120
			private TextWriter _out;
		}

		// Token: 0x02000B2C RID: 2860
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06006B5A RID: 27482 RVA: 0x001733C9 File Offset: 0x001715C9
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06006B5B RID: 27483 RVA: 0x001733D5 File Offset: 0x001715D5
			public <>c()
			{
			}

			// Token: 0x06006B5C RID: 27484 RVA: 0x001733E0 File Offset: 0x001715E0
			internal void <.cctor>b__72_0(object state)
			{
				Tuple<TextWriter, char> tuple = (Tuple<TextWriter, char>)state;
				tuple.Item1.Write(tuple.Item2);
			}

			// Token: 0x06006B5D RID: 27485 RVA: 0x00173408 File Offset: 0x00171608
			internal void <.cctor>b__72_1(object state)
			{
				Tuple<TextWriter, string> tuple = (Tuple<TextWriter, string>)state;
				tuple.Item1.Write(tuple.Item2);
			}

			// Token: 0x06006B5E RID: 27486 RVA: 0x00173430 File Offset: 0x00171630
			internal void <.cctor>b__72_2(object state)
			{
				Tuple<TextWriter, char[], int, int> tuple = (Tuple<TextWriter, char[], int, int>)state;
				tuple.Item1.Write(tuple.Item2, tuple.Item3, tuple.Item4);
			}

			// Token: 0x06006B5F RID: 27487 RVA: 0x00173464 File Offset: 0x00171664
			internal void <.cctor>b__72_3(object state)
			{
				Tuple<TextWriter, char> tuple = (Tuple<TextWriter, char>)state;
				tuple.Item1.WriteLine(tuple.Item2);
			}

			// Token: 0x06006B60 RID: 27488 RVA: 0x0017348C File Offset: 0x0017168C
			internal void <.cctor>b__72_4(object state)
			{
				Tuple<TextWriter, string> tuple = (Tuple<TextWriter, string>)state;
				tuple.Item1.WriteLine(tuple.Item2);
			}

			// Token: 0x06006B61 RID: 27489 RVA: 0x001734B4 File Offset: 0x001716B4
			internal void <.cctor>b__72_5(object state)
			{
				Tuple<TextWriter, char[], int, int> tuple = (Tuple<TextWriter, char[], int, int>)state;
				tuple.Item1.WriteLine(tuple.Item2, tuple.Item3, tuple.Item4);
			}

			// Token: 0x06006B62 RID: 27490 RVA: 0x001734E5 File Offset: 0x001716E5
			internal void <.cctor>b__72_6(object state)
			{
				((TextWriter)state).Flush();
			}

			// Token: 0x04003341 RID: 13121
			public static readonly TextWriter.<>c <>9 = new TextWriter.<>c();
		}
	}
}
