// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Network.SyncServerPacket
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Microsoft.Win32.SafeHandles;
using Plugin.Core.Enums;
using Plugin.Core.SharpDX;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace Plugin.Core.Network;

public class SyncServerPacket : IDisposable
{
  protected MemoryStream MStream;
  protected BinaryWriter BWriter;
  protected SafeHandle Handle;
  protected bool Disposed;

  public byte ReadC(ref bool Length)
  {
    try
    {
      int num = (int) ((SyncClientPacket) this).BReader.ReadByte();
      Length = false;
      return (byte) num;
    }
    catch
    {
      Length = true;
      return 0;
    }
  }

  public ushort ReadUH(ref bool Length)
  {
    try
    {
      int num = (int) ((SyncClientPacket) this).BReader.ReadUInt16();
      Length = false;
      return (ushort) num;
    }
    catch
    {
      Length = true;
      return 0;
    }
  }

  public void Advance([In] int obj0)
  {
    if ((((SyncClientPacket) this).BReader.BaseStream.Position += (long) obj0) > ((SyncClientPacket) this).BReader.BaseStream.Length)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("Advance crashed.", LoggerType.Warning, (Exception) null);
      throw new Exception("Offset has exceeded the buffer value.");
    }
  }

  public Half3 ReadUHV()
  {
    return new Half3(((SyncClientPacket) this).ReadUH(), ((SyncClientPacket) this).ReadUH(), ((SyncClientPacket) this).ReadUH());
  }

  public Half3 ReadTV()
  {
    return new Half3(((SyncClientPacket) this).ReadT(), ((SyncClientPacket) this).ReadT(), ((SyncClientPacket) this).ReadT());
  }

  public SyncServerPacket()
  {
    this.MStream = new MemoryStream();
    this.BWriter = new BinaryWriter((Stream) this.MStream);
    this.Handle = (SafeHandle) new SafeFileHandle(IntPtr.Zero, true);
    this.Disposed = false;
  }

  public SyncServerPacket(long Length)
  {
    this.MStream = new MemoryStream();
    this.MStream.SetLength(Length);
    this.BWriter = new BinaryWriter((Stream) this.MStream);
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

  protected virtual void Dispose([Out] bool Exception)
  {
    if (this.Disposed)
      return;
    this.MStream.Dispose();
    this.BWriter.Dispose();
    if (Exception)
      this.Handle.Dispose();
    this.Disposed = true;
  }

  public void WriteB([Out] byte[] Exception, [In] int obj1, [In] int obj2)
  {
    this.BWriter.Write(Exception, obj1, obj2);
  }

  public void WriteB(byte[] MStream) => this.BWriter.Write(MStream);

  public void WriteC(byte Disposing) => this.BWriter.Write(Disposing);

  public void WriteH(ushort Value) => this.BWriter.Write(Value);

  public void WriteH([In] short obj0) => this.BWriter.Write(obj0);

  public void WriteD([In] uint obj0) => this.BWriter.Write(obj0);

  public void WriteD(int Value) => this.BWriter.Write(Value);

  public void WriteT(float Value) => this.BWriter.Write(Value);

  public void WriteF(double Value) => this.BWriter.Write(Value);

  public void WriteQ(ulong Value) => this.BWriter.Write(Value);

  public void WriteQ(long Value) => this.BWriter.Write(Value);

  public void WriteN(string Value, [In] int obj1, [In] string obj2)
  {
    if (Value == null)
      return;
    this.WriteB(Encoding.GetEncoding(obj2).GetBytes(Value));
    this.WriteB(new byte[obj1 - Value.Length]);
  }
}
