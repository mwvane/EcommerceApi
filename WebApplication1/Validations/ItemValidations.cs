using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EcommerceApp.Validations
{
    public static class ItemValidations
    {
        public static async Task<bool> IsItemNameExist<T>(Context context, string name) where T : class
        {
            var dbSet = context.Set<T>();
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, "Name");
            var lambda = Expression.Lambda<Func<T, bool>>(Expression.Equal(property, Expression.Constant(name)), parameter);

            return await dbSet.AnyAsync(lambda);
        }
    }
}
