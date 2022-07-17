using System;

namespace AvaloniaApplication13.ViewModels.DestinationEntry;

public interface IMutableAddressHost
{
    string Text { get; set; }
    IObservable<string> TextChanged { get; }
    IObservable<Address?> ParsedAddress { get; }
}