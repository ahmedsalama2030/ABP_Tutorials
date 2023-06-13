using System;
using Acme.BookStore.Domain.Shared.Employees;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Domain.Employees;
public class Employee : AuditedAggregateRoot<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public GenderType Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public float  Salary { get; set; }
    public Guid DepartmentId { get; set; }

}

