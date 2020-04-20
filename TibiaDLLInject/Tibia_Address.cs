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


        ArmorAddress = 0x74FA00,
        FoodAddress = 0x74FA00 - 0x30,




        MarketWindowLeftBottom = 0x7FFDF13BB9F0 // dolny




    }

    public static class GetMarketTopEnums
    {
        public static List<ADDRESSES> ListOfTopLeftMarket()
        {
            var list = new List<ADDRESSES>();
            list.Add(ADDRESSES.ArmorAddress);
            list.Add(ADDRESSES.FoodAddress);
            return list;
        }
    }
    

}
