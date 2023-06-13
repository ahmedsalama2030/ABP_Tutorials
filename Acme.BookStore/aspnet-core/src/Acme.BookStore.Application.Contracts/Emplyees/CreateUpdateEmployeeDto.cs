using System;
using System.ComponentModel.DataAnnotations;
using Acme.BookStore.Domain.Shared.Employees;

namespace Acme.BookStore.Application.Contracts.Emplyees;
public class CreateUpdateEmployeeDto
{
    [Required]
    [StringLength(128)]
    public string FirstName { get; set; }
    [Required]
    [StringLength(128)]
    public string LastName { get; set; }
    [Required]
    public GenderType Gender { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    [Required]
    public float Salary { get; set; }
    public Guid DepartmentId { get; set; }
}

