using eShopSolution.Shared.Enums;
using System.Collections.Generic;

namespace eShopSolution.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string SeoDescription { get; set; }

        public string SeoTitle { get; set; }

        public int SortOrder { get; set; }

        public bool IsShowOnHome { get; set; }

        public int? ParentId { get; set; }

        public Status Status { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }

    }
}
