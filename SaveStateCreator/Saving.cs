using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SaveStateCreator
{
    internal static class Saving
    {
        public static readonly string appdataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BTD6SaveStateCreator");
        public static void CreateSaveState(string btdSavesPath, string saveName)
        {
            string saveToPath = Path.Combine(appdataPath, saveName);
            if (!Directory.Exists(appdataPath))
                Directory.CreateDirectory(appdataPath);

            if (File.Exists(btdSavesPath))
            {
                File.Copy(btdSavesPath, saveToPath, true);
            }
            else
                throw new ArgumentException($"The file {btdSavesPath} does not exist or is not a valid save file.", nameof(btdSavesPath));
        }

        public static void LoadSaveState(string btdSavesPath, string saveName)
        {
            string saveToPath = Path.Combine(appdataPath, saveName);
            if (!File.Exists(saveToPath))
                throw new ArgumentException($"The path {saveToPath} is not a valid save file.", nameof(saveToPath));
            File.Copy(saveToPath, btdSavesPath, true);
        }

        public static string? GetSavePath()
        {
            var steamPath = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam")?.GetValue("SteamPath") as string;
            if (steamPath == null) return null;

            var userdata = Path.Combine(steamPath, "userdata");
            var userId = Directory.GetDirectories(userdata)?[0];
            if (userId == null) return null;

            return Path.Combine(userId, "960090", "local", "link", "PRODUCTION", "current", "Profile.Save");
        }
    }
}
