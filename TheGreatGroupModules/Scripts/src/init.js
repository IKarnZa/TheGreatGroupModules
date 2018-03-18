var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : sParameterName[1];
        }
    }
};


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
      "อื่นๆ",
];

function LoadForm_CustomerInfo(
    datasourceCustomer,
    dataProvince,
     dataDistrict,
    dataSubDistrict
 
    ) {


    $("#form").dxForm({
        formData: datasourceCustomer,
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
                    }, isRequired: true
                    ,
                        validationRules: [{
                            type: "required",
                            message: "ต้องการชื่อลูกค้า"
                        }],
                  
                }, {
                    dataField: "CustomerLastName",

                    label: {
                        text: "นามสกุล"
                    }
                    , isRequired: true,
                    validationRules: [{
                        type: "required",
                        message: "ต้องการนามสกุลลูกค้า"
                    }],
                },
                {
                    dataField: "CustomerNickName",
                    label: {
                        text: "ชื่อเล่น"
                    },
                    
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
                     validationRules: [{
                         type: "required",
                         message: "ต้องการเบอร์มือถือลูกค้า"
                     }],
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

}

function convertDate(inputFormat) {
  function pad(s) { return (s < 10) ? '0' + s : s; }
  var d = new Date(inputFormat);
  return [pad(d.getDate()), pad(d.getMonth()+1), d.getFullYear()].join('/');
}

function formatDate(date) {
    var monthNames = [
      "มกราคม", "กุมภาพันธ์", "มีนาคม",
      "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฏาคม",
      "สิงหาคม", "กันยายน", "ตุลาคม",
      "พฤศจิกายน", "ธันวาคม"
    ];

    var day = date.getDate();
    var monthIndex = date.getMonth();
    var year = date.getFullYear();

    return day + ' ' + monthNames[monthIndex] + ' ' + year;
}
