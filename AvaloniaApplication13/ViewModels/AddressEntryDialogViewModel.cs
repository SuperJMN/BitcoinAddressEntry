using AvaloniaApplication13.ViewModels.Feature;
using NBitcoin;

namespace AvaloniaApplication13.ViewModels;

public class AddressEntryDialogViewModel
{
    public AddressEntryDialogViewModel()
    {
        PaymentViewModel = Factory.Create(new BtcOnlyAddressParser(Network.TestNet));
    }

    public PaymentViewModel PaymentViewModel { get; }
}