import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgZorroAntdModule } from 'ng-zorro-antd';
import { DeliveryListComponent } from './delivery-list/delivery-list.component';
import { DeliveryModalComponent } from './delivery-modal/delivery-modal.component';
import { DeliveriesRoutingModule } from './deliveries-routing.module';

@NgModule({
  declarations: [DeliveryListComponent, DeliveryModalComponent],
  imports: [
    CommonModule,
    DeliveriesRoutingModule,
    FormsModule, ReactiveFormsModule,
    NgZorroAntdModule
  ],

  entryComponents: [DeliveryModalComponent]
})
export class DeliveriesModule { }
