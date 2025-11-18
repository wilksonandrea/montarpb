// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Utility.Bitwise
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Security;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.XML;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

#nullable disable
namespace Plugin.Core.Utility;

public static class Bitwise
{
  private static readonly string string_0 = string.Format("\n");
  private static readonly string string_1 = string.Format("");
  private static readonly char[] char_0 = new char[256 /*0x0100*/];
  private static readonly string[] string_2 = new string[256 /*0x0100*/];
  private static readonly string[] string_3 = new string[16 /*0x10*/];
  private static readonly string[] string_4 = new string[16 /*0x10*/];
  public static readonly int[] CRYPTO = new int[3]
  {
    29890,
    32759,
    1360
  };

  private static void smethod_0([In] string obj0)
  {
    XmlDocument xmlDocument = new XmlDocument();
    using (FileStream inStream = new FileStream(obj0, FileMode.Open))
    {
      if (inStream.Length == 0L)
      {
        // ISSUE: reference to a compiler-generated method
        CLogger.Class1.Print("File is empty: " + obj0, LoggerType.Warning, (Exception) null);
      }
      else
      {
        try
        {
          xmlDocument.Load((Stream) inStream);
          for (XmlNode xmlNode1 = xmlDocument.FirstChild; xmlNode1 != null; xmlNode1 = xmlNode1.NextSibling)
          {
            if ("List".Equals(xmlNode1.Name))
            {
              for (XmlNode xmlNode2 = xmlNode1.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
              {
                if ("Title".Equals(xmlNode2.Name))
                {
                  XmlNamedNodeMap attributes = (XmlNamedNodeMap) xmlNode2.Attributes;
                  TRuleModel truleModel = new TRuleModel(int.Parse(attributes.GetNamedItem("Id").Value));
                  ((TitleModel) truleModel).ClassId = int.Parse(attributes.GetNamedItem("List").Value);
                  ((TitleModel) truleModel).Ribbon = int.Parse(attributes.GetNamedItem("Ribbon").Value);
                  ((TitleModel) truleModel).Ensign = int.Parse(attributes.GetNamedItem("Ensign").Value);
                  ((TitleModel) truleModel).Medal = int.Parse(attributes.GetNamedItem("Medal").Value);
                  ((TitleModel) truleModel).MasterMedal = int.Parse(attributes.GetNamedItem("MasterMedal").Value);
                  ((TitleModel) truleModel).Rank = int.Parse(attributes.GetNamedItem("Rank").Value);
                  ((TitleModel) truleModel).Slot = int.Parse(attributes.GetNamedItem("Slot").Value);
                  ((TitleModel) truleModel).Req1 = int.Parse(attributes.GetNamedItem("ReqT1").Value);
                  truleModel.set_Req2(int.Parse(attributes.GetNamedItem("ReqT2").Value));
                  TitleModel titleModel = (TitleModel) truleModel;
                  TitleSystemXML.list_0.Add(titleModel);
                }
              }
            }
          }
        }
        catch (XmlException ex)
        {
          // ISSUE: reference to a compiler-generated method
          CLogger.Class1.Print(ex.Message, LoggerType.Error, (Exception) ex);
        }
      }
      inStream.Dispose();
      inStream.Close();
    }
  }

  public Bitwise()
  {
  }

  static Bitwise() => TitleSystemXML.list_0 = new List<TitleModel>();

  public Bitwise()
  {
  }

  static Bitwise()
  {
    ColorUtil.White = Color.FromArgb((int) byte.MaxValue, ColorTranslator.FromHtml("#FFFFFF"));
    ColorUtil.Black = Color.FromArgb((int) byte.MaxValue, ColorTranslator.FromHtml("#000000"));
    ColorUtil.Red = Color.FromArgb((int) byte.MaxValue, ColorTranslator.FromHtml("#FF0000"));
    ColorUtil.Green = Color.FromArgb((int) byte.MaxValue, ColorTranslator.FromHtml("#00FF00"));
    ColorUtil.Blue = Color.FromArgb((int) byte.MaxValue, ColorTranslator.FromHtml("#0000FF"));
    ColorUtil.Yellow = Color.FromArgb((int) byte.MaxValue, ColorTranslator.FromHtml("#FFFF00"));
    ColorUtil.Fuchsia = Color.FromArgb((int) byte.MaxValue, ColorTranslator.FromHtml("#FF00FF"));
    ColorUtil.Cyan = Color.FromArgb((int) byte.MaxValue, ColorTranslator.FromHtml("#00FFFF"));
    ColorUtil.Silver = Color.FromArgb((int) byte.MaxValue, ColorTranslator.FromHtml("#C0C0C0"));
    ColorUtil.LightGrey = Color.FromArgb((int) byte.MaxValue, ColorTranslator.FromHtml("#D3D3D3"));
  }

  static Bitwise()
  {
    for (int index = 0; index < 10; ++index)
    {
      StringBuilder stringBuilder = new StringBuilder(3);
      stringBuilder.Append(" 0");
      stringBuilder.Append(index);
      Bitwise.string_2[index] = stringBuilder.ToString().ToUpper();
    }
    for (int index = 10; index < 16 /*0x10*/; ++index)
    {
      StringBuilder stringBuilder = new StringBuilder(3);
      stringBuilder.Append(" 0");
      stringBuilder.Append((char) (97 + index - 10));
      Bitwise.string_2[index] = stringBuilder.ToString().ToUpper();
    }
    for (int index = 16 /*0x10*/; index < Bitwise.string_2.Length; ++index)
    {
      StringBuilder stringBuilder = new StringBuilder(3);
      stringBuilder.Append(' ');
      stringBuilder.Append(index.ToString("X"));
      Bitwise.string_2[index] = stringBuilder.ToString().ToUpper();
    }
    for (int index1 = 0; index1 < Bitwise.string_3.Length; ++index1)
    {
      int num = Bitwise.string_3.Length - index1;
      StringBuilder stringBuilder = new StringBuilder(num * 3);
      for (int index2 = 0; index2 < num; ++index2)
        stringBuilder.Append("   ");
      Bitwise.string_3[index1] = stringBuilder.ToString().ToUpper();
    }
    for (int index3 = 0; index3 < Bitwise.string_4.Length; ++index3)
    {
      int capacity = Bitwise.string_4.Length - index3;
      StringBuilder stringBuilder = new StringBuilder(capacity);
      for (int index4 = 0; index4 < capacity; ++index4)
        stringBuilder.Append(' ');
      Bitwise.string_4[index3] = stringBuilder.ToString().ToUpper();
    }
    for (int index = 0; index < Bitwise.char_0.Length; ++index)
      Bitwise.char_0[index] = index <= 31 /*0x1F*/ || index >= (int) sbyte.MaxValue ? '.' : (char) index;
  }

  public static byte[] Decrypt([In] byte[] obj0, [In] int obj1)
  {
    byte[] destinationArray = new byte[obj0.Length];
    Array.Copy((Array) obj0, 0, (Array) destinationArray, 0, destinationArray.Length);
    byte num = destinationArray[destinationArray.Length - 1];
    for (int index = destinationArray.Length - 1; index > 0; --index)
      destinationArray[index] = (byte) (((int) destinationArray[index - 1] & (int) byte.MaxValue) << 8 - obj1 | ((int) destinationArray[index] & (int) byte.MaxValue) >> obj1);
    destinationArray[0] = (byte) ((int) num << 8 - obj1 | ((int) destinationArray[0] & (int) byte.MaxValue) >> obj1);
    return destinationArray;
  }

  public static byte[] Encrypt([In] byte[] obj0, [In] int obj1)
  {
    byte[] destinationArray = new byte[obj0.Length];
    Array.Copy((Array) obj0, 0, (Array) destinationArray, 0, destinationArray.Length);
    byte num = destinationArray[0];
    for (int index = 0; index < destinationArray.Length - 1; ++index)
      destinationArray[index] = (byte) (((int) destinationArray[index + 1] & (int) byte.MaxValue) >> 8 - obj1 | ((int) destinationArray[index] & (int) byte.MaxValue) << obj1);
    destinationArray[destinationArray.Length - 1] = (byte) ((int) num >> 8 - obj1 | ((int) destinationArray[destinationArray.Length - 1] & (int) byte.MaxValue) << obj1);
    return destinationArray;
  }

  public static string ToHexData(string string_0, [In] byte[] obj1)
  {
    int length1 = obj1.Length;
    int num1 = 0;
    int length2 = obj1.Length;
    StringBuilder stringBuilder = new StringBuilder((length1 / 16 /*0x10*/ + (length1 % 15 != 0 ? 1 : 0) + 4) * 80 /*0x50*/ + string_0.Length + 16 /*0x10*/);
    stringBuilder.Append(Bitwise.string_1 + "+--------+-------------------------------------------------+----------------+");
    stringBuilder.Append($"{Bitwise.string_0}[!] {string_0}; Length: [{obj1.Length} Bytes] </>");
    stringBuilder.Append(Bitwise.string_0 + "         +-------------------------------------------------+");
    stringBuilder.Append(Bitwise.string_0 + "         |  0  1  2  3  4  5  6  7  8  9  A  B  C  D  E  F |");
    stringBuilder.Append(Bitwise.string_0 + "+--------+-------------------------------------------------+----------------+");
    int index1;
    for (index1 = 0; index1 < length2; ++index1)
    {
      int num2 = index1 - num1;
      int num3 = num2 & 15;
      if (num3 == 0)
      {
        stringBuilder.Append(Bitwise.string_0);
        stringBuilder.Append(((long) num2 & (long) uint.MaxValue | 4294967296L /*0x0100000000*/).ToString("X"));
        stringBuilder[stringBuilder.Length - 9] = '|';
        stringBuilder.Append('|');
      }
      stringBuilder.Append(Bitwise.string_2[(int) obj1[index1]]);
      if (num3 == 15)
      {
        stringBuilder.Append(" |");
        for (int index2 = index1 - 15; index2 <= index1; ++index2)
          stringBuilder.Append(Bitwise.char_0[(int) obj1[index2]]);
        stringBuilder.Append('|');
      }
    }
    if ((index1 - num1 & 15) != 0)
    {
      int index3 = length1 & 15;
      stringBuilder.Append(Bitwise.string_3[index3]);
      stringBuilder.Append(" |");
      for (int index4 = index1 - index3; index4 < index1; ++index4)
        stringBuilder.Append(Bitwise.char_0[(int) obj1[index4]]);
      stringBuilder.Append(Bitwise.string_4[index3]);
      stringBuilder.Append('|');
    }
    stringBuilder.Append(Bitwise.string_0 + "+--------+-------------------------------------------------+----------------+");
    return stringBuilder.ToString();
  }

  public static string HexArrayToString([In] byte[] obj0, int Shift)
  {
    string str = "";
    try
    {
      str = Encoding.Unicode.GetString(obj0, 0, Shift);
      int length = str.IndexOf(char.MinValue);
      if (length != -1)
        str = str.Substring(0, length);
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return str;
  }

  public static byte[] HexStringToByteArray([In] string obj0)
  {
    string source = obj0.Replace(":", "").Replace("-", "").Replace(" ", "");
    byte[] byteArray = new byte[source.Length / 2];
    for (int index = 0; index < source.Length; index += 2)
      byteArray[index / 2] = (byte) (Bitwise.smethod_2(source.ElementAt<char>(index)) << 4 | Bitwise.smethod_2(source.ElementAt<char>(index + 1)));
    return byteArray;
  }

  private static string smethod_0(byte[] EventName)
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (int num in EventName)
      stringBuilder.Append(num.ToString("x2"));
    return stringBuilder.ToString();
  }

  private static byte[] smethod_1([In] string obj0)
  {
    using (SHA256 shA256 = SHA256.Create())
      return shA256.ComputeHash(Encoding.UTF8.GetBytes(obj0));
  }

  private static int smethod_2(char Buffer)
  {
    if (Buffer >= '0' && Buffer <= '9')
      return (int) Buffer - 48 /*0x30*/;
    if (Buffer >= 'A' && Buffer <= 'F')
      return (int) Buffer - 65 + 10;
    return Buffer >= 'a' && Buffer <= 'f' ? (int) Buffer - 97 + 10 : 0;
  }

  public static string ToByteString([In] byte[] obj0)
  {
    string byteString = "";
    string str1 = BitConverter.ToString(obj0);
    char[] chArray = new char[5]{ '-', ',', '.', ':', '\t' };
    foreach (string str2 in str1.Split(chArray))
      byteString = $"{byteString} {str2}";
    return byteString;
  }

  public static string GenerateRandomPassword(string HexString, [In] int obj1, [In] string obj2)
  {
    using (RNGCryptoServiceProvider cryptoServiceProvider = new RNGCryptoServiceProvider())
    {
      byte[] data = new byte[obj1];
      cryptoServiceProvider.GetBytes(data);
      char[] chArray = new char[obj1];
      for (int index = 0; index < obj1; ++index)
        chArray[index] = HexString[(int) data[index] % HexString.Length];
      return Bitwise.HashString(new string(chArray), obj2, obj1);
    }
  }

  public static string HashString(string char_1, [In] string obj1, [In] int obj2)
  {
    using (HMACMD5 hmacmD5 = new HMACMD5(Encoding.UTF8.GetBytes(obj1)))
      return Bitwise.smethod_0(hmacmD5.ComputeHash(Encoding.UTF8.GetBytes(char_1))).Substring(0, obj2);
  }

  public static List<byte[]> GenerateRSAKeyPair([In] int obj0, int Length, int Salt)
  {
    List<byte[]> rsaKeyPair = new List<byte[]>();
    RsaKeyPairGenerator keyPairGenerator = new RsaKeyPairGenerator();
    keyPairGenerator.Init(new KeyGenerationParameters(new SecureRandom((IRandomGenerator) new CryptoApiRandomGenerator()), Salt));
    RsaKeyParameters rsaKeyParameters = (RsaKeyParameters) keyPairGenerator.GenerateKeyPair().Public;
    rsaKeyPair.Add(rsaKeyParameters.Modulus.ToByteArrayUnsigned());
    rsaKeyPair.Add(rsaKeyParameters.Exponent.ToByteArrayUnsigned());
    byte[] bytes = BitConverter.GetBytes(obj0 + Length);
    Array.Copy((Array) bytes, 0, (Array) rsaKeyPair[0], 0, Math.Min(bytes.Length, rsaKeyPair[0].Length));
    return rsaKeyPair;
  }

  public static ushort GenerateRandomUShort()
  {
    using (RNGCryptoServiceProvider cryptoServiceProvider = new RNGCryptoServiceProvider())
    {
      byte[] data = new byte[2];
      cryptoServiceProvider.GetBytes(data);
      return BitConverter.ToUInt16(data, 0);
    }
  }
}
