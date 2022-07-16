using System;
using System.Reactive;
using System.Reactive.Subjects;
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

            var contents = new ClipboardObserver().Contents;
            var behavior = new BehaviorSubject<string>("");
            contents.Subscribe(behavior);
            var p = Sut.WhenAnyValue(sut => sut.Text);
            var contentChecker = new ContentChecker<string>(contents, p, s => parser.GetAddress(s) is not null);
            HasNewContent = contentChecker.HasNewContent;
            PasteCommand = ReactiveCommand.Create(() =>
            {
                Sut.Text = behavior.Value;
            });
        }

        public ReactiveCommand<Unit, Unit> PasteCommand { get; }

        public IObservable<bool> HasNewContent { get; }

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
