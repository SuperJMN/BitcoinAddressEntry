using System;
using AvaloniaApplication13.ViewModels.Feature;
using NBitcoin;

namespace AvaloniaApplication13.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        var clipboardObserver = new ClipboardObserver();
        var network = Network.TestNet;
        FullEditor = Create(clipboardObserver.ContentChanged, network, new FullAddressParser(network));
        BtcOnlyEditor = Create(clipboardObserver.ContentChanged, network, new BtcOnlyAddressParser(network));
    }

    private static PaymentViewModel Create(IObservable<string> newContentsChanged, Network network,
        IAddressParser parser)
    {
        IMutableAddressHost mutableAddressHost = new MutableAddressHost(parser);
        return new PaymentViewModel(newContentsChanged, mutableAddressHost,
            new ContentChecker<string>(newContentsChanged, mutableAddressHost.TextChanged,
                s => parser.GetAddress(s) is not null));
    }

    public PaymentViewModel BtcOnlyEditor { get; }

    public PaymentViewModel FullEditor { get; }
}