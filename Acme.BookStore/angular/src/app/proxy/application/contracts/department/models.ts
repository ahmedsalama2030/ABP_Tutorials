import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateDepartmentDto {
  name: string;
  description?: string;
}

export interface DepartmentDto extends EntityDto<string> {
  name?: string;
  description?: string;
}

export interface DepartmentLookupDto extends EntityDto<string> {
  name?: string;
}

export interface GetDepartmentListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface UpdateDepartmentDto {
  name: string;
  description?: string;
}
