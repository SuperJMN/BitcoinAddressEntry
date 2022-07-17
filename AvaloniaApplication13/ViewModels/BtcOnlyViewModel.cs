using AvaloniaApplication13.ViewModels.Feature;
using NBitcoin;

namespace AvaloniaApplication13.ViewModels;

public class BtcOnlyViewModel
{
    public BtcOnlyViewModel()
    {
        PaymentViewModel = Factory.Create(new BtcOnlyAddressParser(Network.TestNet));
    }

    public PaymentViewModel PaymentViewModel { get; }
}