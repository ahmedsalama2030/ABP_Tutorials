using System;
 using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Acme.BookStore.Domain.Departments;
public class DepartmentManager : DomainService
{
    private readonly IDepartmentRepository _DepartmentRepository;

    public DepartmentManager(IDepartmentRepository DepartmentRepository)
    {
        _DepartmentRepository = DepartmentRepository;
    }

    public async Task<Department> CreateAsync(
        [NotNull] string name,
         [CanBeNull] string description = null)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));

        var existingDepartment = await _DepartmentRepository.FindByNameAsync(name);
        if (existingDepartment != null)
        {
            throw new DepartmentAlreadyExistsException(name);
        }

        return new Department(
            GuidGenerator.Create(),
            name,
            description
         );
    }

    public async Task ChangeNameAsync(
        [NotNull] Department Department,
        [NotNull] string newName)
    {
        Check.NotNull(Department, nameof(Department));
        Check.NotNullOrWhiteSpace(newName, nameof(newName));

        var existingDepartment = await _DepartmentRepository.FindByNameAsync(newName);
        if (existingDepartment != null && existingDepartment.Id != Department.Id)
        {
            throw new DepartmentAlreadyExistsException(newName);
        }

        Department.ChangeName(newName);
    }
}
