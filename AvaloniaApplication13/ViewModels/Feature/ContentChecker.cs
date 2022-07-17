using System;
using System.Reactive.Linq;

namespace AvaloniaApplication13.ViewModels.Feature;

public class ContentChecker<T>
{
    public ContentChecker(IObservable<T> from, IObservable<T> to, Func<T, bool> isValid)
    {
        HasNewContent = from
            .CombineLatest(to, (clipboard, current) => isValid(clipboard) &&
                                                       !Equals(clipboard, current));
        ActivatedWithNewContent = ApplicationUtils.IsMainWindowActive
            .CombineLatest(HasNewContent, (isActive, newContent) =>
                isActive && newContent);
    }

    public IObservable<bool> ActivatedWithNewContent { get; }

    public IObservable<bool> HasNewContent { get; }
}