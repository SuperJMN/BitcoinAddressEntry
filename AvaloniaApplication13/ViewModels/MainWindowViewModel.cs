using AvaloniaApplication13.ViewModels.Feature;

namespace AvaloniaApplication13.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Avalonia!";

        public MainWindowViewModel()
        {
            AddressProp = new AddressViewModel();
        }

        public AddressViewModel AddressProp { get; }
    }
}
