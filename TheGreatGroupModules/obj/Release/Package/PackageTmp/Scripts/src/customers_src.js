
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
                width: 240,
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
        
            columnFixing: {
                enabled: true
            },
            columns: [{
                dataField: "CustomerCode",
                caption: "รหัสลูกค้า",
                width: 100,
                allowFiltering: false
            }, {
                dataField: "CustomerName",
                caption: "ชื่อ-นามสกุล",
                width: 240,
            },
              {
                  dataField: "CustomerAddress1",
                  caption: "ที่อยู่",
                  width: 240,
                     
              },
              {
                  dataField: "CustomerSubDistrict",
                  caption: "ตำบล",
                  width: 100,
              },
            {
                dataField: "CustomerSubDistrict",
                caption: "อำเภอ",
                width: 100,
            },
             
                {
                    dataField: "CustomerProvince",
                    caption: "จังหวัด",
                    width: 160,

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
                  caption: "ทำสัญญา",
                  alignment: 'center',
                  allowFiltering: false,
                  cellTemplate: function (container, options) {
                      console.log(options.key);
                      $("<div>")
                          .append("<button type='button'  class='btn btn-info btn-circle btn-sm' ><i class='fa fa-list-alt'></i></button>")
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
                        onClick: function(e) {
                            DevExpress.ui.notify("Export PDF Successful!");
                        }
                    }
                })
                }
        });

    }
    
