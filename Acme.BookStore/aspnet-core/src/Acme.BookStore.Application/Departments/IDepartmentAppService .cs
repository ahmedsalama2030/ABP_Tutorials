using System;
using System.Threading.Tasks;
using Acme.BookStore.Application.Contracts.Department;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Application.Departments;
public interface IDepartmentAppService: IApplicationService
{
    Task<DepartmentDto> GetAsync(Guid id);

    Task<PagedResultDto<DepartmentDto>> GetListAsync(GetDepartmentListDto input);

    Task<DepartmentDto> CreateAsync(CreateDepartmentDto input);

    Task UpdateAsync(Guid id, UpdateDepartmentDto input);

    Task DeleteAsync(Guid id);
}