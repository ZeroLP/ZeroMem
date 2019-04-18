using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace ZeroMem.OP
{
    class Output
    {
        //Constructors
        ZeroMem.RD.Reader reader = new ZeroMem.RD.Reader();
        ZeroMem.OFM.OffsetManager Offset = new ZeroMem.OFM.OffsetManager();
        ZeroMem.PM.ProcessManager ProcessManager = new ZeroMem.PM.ProcessManager();
        Form1 MainForm;

        public void League()
        {
            Process Proc = ProcessManager.GetProcess("League of Legends");
            if (Proc == null) return;
            IntPtr ProcHandle = ProcessManager.GetProcessHandle(Proc);
            IntPtr BaseAddress = ProcessManager.GetBaseAddress(Proc);

            var LocalPlayer = reader.ReadInt(ProcHandle, BaseAddress.ToInt32() + Offset.LocalPlayer); //Reading LocalPlayer Pointer (Base + oLocalPlayer)
            var iAIHC = reader.ReadString(ProcHandle, LocalPlayer + Offset.SummonerName, Encoding.ASCII); //Reading AIHeroClient(Summoner Name) as a string value (LocalPlayer(Base + oLocalPlayer) + AIHeroClient)
            var iCN = reader.ReadString(ProcHandle, LocalPlayer + Offset.ChampionName, Encoding.ASCII); // Reading oObjChampionName as a string value(LocalPlayer +oObjChampionName)
            var iGT = reader.ReadFloat(ProcHandle, BaseAddress.ToInt32() + Offset.GameTime); // Reading oGameTime as a int (BaseAddress + oGameTime)
            var iGV = reader.ReadString(ProcHandle, BaseAddress.ToInt32() + Offset.GameVersion, Encoding.ASCII); // Reading oGameVersion as a int (BaseAddress + oGameVersion) | TO-DO: Conversion into float or equivalent
            var iAR = reader.ReadFloat(ProcHandle, LocalPlayer + Offset.AttackRange);
            var iCH = reader.ReadFloat(ProcHandle, LocalPlayer + Offset.Health);
            var iGTC = iGT * 1000;// Convert GameTime offset to milliseconds
            TimeSpan T = TimeSpan.FromMilliseconds(iGTC); //Create a instance of Time
            string GameTime = string.Format("{0:D2} Minutes {1:D2} Seconds", T.Minutes, T.Seconds); //Convert GameTime ms to minutes and seconds

            MessageBox.Show($"LocalPlayer: {iAIHC}\nChampion: {iCN}\nGame Time: {GameTime}\nGame Version {iGV}\nChampion Range: {iAR}\nChampion Health: {iCH}\nPID: {Proc.Id}"); //OUTPUT
            MainForm.textBox1.Text = $"Base Address: {BaseAddress.ToString("X")}";
        }
    }
}
