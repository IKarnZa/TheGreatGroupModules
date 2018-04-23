$(function () {
    var data = [];
    Load_DataGrid(data);
});





function Load_DataGrid(data) {

    $("#gridContainer").dxDataGrid({
       // dataSource: data.data,
        showColumnLines: true,
        showRowLines: true,
        showBorders: true,
     
        paging: {
            enabled: false
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
        height: 500,
        columnFixing: {
            enabled: true
        },


        columns: [{
            dataField: "StaffPermissionGroupName",
            caption: "ลำดับ",
            width: 50 + "%",
        }, {
            dataField: "StaffPermissionName",
            caption: "รายการสินค้า",
            width: 300 + "%",
            alignment: 'left',
            allowFiltering: false
        }, {
            dataField: "StaffPermissionGroupName",
            caption: "น้ำหนัก",
            width: 100 + "%",
        },
         {
             dataField: "StaffPermissionGroupName",
             caption: "ราคา",
             width: 100 + "%",
         },


        ],
    });

}