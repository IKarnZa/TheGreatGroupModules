
$(function () {

    $("#gridshow").hide();
    LoadFormSearch();

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

function LoadFormSearch() {

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
                  dataField: "CustomerID",
                  label: {
                      text: "เลือกชื่อลูกค้า "
                  },
                  visible: true,
                  editorType: "dxSelectBox",
                  editorOptions: {
                      items: Months,
                      displayExpr: "Name",
                      valueExpr: "ID",

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
    var DataSearch = $("#form").dxForm("instance").option('formData');

    $.ajax({
        url: '../Report/GetPaymentReportByCustomer?CustomerID=1&ContractID=1',
        type: 'GET',
        contentType: 'application/json',
       // data: JSON.stringify(DataSearch),
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

            },
                 {
                     dataField: "Month2_Str",
                     caption: "กุมภาพันธ์",
                     alignment: 'right',
                 },
                 {
                     dataField: "Month3_Str",
                     caption: "มีนาคม",
                     alignment: 'right',
                 },
                  {
                      dataField: "Month4_Str",
                      caption: "เมษายน",
                      alignment: 'right',
                  },

                  {
                          dataField: "Month5_Str",
                          caption: "พฤษภาคม",
                          alignment: 'center',
                  },
                  {
                      dataField: "Month6_Str",
                      caption: "มิถุนายน",
                      alignment: 'center',
                  },

                       {
                           dataField: "Month7_Str",
                           caption: "กรกฎาคม",
                           alignment: 'center',
                       },

                            {
                                dataField: "Month8_Str",
                                caption: "สิงหาคม",
                                alignment: 'center',
                            },

                       {
                           dataField: "Month9_Str",
                           caption: "กันยายน",
                           alignment: 'center',
                       },

                        {
                            dataField: "Month7_Str",
                            caption: "ตุลาคม",
                            alignment: 'center',
                        },

                            {
                                dataField: "Month8_Str",
                                caption: "พฤศจิกายน",
                                alignment: 'center',
                            },

                       {
                           dataField: "Month9_Str",
                           caption: "ธันวาคม",
                           alignment: 'center',
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