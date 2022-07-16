using System;
using System.Reactive.Linq;
using AvaloniaApplication13.ViewModels.Feature;
using NBitcoin;
using ReactiveUI;

namespace AvaloniaApplication13.ViewModels
{
    public class FullPaymentViewModel : ViewModelBase
    {
        private string address;
        private decimal? amount;

        public FullPaymentViewModel()
        {
            Sut = new Sut();
            Sut.Address.Subscribe(address =>
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

            var parser = new AddressParser(Network.TestNet);
            ReactiveCommand.Create(() => Paste());
            var contentChecker = new ContentChecker<NewAddress?>(new ClipboardObserver().Contents.Select(n => parser.GetAddress(n)), Sut.Address);
            HasNewContent = contentChecker.HasNewContent;
        }

        public IObservable<bool> HasNewContent { get; }

        private void Paste()
        {
        }

        public Sut Sut { get; set; }

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
