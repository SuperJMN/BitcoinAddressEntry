using AvaloniaApplication13.ViewModels.Feature;
using NBitcoin;

namespace AvaloniaApplication13.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        FullEditor = new PaymentViewModel(new FullAddressParser(Network.TestNet));
        BtcOnlyEditor = new PaymentViewModel(new BtcOnlyAddressParser(Network.TestNet));
    }

    public PaymentViewModel BtcOnlyEditor { get; }

    public PaymentViewModel FullEditor { get; }
}