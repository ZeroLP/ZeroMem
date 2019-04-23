using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ZeroMem.WR
{
    class Writer
    {
        //Kernel32 DLLIMPORT: WriteProcessMemory
        [DllImport("kernel32.dll")]
        static extern bool WriteProcessMemory(
        IntPtr hProcess,
        IntPtr lpBaseAddress,
        byte[] lpBuffer,
        int dwSize,
        out IntPtr lpNumberOfBytesWritten);



        /// <summary>
        /// Write bytes to process's memory
        /// </summary>
        /// <param name="handle">Process Handle</param>
        /// <param name="address">Memory Address</param>
        /// <param name="value">Byte Value</param>
        /// <returns>True at succession, else false</returns>
        public bool WriteBytes(IntPtr handle, int address, int value)
        {
            //Buffer and Pointer Storage
            byte[] dataBuffer = BitConverter.GetBytes(value);
            IntPtr bytesWritten = IntPtr.Zero;

            //Writing Memory and storing the written bytes to dataBuffer byte Array
            //handle: handle of the process | (IntPtr)address: Pointer Address | dataBuffer: Written Bytes from the Pointer Address | dataBuffer.Length: Byte sizes | out bytesRead: Output to pointer of bytes written
            WriteProcessMemory(handle, (IntPtr)address, dataBuffer, dataBuffer.Length, out bytesWritten);

            //Error Handler: If no bytes are written, return 0;
            if (bytesWritten == IntPtr.Zero)
            {
                return false;
            }

            //Return true if byte writing is successful
            return true;
        }
        

    }
}
