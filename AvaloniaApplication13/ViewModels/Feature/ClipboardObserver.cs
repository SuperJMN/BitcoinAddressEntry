using System;
using System.Reactive.Linq;
using ReactiveUI;

namespace AvaloniaApplication13.ViewModels.Feature;

public class ClipboardObserver
{
    public ClipboardObserver()
    {
        ContentChanged = Observable
            .Timer(TimeSpan.FromMilliseconds(200), RxApp.MainThreadScheduler)
            .Repeat()
            .SelectMany(_ => ApplicationUtils.GetClipboardTextAsync())
            .Select(x => x.Trim())
            .DistinctUntilChanged();
    }

    public IObservable<string> ContentChanged { get; }
}