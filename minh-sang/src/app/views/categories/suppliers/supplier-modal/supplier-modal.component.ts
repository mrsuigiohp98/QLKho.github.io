import { Component, OnInit, Input, HostListener } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NzModalRef } from 'ng-zorro-antd';
import { NotifyService } from '../../../../shared/services/notify-service'
import { MessageConstant } from '../../../../shared/constants/message-constant'
import { Supplier } from 'src/app/shared/models/supplier.model';
import { SupplierService } from 'src/app/shared/services/supplier-service';

@Component({
  selector: 'app-supplier-modal',
  templateUrl: './supplier-modal.component.html',
  styleUrls: ['./supplier-modal.component.scss']
})


export class SupplierModalComponent implements OnInit {

  @Input() supplier: Supplier;

  @Input() isAddNew: boolean;

  supplierForm: FormGroup;
  loadingSaveChanges: boolean;

  constructor(private fb: FormBuilder,
    private modal: NzModalRef,
    private supplierService: SupplierService,
    private notify: NotifyService) { }

  ngOnInit() {
    this.createForm();
    this.supplierForm.reset();
    this.supplierForm.patchValue(this.supplier);
  }

  createForm() {
    this.supplierForm = this.fb.group({
      id: [null],
      name: [null, [Validators.required]],
      diachi: [null, [Validators.required]],
      sdt: [null, [Validators.required]]
      // status: [null]
    });
  }

  destroyModal(){
    this.modal.destroy(false);
  }

  saveChanges(){
    const supplier = this.supplierForm.getRawValue();
    console.log(supplier);
    if(this.isAddNew){
      this.supplierService.addNew(supplier).subscribe((res: any) => {
        if (res) {
          this.notify.success(MessageConstant.CREATED_OK_MSG);
          this.modal.destroy(true);
        }

        this.loadingSaveChanges = false;
      }, _ => this.loadingSaveChanges = false);
      
    }
    else{
      this.supplierService.update(supplier).subscribe((res: any) => {
        if (res) {
          this.notify.success(MessageConstant.UPDATED_OK_MSG);
          this.modal.destroy(true);
        }

        this.loadingSaveChanges = false;
      }, _ => this.loadingSaveChanges = false);
    }

  }

}
