
$(function () {

    $("#gridshow").hide();
    LoadFormSearch();

});
var formdata = {
    ContractPayEveryDay: 1,
    FromDate: new Date(),
    ToDate: new Date(),
    Month: 1,
    Year: new Date().getFullYear()
};
var TypeDate = [
            {
                ID: 1,
                Name: "เลือกวัน เดือน ปี"

            },
            {
                ID: 2,
                Name: "เลือกเดือน ปี"
            },
            {
                  ID: 3,
                  Name: "เลือกปี "
            },
            {
                  ID: 4,
                  Name: "เลือกช่วงวันที่ "
            }
];

function LoadFormSearch() {
    var formInstance = $("#form").dxForm({
        colCount: 1,
        formData: formdata,
        showColonAfterLabel: true,
        showValidationSummary: false,
        width:450,
        items: [
             {
                 dataField: "ContractPayEveryDay",
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
                         } else if(e.value == 2){
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
                visible:false,
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


$("#myButton").dxButton({
    text: 'บันทึก',
    type: 'success',
    useSubmitBehavior: true,
    onClick: function () {
        console.log($("#form").dxForm("instance").option('formData'));


    }
});