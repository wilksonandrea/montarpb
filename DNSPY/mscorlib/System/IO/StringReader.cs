using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x020001A4 RID: 420
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class StringReader : TextReader
	{
		// Token: 0x06001A0D RID: 6669 RVA: 0x00057347 File Offset: 0x00055547
		[__DynamicallyInvokable]
		public StringReader(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			this._s = s;
			this._length = ((s == null) ? 0 : s.Length);
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x00057376 File Offset: 0x00055576
		public override void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x0005737F File Offset: 0x0005557F
		[__DynamicallyInvokable]
		protected override void Dispose(bool disposing)
		{
			this._s = null;
			this._pos = 0;
			this._length = 0;
			base.Dispose(disposing);
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x0005739D File Offset: 0x0005559D
		[__DynamicallyInvokable]
		public override int Peek()
		{
			if (this._s == null)
			{
				__Error.ReaderClosed();
			}
			if (this._pos == this._length)
			{
				return -1;
			}
			return (int)this._s[this._pos];
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x000573D0 File Offset: 0x000555D0
		[__DynamicallyInvokable]
		public override int Read()
		{
			if (this._s == null)
			{
				__Error.ReaderClosed();
			}
			if (this._pos == this._length)
			{
				return -1;
			}
			string s = this._s;
			int pos = this._pos;
			this._pos = pos + 1;
			return (int)s[pos];
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x00057418 File Offset: 0x00055618
		[__DynamicallyInvokable]
		public override int Read([In] [Out] char[] buffer, int index, int count)
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
			if (this._s == null)
			{
				__Error.ReaderClosed();
			}
			int num = this._length - this._pos;
			if (num > 0)
			{
				if (num > count)
				{
					num = count;
				}
				this._s.CopyTo(this._pos, buffer, index, num);
				this._pos += num;
			}
			return num;
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x000574D0 File Offset: 0x000556D0
		[__DynamicallyInvokable]
		public override string ReadToEnd()
		{
			if (this._s == null)
			{
				__Error.ReaderClosed();
			}
			string text;
			if (this._pos == 0)
			{
				text = this._s;
			}
			else
			{
				text = this._s.Substring(this._pos, this._length - this._pos);
			}
			this._pos = this._length;
			return text;
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x00057528 File Offset: 0x00055728
		[__DynamicallyInvokable]
		public override string ReadLine()
		{
			if (this._s == null)
			{
				__Error.ReaderClosed();
			}
			int i;
			for (i = this._pos; i < this._length; i++)
			{
				char c = this._s[i];
				if (c == '\r' || c == '\n')
				{
					string text = this._s.Substring(this._pos, i - this._pos);
					this._pos = i + 1;
					if (c == '\r' && this._pos < this._length && this._s[this._pos] == '\n')
					{
						this._pos++;
					}
					return text;
				}
			}
			if (i > this._pos)
			{
				string text2 = this._s.Substring(this._pos, i - this._pos);
				this._pos = i;
				return text2;
			}
			return null;
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x000575F7 File Offset: 0x000557F7
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public override Task<string> ReadLineAsync()
		{
			return Task.FromResult<string>(this.ReadLine());
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x00057604 File Offset: 0x00055804
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public override Task<string> ReadToEndAsync()
		{
			return Task.FromResult<string>(this.ReadToEnd());
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x00057614 File Offset: 0x00055814
		[ComVisible(false)]
		[__DynamicallyInvokable]
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

		// Token: 0x06001A18 RID: 6680 RVA: 0x00057688 File Offset: 0x00055888
		[ComVisible(false)]
		[__DynamicallyInvokable]
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

		// Token: 0x0400091D RID: 2333
		private string _s;

		// Token: 0x0400091E RID: 2334
		private int _pos;

		// Token: 0x0400091F RID: 2335
		private int _length;
	}
}
