// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Network.BaseServerPacket
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using Plugin.Core.SharpDX;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace Plugin.Core.Network;

public abstract class BaseServerPacket
{
  protected MemoryStream MStream;
  protected BinaryWriter BWriter;
  protected SafeHandle Handle;
  protected bool Disposed;
  protected int SECURITY_KEY;
  protected int HASH_CODE;
  protected int SEED_LENGTH;
  protected NationsEnum NATIONS;

  public void WriteS(string Value, [In] int obj1)
  {
    if (Value == null)
      return;
    ((SyncServerPacket) this).WriteB(Encoding.UTF8.GetBytes(Value));
    ((SyncServerPacket) this).WriteB(new byte[obj1 - Value.Length]);
  }

  public void WriteU(string Name, int Count)
  {
    if (Name == null)
      return;
    ((SyncServerPacket) this).WriteB(Encoding.Unicode.GetBytes(Name));
    ((SyncServerPacket) this).WriteB(new byte[Count - Name.Length * 2]);
  }

  public void GoBack([In] int obj0)
  {
    ((SyncServerPacket) this).BWriter.BaseStream.Position -= (long) obj0;
  }

  public void WriteHV(Half3 Text)
  {
    ((SyncServerPacket) this).WriteH(Text.X.RawValue);
    ((SyncServerPacket) this).WriteH(Text.Y.RawValue);
    ((SyncServerPacket) this).WriteH(Text.Z.RawValue);
  }

  public void WriteTV([In] Half3 obj0)
  {
    ((SyncServerPacket) this).WriteT((float) obj0.X);
    ((SyncServerPacket) this).WriteT((float) obj0.Y);
    ((SyncServerPacket) this).WriteT((float) obj0.Z);
  }

  protected internal void WriteB(byte[] Text, int Count, [In] int obj2)
  {
    this.BWriter.Write(Text, Count, obj2);
  }

  protected internal void WriteB(byte[] Half) => this.BWriter.Write(Half);

  protected internal void WriteC(byte Half) => this.BWriter.Write(Half);

  protected internal void WriteH(ushort Value) => this.BWriter.Write(Value);

  protected internal void WriteH([In] short obj0) => this.BWriter.Write(obj0);

  protected internal void WriteD([In] uint obj0) => this.BWriter.Write(obj0);

  protected internal void WriteD(int Value) => this.BWriter.Write(Value);

  protected internal void WriteT(float Value) => this.BWriter.Write(Value);

  protected internal void WriteF(double Value) => this.BWriter.Write(Value);
}
