namespace AvaloniaApplication13.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        FullPayment = new FullViewModel();
        BtcOnly = new BtcOnlyViewModel();
    }

    public BtcOnlyViewModel BtcOnly { get; }

    public FullViewModel FullPayment { get; }
}