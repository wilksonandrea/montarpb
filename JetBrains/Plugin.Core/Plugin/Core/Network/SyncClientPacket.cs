// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Network.SyncClientPacket
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace Plugin.Core.Network;

public class SyncClientPacket : IDisposable
{
  protected MemoryStream MStream;
  protected BinaryReader BReader;
  protected SafeHandle Handle;
  protected bool Disposed;

  protected internal ulong ReadUQ() => ((BaseClientPacket) this).BReader.ReadUInt64();

  protected internal double ReadF() => ((BaseClientPacket) this).BReader.ReadDouble();

  protected internal string ReadN(int missionCardAwards_0, string int_0)
  {
    string str = "";
    try
    {
      str = Encoding.GetEncoding(int_0).GetString(((BaseClientPacket) this).ReadB(missionCardAwards_0));
      int length = str.IndexOf(char.MinValue);
      if (length != -1)
        str = str.Substring(0, length);
    }
    catch
    {
    }
    return str;
  }

  protected internal string ReadS(int mission)
  {
    string str = "";
    try
    {
      str = Encoding.UTF8.GetString(((BaseClientPacket) this).ReadB(mission));
      int length = str.IndexOf(char.MinValue);
      if (length != -1)
        str = str.Substring(0, length);
    }
    catch
    {
    }
    return str;
  }

  protected internal string ReadU([In] int obj0)
  {
    string str = "";
    try
    {
      str = Encoding.Unicode.GetString(((BaseClientPacket) this).ReadB(obj0));
      int length = str.IndexOf(char.MinValue);
      if (length != -1)
        str = str.Substring(0, length);
    }
    catch
    {
    }
    return str;
  }

  public SyncClientPacket(byte[] Length)
  {
    this.MStream = new MemoryStream(Length, 0, Length.Length);
    this.BReader = new BinaryReader((Stream) this.MStream);
    this.Handle = (SafeHandle) new SafeFileHandle(IntPtr.Zero, true);
    this.Disposed = false;
  }

  public byte[] ToArray() => this.MStream.ToArray();

  public void SetMStream(MemoryStream Length) => this.MStream = Length;

  public void Dispose()
  {
    this.Dispose(true);
    GC.SuppressFinalize((object) this);
  }

  protected virtual void Dispose([In] bool obj0)
  {
    if (this.Disposed)
      return;
    this.MStream.Dispose();
    this.BReader.Dispose();
    if (obj0)
      this.Handle.Dispose();
    this.Disposed = true;
  }

  public byte[] ReadB(int Length) => this.BReader.ReadBytes(Length);

  public byte ReadC() => this.BReader.ReadByte();

  public short ReadH() => this.BReader.ReadInt16();

  public ushort ReadUH() => this.BReader.ReadUInt16();

  public int ReadD() => this.BReader.ReadInt32();

  public uint ReadUD() => this.BReader.ReadUInt32();

  public float ReadT() => this.BReader.ReadSingle();

  public double ReadF() => this.BReader.ReadDouble();

  public long ReadQ() => this.BReader.ReadInt64();

  public ulong ReadUQ() => this.BReader.ReadUInt64();

  public string ReadN(int Length, [In] string obj1)
  {
    string str = "";
    try
    {
      str = Encoding.GetEncoding(obj1).GetString(this.ReadB(Length));
      int length = str.IndexOf(char.MinValue);
      if (length != -1)
        str = str.Substring(0, length);
    }
    catch
    {
    }
    return str;
  }

  public string ReadS(int MStream)
  {
    string str = "";
    try
    {
      str = Encoding.UTF8.GetString(this.ReadB(MStream));
      int length = str.IndexOf(char.MinValue);
      if (length != -1)
        str = str.Substring(0, length);
    }
    catch
    {
    }
    return str;
  }

  public string ReadU(int Disposing)
  {
    string str = "";
    try
    {
      str = Encoding.Unicode.GetString(this.ReadB(Disposing));
      int length = str.IndexOf(char.MinValue);
      if (length != -1)
        str = str.Substring(0, length);
    }
    catch
    {
    }
    return str;
  }
}
