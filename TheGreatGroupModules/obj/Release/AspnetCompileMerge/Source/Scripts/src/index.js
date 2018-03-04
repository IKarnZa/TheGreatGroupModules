$.get("../Home/CallGold")
.done(function (data) {

    if (data.success == true) {
        var result = data.data;

        $("#T1").html('<h3 class="text-right" >' + result[0].bid + '</h3>');
        $("#T2").html('<h3 class="text-right" >' + result[0].ask + '</h3>');
        $("#T3").html('<h3 class="text-right" >' + result[1].bid + '</h3>');
        $("#T4").html('<h3 class="text-right" >' + result[1].ask + '</h3>');
    } else {

        DevExpress.ui.notify(data.errMsg);
    }
});