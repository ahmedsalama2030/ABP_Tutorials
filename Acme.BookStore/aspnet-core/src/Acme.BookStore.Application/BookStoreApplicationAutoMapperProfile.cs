using Acme.BookStore.Application.Contracts.Department;
using Acme.BookStore.Application.Contracts.Emplyees;
using Acme.BookStore.Domain.Departments;
using Acme.BookStore.Domain.Employees;
using AutoMapper;

namespace Acme.BookStore;

public class BookStoreApplicationAutoMapperProfile : Profile
{
    public BookStoreApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
         CreateMap<Employee, EmployeeDto>();
         CreateMap<CreateUpdateEmployeeDto, Employee>();
         CreateMap<Department, DepartmentDto>();
         CreateMap<Department, DepartmentLookupDto>();


    }
}
