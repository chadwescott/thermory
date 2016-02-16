﻿using System;
using System.Collections.Generic;

namespace Thermory.Domain
{
    public interface ILumberType
    {
        Guid Id { get; }

        ILumberSubCategory LumberSubCategory { get; }

        string Name { get; }

        int SortOrder { get; }

        IList<ILumberProduct> LumberProducts { get; }
    }
}
