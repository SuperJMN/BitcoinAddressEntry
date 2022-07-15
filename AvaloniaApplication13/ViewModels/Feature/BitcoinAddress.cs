namespace AvaloniaApplication13.ViewModels.Feature;

class BitcoinAddress : Address
{
    public string Address { get; }

    public BitcoinAddress(string address)
    {
        Address = address;
    }
}