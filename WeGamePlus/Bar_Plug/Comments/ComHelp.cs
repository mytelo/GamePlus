using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Json;
using System.Text;

namespace WeGamePlus.Bar_Plug.Comments
{
    public class ComHelp
    {
        public static string Cmd(string strInput)
        {
            try
            {
                Process process1 = new Process {
                    StartInfo = { 
                        FileName = "cmd.exe",
                        UseShellExecute = false,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    }
                };
                process1.Start();
                process1.StandardInput.WriteLine(strInput + "&exit");
                process1.StandardInput.AutoFlush = true;
                string str = process1.StandardOutput.ReadToEnd();
                process1.WaitForExit();
                process1.Close();
                return str;
            }
            catch (Exception exception1)
            {
                return exception1.ToString();
            }
        }

        public static T Deserialize<T>(string jsonString)
        {
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                return (T) new DataContractJsonSerializer(typeof(T)).ReadObject(stream);
            }
        }

        public static string GetIP()
        {
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
                for (int i = 0; i < hostEntry.AddressList.Length; i++)
                {
                    if (hostEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        return hostEntry.AddressList[i].ToString();
                    }
                }
                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static uint GetPixelColor(Point pt, IntPtr t)
        {
            IntPtr dC = Win32Native.GetDC(t);
            Win32Native.ReleaseDC(IntPtr.Zero, dC);
            return Win32Native.GetPixel(dC, pt.X, pt.Y);
        }

        public static string Serialize(object objectToSerialize)
        {
            string str;
            using (MemoryStream stream = new MemoryStream())
            {
                new DataContractJsonSerializer(objectToSerialize.GetType()).WriteObject(stream, objectToSerialize);
                stream.Position = 0L;
                using (StreamReader reader = new StreamReader(stream))
                {
                    str = reader.ReadToEnd();
                }
            }
            return str;
        }
    }
}

