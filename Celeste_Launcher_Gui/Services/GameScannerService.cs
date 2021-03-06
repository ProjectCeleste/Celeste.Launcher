﻿using Celeste_Launcher_Gui.Helpers;
using Celeste_Launcher_Gui.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Celeste_Launcher_Gui.Services
{
    public class GameScannerService
    {
        private const string GameScannerBinaryName = "Celeste Game Scanner UI.exe";

        public static void StartGameScanner(string gamePath, bool isSteamInstallation)
        {
            if (GameScanInProgress())
            {
                // TODO: Show message dialogue
                return;
            }

            var language = Thread.CurrentThread.CurrentCulture.Name;

            var adminRequired = !FileSystem.IsWritableDirectory(gamePath);

            ProcesInvoker.StartNewProcessAsDialog(GameScannerBinaryName, $"\"{gamePath}\" \"{language}\" {isSteamInstallation}", adminRequired);
        }

        private static bool GameScanInProgress()
        {
            var processes = Process.GetProcessesByName(GameScannerBinaryName);
            return processes?.Length > 0;
        }
    }
}
