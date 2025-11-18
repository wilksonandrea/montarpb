using System;
using System.IO;
using Microsoft.Win32.SafeHandles;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;

namespace Server.Game.Network
{
	// Token: 0x02000007 RID: 7
	public abstract class GameClientPacket : BaseClientPacket, IDisposable
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00008AB0 File Offset: 0x00006CB0
		public GameClientPacket()
		{
			this.Handle = new SafeFileHandle(IntPtr.Zero, true);
			this.Disposed = false;
			this.SECURITY_KEY = Bitwise.CRYPTO[0];
			this.HASH_CODE = Bitwise.CRYPTO[1];
			this.SEED_LENGTH = Bitwise.CRYPTO[2];
			this.NATIONS = ConfigLoader.National;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002640 File Offset: 0x00000840
		protected internal void Makeme(GameClient Client, byte[] Buffer)
		{
			this.Client = Client;
			this.MStream = new MemoryStream(Buffer, 4, Buffer.Length - 4);
			this.BReader = new BinaryReader(this.MStream);
			this.Read();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00008B10 File Offset: 0x00006D10
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

		// Token: 0x06000043 RID: 67 RVA: 0x00008B4C File Offset: 0x00006D4C
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

		// Token: 0x06000044 RID: 68
		public abstract void Read();

		// Token: 0x06000045 RID: 69
		public abstract void Run();

		// Token: 0x0400001D RID: 29
		protected GameClient Client;
	}
}
