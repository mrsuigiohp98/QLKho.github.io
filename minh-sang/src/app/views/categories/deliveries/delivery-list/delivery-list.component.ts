import { Component, OnInit } from '@angular/core';
import {NzModalService} from 'ng-zorro-antd';
import { DeliveryService  } from '../../../../shared/services/delivery-service';
import { NotifyService } from '../../../../shared/services/notify-service';
import { MessageConstant } from '../../../../shared/constants/message-constant';
import { ConfigConstant } from '../../../../shared/constants/config-constant';
import { Delivery  } from '../../../../shared/models/delivery.model';
import { ModalBuilderForService } from 'ng-zorro-antd/modal/nz-modal.service';
import {DeliveryModalComponent} from '../delivery-modal/delivery-modal.component'
import { Pagination, PaginatedResult } from '../../../../shared/models/pagination.model';
import { PagingParams } from '../../../../shared/params/paging-params.model';


@Component({
  selector: 'app-delivery-list',
  templateUrl: './delivery-list.component.html',
  styleUrls: ['./delivery-list.component.scss']
})
export class DeliveryListComponent implements OnInit {

  listOfData = []
  pagination: Pagination = {
    currentPage: 1,
    itemsPerPage: 10
  }

  pagingParams: PagingParams = {
    pageNumber: 1,
    pageSize: 5,
    keyword: '',
    sortKey: '',
    sortValue: '',
    searchKey: '',
    searchValue: ''
  };

  constructor(private deliveryService: DeliveryService,
     private notify: NotifyService,
     private modalService: NzModalService) { }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.deliveryService.getAllPaging(this.pagination.currentPage, this.pagination.itemsPerPage, this.pagingParams)
      .subscribe((res: PaginatedResult<Delivery[]>) => {

        this.pagination = res.pagination;
        this.listOfData = res.result;

         console.log(res);
      });
  }
  sort(sort: { key: string, value: string }): void {
    this.pagingParams.sortKey = sort.key;
    this.pagingParams.sortValue = sort.value;

    console.log(this.pagingParams);
    this.loadData();
  }

  search(keyword: string) {
    this.pagingParams.searchValue = "name";
    this.pagingParams.searchKey = keyword;
    this.loadData();
  }
  searchColumn(searchKey: string) {
    this.pagingParams.searchKey = searchKey;



    //this.loadData();
  }



  

  delete(id: number){
    this.notify.confirm(MessageConstant.CONFIRM_DELETE_MSG, () => { 
      this.deliveryService.delete(id).subscribe((res :boolean)=>{
        if (res) {
          this.notify.success(MessageConstant.DELETED_OK_MSG);
          this.loadData();
        }
      });
    });
    console.log(id);
  }

  addNew(){
    const modal = this.modalService.create({
      nzTitle: 'Thêm mới phiếu xuất',
      nzContent: DeliveryModalComponent,
      nzStyle: {
        top: ConfigConstant.MODAL_TOP_20PX
      },
      nzBodyStyle: {
        padding: ConfigConstant.MODAL_BODY_PADDING_HORIZONTAL
      },
      nzMaskClosable: false,
      nzClosable: false,
      nzComponentParams: {
        delivery: {id: 0, name:"", ngayxuat: null, customerId: null, customerName: "", inventoryId: null, inventoryName:"",
                     soluong: null, dongia: null, thanhtien: null},
        isAddNew: true
      }
    });

    modal.afterClose.subscribe((result: boolean) => {
      if (result) {
        this.loadData();
      }
    });
  }

  update(delivery: Delivery) {
    const modal = this.modalService.create({
      nzTitle: 'Sửa thông tin phiếu xuất',
      nzContent: DeliveryModalComponent,
      nzStyle: {
        top: ConfigConstant.MODAL_TOP_20PX
      },
      nzBodyStyle: {
        padding: ConfigConstant.MODAL_BODY_PADDING_HORIZONTAL
      },
      nzMaskClosable: false,
      nzClosable: false,
      nzComponentParams: {
        delivery: delivery,
        isAddNew: false
      }
    });

    modal.afterClose.subscribe((result: boolean) => {
      if (result) {
        this.loadData();
      }
    });

    console.log(delivery);
  }

  
}
