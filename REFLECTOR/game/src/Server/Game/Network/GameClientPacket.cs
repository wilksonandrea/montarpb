namespace Server.Game.Network
{
    using Microsoft.Win32.SafeHandles;
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Plugin.Core.Utility;
    using Server.Game;
    using System;
    using System.IO;

    public abstract class GameClientPacket : BaseClientPacket, IDisposable
    {
        protected GameClient Client;

        public GameClientPacket()
        {
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
                    base.BReader.Dispose();
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

        protected internal void Makeme(GameClient Client, byte[] Buffer)
        {
            this.Client = Client;
            base.MStream = new MemoryStream(Buffer, 4, Buffer.Length - 4);
            base.BReader = new BinaryReader(base.MStream);
            this.Read();
        }

        public abstract void Read();
        public abstract void Run();
    }
}

