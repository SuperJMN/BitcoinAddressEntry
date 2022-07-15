using System;
using System.Reactive.Linq;
using ReactiveUI;

namespace AvaloniaApplication13.ViewModels.Feature;

public interface IClipboardObserver
{
    IObservable<string> Contents { get; }
}

public class ClipboardObserver : IClipboardObserver
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