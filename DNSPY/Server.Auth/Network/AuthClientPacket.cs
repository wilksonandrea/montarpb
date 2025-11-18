using System;
using System.IO;
using Microsoft.Win32.SafeHandles;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;

namespace Server.Auth.Network
{
	// Token: 0x02000008 RID: 8
	public abstract class AuthClientPacket : BaseClientPacket, IDisposable
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00004084 File Offset: 0x00002284
		public AuthClientPacket()
		{
			this.Handle = new SafeFileHandle(IntPtr.Zero, true);
			this.Disposed = false;
			this.SECURITY_KEY = Bitwise.CRYPTO[0];
			this.HASH_CODE = Bitwise.CRYPTO[1];
			this.SEED_LENGTH = Bitwise.CRYPTO[2];
			this.NATIONS = ConfigLoader.National;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000246A File Offset: 0x0000066A
		protected internal void Makeme(AuthClient Client, byte[] Buffer)
		{
			this.Client = Client;
			this.MStream = new MemoryStream(Buffer, 4, Buffer.Length - 4);
			this.BReader = new BinaryReader(this.MStream);
			this.Read();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000040E4 File Offset: 0x000022E4
		public void Dispose()
		{
			try
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00004120 File Offset: 0x00002320
		protected virtual void Dispose(bool disposing)
		{
			try
			{
				if (!this.Disposed)
				{
					this.MStream.Dispose();
					this.BReader.Dispose();
					if (disposing)
					{
						this.Handle.Dispose();
					}
					this.Disposed = true;
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000042 RID: 66
		public abstract void Read();

		// Token: 0x06000043 RID: 67
		public abstract void Run();

		// Token: 0x0400001C RID: 28
		protected AuthClient Client;
	}
}
