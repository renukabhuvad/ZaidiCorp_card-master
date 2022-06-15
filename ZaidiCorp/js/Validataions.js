var Validation = {};
Validation.Required = function (MappedIds) {
    var Status = "";
    $(MappedIds + ' .erromessage').each(function (i, data) {
        $(this).remove();
    });
    $(MappedIds + ' .requireddata').each(function (i, data) {
        $(this).css({ 'border-color': '' });
        if ($(this).val() == '' || $(this).val() == '0') {
            Status = "1";
            $(this).css({ 'border-color': 'red' });
            $(this).after("<span class='erromessage' style='color:red' >This Field is Required</span>");
        }
    });
    var emailStatus = Validation.Email(MappedIds);

    var mobileStatus = Validation.ValidateMobile(MappedIds);
    if (Status == "1" || emailStatus == "1" || mobileStatus == 1)
        return "Fail";
    else {
        return "Pass";
    }
}
$('.requireddata').blur(function () {
    $(this).next("span").remove();
    if ($(this).val() != "" && $(this).val() != 0) {
        $(this).next("span").remove();
        $(this).css({ 'border-color': '' });
    }
    else {
        $(this).css({ 'border-color': 'red' });
        $(this).after("<span class='erromessage' style='color:red' >This Field is Required</span>");
    }

});
$('.requireddata').change(function () {
    $(this).next("span").remove();
    if ($(this).val() != "" && $(this).val() != 0) {
        $(this).next("span").remove();
        $(this).css({ 'border-color': '' });
    }
    else {
        $(this).css({ 'border-color': 'red' });
        $(this).after("<span class='erromessage' style='color:red' >This Field is Required</span>");
    }

});

$('.requireddata').click(function () {
    $(this).next("span").remove();
    $(this).css({ 'border-color': '' });
});
Validation.Email = function (MappedIds) {
    var Status = "";
    $(MappedIds + ' .validemail').each(function (i, data) {
        if ($(this).val() != "") {
            var emailstatus = Validation.ValidateEmail($(this).val());
            if (emailstatus == "Invalid") {
                Status = 1;
                $(this).css({ 'border-color': 'red' });
                $(this).after("<span class='erromessage' style='color:red' >Invalid Email</span>");
            }
            else {
                $(this).css({ 'border-color': '' });
            }
        }
        else {
            Status = 1;
            $(this).css({ 'border-color': 'red' });
            $(this).after("<span class='erromessage' style='color:red' >This Field is Required</span>");
        }
    });
    if (Status == "1")
        return "1";
    else
        return "";
}
Validation.ValidateEmail = function (sEmail) {
    var filter = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-z0-9]{2,4}$/;
    if (filter.test(sEmail)) {
        return "";
    }
    else {
        return "Invalid";
    }
}
Validation.ValidateMobile = function (MappedIds) {
    var Status = "";
    $(MappedIds + ' .validnumber').each(function (i, data) {
        if ($(this).val() != "") {
            if ($(this).val().length != 10) {
                Status = 1;
                $(this).css({ 'border-color': 'red' });
                $(this).after("<span class='erromessage' style='color:red' >Invalid Mobile NO.</span>");
            }
            else {
                $(this).css({ 'border-color': '' });
            }
        }
    });
    if (Status == "1")
        return "1";
    else
        return "";
}

$(".numbersOnly").keypress(function (e) {
    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
        return false;
    }

});
