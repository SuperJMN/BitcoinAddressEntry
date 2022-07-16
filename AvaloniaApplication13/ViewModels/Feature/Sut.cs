using System;
using NBitcoin;
using ReactiveUI;

namespace AvaloniaApplication13.ViewModels.Feature;

public class Sut : ViewModelBase
{
    private string text;

    public Sut()
    {
        Text = "";
        var parser = new AddressParser(Network.TestNet);
        Address = this.WhenAnyValue(s => s.Text, s => parser.GetAddress(s));
    }

    public string Text
    {
        get => text;
        set => this.RaiseAndSetIfChanged(ref text, value);
    }

    public IObservable<NewAddress?> Address { get; }
}