import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SuppliersRoutingModule } from './suppliers-routing.module';
import { SupplierListComponent } from './supplier-list/supplier-list.component';
import { SupplierModalComponent } from './supplier-modal/supplier-modal.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgZorroAntdModule } from 'ng-zorro-antd';


@NgModule({
  declarations: [SupplierListComponent, SupplierModalComponent],
  imports: [
    CommonModule,
    SuppliersRoutingModule,
    FormsModule, ReactiveFormsModule,
    NgZorroAntdModule
  ],
  entryComponents: [SupplierModalComponent]
})
export class SuppliersModule { }
