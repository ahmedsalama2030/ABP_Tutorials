using System;
using System.Threading.Tasks;
using Acme.BookStore.Application.Contracts.Department;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Application.Contracts.Emplyees;
public interface IEmployeeAppService:
    ICrudAppService< //Defines CRUD methods
       EmployeeDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateEmployeeDto> //Used to create/update a book
{
 Task<ListResultDto<DepartmentLookupDto>> GetDepartmentLookupAsync();
}