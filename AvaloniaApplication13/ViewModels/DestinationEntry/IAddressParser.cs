namespace AvaloniaApplication13.ViewModels.DestinationEntry;

public interface IAddressParser
{
    Address? GetAddress(string str);
}