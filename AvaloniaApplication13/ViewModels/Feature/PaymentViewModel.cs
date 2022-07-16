using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using ReactiveUI;

namespace AvaloniaApplication13.ViewModels.Feature
{
    public class PaymentViewModel : ViewModelBase, IDisposable
    {
        private string address = "";
        private decimal? amount;
        private readonly CompositeDisposable disposables = new();

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
            }).DisposeWith(disposables);

            var clipboardContent = new BehaviorSubject<string>("");
            clipboardObserver.Subscribe(clipboardContent).DisposeWith(disposables);
            HasNewContent = contentChecker.ActivatedWithNewContent;
            PasteCommand = ReactiveCommand.Create(() =>
            {
                MutableAddressHost.Text = clipboardContent.Value;
            }).DisposeWith(disposables);
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

        public void Dispose()
        {
            disposables.Dispose();
        }
    }
}
