using Acme.BookStore.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Acme.BookStore.Permissions;

public class BookStorePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(BookStorePermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(BookStorePermissions.MyPermission1, L("Permission:MyPermission1"));

          var productp = myGroup.AddPermission(BookStorePermissions.Products.Default, L("…Ã∆∑"));   

        productp.AddChild(BookStorePermissions.Products.create, L("create"));
        productp.AddChild(BookStorePermissions.Products.delete, L("delete"));
        productp.AddChild(BookStorePermissions.Products.query, L("≤È—Ø"));
        productp.AddChild(BookStorePermissions.Products.edit, L("±‡º≠"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BookStoreResource>(name);
    }
}
