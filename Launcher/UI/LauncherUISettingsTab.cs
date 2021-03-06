﻿using System;
using System.IO;
using Avalonia.Interactivity;
using Avalonia.Controls;
using Avalonia.Input;
using Launcher.Configuration;
using Launcher.Utils;

namespace Launcher.UI
{
    public partial class MainWindow : Window
    {
        private void SettingsTabBlacklightDirectoryTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            var SettingsTabBlacklightDirectoryTextBox = this.Find<TextBox>("SettingsTabBlacklightDirectoryTextBox");

            if (e.Key == Key.Enter)
                TrySetGameDirectory(SettingsTabBlacklightDirectoryTextBox.Text);
        }

        private async void SettingsTabBlacklightDirectoryBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var LauncherWindow = this.Find<Window>("LauncherWindow");

            var folderDialog = new OpenFolderDialog();
            folderDialog.Directory = Config.App.GameFolder;

            var result = await folderDialog.ShowAsync(LauncherWindow);
            if (result != null)
            {
                // http://reference.avaloniaui.net/api/Avalonia.Controls/OpenFolderDialog/
                TrySetGameDirectory(Path.GetFullPath(result));
            }
        }

        private bool TrySetGameDirectory(string path, bool displayErrors = true)
        {
            var SettingsTabBlacklightDirectoryTextBox = this.Find<TextBox>("SettingsTabBlacklightDirectoryTextBox");
            var PatchTabGameFileInputTextBox = this.Find<TextBox>("PatchTabGameFileInputTextBox");
            var PatchTabGameFileOutputTextBox = this.Find<TextBox>("PatchTabGameFileOutputTextBox");

            if (!Directory.Exists(path))
            {
                if (displayErrors)
                    MessageBox.Avalonia.MessageBoxManager.
                    GetMessageBoxStandardWindow("Error", "The path does not exist!")
                    .Show();
                return false;
            }


            if (!GameInstanceManager.IsValidGameDirectory(path))
            {
                if (displayErrors)
                    MessageBox.Avalonia.MessageBoxManager.
                    GetMessageBoxStandardWindow("Error", "The path you selected is not a valid blacklight installation directory!")
                    .Show();
                return false;
            }

            Config.App.GameFolder = path;
            Config.Save();

            SettingsTabBlacklightDirectoryTextBox.Text = path;

            if (String.IsNullOrWhiteSpace(PatchTabGameFileInputTextBox.Text))
                PatchTabGameFileInputTextBox.Text = Path.Join(path, "Binaries", "Win32", "FoxGame-win32-Shipping.exe");

            if (String.IsNullOrWhiteSpace(PatchTabGameFileOutputTextBox.Text))
                PatchTabGameFileOutputTextBox.Text = Path.Join(path, "Binaries", "Win32", "FoxGame-win32-Shipping-Patched.exe");

            return true;
        }
    }
}
