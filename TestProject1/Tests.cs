using AvaloniaApplication13.ViewModels.DestinationEntry;
using FluentAssertions;
using NBitcoin;

namespace TestProject1;

public class Tests
{
    private const string BtcAddress = "tb1q0382a3m2jzvyk5lkea5h5jcht88xa6l0jufgwx";

    [Fact]
    public void Test()
    {
        Network network = Network.TestNet;
        var sut = new MutableAddressHost(new FullAddressParser(network));
        sut.Text = BtcAddress;
        sut.ParsedAddress.Should().Be(new Address(BtcAddress));
    }

    [Fact]
    public void Address_should_match()
    {
        Network network = Network.TestNet;
        var sut = new MutableAddressHost(new FullAddressParser(network));
        sut.Text = BtcAddress;
        sut.ParsedAddress.Should().Be(new Address(BtcAddress));
    }
}