using Microsoft.Build.Locator;
using Xunit;

namespace IntelliTect.Multitool.Tests;

[CollectionDefinition(CollectionName)]
public class MSBuildCollection : ICollectionFixture<MSBuildFixture>
{
    public const string CollectionName = "MSBuild";
}

public class MSBuildFixture : IDisposable
{
    public MSBuildFixture()
    {
        if (!MSBuildLocator.IsRegistered)
            MSBuildLocator.RegisterDefaults();
    }

    public void Dispose() { }
}
