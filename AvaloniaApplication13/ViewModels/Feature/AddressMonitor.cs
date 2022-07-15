using System;
using System.Reactive.Linq;
using NBitcoin;

namespace AvaloniaApplication13.ViewModels.Feature;

public class AddressMonitor
{
    private readonly Network _network;
    private readonly BtcAddressValidator _btcValidator;
    private readonly PayjoinAddressParser _payjoinValidator;

    public AddressMonitor(Network network)
    {
        _network = network;
        _btcValidator = new BtcAddressValidator(_network);
        _payjoinValidator = new PayjoinAddressParser(_network);
    }

    public AddressMonitor(IObservable<string> source)
    {
        Address = source.Select(GetAddress);
    }

    private Address GetAddress(string str)
    {
        if (_btcValidator.IsValid(str))
        {
            return new BitcoinAddress(str);
        }

        if (_payjoinValidator.TryParse(str, out var payjoinRequest))
        {
            return new PayjoinAddress(payjoinRequest);
        }

        return new InvalidAddress();
    }

    public IObservable<Address> Address { get; }
}