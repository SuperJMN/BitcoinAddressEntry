using System;
using NBitcoin;

namespace AvaloniaApplication13.ViewModels.Feature;

public class AddressParser
{
    private readonly BtcAddressValidator btcValidator;
    private readonly PayjoinAddressParser payjoinValidator;

    public AddressParser(Network network)
    {
        var network1 = network;
        btcValidator = new BtcAddressValidator(network1);
        payjoinValidator = new PayjoinAddressParser(network1);
    }

    public NewAddress? GetAddress(string str)
    {
        str = str.Trim();

        if (btcValidator.IsValid(str))
        {
            return new NewAddress(str);
        }

        if (payjoinValidator.TryParse(str, out var payjoinRequest))
        {
            return new NewAddress(payjoinRequest.Address, payjoinRequest.Endpoint, payjoinRequest.Amount);
        }

        return null;
    }
}