namespace Plugin.Core.SharpDX
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    public struct Vector3Union
    {
        [FieldOffset(0)]
        public Vector3 Vec3;
        [FieldOffset(0)]
        public RawVector3 RawVec3;
    }
}

