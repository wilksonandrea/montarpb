using Npgsql;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.RAW;
using Plugin.Core.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Plugin.Core.Utility
{

    public static class ComDiv
    {
        public static bool firewallsystem = false;
        static string API = "https://cdn.deluxe-pb.com/pblauncher/launcher/";
        public static void GuardFirewall(string method, string ip, string why)
        {
            if (firewallsystem == true)
            {

                if (why != "userexit.")
                {
                    Console.WriteLine("[FW-API] " + ip + " removed from firewall. WHY? : " + why);
                }
                try
                {
                    PerformRequestFirewall(API + "Firewall.php", method, ip);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("GoldGuard Güvenlik duvarında sorun! : " + ex.ToString());
                }
            }
            else
            {
                CLogger.Print("[FW STATUS] Firewall system is disabled.", LoggerType.Warning);
            }
        }


        public static string PerformRequestFirewall(string url, string method, string ip)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Proxy = new WebProxy();
                request.UserAgent = "GoldGuard";

                string postData = "value=" + Uri.EscapeDataString(method) + "&userip=" + Uri.EscapeDataString(ip);
                byte[] data = Encoding.UTF8.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
                request.ContentLength = data.Length;

                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                using (WebResponse response = request.GetResponse())
                {
                    if (response == null)
                    {
                        Console.WriteLine("PerformRequest-guard api null response geldi.");
                    }
                    return new StreamReader(response.GetResponseStream()).ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("php api PerformRequestFirewall error - : " + ex.ToString() + " method : " + url);
                return null;
            }
        }


        public static void ConfigureServicePointManager()
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls |
                                                       (SecurityProtocolType)768 |
                                                       (SecurityProtocolType)3072 |
                                                       SecurityProtocolType.Ssl3;
            }
            catch
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
            }
            ServicePointManager.ServerCertificateValidationCallback = delegate
            {
                return true;
            };
            ServicePointManager.Expect100Continue = true;
        }

        // Bu metodu ComDiv sınıfına ekleyin
        public static uint Verificate(byte A, byte B, byte C, byte D)
        {
            byte[] source = new byte[4] { A, B, C, D };

            // Log para debugging
            Console.WriteLine($"Verificate called with: A={A}, B={B}, C={C}, D={D}");

            // Si todos son cero, usar valores por defecto
            if (A == 0 && B == 0 && C == 0 && D == 0)
            {
                Console.WriteLine("All bytes are zero, using default values");
                source = new byte[4] { 0, 60, 0, 1 }; // Valores por defecto válidos
                return BitConverter.ToUInt32(source, 0);
            }

            // Verificación más permisiva para el refresh rate
            if (B >= 30) // Reducir de 60 a 30 Hz temporalmente
            {
                if (C >= 0 && C <= 1)
                    return BitConverter.ToUInt32(source, 0);

                Console.WriteLine($"Invalid window state: {C}, accepting anyway");
                return BitConverter.ToUInt32(source, 0); // Aceptar de todas formas
            }

            Console.WriteLine($"Refresh rate too low: {B}, using minimum");
            source[1] = 60; // Forzar 60 Hz
            return BitConverter.ToUInt32(source, 0);
        }

        //public static int CheckEquipedItems(PlayerEquipment Equip, List<ItemsModel> Inventory, bool BattleRules)
        //{
        //    int ValidationType = 0;
        //    (bool, bool, bool, bool, bool) WeaponStatus = (false, false, false, false, false);
        //    (bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool) CharacterStatus = (false, false, false, false, false, false, false, false, false, false, false, false);
        //    (bool, bool, bool) NewItemStatus = (false, false, false);
        //    if (Equip.BeretItem == 0)
        //    {
        //        CharacterStatus.Item11 = true;
        //    }
        //    if (Equip.AccessoryId == 0)
        //    {
        //        NewItemStatus.Item1 = true;
        //    }
        //    if (Equip.SprayId == 0)
        //    {
        //        NewItemStatus.Item2 = true;
        //    }
        //    if (Equip.NameCardId == 0)
        //    {
        //        NewItemStatus.Item3 = true;
        //    }
        //    if (Equip.WeaponPrimary == 103004)
        //    {
        //        WeaponStatus.Item1 = true;
        //    }
        //    if (BattleRules)
        //    {
        //        if (!WeaponStatus.Item1 && (Equip.WeaponPrimary == 105025 || Equip.WeaponPrimary == 106007))
        //        {
        //            WeaponStatus.Item1 = true;
        //        }
        //        if (!WeaponStatus.Item3 && Equip.WeaponMelee == 323001)
        //        {
        //            WeaponStatus.Item3 = true;
        //        }
        //    }
        //    (WeaponStatus, CharacterStatus, NewItemStatus) = ValidateItemsFromInventory(Equip, Inventory, WeaponStatus, CharacterStatus, NewItemStatus);
        //    bool HasInvalidWeapon = !WeaponStatus.Item1 || !WeaponStatus.Item2 || !WeaponStatus.Item3 || !WeaponStatus.Item4 || !WeaponStatus.Item5;
        //    bool HasInvalidCharacter = !CharacterStatus.Item1 || !CharacterStatus.Item2 || !CharacterStatus.Item3 || !CharacterStatus.Item4 || !CharacterStatus.Item5 || !CharacterStatus.Item6 || !CharacterStatus.Item7 || !CharacterStatus.Item8 || !CharacterStatus.Item9 || !CharacterStatus.Item10 || !CharacterStatus.Item11 || !CharacterStatus.Item12;
        //    bool HasInvalidNewItem = !NewItemStatus.Item1 || !NewItemStatus.Item2 || !NewItemStatus.Item3;
        //    if (HasInvalidWeapon)
        //    {
        //        ValidationType += 2;
        //    }
        //    if (HasInvalidCharacter)
        //    {
        //        ValidationType += 1;
        //    }
        //    if (HasInvalidNewItem)
        //    {
        //        ValidationType += 3;
        //    }
        //    SetDefaultEquipment(Equip, ref WeaponStatus, ref CharacterStatus, ref NewItemStatus);
        //    return ValidationType;
        //}
        //private static ((bool, bool, bool, bool, bool), (bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool), (bool, bool, bool)) ValidateItemsFromInventory(PlayerEquipment Equip, List<ItemsModel> Inventory, (bool, bool, bool, bool, bool) WeaponStatus, (bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool) CharacterStatus, (bool, bool, bool) NewItemStatus)
        //{
        //    lock (Inventory)
        //    {
        //        HashSet<int> InventoryItemIds = new HashSet<int>(Inventory.Where(Item => Item.Count > 0).Select(Item => Item.Id));
        //        if (InventoryItemIds.Contains(Equip.WeaponPrimary))
        //        {
        //            WeaponStatus.Item1 = true;
        //        }
        //        if (InventoryItemIds.Contains(Equip.WeaponSecondary))
        //        {
        //            WeaponStatus.Item2 = true;
        //        }
        //        if (InventoryItemIds.Contains(Equip.WeaponMelee))
        //        {
        //            WeaponStatus.Item3 = true;
        //        }
        //        if (InventoryItemIds.Contains(Equip.WeaponExplosive))
        //        {
        //            WeaponStatus.Item4 = true;
        //        }
        //        if (InventoryItemIds.Contains(Equip.WeaponSpecial))
        //        {
        //            WeaponStatus.Item5 = true;
        //        }
        //        if (InventoryItemIds.Contains(Equip.CharaRedId))
        //        {
        //            CharacterStatus.Item1 = true;
        //        }
        //        if (InventoryItemIds.Contains(Equip.CharaBlueId))
        //        {
        //            CharacterStatus.Item2 = true;
        //        }
        //        if (InventoryItemIds.Contains(Equip.PartHead))
        //        {
        //            CharacterStatus.Item3 = true;
        //        }
        //        if (InventoryItemIds.Contains(Equip.PartFace))
        //        {
        //            CharacterStatus.Item4 = true;
        //        }
        //        if (InventoryItemIds.Contains(Equip.PartJacket))
        //        {
        //            CharacterStatus.Item5 = true;
        //        }
        //        if (InventoryItemIds.Contains(Equip.PartPocket))
        //        {
        //            CharacterStatus.Item6 = true;
        //        }
        //        if (InventoryItemIds.Contains(Equip.PartGlove))
        //        {
        //            CharacterStatus.Item7 = true;
        //        }
        //        if (InventoryItemIds.Contains(Equip.PartBelt))
        //        {
        //            CharacterStatus.Item8 = true;
        //        }
        //        if (InventoryItemIds.Contains(Equip.PartHolster))
        //        {
        //            CharacterStatus.Item9 = true;
        //        }
        //        if (InventoryItemIds.Contains(Equip.PartSkin))
        //        {
        //            CharacterStatus.Item10 = true;
        //        }
        //        if (Equip.BeretItem != 0 && InventoryItemIds.Contains(Equip.BeretItem))
        //        {
        //            CharacterStatus.Item11 = true;
        //        }
        //        if (InventoryItemIds.Contains(Equip.DinoItem))
        //        {
        //            CharacterStatus.Item12 = true;
        //        }
        //        if (Equip.AccessoryId != 0 && InventoryItemIds.Contains(Equip.AccessoryId))
        //        {
        //            NewItemStatus.Item1 = true;
        //        }
        //        if (Equip.SprayId != 0 && InventoryItemIds.Contains(Equip.SprayId))
        //        {
        //            NewItemStatus.Item2 = true;
        //        }
        //        if (Equip.NameCardId != 0 && InventoryItemIds.Contains(Equip.NameCardId))
        //        {
        //            NewItemStatus.Item3 = true;
        //        }
        //    }
        //    return (WeaponStatus, CharacterStatus, NewItemStatus);
        //}
        //private static void SetDefaultEquipment(PlayerEquipment Equip, ref (bool, bool, bool, bool, bool) WeaponStatus, ref (bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool) CharacterStatus, ref (bool, bool, bool) NewItemStatus)
        //{
        //    if (!WeaponStatus.Item1)
        //    {
        //        Equip.WeaponPrimary = 103004;
        //    }
        //    if (!WeaponStatus.Item2)
        //    {
        //        Equip.WeaponSecondary = 202003;
        //    }
        //    if (!WeaponStatus.Item3)
        //    {
        //        Equip.WeaponMelee = 301001;
        //    }
        //    if (!WeaponStatus.Item4)
        //    {
        //        Equip.WeaponExplosive = 407001;
        //    }
        //    if (!WeaponStatus.Item5)
        //    {
        //        Equip.WeaponSpecial = 508001;
        //    }
        //    if (!CharacterStatus.Item1)
        //    {
        //        Equip.CharaRedId = 601001;
        //    }
        //    if (!CharacterStatus.Item2)
        //    {
        //        Equip.CharaBlueId = 602002;
        //    }
        //    if (!CharacterStatus.Item3)
        //    {
        //        Equip.PartHead = 1000700000;
        //    }
        //    if (!CharacterStatus.Item4)
        //    {
        //        Equip.PartFace = 1000800000;
        //    }
        //    if (!CharacterStatus.Item5)
        //    {
        //        Equip.PartJacket = 1000900000;
        //    }
        //    if (!CharacterStatus.Item6)
        //    {
        //        Equip.PartPocket = 1001000000;
        //    }
        //    if (!CharacterStatus.Item7)
        //    {
        //        Equip.PartGlove = 1001100000;
        //    }
        //    if (!CharacterStatus.Item8)
        //    {
        //        Equip.PartBelt = 1001200000;
        //    }
        //    if (!CharacterStatus.Item9)
        //    {
        //        Equip.PartHolster = 1001300000;
        //    }
        //    if (!CharacterStatus.Item10)
        //    {
        //        Equip.PartSkin = 1001400000;
        //    }
        //    if (!CharacterStatus.Item11)
        //    {
        //        Equip.BeretItem = 0;
        //    }
        //    if (!CharacterStatus.Item12)
        //    {
        //        Equip.DinoItem = 1500511;
        //    }
        //    if (!NewItemStatus.Item1)
        //    {
        //        Equip.AccessoryId = 0;
        //    }
        //    if (!NewItemStatus.Item2)
        //    {
        //        Equip.SprayId = 0;
        //    }
        //    if (!NewItemStatus.Item3)
        //    {
        //        Equip.NameCardId = 0;
        //    }
        //    FixInvalidHeadFace(Equip);
        //}
        //public static void FixInvalidHeadFace(PlayerEquipment Equip)
        //{
        //    bool isHeadSpecial = Equip.PartHead == 1000700000;
        //    bool isFaceSpecial = Equip.PartFace == 1000800000;
        //    bool isHeadEmpty = Equip.PartHead == 0;
        //    bool isFaceEmpty = Equip.PartFace == 0;

        //    // Kural 1: ikisi özelse dokunma
        //    if (isHeadSpecial && isFaceSpecial)
        //        return;

        //    // Kural 2: Head özel ama Face özel değilse → Head sıfırlanır
        //    if (isHeadSpecial && !isFaceSpecial)
        //    {
        //        Equip.PartHead = 0;
        //        return;
        //    }

        //    // Kural 3: Face özel ama Head özel değilse → Face sıfırlanır
        //    if (isFaceSpecial && !isHeadSpecial)
        //    {
        //        Equip.PartFace = 0;
        //        return;
        //    }

        //    // Kural 4: ikisi de özel değil ve ikisi de doluysa → Head sıfırlanır (senin isteğine göre)
        //    if (!isHeadEmpty && !isFaceEmpty)
        //    {
        //        Equip.PartHead = 0;
        //        return;
        //    }

        //    // Kural 5: ikisi de boşsa → Head varsayılan atanır
        //    if (isHeadEmpty && isFaceEmpty)
        //    {
        //        Equip.PartHead = 1000700000;
        //    }
        //}
        // ---- ComDiv.CheckEquipedItems (bit mask fix + diagnostics) ----
        //public static int CheckEquipedItems(
        //    PlayerEquipment Equip,
        //    List<ItemsModel> Inventory,
        //    bool BattleRules,
        //    bool diagnostics = false)
        //{
        //    int ValidationType = 0;

        //    (bool, bool, bool, bool, bool) WeaponStatus = (false, false, false, false, false);
        //    (bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool) CharacterStatus =
        //        (false, false, false, false, false, false, false, false, false, false, false, false);
        //    (bool, bool, bool) NewItemStatus = (false, false, false);

        //    if (Equip.BeretItem == 0) CharacterStatus.Item11 = true;
        //    if (Equip.AccessoryId == 0) NewItemStatus.Item1 = true;
        //    if (Equip.SprayId == 0) NewItemStatus.Item2 = true;
        //    if (Equip.NameCardId == 0) NewItemStatus.Item3 = true;

        //    // Varsayılanlar (battle rules istisnaları)
        //    if (Equip.WeaponPrimary == 103004) WeaponStatus.Item1 = true;
        //    if (BattleRules)
        //    {
        //        if (!WeaponStatus.Item1 && (Equip.WeaponPrimary == 105025 || Equip.WeaponPrimary == 106007))
        //            WeaponStatus.Item1 = true;
        //        if (!WeaponStatus.Item3 && Equip.WeaponMelee == 323001)
        //            WeaponStatus.Item3 = true;
        //    }

        //    (WeaponStatus, CharacterStatus, NewItemStatus) =
        //        ValidateItemsFromInventory(Equip, Inventory, WeaponStatus, CharacterStatus, NewItemStatus);

        //    bool HasInvalidWeapon = !WeaponStatus.Item1 || !WeaponStatus.Item2 || !WeaponStatus.Item3 || !WeaponStatus.Item4 || !WeaponStatus.Item5;
        //    bool HasInvalidCharacter = !CharacterStatus.Item1 || !CharacterStatus.Item2 || !CharacterStatus.Item3 || !CharacterStatus.Item4 ||
        //                               !CharacterStatus.Item5 || !CharacterStatus.Item6 || !CharacterStatus.Item7 || !CharacterStatus.Item8 ||
        //                               !CharacterStatus.Item9 || !CharacterStatus.Item10 || !CharacterStatus.Item11 || !CharacterStatus.Item12;
        //    bool HasInvalidNewItem = !NewItemStatus.Item1 || !NewItemStatus.Item2 || !NewItemStatus.Item3;

        //    // ---- DÜZELTME: items için 4. bit
        //    if (HasInvalidWeapon) ValidationType |= 2; // weapons
        //    if (HasInvalidCharacter) ValidationType |= 1; // chars
        //    if (HasInvalidNewItem) ValidationType |= 4; // items (ACCESSORY/SPRAY/NAMECARD)

        //    if (diagnostics)
        //    {
        //        CLogger.Print($"[CHECK] rawType={ValidationType} -> " +
        //                      $"needChars={(ValidationType & 1) != 0} " +
        //                      $"needWeapons={(ValidationType & 2) != 0} " +
        //                      $"needItems={(ValidationType & 4) != 0}", LoggerType.Debug);
        //    }

        //    SetDefaultEquipment(Equip, ref WeaponStatus, ref CharacterStatus, ref NewItemStatus);
        //    return ValidationType;
        //}

        //private static ((bool, bool, bool, bool, bool),
        //                (bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool),
        //                (bool, bool, bool))
        //ValidateItemsFromInventory(
        //    PlayerEquipment Equip,
        //    List<ItemsModel> Inventory,
        //    (bool, bool, bool, bool, bool) WeaponStatus,
        //    (bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool) CharacterStatus,
        //    (bool, bool, bool) NewItemStatus)
        //{
        //    lock (Inventory)
        //    {
        //        HashSet<int> ids = new HashSet<int>(Inventory.Where(it => it.Count > 0).Select(it => it.Id));

        //        if (ids.Contains(Equip.WeaponPrimary)) WeaponStatus.Item1 = true;
        //        if (ids.Contains(Equip.WeaponSecondary)) WeaponStatus.Item2 = true;
        //        if (ids.Contains(Equip.WeaponMelee)) WeaponStatus.Item3 = true;
        //        if (ids.Contains(Equip.WeaponExplosive)) WeaponStatus.Item4 = true;
        //        if (ids.Contains(Equip.WeaponSpecial)) WeaponStatus.Item5 = true;

        //        if (ids.Contains(Equip.CharaRedId)) CharacterStatus.Item1 = true;
        //        if (ids.Contains(Equip.CharaBlueId)) CharacterStatus.Item2 = true;
        //        if (ids.Contains(Equip.PartHead)) CharacterStatus.Item3 = true;
        //        if (ids.Contains(Equip.PartFace)) CharacterStatus.Item4 = true;
        //        if (ids.Contains(Equip.PartJacket)) CharacterStatus.Item5 = true;
        //        if (ids.Contains(Equip.PartPocket)) CharacterStatus.Item6 = true;
        //        if (ids.Contains(Equip.PartGlove)) CharacterStatus.Item7 = true;
        //        if (ids.Contains(Equip.PartBelt)) CharacterStatus.Item8 = true;
        //        if (ids.Contains(Equip.PartHolster)) CharacterStatus.Item9 = true;
        //        if (ids.Contains(Equip.PartSkin)) CharacterStatus.Item10 = true;
        //        if (Equip.BeretItem != 0 && ids.Contains(Equip.BeretItem)) CharacterStatus.Item11 = true;
        //        if (ids.Contains(Equip.DinoItem)) CharacterStatus.Item12 = true;

        //        if (Equip.AccessoryId != 0 && ids.Contains(Equip.AccessoryId)) NewItemStatus.Item1 = true;
        //        if (Equip.SprayId != 0 && ids.Contains(Equip.SprayId)) NewItemStatus.Item2 = true;
        //        if (Equip.NameCardId != 0 && ids.Contains(Equip.NameCardId)) NewItemStatus.Item3 = true;
        //    }
        //    return (WeaponStatus, CharacterStatus, NewItemStatus);
        //}

        //private static void SetDefaultEquipment(
        //    PlayerEquipment Equip,
        //    ref (bool, bool, bool, bool, bool) WeaponStatus,
        //    ref (bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool) CharacterStatus,
        //    ref (bool, bool, bool) NewItemStatus)
        //{
        //    if (!WeaponStatus.Item1) Equip.WeaponPrimary = 103004;
        //    if (!WeaponStatus.Item2) Equip.WeaponSecondary = 202003;
        //    if (!WeaponStatus.Item3) Equip.WeaponMelee = 301001;
        //    if (!WeaponStatus.Item4) Equip.WeaponExplosive = 407001;
        //    if (!WeaponStatus.Item5) Equip.WeaponSpecial = 508001;

        //    if (!CharacterStatus.Item1) Equip.CharaRedId = 601001;
        //    if (!CharacterStatus.Item2) Equip.CharaBlueId = 602002;
        //    if (!CharacterStatus.Item3) Equip.PartHead = 1000700000;
        //    if (!CharacterStatus.Item4) Equip.PartFace = 1000800000;
        //    if (!CharacterStatus.Item5) Equip.PartJacket = 1000900000;
        //    if (!CharacterStatus.Item6) Equip.PartPocket = 1001000000;
        //    if (!CharacterStatus.Item7) Equip.PartGlove = 1001100000;
        //    if (!CharacterStatus.Item8) Equip.PartBelt = 1001200000;
        //    if (!CharacterStatus.Item9) Equip.PartHolster = 1001300000;
        //    if (!CharacterStatus.Item10) Equip.PartSkin = 1001400000;
        //    if (!CharacterStatus.Item11) Equip.BeretItem = 0;
        //    if (!CharacterStatus.Item12) Equip.DinoItem = 1500511;

        //    if (!NewItemStatus.Item1) Equip.AccessoryId = 0;
        //    if (!NewItemStatus.Item2) Equip.SprayId = 0;
        //    if (!NewItemStatus.Item3) Equip.NameCardId = 0;

        //    FixInvalidHeadFace(Equip);
        //}

        //public static void FixInvalidHeadFace(PlayerEquipment Equip)
        //{
        //    bool isHeadSpecial = Equip.PartHead == 1000700000;
        //    bool isFaceSpecial = Equip.PartFace == 1000800000;
        //    bool isHeadEmpty = Equip.PartHead == 0;
        //    bool isFaceEmpty = Equip.PartFace == 0;

        //    if (isHeadSpecial && isFaceSpecial) return;
        //    if (isHeadSpecial && !isFaceSpecial) { Equip.PartHead = 0; return; }
        //    if (isFaceSpecial && !isHeadSpecial) { Equip.PartFace = 0; return; }
        //    if (!isHeadEmpty && !isFaceEmpty) { Equip.PartHead = 0; return; }
        //    if (isHeadEmpty && isFaceEmpty) { Equip.PartHead = 1000700000; }
        //}

        // =====================
        // ComDiv.CheckEquipedItems (DIAGNOSTICS ENHANCED + items bit=4)
        // =====================
        public static int CheckEquipedItems(
            PlayerEquipment Equip,
            List<ItemsModel> Inventory,
            bool BattleRules,
            bool diagnostics = false)
        {
            int ValidationType = 0;

            (bool, bool, bool, bool, bool) WeaponStatus = (false, false, false, false, false);
            (bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool) CharacterStatus =
                (false, false, false, false, false, false, false, false, false, false, false, false);
            (bool, bool, bool) NewItemStatus = (false, false, false);

            // Beret 0 ise zaten "geçerli" say
            if (Equip.BeretItem == 0) CharacterStatus.Item11 = true;
            // Accessory/Spray/NameCard 0 ise zaten "geçerli" say
            if (Equip.AccessoryId == 0) NewItemStatus.Item1 = true;
            if (Equip.SprayId == 0) NewItemStatus.Item2 = true;
            if (Equip.NameCardId == 0) NewItemStatus.Item3 = true;

            // Varsayılan “legal” modeller
            if (Equip.WeaponPrimary == 103004) WeaponStatus.Item1 = true;
            if (BattleRules)
            {
                if (!WeaponStatus.Item1 && (Equip.WeaponPrimary == 105025 || Equip.WeaponPrimary == 106007))
                    WeaponStatus.Item1 = true;
                if (!WeaponStatus.Item3 && Equip.WeaponMelee == 323001)
                    WeaponStatus.Item3 = true;
            }

            // Envanter snapshot + hızlı lookup
            HashSet<int> ids;
            lock (Inventory)
                ids = new HashSet<int>(Inventory.Where(it => it.Count > 0).Select(it => it.Id));

            // ---- DIAGNOSTICS: Equip snapshot
            if (diagnostics)
            {
                CLogger.Print(
                    "[CHECK] EQUIP " +
                    $"Red={Equip.CharaRedId} Blue={Equip.CharaBlueId} " +
                    $"Head={Equip.PartHead} Face={Equip.PartFace} Jacket={Equip.PartJacket} Pocket={Equip.PartPocket} " +
                    $"Glove={Equip.PartGlove} Belt={Equip.PartBelt} Holster={Equip.PartHolster} Skin={Equip.PartSkin} " +
                    $"Beret={Equip.BeretItem} Dino={Equip.DinoItem} " +
                    $"Acc={Equip.AccessoryId} Spray={Equip.SprayId} NameCard={Equip.NameCardId}", LoggerType.Debug);
            }

            // Envantere göre doğrula
            (WeaponStatus, CharacterStatus, NewItemStatus) =
                ValidateItemsFromInventoryCore(Equip, ids, WeaponStatus, CharacterStatus, NewItemStatus);

            // Hangi alt parçalar eksik? (karar öncesi log)
            if (diagnostics)
                PrintWhichSubslotMissing("[CHECK] BEFORE AUTOFIX", WeaponStatus, CharacterStatus, NewItemStatus, ids, Equip);

            bool HasInvalidWeapon =
                !WeaponStatus.Item1 || !WeaponStatus.Item2 || !WeaponStatus.Item3 || !WeaponStatus.Item4 || !WeaponStatus.Item5;

            bool HasInvalidCharacter =
                !CharacterStatus.Item1 || !CharacterStatus.Item2 || !CharacterStatus.Item3 || !CharacterStatus.Item4 ||
                !CharacterStatus.Item5 || !CharacterStatus.Item6 || !CharacterStatus.Item7 || !CharacterStatus.Item8 ||
                !CharacterStatus.Item9 || !CharacterStatus.Item10 || !CharacterStatus.Item11 || !CharacterStatus.Item12;

            bool HasInvalidNewItem = !NewItemStatus.Item1 || !NewItemStatus.Item2 || !NewItemStatus.Item3;

            if (HasInvalidWeapon) ValidationType |= 2; // weapons
            if (HasInvalidCharacter) ValidationType |= 1; // chars
            if (HasInvalidNewItem) ValidationType |= 4; // items (Accessory/Spray/NameCard)  <-- DÜZELTME

            if (diagnostics)
            {
                CLogger.Print($"[CHECK] rawType={ValidationType} -> " +
                              $"needChars={(ValidationType & 1) != 0} " +
                              $"needWeapons={(ValidationType & 2) != 0} " +
                              $"needItems={(ValidationType & 4) != 0}", LoggerType.Debug);
            }

            // Auto-fix uygula (DB güncellemeyi üst kat yapıyor)
            SetDefaultEquipment(Equip, ref WeaponStatus, ref CharacterStatus, ref NewItemStatus);

            // İsteğe bağlı: autofix sonrası stateleri de göster (dışarıya TYPE değişmiyor)
            if (diagnostics)
            {
                // Fix sonrası tekrar hesaplayıp neyin düzeldiğini gösterelim
                var w2 = (false, false, false, false, false);
                var c2 = (false, false, false, false, false, false, false, false, false, false, false, false);
                var n2 = (false, false, false);

                if (Equip.BeretItem == 0) c2.Item11 = true;
                if (Equip.AccessoryId == 0) n2.Item1 = true;
                if (Equip.SprayId == 0) n2.Item2 = true;
                if (Equip.NameCardId == 0) n2.Item3 = true;
                if (Equip.WeaponPrimary == 103004) w2.Item1 = true;

                (w2, c2, n2) = ValidateItemsFromInventoryCore(Equip, ids, w2, c2, n2);
                PrintWhichSubslotMissing("[CHECK] AFTER  AUTOFIX", w2, c2, n2, ids, Equip);
            }

            return ValidationType;
        }

        private static ((bool, bool, bool, bool, bool),
                (bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool),
                (bool, bool, bool))
ValidateItemsFromInventoryCore(
    PlayerEquipment Equip,
    HashSet<int> ids,
    (bool, bool, bool, bool, bool) WeaponStatus,
    (bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool) CharacterStatus,
    (bool, bool, bool) NewItemStatus)
        {
            // ---- Weapons
            if (ids.Contains(Equip.WeaponPrimary)) WeaponStatus.Item1 = true;
            if (ids.Contains(Equip.WeaponSecondary)) WeaponStatus.Item2 = true;
            if (ids.Contains(Equip.WeaponMelee)) WeaponStatus.Item3 = true;
            if (ids.Contains(Equip.WeaponExplosive)) WeaponStatus.Item4 = true;
            if (ids.Contains(Equip.WeaponSpecial)) WeaponStatus.Item5 = true;

            // ---- Characters & Parts
            if (ids.Contains(Equip.CharaRedId)) CharacterStatus.Item1 = true;
            if (ids.Contains(Equip.CharaBlueId)) CharacterStatus.Item2 = true;

            // Head/Face: 0 ise opsiyonel → valid say
            if (Equip.PartHead == 0 || ids.Contains(Equip.PartHead)) CharacterStatus.Item3 = true;
            if (Equip.PartFace == 0 || ids.Contains(Equip.PartFace)) CharacterStatus.Item4 = true;

            // Diğer kozmetik parçalar: istersen 0'ı da opsiyonel say (boş kullanılabiliyorsa)
            if (Equip.PartJacket == 0 || ids.Contains(Equip.PartJacket)) CharacterStatus.Item5 = true;
            if (Equip.PartPocket == 0 || ids.Contains(Equip.PartPocket)) CharacterStatus.Item6 = true;
            if (Equip.PartGlove == 0 || ids.Contains(Equip.PartGlove)) CharacterStatus.Item7 = true;
            if (Equip.PartBelt == 0 || ids.Contains(Equip.PartBelt)) CharacterStatus.Item8 = true;
            if (Equip.PartHolster == 0 || ids.Contains(Equip.PartHolster)) CharacterStatus.Item9 = true;
            if (Equip.PartSkin == 0 || ids.Contains(Equip.PartSkin)) CharacterStatus.Item10 = true;

            // Beret 0 zaten opsiyonel (önceden de böyleydi)
            if (Equip.BeretItem == 0 || ids.Contains(Equip.BeretItem)) CharacterStatus.Item11 = true;

            // Dino zorunlu değil ama 0 olmaz; mevcutsa doğrula
            if (ids.Contains(Equip.DinoItem)) CharacterStatus.Item12 = true;

            // ---- New Items (0 = opsiyonel)
            if (Equip.AccessoryId == 0 || ids.Contains(Equip.AccessoryId)) NewItemStatus.Item1 = true;
            if (Equip.SprayId == 0 || ids.Contains(Equip.SprayId)) NewItemStatus.Item2 = true;
            if (Equip.NameCardId == 0 || ids.Contains(Equip.NameCardId)) NewItemStatus.Item3 = true;

            return (WeaponStatus, CharacterStatus, NewItemStatus);
        }

        // Log helper’ı da 0 için "n/a" göstersin:
        private static void PrintWhichSubslotMissing(
            string tag,
            (bool, bool, bool, bool, bool) W,
            (bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool) C,
            (bool, bool, bool) N,
            HashSet<int> ids,
            PlayerEquipment e)
        {
            List<string> miss = new List<string>();
            string has(int id) => id == 0 ? "n/a" : ids.Contains(id).ToString();

            if (!W.Item1) miss.Add($"W.Primary({e.WeaponPrimary},has={has(e.WeaponPrimary)})");
            if (!W.Item2) miss.Add($"W.Secondary({e.WeaponSecondary},has={has(e.WeaponSecondary)})");
            if (!W.Item3) miss.Add($"W.Melee({e.WeaponMelee},has={has(e.WeaponMelee)})");
            if (!W.Item4) miss.Add($"W.Explosive({e.WeaponExplosive},has={has(e.WeaponExplosive)})");
            if (!W.Item5) miss.Add($"W.Special({e.WeaponSpecial},has={has(e.WeaponSpecial)})");

            if (!C.Item1) miss.Add($"C.Red({e.CharaRedId},has={has(e.CharaRedId)})");
            if (!C.Item2) miss.Add($"C.Blue({e.CharaBlueId},has={has(e.CharaBlueId)})");
            if (!C.Item3) miss.Add($"C.Head({e.PartHead},has={has(e.PartHead)})");
            if (!C.Item4) miss.Add($"C.Face({e.PartFace},has={has(e.PartFace)})");
            if (!C.Item5) miss.Add($"C.Jacket({e.PartJacket},has={has(e.PartJacket)})");
            if (!C.Item6) miss.Add($"C.Pocket({e.PartPocket},has={has(e.PartPocket)})");
            if (!C.Item7) miss.Add($"C.Glove({e.PartGlove},has={has(e.PartGlove)})");
            if (!C.Item8) miss.Add($"C.Belt({e.PartBelt},has={has(e.PartBelt)})");
            if (!C.Item9) miss.Add($"C.Holster({e.PartHolster},has={has(e.PartHolster)})");
            if (!C.Item10) miss.Add($"C.Skin({e.PartSkin},has={has(e.PartSkin)})");
            if (!C.Item11) miss.Add($"C.Beret({e.BeretItem},has={has(e.BeretItem)})");
            if (!C.Item12) miss.Add($"C.Dino({e.DinoItem},has={has(e.DinoItem)})");

            if (!N.Item1) miss.Add($"I.Accessory({e.AccessoryId},has={has(e.AccessoryId)})");
            if (!N.Item2) miss.Add($"I.Spray({e.SprayId},has={has(e.SprayId)})");
            if (!N.Item3) miss.Add($"I.NameCard({e.NameCardId},has={has(e.NameCardId)})");

            CLogger.Print($"{tag} missing: {(miss.Count == 0 ? "none" : string.Join(", ", miss))}", LoggerType.Debug);
        }


        // =====================
        // SetDefaultEquipment / FixInvalidHeadFace (aynen, sadece altta kullanılıyor)
        // =====================
        private static void SetDefaultEquipment(
            PlayerEquipment Equip,
            ref (bool, bool, bool, bool, bool) WeaponStatus,
            ref (bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool) CharacterStatus,
            ref (bool, bool, bool) NewItemStatus)
        {
            if (!WeaponStatus.Item1) Equip.WeaponPrimary = 103004;
            if (!WeaponStatus.Item2) Equip.WeaponSecondary = 202003;
            if (!WeaponStatus.Item3) Equip.WeaponMelee = 301001;
            if (!WeaponStatus.Item4) Equip.WeaponExplosive = 407001;
            if (!WeaponStatus.Item5) Equip.WeaponSpecial = 508001;

            if (!CharacterStatus.Item1) Equip.CharaRedId = 601001;
            if (!CharacterStatus.Item2) Equip.CharaBlueId = 602002;
            if (!CharacterStatus.Item3) Equip.PartHead = 1000700000;
            if (!CharacterStatus.Item4) Equip.PartFace = 1000800000;
            if (!CharacterStatus.Item5) Equip.PartJacket = 1000900000;
            if (!CharacterStatus.Item6) Equip.PartPocket = 1001000000;
            if (!CharacterStatus.Item7) Equip.PartGlove = 1001100000;
            if (!CharacterStatus.Item8) Equip.PartBelt = 1001200000;
            if (!CharacterStatus.Item9) Equip.PartHolster = 1001300000;
            if (!CharacterStatus.Item10) Equip.PartSkin = 1001400000;
            if (!CharacterStatus.Item11) Equip.BeretItem = 0;
            if (!CharacterStatus.Item12) Equip.DinoItem = 1500511;

            if (!NewItemStatus.Item1) Equip.AccessoryId = 0;
            if (!NewItemStatus.Item2) Equip.SprayId = 0;
            if (!NewItemStatus.Item3) Equip.NameCardId = 0;

            FixInvalidHeadFace(Equip);
        }

        public static void FixInvalidHeadFace(PlayerEquipment Equip)
        {
            bool isHeadSpecial = Equip.PartHead == 1000700000;
            bool isFaceSpecial = Equip.PartFace == 1000800000;
            bool isHeadEmpty = Equip.PartHead == 0;
            bool isFaceEmpty = Equip.PartFace == 0;

            if (isHeadSpecial && isFaceSpecial) return;
            if (isHeadSpecial && !isFaceSpecial) { Equip.PartHead = 0; return; }
            if (isFaceSpecial && !isHeadSpecial) { Equip.PartFace = 0; return; }
            if (!isHeadEmpty && !isFaceEmpty) { Equip.PartHead = 0; return; }
            if (isHeadEmpty && isFaceEmpty) { Equip.PartHead = 1000700000; }
        }

        public static void UpdateWeapons(PlayerEquipment Equip, DBQuery Query)
        {
            Query.AddQuery("weapon_primary", Equip.WeaponPrimary);
            Query.AddQuery("weapon_secondary", Equip.WeaponSecondary);
            Query.AddQuery("weapon_melee", Equip.WeaponMelee);
            Query.AddQuery("weapon_explosive", Equip.WeaponExplosive);
            Query.AddQuery("weapon_special", Equip.WeaponSpecial);
        }
        public static void UpdateChars(PlayerEquipment Equip, DBQuery Query)
        {
            Query.AddQuery("chara_red_side", Equip.CharaRedId);
            Query.AddQuery("chara_blue_side", Equip.CharaBlueId);
            Query.AddQuery("part_head", Equip.PartHead);
            Query.AddQuery("part_face", Equip.PartFace);
            Query.AddQuery("part_jacket", Equip.PartJacket);
            Query.AddQuery("part_pocket", Equip.PartPocket);
            Query.AddQuery("part_glove", Equip.PartGlove);
            Query.AddQuery("part_belt", Equip.PartBelt);
            Query.AddQuery("part_holster", Equip.PartHolster);
            Query.AddQuery("part_skin", Equip.PartSkin);
            Query.AddQuery("beret_item_part", Equip.BeretItem);
            Query.AddQuery("dino_item_chara", Equip.DinoItem);
        }
        public static void UpdateItems(PlayerEquipment Equip, DBQuery Query)
        {
            Query.AddQuery("accesory_id", Equip.AccessoryId);
            Query.AddQuery("spray_id", Equip.SprayId);
            Query.AddQuery("namecard_id", Equip.NameCardId);
        }
        public static void TryCreateItem(ItemsModel Model, PlayerInventory Inventory, long OwnerId)
        {
            try
            {
                ItemsModel Item = Inventory.GetItem(Model.Id);
                if (Item == null)
                {
                    if (DaoManagerSQL.CreatePlayerInventoryItem(Model, OwnerId))
                    {
                        Inventory.AddItem(Model);
                    }
                }
                else
                {
                    Model.ObjectId = Item.ObjectId;
                    if (Item.Equip == ItemEquipType.Durable)
                    {
                        if (ShopManager.IsRepairableItem(Model.Id))
                        {
                            Model.Count = 100;
                            UpdateDB("player_items", "count", (long)Model.Count, "owner_id", OwnerId, "id", Model.Id);
                        }
                        else
                        {
                            Model.Count += Item.Count;
                            UpdateDB("player_items", "count", (long)Model.Count, "owner_id", OwnerId, "id", Model.Id);
                        }
                    }
                    else if (Item.Equip == ItemEquipType.Temporary)
                    {
                        DateTime Data = DateTime.ParseExact(Item.Count.ToString(), "yyMMddHHmm", CultureInfo.InvariantCulture);
                        if (Model.Category != ItemCategory.Coupon)
                        {
                            Model.Equip = ItemEquipType.Temporary;
                            Model.Count = Convert.ToUInt32(Data.AddSeconds(Model.Count).ToString("yyMMddHHmm"));
                        }
                        else
                        {
                            TimeSpan Time = DateTime.ParseExact(Model.Count.ToString(), "yyMMddHHmm", CultureInfo.InvariantCulture) - DateTimeUtil.Now();
                            Model.Equip = ItemEquipType.Temporary;
                            Model.Count = Convert.ToUInt32(Data.AddDays(Time.TotalDays).ToString("yyMMddHHmm"));
                        }
                        UpdateDB("player_items", "count", (long)Model.Count, "owner_id", OwnerId, "id", Model.Id);
                    }
                    Item.Equip = Model.Equip;
                    Item.Count = Model.Count;
                }
            }
            catch (Exception Ex)
            {
                CLogger.Print(Ex.Message, LoggerType.Error, Ex);
            }
        }
        public static ItemCategory GetItemCategory(int ItemId)
        {
            int BasicValue = GetIdStatics(ItemId, 1), PartValue = GetIdStatics(ItemId, 4);
            if (BasicValue >= 1 && BasicValue <= 5)
            {
                return ItemCategory.Weapon;
            }
            else if ((BasicValue >= 6 && BasicValue <= 14) || BasicValue == 27 || (PartValue >= 7 && PartValue <= 14))
            {
                return ItemCategory.Character;
            }
            else if ((BasicValue >= 16 && BasicValue <= 20) || BasicValue == 22 || BasicValue == 26 || BasicValue == 28 || (BasicValue >= 36 && BasicValue <= 40))
            {
                return ItemCategory.Coupon;
            }
            else if (BasicValue == 15 || (BasicValue >= 30 && BasicValue <= 35))
            {
                return ItemCategory.NewItem;
            }
            else
            {
                CLogger.Print($"Invalid Category [{BasicValue}]: {ItemId}", LoggerType.Warning);
            }
            return ItemCategory.None;
        }
        public static uint ValidateStockId(int ItemId)
        {
            int CharaPart = GetIdStatics(ItemId, 4);
            return GenStockId((CharaPart >= 7 && CharaPart <= 14) ? CharaPart : ItemId);
        }
        public static int GetIdStatics(int Id, int Type)
        {
            switch (Type)
            {
                case 1: return Id / 100000; // Item Class
                case 2: return Id % 100000 / 1000; // Class Type
                case 3: return Id % 1000; // Number
                case 4: return Id % 10000000 / 100000; //Partial
                case 5: return Id / 1000; //Addons
                default: return 0;
            }
        }
        public static double GetDuration(DateTime Date) => (double)(DateTimeUtil.Now() - Date).TotalSeconds;
        public static byte[] AddressBytes(string Host) => IPAddress.Parse(Host).GetAddressBytes();
        public static int CreateItemId(int ItemClass, int ClassType, int Number) => ItemClass * 100000 + ClassType * 1000 + Number;
        public static int Percentage(int Total, int Percent) => Total * Percent / 100;
        public static float Percentage(float Total, int Percent) => Total * Percent / 100;
        public static char[] SubArray(this char[] Input, int StartIndex, int Length)
        {
            List<char> Result = new List<char>();
            for (int i = StartIndex; i < Length; i++)
            {
                Result.Add(Input[i]);
            }
            return Result.ToArray();
        }
        public static bool UpdateDB(string TABEL, string[] COLUMNS, params object[] VALUES)
        {
            if (COLUMNS.Length > 0 && VALUES.Length > 0 && COLUMNS.Length != VALUES.Length)
            {
                CLogger.Print($"[Update Database] Wrong values: {string.Join(",", COLUMNS)}/{string.Join(",", VALUES)}", LoggerType.Warning);
                return false;
            }
            else if (COLUMNS.Length == 0 || VALUES.Length == 0)
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection Connection = ConnectionSQL.GetInstance().Conn())
                using (NpgsqlCommand Command = Connection.CreateCommand())
                {
                    Connection.Open();
                    Command.CommandType = CommandType.Text;
                    string LOADED = "";
                    List<string> Parameters = new List<string>();
                    for (int i = 0; i < VALUES.Length; i++)
                    {
                        object Obj = VALUES[i];
                        string Column = COLUMNS[i];
                        string Param = "@Value" + i;
                        Command.Parameters.AddWithValue(Param, Obj);
                        Parameters.Add(Column + "=" + Param);
                    }
                    LOADED = string.Join(",", Parameters.ToArray());
                    Command.CommandText = $"UPDATE {TABEL} SET {LOADED}";
                    Command.ExecuteNonQuery();
                    Command.Dispose();
                    Connection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                CLogger.Print($"[AllUtils.UpdateDB1] {ex.Message}", LoggerType.Error, ex);
                return false;
            }
        }
        public static bool UpdateDB(string TABEL, string Req1, object ValueReq1, string[] COLUMNS, params object[] VALUES)
        {
            if (COLUMNS.Length > 0 && VALUES.Length > 0 && COLUMNS.Length != VALUES.Length)
            {
                CLogger.Print("[Update Database] Wrong values: " + string.Join(",", COLUMNS) + "/" + string.Join(",", VALUES), LoggerType.Warning);
                return false;
            }
            else if (COLUMNS.Length == 0 || VALUES.Length == 0)
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection Connection = ConnectionSQL.GetInstance().Conn())
                using (NpgsqlCommand Command = Connection.CreateCommand())
                {
                    Connection.Open();
                    Command.CommandType = CommandType.Text;
                    string Loaded = "";
                    List<string> Parameters = new List<string>();
                    for (int i = 0; i < VALUES.Length; i++)
                    {
                        object Obj = VALUES[i];
                        string Column = COLUMNS[i];
                        string Param = "@Value" + i;
                        Command.Parameters.AddWithValue(Param, Obj);
                        Parameters.Add(Column + "=" + Param);
                    }
                    Loaded = string.Join(",", Parameters.ToArray());
                    Command.Parameters.AddWithValue("@Req1", ValueReq1);
                    Command.CommandText = $"UPDATE {TABEL} SET {Loaded} WHERE {Req1}=@Req1";
                    Command.ExecuteNonQuery();
                    Command.Dispose();
                    Connection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                CLogger.Print($"[AllUtils.UpdateDB2] {ex.Message}", LoggerType.Error, ex);
                return false;
            }
        }
        public static bool UpdateDB(string TABEL, string COLUMN, object VALUE, string Req1, object ValueReq1)
        {
            try
            {
                using (NpgsqlConnection Connection = ConnectionSQL.GetInstance().Conn())
                using (NpgsqlCommand Command = Connection.CreateCommand())
                {
                    Connection.Open();
                    Command.CommandType = CommandType.Text;
                    Command.Parameters.AddWithValue("@Value", VALUE);
                    Command.Parameters.AddWithValue("@Req1", ValueReq1);
                    Command.CommandText = $"UPDATE {TABEL} SET {COLUMN}=@Value WHERE {Req1}=@Req1";
                    Command.ExecuteNonQuery();
                    Command.Dispose();
                    Connection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                CLogger.Print($"[AllUtils.UpdateDB3] {ex.Message}", LoggerType.Error, ex);
                return false;
            }
        }
        public static bool UpdateDB(string TABEL, string Req1, object ValueReq1, string Req2, object valueReq2, string[] COLUMNS, params object[] VALUES)
        {
            if (COLUMNS.Length > 0 && VALUES.Length > 0 && COLUMNS.Length != VALUES.Length)
            {
                CLogger.Print("[Update Database] Wrong values: " + string.Join(",", COLUMNS) + "/" + string.Join(",", VALUES), LoggerType.Warning);
                return false;
            }
            else if (COLUMNS.Length == 0 || VALUES.Length == 0)
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection Connection = ConnectionSQL.GetInstance().Conn())
                using (NpgsqlCommand Command = Connection.CreateCommand())
                {
                    Connection.Open();
                    Command.CommandType = CommandType.Text;
                    string Loaded = "";
                    List<string> Parameters = new List<string>();
                    for (int i = 0; i < VALUES.Length; i++)
                    {
                        object Obj = VALUES[i];
                        string Column = COLUMNS[i];
                        string Param = "@Value" + i;
                        Command.Parameters.AddWithValue(Param, Obj);
                        Parameters.Add(Column + "=" + Param);
                    }
                    Loaded = string.Join(",", Parameters.ToArray());
                    if (Req1 != null)
                    {
                        Command.Parameters.AddWithValue("@Req1", ValueReq1);
                    }
                    if (Req2 != null)
                    {
                        Command.Parameters.AddWithValue("@Req2", valueReq2);
                    }
                    if (Req1 != null && Req2 == null)
                    {
                        Command.CommandText = $"UPDATE {TABEL} SET {Loaded} WHERE {Req1}=@Req1";
                    }
                    else if (Req2 != null && Req1 == null)
                    {
                        Command.CommandText = $"UPDATE {TABEL} SET {Loaded} WHERE {Req2}=@Req2";
                    }
                    else if (Req2 != null && Req1 != null)
                    {
                        Command.CommandText = $"UPDATE {TABEL} SET {Loaded} WHERE {Req1}=@Req1 AND {Req2}=@Req2";
                    }
                    Command.ExecuteNonQuery();
                    Command.Dispose();
                    Connection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                CLogger.Print($"[AllUtils.UpdateDB4] {ex.Message}", LoggerType.Error, ex);
                return false;
            }
        }
        public static bool UpdateDB(string TABEL, string Req1, int[] ValueReq1, string Req2, object ValueReq2, string[] COLUMNS, params object[] VALUES)
        {
            if (COLUMNS.Length > 0 && VALUES.Length > 0 && COLUMNS.Length != VALUES.Length)
            {
                CLogger.Print("[updateDB5] Wrong values: " + string.Join(",", COLUMNS) + "/" + string.Join(",", VALUES), LoggerType.Warning);
                return false;
            }
            else if (COLUMNS.Length == 0 || VALUES.Length == 0)
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection Connection = ConnectionSQL.GetInstance().Conn())
                using (NpgsqlCommand Command = Connection.CreateCommand())
                {
                    Connection.Open();
                    Command.CommandType = CommandType.Text;
                    string Loaded = "";
                    List<string> Parameters = new List<string>();
                    for (int i = 0; i < VALUES.Length; i++)
                    {
                        object Obj = VALUES[i];
                        string Column = COLUMNS[i];
                        string Param = "@Value" + i;
                        Command.Parameters.AddWithValue(Param, Obj);
                        Parameters.Add(Column + "=" + Param);
                    }
                    Loaded = string.Join(",", Parameters.ToArray());
                    if (Req1 != null)
                    {
                        Command.Parameters.AddWithValue("@Req1", ValueReq1);
                    }
                    if (Req2 != null)
                    {
                        Command.Parameters.AddWithValue("@Req2", ValueReq2);
                    }
                    if (Req1 != null && Req2 == null)
                    {
                        Command.CommandText = $"UPDATE {TABEL} SET {Loaded} WHERE {Req1} = ANY (@Req1)";
                    }
                    else if (Req2 != null && Req1 == null)
                    {
                        Command.CommandText = $"UPDATE {TABEL} SET {Loaded} WHERE {Req2}=@Req2";
                    }
                    else if (Req2 != null && Req1 != null)
                    {
                        Command.CommandText = $"UPDATE {TABEL} SET {Loaded} WHERE {Req1} = ANY (@Req1) AND {Req2}=@Req2";
                    }
                    Command.ExecuteNonQuery();
                    Command.Dispose();
                    Connection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                CLogger.Print($"[AllUtils.UpdateDB5] {ex.Message}", LoggerType.Error, ex);
                return false;
            }
        }
        public static bool UpdateDB(string TABEL, string COLUMN, object VALUE, string Req1, object ValueReq1, string Req2, object ValueReq2)
        {
            try
            {
                using (NpgsqlConnection Connection = ConnectionSQL.GetInstance().Conn())
                using (NpgsqlCommand Command = Connection.CreateCommand())
                {
                    Connection.Open();
                    Command.CommandType = CommandType.Text;
                    Command.Parameters.AddWithValue("@Value", VALUE);
                    if (Req1 != null)
                    {
                        Command.Parameters.AddWithValue("@Req1", ValueReq1);
                    }
                    if (Req2 != null)
                    {
                        Command.Parameters.AddWithValue("@Req2", ValueReq2);
                    }
                    if (Req1 != null && Req2 == null)
                    {
                        Command.CommandText = $"UPDATE {TABEL} SET {COLUMN}=@Value WHERE {Req1}=@Req1";
                    }
                    else if (Req2 != null && Req1 == null)
                    {
                        Command.CommandText = $"UPDATE {TABEL} SET {COLUMN}=@Value WHERE {Req2}=@Req2";
                    }
                    else if (Req2 != null && Req1 != null)
                    {
                        Command.CommandText = $"UPDATE {TABEL} SET {COLUMN}=@Value WHERE {Req1}=@Req1 AND {Req2}=@Req2";
                    }
                    Command.ExecuteNonQuery();
                    Command.Dispose();
                    Connection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                CLogger.Print($"[AllUtils.UpdateDB6] {ex.Message}", LoggerType.Error, ex);
                return false;
            }
        }
        public static bool DeleteDB(string TABEL, string Req1, object ValueReq1)
        {
            try
            {
                using (NpgsqlConnection Connection = ConnectionSQL.GetInstance().Conn())
                using (NpgsqlCommand Command = Connection.CreateCommand())
                {
                    Connection.Open();
                    Command.CommandType = CommandType.Text;
                    Command.Parameters.AddWithValue("@Req1", ValueReq1);
                    Command.CommandText = $"DELETE FROM {TABEL} WHERE {Req1}=@Req1";
                    Command.ExecuteNonQuery();
                    Command.Dispose();
                    Connection.Dispose();
                    Connection.Close();
                }
                return true;
            }
            catch (Exception Ex)
            {
                CLogger.Print(Ex.Message, LoggerType.Error, Ex);
                return false;
            }
        }
        public static bool DeleteDB(string TABEL, string Req1, object ValueReq1, string Req2, object ValueReq2)
        {
            try
            {
                using (NpgsqlConnection Connection = ConnectionSQL.GetInstance().Conn())
                using (NpgsqlCommand Command = Connection.CreateCommand())
                {
                    Connection.Open();
                    Command.CommandType = CommandType.Text;
                    if (Req1 != null)
                    {
                        Command.Parameters.AddWithValue("@Req1", ValueReq1);
                    }
                    if (Req2 != null)
                    {
                        Command.Parameters.AddWithValue("@Req2", ValueReq2);
                    }
                    if (Req1 != null && Req2 == null)
                    {
                        Command.CommandText = $"DELETE FROM {TABEL} WHERE {Req1}=@Req1";
                    }
                    else if (Req2 != null && Req1 == null)
                    {
                        Command.CommandText = $"DELETE FROM {TABEL} WHERE {Req2}=@Req2";
                    }
                    else if (Req2 != null && Req1 != null)
                    {
                        Command.CommandText = $"DELETE FROM {TABEL} WHERE {Req1}=@Req1 AND {Req2}=@Req2";
                    }
                    Command.ExecuteNonQuery();
                    Command.Dispose();
                    Connection.Dispose();
                    Connection.Close();
                }
                return true;
            }
            catch (Exception Ex)
            {
                CLogger.Print(Ex.Message, LoggerType.Error, Ex);
                return false;
            }
        }
        public static bool DeleteDB(string TABEL, string Req1, object[] ValueReq1, string Req2, object ValueReq2)
        {
            if (ValueReq1.Length == 0)
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection Connection = ConnectionSQL.GetInstance().Conn())
                using (NpgsqlCommand Command = Connection.CreateCommand())
                {
                    Connection.Open();
                    Command.CommandType = CommandType.Text;
                    string Loaded = "";
                    List<string> Parameters = new List<string>();
                    for (int i = 0; i < ValueReq1.Length; i++)
                    {
                        object Obj = ValueReq1[i];
                        string Param = "@Value" + i;
                        Command.Parameters.AddWithValue(Param, Obj);
                        Parameters.Add(Param);
                    }
                    Loaded = string.Join(",", Parameters.ToArray());
                    Command.Parameters.AddWithValue("@Req2", ValueReq2);
                    Command.CommandText = $"DELETE FROM {TABEL} WHERE {Req1} in ({Loaded}) AND {Req2}=@Req2";
                    Command.ExecuteNonQuery();
                    Command.Dispose();
                    Connection.Dispose();
                    Connection.Close();
                }
                return true;
            }
            catch (Exception Ex)
            {
                CLogger.Print(Ex.Message, LoggerType.Error, Ex);
                return false;
            }
        }
        public static uint GetPlayerStatus(AccountStatus status, bool isOnline)
        {
            GetPlayerLocation(status, isOnline, out FriendState state, out int roomId, out int channelId, out int serverId);
            return GetPlayerStatus(roomId, channelId, serverId, (int)state);
        }
        public static uint GetPlayerStatus(int roomId, int channelId, int serverId, int stateId)
        {
            int p1 = (stateId & 0xFF) << 28, p2 = (serverId & 0xFF) << 20, p3 = (channelId & 0xFF) << 12, p4 = roomId & 0xFFF;
            return (uint)(p1 | p2 | p3 | p4);
        }
        public static ulong GetPlayerStatus(int clanFId, int roomId, int channelId, int serverId, int stateId)
        {
            long p1 = (clanFId & 0xFFFFFFFF) << 32, p2 = (stateId & 0xFF) << 28, p3 = (serverId & 0xFF) << 20, p4 = (channelId & 0xFF) << 12, p5 = roomId & 0xFFF;
            return (ulong)(p1 | p2 | p3 | p4 | p5);
        }
        public static ulong GetClanStatus(AccountStatus status, bool isOnline)
        {
            GetPlayerLocation(status, isOnline, out FriendState state, out int roomId, out int channelId, out int serverId, out int clanFId);
            return GetPlayerStatus(clanFId, roomId, channelId, serverId, (int)state);
        }
        public static ulong GetClanStatus(FriendState state)
        {
            return GetPlayerStatus(0, 0, 0, 0, (int)state);
        }
        public static uint GetFriendStatus(FriendModel f)
        {
            PlayerInfo info = f.Info;
            if (info == null)
            {
                return 0;
            }
            FriendState state = 0;
            int serverId = 0, channelId = 0, roomId = 0;
            if (f.Removed)
            {
                state = FriendState.Offline;
            }
            else if (f.State > 0)
            {
                state = (FriendState)f.State;
            }
            else
            {
                GetPlayerLocation(info.Status, info.IsOnline, out state, out roomId, out channelId, out serverId);
            }
            return GetPlayerStatus(roomId, channelId, serverId, (int)state);
        }
        public static uint GetFriendStatus(FriendModel f, FriendState stateN)
        {
            PlayerInfo info = f.Info;
            if (info == null)
            {
                return 0;
            }
            FriendState state = stateN;
            int serverId = 0, channelId = 0, roomId = 0;
            if (f.Removed)
            {
                state = FriendState.Offline;
            }
            else if (f.State > 0)
            {
                state = (FriendState)f.State;
            }
            else if (stateN == 0)
            {
                GetPlayerLocation(info.Status, info.IsOnline, out state, out roomId, out channelId, out serverId);
            }
            return GetPlayerStatus(roomId, channelId, serverId, (int)state);
        }
        public static void GetPlayerLocation(AccountStatus status, bool isOnline, out FriendState state, out int roomId, out int channelId, out int serverId)
        {
            roomId = 0;
            channelId = 0;
            serverId = 0;
            if (isOnline)
            {
                if (status.RoomId != 255)
                {
                    roomId = status.RoomId;
                    channelId = status.ChannelId;
                    state = FriendState.Room;
                }
                else if (status.RoomId == 255 && status.ChannelId != 255)
                {
                    channelId = status.ChannelId;
                    state = FriendState.Lobby;
                }
                else if (status.RoomId == 255 && status.ChannelId == 255)
                {
                    state = FriendState.Online;
                }
                else
                {
                    state = FriendState.Offline;
                }
                if (status.ServerId != 255)
                {
                    serverId = status.ServerId;
                }
            }
            else
            {
                state = FriendState.Offline;
            }
        }
        public static void GetPlayerLocation(AccountStatus status, bool isOnline, out FriendState state, out int roomId, out int channelId, out int serverId, out int clanFId)
        {
            roomId = 0;
            channelId = 0;
            serverId = 0;
            clanFId = 0;
            if (isOnline)
            {
                if (status.RoomId != 255)
                {
                    roomId = status.RoomId;
                    channelId = status.ChannelId;
                    state = FriendState.Room;
                }
                else if ((status.ClanMatchId != 255 || status.RoomId == 255) && status.ChannelId != 255)
                {
                    channelId = status.ChannelId;
                    state = FriendState.Lobby;
                }
                else if (status.RoomId == 255 && status.ChannelId == 255)
                {
                    state = FriendState.Online;
                }
                else
                {
                    state = FriendState.Offline;
                }
                if (status.ServerId != 255)
                {
                    serverId = status.ServerId;
                }
                if (status.ClanMatchId != 255)
                {
                    clanFId = status.ClanMatchId + 1;
                }
            }
            else
            {
                state = FriendState.Offline;
            }
        }
        public static ushort GetMissionCardFlags(int missionId, int cardIdx, byte[] arrayList)
        {
            if (missionId == 0)
            {
                return 0;
            }
            int result = 0;
            List<MissionCardModel> List = MissionCardRAW.GetCards(missionId, cardIdx);
            foreach (MissionCardModel Card in List)
            {
                if (arrayList[Card.ArrayIdx] >= Card.MissionLimit)
                {
                    result |= Card.Flag;
                }
            }
            return (ushort)result;
        }
        public static byte[] GetMissionCardFlags(int missionId, byte[] arrayList)
        {
            if (missionId == 0)
            {
                return new byte[20];
            }
            List<MissionCardModel> List = MissionCardRAW.GetCards(missionId);
            if (List.Count == 0)
            {
                return new byte[20];
            }
            using (SyncServerPacket S = new SyncServerPacket(20))
            {
                int result = 0;
                for (int i = 0; i < 10; i++)
                {
                    List<MissionCardModel> Result = MissionCardRAW.GetCards(List, i);
                    foreach (MissionCardModel Card in Result)
                    {
                        if (arrayList[Card.ArrayIdx] >= Card.MissionLimit)
                        {
                            result |= Card.Flag;
                        }
                    }
                    S.WriteH((ushort)result);
                    result = 0;
                }
                return S.ToArray();
            }
        }
        public static int CountDB(string CommandArgument)
        {
            int Result = 0;
            try
            {
                using (NpgsqlConnection Connection = ConnectionSQL.GetInstance().Conn())
                {
                    NpgsqlCommand Command = Connection.CreateCommand();
                    Connection.Open();
                    Command.CommandText = CommandArgument;
                    Result = Convert.ToInt32(Command.ExecuteScalar());
                    Command.Dispose();
                    Connection.Dispose();
                    Connection.Close();
                }
            }
            catch (Exception ex)
            {
                CLogger.Print($"[QuerySQL.CountDB] {ex.Message}", LoggerType.Error, ex);
            }
            return Result;
        }
        public static bool ValidateAllPlayersAccount()
        {
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    NpgsqlCommand command = connection.CreateCommand();
                    connection.Open();
                    command.CommandType = CommandType.Text;
                    command.CommandText = $"UPDATE accounts SET online = {false} WHERE online = {true}";
                    command.ExecuteNonQuery();
                    command.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception Ex)
            {
                CLogger.Print(Ex.Message, LoggerType.Error, Ex);
                return false;
            }
        }
        public static uint GenStockId(int ItemId) => BitConverter.ToUInt32(ReplaceBytes(ItemId), 0);
        private static byte[] ReplaceBytes(int Value)
        {
            byte[] VALUES = BitConverter.GetBytes(Value);
            VALUES[3] = 0x40;
            return VALUES;
        }
        public static T NextOf<T>(IList<T> List, T Item)
        {
            int IndexOf = List.IndexOf(Item);
            return List[IndexOf == List.Count - 1 ? 0 : IndexOf + 1];
        }
        public static T ParseEnum<T>(string Value) => (T)Enum.Parse(typeof(T), Value, true);
        public static string[] SplitObjects(string Input, string Delimiter) => Input.Split(new string[] { Delimiter }, StringSplitOptions.None);
        public static string ToTitleCase(string Text)
        {
            string FirstWord = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Text.Split(' ')[0].ToLower());
            Text = Text.Replace(Text.Split(' ')[0], FirstWord);
            return Text;
        }
        private static int GCD(int A, int B)
        {
            int Remainder;
            while (B != 0)
            {
                Remainder = A % B;
                A = B;
                B = Remainder;
            }
            return A;
        }
        public static string AspectRatio(int X, int Y) => string.Format("{0}:{1}", X / GCD(X, Y), Y / GCD(X, Y));
    }
}