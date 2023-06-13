using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Application.Contracts.Department;
public class GetDepartmentListDto : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
}
