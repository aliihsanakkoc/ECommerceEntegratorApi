using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class Category : Entity<int>
{
    public string SingleCategoryName { get; set; } = default!;
    public string? FullCategoryName { get; }
    public bool IsProductCategorization { get; set; }
    public string TopCategoryName { get; set; } = default!;
    public int TopCategoryId { get; set; }
    public virtual Category TopCategory { get; set; } = default!;
    public virtual ICollection<Category> SubCategories { get; set; } = [];
    public virtual ICollection<CategoryProduct> CategoryProducts { get; set; } = [];
    public Category() { }
    public Category(string singleCategoryName, string topCategoryName)
    {
        SingleCategoryName = singleCategoryName;
        TopCategoryName = topCategoryName;

        if (TopCategoryName == "Top Category" || TopCategoryName == "Seed Top Category")
            FullCategoryName = singleCategoryName;
        else FullCategoryName = topCategoryName + " > " + singleCategoryName;
    }
}
