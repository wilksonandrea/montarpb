using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using System;

public class GClass14
{
    public static void smethod_0(SyncClientPacket syncClientPacket_0)
    {
        byte length = syncClientPacket_0.ReadC();
        string str = syncClientPacket_0.ReadS(length);
        if (!string.IsNullOrEmpty(str) && (length < 60))
        {
            CLogger.Print("From Server Message: " + str, LoggerType.Info, null);
        }
    }
}

