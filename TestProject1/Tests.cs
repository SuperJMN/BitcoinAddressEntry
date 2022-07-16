using AvaloniaApplication13.ViewModels.Feature;
using FluentAssertions;

namespace TestProject1;

public class Tests
{
    private const string BtcAddress = "tb1q0382a3m2jzvyk5lkea5h5jcht88xa6l0jufgwx";

    [Fact]
    public void Test()
    {
        var sut = new Sut();
        sut.Text = BtcAddress;
        sut.Address.Should().Be(new NewAddress(BtcAddress));
    }

    [Fact]
    public void Address_should_match()
    {
        var sut = new Sut();
        sut.Text = BtcAddress;
        sut.Address.Should().Be(new NewAddress(BtcAddress));
    }
}