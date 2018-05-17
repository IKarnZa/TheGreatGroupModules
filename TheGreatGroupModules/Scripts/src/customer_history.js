
$(function () {

    $("#gridshow").hide();
  


    $.ajax({
        url: '../Customers/GetListCustomers/0',
        type: 'GET',
        contentType: 'application/json',
        // data: JSON.stringify(DataSearch),
        success: function (data) {



            if (data.success == true) {
                console.log(data.data);
                LoadFormSearch(data.data);
           
                $("#loadIndicator").dxLoadIndicator({
                    visible: false
                });

            } else {

                swal("ผิดพลาด!!", data.data, "error");
                $("#loadIndicator").dxLoadIndicator({
                    visible: false
                });
            }


        },
        error: function () {
            console.log("error");

        }
    });

});
var formdata = {
    TypeDate: 1,
    FromDate: new Date(),
    ToDate: new Date(),
    Month: new Date().getMonth(),
    Year: new Date().getFullYear(),
    CustomerID: 1,
};
var TypeDate = [
            {
                ID: 1,
                Name: "เลือก วัน เดือน ปี"

            },
            {
                ID: 2,
                Name: "เลือก เดือน ปี"
            },
            {
                ID: 3,
                Name: "เลือก ปี "
            },
            {
                ID: 4,
                Name: "เลือก ช่วงวันที่ "
            }
];



function LoadFormSearch(lscustomers) {

    $("#gridshow").show();
    var formInstance = $("#form").dxForm({
        colCount: 2,
        formData: formdata,
        showColonAfterLabel: true,
        showValidationSummary: false,
        width: 60 + "%",
        items: [
              
             {
                 dataField: "TypeDate",
                 label: {
                     text: "เงื่อนไขวันที่"
                 },
                 editorType: "dxSelectBox",
                 editorOptions: {
                     items: TypeDate,
                     displayExpr: "Name",
                     valueExpr: "ID",
                     onValueChanged: function (e) {

                         if (e.value == 1) {
                             formInstance.itemOption('FromDate', 'visible', true);
                             formInstance.itemOption('ToDate', 'visible', false);
                             formInstance.itemOption('Month', 'visible', false);
                             formInstance.itemOption('Year', 'visible', false);
                         } else if (e.value == 2) {
                             formInstance.itemOption('FromDate', 'visible', false);
                             formInstance.itemOption('ToDate', 'visible', false);
                             formInstance.itemOption('Month', 'visible', true);
                             formInstance.itemOption('Year', 'visible', true);
                         }
                         else if (e.value == 3) {
                             formInstance.itemOption('FromDate', 'visible', false);
                             formInstance.itemOption('ToDate', 'visible', false);
                             formInstance.itemOption('Month', 'visible', false);
                             formInstance.itemOption('Year', 'visible', true);
                         }
                         else if (e.value == 4) {
                             formInstance.itemOption('FromDate', 'visible', true);
                             formInstance.itemOption('ToDate', 'visible', true);
                             formInstance.itemOption('Month', 'visible', false);
                             formInstance.itemOption('Year', 'visible', false);
                         }
                         //alert(e.value)
                     }
                 },
             },
              {
                  dataField: "ContractID",
                  label: {
                      text: "เลือกชื่อลูกค้า "
                  },
                  visible: true,
                  editorType: "dxLookup",
                  editorOptions: {
                      items: lscustomers,
                      displayExpr: "CustomerName",
                      valueExpr: "ContractID",
                      
                  },
              },
            {
                dataField: "FromDate",
                editorType: "dxDateBox",
                label: {
                    text: "วันที่"
                },
                editorOptions: {
                    width: "100%",
                    displayFormat: "dd/MM/yyyy"
                },

            },
            {
                dataField: "ToDate",
                editorType: "dxDateBox",
                label: {
                    text: "ถึงวันที่"
                },
                visible: false,
                editorOptions: {
                    width: "100%",
                    displayFormat: "dd/MM/yyyy",

                },
            },

        {
            dataField: "Month",
            label: {
                text: "เลือกเดือน "
            },
            visible: false,
            editorType: "dxSelectBox",
            editorOptions: {
                items: Months,
                displayExpr: "Name",
                valueExpr: "ID",

            },
        },

        {
            dataField: "Year",
            label: {
                text: "เลือกปี "
            },
            visible: false,
            editorType: "dxSelectBox",
            editorOptions: {
                items: Years,


            },
        },
        ]
    }).dxForm("instance");



}


function SearchData() {
    $("#gridshow").show();
    $("#loadIndicator").dxLoadIndicator({
        visible: true
    });
    var item = $("#form").dxForm("instance").option('formData');
  
    if (item.ContractID > 0) {
        $.ajax({
            url: '../Report/GetPaymentReportByCustomer',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(item),
            success: function (data) {

                if (data.success == true) {

                    Load_DataGrid(data.data);
                    $("#loadIndicator").dxLoadIndicator({
                        visible: false
                    });

                } else {

                    swal("ผิดพลาด!!", data.data, "error");
                    $("#loadIndicator").dxLoadIndicator({
                        visible: false
                    });
                }


            },
            error: function () {
                console.log("error");

            }
        });

    } else {


        swal("ผิดพลาด!!", "กรุณาเลือกชื่อลูกค้า", "error");

    }
   
}


function Load_DataGrid(data) {

    $("#gridContainer").dxDataGrid({
        dataSource: data,
        showColumnLines: true,
        showRowLines: true,
        rowAlternationEnabled: true,
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
            fileName: "รายงานส่วนลด",
        },
        paging: {
            enabled: false,
        },
        pager: {
            enabled: false,
        },
        allowColumnReordering: true,
        allowColumnResizing: true,
        columnAutoWidth: true,
        height: 530,
        columnFixing: {
            enabled: true
        },
        columns: [

            {
                dataField: "Day",
                caption: "วันที่",
                alignment: 'center',

            },
            {
                dataField: "Month1_Str",
                caption: "มกราคม",
                alignment: 'center',
                cellTemplate: function (container, options) {
                    if (options.key.Month1 > 0) {

                        $("<div>")
                     .append("<span class='badge badge-success'> " + options.key.Month1_Str + " </span>")
                     .appendTo(container);
                    } else {


                        $("<div>")
                     .append(options.key.Month1_Str)
                     .appendTo(container);
                    }

                }
            },
                 {
                     dataField: "Month2_Str",
                     caption: "กุมภาพันธ์",
                     alignment: 'right',
                     cellTemplate: function (container, options) {
                         if (options.key.Month2 > 0) {

                             $("<div>")
                          .append("<span class='badge badge-success'> " + options.key.Month2_Str + " </span>")
                          .appendTo(container);
                         } else {


                             $("<div>")
                          .append(options.key.Month2_Str)
                          .appendTo(container);
                         }

                     }
                 },
                 {
                     dataField: "Month3_Str",
                     caption: "มีนาคม",
                     alignment: 'right',
                     cellTemplate: function (container, options) {
                         if (options.key.Month3 > 0) {

                             $("<div>")
                          .append("<span class='badge badge-success'> " + options.key.Month3_Str + " </span>")
                          .appendTo(container);
                         } else {


                             $("<div>")
                          .append(options.key.Month3_Str)
                          .appendTo(container);
                         }

                     }
                 },
                  {
                      dataField: "Month4_Str",
                      caption: "เมษายน",
                      alignment: 'right',
                      cellTemplate: function (container, options) {
                          if (options.key.Month4 > 0) {

                              $("<div>")
                           .append("<span class='badge badge-success'> " + options.key.Month4_Str + " </span>")
                           .appendTo(container);
                          } else {


                              $("<div>")
                           .append(options.key.Month4_Str)
                           .appendTo(container);
                          }

                      }
                  },

                  {
                          dataField: "Month5_Str",
                          caption: "พฤษภาคม",
                          alignment: 'center',
                          cellTemplate: function (container, options) {
                              if (options.key.Month5 > 0) {

                                  $("<div>")
                               .append("<span class='badge badge-success'> " + options.key.Month5_Str + " </span>")
                               .appendTo(container);
                              } else {


                                  $("<div>")
                               .append( options.key.Month5_Str)
                               .appendTo(container);
                              }

                          }
                  },
                  {
                      dataField: "Month6_Str",
                      caption: "มิถุนายน",
                      alignment: 'center',
                      cellTemplate: function (container, options) {
                          if (options.key.Month6> 0) {

                              $("<div>")
                           .append("<span class='badge badge-success'> " + options.key.Month6_Str + " </span>")
                           .appendTo(container);
                          } else {


                              $("<div>")
                           .append(options.key.Month6_Str)
                           .appendTo(container);
                          }

                      }
                  },

                       {
                           dataField: "Month7_Str",
                           caption: "กรกฎาคม",
                           alignment: 'center',
                           cellTemplate: function (container, options) {
                               if (options.key.Month7 > 0) {

                                   $("<div>")
                                .append("<span class='badge badge-success'> " + options.key.Month7_Str + " </span>")
                                .appendTo(container);
                               } else {


                                   $("<div>")
                                .append(options.key.Month7_Str)
                                .appendTo(container);
                               }

                           }
                       },

                            {
                                dataField: "Month8_Str",
                                caption: "สิงหาคม",
                                alignment: 'center',
                                cellTemplate: function (container, options) {
                                    if (options.key.Month8 > 0) {

                                        $("<div>")
                                     .append("<span class='badge badge-success'> " + options.key.Month8_Str + " </span>")
                                     .appendTo(container);
                                    } else {


                                        $("<div>")
                                     .append(options.key.Month8_Str)
                                     .appendTo(container);
                                    }

                                }
                            },

                       {
                           dataField: "Month9_Str",
                           caption: "กันยายน",
                           alignment: 'center',
                           cellTemplate: function (container, options) {
                               if (options.key.Month9 > 0) {

                                   $("<div>")
                                .append("<span class='badge badge-success'> " + options.key.Month9_Str + " </span>")
                                .appendTo(container);
                               } else {


                                   $("<div>")
                                .append(options.key.Month9_Str)
                                .appendTo(container);
                               }

                           }
                       },

                        {
                            dataField: "Month7_Str",
                            caption: "ตุลาคม",
                            alignment: 'center',
                            cellTemplate: function (container, options) {
                                if (options.key.Month10 > 0) {

                                    $("<div>")
                                 .append("<span class='badge badge-success'> " + options.key.Month10_Str + " </span>")
                                 .appendTo(container);
                                } else {


                                    $("<div>")
                                 .append(options.key.Month10_Str)
                                 .appendTo(container);
                                }

                            }
                        },

                            {
                                dataField: "Month8_Str",
                                caption: "พฤศจิกายน",
                                alignment: 'center',
                                cellTemplate: function (container, options) {
                                    if (options.key.Month11 > 0) {

                                        $("<div>")
                                     .append("<span class='badge badge-success'> " + options.key.Month11_Str + " </span>")
                                     .appendTo(container);
                                    } else {


                                        $("<div>")
                                     .append(options.key.Month11_Str)
                                     .appendTo(container);
                                    }

                                }
                            },

                       {
                           dataField: "Month9_Str",
                           caption: "ธันวาคม",
                           alignment: 'center',
                           cellTemplate: function (container, options) {
                               if (options.key.Month12 > 0) {

                                   $("<div>")
                                .append("<span class='badge badge-success'> " + options.key.Month12_Str + " </span>")
                                .appendTo(container);
                               } else {


                                   $("<div>")
                                .append(options.key.Month12_Str)
                                .appendTo(container);
                               }

                           }
                       },
        ],

    });

}


function ClearData() {


    $("#gridshow").hide();
    var formInstance = $("#form").dxForm("instance");
    formInstance.option('formData.TypeDate', 1);
    formInstance.option('formData.FromDate', new Date());
    formInstance.itemOption('FromDate', 'visible', true);
    formInstance.itemOption('ToDate', 'visible', false);
    formInstance.itemOption('Month', 'visible', false);
    formInstance.itemOption('Year', 'visible', false);
}