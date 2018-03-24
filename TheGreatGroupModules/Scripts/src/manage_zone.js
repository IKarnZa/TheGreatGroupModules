function Load_DataGrid(data) {


    $("#gridListZone").dxDataGrid({
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
                dataField: "ID",
                caption: "No.",
                width: 50,
                alignment: 'center',
                allowFiltering: false,
                fixed: false,
                fixedPosition: 'left',
            },
            {
                dataField: "Code",
                caption: "รหัสพื้นที่",
                alignment: 'center',
                width: 100,
                fixed: false,
                fixedPosition: 'left',
            },
            {
                dataField: "Value",
                caption: "ชื่อพื้นที่",
                alignment: 'left',
                

            },
            {
                dataField: "ID",
                caption: "กำหนดพนักงาน",
                alignment: 'center',
                width: 100,
                fixed: true,
                fixedPosition: 'right',
                verticalAlignment: 'middle',
                cellTemplate: function (container, options) {

                    $("<div>")
                        .append("<a href='\ListContract?CustomerID=" + options.key.ID + "'  title='กำหนดพนักงาน'  class='btn btn-info btn-circle btn-sm' ><i class='fa fa-user' aria-hidden='true'></i></a>")
                        .appendTo(container);
                }
            },
            {
                dataField: "ID",
                caption: "แก้ไข",
                alignment: 'center',
                width: 60,
                fixed: true,
                fixedPosition: 'right',
                cellTemplate: function (container, options) {

                    $("<div>")
                        .append("<a href='\ListContract?CustomerID=" + options.key.ID + "'  title='แก้ไขพื้นที่'  class='btn btn-info btn-circle btn-sm' ><i class='fa fa-pencil'></i></a>")
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
                        .append("<a href='\ListContract?CustomerID=" + options.key.ID + "'  title='ลบพื้นที่'  class='btn btn-info btn-circle btn-sm' ><i class='fa fa-trash'></i></a>")
                        .appendTo(container);
                }
            },

        ],

    });

}
function NewZone() {
    alert("Success");
}
