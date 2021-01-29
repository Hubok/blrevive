﻿using Avalonia;
using Avalonia.Logging.Serilog;
using Launcher;
using Utils;

// Initialization code. Don't use any Avalonia, third-party APIs or any
// SynchronizationContext-reliant code before AppMain is called: things aren't initialized
// yet and stuff might break.
if(args.Length > 0)
    CLI.Run(args);
else
    BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

// Avalonia configuration, don't remove; also used by visual designer.
static AppBuilder BuildAvaloniaApp()
    => AppBuilder.Configure<App>()
        .UsePlatformDetect()
        .LogToDebug();