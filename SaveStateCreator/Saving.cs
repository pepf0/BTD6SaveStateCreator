using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveStateCreator
{
    internal static class Saving
    {
        public static readonly string appdataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BTD6SaveStateCreator");
        public static readonly string saveToPath = Path.Combine(appdataPath, "Profile.Save");
        public static void CreateSaveState(string btdSavesPath)
        {
            string oldSavesPath = Path.Combine(saveToPath, "OldSaves");
            if (!Directory.Exists(saveToPath))
                Directory.CreateDirectory(saveToPath);

            if (!Directory.Exists(oldSavesPath))
                Directory.CreateDirectory(oldSavesPath);

            if (File.Exists(btdSavesPath))
            {
                if (File.Exists(saveToPath))
                {
                    string nameWithoutExtension = Path.GetFileNameWithoutExtension(saveToPath);
                    string extension = Path.GetExtension(saveToPath);

                    int index = 1;
                    string newOldPath;
                    do
                    {
                        string newFileName = $"{nameWithoutExtension}{index}{extension}";
                        newOldPath = Path.Combine(oldSavesPath, newFileName);
                        index++;
                    } while (File.Exists(newOldPath));

                    File.Move(saveToPath, newOldPath);
                }
                File.Copy(btdSavesPath, saveToPath, true);
            }
            else
                throw new ArgumentException($"The file {btdSavesPath} does not exist or is not a valid save file.", nameof(btdSavesPath));
        }

        public static void LoadSaveState(string btdSavesPath, string saveToPath)
        {
            string saveFileName = Path.GetFileName(btdSavesPath);
            string sourcePath = Path.Combine(saveToPath, saveFileName);

            if (!File.Exists(sourcePath))
                throw new ArgumentException($"The path {saveToPath} does not contain a valid save file.", nameof(saveToPath));
            File.Copy(sourcePath, btdSavesPath, true);
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
