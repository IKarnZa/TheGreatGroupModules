$(function () {
    $.fn.datepicker.defaults.language = 'th';
    $('#DateAsOf').datepicker(
        {
            autoclose: true,
            format: 'dd/mm/yyyy',
            todayHighlight: true,
            changeMonth: true,
            changeYear: true,
        }
    );
});

var data = [];

DevExpress.viz.currentTheme("generic.light");

$.ajax({
    url: '../Staffs/GetStaffData?staffID=0&staffroleId=0',
    type: 'GET',
    contentType: 'application/json',
    success: function (data) {
        $('#StaffID')
            .find('option')
            .remove()
            .end()
            .append('<option value="" selected>=== เลือกพนักงาน === </option>');

        if (data.success == true) {

            $.each(data.data, function (key, value) {
                $("#StaffID").append('<option value="' + value.StaffID + '"' + ">" + value.StaffName + "</option>");
            });

        }

    },
    error: function () {
        console.log("error");
    }
});


function SearchLocation() {


    $("#loadIndicator").dxLoadIndicator({
        visible: true
    });

    if ($("#StaffID").val() == '') {

        $("#toast").dxToast({
            message: "กรุณาเลือกรายชื่อพนักงาน",
            type: "error",
            displayTime: 3000
        })
        $("#toast").dxToast("show");
        $("#loadIndicator").dxLoadIndicator({
            visible: false
        });
        return;
    }
    var date = $("#DateAsOf").datepicker({ dateFormat: 'dd-mm-yy' }).val();

    if (date == '' || date == null) {

        $("#toast").dxToast({
            message: "กรุณาเลือกวันที่",
            type: "error",
            displayTime: 3000
        })
        $("#toast").dxToast("show");
        $("#loadIndicator").dxLoadIndicator({
            visible: false
        });
        return;
    }


    var url = "../Staffs/GetLocationStaff?dateTime=" + $('#DateAsOf').val() + "&staffId=" + $("#StaffID").val();

    console.log("url ==> " + url);

    $.get(url)
        .done(function (data) {
            console.log(data);
            if (data.success == true) {

                Load_DataGrid(data);


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


$(function () {
    var markerUrl = "https://js.devexpress.com/Demos/RealtorApp/images/map-marker.png",
        markersData = [
            //   {
            //    location: [13.41149792, 100.0144377],
            //    tooltip: {
            //        text: "บริษัท เดอะ เกรท กรุ๊ป จำกัด"
            //    }
            //}
            //, 
            {
                location: "13.4257831, 99.9553108",
                tooltip: {
                    text: "คุณธนคุปชำระ 300 บาท เวลา 14.00 น."
                }
            },

            {
                location: "13.4257831, 99.95503185",
                tooltip: {
                    text: "คุณอโณชาชำระ 200 บาท"
                }
            },
            //{
            //    location: { lat: 40.753889, lng: -73.981389 },
            //    tooltip: {
            //        text: "Fifth Avenue"
            //    }
            //}, {
            //    location: "Brooklyn Bridge,New York,NY",
            //    tooltip: {
            //        text: "Brooklyn Bridge"
            //    }
            //}
        ];

    var mapWidget = $("#map").dxMap({
        zoom: 24,
        height: 440,
        width: "100%",
        controls: false,
        markerIconSrc: markerUrl,
        markers: markersData
    }).dxMap("instance");

    mapWidget.option("markerIconSrc", markerUrl);
    //$("#use-custom-markers").dxCheckBox({
    //    value: true,
    //    text: "Use custom marker icons",
    //    onValueChanged: function (data) {
    //        mapWidget.option("markers", markersData);
    //        mapWidget.option("markerIconSrc", data.value ? markerUrl : null);
    //    }
    //});

    $("#show-tooltips").dxButton({
        text: "แสดงคำอธิบาย",
        onClick: function () {
            var newMarkers = $.map(markersData, function (item) {
                return $.extend(true, {}, item, { tooltip: { isShown: true } });
            });

            mapWidget.option("markers", newMarkers);
        }
    });
});