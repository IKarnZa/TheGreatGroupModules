function Load_DataGrid(data) {


    $("#gridListStaffs").dxDataGrid({
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
        columns: [
            {
                dataField: "StaffID",
                caption: "ลำดับ",
                width: 50,
                alignment: 'center',
                allowFiltering: false,
                fixed: false,
                fixedPosition: 'left',
            },
            {
                dataField: "StaffCode",
                caption: "รหัสพนักงาน",
                alignment: 'center',
                width: 100,
                fixed: false,
                fixedPosition: 'left',
            },
            {
                dataField: "StaffFirstName",
                caption: "ชื่อ",
                alignment: 'left',


            },
            {
                dataField: "StaffLastName",
                caption: "นามสกุล",
                alignment: 'left',


            },
            {
                dataField: "StaffRoleName",
                caption: "ตำแหน่ง",
                alignment: 'left',


            },
            {
                dataField: "StaffID",
                caption: "แก้ไข",
                alignment: 'center',
                width: 60,
                fixed: true,
                fixedPosition: 'right',
                cellTemplate: function (container, options) {

                    $("<div>")
                        .append("<a href='\ListContract?CustomerID=" + options.key.StaffID + "'  title='แก้ไขพนักงาน'  class='btn btn-info btn-circle btn-sm' ><i class='fa fa-pencil'></i></a>")
                        .appendTo(container);
                }
            },
            {
                dataField: "ID",
                caption: "ลบ",
                alignment: 'center',
                width: 60,
                fixed: true,
                fixedPosition: 'right',
                cellTemplate: function (container, options) {

                    $("<div>")
                        .append("<a href='\ListContract?CustomerID=" + options.key.StaffID + "'  title='ลบพนักงาน'  class='btn btn-info btn-circle btn-sm' ><i class='fa fa-trash'></i></a>")
                        .appendTo(container);
                }
            },

        ],

    });

}
function NewZone() {
    alert("Success");
}
