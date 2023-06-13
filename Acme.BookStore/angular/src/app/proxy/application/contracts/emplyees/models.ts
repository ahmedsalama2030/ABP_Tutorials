import type { GenderType } from '../../../domain/shared/employees/gender-type.enum';
import type { AuditedEntityDto } from '@abp/ng.core';

export interface CreateUpdateEmployeeDto {
  firstName: string;
  lastName: string;
  gender: GenderType;
  dateOfBirth: string;
  salary: number;
  authorId?: string;
}

export interface EmployeeDto extends AuditedEntityDto<string> {
  firstName?: string;
  lastName?: string;
  gender: GenderType;
  dateOfBirth?: string;
  salary: number;
  departmentId?: string;
  departmentName?: string;
}
