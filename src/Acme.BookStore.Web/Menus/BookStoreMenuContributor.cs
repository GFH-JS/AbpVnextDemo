using System.Threading.Tasks;
using Acme.BookStore.Localization;
using Acme.BookStore.Permissions;
using Acme.BookStore.MultiTenancy;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;

namespace Acme.BookStore.Web.Menus;

public class BookStoreMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private static Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<BookStoreResource>();

        //Home
        context.Menu.AddItem(
            new ApplicationMenuItem(
                BookStoreMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fa fa-home",
                order: 1
            )
        );
        context.Menu.AddItem(
          new ApplicationMenuItem(
             "ProductManagement",
              l["Menu:ProductManagement"],
              icon: "fa fa-shopping-cart",
              order: 1
          ).AddItem(
              new ApplicationMenuItem(
                  "BookStore.Products",
                  l["Menu:Products"],
                  url: "/Products"))
      );


        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 5;

        //Administration->Identity
        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 1);
    
        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }
        
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        //Administration->Settings
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 7);
        
        return Task.CompletedTask;
    }
}