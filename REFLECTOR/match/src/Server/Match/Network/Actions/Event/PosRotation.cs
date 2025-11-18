namespace Server.Match.Network.Actions.Event
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Server.Match.Data.Models;
    using Server.Match.Data.Models.Event;
    using System;

    public class PosRotation
    {
        public static PosRotationInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
        {
            PosRotationInfo info1 = new PosRotationInfo();
            info1.CameraX = C.ReadUH();
            info1.CameraY = C.ReadUH();
            info1.Area = C.ReadUH();
            info1.RotationZ = C.ReadUH();
            info1.RotationX = C.ReadUH();
            info1.RotationY = C.ReadUH();
            PosRotationInfo info = info1;
            if (GenLog)
            {
                CLogger.Print($"PVP Slot: {Action.Slot}; Camera: (X: {info.CameraX}, Y: {info.CameraY}); Area: {info.Area}; Rotation: (X: {info.RotationX}, Y: {info.RotationY}, Z: {info.RotationZ})", LoggerType.Warning, null);
            }
            return info;
        }

        public static void WriteInfo(SyncServerPacket S, PosRotationInfo Info)
        {
            S.WriteH(Info.CameraX);
            S.WriteH(Info.CameraY);
            S.WriteH(Info.Area);
            S.WriteH(Info.RotationZ);
            S.WriteH(Info.RotationX);
            S.WriteH(Info.RotationY);
        }
    }
}

