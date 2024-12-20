using RobloxFiles;
using RoXMX.Utilities;
using System;
using System.Windows.Forms;

namespace RoXMX
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Logger.Info($"RoXMX by ninjaninja140 - Version {Configuration.Version}");

            OpenFileDialog openFileDialog = new OpenFileDialog {
                Title = "Open Roblox Model or Place File",
                Filter = "Roblox XML Files (*.rbxmx, *.rbxlx)|*.rbxmx;*.rbxlx|Roblox Files (*.rbxm, *.rbxl)|*.rbxm;*.rbxl",
                FilterIndex = 0,
                RestoreDirectory = true
            };

            Application.EnableVisualStyles();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Logger.Info($"Selected file: {openFileDialog.FileName}");
            }
            else
            {
                Logger.Info("No file selected.");
                Process.Exit(1);
            }

            RobloxFile file = RobloxFile.Open($@"{openFileDialog.FileName}");

            Console.Write(file);

            Process.Exit(0);
        }
    }
}
