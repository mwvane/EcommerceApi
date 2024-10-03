using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection;

namespace EcommerceApp
{
    public static class Helpers
    {
        public static object ConcatObj(params dynamic[] objects)
        {
            return null;
        }
        public const string hostName = "https://localhost:7287/";

        public static bool NameExists<T>(this DbContext context, string name) where T : class
        {
            var dbSet = context.Set<T>();
            var propertyInfo = typeof(T).GetProperty("Name", BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo == null)
            {
                throw new InvalidOperationException($"{typeof(T).Name} does not have a Name property.");
            }

            return dbSet.Any(e => propertyInfo.GetValue(e).ToString() == name);
        }

        public static string GenerateUniqueId(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
