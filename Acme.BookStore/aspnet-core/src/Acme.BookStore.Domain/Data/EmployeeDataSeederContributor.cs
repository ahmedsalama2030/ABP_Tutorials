using System;
using System.Threading.Tasks;
using Acme.BookStore.Domain.Departments;
using Acme.BookStore.Domain.Employees;
using Acme.BookStore.Domain.Shared.Employees;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Acme.EmployeeStore.Domain.Data;
public class EmployeeDataSeederContributor : IDataSeedContributor, ITransientDependency
{
     private readonly IRepository<Employee, Guid> _employeeRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly DepartmentManager _departmentManager;

    public EmployeeDataSeederContributor(
        IRepository<Employee, Guid> EmployeeRepository,
          IDepartmentRepository departmentRepository,
        DepartmentManager DepartmentManager

        )
    {
        _employeeRepository = EmployeeRepository;
        _departmentRepository = departmentRepository;
        _departmentManager = DepartmentManager;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _employeeRepository.GetCountAsync() <0)
        {
             

        await _employeeRepository.InsertAsync(
            new Employee
            {
                FirstName = "ahmed",
                LastName="salama",
                Gender = GenderType.male,
                DateOfBirth = new DateTime(1995, 9, 10),
                Salary = 5000.37F
            },
            autoSave: true
        );

        await _employeeRepository.InsertAsync(
            new Employee
            {
                FirstName = "Dina",
                LastName="Ahmed",
                Gender = GenderType.female,
                DateOfBirth = new DateTime(2000, 9, 10),
                Salary = 9000.89F
            },
            autoSave: true
        );
        }
          if (await _departmentRepository.GetCountAsync() <= 0)
        {
            await _departmentRepository.InsertAsync(
                await _departmentManager.CreateAsync(
                    "hr",
                     
                    ""
                )
            );

            await _departmentRepository.InsertAsync(
                await _departmentManager.CreateAsync(
                    "sales",
                     ""
                )
            );
        }
    }
}

