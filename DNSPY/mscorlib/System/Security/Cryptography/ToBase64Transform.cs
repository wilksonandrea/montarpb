using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Cryptography
{
	// Token: 0x0200024F RID: 591
	[ComVisible(true)]
	public class ToBase64Transform : ICryptoTransform, IDisposable
	{
		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x060020F6 RID: 8438 RVA: 0x00072AE2 File Offset: 0x00070CE2
		public int InputBlockSize
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x060020F7 RID: 8439 RVA: 0x00072AE5 File Offset: 0x00070CE5
		public int OutputBlockSize
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x060020F8 RID: 8440 RVA: 0x00072AE8 File Offset: 0x00070CE8
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x060020F9 RID: 8441 RVA: 0x00072AEB File Offset: 0x00070CEB
		public virtual bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x00072AF0 File Offset: 0x00070CF0
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
			char[] array = new char[4];
			Convert.ToBase64CharArray(inputBuffer, inputOffset, 3, array, 0);
			byte[] bytes = Encoding.ASCII.GetBytes(array);
			if (bytes.Length != 4)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_SSE_InvalidDataSize"));
			}
			Buffer.BlockCopy(bytes, 0, outputBuffer, outputOffset, bytes.Length);
			return bytes.Length;
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x00072B9C File Offset: 0x00070D9C
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
			if (inputCount == 0)
			{
				return EmptyArray<byte>.Value;
			}
			char[] array = new char[4];
			Convert.ToBase64CharArray(inputBuffer, inputOffset, inputCount, array, 0);
			return Encoding.ASCII.GetBytes(array);
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x00072C2A File Offset: 0x00070E2A
		public void Dispose()
		{
			this.Clear();
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x00072C32 File Offset: 0x00070E32
		public void Clear()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x00072C41 File Offset: 0x00070E41
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x00072C44 File Offset: 0x00070E44
		~ToBase64Transform()
		{
			this.Dispose(false);
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x00072C74 File Offset: 0x00070E74
		public ToBase64Transform()
		{
		}
	}
}
