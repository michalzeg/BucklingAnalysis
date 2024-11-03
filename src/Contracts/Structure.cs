﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public record Structure
    {
        public required IReadOnlyCollection<Element> Elements { get; init; } = [];
        public required Supports Supports { get; init; }
        public required IReadOnlyCollection<Load> Loads { get; init; } = [];

    }
}