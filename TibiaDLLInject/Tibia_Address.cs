using System;
using System.Collections.Generic;
using System.Text;

namespace TibiaDLLInject
{
    public enum ADDRESSES : int
    {
        XOR = 0x432BB0,
        MANA_POINTS = 0x432C04,
        MAX_MANA_POINTS = XOR + 0x4, //MAX_MANA_POINTS          = XOR + sizeof(int),
        HEALTH_POINTS = 0x5CF000,
        MAX_HEALTH_POINTS = 0x5CF02C
    }
}
