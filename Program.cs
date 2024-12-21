using Microsoft.WindowsAPICodePack.Dialogs;
using RobloxFiles;
using RoXMX.Utilities;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoXMX
{
    internal class Program
    {
        public static int InstanceCount = 0;
        public static string RojoVersion = "v7.4.4"; // Default Rojo Version

        [STAThread]
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            RobloxFile.LogErrors = true;
            Logger.Info($"RoXMX by ninjaninja140 - Version {Configuration.Version}");
            Logger.Info("Select a Roblox file to port to a Rojo Project.");
            Logger.Info("Model files and their XML (rbxmx) equivalents are able to be processed through this program.");
            Console.WriteLine();

            OpenFileDialog OpenRobloxFile = new OpenFileDialog {
                Title = "Open Roblox Model or Place File",
                Filter = "Roblox XML Files (*.rbxmx)|*.rbxmx|Roblox Files (*.rbxm)|*.rbxm",
                FilterIndex = 0,
                RestoreDirectory = true
            };

            Application.EnableVisualStyles();

            if (OpenRobloxFile.ShowDialog() == DialogResult.OK)
            {
                Logger.Info($"Selected file: {OpenRobloxFile.FileName}");
            }
            else
            {
                Logger.Info("No file selected.");
                Process.Exit(1);
            }

            Logger.Info($"Reading \"{OpenRobloxFile.FileName}\", this could take a minute...");
            RobloxFile file = RobloxFile.Open($@"{OpenRobloxFile.FileName}");

            Logger.Info("File loaded successfully.");

            Logger.Info("Identifying instances...");


            static void TraverseRobloxFile(RobloxFile rofile)
            {
                static void TraverseRobloxInstance(Instance instance, string parent)
                {
                    foreach (var child in instance.Children)
                    {
                        InstanceCount++;
                        Logger.BackInfo($"Reading Instances... ({InstanceCount})");
                        TraverseRobloxInstance(child, $"{parent} > {child.Name}");
                    }
                }

                foreach (var child in rofile.Children)
                {
                    InstanceCount++;
                    Logger.BackInfo($"Identifying Instances... ({InstanceCount})");
                    TraverseRobloxInstance(child, child.Name);
                }
            }

            TraverseRobloxFile(file);

            Console.WriteLine();
            Logger.Info($"Identified {InstanceCount} instances.");

            CommonOpenFileDialog SaveFolder = new CommonOpenFileDialog
            {
                Title = "Select a Folder to store the Rojo Project",
                RestoreDirectory = true,
                IsFolderPicker = true
            };

            if (SaveFolder.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Logger.Info($"Selected Folder: {Path.GetDirectoryName(SaveFolder.FileName)}");
            }
            else
            {
                Logger.Info("No folder selected.");
                Process.Exit(1);
            }

            string workspaceName = Path.GetFileNameWithoutExtension(OpenRobloxFile.FileName) + "-Workspace";

            Logger.Info($"Creating Rojo Project to \"{SaveFolder.FileName + "\\" + workspaceName}\"...");
           

            if (Directory.Exists(SaveFolder.FileName + "\\" + workspaceName))
            {
                Logger.Error($"Project Folder already exists at: {SaveFolder.FileName + "\\" + workspaceName}");
                Process.Exit(1);
            }

            DirectoryInfo workspace = Directory.CreateDirectory(SaveFolder.FileName + "\\" + workspaceName);
            Logger.Info("Created Workspace folder successfully");

            Logger.Info("Checking for internet connection to get the latest Rojo version tag...");
            
            if (!Internet.CheckForInternetConnection())
            {
                RojoVersion = Configuration.RojoVersionFallback;
                Logger.Error($"No internet connection detected, using fallback Rojo version ({RojoVersion})");
            } else
            {
                RojoVersion = await Internet.GetLatestRojoVersion();

                if (RojoVersion == "unknown")
                {
                    RojoVersion = Configuration.RojoVersionFallback;
                    Logger.Error($"Failed to get the latest Rojo version (unable to read), using fallback Rojo version ({RojoVersion})");

                }
                else if (RojoVersion == "none")
                {
                    RojoVersion = Configuration.RojoVersionFallback;
                    Logger.Error($"Failed to get the latest Rojo version (request failed), using fallback Rojo version ({RojoVersion})");

                }
                else
                {
                    Logger.Info($"Retrieved latest Rojo Version: ({RojoVersion})");
                }
            }

            Logger.Info("Creating Rojo Project...");

            Directory.CreateDirectory(workspace.FullName + "\\src");

            Utilities.Templates.Project.SetContext(workspaceName);
            Utilities.Templates.Aftman.SetContext(RojoVersion);

            File.WriteAllText(workspace.FullName + "\\default.project.json", Utilities.Templates.Project.SetContext(workspaceName));
            File.WriteAllText(workspace.FullName + "\\.gitignore", Utilities.Templates.GitIgnore.Content);
            File.WriteAllText(workspace.FullName + "\\aftman.toml", Utilities.Templates.Aftman.SetContext(RojoVersion));

            Process.Exit(0);
        }
    }
}
