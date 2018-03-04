﻿$(function () {
    $("#loadIndicator").dxLoadIndicator({
        visible: true
    });
var employee = {

};

var dataProvince = [];
var dataDistrict = [];
var dataSubDistrict = [];

var customerListstatus = [
     "หย่าร้าง",
     "สมรส",
      "โสด",
];
var title = [
     "นาย",
      "นาง",
      "นางสาว",
];
var Career = [
    "รับราชการ",
     "รัฐวิสาหกิจ",
     "บริษัทเอกชน",
     "ธุรกิจส่วนตัว",
     "อาชีพอิสระ",
      "อื่นๆห",
];
var dataProvince = [];
var dataDistrict = [];
var dataSubDistrict = [];
$.get("../Setting/GetLocation")
.done(function (data) {
    if (data.success == true) {
      
        dataProvince = data.dataProvince;
        dataDistrict = data.dataDistrict;
        dataSubDistrict = data.dataSubDistrict;
       
        $("#form").dxForm({
            formData: employee,
            readOnly: false,
            showColonAfterLabel: true,
            showValidationSummary: true,
            validationGroup: "customerData",
            items: [
            {

                itemType: "group",
                colCount: 3,
                items: [{
                    itemType: "group",
                    caption: "ข้อมูลลูกค้า",
                    items: [{
                        dataField: "CustomerTitleName",
                        editorType: "dxSelectBox",
                        editorOptions: {
                            items: title
                        },
                        label: {
                            text: "คำนำหน้า"
                        },
                        isRequired: true
                    },
                    {
                        dataField: "CustomerFirstName",
                        label: {
                            text: "ชื่อ"
                        },
                        isRequired: true
                    }, {
                        dataField: "CustomerLastName",

                        label: {
                            text: "นามสกุล"
                        }
                        , isRequired: true
                    },
                    {
                        dataField: "CustomerNickName",
                        label: {
                            text: "ชื่อเล่น"
                        },
                        isRequired: true
                    }, {
                        dataField: "CustomerIdCard",
                        label: {
                            text: "รหัสประจำตัวประชาชน"
                        }, isRequired: true,

                        validationRules: [{
                            type: "stringLength",
                            max: 13,
                            min: 13,
                            message: "รหัสประจำตัวประชาชนต้องมี 13 หลัก"
                        }, ],
                    },
                    {
                        dataField: "CustomerTelephone",

                        label: {
                            text: "เบอร์โทรศัพท์"
                        },
                        isRequired: false
                    },
                     {
                         dataField: "CustomerMobile",
                         label: {
                             text: "เบอร์มือถือ"
                         },
                         isRequired: true,
                     }, {
                         dataField: "CustomerStatus",
                         editorOptions: {
                             items: customerListstatus
                         },
                         editorType: "dxLookup",
                         label: {
                             text: "สถานภาพ"
                         }
                        , isRequired: true
                     }, {
                         dataField: "CustomerEmail",
                         label: {
                             text: "อีเมลล์"
                         }
                     },
                    ]
                }, {
                    itemType: "group",
                    caption: "ที่อยู่ลูกค้า",
                    items: [
                        {
                            dataField: "CustomerAddress1",
                            label: {
                                text: "ที่อยู่ปัจจุบัน"
                            },
                            isRequired: true
                        },

                    {
                        dataField: "CustomerSubDistrictId",
                        label: {
                            text: "ตำบล/แขวง"
                        },
                        editorType: "dxLookup",
                        editorOptions: {
                            dataSource: dataSubDistrict,
                            valueExpr: 'SubDistrictID',
                            displayExpr: 'SubDistrictName'
                        },
                        isRequired: true
                    }, {
                        dataField: "CustomerDistrictId",
                        editorType: "dxLookup",
                        editorOptions: {
                            dataSource: dataDistrict,
                            valueExpr: 'DistrictID',
                            displayExpr: 'DistrictName'
                        },
                        label: {
                            text: "อำเภอ/เขต"
                        },
                        isRequired: true
                    },
                      {
                          dataField: "CustomerProvinceId",
                          editorType: "dxLookup",
                          editorOptions: {
                              dataSource: dataProvince,
                              valueExpr: 'ProvinceID',
                              displayExpr: 'ProvinceName'
                          },
                          label: {
                              text: "จังหวัด"
                          },
                          isRequired: true
                      }, {
                          dataField: "CustomerZipCode",
                          label: {
                              text: "รหัสไปรษณีย์"
                          }
                      }, ]
                },
                {
                    itemType: "group",
                    caption: "ข้อมูลที่ทำงาน",
                    items: [
                        {
                            dataField: "CustomerCareer",
                            label: {
                                text: "สถานภาพการทำงาน"
                            },
                            editorType: "dxSelectBox",
                            editorOptions: {
                                items: Career
                            },
                            isRequired: true
                        }, {
                            dataField: "CustomerJob",
                            label: {
                                text: "ลักษณะงาน"
                            },
                            isRequired: true
                        }, {
                            dataField: "CustomerSalary",
                            label: {
                                text: "รายได้"
                            },
                            isRequired: true
                        }, {
                            dataField: "CustomerJobYear",
                            label: {
                                text: "จำนวนปีที่ทำงาน"
                            },
                            isRequired: true
                        },
                        {
                            dataField: "CustomerJobAddress",
                            label: {
                                text: "สถานที่ทำงาน"
                            },
                            isRequired: false,
                        },

                       {
                           dataField: "CustomerJobSubDistrictId",
                           label: {
                               text: "ตำบล/แขวง"
                           },
                           editorType: "dxLookup",
                           editorOptions: {
                               dataSource: dataSubDistrict,
                               valueExpr: 'SubDistrictID',
                               displayExpr: 'SubDistrictName'
                           },
                           isRequired: false,
                       }, {
                           dataField: "CustomerJobDistrictId",
                           editorType: "dxLookup",
                           editorOptions: {
                               dataSource: dataDistrict,
                               valueExpr: 'DistrictID',
                               displayExpr: 'DistrictName'
                           },
                           label: {
                               text: "อำเภอ/เขต"
                           },
                           isRequired: false,
                       },
                      {
                          dataField: "CustomerJobProvinceId",
                          editorType: "dxLookup",
                          editorOptions: {
                              dataSource: dataProvince,
                              valueExpr: 'ProvinceID',
                              displayExpr: 'ProvinceName'
                          },
                          label: {
                              text: "จังหวัด"
                          },
                          isRequired: false,
                      },
                      {
                          dataField: "CustomerJobZipCode",
                          label: {
                              text: "รหัสไปรษณีย์"
                          }
                      }, ]
                }, ]
            },
             {

                 itemType: "group",
                 colCount: 3,
                 items: [{
                     itemType: "group",
                     caption: "ข้อมูลคู่สมรส",
                     items: [{
                         dataField: "CustomerSpouseTitle",
                         editorType: "dxSelectBox",
                         editorOptions: {
                             items: title
                         },
                            
                         label: {
                             text: "คำนำหน้า"
                         },
                         isRequired: false
                     },
                    {
                        dataField: "CustomerSpouseFirstName",
                        label: {
                            text: "ชื่อ"
                        },
                        isRequired: false
                    }, {
                        dataField: "CustomerSpouseLastName",

                        label: {
                            text: "นามสกุล"
                        }
                        , isRequired: false
                    },
                    {
                        dataField: "CustomerSpouseNickName",
                        label: {
                            text: "ชื่อเล่น"
                        },
                        isRequired: false
                    }, {
                        dataField: "CustomerSpouseTelephone",
                        label: {
                            text: "เบอร์โทรศัพท์"
                        },

                    },
                     {
                         dataField: "CustomerSpouseMobile",
                         label: {
                             text: "เบอร์มือถือ"
                         },
                         isRequired: false

                     }, {
                         dataField: "CustomerSpouseAddress",
                         label: {
                             text: "ที่อยู่"
                         }
                     },
                       {
                           dataField: "CustomerSpouseSubDistrictId",
                           label: {
                               text: "ตำบล/แขวง"
                           },
                           editorType: "dxLookup",
                           editorOptions: {
                               dataSource: dataSubDistrict,
                               valueExpr: 'SubDistrictID',
                               displayExpr: 'SubDistrictName'
                           },
                           isRequired: false,
                       }, {
                           dataField: "CustomerSpouseDistrictId",
                           editorType: "dxLookup",
                           editorOptions: {
                               dataSource: dataDistrict,
                               valueExpr: 'DistrictID',
                               displayExpr: 'DistrictName'
                           },
                           label: {
                               text: "อำเภอ/เขต"
                           },
                           isRequired: false,
                       },
                      {
                          dataField: "CustomerSpouseProvinceId",
                          editorType: "dxLookup",
                          editorOptions: {
                              dataSource: dataProvince,
                              valueExpr: 'ProvinceID',
                              displayExpr: 'ProvinceName'
                          },
                          label: {
                              text: "จังหวัด"
                          },
                          isRequired: false,
                      },
                    {
                         dataField: "CustomerSpouseZipCode",
                         label: {
                             text: "รหัสไปรษณีย์"
                         }
                     }, ]
                 },
                        {
                            itemType: "group",
                            caption: "บุคคลที่ติดต่อได้ในกรณีฉุกเฉิน",
                            items: [{
                                dataField: "CustomerEmergencyTitle",
                                editorType: "dxSelectBox",
                                editorOptions: {
                                    items: title
                                },
                                label: {
                                    text: "คำนำหน้า"
                                }
                            }, {
                                dataField: "CustomerEmergencyFirstName",
                                label: {
                                    text: "ชื่อ"
                                }
                            }, {
                                dataField: "CustomerEmergencyLastName",
                                label: {
                                    text: "นามสกุล"
                                }
                            }, {
                                dataField: "CustomerEmergencyRelation",
                                label: {
                                    text: "ความสัมพันธ์"
                                }
                            }, {
                                dataField: "CustomerEmergencyTelephone",
                                isRequired: false,
                                label: {
                                    text: "เบอร์โทรศัพท์"
                                }
                            },
                       {
                           dataField: "CustomerEmergencyMobile",
                           label: {
                               text: "เบอร์มือถือ"
                           },
                           isRequired: false

                       }, ]
                        },

                 ]
             },




            ]
        });


        $("#loadIndicator").dxLoadIndicator({
            visible: false
        });
    }

  
});
$("#button").dxButton({
    text: "เพิ่มข้อมูลลูกค้า",
    type: "success",
    useSubmitBehavior: true,
    validationGroup: "customerData",
    onInitialized: function (e) {


    }
});


$("#form").on("submit", function (e) {
    console.log(e);
    DevExpress.ui.notify({
        message: "You have submitted the form",
        position: {
            my: "center top",
            at: "center top"
        }
    }, "success", 3000);

    e.preventDefault();
});
});