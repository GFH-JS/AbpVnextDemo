namespace Acme.BookStore.Permissions;

public static class BookStorePermissions
{
    public const string GroupName = "BookStore";


    //Add your own permission names. Example:

    public static class Products  //Ȩ������浽���ݿ���
    {
        public const string Default = GroupName + ".Products";
        public const string query = Default + ".query";
        public const string edit = Default + ".�༭";
        public const string create = Default + ".creat";
        public const string delete = Default + ".ɾ��";
    }
}
