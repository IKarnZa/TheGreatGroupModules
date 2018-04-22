
$(function () {
    //LoadForm();
});

function NewStaff() {
    window.location = "\AddStaff";
}

var
staffs,
dataProvince,
dataDistrict ,
dataSubDistrict ,
dataStaffRole ;


if (getUrlParameter('staffID') !=0) {

    $.get("../Staffs/GetListStaffs?staffID=" + getUrlParameter('staffID'))
    .done(function (result) {

        if (result.success) {

          
           
            dataProvince = result.dataProvince;
            dataDistrict = result.dataDistrict;
            dataSubDistrict = result.dataSubDistrict;
            dataStaffRole = result.dataStaffRole;

            if (getUrlParameter('staffID') != 0) {
                staffs = result.data[0];
                console.log(staffs, result.data[0]);
                LoadForm(staffs, dataProvince, dataDistrict, dataSubDistrict, dataStaffRole);

            } else { staffs.push({}); }

        } else {
            alertError(result.data);

        }

    });
} else {


    $.get("../Staffs/GetListStaffs?staffID=0")
      .done(function (result) {

          if (result.success) {

              
              dataProvince = result.dataProvince;
              dataDistrict = result.dataDistrict;
              dataSubDistrict = result.dataSubDistrict;
              dataStaffRole = result.dataStaffRole;

              LoadForm(staffs, dataProvince, dataDistrict, dataSubDistrict, dataStaffRole);

          } else {
              alertError(result.data);

          }

      });
}
function LoadForm(staffs, dataProvince, dataDistrict, dataSubDistrict,dataStaffRole) {

 
         
    console.log(staffs)
         
    // ==================== ข้อมูลพนักงาน =====================
    $("#form").dxForm({
        colCount: 3,
        formData: staffs,
        showColonAfterLabel: true,
        showValidationSummary: true,
        items: [
            {
                dataField: "StaffTitleName",
                label: {
                    text: "คำนำหน้า"
                },
                editorType: "dxSelectBox",
                editorOptions: {
                    items: title,
                },
                isRequired: true
                    , validationRules: [{
                        type: "required",
                        message: "ต้องการคำนำหน้า"
                    }],
            }, {
                dataField: "StaffFirstName",
                label: {
                    text: "ชื่อ"
                },
                isRequired: true
                    , validationRules: [{
                        type: "required",
                        message: "ต้องการชื่อ"
                    }],
            },


        {
            dataField: "StaffLastName",
            label: {
                text: "นามสกุล"
            },
            isRequired: true
                    , validationRules: [{
                        type: "required",
                        message: "ต้องการนามสกุล"
                    }],
        },
          {
              dataField: "StaffRoleID",
              label: {
                  text: "สิทธิ์พนังงาน"
              },
              editorType: "dxLookup",
              editorOptions: {
                  dataSource: dataStaffRole,
                  valueExpr: 'StaffRoleID',
                  displayExpr: 'StaffRoleName'
              },
              isRequired: true
                    , validationRules: [{
                        type: "required",
                        message: "ต้องการสิทธิ์พนังงาน"
                    }],
          },
        {
            dataField: "StaffCode",
            label: {
                text: "ชื่อเข้าใช้งาน"
            },
            isRequired: true
                    , validationRules: [{
                        type: "required",
                        message: "ต้องการชื่อเข้าใช้งาน"
                    }],
        },


        {
            dataField: "StaffPassword",
            label: {
                text: "รหัสผ่าน"
            },
            editorOptions: {
                mode: "password"
            },
            isRequired: true
                    , validationRules: [{
                        type: "required",
                        message: "ต้องการรหัสผ่าน"
                    }],
        },

      
        {
            dataField: "StaffAddress1",
            label: {
                text: "ที่อยู่"
            },

        },
        {
            dataField: "StaffSubDistrictId",
            label: {
                text: "ตำบล"
            },
             editorType: "dxLookup",
            editorOptions: {
                dataSource: dataSubDistrict,
                valueExpr: 'SubDistrictID',
                displayExpr: 'SubDistrictName'
            },
        }, {
            dataField: "StaffDistrictId",
            label: {
                text: "อำเภอ"
            },
            editorType: "dxLookup",
            editorOptions: {
                dataSource: dataDistrict,
                valueExpr: 'DistrictID',
                displayExpr: 'DistrictName'
            },
        },
         {
             dataField: "StaffProvinceId",
             label: {
                 text: "จังหวัด"
             },
             editorType: "dxLookup",
             editorOptions: {
                 dataSource: dataProvince,
                 valueExpr: 'ProvinceID',
                 displayExpr: 'ProvinceName'
             },
         },
      {
          dataField: "StaffZipCode",
          label: {
              text: "รหัสไปรษณีย์"
          },

      },

         {
             dataField: "StaffTelephone",
             label: {
                 text: "เบอร์โทรศัพท์"
             },

         },
          {
              dataField: "StaffMobile",
              label: {
                  text: "เบอร์มือถือ"
              },
              isRequired: true
                    , validationRules: [{
                        type: "required",
                        message: "ต้องการเบอร์มือถือ"
                    }],
          },
             {
                 dataField: "StaffEmail",
                 label: {
                     text: "อีเมลล์"
                 },
               
             },
        ]
    });


}



if (getUrlParameter('staffID') != 0) {


    $("#myButton").dxButton({
        text: 'แก้ไขข้อมูลพนักงาน',
        type: 'success',
        useSubmitBehavior: true,
        onClick: function () {
            var staff = $("#form").dxForm("instance").option('formData');
            var result = $("#form").dxForm("instance").validate();
            if (result.isValid) {

                $.ajax({
                    url: '../Staffs/EditStaffs',
                    type: 'POST',
                    contentType: 'application/json',
                    data: JSON.stringify(staff),
                    success: function (data) {
                        if (data.success == true) {

                            DevExpress.ui.notify({
                                message: "บันทึกข้อมูลพนักงานสำเร็จ !!!",
                            }, "success", 3000);

                            window.location = "\ListStaff";
                        } else {

                            DevExpress.ui.notify(data.data);
                        }
                    },
                    error: function () {
                        console.log("error");
                    }
                });
            }
        }
    });

}
else {


    $("#myButton").dxButton({
        text: 'เพิ่มพนักงาน',
        type: 'success',
        useSubmitBehavior: true,
        onClick: function () {
            var staff = $("#form").dxForm("instance").option('formData');
            var result = $("#form").dxForm("instance").validate();
            if (result.isValid) {

                $.ajax({
                    url: '../Staffs/AddStaffs',
                    type: 'POST',
                    contentType: 'application/json',
                    data: JSON.stringify(staff),
                    success: function (data) {
                        if (data.success == true) {

                            DevExpress.ui.notify({
                                message: "บันทึกข้อมูลพนักงานสำเร็จ !!!",
                            }, "success", 3000);

                            window.location = "\ListStaff";
                        } else {

                            DevExpress.ui.notify(data.data);
                        }
                    },
                    error: function () {
                        console.log("error");
                    }
                });
            }
        }
    });

}


