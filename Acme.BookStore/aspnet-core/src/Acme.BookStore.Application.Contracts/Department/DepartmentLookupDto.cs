using System;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Application.Contracts.Department;
    public class DepartmentLookupDto: EntityDto<Guid>
    {
        public string Name { get; set; }
    }

