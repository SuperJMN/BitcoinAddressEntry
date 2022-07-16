using System;

namespace AvaloniaApplication13.ViewModels.Feature;

public record NewAddress
{
    public NewAddress(string btcAddress)
    {
        BtcAddress = btcAddress;
    }

    public NewAddress(string btcAddress, Uri endPoint, decimal amount) : this(btcAddress)
    {
        EndPoint = endPoint;
        Amount = amount;
    }

    public Uri? EndPoint { get; }
    public decimal? Amount { get; }
    public string BtcAddress { get; }
}