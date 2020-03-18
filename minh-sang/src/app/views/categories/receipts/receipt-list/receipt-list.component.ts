import { Component, OnInit } from '@angular/core';
import {NzModalService} from 'ng-zorro-antd';
import { ReceiptService  } from '../../../../shared/services/receipt-service';
import { NotifyService } from '../../../../shared/services/notify-service';
import { MessageConstant } from '../../../../shared/constants/message-constant';
import { ConfigConstant } from '../../../../shared/constants/config-constant';
import { Receipt  } from '../../../../shared/models/receipt.model';
import { ModalBuilderForService } from 'ng-zorro-antd/modal/nz-modal.service';
import {ReceiptModalComponent} from '../receipt-modal/receipt-modal.component'
import { Pagination, PaginatedResult } from '../../../../shared/models/pagination.model';
import { PagingParams } from '../../../../shared/params/paging-params.model';


@Component({
  selector: 'app-receipt-list',
  templateUrl: './receipt-list.component.html',
  styleUrls: ['./receipt-list.component.scss']
})
export class ReceiptListComponent implements OnInit {

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

  constructor(private receiptService: ReceiptService,
     private notify: NotifyService,
     private modalService: NzModalService) { }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.receiptService.getAllPaging(this.pagination.currentPage, this.pagination.itemsPerPage, this.pagingParams)
      .subscribe((res: PaginatedResult<Receipt[]>) => {

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
      this.receiptService.delete(id).subscribe((res :boolean)=>{
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
      nzTitle: 'Thêm mới phiếu nhập',
      nzContent: ReceiptModalComponent,
      nzStyle: {
        top: ConfigConstant.MODAL_TOP_20PX
      },
      nzBodyStyle: {
        padding: ConfigConstant.MODAL_BODY_PADDING_HORIZONTAL
      },
      nzMaskClosable: false,
      nzClosable: false,
      nzComponentParams: {
        receipt: {id: 0, name:"", ngaynhap: null,supplierId: null, supplierName: "", inventoryId: null, inventoryName:"",
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

  update(receipt: Receipt) {
    const modal = this.modalService.create({
      nzTitle: 'Sửa thông tin phiếu nhập',
      nzContent: ReceiptModalComponent,
      nzStyle: {
        top: ConfigConstant.MODAL_TOP_20PX
      },
      nzBodyStyle: {
        padding: ConfigConstant.MODAL_BODY_PADDING_HORIZONTAL
      },
      nzMaskClosable: false,
      nzClosable: false,
      nzComponentParams: {
        receipt: receipt,
        isAddNew: false
      }
    });

    modal.afterClose.subscribe((result: boolean) => {
      if (result) {
        this.loadData();
      }
    });

    console.log(receipt);
  }

  
}
