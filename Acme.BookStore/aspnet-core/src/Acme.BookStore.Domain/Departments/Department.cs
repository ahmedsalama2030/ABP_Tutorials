using System;
using Acme.BookStore.Domain.Shared.Departments;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Domain.Departments;
public class Department : FullAuditedAggregateRoot<Guid>
{
public string Name { get;private set; }
public string Description { get; set; }
public Department()
{}

internal Department(
        Guid id,
        [NotNull] string name,
       [CanBeNull] string description = null)
        : base(id)
    {
        SetName(name);
         Description = description;
    }
     internal Department ChangeName([NotNull] string name)
    {
        SetName(name);
        return this;
    }
     private void SetName([NotNull] string name)
    {
        Name = Check.NotNullOrWhiteSpace(
            name,
            nameof(name),
            maxLength: DepartmentConsts.MaxNameLength
        );
    }

}
