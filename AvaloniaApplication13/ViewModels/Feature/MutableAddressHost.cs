using System;
using NBitcoin;
using ReactiveUI;

namespace AvaloniaApplication13.ViewModels.Feature;

public class MutableAddressHost : ViewModelBase, IMutableAddressHost
{
    private string text;

    public MutableAddressHost(Network network, IAddressParser addressParser)
    {
        text = "";
        var parser = addressParser;
        Address = this.WhenAnyValue(s => s.Text, s => parser.GetAddress(s));
        TextChanged = this.WhenAnyValue(x => x.Text);
    }

    public IObservable<string> TextChanged { get; }

    public string Text
    {
        get => text;
        set => this.RaiseAndSetIfChanged(ref text, value);
    }

    public IObservable<NewAddress?> Address { get; }
}