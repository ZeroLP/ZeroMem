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
        RD.Reader reader = new RD.Reader();
        WR.Writer writer = new WR.Writer();
        OFM.OffsetManager Offset = new OFM.OffsetManager();
        PM.ProcessManager ProcessManager = new PM.ProcessManager();
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


        public void Notepad()
        {
            Process nProc = ProcessManager.GetProcess("Notepad");
            if (nProc == null) return;
            IntPtr nProcHandle = ProcessManager.GetProcessHandle(nProc);
            IntPtr nBaseAddress = ProcessManager.GetBaseAddress(nProc);

            
            if (writer.WriteBytes(nProcHandle, 0x3FFF83, 10) == false)
            {
                MessageBox.Show("No bytes written!");
                return;
            };

            MessageBox.Show("Wrote 10 bytes on 0x3FFF83 in Notepad");


        }
    }
}
