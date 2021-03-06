﻿using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace Application.Logs
{
    public class Logger
    {
        private static int count = 0;

        public static void AddLog(string message, string methodLocation)
        {            
            string path = Directory.GetCurrentDirectory();

            string fileName = "log" + DateTime.Today.ToString("yyyyMMdd") + $"_[{count}]" + ".txt";

            string lines = $"[{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToShortDateString()}]" +
                $"[{methodLocation}] : [{message}]";

            VerifyDir(path);

            try
            {
                StreamWriter file = new StreamWriter(path + fileName, true);
                file.WriteLine(lines);
                file.Close();

                byte[] fileBytes = File.ReadAllBytes(path + fileName);

                if (fileBytes.Length >= 30_000)
                {
                    count++;
                }
            }
            catch (Exception) { }
        }

        public static void VerifyDir(string path)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                if (!dir.Exists)
                {
                    dir.Create();
                }
            }
            catch { }
        }       

    }
}


