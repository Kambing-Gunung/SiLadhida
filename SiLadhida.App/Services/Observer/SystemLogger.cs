using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SiLadhida.App.Services.Observer
{
    public class SystemLogger : IObserver
    {
        public void UpdateNotification(string message)
        {
            var basePath = AppContext.BaseDirectory;
            var projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\.."));
            var folderPath = Path.Combine(projectPath, "Services", "Observer");
            var filePath = Path.Combine(folderPath, "history_aktivitas.txt");
            Directory.CreateDirectory(folderPath);

            string logText = $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] Aktivitas: {message}\n";
            File.AppendAllText(filePath, logText);

            Console.WriteLine($"[AUDIT LOG: {DateTime.Now}] Aktivitas: {message}");
        }
    }
}
