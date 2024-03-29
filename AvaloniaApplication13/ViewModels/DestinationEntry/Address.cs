﻿using System;

namespace AvaloniaApplication13.ViewModels.DestinationEntry;

public record Address
{
    public Address(string btcAddress)
    {
        BtcAddress = btcAddress;
    }

    public Address(string btcAddress, Uri endPoint, decimal amount) : this(btcAddress)
    {
        EndPoint = endPoint;
        Amount = amount;
    }

    public Uri? EndPoint { get; }
    public decimal? Amount { get; }
    public string BtcAddress { get; }
}