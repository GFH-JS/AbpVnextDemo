namespace Acme.BookStore.Permissions;

public static class BookStorePermissions
{
    public const string GroupName = "BookStore";


    //Add your own permission names. Example:

    public static class Products  //权限名会存到数据库中
    {
        public const string Default = GroupName + ".Products";
        public const string query = Default + ".query";
        public const string edit = Default + ".编辑";
        public const string create = Default + ".creat";
        public const string delete = Default + ".删除";
    }
}
