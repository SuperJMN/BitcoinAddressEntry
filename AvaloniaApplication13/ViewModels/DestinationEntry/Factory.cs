﻿namespace AvaloniaApplication13.ViewModels.DestinationEntry;

public static class Factory
{
    private static readonly ClipboardObserver ClipboardObserver;

    static Factory()
    {
        ClipboardObserver = new ClipboardObserver();
    }

    public static PaymentViewModel Create(IAddressParser parser)
    {
        var newContentsChanged = ClipboardObserver.ContentChanged;
        IMutableAddressHost mutableAddressHost = new MutableAddressHost(parser);
        return new PaymentViewModel(newContentsChanged, mutableAddressHost,
            new ContentChecker<string>(newContentsChanged, mutableAddressHost.TextChanged,
                s => parser.GetAddress(s) is not null));
    }
}