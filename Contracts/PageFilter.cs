// <copyright file="PageFilter.cs" company="Veel">
// Copyright (c) Veel. All rights reserved.
// </copyright>

namespace TodoAppApi.Contracts;

public class PageFilter
{
    public PageFilter()
    {
        // Default values if no constructor values are provided
        Page = 1;
        Count = 10;
    }

    public PageFilter(int page, int count)
    {
        // Constructor to set custom values
        Page = page == 0 ? 1 : page;
        Count = count == 0 ? 10 : count;
    }

    // [Range(1, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
    public int Page { get; set; }

    // [Range(1, 2500)]
    public int Count { get; set; }
}
