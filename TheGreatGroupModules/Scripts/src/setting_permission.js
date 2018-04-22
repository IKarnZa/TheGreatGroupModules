
$(function () {
    var data = [];
    Call_Grid();
});


function Call_Grid() {

    $.ajax({
        url: '../Staffs/GetStaffPermission?staffroleID=' + getUrlParameter('staffID'),
        type: 'GET',
        contentType: 'application/json',
        success: function (data) {

            if (data.success) {
                Load_DataGrid(data.data);
                $("#lblStaffRoleName").html("<h4> กลุ่มพนักงาน : "+data.dataStaffRole[0].StaffRoleName+"</h4>");
                console.log(data.dataStaffRole[0].StaffRoleName);
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



$("#myButton").dxButton({
    text: 'บันทึกข้อมูล',
    type: 'success',
    useSubmitBehavior: true,
    onClick: function () {
       
            //$.ajax({
            //    url: '../Staffs/AddStaffs',
            //    type: 'POST',
            //    contentType: 'application/json',
            //    data: JSON.stringify(staff),
            //    success: function (data) {
            //        if (data.success == true) {

            //            DevExpress.ui.notify({
            //                message: "บันทึกข้อมูลพนักงานสำเร็จ !!!",
            //            }, "success", 3000);

            //            window.location = "\ListStaff";
            //        } else {

            //            DevExpress.ui.notify(data.data);
            //        }
            //    },
            //    error: function () {
            //        console.log("error");
            //    }
            //});
        
    }
});