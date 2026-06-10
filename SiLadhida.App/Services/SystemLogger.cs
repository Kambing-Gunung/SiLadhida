using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SiLadhida.App.Services
{
    public class SystemLogger : IObserver
    {
        public void UpdateNotification(string message)
        {
            Console.WriteLine($"[AUDIT LOG] {DateTime.Now}: {message}");

            string logText = $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] Aktivitas: {message}\n";
            File.AppendAllText("history_aktivitas.txt", logText);
        }
    }
}
