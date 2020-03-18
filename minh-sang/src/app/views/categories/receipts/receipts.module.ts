import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';


import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgZorroAntdModule } from 'ng-zorro-antd';
import { ReceiptListComponent } from './receipt-list/receipt-list.component';
import { ReceiptModalComponent } from './receipt-modal/receipt-modal.component';
import { ReceiptsRoutingModule } from './receipts-routing.module';

@NgModule({
  declarations: [ReceiptListComponent, ReceiptModalComponent],
  imports: [
    CommonModule,
    ReceiptsRoutingModule,
    FormsModule, ReactiveFormsModule,
    NgZorroAntdModule
  ],

  entryComponents: [ReceiptModalComponent]
})
export class ReceiptsModule { }
