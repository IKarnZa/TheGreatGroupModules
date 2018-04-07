
var data = [];

$("#gridshow").hide();


function SearchStaff() {


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

    var dateFrom = $("#dateFrom").datepicker({ dateFormat: 'dd-mm-yyyy' }).val();
    if (dateFrom == '' || dateFrom == null) {

        $("#toast").dxToast({
            message: "กรุณาเลือกวันที่",
            type: "error",
            displayTime: 3000
        })
        $("#toast").dxToast("show");
        $("#loadIndicator").dxLoadIndicator({
            visible: false
        });
        return;
    }

    var dateTo = $("#dateTo").datepicker({ dateFormat: 'dd-mm-yy' }).val();
    if (dateTo == '' || dateTo == null) {

        $("#toast").dxToast({
            message: "กรุณาเลือกวันที่",
            type: "error",
            displayTime: 3000
        })
        $("#toast").dxToast("show");
        $("#loadIndicator").dxLoadIndicator({
            visible: false
        });
        return;
    }

    var url = "../Report/GetOpenAccountReport?zoneId=" + $("#zoneid").val() +
        "&datefrom=" + $('#dateFrom').val() + "&dateto=" + $('#dateTo').val();

    $.get(url)
        .done(function (data) {
            console.log(data);
            if (data.success == true) {

                Load_DataGrid(data);


                $("#gridshow").show();
                $("#loadIndicator").dxLoadIndicator({
                    visible: false
                });

            } else {

                $("#loadIndicator").dxLoadIndicator({
                    visible: false
                });

                DevExpress.ui.notify(data.errMsg);
            }


        });
}

function btnClear() {
    $("#gridshow").hide();
    $("#zoneid").val('');
    $("#dateFrom").val('');
    $("#dateTo").val('');
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
                dataField: "TotalPayment",
                caption: "ยอดสินเชื่อ",
                width: 120,
                alignment: 'right',
            },

            {
                dataField: "CostAmount",
                caption: "ราคาทุน",
                alignment: 'right',
                width: 100 + "%",
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
         
        ],
       
    });

}




