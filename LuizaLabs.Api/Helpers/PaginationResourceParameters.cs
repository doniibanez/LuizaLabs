using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuizaLabs.Api.Helpers
{
    public abstract class PaginationResourceParameters
    {
        private const int MaxPageSize = 20;
        public int PageNumber { get; set; } = 1;
        private int _PageSize = 10; // Default 10 per page
        public int PageSize
        {
            get => _PageSize;
            set => _PageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}