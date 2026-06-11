using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Components;

using static Nuke.Common.Tools.DotNet.DotNetTasks;

class Build : NukeBuild, IHazSolution {
    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Parameter] readonly AbsolutePath DocsCaches = RootDirectory / Path.Combine("docs", "api");
    [Parameter] readonly string DocsConfig = Path.Combine("docs", "docfx.json");
    [Parameter] readonly string DocsOutput = Path.Combine("docs", "_site");

    [Parameter] readonly AbsolutePath Output = RootDirectory / "bin";

    IReadOnlyCollection<Project> BuildProjects;
    [Parameter] readonly AbsolutePath PublishOutput;

    public Build() {
        AbsolutePath appdataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        PublishOutput = appdataFolder / "pyRevit" / "Extensions" / "BIM4Everyone.lib" / "dosymep_libs" / "libs";
    }

    protected override void OnBuildInitialized() {
        BuildProjects = [
            ..((IHazSolution) this).Solution.AllProjects
            .Where(item => item.Parent.ToString()?.Equals("src") == true)
        ];
    }

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() => {
            Output.CreateOrCleanDirectory();
            (RootDirectory / DocsOutput).CreateOrCleanDirectory();
            DocsCaches.GlobFiles("**/*.yml").DeleteFiles();
            RootDirectory.GlobDirectories("**/bin", "**/obj")
                .Where(item => item != RootDirectory / "build" / "bin")
                .Where(item => item != RootDirectory / "build" / "obj")
                .DeleteDirectories();
        });

    Target Restore => _ => _
        .Executes(() => {
            DotNetRestore(s => s
                .SetProjectFile(((IHazSolution) this).Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() => {
            DotNetBuild(s => s
                .EnableForce()
                .DisableNoRestore()
                .SetConfiguration(Configuration)
                .CombineWith(BuildProjects,
                    (s, p) => s
                        .SetProjectFile(p)
                        .SetOutputDirectory(Output)));
        });

    Target Tests => _ => _
        .DependsOn(Compile)
        .Executes(() => {
            DotNetTest(s => s
                .EnableNoBuild()
                .EnableNoRestore()
                .SetConfiguration(Configuration)
                .SetProjectFile(((IHazSolution) this).Solution));
        });

    Target Publish => _ => _
        // .DependsOn(Tests)
        .DependsOn(Compile)
        .OnlyWhenStatic(() => IsLocalBuild)
        .Executes(() => {
            DotNetPublish(s => s
                .EnableForce()
                .DisableNoRestore()
                .SetConfiguration(Configuration)
                .CombineWith(BuildProjects,
                    (s, p) => s
                        .SetProject(p)
                        .SetOutput(PublishOutput)));
        });


    Target DocsCompile => _ => _
        .DependsOn(Compile)
        .Executes(() => {
            ProcessTasks.StartProcess(
                "docfx",
                DocsConfig
                + (IsLocalBuild
                    ? " --serve"
                    : string.Empty),
                RootDirectory).WaitForExit();

            // DocFXBuild(s => s
            //     .EnableForceRebuild()
            //     .SetServe(IsLocalBuild)
            //     .SetOutputFolder(DocsOutput)
            //     .SetProcessWorkingDirectory(RootDirectory)
            // );
        });

    /// Support plugins are available for:
    /// - JetBrains ReSharper        https://nuke.build/resharper
    /// - JetBrains Rider            https://nuke.build/rider
    /// - Microsoft VisualStudio     https://nuke.build/visualstudio
    /// - Microsoft VSCode           https://nuke.build/vscode
    public static int Main() => Execute<Build>(x => x.Compile);
}