
var data = [];

$("#gridshow").hide();


function SearchStaff() {

    $("#loadIndicator").dxLoadIndicator({
        visible: true
    });

    $.post("../Customers/GetCustomers", {

        CustomerFirstName: "",
        CustomerLastName: "",
        CustomerMobile: "",
        CustomerIdCard: "",

    })
.done(function (data) {
    console.log(data);
    if (data.success == true) {

        Load_DataGrid(data.data);


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
    $("#CustomerFirstName").val('');
    $("#CustomerLastName").val('');
    $("#CustomerMobile").val('');
    $("#CustomerIdCard").val('');
}
function Load_DataGrid(data) {
    data = [{
        No: "1.",
        data1:"GT00009999",
        data2: "นายวิรุศ ภักดี",
        data3: "2",
        data4: "250.00",
        data5: "250.00",
        data6: "7,900.00",
        data7: "",
    }, {
        No: "2.",
        data1: "GT0009499",
        data2: "นายธนวัตร มีดี",
        data3: "45",
        data4: "250.00",
        data5: "250.00",
        data6: "8,250.00",
        data7: "",
    },
    ]
    $("#gridContainer").dxDataGrid({
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
            visible: true,
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
        columns: [{
            dataField: "No",
            caption: "ลำดับ",
            width: 140,
            alignment: 'center',
            allowFiltering: false
        },  {
            dataField: "data1",
            caption: "เลขที่สัญญา",
            width: 140,
            alignment: 'left',
            allowFiltering: false
        },
        {
            dataField: "data2",
            caption: "ชื่อ-นามสกุล",
            width: 240,
        },
         {
             dataField: "data3",
             caption: "งวดที่",
             alignment: 'center',
             width: 120,

         },
          {
              dataField: "data4",
              caption: "ยอดเรียกเก็บ",
              width: 150,
              alignment: 'right',
          },
      
        {
            dataField: "data5",
            caption: "ยอดที่ชำระ",
            alignment: 'right',
            width: 150,
        },

            {
                dataField: "data6",
                caption: "ยอดคงเหลือ",
                width: 150,
                alignment: 'right',

            },
        {
            dataField: "data7",
            caption: "หมายเหตุ",
            alignment: 'center',
            width: 200,
        },
       

        ],
        summary: {
            totalItems: [
                { column: 'data2', displayFormat: 'จำนวนลูกค้าในสายทั้งหมด 32 คน' },
                 { column: 'data3', displayFormat: 'ยอดรวมทั้งสิ้น' },
                   { column: 'data4', displayFormat: '250.00' },
                     { column: 'data5', displayFormat: '250.00' },
                      { column: 'data6', displayFormat: '16,150.00' },
            ],},
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
                        DevExpress.ui.notify("Export PDF Successful!");
                    }
                }
            })
        }
    });

}
