using System;
using System.Reactive.Linq;

namespace AvaloniaApplication13.ViewModels.Feature;

public class ContentChecker<T> 
{
    public ContentChecker(IObservable<T> from, IObservable<T> to)
    {
        HasNewContent = from
            .CombineLatest(to, (clipboard, current) =>
                !Equals(clipboard, current));
    }

    public IObservable<bool> HasNewContent { get; }
}