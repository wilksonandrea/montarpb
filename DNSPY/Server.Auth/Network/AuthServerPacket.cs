using System;
using System.IO;
using Microsoft.Win32.SafeHandles;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;

namespace Server.Auth.Network
{
	// Token: 0x02000007 RID: 7
	public abstract class AuthServerPacket : BaseServerPacket, IDisposable
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00003EA8 File Offset: 0x000020A8
		public AuthServerPacket()
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

		// Token: 0x06000039 RID: 57 RVA: 0x00003F24 File Offset: 0x00002124
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

		// Token: 0x0600003A RID: 58 RVA: 0x00003F80 File Offset: 0x00002180
		public byte[] GetCompleteBytes(string Name)
		{
			byte[] array;
			try
			{
				byte[] bytes = this.GetBytes("AuthServerPacket.GetCompleteBytes");
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

		// Token: 0x0600003B RID: 59 RVA: 0x00003FE4 File Offset: 0x000021E4
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

		// Token: 0x0600003C RID: 60 RVA: 0x00004020 File Offset: 0x00002220
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

		// Token: 0x0600003D RID: 61
		public abstract void Write();
	}
}
