

$(function () {

    Call_Grid("");


})
function SearchStaff() {

   
    Call_Grid($("#CustomerIdCard").val());
}
function Call_Grid(data) {

   
    $.ajax({
        url: '../Contract/GetApproveOpen_CloseContract?custpmerIDCard=' + data,
        type: 'GET',
        contentType: 'application/json',
        success: function (data) {

            if (data.success) {

                Load_DataGrid1(data.dataOpen);
                Load_DataGrid2(data.dataClose);
            } else {
                alertError(data.errMsg);

            }
        

        },
        error: function () {
            console.log("error");
        }
    });
}

function btnClear() {

    $("#CustomerIdCard").val("");
    Call_Grid("");
}

function Load_DataGrid1(data) {


    $("#gridContainer").dxDataGrid({
       dataSource: data,
        showColumnLines: true,
        showRowLines: true,

        rowAlternationEnabled: true,
        showBorders: true,
        selection: {
            mode: "single"
        },
        searchPanel: {
            visible: true,
            width: 300,
            placeholder: "ค้นหา..."
        },
        filterRow: {
            visible: false,
            applyFilter: "auto"
        },
        export: {
            enabled: false,
            fileName: "File",
        },

        allowColumnReordering: true,
        allowColumnResizing: true,
        columnAutoWidth: true,
        height: 300,
        columnFixing: {
            enabled: true
        },
        columns: [
           
            {
                dataField: "ContractNumber",
                caption: "เลขที่สัญญา",
                alignment: 'left',
                width: 100,
                fixed: false,
                fixedPosition: 'left',
            },
            {
                dataField: "CustomerName",
                caption: "ชื่อ-นามสกุล ลูกค้า",
                alignment: 'left',
                width: 150,
            },
               {
                   dataField: "ContractCreateDate_Text",
                   caption: "วันที่ทำสัญญา",
                   alignment: 'center',


               },
                {
                    dataField: "ContractExpDate_Text",
                    caption: "วันที่สิ้นสุดสัญญา",
                    alignment: 'center',


                },
                 {
                     dataField: "TotalSales_Text",
                     caption: "จำนวนเงินทั้งหมด",
                     alignment: 'right',
                 },
                 {
                     dataField: "PriceReceipts_Text",
                     caption: "ชำระแล้ว",
                     alignment: 'right',
                 },
                  {
                      dataField: "Balance_Text",
                      caption: "คงเหลือ",
                      alignment: 'right',
                  },

                  
            {
                dataField: "ContractID",
                caption: "ให้ส่วนลด",
                alignment: 'center',
                width: 100,
                fixed: true,
                fixedPosition: 'right',
                verticalAlignment: 'middle',
                cellTemplate: function (container, options) {
                    $("<div>")
                         .append("<button type='link' onclick='Show_PopupEdit("
                         + '"' + options.data.CustomerName
                         + '","' + options.data.Balance_Text
                         + '","' + options.data.ContractID
                         + '","' + options.data.CustomerID

                         + '"' + ")' title='แก้ไขพื้นที่'  class='btn btn-info btn-circle btn-sm' ><i class='fa fa-money'></i></button>")
                         .appendTo(container);
                }
            },
          
          

        ],

    });

}

function Load_DataGrid2(data) {


    $("#gridContainer2").dxDataGrid({
      dataSource: data,
        showColumnLines: true,
        showRowLines: true,

        //  rowAlternationEnabled: true,
        showBorders: true,
        selection: {
            mode: "single"
        },
        searchPanel: {
            visible: true,
            width: 300,
            placeholder: "ค้นหา..."
        },
        filterRow: {
            visible: false,
            applyFilter: "auto"
        },
        export: {
            enabled: false,
            fileName: "File",
        },

        allowColumnReordering: true,
        allowColumnResizing: true,
        columnAutoWidth: true,
        height: 300,
        columnFixing: {
            enabled: true
        },
        columns: [

          {
              dataField: "ContractNumber",
              caption: "เลขที่สัญญา",
              alignment: 'left',
              width: 100,
              fixed: false,
              fixedPosition: 'left',
          },
          {
              dataField: "CustomerName",
              caption: "ชื่อ-นามสกุล ลูกค้า",
              alignment: 'left',
              width: 150,
          },
             {
                 dataField: "ContractCreateDate_Text",
                 caption: "วันที่ทำสัญญา",
                 alignment: 'center',


             },
              {
                  dataField: "ContractExpDate_Text",
                  caption: "วันที่สิ้นสุดสัญญา",
                  alignment: 'center',


              },
               {
                   dataField: "TotalSales_Text",
                   caption: "จำนวนเงินทั้งหมด",
                   alignment: 'right',
               },
               {
                   dataField: "PriceReceipts_Text",
                   caption: "ชำระแล้ว",
                   alignment: 'right',
               },
                {
                    dataField: "Balance_Text",
                    caption: "คงเหลือ",
                    alignment: 'right',
                },


          {
              dataField: "Balance_Text",
              caption: "ให้ส่วนลด",
              alignment: 'center',
              width: 100,
              fixed: true,
              fixedPosition: 'right',
              verticalAlignment: 'middle',
              cellTemplate: function (container, options) {
                  $("<div>")
                       .append("<lable class='label label-warning' >" + options.data.Balance_Text + "</label>")
                       .appendTo(container);
              }
          },



        ],
    });

}

function Show_PopupEdit(CustomerName,Discount,ID) {
  
    //alertConfirm("ต้องการให้ส่วนลดคุณ ธิดา ชัยชา  จำนวน " + data + " บาท ใช่หรือไม่ ? ", "ให้ส่วนลดสำเร็จ", "ยกเลิกการให้ส่วนลด");
    swal({
        title:"",
        text: "ต้องการให้ส่วนลด " + CustomerName + "  จำนวน " + Discount + " บาท ใช่หรือไม่ ",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'ตกลง',
        cancelButtonText: "ยกเลิก",
        closeOnConfirm: false,
        closeOnCancel: false
    },
function (isConfirm) {

    if (isConfirm) {
        swal("สำเร็จ !", "", "success");


        var data = {
            CustomerID: 1,
            ContractID: 1,
            PriceReceipts:1000,
        }


     ///   /Contract/PostAddDiscount

    } else {
        swal.close();
        e.preventDefault();
    }
});
    
}