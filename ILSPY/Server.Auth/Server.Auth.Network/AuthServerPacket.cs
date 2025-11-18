using System;
using System.IO;
using Microsoft.Win32.SafeHandles;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;

namespace Server.Auth.Network;

public abstract class AuthServerPacket : BaseServerPacket, IDisposable
{
	public AuthServerPacket()
	{
		MStream = new MemoryStream();
		BWriter = new BinaryWriter(MStream);
		Handle = new SafeFileHandle(IntPtr.Zero, ownsHandle: true);
		Disposed = false;
		SECURITY_KEY = Bitwise.CRYPTO[0];
		HASH_CODE = Bitwise.CRYPTO[1];
		SEED_LENGTH = Bitwise.CRYPTO[2];
		NATIONS = ConfigLoader.National;
	}

	public byte[] GetBytes(string Name)
	{
		try
		{
			Write();
			return MStream.ToArray();
		}
		catch (Exception ex)
		{
			CLogger.Print("GetBytes problem at: " + Name + "; " + ex.Message, LoggerType.Error, ex);
			return new byte[0];
		}
	}

	public byte[] GetCompleteBytes(string Name)
	{
		try
		{
			byte[] bytes = GetBytes("AuthServerPacket.GetCompleteBytes");
			if (bytes.Length >= 2)
			{
				return bytes;
			}
			return new byte[0];
		}
		catch (Exception ex)
		{
			CLogger.Print("GetCompleteBytes problem at: " + Name + "; " + ex.Message, LoggerType.Error, ex);
			return new byte[0];
		}
	}

	public void Dispose()
	{
		try
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	protected virtual void Dispose(bool disposing)
	{
		try
		{
			if (!Disposed)
			{
				MStream.Dispose();
				BWriter.Dispose();
				if (disposing)
				{
					Handle.Dispose();
				}
				Disposed = true;
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	public abstract void Write();
}
