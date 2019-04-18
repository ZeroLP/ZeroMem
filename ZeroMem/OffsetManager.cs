using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroMem.OFM
{
    class OffsetManager
    {
            // League of Legends Offsets
            public int LocalPlayer = 0x2F457FC; //LocalPlayer Pointer(ME)
            public int SummonerName = 0x0060;
            public int ChampionName = 0x3158;
            public int ChampionLevel = 0x4BAC;
            public int AttackRange = 0x12A8;
            public int Health = 0x0DE8;

            public int GameTime = 0x2F41498;
            public int GameVersion = 0x2F50530; 
      
    }
}
