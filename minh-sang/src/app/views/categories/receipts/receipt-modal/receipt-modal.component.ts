import { Component, OnInit, Input, HostListener } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NzModalRef } from 'ng-zorro-antd';
import { Receipt } from '../../../../shared/models/receipt.model'
import { NotifyService } from '../../../../shared/services/notify-service'
import { MessageConstant } from '../../../../shared/constants/message-constant'
import { InventoryService } from '../../../../shared/services/inventory-service'
import { ReceiptService } from 'src/app/shared/services/receipt-service';
import { Inventory } from 'src/app/shared/models/inventory.model';
import { Supplier } from 'src/app/shared/models/supplier.model';
import { SupplierService } from 'src/app/shared/services/supplier-service';

@Component({
  selector: 'app-receipt-modal',
  templateUrl: './receipt-modal.component.html',
  styleUrls: ['./receipt-modal.component.scss']
})


export class ReceiptModalComponent implements OnInit {

  @Input() receipt: Receipt;
  @Input() isAddNew: boolean;

  receiptForm: FormGroup;
  loadingSaveChanges: boolean;

  listOfInventories = [];
  listOfSuppliers = [];

  constructor(private fb: FormBuilder,
    private modal: NzModalRef,
    private receiptService: ReceiptService,
    private inventoryService: InventoryService,
    private supplierService: SupplierService,
    private notify: NotifyService) { }

  ngOnInit() {

    this.loadInventories();
    this.loadSuppliers();
    this.createForm();
    this.receiptForm.reset();
    this.receiptForm.patchValue(this.receipt);
  }

  createForm() {
    this.receiptForm = this.fb.group({
      id: [null],
      name: [null, [Validators.required]],
      ngaynhap: [null, [Validators.required]],
      supplierId: [Validators.required],
      inventoryId: [Validators.required],
      soluong:[null, [Validators.required]],
      dongia: [null, [Validators.required]],
      thanhtien: [null, [Validators.required]]


      // createdDate: [null],
      // createdBy: [null],
      // status: [null]
    });
  }

  loadSuppliers() {
    this.supplierService.getAll().subscribe((res: Supplier[]) => {     
      this.listOfSuppliers = res;
    });
  }

 

  loadInventories() {
    this.inventoryService.getAll().subscribe((res: Inventory[]) => {     
      this.listOfInventories = res;
    });
  }

  destroyModal(){
    this.modal.destroy(false);
  }

  saveChanges(){
    const receipt = this.receiptForm.getRawValue();
    console.log(receipt);
    if(this.isAddNew){
      this.receiptService.addNew(receipt).subscribe((res: any) => {
        if (res) {
          this.notify.success(MessageConstant.CREATED_OK_MSG);
          this.modal.destroy(true);
        }

        this.loadingSaveChanges = false;
      }, _ => this.loadingSaveChanges = false);
      
    }
    else{
      this.receiptService.update(receipt).subscribe((res: any) => {
        if (res) {
          this.notify.success(MessageConstant.UPDATED_OK_MSG);
          this.modal.destroy(true);
        }

        this.loadingSaveChanges = false;
      }, _ => this.loadingSaveChanges = false);
    }

  }

}
