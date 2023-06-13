import { mapEnumToOptions } from '@abp/ng.core';

export enum GenderType {
  male = 0,
  female = 1,
}

export const genderTypeOptions = mapEnumToOptions(GenderType);
