using Volo.Abp;

namespace Acme.BookStore.Domain.Departments;
public class DepartmentAlreadyExistsException: BusinessException
{
    public DepartmentAlreadyExistsException(string name)
        : base(BookStoreDomainErrorCodes.DepartmentAlreadyExists)
    {
        WithData("name", name);
    }
}