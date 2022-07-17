using System;

namespace AvaloniaApplication13.ViewModels.DestinationEntry;

public record PayjoinRequest(Uri Endpoint, string Address, decimal Amount);