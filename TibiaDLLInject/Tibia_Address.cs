using System;
using System.Collections.Generic;
using System.Text;

namespace TibiaDLLInject
{
    public enum ADDRESSES : long
    {
        // old and bad adresses

        //XOR = 0x432BB0,
        //MANA_POINTS = 0x432C04,
        //MAX_MANA_POINTS = XOR + 0x4, //MAX_MANA_POINTS  = XOR + sizeof(int),
        //HEALTH_POINTS = 0x5CF000,
        //MAX_HEALTH_POINTS = 0x5CF02C,



        // from this link
        // https://github.com/Mytherin/Tibialyzer/issues/337


        PlayerIDAddress = 0x70F050,
        BattleListAddress = 0x76A48C,
        HealthAddress = 0x70E000,
        MaxManaAddress = 0x57045C,
        ManaAddress = 0x57048C,
        TabsBaseAddress = 0x570744,
        XORAddress = 0x570458,
        MaxHealthAddress = 0x70E048,
        AmmunitionCountAddress = 0x7B147C,
        AmmunitionTypeAddress = 0x7B1480,
        WeaponCountAddress = 0x7B14FC,
        WeaponTypeAddress = 0x7B1500,
        LevelAddress = 0x570470,
        ExperienceAddress = 0x570460,

        MarketFirstSelectListAddress = 0x196622, // guessing?

        // gathering addreses of tibia market items

        MarketWindowTopLeft = 0x7FFDF13BB9E8, //gorny sa w long wpisane


        Armors = 0x74FA00,
        Amulets = 0x74FA00 - 0x8,
        Boots = 0x74FA00 - 0x10,
        Containers = 0x74FA00 - 0x1C,
        Decoration = 0x74FA00 - 0x28,
        Food = 0x74FA00 - 0x30,
        HelmetsAndHats = 0x74FA00 - 0x44,
        Legs = 0x74FA00 - 0x4C,
        Others = 0x74FA00 - 0x54,
        Potions = 0x74FA00 - 0x5C,
        Rings = 0x74FA00 - 0x64,
        Runes = 0x74FA00 - 0x6C,
        Shields = 0x74FA00 - 0x74,
        TibiaCoins = 0x74FA00  + 0x54,
        Tools = 0x74FA00 - 0x7C,
        Valuables = 0x74FA00 - 0x88,
        WeaponsAll = 0x74FA00  + 0x30 + 0x30, // all is enough
        //WeaponsAmmunition = 0x74FA00 - 0x9C,
        //WeaponsDistance = 0x74FA00 + 0x30 + 0x30 + 0x30,
    }

    public static class GetMarketTopEnums
    {
        public static List<ADDRESSES> ListOfTopLeftMarket()
        {
            var list = new List<ADDRESSES>();
            list.Add(ADDRESSES.Armors);
            list.Add(ADDRESSES.Amulets);
            list.Add(ADDRESSES.Boots);
            list.Add(ADDRESSES.Containers);
            list.Add(ADDRESSES.Decoration);
            list.Add(ADDRESSES.Food);
            list.Add(ADDRESSES.HelmetsAndHats);
            list.Add(ADDRESSES.Legs);
            list.Add(ADDRESSES.Others);
            list.Add(ADDRESSES.Potions);
            list.Add(ADDRESSES.Rings);
            list.Add(ADDRESSES.Runes);
            list.Add(ADDRESSES.Shields);
            list.Add(ADDRESSES.TibiaCoins);
            list.Add(ADDRESSES.Tools);
            list.Add(ADDRESSES.Valuables);
            list.Add(ADDRESSES.WeaponsAll);
            return list;
        }
    }
    

}
