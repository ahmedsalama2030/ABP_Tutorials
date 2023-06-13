using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Acme.BookStore.Application.Contracts.Department;
using Acme.BookStore.Application.Contracts.Emplyees;
using Acme.BookStore.Domain.Departments;
using Acme.BookStore.Domain.Employees;
using Acme.BookStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
namespace Acme.BookStore.Application.Employees;
public class EmployeesService : CrudAppService< //Defines CRUD methods
Employee,
       EmployeeDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateEmployeeDto>, IEmployeeAppService
{
    private readonly IDepartmentRepository _departmentRepository;

    public EmployeesService(IRepository<Employee, Guid> repository, IDepartmentRepository departmentRepository)
       : base(repository)
    {
        GetPolicyName = BookStorePermissions.Employees.Default;
        GetListPolicyName = BookStorePermissions.Employees.Default;
        CreatePolicyName = BookStorePermissions.Employees.Create;
        UpdatePolicyName = BookStorePermissions.Employees.Edit;
        DeletePolicyName = BookStorePermissions.Employees.Delete;
        _departmentRepository = departmentRepository;
    }
    public override async Task<EmployeeDto> GetAsync(Guid id)
    {
         var queryable = await Repository.GetQueryableAsync();

         var query = from employee in queryable
                    join department in await _departmentRepository.GetQueryableAsync() on employee.DepartmentId equals department.Id
                    where employee.Id == id
                    select new { employee, department };

        //Execute the query and get the book with author
        var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
        if (queryResult == null)
        {
            throw new EntityNotFoundException(typeof(Employee), id);
        }

        var bookDto = ObjectMapper.Map<Employee, EmployeeDto>(queryResult.employee);
        bookDto.DepartmentName = queryResult.department.Name;
        return bookDto;
    }

    public override async Task<PagedResultDto<EmployeeDto>>
        GetListAsync(PagedAndSortedResultRequestDto input)
    {
        //Get the IQueryable<Employee> from the repository
        var queryable = await Repository.GetQueryableAsync();

        //Prepare a query to join books and authors
        var query = from employee in queryable
                    join department in await _departmentRepository.GetQueryableAsync() on employee.DepartmentId equals department.Id
                    select new { employee, department };

        query = query
            .OrderBy(NormalizeSorting(input.Sorting))
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        //Execute the query and get a list
        var queryResult = await AsyncExecuter.ToListAsync(query);

        //Convert the query result to a list of BookDto objects
        var employeeDtos = queryResult.Select(x =>
        {
            var employeeDto = ObjectMapper.Map<Employee, EmployeeDto>(x.employee);
            employeeDto.DepartmentName = x.department.Name;
            return employeeDto;
        }).ToList();

        //Get the total count with Employee query
        var totalCount = await Repository.GetCountAsync();

        return new PagedResultDto<EmployeeDto>(
            totalCount,
            employeeDtos
        );
    }

    public async Task<ListResultDto<DepartmentLookupDto>> GetDepartmentLookupAsync()
    {
        var departments = await _departmentRepository.GetListAsync();

        return new ListResultDto<DepartmentLookupDto>(
            ObjectMapper.Map<List<Department>, List<DepartmentLookupDto>>(departments)
        );
    }

    private static string NormalizeSorting(string sorting)
    {
        if (sorting.IsNullOrEmpty())
        {
            return $"employee.{nameof(Employee.FirstName)}";
        }

        if (sorting.Contains("departmentName", StringComparison.OrdinalIgnoreCase))
        {
            return sorting.Replace(
                "departmentName",
                "department.Name",
                StringComparison.OrdinalIgnoreCase
            );
        }

        return $"employee.{sorting}";
    }

    
}

