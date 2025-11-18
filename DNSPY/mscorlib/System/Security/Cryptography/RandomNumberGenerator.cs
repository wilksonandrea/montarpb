using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000246 RID: 582
	[ComVisible(true)]
	public abstract class RandomNumberGenerator : IDisposable
	{
		// Token: 0x060020C0 RID: 8384 RVA: 0x00072755 File Offset: 0x00070955
		protected RandomNumberGenerator()
		{
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x0007275D File Offset: 0x0007095D
		public static RandomNumberGenerator Create()
		{
			return RandomNumberGenerator.Create("System.Security.Cryptography.RandomNumberGenerator");
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x00072769 File Offset: 0x00070969
		public static RandomNumberGenerator Create(string rngName)
		{
			return (RandomNumberGenerator)CryptoConfig.CreateFromName(rngName);
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x00072776 File Offset: 0x00070976
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x00072785 File Offset: 0x00070985
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x060020C5 RID: 8389
		public abstract void GetBytes(byte[] data);

		// Token: 0x060020C6 RID: 8390 RVA: 0x00072788 File Offset: 0x00070988
		public virtual void GetBytes(byte[] data, int offset, int count)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (offset + count > data.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (count > 0)
			{
				byte[] array = new byte[count];
				this.GetBytes(array);
				Array.Copy(array, 0, data, offset, count);
			}
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x00072809 File Offset: 0x00070A09
		public virtual void GetNonZeroBytes(byte[] data)
		{
			throw new NotImplementedException();
		}
	}
}
