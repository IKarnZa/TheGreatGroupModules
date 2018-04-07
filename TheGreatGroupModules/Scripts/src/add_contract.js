$(function () {

 

    $("#loadIndicator").dxLoadIndicator({
        visible: true
    });

    LoadDataGrid();

    var products = [];
    $("#product_amount").dxTextBox({
        placeholder: 'ใส่จำนวน',
        value: 1
    });

    var days = [{
        "ID": 1,
        "Name": "ทุกวัน"
    },
        { "ID": 2, "Name": "ยกเว้นวันเสาร์อาทิตย์" },
    ];
    var contract = {

    };
    var data = [{}];
    var listproducts = Array();
    var CustomerSuretyData1 = Object();
    var CustomerSuretyData2 = Object();
    var CustomerPartnerData = Object();
    var dataProvince = [];
    var dataDistrict = [];
    var dataSubDistrict = [];
    $.get("../Contract/GetContract?CustomerID=" + getUrlParameter('CustomerID')
       + "&ContractID=" + getUrlParameter('ContractID'))
        .done(function (result) {
            products = result.dataProduct;
            dataProvince = result.dataProvince;
            dataDistrict = result.dataDistrict;
            dataSubDistrict = result.dataSubDistrict;
           /* ================= ข้อมูลสัญญา =====================*/
           var ListContract = [];
           if (getUrlParameter('ContractID') != 0) {
               result.data.forEach(function (element) {
                 
                   element.ContractCustomerID = getUrlParameter('CustomerID');
                   element.ContractCreateDate = new Date(element.ContractCreateDate_Text);
                   element.ContractStartDate = new Date(element.ContractStartDate_Text);
                   element.ContractExpDate = new Date(element.ContractExpDate_Text);
                   element.ContractExpDate_Text = convertDate(element.ContractExpDate);
                   element.ContractCreateDate_Text = convertDate(element.ContractCreateDate);
                   element.ContractStartDate_Text = convertDate(element.ContractStartDate);
                   ListContract.push(element);

                   if (ListContract[0].CustomerSuretyData1!==null) {
                   
                       CustomerSuretyData1=  ListContract[0].CustomerSuretyData1;
                   }
                   if (ListContract[0].CustomerSuretyData2 !== null) {

                       CustomerSuretyData1= ListContract[0].CustomerSuretyData2;
                   }
                   if (ListContract[0].CustomerPartnerData !== null) {

                       CustomerPartnerData = ListContract[0].CustomerPartnerData;
                   }
                
               });

           } else { ListContract.push({}); }

           console.log(ListContract);

           $("#product_name").dxLookup({
               dataSource: products,
               displayExpr: 'ProductDetail',
               valueExpr: 'ProductID',
               title: 'เลือกสินค้า',
               placeholder: 'เลือกสินค้า',
               cancelButtonText: "ยกเลิก",
               seachPlaceholder: 'เลือกสินค้า',
           });

           $("#loadIndicator").dxLoadIndicator({
               visible: false
           });
           $("#form").dxForm({
               colCount: 3,
               formData: ListContract[0],
               items: [{
                   dataField: "ContractNumber",
                   label: {
                       text: "เลขที่สัญญา"
                   },
               }, {
                   dataField: "ContractRefNumber",
                   label: {
                       text: "เลขที่ใบกำกับภาษี"
                   },
               }, {
                   dataField: "ContractCreateDate",
                   editorType: "dxDateBox",
                   label: {
                       text: "วันที่ทำสัญญา"
                   },
                   editorOptions: {
                       width: "100%",
                       displayFormat: "dd/MM/yyyy"
                   },
               }, {
                   dataField: "ContractExpDate",
                   editorType: "dxDateBox",
                   label: {
                       text: "วันที่สิ้นสุดสัญญา"
                   },
                   editorOptions: {
                       width: "100%",
                       displayFormat: "dd/MM/yyyy",

                   },
               }, {
                   dataField: "ContractStartDate",
                   editorType: "dxDateBox",
                   label: {
                       text: "วันที่เริ่มจ่าย"
                   },
                   editorOptions: {
                       width: "100%",
                       displayFormat: "dd/MM/yyyy"
                   },
               },

               {
                   dataField: "ContractType",
                   label: {
                       text: "ประเภทลูกค้า"
                   },
                   editorType: "dxSelectBox",
                   editorOptions: {
                       items: ["P", "G"],
                       value: "P"

                   },
               },
               {
                   dataField: "ContractPeriod",
                   label: {
                       text: "จำนวนงวด/งวด"
                   },
               },
               {
                   dataField: "ContractAmount",
                   label: {
                       text: "งวดละ/บาท"
                   },
               },
               {
                   dataField: "ContractAmountLast",
                   label: {
                       text: "งวดสุดท้าย/บาท"
                   },
                   editorOptions: {
                       disabled: true
                   }
               }, {
                   dataField: "ContractInterest",
                   label: {
                       text: "อัตราดอกเบี้ย(%)"
                   },

               },
               {
                   dataField: "ContractPayEveryDay",
                   label: {
                       text: "วันที่ผ่อนชำระ"
                   },
                   editorType: "dxSelectBox",
                   editorOptions: {
                       items: days,
                       displayExpr: "Name",
                       valueExpr: "ID",
                   
                   },
               },
                {
                    dataField: "ContractSpecialholiday",
                    label: {
                        text: "ยกเว้นวันหยุดพิเศษ"
                    },
                    editorType: "dxCheckBox",
                   
                },
                {
                    dataField: "ContractReward",
                    label: {
                        text: "ค่ากำเหน็จ/บาท"
                    },

                },
               ]
           });
        
           // ==================== ข้อมูลผู้ค้ำประกัน =====================
           $("#form1").dxForm({
               colCount: 3,
               formData: CustomerSuretyData1,
               items: [
                   {
                       dataField: "CustomerSuretyTitle",
                       label: {
                           text: "คำนำหน้า"
                       },
                       editorType: "dxSelectBox",
                       editorOptions: {
                           items: title,
                       },
                   }, {
                       dataField: "CustomerSuretyFirstName",
                   label: {
                       text: "ชื่อผู้ค้ำประกันคนที่ 1"
                   },
               },

             
               {
                   dataField: "CustomerSuretyLastName",
                   label: {
                       text: "นามสกุล"
                   },
               },
               {
                   dataField: "CustomerSuretyAddress",
                   label: {
                       text: "ที่อยู่"
                   },

               },
               {
                   dataField: "CustomerSuretySubDistrictId",
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
                   dataField: "CustomerSuretyDistrictId",
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
                    dataField: "CustomerSuretyProvinceId",
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
                 dataField: "CustomerSuretyZipCode",
                 label: {
                     text: "รหัสไปรษณีย์"
                 },

             },
               {
                   dataField: "CustomerSuretyIdCard",
                   label: {
                       text: "เลขประจำตัวประชาชน"
                   },

               },
                {
                    dataField: "CustomerSuretyTelephone",
                    label: {
                        text: "เบอร์โทรศัพท์"
                    },

                },
                 {
                     dataField: "CustomerSuretyMobile",
                     label: {
                         text: "เบอร์มือถือ"
                     },

                 },
               ]
           });


           $("#form2").dxForm({
               colCount: 3,
               formData: CustomerSuretyData2,
               items: [
                   {
                       dataField: "CustomerSuretyTitle",
                       label: {
                           text: "คำนำหน้า"
                       },
                       editorType: "dxSelectBox",
                       editorOptions: {
                           items: title,
                       },
                   }, {
                       dataField: "CustomerSuretyFirstName",
                       label: {
                           text: "ชื่อผู้ค้ำประกันคนที่ 2"
                       },
                   },


               {
                   dataField: "CustomerSuretyLastName",
                   label: {
                       text: "นามสกุล"
                   },
               },
               {
                   dataField: "CustomerSuretyAddress",
                   label: {
                       text: "ที่อยู่"
                   },

               },
               {
                   dataField: "CustomerSuretySubDistrictId",
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
                   dataField: "CustomerSuretyDistrictId",
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
                    dataField: "CustomerSuretyProvinceId",
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
                 dataField: "CustomerSuretyZipCode",
                 label: {
                     text: "รหัสไปรษณีย์"
                 },

             },
               {
                   dataField: "CustomerSuretyIdCard",
                   label: {
                       text: "เลขประจำตัวประชาชน"
                   },

               },
                {
                    dataField: "CustomerSuretyTelephone",
                    label: {
                        text: "เบอร์โทรศัพท์"
                    },

                },
                 {
                     dataField: "CustomerSuretyMobile",
                     label: {
                         text: "เบอร์มือถือ"
                     },

                 },
               ]
           });

           $("#form4").dxForm({
               colCount: 3,
               formData: CustomerPartnerData,
               items: [
                   {
                       dataField: "CustomerPartnerTitle",
                       label: {
                           text: "คำนำหน้า"
                       },
                       editorType: "dxSelectBox",
                       editorOptions: {
                           items: title,
                       },
                   }, {
                       dataField: "CustomerPartnerFirstName",
                       label: {
                           text: "ชื่อผู้ซื้อร่วม"
                       },
                   },


               {
                   dataField: "CustomerPartnerLastName",
                   label: {
                       text: "นามสกุล"
                   },
               },
               {
                   dataField: "CustomerPartnerAddress",
                   label: {
                       text: "ที่อยู่"
                   },

               },
               {
                   dataField: "CustomerPartnerSubDistrictId",
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
                   dataField: "CustomerPartnerDistrictId",
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
                    dataField: "CustomerPartnerProvinceId",
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
                 dataField: "CustomerPartnerZipCode",
                 label: {
                     text: "รหัสไปรษณีย์"
                 },

             },
               {
                   dataField: "CustomerPartnerIdCard",
                   label: {
                       text: "เลขประจำตัวประชาชน"
                   },

               },
                {
                    dataField: "CustomerPartnerTelephone",
                    label: {
                        text: "เบอร์โทรศัพท์"
                    },

                },
                 {
                     dataField: "CustomerPartnerMobile",
                     label: {
                         text: "เบอร์มือถือ"
                     },

                 },
               ]
           });
           //   $("#form").dxForm("instance").validate();
           /* ===========================ข้อมูลลูกค้า  =============================*/
           var response = result.dataCustomers[0];
           if (result.success == true) {
               $("#customerName").html(' <h4> &nbsp;<b>ชื่อลูกค้า : </b>' + response.CustomerName + '</h4>');
               $("#customerAddress").html('  <p class="text-muted m-l-5">ที่อยู่ : ' + response.CustomerAddress1 + '</p>');
           }


           /* =========================== ข้อมูลสินค้า =============================*/

           var i = listproducts.length;
           var sum = 0;
           if (result.dataProductSelect.length > 0) {
            
               $('#gridData').dxDataGrid('instance').option('dataSource', result.dataProductSelect);

           }


           /* ================================================================*/

       });



    $("#myPopup").dxPopup({
        title: 'พิมพ์เอกสาร',
        visible: false,
    });

    function LoadDataGrid() {
        $("#gridData").dxDataGrid({
            //  dataSource: productsdatasource,
            showColumnLines: true,
            showRowLines: true,
            rowAlternationEnabled: true,
            showBorders: true,
            columns: [
                {
                    dataField: "No",
                    caption: "ลำดับที่",
                    width: 80,
                    alignment: 'center',
                    allowFiltering: false,
                    allowSorting: false
                },
                {
                    dataField: "ProductName",
                    caption: "รายการสินค้า",
                    width: 300,
                    alignment: 'left',
                    allowFiltering: false,
                    allowSorting:false
                }, {
                    dataField: "Unit_Text",
                    caption: "จำนวน(หน่วย)",
                    width: 160,
                    alignment: 'right',
                    allowSorting: false
                },
              {
                  dataField: "ProductPrice_Text",
                  caption: "ราคาต่อหน่วย",
                  width: 160,
                  alignment: 'right',
                  allowSorting: false

              },
             {
                 dataField: "TotalPrice_Text",
                 caption: "จำนวนเงิน",
                 alignment: 'right',
                 width: 160,
                 allowSorting: false
             },
            ],
            summary: {

            },
            onRowUpdated: function (e) {
                console.log(e);
            },
            onRowRemoving: function (e) {
                console.log(e);
            },
            onRowRemoved: function (e) {
                console.log(e)

            }
        });
    }
    var CustomerID = getUrlParameter('CustomerID');



});

function PrintCard() {

    var a = $("<a>").attr("href", "../Customers/ExportCard?ContractID=" + getUrlParameter('ContractID')
        + "&CustomerID=" + getUrlParameter('CustomerID')).attr("download", "img1.png").appendTo("body");
    a[0].click();
    a.remove();
    
}

function PrintContract() {
    alert("พิมพ์สัญญา");

    //http://localhost:31659/Report/ContractBookReport.aspx?staffID=1&date=12/03/2018
}

function Submit_Click() {

    var Contract = $("#form").dxForm("instance").option('formData');

    Contract.ContractID = getUrlParameter('ContractID');
    Contract.ContractCustomerID = getUrlParameter('CustomerID');
    Contract.CustomerPartnerData = $("#form4").dxForm("instance").option('formData');
    Contract.CustomerSuretyData1 = $("#form1").dxForm("instance").option('formData');
    Contract.CustomerSuretyData2 = $("#form2").dxForm("instance").option('formData');
    console.log(Contract);


    if (getUrlParameter('ContractID') == 0) {
        //Update

        $.ajax({
            url: '../Contract/PostAdd_NewContract',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(Contract),
            success: function (data) {
                if (data.success == true) {
                    window.location = "../Customers/Contract?CustomerID=" + getUrlParameter('CustomerID') +
               "&ContractID=" + data.ContractID;
                } else {
                    alert(data.data);
                }
            },
            error: function () {
                console.log("error");
            }
        });
     //  
    } else {
       
        $.ajax({
            url: '../Contract/PostEdit_Contract',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(Contract),
            success: function (data) {
                console.log('success');
                alert("Update Data Success !!!");
            },
            error: function () {
                console.log("error");
            }
        });
     
    }





}


function AddProduct() {
    if ($("#product_name").dxLookup("instance").option("value") != null) {

        var lookup = $("#product_name").data("dxLookup");
        var selectedValue = lookup.option("value");

        console.log($("#product_name").dxLookup("instance").option("value"), selectedValue);

    } else {
        DevExpress.ui.notify("โปรดเลือกสินค้า !!");

    }



}
