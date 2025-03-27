using Application.Features.Auth.Constants;
using Application.Features.OperationClaims.Constants;
using Application.Features.UserOperationClaims.Constants;
using Application.Features.Users.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NArchitecture.Core.Security.Constants;
using Application.Features.Categories.Constants;
using Application.Features.Products.Constants;
using Application.Features.CategoryProducts.Constants;
using Application.Features.Products.Constants;
using Application.Features.Products.Constants;
using Application.Features.Variants.Constants;
using Application.Features.VariantProducts.Constants;
using Application.Features.Books.Constants;
using Application.Features.Clothings.Constants;
using Application.Features.Foods.Constants;











namespace Persistence.EntityConfigurations;

public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
{
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.ToTable("OperationClaims").HasKey(oc => oc.Id);

        builder.Property(oc => oc.Id).HasColumnName("Id").IsRequired();
        builder.Property(oc => oc.Name).HasColumnName("Name").IsRequired();
        builder.Property(oc => oc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(oc => oc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(oc => oc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(oc => !oc.DeletedDate.HasValue);

        builder.HasData(_seeds);

        builder.HasBaseType((string)null!);
    }

    public static int AdminId => 1;
    private IEnumerable<OperationClaim> _seeds
    {
        get
        {
            yield return new() { Id = AdminId, Name = GeneralOperationClaims.Admin };

            IEnumerable<OperationClaim> featureOperationClaims = getFeatureOperationClaims(AdminId);
            foreach (OperationClaim claim in featureOperationClaims)
                yield return claim;
        }
    }

#pragma warning disable S1854 // Unused assignments should be removed
    private IEnumerable<OperationClaim> getFeatureOperationClaims(int initialId)
    {
        int lastId = initialId;
        List<OperationClaim> featureOperationClaims = new();

        #region Auth
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = AuthOperationClaims.Admin },
                new() { Id = ++lastId, Name = AuthOperationClaims.Read },
                new() { Id = ++lastId, Name = AuthOperationClaims.Write },
                new() { Id = ++lastId, Name = AuthOperationClaims.RevokeToken },
            ]
        );
        #endregion

        #region OperationClaims
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Admin },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Read },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Write },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Create },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Update },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Delete },
            ]
        );
        #endregion

        #region UserOperationClaims
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Admin },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Read },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Write },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Create },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Update },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Delete },
            ]
        );
        #endregion

        #region Users
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = UsersOperationClaims.Admin },
                new() { Id = ++lastId, Name = UsersOperationClaims.Read },
                new() { Id = ++lastId, Name = UsersOperationClaims.Write },
                new() { Id = ++lastId, Name = UsersOperationClaims.Create },
                new() { Id = ++lastId, Name = UsersOperationClaims.Update },
                new() { Id = ++lastId, Name = UsersOperationClaims.Delete },
            ]
        );
        #endregion

        
        #region Categories CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Admin },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Read },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Write },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Create },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Update },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Products CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = ProductsOperationClaims.Admin },
                new() { Id = ++lastId, Name = ProductsOperationClaims.Read },
                new() { Id = ++lastId, Name = ProductsOperationClaims.Write },
                new() { Id = ++lastId, Name = ProductsOperationClaims.Create },
                new() { Id = ++lastId, Name = ProductsOperationClaims.Update },
                new() { Id = ++lastId, Name = ProductsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region CategoryProducts CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = CategoryProductsOperationClaims.Admin },
                new() { Id = ++lastId, Name = CategoryProductsOperationClaims.Read },
                new() { Id = ++lastId, Name = CategoryProductsOperationClaims.Write },
                new() { Id = ++lastId, Name = CategoryProductsOperationClaims.Create },
                new() { Id = ++lastId, Name = CategoryProductsOperationClaims.Update },
                new() { Id = ++lastId, Name = CategoryProductsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Products CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = ProductsOperationClaims.Admin },
                new() { Id = ++lastId, Name = ProductsOperationClaims.Read },
                new() { Id = ++lastId, Name = ProductsOperationClaims.Write },
                new() { Id = ++lastId, Name = ProductsOperationClaims.Create },
                new() { Id = ++lastId, Name = ProductsOperationClaims.Update },
                new() { Id = ++lastId, Name = ProductsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Products CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = ProductsOperationClaims.Admin },
                new() { Id = ++lastId, Name = ProductsOperationClaims.Read },
                new() { Id = ++lastId, Name = ProductsOperationClaims.Write },
                new() { Id = ++lastId, Name = ProductsOperationClaims.Create },
                new() { Id = ++lastId, Name = ProductsOperationClaims.Update },
                new() { Id = ++lastId, Name = ProductsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Variants CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = VariantsOperationClaims.Admin },
                new() { Id = ++lastId, Name = VariantsOperationClaims.Read },
                new() { Id = ++lastId, Name = VariantsOperationClaims.Write },
                new() { Id = ++lastId, Name = VariantsOperationClaims.Create },
                new() { Id = ++lastId, Name = VariantsOperationClaims.Update },
                new() { Id = ++lastId, Name = VariantsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region VariantProducts CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = VariantProductsOperationClaims.Admin },
                new() { Id = ++lastId, Name = VariantProductsOperationClaims.Read },
                new() { Id = ++lastId, Name = VariantProductsOperationClaims.Write },
                new() { Id = ++lastId, Name = VariantProductsOperationClaims.Create },
                new() { Id = ++lastId, Name = VariantProductsOperationClaims.Update },
                new() { Id = ++lastId, Name = VariantProductsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Books CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = BooksOperationClaims.Admin },
                new() { Id = ++lastId, Name = BooksOperationClaims.Read },
                new() { Id = ++lastId, Name = BooksOperationClaims.Write },
                new() { Id = ++lastId, Name = BooksOperationClaims.Create },
                new() { Id = ++lastId, Name = BooksOperationClaims.Update },
                new() { Id = ++lastId, Name = BooksOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Clothings CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = ClothingsOperationClaims.Admin },
                new() { Id = ++lastId, Name = ClothingsOperationClaims.Read },
                new() { Id = ++lastId, Name = ClothingsOperationClaims.Write },
                new() { Id = ++lastId, Name = ClothingsOperationClaims.Create },
                new() { Id = ++lastId, Name = ClothingsOperationClaims.Update },
                new() { Id = ++lastId, Name = ClothingsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Foods CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = FoodsOperationClaims.Admin },
                new() { Id = ++lastId, Name = FoodsOperationClaims.Read },
                new() { Id = ++lastId, Name = FoodsOperationClaims.Write },
                new() { Id = ++lastId, Name = FoodsOperationClaims.Create },
                new() { Id = ++lastId, Name = FoodsOperationClaims.Update },
                new() { Id = ++lastId, Name = FoodsOperationClaims.Delete },
            ]
        );
        #endregion
        
        return featureOperationClaims;
    }
#pragma warning restore S1854 // Unused assignments should be removed
}
