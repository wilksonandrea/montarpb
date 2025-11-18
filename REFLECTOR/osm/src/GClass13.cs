using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;
using System;
using System.Net;

public class GClass13
{
    public static void smethod_0(string string_0)
    {
        try
        {
            foreach (SChannelModel model in SChannelXML.Servers)
            {
                if (model == null)
                {
                    break;
                }
                IPEndPoint connection = SynchronizeXML.GetServer(model.Port).Connection;
                using (SyncServerPacket packet = new SyncServerPacket())
                {
                    packet.WriteH((short) 0x1c03);
                    packet.WriteC((byte) string_0.Length);
                    packet.WriteS(string_0, string_0.Length);
                    GClass12.smethod_4(packet.ToArray(), connection);
                }
            }
        }
        catch (Exception exception)
        {
            CLogger.Print(exception.Message, LoggerType.Error, exception);
        }
    }
}

