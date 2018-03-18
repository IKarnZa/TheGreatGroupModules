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
            data.dataCustomer.CustomerID = CustomerID;
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
        text: "แก้ไขข้อมูลลูกค้า",
        type: "success",
        useSubmitBehavior: true,
        validationGroup: "customerData",
        onClick: function (e) {
            datasourceCustomer = $("#form").dxForm("instance").option('formData');
            datasourceCustomer.CustomerID = getUrlParameter('CustomerID');
            $.ajax({
                url: '../Customers/EditCustomers',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(datasourceCustomer),
                success: function (data) {
                  

                    DevExpress.ui.notify(data.data);
                },
                error: function () {
                    console.log("error");
                }
            });


        }
    });


});