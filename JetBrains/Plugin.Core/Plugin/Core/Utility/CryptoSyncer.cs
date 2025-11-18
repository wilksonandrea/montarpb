// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Utility.CryptoSyncer
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text.Json;

#nullable disable
namespace Plugin.Core.Utility;

public static class CryptoSyncer
{
  private static readonly byte[] byte_0 = Bitwise.HexStringToByteArray("BC 34 88 F9 C3 00 B1 64 37 B0 6C B3 EE F3 33 3A");
  private static readonly byte[] byte_1 = Bitwise.HexStringToByteArray("71 F0 D9 9E 15 47 9A BA 72 C3 4F F8 04 27 D8 0A");

  public static bool ProcessPacket(byte[] Source, int Byte2, int Byte3, [In] ushort[] obj3)
  {
    // ISSUE: variable of a compiler-generated type
    Bitwise.Class4 class4 = (Bitwise.Class4) new CryptoSyncer();
    // ISSUE: reference to a compiler-generated field
    class4.ushort_0 = BitConverter.ToUInt16(Source, 2);
    if (((IEnumerable<ushort>) obj3).FirstOrDefault<ushort>(new Func<ushort, bool>(((CryptoSyncer) class4).method_0)) != (ushort) 0)
      return false;
    int count = Source.Length - Byte2 - Byte3;
    if (count < 0)
      count = 0;
    int num = Byte2 + Byte3;
    if (Source.Length < num)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("PacketData is too short to apply encryption logic.", LoggerType.Warning, (Exception) null);
      return false;
    }
    byte[] sourceArray = CryptoSyncer.Encrypt(((IEnumerable<byte>) Source).Skip<byte>(Byte2).Take<byte>(count).ToArray<byte>());
    if (sourceArray.Length != count)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("Encrypted data length mismatch! Encryption function changed data size.", LoggerType.Warning, (Exception) null);
      return false;
    }
    Array.Copy((Array) sourceArray, 0, (Array) Source, Byte2, sourceArray.Length);
    return true;
  }

  internal bool method_0([In] ushort obj0) => (int) obj0 == (int) ((Bitwise.Class4) this).ushort_0;

  static CryptoSyncer()
  {
    CountryDetector.httpClient_0 = new HttpClient();
    CountryDetector.dictionary_0 = new Dictionary<string, CountryFlags>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
    {
      {
        "Peru",
        CountryFlags.Peru
      },
      {
        "Venezuela",
        CountryFlags.Venezuela
      },
      {
        "Bolivia",
        CountryFlags.Bolivia
      },
      {
        "Ecuador",
        CountryFlags.Ecuador
      },
      {
        "United States",
        CountryFlags.US
      },
      {
        "Brazil",
        CountryFlags.Brazil
      },
      {
        "Argentina",
        CountryFlags.Argentina
      },
      {
        "Chile",
        CountryFlags.Chile
      },
      {
        "Colombia",
        CountryFlags.Colombia
      },
      {
        "Spain",
        CountryFlags.Spain
      },
      {
        "Mexico",
        CountryFlags.Mexico
      },
      {
        "Sweden",
        CountryFlags.Sweden
      },
      {
        "Indonesia",
        CountryFlags.Indonesia
      },
      {
        "Kazakhstan",
        CountryFlags.Kazakhstan
      }
    };
  }

  public static CountryFlags GetCountryByIp(string PacketData)
  {
    if (string.IsNullOrEmpty(PacketData))
      return CountryFlags.None;
    if (PacketData == "127.0.0.1")
      return CountryFlags.None;
    try
    {
      string requestUri = $"http://ip-api.com/json/{PacketData}?fields=status,country";
      using (JsonDocument jsonDocument = JsonDocument.Parse(CountryDetector.httpClient_0.GetStringAsync(requestUri).Result))
      {
        JsonElement rootElement = jsonDocument.RootElement;
        JsonElement jsonElement1;
        if (rootElement.TryGetProperty("status", out jsonElement1))
        {
          if (jsonElement1.GetString() == "success")
          {
            JsonElement jsonElement2;
            if (rootElement.TryGetProperty("country", out jsonElement2))
            {
              string key = jsonElement2.GetString();
              CountryFlags countryByIp;
              if (CountryDetector.dictionary_0.TryGetValue(key, out countryByIp))
                return countryByIp;
              Console.WriteLine($"[CountryDetector] Connected: '{key}' IP: {PacketData}");
              return CountryFlags.None;
            }
          }
        }
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"[CountryDetector] Error processing IP {PacketData}: {ex.Message}");
    }
    return CountryFlags.None;
  }

  static CryptoSyncer()
  {
  }

  private static byte[] smethod_0([In] byte[] obj0)
  {
    using (Aes aes = Aes.Create())
    {
      aes.Mode = CipherMode.ECB;
      aes.Padding = PaddingMode.None;
      aes.Key = CryptoSyncer.byte_0;
      using (ICryptoTransform encryptor = aes.CreateEncryptor())
      {
        byte[] inputBuffer = (byte[]) CryptoSyncer.byte_1.Clone();
        byte[] numArray1 = new byte[obj0.Length];
        for (int index1 = 0; index1 < obj0.Length; index1 += 16 /*0x10*/)
        {
          byte[] numArray2 = encryptor.TransformFinalBlock(inputBuffer, 0, 16 /*0x10*/);
          int num = Math.Min(16 /*0x10*/, obj0.Length - index1);
          for (int index2 = 0; index2 < num; ++index2)
            numArray1[index1 + index2] = (byte) ((uint) obj0[index1 + index2] ^ (uint) numArray2[index2]);
          int index3 = 15;
          while (index3 >= 0 && ++inputBuffer[index3] == (byte) 0)
            --index3;
        }
        return numArray1;
      }
    }
  }

  public static byte[] Encrypt([In] byte[] obj0) => CryptoSyncer.smethod_0(obj0);

  public static byte[] Decrypt([In] byte[] obj0) => CryptoSyncer.smethod_0(obj0);
}
