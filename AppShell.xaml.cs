﻿using CommunityToolkit.Mvvm.DependencyInjection;

namespace RadioRecord;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(ListDetailDetailPage), typeof(ListDetailDetailPage));
    } 
}
