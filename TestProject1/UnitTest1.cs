using System.Reactive;
using System.Reactive.Linq;
using AvaloniaApplication13.ViewModels;
using AvaloniaApplication13.ViewModels.Feature;
using DynamicData.Binding;
using FluentAssertions;
using Moq;
using NBitcoin;
using NBitcoin.Scripting;
using ReactiveUI;

namespace TestProject1
{
    public class AddressViewModelTests
    {
        [Theory]
        [InlineData("miner8VH6WPrsQ1Fxqb7MPgJEoFYX2RCkS", "miner8VH6WPrsQ1Fxqb7MPgJEoFYX2RCkS", false)]
        [InlineData("miner8VH6WPrsQ1Fxqb7MPgJEoFYX2RCkS", "", false)]
        [InlineData("miner8VH6WPrsQ1Fxqb7MPgJEoFYX2RCkS", "mzMcNcKMXQdwMpdgknDQnHZiMxnQKWZ4vh", true)]
        [InlineData("bitcoin:tb1q0382a3m2jzvyk5lkea5h5jcht88xa6l0jufgwx?amount=0.00010727&pj=https://payjoin.test.kukks.org/BTC/pj", "bitcoin:tb1q0382a3m2jzvyk5lkea5h5jcht88xa6l0jufgwx?amount=0.00010727&pj=https://payjoin.test.kukks.org/BTC/pj", false)]
        [InlineData("bitcoin:tb1q0382a3m2jzvyk5lkea5h5jcht88xa6l0jufgwx?amount=0.00010727&pj=https://payjoin.test.kukks.org/BTC/pj", "miner8VH6WPrsQ1Fxqb7MPgJEoFYX2RCkS", true)]
        [InlineData("miner8VH6WPrsQ1Fxqb7MPgJEoFYX2RCkS", "bitcoin:tb1q0382a3m2jzvyk5lkea5h5jcht88xa6l0jufgwx?amount=0.00010727&pj=https://payjoin.test.kukks.org/BTC/pj", true)]
        [InlineData("", "miner8VH6WPrsQ1Fxqb7MPgJEoFYX2RCkS", true)]
        public async Task Can_paste(string currentAddress, string clipboardAddress, bool expectedCanPaste)
        {
            var clipboardObserver = ClipboardWith(clipboardAddress);
            var sut = new NewAddressViewModel(clipboardObserver, Network.TestNet);

            sut.RawAddress = currentAddress;

            var canPaste = await sut.HasNewContent.Take(1);
            canPaste.Should().Be(expectedCanPaste);
        }

        [Fact]
        public void Initial_value_is_empty()
        {
            var clipboardObserver = ClipboardWith("");
            var sut = new NewAddressViewModel(clipboardObserver, Network.TestNet);
            sut.RawAddress.Should().BeEmpty();
        }

        [Fact]
        public void Pasting_regular_address()
        {
            var clipboardObserver = ClipboardWith("mzMcNcKMXQdwMpdgknDQnHZiMxnQKWZ4vh");
            var sut = new NewAddressViewModel(clipboardObserver, Network.TestNet);
            sut.Paste();
            sut.RawAddress.Should().Be("mzMcNcKMXQdwMpdgknDQnHZiMxnQKWZ4vh");
            sut.Amount.Should().Be(0);
        }

        [Fact]
        public void Pasting_payjoin_address()
        {
            var clipboardObserver = ClipboardWith("bitcoin:tb1q0382a3m2jzvyk5lkea5h5jcht88xa6l0jufgwx?amount=0.00010727&pj=https://payjoin.test.kukks.org/BTC/pj");
            var sut = new NewAddressViewModel(clipboardObserver, Network.TestNet);
            sut.Paste();
            sut.RawAddress.Should().Be("tb1q0382a3m2jzvyk5lkea5h5jcht88xa6l0jufgwx");
            sut.Amount.Should().Be((decimal) 0.00010727);
        }

        [Fact]
        public async Task After_pasting_payjoin_address_cannot_paste()
        {
            var clipboardObserver = ClipboardWith("bitcoin:tb1q0382a3m2jzvyk5lkea5h5jcht88xa6l0jufgwx?amount=0.00010727&pj=https://payjoin.test.kukks.org/BTC/pj");
            var sut = new NewAddressViewModel(clipboardObserver, Network.TestNet);
            sut.Paste();

            var canPaste = await sut.HasNewContent.Take(1);
            canPaste.Should().BeFalse();
        }

        private static IClipboardObserver ClipboardWith(string contents)
        {
            return Mock.Of<IClipboardObserver>(x => x.Contents == Observable.Return(contents));
        }
    }

    public class NewAddressViewModel : ViewModelBase
    {
        private readonly ObservableAsPropertyHelper<Address> address;
        private readonly ObservableAsPropertyHelper<Address> clipboardAddress;
        private string rawAddress = "";

        public NewAddressViewModel(IClipboardObserver clipboardObserver, Network network)
        {
            var monitor = new AddressParser(network);

            AddressChanged = this.WhenAnyValue(model => model.RawAddress)
                .Select(s => monitor.GetAddress(s));

            address = AddressChanged.ToProperty(this, model => model.Address);
            
            var clipboardAddressChanged = clipboardObserver.Contents.Select(s => monitor.GetAddress(s));
            clipboardAddress = clipboardAddressChanged.ToProperty(this, model => model.ClipboardContents);

            HasNewContent = clipboardAddressChanged
                .CombineLatest(AddressChanged, (clipboard, current) =>
                    !Equals(clipboard, current) && clipboard is not InvalidAddress);
        }

        public Address Address => address.Value;

        public Address ClipboardContents => clipboardAddress.Value;

        public IObservable<Address> AddressChanged { get; }

        public string RawAddress
        {
            get => rawAddress;
            set => this.RaiseAndSetIfChanged(ref rawAddress, value);
        }

        public IObservable<bool> HasNewContent { get; }

        public void Paste()
        {
            if (ClipboardContents is PayjoinAddress pj)
            {
                RawAddress = pj.Request.Address;
                Amount = pj.Request.Amount;
            }
            else if (ClipboardContents is AvaloniaApplication13.ViewModels.Feature.BitcoinAddress bt)
            {
                RawAddress = bt.Address;
            }
        }

        public decimal Amount { get; set; }
    }
}