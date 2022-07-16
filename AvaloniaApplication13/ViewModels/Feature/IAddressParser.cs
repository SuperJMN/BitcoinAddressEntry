namespace AvaloniaApplication13.ViewModels.Feature;

public interface IAddressParser
{
    NewAddress? GetAddress(string str);
}