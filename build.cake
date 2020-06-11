#tool "nuget:?package=OpenCover"
#tool nuget:?package=Codecov
#addin nuget:?package=Cake.Codecov

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var cleanTask = Task("Clean")
    .Does(() =>
{
    Information("Cleaning solution");
    DotNetCoreClean("./");
});

var nugetTask = Task("NuGetRestorePackages")
    .IsDependentOn(cleanTask)
    .Does(() =>
{
    Information("Restoring Nuget packages for solution");
    DotNetCoreRestore("./MtgApiManager.sln");
});

var buildTask = Task("BuildSolution")
    .IsDependentOn(nugetTask)
    .Does(() =>
{
    Information("Building Library");
    DotNetCoreBuild(
        "./MtgApiManager.Lib/MtgApiManager.Lib.csproj",
        new DotNetCoreBuildSettings()
        {
            Configuration = configuration,
            NoRestore = true,
            ArgumentCustomization = arg => arg.AppendSwitch("/p:DebugType","=","Full")
        });

    DotNetCoreBuild(
        "./MtgApiManager.Lib.Test/MtgApiManager.Lib.Test.csproj",
        new DotNetCoreBuildSettings()
        {
            Configuration = configuration,
            NoRestore = true,
            ArgumentCustomization = arg => arg.AppendSwitch("/p:DebugType","=","Full")
        });
});


var unitTestsTask = Task("RunUnitTests")
    .IsDependentOn(buildTask)
    .Does(() =>
{
    Information("Running xunit tests");

    OpenCover(tool => {
        tool.DotNetCoreTest(
            "./MtgApiManager.Lib.Test/MtgApiManager.Lib.Test.csproj",   
            new DotNetCoreTestSettings()
            {
                Configuration = configuration,
                NoBuild = true
            });
        },
        new FilePath("./OpenCoverResults.xml"),
        new OpenCoverSettings() 
        { 
            SkipAutoProps = true,
            Register = "user",
            OldStyle = true 
        }
        .WithFilter("+[MtgApiManager.Lib]*")
        .WithFilter("-[MtgApiManager.Lib.Test]*"));

    Codecov("./OpenCoverResults.xml", "f7c9d882-99e0-4a44-a651-16ed7cee7bc4");
});

Task("Default")
    .IsDependentOn(unitTestsTask);

RunTarget(target);