using System;
using NBitcoin;

namespace AvaloniaApplication13.ViewModels.Feature;

public class BtcAddressValidator
{
    private readonly Network _expectedNetwork;

    public BtcAddressValidator(Network expectedNetwork)
    {
        _expectedNetwork = expectedNetwork ?? throw new ArgumentNullException(nameof(expectedNetwork));
    }

    public bool IsValid(string text)
    {
        text = text.Trim();

        if (text.Length is > 100 or < 20)
        {
            return false;
        }

        try
        {
            NBitcoin.BitcoinAddress.Create(text, _expectedNetwork);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}