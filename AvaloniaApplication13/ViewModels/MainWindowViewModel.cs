using System;
using System.Reactive;
using System.Reactive.Linq;
using AvaloniaApplication13.ViewModels.Feature;
using ReactiveUI;

namespace AvaloniaApplication13.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private decimal amount;
        private string address = "";
        public string Greeting => "Welcome to Avalonia!";

        public MainWindowViewModel()
        {
            AddressProp = new AddressViewModel(new ClipboardObserver());

            this.WhenAnyValue(model => model.Address).CombineLatest(AddressProp.AddressChanged).Subscribe(tuple =>
            {
                if (tuple.Second is PayjoinAddress pj && pj.Request.Address == tuple.First)
                {
                    return;
                }

                AddressProp.CurrentAddress = tuple.First;
            });
            PasteCommand = ReactiveCommand.CreateFromObservable(() => AddressProp.AddressChanged.Take(1).LastAsync());
            PasteCommand.Subscribe(a =>
            {
                if (a is PayjoinAddress pj)
                {
                    Address = pj.Request.Address;
                    Amount = pj.Request.Amount;
                }

                if (a is BitcoinAddress btcAddr)
                {
                    Address = btcAddr.Address;
                    Amount = 0;
                }
            });
        }

        public ReactiveCommand<Unit, Address> PasteCommand { get; }

        public string Address
        {
            get => address;
            set => this.RaiseAndSetIfChanged(ref address, value);
        }

        public decimal Amount
        {
            get => amount;
            set => this.RaiseAndSetIfChanged(ref amount, value);
        }

        public AddressViewModel AddressProp { get; }
    }
}
