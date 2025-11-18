namespace Server.Auth.Network
{
    using Microsoft.Win32.SafeHandles;
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Plugin.Core.Utility;
    using System;
    using System.IO;

    public abstract class AuthServerPacket : BaseServerPacket, IDisposable
    {
        public AuthServerPacket()
        {
            base.MStream = new MemoryStream();
            base.BWriter = new BinaryWriter(base.MStream);
            base.Handle = new SafeFileHandle(IntPtr.Zero, true);
            base.Disposed = false;
            base.SECURITY_KEY = Bitwise.CRYPTO[0];
            base.HASH_CODE = Bitwise.CRYPTO[1];
            base.SEED_LENGTH = Bitwise.CRYPTO[2];
            base.NATIONS = ConfigLoader.National;
        }

        public void Dispose()
        {
            try
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (!base.Disposed)
                {
                    base.MStream.Dispose();
                    base.BWriter.Dispose();
                    if (disposing)
                    {
                        base.Handle.Dispose();
                    }
                    base.Disposed = true;
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }

        public byte[] GetBytes(string Name)
        {
            try
            {
                this.Write();
                return base.MStream.ToArray();
            }
            catch (Exception exception)
            {
                CLogger.Print("GetBytes problem at: " + Name + "; " + exception.Message, LoggerType.Error, exception);
                return new byte[0];
            }
        }

        public byte[] GetCompleteBytes(string Name)
        {
            try
            {
                byte[] bytes = this.GetBytes("AuthServerPacket.GetCompleteBytes");
                return ((bytes.Length < 2) ? new byte[0] : bytes);
            }
            catch (Exception exception)
            {
                CLogger.Print("GetCompleteBytes problem at: " + Name + "; " + exception.Message, LoggerType.Error, exception);
                return new byte[0];
            }
        }

        public abstract void Write();
    }
}

