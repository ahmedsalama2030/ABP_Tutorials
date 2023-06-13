using Acme.BookStore.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Acme.BookStore.Permissions;

public class BookStorePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var bookStoreGroup = context.AddGroup(BookStorePermissions.GroupName, L("Permission:Employees"));

        var employeesPermission = bookStoreGroup.AddPermission(BookStorePermissions.Employees.Default, L("Permission:Employees"));
        employeesPermission.AddChild(BookStorePermissions.Employees.Create, L("Permission:Employees.Create"));
        employeesPermission.AddChild(BookStorePermissions.Employees.Edit, L("Permission:Employees.Edit"));
        employeesPermission.AddChild(BookStorePermissions.Employees.Delete, L("Permission:Employees.Delete"));

        var departmentsPermission = bookStoreGroup.AddPermission(BookStorePermissions.Department.Default, L("Permission:Departments"));
        departmentsPermission.AddChild(BookStorePermissions.Department.Create, L("Permission:Departments.Create"));
        departmentsPermission.AddChild(BookStorePermissions.Department.Edit, L("Permission:Departments.Edit"));
        departmentsPermission.AddChild(BookStorePermissions.Department.Delete, L("Permission:Departments.Delete"));

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BookStoreResource>(name);
    }
}
