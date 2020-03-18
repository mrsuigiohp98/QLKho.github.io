import { Component, OnInit, Input, HostListener } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NzModalRef } from 'ng-zorro-antd';
import { Inventory } from '../../../../shared/models/inventory.model'
import { NotifyService } from '../../../../shared/services/notify-service'
import { MessageConstant } from '../../../../shared/constants/message-constant'
import { InventoryService } from '../../../../shared/services/inventory-service'
import { StockService } from '../../../../shared/services/stock-service'
import { UnitService } from '../../../../shared/services/unit-service'
import { Stock } from 'src/app/shared/models/stock.model';
import { Unit } from 'src/app/shared/models/unit.model';

@Component({
  selector: 'app-inventory-modal',
  templateUrl: './inventory-modal.component.html',
  styleUrls: ['./inventory-modal.component.scss']
})


export class InventoryModalComponent implements OnInit {

  @Input() inventory: Inventory;
  @Input() isAddNew: boolean;

  inventoryForm: FormGroup;
  loadingSaveChanges: boolean;

  listOfUnits = [];
  listOfStocks = [];

  constructor(private fb: FormBuilder,
    private modal: NzModalRef,
    private inventoryService: InventoryService,
    private stockService: StockService,
    private unitService: UnitService,
    private notify: NotifyService) { }

  ngOnInit() {

    this.loadStocks();
    this.loadUnits();
    this.createForm();
    this.inventoryForm.reset();
    this.inventoryForm.patchValue(this.inventory);
  }

  createForm() {
    this.inventoryForm = this.fb.group({
      id: [null],
      name: [null, [Validators.required]],
      soluong:[null, [Validators.required]],
      noiSX: [null, [Validators.required]],
      unitId:  [Validators.required],
      stockId: [Validators.required]


      // createdDate: [null],
      // createdBy: [null],
      // status: [null]
    });
  }

  loadUnits() {
    this.unitService.getAll().subscribe((res: Unit[]) => {     
      this.listOfUnits = res;
    });
  }

  loadStocks() {
    this.stockService.getAll().subscribe((res: Stock[]) => {     
      this.listOfStocks = res;
    });
  }

  destroyModal(){
    this.modal.destroy(false);
  }

  saveChanges(){
    const inventory = this.inventoryForm.getRawValue();
    console.log(inventory);
    if(this.isAddNew){
      this.inventoryService.addNew(inventory).subscribe((res: any) => {
        if (res) {
          this.notify.success(MessageConstant.CREATED_OK_MSG);
          this.modal.destroy(true);
        }

        this.loadingSaveChanges = false;
      }, _ => this.loadingSaveChanges = false);
      
    }
    else{
      this.inventoryService.update(inventory).subscribe((res: any) => {
        if (res) {
          this.notify.success(MessageConstant.UPDATED_OK_MSG);
          this.modal.destroy(true);
        }

        this.loadingSaveChanges = false;
      }, _ => this.loadingSaveChanges = false);
    }

  }

}
