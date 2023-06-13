using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acme.BookStore.Application.Contracts.Department;
using Acme.BookStore.Domain.Departments;
using Acme.BookStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Application.Departments;
[Authorize(BookStorePermissions.Department.Default)]
public class DepartmentsService: BookStoreAppService, IDepartmentAppService
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly DepartmentManager _DepartmentManager;

    public DepartmentsService(
        IDepartmentRepository DepartmentRepository,
        DepartmentManager DepartmentManager)
    {
        _departmentRepository = DepartmentRepository;
        _DepartmentManager = DepartmentManager;
    }

    public async Task<DepartmentDto> GetAsync(Guid id)
    {
        var department = await _departmentRepository.GetAsync(id);
    return ObjectMapper.Map<Department, DepartmentDto>(department);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _departmentRepository.DeleteAsync(id);
    }

 

    public async Task<PagedResultDto<DepartmentDto>> GetListAsync(GetDepartmentListDto input)
    {
         if (input.Sorting.IsNullOrWhiteSpace())
    {
        input.Sorting = nameof(Department.Name);
    }

    var departments = await _departmentRepository.GetListAsync(
        input.SkipCount,
        input.MaxResultCount,
        input.Sorting,
        input.Filter
    );

    var totalCount = input.Filter == null
        ? await _departmentRepository.CountAsync()
        : await _departmentRepository.CountAsync(
            author => author.Name.Contains(input.Filter));

    return new PagedResultDto<DepartmentDto>(
        totalCount,
        ObjectMapper.Map<List<Department>, List<DepartmentDto>>(departments)
    );
    }

    public async Task UpdateAsync(Guid id, UpdateDepartmentDto input)
    {
         var author = await _departmentRepository.GetAsync(id);

    if (author.Name != input.Name)
    {
        await _DepartmentManager.ChangeNameAsync(author, input.Name);
    }

     author.Description = input.Description;

    await _departmentRepository.UpdateAsync(author);
    }

    public async Task<DepartmentDto> CreateAsync(CreateDepartmentDto input)
    {
       var department = await _DepartmentManager.CreateAsync(
        input.Name,
       input.Description
    );

    await _departmentRepository.InsertAsync(department);

    return ObjectMapper.Map<Department, DepartmentDto>(department);
    }

    //...SERVICE METHODS WILL COME HERE...
}