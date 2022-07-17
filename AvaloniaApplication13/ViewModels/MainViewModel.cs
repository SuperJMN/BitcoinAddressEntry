namespace AvaloniaApplication13.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        SendViewModel = new SendViewModel();
        AddressEntryDialog = new AddressEntryDialogViewModel();
    }

    public AddressEntryDialogViewModel AddressEntryDialog { get; }

    public SendViewModel SendViewModel { get; }
}