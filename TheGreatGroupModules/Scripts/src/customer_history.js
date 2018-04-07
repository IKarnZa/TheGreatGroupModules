﻿
var data = [];

$("#gridshow").hide();


function SearchCustomer() {


    $("#loadIndicator").dxLoadIndicator({
        visible: true
    });
    if ($("#zoneid").val() == '') {

        $("#toast").dxToast({
            message: "กรุณาเลือกสาย",
            type: "error",
            displayTime: 3000
        })
        $("#toast").dxToast("show");
        $("#loadIndicator").dxLoadIndicator({
            visible: false
        });
        return;
    }

    if ($("#CustomerID").val() == '') {

        $("#toast").dxToast({
            message: "กรุณาเลือกรายชื่อลูกค้า",
            type: "error",
            displayTime: 3000
        })
        $("#toast").dxToast("show");
        $("#loadIndicator").dxLoadIndicator({
            visible: false
        });
        return;
    }
    
    if ($("#ContractID").val() == '') {

        $("#toast").dxToast({
            message: "กรุณาเลือกหนังสือสัญญา",
            type: "error",
            displayTime: 3000
        })
        $("#toast").dxToast("show");
        $("#loadIndicator").dxLoadIndicator({
            visible: false
        });
        return;
    }
    setTimeout(function () {
        $("#loadIndicator").dxLoadIndicator({
            visible: false
        });
    },2000);
  

/////////////////////////////////////////////////// Grid View ///////////////////////////////////////////////////////////////

//    var url = "../ManagePayment/GetDailyReceiptsReport?staffId=" + $("#StaffID").val() +
//        "&dateAsOf=" + $('#DateAsOf').val();

//    $.get(url)
//        .done(function (data) {
//            console.log(data);
//            if (data.success == true) {

//                Load_DataGrid(data);


//                $("#gridshow").show();
//                $("#loadIndicator").dxLoadIndicator({
//                    visible: false
//                });

//            } else {

//                $("#loadIndicator").dxLoadIndicator({
//                    visible: false
//                });

//                DevExpress.ui.notify(data.errMsg);
//            }


//        });


}


function btnClear() {
    $("#gridshow").hide();
    $("#zoneid").val('');
    $("#CustomerID").val('');
    $("#ContractID").val('');
    $("#loadIndicator").dxLoadIndicator({
        visible: false
    });
}

function btnSaveData() {

    $("#loadIndicator").dxLoadIndicator({
        visible: true
    });

    var url = "../ManagePayment/SaveActivateDailyReceipts?staffId=" + $("#StaffID").val() +
        "&dateAsOf=" + $('#DateAsOf').val();

    $.get(url)
        .done(function (data) {

            if (data.success == true) {
                SearchCustomer();
                DevExpress.ui.notify("บันทึกการตรวจสอบสำเร็จ !!!");
                $("#loadIndicator").dxLoadIndicator({
                    visible: false
                });

            } else {
                DevExpress.ui.notify(data.errMsg);
                $("#loadIndicator").dxLoadIndicator({
                    visible: false
                });

            }

        });
}

function Load_DataGrid(data) {


    $("#gridContainer").dxDataGrid({
        dataSource: data.data,
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
            enabled: true,
            fileName: "File",
        },

        allowColumnReordering: true,
        allowColumnResizing: true,
        columnAutoWidth: true,
        height: 500,
        columnFixing: {
            enabled: true
        },
        columns: [
            {
                dataField: "ContractNumber",
                caption: "เลขที่สัญญา",
                width: 120 + "%",
                alignment: 'left',
                allowFiltering: false,
                fixed: false,
                fixedPosition: 'left',
            },
            {
                dataField: "CustomerName",
                caption: "ชื่อ-นามสกุล",
                width: 230 + "%",
                fixed: false,
                fixedPosition: 'left',
            },
            {
                dataField: "ContractCreateDate_Text",
                caption: "วันที่ทำสัญญา",
                alignment: 'center',
                width: 120 + "%",

            },
            {
                dataField: "ContractExpDate_Text",
                caption: "วันที่หมดสัญญา",
                width: 120,
                alignment: 'right',
            },

            {
                dataField: "ContractAmount_Text",
                caption: "งวดละ",
                alignment: 'right',
                width: 100 + "%",
            },
            {
                dataField: "PriceReceipts_Text",
                caption: "ยอดที่ชำระ",
                alignment: 'right',
                width: 100 + "%",
                fixed: false,
                fixedPosition: 'right',

            },
            {
                dataField: "Balance_Text",
                caption: "ยอดคงเหลือ",
                width: 120 + "%",
                alignment: 'right',
                fixed: false,
                fixedPosition: 'right',
            },
            //{
            //    dataField: "Remark",
            //    caption: "หมายเหตุ",
            //    alignment: 'center',
            //    width: 100 + "%",
            //    fixed: false,
            //    fixedPosition: 'right',
            //    cellTemplate: function (container, options) {
            //        $("<div>")
            //            .append("<button  title='ระบุหมายเหตุ' class='btn btn-info btn-circle btn-sm' ><i class='fa fa-pencil'></i></a>")
            //            .appendTo(container);
            //    }
            //},
            {
                dataField: "Status",
                caption: "สถานะ",
                alignment: 'center',
                width: 100 + "%",
                fixed: true,
                fixedPosition: 'right'
            },

        ],
        summary: {
            totalItems: [
                { column: 'CustomerName', displayFormat: 'จำนวนลูกค้าทั้งหมด ' + data.countData + ' คน' },
                { column: 'ContractExpDate_Text', displayFormat: 'ยอดรวม' },
                { column: 'PriceReceipts_Text', displayFormat: data.SumData },
                { column: 'ContractAmount_Text', displayFormat: data.SumDataContractAmount }
            ],
        },
        onToolbarPreparing: function (e) {
            e.toolbarOptions.items.push({
                location: "before",
                widget: "dxButton",
                options: {
                    icon: "export",
                    //text: "",
                    onInitialized: function (e) {
                        clearFilterButton = e.component;
                    },
                    onClick: function (e) {
                        window.open('/Report/DailyReport.aspx?staffID=' + $("#StaffID").val() +
                            "&date=" + $('#DateAsOf').val(), '_blank');
                        //window.location.href = '/Report/ReportPage1.aspx?staffID=' + $("#StaffID").val() +
                        //"&date=" + $('#DateAsOf').val();
                        DevExpress.ui.notify("Export PDF Successful!");
                    }
                }
            })
        }
    });

}



