using Microsoft.Win32.SafeHandles;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Server.Auth.Network
{
	public abstract class AuthServerPacket : BaseServerPacket, IDisposable
	{
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
					this.BWriter.Dispose();
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

		public byte[] GetBytes(string Name)
		{
			byte[] array;
			try
			{
				this.Write();
				array = this.MStream.ToArray();
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("GetBytes problem at: ", Name, "; ", exception.Message), LoggerType.Error, exception);
				array = new byte[0];
			}
			return array;
		}

		public byte[] GetCompleteBytes(string Name)
		{
			byte[] numArray;
			try
			{
				byte[] bytes = this.GetBytes("AuthServerPacket.GetCompleteBytes");
				numArray = ((int)bytes.Length < 2 ? new byte[0] : bytes);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("GetCompleteBytes problem at: ", Name, "; ", exception.Message), LoggerType.Error, exception);
				numArray = new byte[0];
			}
			return numArray;
		}

		public abstract void Write();
	}
}