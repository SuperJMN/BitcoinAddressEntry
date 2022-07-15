using System;
using System.Reactive.Linq;
using NBitcoin;
using ReactiveUI;

namespace AvaloniaApplication13.ViewModels.Feature;

public class AddressViewModel : ViewModelBase
{
    private string currentAddress = "";

    public AddressViewModel(IClipboardObserver clipboardObserver)
    {
        var clipboardMonitor = clipboardObserver;

        var monitor = new AddressParser(Network.TestNet);

        AddressChanged = this.WhenAnyValue(model => model.CurrentAddress)
            .Select(s => monitor.GetAddress(s));

        IsNewContent = clipboardMonitor.Contents.Select(s => monitor.GetAddress(s))
            .CombineLatest(AddressChanged, (cpText, curAddr) =>
                !Equals(cpText, curAddr) && cpText is not InvalidAddress);

        IsNewContentOnActivated = ApplicationUtils.IsMainWindowActive
            .CombineLatest(IsNewContent, (isActive, newContent) =>
                isActive && newContent);
    }

    public IObservable<Address> AddressChanged { get; }

    public IObservable<bool> IsNewContentOnActivated { get; }

    public IObservable<bool> IsNewContent { get; }

    public string CurrentAddress
    {
        get => currentAddress;
        set => this.RaiseAndSetIfChanged(ref currentAddress, value);
    }
}