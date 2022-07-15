using System.Reactive.Linq;
using AvaloniaApplication13.ViewModels;
using AvaloniaApplication13.ViewModels.Feature;
using DynamicData.Binding;
using FluentAssertions;
using Moq;
using NBitcoin;
using ReactiveUI;

namespace TestProject1
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("miner8VH6WPrsQ1Fxqb7MPgJEoFYX2RCkS", "miner8VH6WPrsQ1Fxqb7MPgJEoFYX2RCkS", false)]
        [InlineData("miner8VH6WPrsQ1Fxqb7MPgJEoFYX2RCkS", "", false)]
        [InlineData("miner8VH6WPrsQ1Fxqb7MPgJEoFYX2RCkS", "mzMcNcKMXQdwMpdgknDQnHZiMxnQKWZ4vh", true)]
        [InlineData("bitcoin:tb1q0382a3m2jzvyk5lkea5h5jcht88xa6l0jufgwx?amount=0.00010727&pj=https://payjoin.test.kukks.org/BTC/pj", "bitcoin:tb1q0382a3m2jzvyk5lkea5h5jcht88xa6l0jufgwx?amount=0.00010727&pj=https://payjoin.test.kukks.org/BTC/pj", false)]
        [InlineData("bitcoin:tb1q0382a3m2jzvyk5lkea5h5jcht88xa6l0jufgwx?amount=0.00010727&pj=https://payjoin.test.kukks.org/BTC/pj", "miner8VH6WPrsQ1Fxqb7MPgJEoFYX2RCkS", true)]
        [InlineData("miner8VH6WPrsQ1Fxqb7MPgJEoFYX2RCkS", "bitcoin:tb1q0382a3m2jzvyk5lkea5h5jcht88xa6l0jufgwx?amount=0.00010727&pj=https://payjoin.test.kukks.org/BTC/pj", true)]
        [InlineData("", "miner8VH6WPrsQ1Fxqb7MPgJEoFYX2RCkS", true)]
        public async Task Test1(string currentAddress, string clipboardAddress, bool canPaste)
        {
            var clipboardObserver = Mock.Of<IClipboardObserver>(x => x.Contents == Observable.Return(clipboardAddress));
            var sut = new NewAddressViewModel(clipboardObserver, Network.TestNet);
            sut.RawAddress = currentAddress;

            var canPast = await sut.CanPaste.Take(1);
            canPast.Should().Be(canPaste);
        }
    }

    public class NewAddressViewModel : ViewModelBase
    {
        public NewAddressViewModel(IClipboardObserver clipboardObserver, Network network)
        {
            var monitor = new AddressParser(network);

            AddressChanged = this.WhenAnyValue(model => model.RawAddress)
                .Select(s => monitor.GetAddress(s));

            CanPaste = clipboardObserver.Contents.Select(s => monitor.GetAddress(s))
                .CombineLatest(AddressChanged, (cpText, curAddr) =>
                    !Equals(cpText, curAddr) && cpText is not InvalidAddress);
        }

        public IObservable<Address> AddressChanged { get; }
        public string RawAddress { get; set; }
        public IObservable<bool> CanPaste { get; }
    }
}