using System;

namespace AvaloniaApplication13.ViewModels.Feature;

public record PayjoinRequest(Uri Endpoint, string Address, decimal Amount);