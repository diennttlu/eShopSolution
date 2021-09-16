﻿using eShopSolution.ViewModels.Common;

namespace eShopSolution.ViewModels.Catelog.Products
{
    public class GetPublicProductPagingRequest : PagingRequestBase
    {
        public int? CategoryId { get; set; }

        public string LanguageId { get; set; }
    }
}
