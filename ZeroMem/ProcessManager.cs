using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace ZeroMem.PM
{
    class ProcessManager
    {
        public Process GetProcess(string ProcessName)
        {
            Process[] processes = Process.GetProcessesByName(ProcessName);
            if (processes.Length == 0)
            {
                MessageBox.Show($"The Process Named [{ProcessName}] Couldn't Be Found.");
                return null;
            }
             
            return Process.GetProcessesByName(ProcessName).FirstOrDefault();

        }

        public IntPtr GetProcessHandle(Process Prcs)
        {
           return Prcs.Handle;
        }

        public IntPtr GetBaseAddress(Process Prcs)
        {
            return Prcs.MainModule.BaseAddress;
        }

    }
}
