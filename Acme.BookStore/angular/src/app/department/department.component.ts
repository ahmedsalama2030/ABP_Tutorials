
import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { EmployeeDto } from '@proxy/application/contracts/emplyees';
 import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { DepartmentDto } from '@proxy/application/contracts/department';
import { DepartmentsService } from '@proxy/application/departments';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.scss'],
  providers: [
    ListService,
    { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter } // add this line
  ],})
export class DepartmentComponent {
  entities= { items: [], totalCount: 0 } as PagedResultDto<DepartmentDto>;
  isModalOpen = false; // add this line
  form: FormGroup; // add this line
   selectedEntity = {} as DepartmentDto; // declare selectedBook
  constructor(
    public readonly list: ListService,
     private departmentService: DepartmentsService,
     private fb: FormBuilder, // inject FormBuilder,
     private confirmation: ConfirmationService
     ) {}

  ngOnInit() {
    const employeeStreamCreator = (query) => this.departmentService.getList(query);

    this.list.hookToQuery(employeeStreamCreator).subscribe((response) => {
      this.entities = response;
    });
  }
  createEmployee() {
    this.selectedEntity = {} as EmployeeDto; // reset the selected book
    this.buildForm();
    this.isModalOpen = true;
  }
  buildForm() {
    this.form = this.fb.group({
      name: [this.selectedEntity.name || '', Validators.required],
      description: [this.selectedEntity.description || ''],

    });
  }
  save() {
    if (this.form.invalid) {
      return;
    }

    if (this.selectedEntity.id) {
      this.departmentService
        .update(this.selectedEntity.id, this.form.value)
        .subscribe(() => {
          this.isModalOpen = false;
          this.form.reset();
          this.list.get();
        });
    } else {
      this.departmentService.create(this.form.value).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
      });
    }
  }
   // Add editBook method
   editEmployee(id: string) {
    this.departmentService.get(id).subscribe((employee) => {
      this.selectedEntity = employee;
      this.buildForm();
      this.isModalOpen = true;
    });
  }
  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.departmentService.delete(id).subscribe(() => this.list.get());
      }
    });
  }
}
