namespace AvaloniaApplication13.ViewModels.Feature;

class PayjoinAddress : Address
{
    public PayjoinRequest Request { get; }

    public PayjoinAddress(PayjoinRequest request)
    {
        Request = request;
    }
}