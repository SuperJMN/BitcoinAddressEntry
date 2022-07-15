using System;
using System.Reactive.Linq;
using NBitcoin;
using ReactiveUI;

namespace AvaloniaApplication13.ViewModels.Feature;

public class AddressViewModel : ViewModelBase
{
    private string currentAddress = "";

    public AddressViewModel()
    {
        var clipboardMonitor = new ClipboardObserver();

        var monitor = new AddressParser(Network.TestNet);

        var currentTextChanged = this.WhenAnyValue(model => model.CurrentAddress)
            .Select(s => monitor.GetAddress(s));

        IsNewContent = clipboardMonitor.Contents.Select(s => monitor.GetAddress(s))
            .CombineLatest(currentTextChanged, (cpText, curAddr) =>
                !Equals(cpText, curAddr) && cpText is not InvalidAddress);

        IsNewContentOnActivated = ApplicationUtils.IsMainWindowActive
            .CombineLatest(IsNewContent, (isActive, newContent) =>
                isActive && newContent);
    }
    
    public IObservable<bool> IsNewContentOnActivated { get; }

    public IObservable<bool> IsNewContent { get; }

    public string CurrentAddress
    {
        get => currentAddress;
        set => this.RaiseAndSetIfChanged(ref currentAddress, value);
    }
}

public class ClipboardObserver : ViewModelBase
{
    public ClipboardObserver()
    {
        Contents = Observable
            .Timer(TimeSpan.FromMilliseconds(200), RxApp.MainThreadScheduler)
            .Repeat()
            .SelectMany(_ => ApplicationUtils.GetClipboardTextAsync())
            .Select(x => x.Trim())
            .DistinctUntilChanged();
    }

    public IObservable<string> Contents { get; }
}