using System;
using System.Reactive;
using System.Reactive.Subjects;
using NBitcoin;
using ReactiveUI;

namespace AvaloniaApplication13.ViewModels.Feature
{
    public class PaymentViewModel : ViewModelBase
    {
        private string address;
        private decimal? amount;

        public PaymentViewModel(IAddressParser addressParser)
        {
            Network network = Network.TestNet;
            MutableAddressHost = new MutableAddressHost(network, addressParser);
            MutableAddressHost.Address.Subscribe(address =>
            {
                if (address is not null)
                {
                    Address = address.BtcAddress;

                    if (address.Amount is not null)
                    {
                        Amount = address.Amount.Value;
                    }
                }
            });

            var parser = addressParser;
            var clipboardContentChanged = new ClipboardObserver().ContentChanged;
            var clipboardContent = new BehaviorSubject<string>("");
            clipboardContentChanged.Subscribe(clipboardContent);
            var contentChecker = new ContentChecker<string>(clipboardContentChanged, MutableAddressHost.TextChanged, s => parser.GetAddress(s) is not null);
            HasNewContent = contentChecker.ActivatedWithNewContent;
            PasteCommand = ReactiveCommand.Create(() =>
            {
                MutableAddressHost.Text = clipboardContent.Value;
            });
        }

        public ReactiveCommand<Unit, Unit> PasteCommand { get; }

        public IObservable<bool> HasNewContent { get; }

        public MutableAddressHost MutableAddressHost { get; }

        public decimal? Amount
        {
            get => amount;
            set => this.RaiseAndSetIfChanged(ref amount, value);
        }

        public string Address
        {
            get => address;
            set => this.RaiseAndSetIfChanged(ref address, value);
        }
    }
}
