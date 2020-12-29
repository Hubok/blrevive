﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bootstrapper
{
    public partial class LauncherUI : Form
    {
        public LauncherUI()
        {
            InitializeComponent();
        }

        private void BGLaunchButton_Click(object sender, EventArgs e)
        {
            GameLauncher.LaunchBotgame((string)BGMapsCombo.SelectedItem, (string)BGGamemodesCombo.SelectedItem, (int)BGBotCountNum.Value, () => this.Close());
        }

        private void ClientLaunchButton_Click(object sender, EventArgs e)
        {
            WriteUsername(ClientPlayerNameTextBox.Text);

            if (ClientCustomURLCheckBox.Checked)
            {
                GameLauncher.LaunchClient("", ClientCustomURLTextBox.Text);
            }
            else
            {
                string options = $"?Name={ClientPlayerNameTextBox.Text}{ClientLaunchOptionsTextBox.Text}";
                GameLauncher.LaunchClient(ClientIPTextBox.Text, options);
            }
        }

        private void ServerLaunchButton_Click(object sender, EventArgs e)
        {
            if (ServerCustomURLCheckbox.Checked)
                GameLauncher.LaunchServer(ServerLaunchOptionsTextBox.Text);
            else
                GameLauncher.LaunchServer((string)ServerMapsCombo.SelectedItem, (string)ServerGamemodesCombo.SelectedItem, (int)ServerBotCountNum.Value, (int)ServerPlayerCountNum.Value, ServerLaunchOptionsTextBox.Text);
        }

        private void ClientCustomURLCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ClientIPTextBox.Enabled = !ClientIPTextBox.Enabled;
            ClientPlayerNameTextBox.Enabled = !ClientPlayerNameTextBox.Enabled;
            ClientLaunchOptionsTextBox.Enabled = !ClientLaunchOptionsTextBox.Enabled;
            ClientCustomURLTextBox.Enabled = !ClientCustomURLTextBox.Enabled;
        }

        private void ServerCustomURLCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            ServerGamemodesCombo.Enabled = !ServerGamemodesCombo.Enabled;
            ServerMapsCombo.Enabled = !ServerMapsCombo.Enabled;
            ServerBotCountNum.Enabled = !ServerBotCountNum.Enabled;
            ServerPlayerCountNum.Enabled = !ServerPlayerCountNum.Enabled;
            ServerLaunchOptionsTextBox.Enabled = !ServerLaunchOptionsTextBox.Enabled;
            ServerCustomURLTextBox.Enabled = !ServerCustomURLTextBox.Enabled;
        }
        private void LauncherUI_Load(object sender, EventArgs e)
        {
            BGGamemodesCombo.DataSource = GameLauncher.GetConfig().Gamemodes;
            BGGamemodesCombo.SelectedIndex = 1;
            BGMapsCombo.DataSource = GameLauncher.GetConfig().Maps;
            BGMapsCombo.SelectedIndex = 9;
            BGBotCountNum.Value = 10;

            ClientPlayerNameTextBox.Text = GameLauncher.GetConfig().Username;

            ServerGamemodesCombo.DataSource = GameLauncher.GetConfig().Gamemodes;
            ServerGamemodesCombo.SelectedIndex = 1;
            ServerMapsCombo.DataSource = GameLauncher.GetConfig().Maps;
            ServerMapsCombo.SelectedIndex = 9;
            ServerBotCountNum.Value = 10;
            ServerPlayerCountNum.Value = 16;
        }

        private void WriteUsername(string Username)
        {
            Config LauncherConfig = GameLauncher.GetConfig();
            LauncherConfig.Username = Username;
            string output = JsonConvert.SerializeObject(LauncherConfig, Formatting.Indented);
            File.WriteAllText("LauncherConfig.json", output);
        }
    }
}
