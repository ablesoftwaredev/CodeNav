﻿using System;
using System.Runtime.InteropServices;
using System.Threading;
using EnvDTE;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace CodeNav.ToolWindow
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(CodeNavToolWindow))]
    [Guid(PackageGuidString)]
    public sealed class CodeNavToolWindowPackage : AsyncPackage
    {
        public const string PackageGuidString = "5c1c8131-1371-4401-8a3e-70e47c8ac0ec";
        public IComponentModel ComponentModel;
        
        public CodeNavToolWindowPackage()
        {
        }

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            ComponentModel = await GetServiceAsync(typeof(SComponentModel)) as IComponentModel;

            CodeNavToolWindowCommand.Initialize(this);
            await base.InitializeAsync(cancellationToken, progress);
        }

        #endregion
    }
}
