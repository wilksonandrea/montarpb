// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Utility.CountryDetector
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Utility;

public static class CountryDetector
{
  private static readonly HttpClient httpClient_0;
  private static readonly Dictionary<string, CountryFlags> dictionary_0;

  public static byte[] ArrayRandomize([In] byte[] obj0)
  {
    if (obj0 == null || obj0.Length < 2)
      return obj0;
    byte[] destinationArray = new byte[obj0.Length];
    Array.Copy((Array) obj0, (Array) destinationArray, obj0.Length);
    Random random = new Random();
    for (int index1 = destinationArray.Length - 1; index1 > 0; --index1)
    {
      int index2 = random.Next(index1 + 1);
      ref byte local1 = ref destinationArray[index1];
      ref byte local2 = ref destinationArray[index2];
      byte num1 = destinationArray[index2];
      byte num2 = destinationArray[index1];
      local1 = num1;
      int num3 = (int) num2;
      local2 = (byte) num3;
    }
    return destinationArray;
  }

  public static int BytesToInt(byte SessionId, byte SecurityKey, byte SeedLength, [In] byte obj3)
  {
    return (int) obj3 << 24 | (int) SeedLength << 16 /*0x10*/ | (int) SecurityKey << 8 | (int) SessionId;
  }
}
