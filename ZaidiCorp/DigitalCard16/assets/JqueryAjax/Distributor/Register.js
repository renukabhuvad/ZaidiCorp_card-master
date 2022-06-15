
var PanCard = "";
var PanFileExtension = "";
var AadharCard = "";
var AadharFileExtension = "";

$('input[id="exampleCheck1"]').click(function () {
    if (this.checked) {
        $("#RegisterBtn").prop("disabled", false);
    }
    else {
        $("#RegisterBtn").prop("disabled", true);
    }
});
init()
function init() {
    $('#frgtpwd').show();
    $('#validOtp').hide();
    $('#ChangePwd').hide();
    $('#sucmessage').hide();
    $('#resetpwd').show();
}

function IsEmail(email) {
    var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!regex.test(email)) {
        return false;
    } else {
        return true;
    }
}

function forgetpwd() {
    if (IsEmail($('#txtemailid').val()) == false) {
        //$.notify('Invalid Email Id', 'error');
        alert('Invalid Email Id');
        return
    }
    var det = {
        EmailId: $('#txtemailid').val()
    };
    $("#divLoading").show();
    $.ajax({
        url: '/Contact/ForgetPassword',
        datatype: "json",
        data: det,
        type: "post",
        success: function (data) {
            if (data.Status == 0) {
                //$.notify(data.Message, "error");
                alert(data.Message);
            }
            else {
                $('#frgtpwd').hide();
                $('#validOtp').show();
                $('#ChangePwd').hide();
                $('#sucmessage').hide();
                $('#resetpwd').show();
            }
            $("#divLoading").hide();
        },
        error: function (xhr) {
            $("#divLoading").hide();
        }
    });
}

function validateotp() {
    if (!$('#txtemailid').val().trim() || !$('#txtotp').val().trim()) {
        //$.notify('Please give OTP', 'error');
        alert('Please give OTP');
        return
    }
    var det = {
        EmailId: $('#txtemailid').val(),
        OTP: $('#txtotp').val()
    };
    $("#divLoading").show();
    $.ajax({
        url: '/Contact/ValidateOTP',
        datatype: "json",
        data: det,
        type: "post",
        success: function (data) {
            if (data.Status == 0) {
                //$.notify(data.Message, "error");
                alert(data.Message);
            }
            else {
                $('#frgtpwd').hide();
                $('#validOtp').hide();
                $('#ChangePwd').show();
                $('#sucmessage').hide();
                $('#resetpwd').show();
            }
            $("#divLoading").hide();
        },
        error: function (xhr) {
            $("#divLoading").hide();
        }
    });
}

function changePassword() {
    if (!$('#txtpassword').val() || !$('#txtcpassword').val()) {
        //$.notify('Fields marketed with * is mandatory', "warning");
        alert('Fields marketed with * is mandatory');
        return
    }
    if ($('#txtpassword').val().trim() != $('#txtcpassword').val().trim()) {
        //$.notify('New and confirm passwords do not match', "warning");
        alert('New and confirm passwords do not match');
        return
    }

    var det = {
        Password: $('#txtpassword').val(),
        CPassword: $('#txtcpassword').val(),
        EmailId: $('#txtemailid').val(),
    };
    $("#divLoading").show();
    $.ajax({
        url: '/Contact/ChangePassword',
        datatype: "json",
        data: det,
        type: "post",
        success: function (data) {
            if (data.Status == 1) {
                $('#frgtpwd').hide();
                $('#validOtp').hide();
                $('#ChangePwd').hide();
                $('#sucmessage').show();
                $('#resetpwd').hide();
                // window.location.href="/login"
            }
            else {
                //$.notify(data.Message, "error");
                alert(data.Message);
            }

            $("#divLoading").hide();
        },
        error: function (data) {
            $("#divLoading").hide();
        }
    });
};

function register() {
    if ($('#exampleCheck1').prop("checked") == true) {
        $("#RegisterBtn").prop("disabled", false);
    }
    else {
        return;
    }

    if (!$('#txtEmailId').val().trim() || !$('#txtPassword').val().trim()
        || !$('#txtMobileNo').val().trim() || !$('#txtFullName').val().trim()
    || !PanCard || !AadharCard) {
        //$.notify('Fields marketed with * is mandatory', "warning");
        alert('Fields marketed with * is mandatory');
        return
    }
    if ($('#txtPassword').val().trim() != $('#txtCPassword').val().trim()) {
        //$.notify('Password mismatch', "warning");
        alert('Password mismatch');
        return
    }
    var det = {
        EmailId: $('#txtEmailId').val(),
        MobileNo: $('#txtMobileNo').val(),
        FullName: $('#txtFullName').val(),
        Password: $('#txtPassword').val(),
        CPassword: $('#txtCPassword').val(),
        PanCard: PanCard,
        PanFileExtension: PanFileExtension,
        AadharCard: AadharCard,
        AadharFileExtension: AadharFileExtension
    };
    $("#divLoading").show();
    $.ajax({
        url: '/DRegister/Register',
        datatype: "json",
        data: det,
        type: "post",
        success: function (data) {
            alert(data.Message);
            if (data.Status == 1) {
        
                window.location.href="/home"
            }

            $("#divLoading").hide();
        },
        error: function (data) {

            $("#divLoading").hide();
        }
    });
};

function login() {
    if (!$('#txtEmailId').val().trim() || !$('#txtPassword').val().trim()) {
        //$.notify('Fields marketed with * is mandatory',"warning");
        alert('Fields marketed with * is mandatory');
        return
    }
    var det = {
        EmailId: $('#txtEmailId').val(),
        Password: $('#txtPassword').val()
    };
    $("#divLoading").show();
    $.ajax({
        url: '/DLogin/Login',
        datatype: "json",
        data: det,
        type: "post",
        success: function (data) {

            if (data.Status == 1) {
                //$.notify(data.Message, "success");
                $("#Login").hide();
                $("#Dashboard").show();
                window.location.href = "/DDashboard/index";
            }
            else {
                //$.notify(data.Message, "error");
                alert(data.Message);
            }

            $("#divLoading").hide();
        },
        error: function (data) {

            $("#divLoading").hide();
        }
    });
};

function getPanBase64() {
    var file1 = $("#PanCard").prop("files")[0];
    var reader = new FileReader();
    reader.readAsDataURL(file1);
    reader.onload = function () {
        console.log(reader.result);
        PanCard = reader.result;
        PanFileExtension = '.' + file1.name.split('.').pop();

    };
    reader.onerror = function (error) {
        console.log(error);
        file = error;
    };
}

function getAadharBase64() {
    var file1 = $("#AdharCard").prop("files")[0];
    var reader = new FileReader();
    reader.readAsDataURL(file1);
    reader.onload = function () {
        console.log(reader.result);
        AadharCard = reader.result;
        AadharFileExtension = '.' + file1.name.split('.').pop();

    };
    reader.onerror = function (error) {
        console.log(error);
        file = error;
    };
}