using System;
using System.Reactive;
using System.Reactive.Subjects;
using ReactiveUI;

namespace AvaloniaApplication13.ViewModels.Feature
{
    public class PaymentViewModel : ViewModelBase
    {
        private string address = "";
        private decimal? amount;

        public PaymentViewModel(IObservable<string> clipboardObserver, IMutableAddressHost mutableAddressHost, ContentChecker<string> contentChecker)
        {
            MutableAddressHost = mutableAddressHost;
            MutableAddressHost.Address.Subscribe(a =>
            {
                if (a is not null)
                {
                    Address = a.BtcAddress;

                    if (a.Amount is not null)
                    {
                        Amount = a.Amount.Value;
                    }
                }
            });

            var clipboardContent = new BehaviorSubject<string>("");
            clipboardObserver.Subscribe(clipboardContent);
            HasNewContent = contentChecker.ActivatedWithNewContent;
            PasteCommand = ReactiveCommand.Create(() =>
            {
                MutableAddressHost.Text = clipboardContent.Value;
            });
        }

        public ReactiveCommand<Unit, Unit> PasteCommand { get; }

        public IObservable<bool> HasNewContent { get; }

        public IMutableAddressHost MutableAddressHost { get; }

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
