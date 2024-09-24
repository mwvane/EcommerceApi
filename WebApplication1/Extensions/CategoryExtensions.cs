using Ecommerce.Core.Entities;
using EcommerceApp.Models.DTO;
using System.Runtime.CompilerServices;

namespace Ecommerce.Api.Extensions
{
    public static class CategoryExtensions
    {
        public static  CategoryDto? ToCategoryDto(this Category category)
        {
            if(category == null)
            {
                return null;
            }
            return new CategoryDto()
            {
                Id = category.Id,
                Name = category.Name,
                Image = category.Image,
                ParentCategoryId = category.ParentCategoryId,
            };

        }
        public static Category? ToCategory(this CategoryDto category)
        {
            if (category == null)
            {
                return null;
            }
            return new Category()
            {
                Id = category.Id,
                Name = category.Name,
                Image = category.Image,
                ParentCategoryId = category.ParentCategoryId,
            };

        }
        public static ICollection<CategoryDto>? ToCategoryDtoList(this ICollection<Category> categories)
        {
            if (categories == null)
            {
                return null;
            }
            var convertedCategories = new List<CategoryDto>();
            foreach (var category in categories)
            {
                var newCategory = category.ToCategoryDto();
                if (newCategory != null)
                {
                    convertedCategories.Add(newCategory);
                }
            }
            return convertedCategories;
        }
    }
}
