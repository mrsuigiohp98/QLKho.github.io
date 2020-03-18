import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SupplierListComponent } from './supplier-list/supplier-list.component';

const routes: Routes = [
  {
      path: '',
      data: {
          breadcrumb: 'Nhà cung cấp'

      },
      children: [
          {
              path: 'danh-sach',
              component: SupplierListComponent,
              data: {
                  breadcrumb: 'Danh sách'
              }
          },
          {
              path: '',
              redirectTo: 'danh-sach',
              pathMatch: 'full'
          }
      ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SuppliersRoutingModule { }
