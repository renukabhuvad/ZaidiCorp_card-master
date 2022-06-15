
function IsEmail(email) {
    var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!regex.test(email)) {
        return false;
    } else {
        return true;
    }
}

function submit() {
    if (!$('#txtName').val() || !$('#txtEmailId').val() || !$('#txtPhoneNo').val()) {
        //$.notify('Fields marketed with * is mandatory', 'error');
        alert('Fields marketed with * is mandatory');
        return
    }
    if (IsEmail($('#txtEmailId').val()) == false) {
        //$.notify('Invalid Email Id', 'error');
        alert('valid Email Id');
        return
    }
   
    var det = {
        Name: $('#txtName').val(),
        EmailId: $('#txtEmailId').val(),
        PhoneNo: $('#txtPhoneNo').val(),
      
        Message: $('#txtMessage').val()
    };
    $("#divLoading").show();
    $('#btnSubmit').attr('disabled', true);
    $.ajax({
        url: '/Contact/RequestQuote',
        datatype: "json",
        data: det,
        type: "post",
        success: function (data) {
            if (data.Status == 0) {
                //$.notify(data.Message, "error");
                alert(data.Message);
            }
            else {
                //$.notify(data.Message, "success");
                alert(data.Message);
                window.location.href = "/contact";

            }
            $("#divLoading").hide();
            $('#btnSubmit').attr('disabled', false);
        },
        error: function (xhr) {
            $("#divLoading").hide();
            $('#btnSubmit').attr('disabled', false);
        }
    });
};

function submitcard() {
    if (!$('#txtName').val() || !$('#txtEmailId').val() || !$('#txtPhoneNo').val()) {
        //$.notify('All fields are mandatory', 'error');
        alert('All fields are mandatory');
        return
    }
    if (IsEmail($('#txtEmailId').val()) == false) {
        //$.notify('Invalid Email Id', 'error');
        alert('Invalid Email Id');
        return
    }

    var det = {
        Name: $('#txtName').val(),
        EmailId: $('#txtEmailId').val(),
        PhoneNo: $('#txtPhoneNo').val(),
        ToEmail: $('#mailid').text(),
        Message: $('#txtMessage').val()
    };
    $("#divLoading").show();
    $('#btnSubmitc').attr('disabled', true);
    $.ajax({
        url: '/Contact/RequestQuoteCard',
        datatype: "json",
        data: det,
        type: "post",
        success: function (data) {
            if (data.Status == 0) {
                //$.notify(data.Message, "error");
                alert(data.Message);
            }
            else {
                //$.notify(data.Message, "success");
                alert(data.Message);
                $('#txtName').val('');
                $('#txtEmailId').val('');
                $('#txtPhoneNo').val('');
                $('#mailid').text('');
                $('#txtMessage').val('');

            }
            $("#divLoading").hide();
            $('#btnSubmitc').attr('disabled', false);
        },
        error: function (xhr) {
            $("#divLoading").hide();
            $('#btnSubmitc').attr('disabled', false);
        }
    });
};