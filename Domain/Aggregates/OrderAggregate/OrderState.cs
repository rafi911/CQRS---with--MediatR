﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.OrderAggregate
{
    public enum OrderState
    {
        DRAFT = 0,
        CONFIRMED = 1
    }
}
