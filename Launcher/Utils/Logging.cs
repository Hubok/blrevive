﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Serilog;
using Configuration;

namespace Launcher
{
    /// <summary>
    /// Logic for logging. Don't try to use this class directly, instead use the static instance of Serilog (just Log)
    /// </summary>
    class Logging
    {
        /// <summary>
        /// relative path to log directory
        /// </summary>
        static string LogDirectory = $"Logs{Path.DirectorySeparatorChar}";

        /// <summary>
        /// absolute path to log directory
        /// </summary>
        static string LogFileDirectoryAbs = $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}{LogDirectory}";

        /// <summary>
        /// log file name
        /// </summary>
        static string LogFileName = $"{LogFileDirectoryAbs}BLReviveLauncher.log";

        /// <summary>
        /// Setup Serilog.
        /// </summary>
        /// <returns>wether setup succeeded</returns>
        public static bool Initialize()
        {
            if (!Directory.Exists(LogFileDirectoryAbs))
            {
                try
                {
                    Directory.CreateDirectory(LogFileDirectoryAbs);
                } catch (Exception ex)
                {
                    MessageBox.Avalonia.MessageBoxManager.
                    GetMessageBoxStandardWindow("Error", $"Logfile directory ({LogFileDirectoryAbs}) doesn't exist and failed to create!")
                    .Show();

                    MessageBox.Avalonia.MessageBoxManager.
                    GetMessageBoxStandardWindow("Error", ex.Message)
                    .Show();

                    Environment.Exit(2121800003);
                }
            }

            try
            {
                LoggerConfiguration loggerConfig = new LoggerConfiguration();
                loggerConfig.WriteTo.File(LogFileName, rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true);

                switch (Config.App.LogLevel)
                {
                    // verbose
                    case 0:
                        loggerConfig.MinimumLevel.Verbose();
                        break;
                    case 1:
                        loggerConfig.MinimumLevel.Debug();
                        break;
                    case 2:
                        loggerConfig.MinimumLevel.Warning();
                        break;
                    case 3:
                        loggerConfig.MinimumLevel.Error();
                        break;
                    case 4:
                        loggerConfig.MinimumLevel.Fatal();
                        break;
                }

                Log.Logger = loggerConfig.CreateLogger();
            }
            catch (Exception ex)
            {
                MessageBox.Avalonia.MessageBoxManager.
                GetMessageBoxStandardWindow("Error", "Failed to initialize logging system. Log file directory may not be writable.")
                .Show();

                MessageBox.Avalonia.MessageBoxManager.
                GetMessageBoxStandardWindow("Error", ex.Message)
                .Show();

                Environment.Exit(2121800004);
            }

            return true;
        }
    }
}