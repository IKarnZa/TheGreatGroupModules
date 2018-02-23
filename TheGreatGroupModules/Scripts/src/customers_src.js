
var data = [];

$("#gridshow").hide();
  
  

    function SearchCustomer() {
 
        $("#loadIndicator").dxLoadIndicator({
            visible: true
        });

        $.post("../Customers/GetCustomers", {

            CustomerFirstName: $("#CustomerFirstName").val(),
            CustomerLastName: $("#CustomerLastName").val(),
            CustomerMobile: $("#CustomerMobile").val(),
            CustomerIdCard: $("#CustomerIdCard").val()

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

        $("#gridContainer").dxDataGrid({
            dataSource: data,
            showColumnLines: true,
            showRowLines: true,
            rowAlternationEnabled: true,
            showBorders: true,
        
       
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
            height:500,
            columnFixing: {
                enabled: true
            },
            columns: [{
                dataField: "CustomerCode",
                caption: "รหัสลูกค้า",
                width: 140,
                allowFiltering: false
            }, {
                dataField: "CustomerName",
                caption: "ชื่อ-นามสกุล",
                width: 240,
            },
              {
                  dataField: "CustomerAddress1",
                  caption: "ที่อยู่",
                  width: 200,
                     
              },
              {
                  dataField: "CustomerSubDistrict",
                  caption: "ตำบล",
                  width: 150,
              },
            {
                dataField: "CustomerDistrict",
                caption: "อำเภอ",
                width: 150,
            },
             
                {
                    dataField: "CustomerProvince",
                    caption: "จังหวัด",
                    width: 150,

                },
            {
                dataField: "CustomerZipCode",
                caption: "รหัสไปรษณีย์",
                width: 100,
            },
             {
                 dataField: "CustomerMobile",
                 caption: "เบอร์ติดต่อ",
                 width: 160,
             },
              {
                  dataField: "CustomerEmail",
                  caption: "อีเมลล์",
                  width: 180,
              },
              {
                  dataField: "CustomerID",
                  caption: "แก้ไขข้อมูลลูกค้า",
                  alignment: 'center',
                  allowFiltering: false,
                  cellTemplate: function (container, options) {
                      console.log(options.key);
                      $("<div>")
                          .append("<a href='\PurchaseOrder'  class='btn btn-primary btn-circle btn-sm' ><i class='fa fa-pencil'></i></a>")
                          .appendTo(container);
                  }

              },
              {
                  dataField: "CustomerID",
                  caption: "ซื้อสินค้า",
                  alignment: 'center',
                  allowFiltering: false,
                  cellTemplate: function (container, options) {
                      console.log(options.key);
                      $("<div>")
                          .append("<a href='\PurchaseOrder'  class='btn btn-info btn-circle btn-sm' ><i class='fa fa-shopping-cart'></i></a>")
                          .appendTo(container);
                  }

              }
              ,
              {
                  dataField: "CustomerID",
                  caption: "ประวัติการซื้อ",
                  alignment: 'center',
                  allowFiltering: false,
                  cellTemplate: function (container, options) {
                      console.log(options.key);
                      $("<div>")
                          .append("<a href='\CustomerProduct'  class='btn btn-warning btn-circle btn-sm' ><i class='fa fa-user'></i></a>")
                          .appendTo(container);
                  }

              }
            ],
            onToolbarPreparing: function(e) {
                e.toolbarOptions.items.push( {
                    location: "before",
                    widget: "dxButton",
                    options: {
                        icon: "export",
                        //text: "",
                        onInitialized: function(e) {
                            clearFilterButton = e.component;
                        },
                        onClick: function (e) {
                            var params = {

                                CustomerFirstName: $("#CustomerFirstName").val(),
                                CustomerLastName: $("#CustomerLastName").val(),
                                CustomerMobile: $("#CustomerMobile").val(),
                                CustomerIdCard: $("#CustomerIdCard").val()

                            };
                            var req = new XMLHttpRequest();
                            req.open("POST", "../Customers/ExportPDF", true);
                            req.responseType = "blob";
                            req.send(params);
                            req.onload = function (event) {
                                var blob = req.response;
                                console.log(blob.size);
                                var link = document.createElement('a');
                                link.href = window.URL.createObjectURL(blob);
                                link.download = "บัตรสมาชิก.pdf";
                                link.click();
                            };

                            //req.send();

                            //var http = new XMLHttpRequest();
                            //var url = "../Customers/ExportPDF";
                         
                            //http.open("POST", url, true);

                            ////Send the proper header information along with the request
                            //http.setRequestHeader("Content-type", "application/x-www-form-urlencoded");

                            //http.onreadystatechange = function () {//Call a function when the state changes.
                            //    if (http.readyState == 4 && http.status == 200) {
                            //        alert(http.responseText);
                            //    }
                            //}
                            //http.send(params);
                            //$.get("../Customers/ExportPDF")
                            //.done(function (data) {

                            //        DevExpress.ui.notify("Export PDF Successful!");
                                
                            //});
                          
                        }
                    }
                })
                }
        });

    }

    function AddCustomer() {

        var data = {
            CustomerTitleName: $('#CustomerTitleName').val(),
            CustomerFirstName: $('#CustomerFirstName').val(),
            CustomerLastName: $('#CustomerLastName').val(),
            CustomerNickName: $('#CustomerNickName').val(),
            CustomerIdCard: $('#CustomerIdCard').val(),
            CustomerAddress1: $('#CustomerAddress1').val(),
            CustomerAddress2: $('#CustomerAddress2').val(),
            CustomerProvinceId: $('#CustomerProvince').val(),
            CustomerDistrictId: $('#CustomerDistrict').val(),
            CustomerSubDistrictId: $('#CustomerSubDistrict').val(),
            CustomerZipCode: $('#CustomerZipCode').val(),
            CustomerMobile: $('#CustomerMobile').val(),
            CustomerEmail: $('#CustomerEmail').val(),
        }

        $("#loadIndicator").dxLoadIndicator({
            visible: true
        });

        $.post("../Customers/AddCustomers", data)
    .done(function (data) {
       
        if (data.success == true) {

            DevExpress.ui.notify(" เพิ่มลูกค้าเรียบร้อยแล้ว ");
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
