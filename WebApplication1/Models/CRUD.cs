namespace EcommerceApp.Models
{
    public interface CRUD
    {
        public Result Get();
        public Result GetById(string id);
        public Result Delete(List<int>[] ids);
        public Result Add<T>(T item) where T : class;
        public Result Update<T>(T item)where T : class;

    }
}
