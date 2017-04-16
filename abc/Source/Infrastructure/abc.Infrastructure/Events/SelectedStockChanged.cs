﻿using abc.Domain;
using Prism.Events;

namespace abc.Infrastructure.Events
{
    public class SelectedStockChanged : PubSubEvent<Stock>
    {
    }
}
