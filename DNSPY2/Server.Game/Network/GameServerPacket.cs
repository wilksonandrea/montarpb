using System;
using System.IO;
using Microsoft.Win32.SafeHandles;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;

namespace Server.Game.Network
{
	// Token: 0x02000008 RID: 8
	public abstract class GameServerPacket : BaseServerPacket, IDisposable
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00008BB0 File Offset: 0x00006DB0
		public GameServerPacket()
		{
			this.MStream = new MemoryStream();
			this.BWriter = new BinaryWriter(this.MStream);
			this.Handle = new SafeFileHandle(IntPtr.Zero, true);
			this.Disposed = false;
			this.SECURITY_KEY = Bitwise.CRYPTO[0];
			this.HASH_CODE = Bitwise.CRYPTO[1];
			this.SEED_LENGTH = Bitwise.CRYPTO[2];
			this.NATIONS = ConfigLoader.National;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00008C2C File Offset: 0x00006E2C
		public byte[] GetBytes(string Name)
		{
			byte[] array;
			try
			{
				this.Write();
				array = this.MStream.ToArray();
			}
			catch (Exception ex)
			{
				CLogger.Print("GetBytes problem at: " + Name + "; " + ex.Message, LoggerType.Error, ex);
				array = new byte[0];
			}
			return array;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00008C88 File Offset: 0x00006E88
		public byte[] GetCompleteBytes(string Name)
		{
			byte[] array;
			try
			{
				byte[] bytes = this.GetBytes("GameServerPacket.GetCompleteBytes");
				if (bytes.Length >= 2)
				{
					array = bytes;
				}
				else
				{
					array = new byte[0];
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("GetCompleteBytes problem at: " + Name + "; " + ex.Message, LoggerType.Error, ex);
				array = new byte[0];
			}
			return array;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00008CEC File Offset: 0x00006EEC
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

		// Token: 0x0600004A RID: 74 RVA: 0x00008D28 File Offset: 0x00006F28
		protected virtual void Dispose(bool disposing)
		{
			try
			{
				if (!this.Disposed)
				{
					this.MStream.Dispose();
					this.BWriter.Dispose();
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

		// Token: 0x0600004B RID: 75
		public abstract void Write();
	}
}
