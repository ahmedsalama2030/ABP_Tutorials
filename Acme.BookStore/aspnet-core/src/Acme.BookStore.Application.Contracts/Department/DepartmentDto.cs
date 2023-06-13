using System;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Application.Contracts.Department;
public class DepartmentDto: EntityDto<Guid>
{
    public string Name { get; set; }
public string Description { get; set; }
}