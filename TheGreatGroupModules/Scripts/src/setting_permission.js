
$(function () {
    var data = [];
    Call_Grid();
});


function Call_Grid() {

    $.ajax({
        url: '../Staffs/GetStaffPermission',
        type: 'GET',
        contentType: 'application/json',
        success: function (data) {

            if (data.success) {
                Load_DataGrid(data.data);
            } else {
                DevExpress.ui.notify(data.data);
                
            }

        },
        error: function () {
            console.log("error");
        }
    });
}

function Load_DataGrid(data) {

    $("#gridContainer").dxDataGrid({
        dataSource: data,
        showColumnLines: true,
        showRowLines: false,
        rowAlternationEnabled: false,
        showBorders: true,

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
        selection: {
            mode: "multiple"
        },
        allowColumnReordering: true,
        allowColumnResizing: true,
        columnAutoWidth: true,
        height: 550,
        columnFixing: {
            enabled: true
        },
        columns: [{
            dataField: "StaffPermissionName",
            caption: "สิทธิ์พนักงาน",
            width: 300 +"%",
            alignment: 'left',
            allowFiltering: false
        }, {
            dataField: "StaffPermissionGroupName",
            caption: "กลุ่ม",
            groupIndex: 0,
            width: 100 + "%",
        },
          
            

        ],
    });

}