using Microsoft.Win32.SafeHandles;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Server.Game.Network
{
	public abstract class GameClientPacket : BaseClientPacket, IDisposable
	{
		protected GameClient Client;

		public GameClientPacket()
		{
			this.Handle = new SafeFileHandle(IntPtr.Zero, true);
			this.Disposed = false;
			this.SECURITY_KEY = Bitwise.CRYPTO[0];
			this.HASH_CODE = Bitwise.CRYPTO[1];
			this.SEED_LENGTH = Bitwise.CRYPTO[2];
			this.NATIONS = ConfigLoader.National;
		}

		public void Dispose()
		{
			try
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

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
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

		protected internal void Makeme(GameClient Client, byte[] Buffer)
		{
			this.Client = Client;
			this.MStream = new MemoryStream(Buffer, 4, (int)Buffer.Length - 4);
			this.BReader = new BinaryReader(this.MStream);
			this.Read();
		}

		public abstract void Read();

		public abstract void Run();
	}
}