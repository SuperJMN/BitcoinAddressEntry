using System;

namespace AvaloniaApplication13.ViewModels.Feature;

public interface IMutableAddressHost
{
    string Text { get; set; }
    IObservable<string> TextChanged { get; }
    IObservable<NewAddress?> Address { get; }
}