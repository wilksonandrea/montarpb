using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Cryptography
{
	// Token: 0x02000250 RID: 592
	[ComVisible(true)]
	public class FromBase64Transform : ICryptoTransform, IDisposable
	{
		// Token: 0x06002101 RID: 8449 RVA: 0x00072C7C File Offset: 0x00070E7C
		public FromBase64Transform()
			: this(FromBase64TransformMode.IgnoreWhiteSpaces)
		{
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x00072C85 File Offset: 0x00070E85
		public FromBase64Transform(FromBase64TransformMode whitespaces)
		{
			this._whitespaces = whitespaces;
			this._inputIndex = 0;
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06002103 RID: 8451 RVA: 0x00072CA7 File Offset: 0x00070EA7
		public int InputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06002104 RID: 8452 RVA: 0x00072CAA File Offset: 0x00070EAA
		public int OutputBlockSize
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06002105 RID: 8453 RVA: 0x00072CAD File Offset: 0x00070EAD
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06002106 RID: 8454 RVA: 0x00072CB0 File Offset: 0x00070EB0
		public virtual bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x00072CB4 File Offset: 0x00070EB4
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (inputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("inputOffset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (inputCount < 0 || inputCount > inputBuffer.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
			}
			if (inputBuffer.Length - inputCount < inputOffset)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (this._inputBuffer == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_Generic"));
			}
			byte[] array = new byte[inputCount];
			int num;
			if (this._whitespaces == FromBase64TransformMode.IgnoreWhiteSpaces)
			{
				array = this.DiscardWhiteSpaces(inputBuffer, inputOffset, inputCount);
				num = array.Length;
			}
			else
			{
				Buffer.InternalBlockCopy(inputBuffer, inputOffset, array, 0, inputCount);
				num = inputCount;
			}
			if (num + this._inputIndex < 4)
			{
				Buffer.InternalBlockCopy(array, 0, this._inputBuffer, this._inputIndex, num);
				this._inputIndex += num;
				return 0;
			}
			int num2 = (num + this._inputIndex) / 4;
			byte[] array2 = new byte[this._inputIndex + num];
			Buffer.InternalBlockCopy(this._inputBuffer, 0, array2, 0, this._inputIndex);
			Buffer.InternalBlockCopy(array, 0, array2, this._inputIndex, num);
			this._inputIndex = (num + this._inputIndex) % 4;
			Buffer.InternalBlockCopy(array, num - this._inputIndex, this._inputBuffer, 0, this._inputIndex);
			char[] chars = Encoding.ASCII.GetChars(array2, 0, 4 * num2);
			byte[] array3 = Convert.FromBase64CharArray(chars, 0, 4 * num2);
			Buffer.BlockCopy(array3, 0, outputBuffer, outputOffset, array3.Length);
			return array3.Length;
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x00072E28 File Offset: 0x00071028
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (inputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("inputOffset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (inputCount < 0 || inputCount > inputBuffer.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
			}
			if (inputBuffer.Length - inputCount < inputOffset)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (this._inputBuffer == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_Generic"));
			}
			byte[] array = new byte[inputCount];
			int num;
			if (this._whitespaces == FromBase64TransformMode.IgnoreWhiteSpaces)
			{
				array = this.DiscardWhiteSpaces(inputBuffer, inputOffset, inputCount);
				num = array.Length;
			}
			else
			{
				Buffer.InternalBlockCopy(inputBuffer, inputOffset, array, 0, inputCount);
				num = inputCount;
			}
			if (num + this._inputIndex < 4)
			{
				this.Reset();
				return EmptyArray<byte>.Value;
			}
			int num2 = (num + this._inputIndex) / 4;
			byte[] array2 = new byte[this._inputIndex + num];
			Buffer.InternalBlockCopy(this._inputBuffer, 0, array2, 0, this._inputIndex);
			Buffer.InternalBlockCopy(array, 0, array2, this._inputIndex, num);
			this._inputIndex = (num + this._inputIndex) % 4;
			Buffer.InternalBlockCopy(array, num - this._inputIndex, this._inputBuffer, 0, this._inputIndex);
			char[] chars = Encoding.ASCII.GetChars(array2, 0, 4 * num2);
			byte[] array3 = Convert.FromBase64CharArray(chars, 0, 4 * num2);
			this.Reset();
			return array3;
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x00072F78 File Offset: 0x00071178
		private byte[] DiscardWhiteSpaces(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			int num = 0;
			for (int i = 0; i < inputCount; i++)
			{
				if (char.IsWhiteSpace((char)inputBuffer[inputOffset + i]))
				{
					num++;
				}
			}
			byte[] array = new byte[inputCount - num];
			num = 0;
			for (int i = 0; i < inputCount; i++)
			{
				if (!char.IsWhiteSpace((char)inputBuffer[inputOffset + i]))
				{
					array[num++] = inputBuffer[inputOffset + i];
				}
			}
			return array;
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x00072FD3 File Offset: 0x000711D3
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x00072FE2 File Offset: 0x000711E2
		private void Reset()
		{
			this._inputIndex = 0;
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x00072FEB File Offset: 0x000711EB
		public void Clear()
		{
			this.Dispose();
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x00072FF3 File Offset: 0x000711F3
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._inputBuffer != null)
				{
					Array.Clear(this._inputBuffer, 0, this._inputBuffer.Length);
				}
				this._inputBuffer = null;
				this._inputIndex = 0;
			}
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x00073024 File Offset: 0x00071224
		~FromBase64Transform()
		{
			this.Dispose(false);
		}

		// Token: 0x04000BEE RID: 3054
		private byte[] _inputBuffer = new byte[4];

		// Token: 0x04000BEF RID: 3055
		private int _inputIndex;

		// Token: 0x04000BF0 RID: 3056
		private FromBase64TransformMode _whitespaces;
	}
}
