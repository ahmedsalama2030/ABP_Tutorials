using System.ComponentModel.DataAnnotations;
using Acme.BookStore.Domain.Shared.Departments;

namespace Acme.BookStore.Application.Contracts.Department;
public class UpdateDepartmentDto
{
    [Required]
    [StringLength(DepartmentConsts.MaxNameLength)]
    public string Name { get; set; }
    public string Description { get; set; }
}
