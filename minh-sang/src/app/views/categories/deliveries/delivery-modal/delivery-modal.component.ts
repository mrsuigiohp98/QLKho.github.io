import { Component, OnInit, Input, HostListener } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NzModalRef } from 'ng-zorro-antd';
import { Delivery } from '../../../../shared/models/delivery.model'
import { NotifyService } from '../../../../shared/services/notify-service'
import { MessageConstant } from '../../../../shared/constants/message-constant'
import { InventoryService } from '../../../../shared/services/inventory-service'
import { Inventory } from 'src/app/shared/models/inventory.model';
import { DeliveryService } from 'src/app/shared/services/delivery-service';
import { Customer } from 'src/app/shared/models/customer.model';
import { CustomerService } from 'src/app/shared/services/customer-service'

@Component({
  selector: 'app-delivery-modal',
  templateUrl: './delivery-modal.component.html',
  styleUrls: ['./delivery-modal.component.scss']
})


export class DeliveryModalComponent implements OnInit {

  @Input() delivery: Delivery;
  @Input() isAddNew: boolean;

  deliveryForm: FormGroup;
  loadingSaveChanges: boolean;

  listOfInventories = [];
  listOfCustomers = []; 

  constructor(private fb: FormBuilder,
    private modal: NzModalRef,
    private deliveryService: DeliveryService,
    private inventoryService: InventoryService,
    private customerService: CustomerService,
    private notify: NotifyService) { }

  ngOnInit() {

    this.loadInventories();
    this.loadCustomers();
    this.createForm();
    this.deliveryForm.reset();
    this.deliveryForm.patchValue(this.delivery);
  }

  createForm() {
    this.deliveryForm = this.fb.group({
      id: [null],
      name: [null, [Validators.required]],
      ngayxuat: [null, [Validators.required]],
      customerId: [Validators.required],
      inventoryId: [Validators.required],
      soluong:[null, [Validators.required]],
      dongia: [null, [Validators.required]],
      thanhtien: [null, [Validators.required]]


      // createdDate: [null],
      // createdBy: [null],
      // status: [null]
    });
  }
  loadCustomers() {
    this.customerService.getAll().subscribe((res: Customer[]) => {     
      this.listOfCustomers = res;
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
    const delivery = this.deliveryForm.getRawValue();
    console.log(delivery);
    if(this.isAddNew){
      this.deliveryService.addNew(delivery).subscribe((res: any) => {
        if (res) {
          this.notify.success(MessageConstant.CREATED_OK_MSG);
          this.modal.destroy(true);
        }

        this.loadingSaveChanges = false;
      }, _ => this.loadingSaveChanges = false);
      
    }
    else{
      this.deliveryService.update(delivery).subscribe((res: any) => {
        if (res) {
          this.notify.success(MessageConstant.UPDATED_OK_MSG);
          this.modal.destroy(true);
        }

        this.loadingSaveChanges = false;
      }, _ => this.loadingSaveChanges = false);
    }

  }

}
