using System;
using System.Reactive.Linq;
using ReactiveUI;

namespace AvaloniaApplication13.ViewModels.Feature;

public class AddressViewModel : ViewModelBase
{
    private string currentAddress;

    public AddressViewModel()
    {
        var clipboardMonitor = new ClipboardObserver();

        var currentTextChanged = this.WhenAnyValue(model => model.CurrentAddress);


        IsNewContent = clipboardMonitor.Contents.CombineLatest(currentTextChanged, (cpText, curAddr) =>
                !string.Equals(cpText, curAddr));

        IsNewContentOnActivated = ApplicationUtils.IsMainWindowActive
            .CombineLatest(IsNewContent, (isActive, newContent) =>
                isActive && newContent);

        CurrentAddressObj = currentTextChanged.Select(s => address)
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