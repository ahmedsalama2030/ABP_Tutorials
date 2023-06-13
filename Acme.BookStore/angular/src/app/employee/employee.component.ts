import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { EmployeeDto } from '@proxy/application/contracts/emplyees';
 import { GenderType, genderTypeOptions } from '@proxy/domain/shared/employees';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Observable, map } from 'rxjs';
import { DepartmentLookupDto } from '@proxy/application/contracts/department';
import { EmployeesService } from '@proxy/application/employees/employees.service';


@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.scss'],
  providers: [
    ListService,
    { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter } // add this line
  ],})
export class EmployeeComponent {
  employees= { items: [], totalCount: 0 } as PagedResultDto<EmployeeDto>;
  isModalOpen = false; // add this line
  form: FormGroup; // add this line
  GenderTypes = genderTypeOptions;
  selectedEmployee = {} as EmployeeDto; // declare selectedBook
  departments: Observable<DepartmentLookupDto[]>;
  constructor(
    public readonly list: ListService,
     private employeeService: EmployeesService,
     private fb: FormBuilder, // inject FormBuilder,
     private confirmation: ConfirmationService
     ) {
      this.departments = employeeService.getDepartmentLookup().pipe(map((r) => r.items));
     }

  ngOnInit() {
    const employeeStreamCreator = (query) => this.employeeService.getList(query);

    this.list.hookToQuery(employeeStreamCreator).subscribe((response) => {
      this.employees = response;
    });
  }
  createEmployee() {
    this.selectedEmployee = {} as EmployeeDto; // reset the selected book
    this.buildForm();
    this.isModalOpen = true;
  }
  buildForm() {
    this.form = this.fb.group({
      firstName: [this.selectedEmployee.firstName || '', Validators.required],
      lastName: [this.selectedEmployee.lastName || '', Validators.required],
      departmentId: [this.selectedEmployee.departmentId || null, Validators.required],
      gender: [this.selectedEmployee.gender || null, Validators.required],
       dateOfBirth: [
        this.selectedEmployee.dateOfBirth ? new Date(this.selectedEmployee.dateOfBirth) : null,
        Validators.required,
      ],
      salary: [this.selectedEmployee.salary || null, Validators.required],
    });
  }
  save() {
    if (this.form.invalid) {
      return;
    }

    const request = this.selectedEmployee.id
    ? this.employeeService.update(this.selectedEmployee.id, this.form.value)
    : this.employeeService.create(this.form.value);


    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }
   // Add editBook method
   editEmployee(id: string) {
    this.employeeService.get(id).subscribe((employee) => {
      this.selectedEmployee = employee;
      this.buildForm();
      this.isModalOpen = true;
    });
  }
  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.employeeService.delete(id).subscribe(() => this.list.get());
      }
    });
  }
}
