using System.Reactive.Linq;
using AvaloniaApplication13.ViewModels.DestinationEntry;
using FluentAssertions;

namespace TestProject1;

public class ContentCheckerTests
{
    [Theory]
    [InlineData("current", "new", true)]
    [InlineData("one", "one", false)]
    [InlineData("", "", false)]
    public async Task Is_new_content(string current, string @new, bool expectedIsNew)
    {
        var sut = new ContentChecker<string>(Observable.Return(@new), Observable.Return(current), _ => true);
        var isNew = await sut.HasNewContent.Take(1);
        isNew.Should().Be(expectedIsNew);
    }
}