using System;

namespace AvaloniaApplication13.ViewModels.Feature;

public interface IClipboardObserver
{
    IObservable<string> ContentChanged { get; }
}