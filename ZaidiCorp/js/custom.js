
$(document).ready(function () {
    $("#DemoButton").click(function () {
        $("#form1").toggle();
        $("#form2").hide();
        $("#form3").hide();
    });

    $("#SalesButton").click(function () {
        $("#form2").toggle();
        $("#form3").hide();
        $("#form1").hide();
    });

    $("#CustomerButton").click(function () {
        $("#form3").toggle();
        $("#form2").hide();
        $("#form1").hide();
    });
});

//function sendmails1() {
//    var f = document.getElementById('contact-form');
//    var result = Validation.Required("#contact-form");
//    if (result == "Fail") {
//        //e.preventDefault();
//        return false;
//    } if (result == "Pass") {
//        data = {
//            message: $("#Message1").val(),
//            name: $("#name1").val(),
//            email: $("#email1").val(),
//            phone: $("#phone1").val()
//        }
//        $.ajax(
//        {
//            url: "../home/sendmail",
//            type: "POST",
//            contentType: "application/json",
//            data: JSON.stringify(data),
//            // data:data,
//            success: function (result) {
//                alert("Email Send Successfully");
//                location.reload();
//            }
//        });
//    }
//}

//function sendmails2() {
//    var f = document.getElementById('contact-form2');
//    var result = Validation.Required("#contact-form2");
//    if (result = "Fail") {
//        //e.preventDefault();
//        return false;
//    } if (result == "Pass") {
//        data = {
//            message: $("#Message2").val(),
//            name: $("#name2").val(),
//            email: $("#email2").val(),
//            phone: $("#phone2").val()
//        }
//        $.ajax(
//        {
//            url: "../home/sendmail21",
//            type: "POST",
//            contentType: "application/json",
//            data: JSON.stringify(data),
//            // data:data,
//            success: function (result) {
//                alert("Email Send Successfully");
//                location.reload();
//            }
//        });
//    }
//}

//function sendmails3() {
//    var f = document.getElementById('contact-form3');
//    var result = Validation.Required("#contact-form3");
//    if (result == "Fail") {
//        //e.preventDefault();
//        return false;
//    } if (result == "Pass") {
//        data = {
//            message: $("#Message3").val(),
//            name: $("#name3").val(),
//            email: $("#email3").val(),
//            phone: $("#phone3").val()
//        }
//        $.ajax(
//        {
//            url: "../home/sendmail22",
//            type: "POST",
//            contentType: "application/json",
//            data: JSON.stringify(data),
//            // data:data,
//            success: function (result) {
//                alert("Email Send Successfully");
//                location.reload();
//            }
//        });
//    }
//}