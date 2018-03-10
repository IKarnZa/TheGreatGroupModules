$(function () {
    $("#loadIndicator").dxLoadIndicator({
        visible: true
    });
  
    var dataProvince = [];
    var dataDistrict = [];
    var dataSubDistrict = [];

   
    var dataProvince = [];
    var dataDistrict = [];
    var dataSubDistrict = [];
    var CustomerID = getUrlParameter('CustomerID');
    $.get("../Customers/GetCustomerID/" + CustomerID)
    .done(function (data) {
        if (data.success == true) {

            dataProvince = data.dataProvince;
            dataDistrict = data.dataDistrict;
            dataSubDistrict = data.dataSubDistrict;
            datasourceCustomer = data.dataCustomer;
            LoadForm_CustomerInfo(datasourceCustomer, dataProvince,dataDistrict, dataSubDistrict);
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