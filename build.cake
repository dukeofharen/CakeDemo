#addin "Cake.Powershell"
//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

var solutionFile = "./CakeBuildDemo.sln";

// Define directories.
var buildDir = Directory("./CakeBuildDemo/bin") + Directory(configuration);
var releaseDir = Directory("./releases") + Directory(configuration);

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
    CleanDirectory(releaseDir);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore(solutionFile);
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
      // Use MSBuild
      MSBuild(solutionFile, settings => settings.SetConfiguration(configuration));
});


Task("Run-Powershell")
    .IsDependentOn("Build")
    .Does(() => 
{
    StartPowershellFile("scripts/test.ps1");
});

Task("Run-Unit-Tests")
    .IsDependentOn("Run-Powershell")
    .Does(() =>
{
    MSTest("./**/bin/" + configuration + "/*.Tests.dll");
});

Task("Pack-Application")
    .IsDependentOn("Run-Unit-Tests")
    .Does(() =>
{

    Zip(buildDir, string.Format("{0}/application.zip", releaseDir));
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Pack-Application");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);