using AvaloniaApplication13.ViewModels.Feature;
using NBitcoin;

namespace AvaloniaApplication13.ViewModels;

public class SendViewModel
{
    public SendViewModel()
    {
        PaymentViewModel = Factory.Create(new FullAddressParser(Network.TestNet));
    }

    public PaymentViewModel PaymentViewModel { get; }
}