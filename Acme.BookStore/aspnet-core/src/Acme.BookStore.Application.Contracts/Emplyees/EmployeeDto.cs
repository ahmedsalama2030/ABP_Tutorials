using System;
using Acme.BookStore.Domain.Shared.Employees;
using Volo.Abp.Application.Dtos;
namespace Acme.BookStore.Application.Contracts.Emplyees;
public class EmployeeDto : AuditedEntityDto<Guid>
{
   public string FirstName { get; set; }
    public string LastName { get; set; }
    public GenderType Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public float  Salary { get; set; }
    public Guid DepartmentId { get; set; }
public string DepartmentName { get; set; }
}

