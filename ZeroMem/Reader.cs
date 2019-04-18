using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ZeroMem.RD
{
   

    class Reader
    {
        //Kernel32 DLLIMPORT: ReadProcessMemory
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(
    IntPtr hProcess,
    IntPtr lpBaseAddress,
    [Out] byte[] lpBuffer,
    int dwSize,
    out IntPtr lpNumberOfBytesRead);



       /// <summary>
       /// ReadProcessMemory as a int
       /// </summary>
       /// <param name="handle">Process Handle</param>
       /// <param name="address">Memory Address</param>
       /// <returns>Int32 buffer</returns>
       public int ReadInt(IntPtr handle, int address)
        {
            //Buffer and Pointer Storage
            byte[] dataBuffer = new byte[4];
            IntPtr bytesRead = IntPtr.Zero;

            //Reading Memory and storing the read bytes to dataBuffer byte Array
            //handle: handle of the process | (IntPtr)address: Pointer Address | dataBuffer: Bytes from the Pointer Address | dataBuffer.Length: Byte sizes | out bytesRead: Output to pointer of bytes read
            ReadProcessMemory(handle, (IntPtr)address, dataBuffer, dataBuffer.Length, out bytesRead);

            //Error Handler: If no bytes are read, return 0;
            if (bytesRead == IntPtr.Zero)
            {
                Console.WriteLine("Mo bytes has been read");
                return 0;
            }

            //Return bytes read from the Pointer Address to Int32 Format
            return BitConverter.ToInt32(dataBuffer, 0);
        }

        /// <summary>
        /// ReadProcessMemory as a float
        /// </summary>
        /// <param name="handle">Process Handle</param>
        /// <param name="address">Memory Address</param>
        /// <returns>Float buffer</returns>
        public float ReadFloat(IntPtr handle, int address)
        {
            //Buffer and Pointer Storage
            byte[] dataBuffer = new byte[4];
            IntPtr bytesRead = IntPtr.Zero;

            //Reading Memory and storing the read bytes to dataBuffer byte Array
            //handle: handle of the process | (IntPtr)address: Pointer Address | dataBuffer: Bytes from the Pointer Address | dataBuffer.Length: Byte sizes | out bytesRead: Output to pointer of bytes read
            ReadProcessMemory(handle, (IntPtr)address, dataBuffer, dataBuffer.Length, out bytesRead);

            //Error Handler: If no bytes are read, return 0;
            if (bytesRead == IntPtr.Zero)
            {
                Console.WriteLine("Mo bytes has been read");
                return 0;
            }

            //Return bytes read from the Pointer Address to float Format
            return BitConverter.ToSingle(dataBuffer, 0);
        }


        /// <summary>
        /// ReadProccessMemory as a string
        /// </summary>
        /// <param name="handle">Process Handle</param>
        /// <param name="address">Memory Address</param>
        /// <param name="Encoding">Encoding Method</param>
        /// <returns>String byte</returns>
        public string ReadString(IntPtr handle, int address, Encoding Encoding)
        {
            //Buffer and Pointer Storage | Byte Array size of 512 since strings don't have fixed amount of length
            byte[] dataBuffer = new byte[512];
            IntPtr bytesRead = IntPtr.Zero;

            //Reading Memory and storing the read bytes to dataBuffer byte Array
            //handle: handle of the process | (IntPtr)address: Pointer Address | dataBuffer: Bytes from the Pointer Address | dataBuffer.Length: Byte sizes | out bytesRead: Output to pointer of bytes read
            ReadProcessMemory(handle, (IntPtr)address, dataBuffer, dataBuffer.Length, out bytesRead);

            //Error Handler: If no bytes are read, return an empty String
            if (bytesRead == IntPtr.Zero)
            {
                Console.WriteLine("Mo bytes has been read");
                return string.Empty;
            }

            //Return bytes read from the Pointer Address to string Format and split excess "0"s pointer values since we don't need them
            return Encoding.GetString(dataBuffer).Split('\0')[0];
        }


    }
}
