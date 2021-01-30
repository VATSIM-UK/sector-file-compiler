
function getBuildVersion()
{
    # If we're on main, invoke semantic release to get the next version
    if (${env:GITHUB_REF} -eq "refs/head/main")
    {
        $semanticRelease = npx semantic-release@17 --dry-run | Select-String -Pattern "Published release ((0|[1-9]\d*)\.(0|[1-9]\d*)\.(0|[1-9]\d*)(?:-((?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+([0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?)"
        $buildVersion = $semanticRelease.Matches.Groups[1].value
    } else
    {
        return "1.0.0"
    }
}

$buildVersion = getBuildVersion();
echo "Building version $buildVersion"

# Publish all the versions
dotnet publish -r win-x64 -c Release -p:Version="$buildVersion" -p:PublishSingleFile=true -p:IncludeAllContentForSelfExtract=true -p:SelfContained=true -p:PublishTrimmed=true -o publish/windows src/CompilerCli/CompilerCli.csproj
dotnet publish -r linux-x64 -c Release -p:Version="$buildVersion" -p:PublishSingleFile=true -p:IncludeAllContentForSelfExtract=true -p:SelfContained=true -p:PublishTrimmed=true -o publish/linux src/CompilerCli/CompilerCli.csproj
dotnet publish -r osx-x64 -c Release -p:Version="$buildVersion" -p:PublishSingleFile=true -p:IncludeAllContentForSelfExtract=true -p:SelfContained=true -p:PublishTrimmed=true -o publish/osx src/CompilerCli/CompilerCli.csproj