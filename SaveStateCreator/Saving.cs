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
        static void CreateSaveState(string btdSavesPath, string saveToPath)
        {
            string oldSavesPath = Path.Combine(saveToPath, "OldSaves");
            if (!Directory.Exists(saveToPath))
                Directory.CreateDirectory(saveToPath);

            if (!Directory.Exists(oldSavesPath))
                Directory.CreateDirectory(oldSavesPath);

            if (File.Exists(btdSavesPath))
            {
                string saveFileName = Path.GetFileName(btdSavesPath);
                string destinationPath = Path.Combine(saveToPath, saveFileName);

                if (File.Exists(destinationPath))
                {
                    string nameWithoutExtension = Path.GetFileNameWithoutExtension(saveFileName);
                    string extension = Path.GetExtension(saveFileName);

                    int index = 1;
                    string newOldPath;
                    do
                    {
                        string newFileName = $"{nameWithoutExtension}{index}{extension}";
                        newOldPath = Path.Combine(oldSavesPath, newFileName);
                        index++;
                    } while (File.Exists(newOldPath));

                    File.Move(destinationPath, newOldPath);
                }
                File.Copy(btdSavesPath, destinationPath, true);
            }
            else
                throw new ArgumentException($"The file {btdSavesPath} does not exist or is not a valid save file.", nameof(btdSavesPath));
        }
        static void LoadSaveState(string btdSavesPath, string saveToPath)
        {
            string saveFileName = Path.GetFileName(btdSavesPath);
            string sourcePath = Path.Combine(saveToPath, saveFileName);

            if (!File.Exists(sourcePath))
                throw new ArgumentException($"The path {saveToPath} does not contain a valid save file.", nameof(saveToPath));
                File.Copy(sourcePath, btdSavesPath, true);
        }
    }
}
