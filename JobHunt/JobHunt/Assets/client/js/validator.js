//define
var Avatar = "";

jQuery.extend(jQuery.validator.messages, {
    required: "Không được để trống",
    //remote: "Please fix this field.",
    //email: "Please enter a valid email address.",
    //url: "Please enter a valid URL.",
    //date: "Please enter a valid date.",
    //dateISO: "Please enter a valid date (ISO).",
    //number: "Please enter a valid number.",
    //digits: "Please enter only digits.",
    //creditcard: "Please enter a valid credit card number.",
    //equalTo: "Please enter the same value again.",
    //accept: "Please enter a value with a valid extension.",
    //maxlength: jQuery.validator.format("Please enter no more than {0} characters."),
    //minlength: jQuery.validator.format("Please enter at least {0} characters."),
    //rangelength: jQuery.validator.format("Please enter a value between {0} and {1} characters long."),
    //range: jQuery.validator.format("Please enter a value between {0} and {1}."),
    //max: jQuery.validator.format("Please enter a value less than or equal to {0}."),
    //min: jQuery.validator.format("Please enter a value greater than or equal to {0}.")
});

$.validator.addMethod(
    "regex",
    function (value, element, regexp) {
        var check = false;
        return this.optional(element) || regexp.test(value);
    },
    "Please check your input."
);
$.validator.addMethod("selectRequired", function (value, element, arg) {
    return arg != value;
}, "Hãy chọn một giá trị");

window.onload = thing();
function thing() {
    var pageURL = $(location).attr("href");
    var url = "/dang-tin";
    if (pageURL.indexOf(url) != -1) {
        $(".modal-backdrop").css({ position: 'inherit !important' });
    }
}
//validate form update profile recruit
$("#valid-update-profile").validate({
    rules: {
        RICompanyName: {
            required: true
        },
        RIAbout: {
            required: true
        }
    },
    messages: {

    },
    submitHandler: function (form) {
        UpdateProfileRecruit();
    }
});

//update infor recruit
function UpdateProfileRecruit() {
    //get backgorund image
    var getbg = $('#bg-recruit').css('background-image');
    getbg = getbg.replace('url(', '').replace(')', '').replace(/\"/gi, "");
    getbg = getbg.replace(window.location.origin, "");
    var recruitdto = {
        RecruitId: $('#RecruitId').val(),
        RICompanyName: $('#RICompanyName').val(),
        RILogo: $('#logocompany').attr('src'),
        FoundedYear: $('#FounedYearString').val(),
        RI_CompanySizeId: $('#RI_CompanySizeId').val(),
        RI_ProfessionId: $('#RI_ProfessionId').val(),
        RIAbout: $('#RIAbout').val(),
        RIAvatar: $('#avatarlink').attr('src'),
        RICoverImage: getbg
    };
    $.ajax({
        type: 'POST',
        url: '/Recruit/UpdateProfileRecruit',
        data: recruitdto,
        success: function (result) {
            $.notify(result.message, result.status);
        },
        error: function (err) {
            $.notify("Có lỗi " + err + " khi tải ảnh lên", "error");
        }
    });
}

//validate update contact recruit
$("#valid-update-profile-contact").validate({
    rules: {
        RICompanyName: {
            required: true
        },
        RIAbout: {
            required: true
        }
    },
    messages: {

    },
    submitHandler: function (form) {
        UpdateContactRecruit();
    }
});

//update contact recruit
function UpdateContactRecruit() {
    var recruitdto = {
        RecruitId: $('#RecruitId').val(),
        RIPhone: $('#RIPhone').val(),
        RIEmail: $('#RIEmail').val(),
        RIWebsite: $('#RIWebsite').val(),
        RI_CityId: $('#RI_CityId').val(),
        RI_DistrictId: $('#RI_DistrictId').val(),
        RI_WardId: $('#RI_WardId').val(),
        RIAddress: $('#RIAddress').val()
    };
    $.ajax({
        type: 'POST',
        url: '/Recruit/UpdateContactRecruit',
        data: recruitdto,
        success: function (result) {
            $.notify(result.message, result.status);
        },
        error: function (err) {
            $.notify("Có lỗi " + err + " khi tải ảnh lên", "error");
        }
    });
}


//post job
$("#valid-postjob").validate({
    rules: {
        RJTitle: {
            required: true
        },
        RJPosition: {
            required: true
        },
        RICompanyName: {
            required: true
        },
        RJAddress: {
            required: true
        },
        RJAmount: {
            required: true
        },
        RIAbout: {
            required: true
        },
        RJ_Require: {
            required: true
        },
        RJ_WorkPlace: {
            required: true
        },
        RJExpirationDate: {
            required: true
        },
        RJ_ProfessionId: {
            required: true,
            selectRequired: ""
        },
        RJ_SalaryId: {
            selectRequired: ""
        },
        RJEmailContact: {
            required: true,
            email: true,
            regex: /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
        },
        RJPhoneContact: {
            required: true,
            regex: /(^\(\+\84\)\s\d{3}\s\d{3}$)|(^\d{10}$)|(^\d{4}\s\d{3}\s\d{3}$)|(^\(\+\84\)\d{3}\d{3}$)/,
        },
        RJNameContact: {
            required: true
        },
        RJ_Describe: {
            required: true
        },
        RJBenefit: {
            required: true
        }
        //RJ_SalaryId: {
        //    selectRequired: null
        //}
    },
    messages: {

        RJTitle: {
        },
        RJPosition: {
        },
        RICompanyName: {
        },
        RJAddress: {
        },
        RJAmount: {
        },
        RIAbout: {
        },
        RJ_Require: {
        },
        RJ_WorkPlace: {
        },
        RJExpirationDate: {
        },
        RJEmailContact: {
            email: "Hãy nhập đúng định dạng email"
        },
        RJPhoneContact: {
            regex: "Hãy nhập đúng định dạng điện thoại"
        },
        RJNameContact: {
        },
        RJ_Describe: {
        },
        RJBenefit: {
        }
    },
    submitHandler: function (form) {
        var typeFix = $('#TypeFix').val();
        PostJob(typeFix);
    }
});
//post job
function PostJob(typeFix) {
    var recruitjobdto = {
        RJTitle: $("#RJTitle").val(),
        RJPosition: $("#RJPosition").val(),
        RJAddress: $('#RJAddress').val(),
        RJAmount: $("#RJAmount").val(),
        RJ_ProfessionId: $("#RJ_ProfessionId").children("option:selected").val(),
        RJ_LevelId: $("#RJ_LevelId").children("option:selected").val(),
        RJ_Require: $('#RJ_Require').summernote('code'),
        RJ_WorkTypeId: $("#RJ_WorkTypeId").children("option:selected").val(),
        RJ_SalaryId: $("#RJ_SalaryId").children("option:selected").val(),
        RJ_WorkPlace: $("#RJ_WorkPlace").val(),
        RJExpirationDate: $("#RJExpirationDate").val(),
        RJEmailContact: $("#RJEmailContact").val(),
        RJPhoneContact: $("#RJPhoneContact").val(),
        RJNameContact: $("#RJNameContact").val(),
        RJ_ExperienceId: $("#RJ_ExperienceId").children("option:selected").val(),
        RJCityId: $("#RJCityId").children("option:selected").val(),
        RJDistrictId: $("#RJDistrictId").children("option:selected").val(),
        RJ_WardId: $("#RJ_WardId").children("option:selected").val(),
        RJGender: $("#RJGender").children("option:selected").val(),
        RJ_Describe: $('#RJ_Describe').summernote('code'),
        RJBenefit: $('#RJBenefit').summernote('code')
    };
    //type: 1-thêm, 2-sửa
    if (typeFix == 2) {
        recruitjobdto.RecruitJobId = $('#RecruitJobId').val();
    }
    $.ajax({
        type: "POST",
        url: '/RecruitJob/PostANewJob',
        data: { recruitjobdto: recruitjobdto, typeFix: parseInt(typeFix) },
        success: function (result) {
            $.notify(result.message, result.status);
            if (result.status == "success") {
                window.setTimeout(function () { location.reload(); }, 500);
            }
        },
        error: function (ex) {
            $.notify("Đăng ký không thành công!", "error");
            return false;
        }
    });
}


//update profile candidate
$("#valid-update-candidate-profile").validate({
    rules: {
        CddFullName: {
            required: true
        },
        CddAge: {
            required: true
        },
        CddAbout: {
            required: true
        },
        CPExperience: {
            required: true
        }
        //RJ_SalaryId: {
        //    selectRequired: null
        //}
    },
    messages: {
    },
    submitHandler: function (form) {
        UpdateProfileCandidate();
    }
});

//update profile in /Candidate/CandidateProfile 
function UpdateProfileCandidate() {
    Avatar = $('#avatarlink').attr('src');
    var candidateDTO = {
        CandidateId: $('#IdCandidate').val(),
        CddFullName: $('#CddFullName').val(),
        CddAge: $('#CddAge').val(),
        CddSex: $('#CddSex').val(),
        CPExperience: $('#CPExperience').val(),
        CP_ProfessionId: $("#CP_ProfessionId option:selected").val(),
        CP_SalaryId: $("#CP_SalaryId option:selected").val(),
        CP_ExperienceId: $("#CP_ExperienceId option:selected").val(),
        CP_LevelId: $("#CP_LevelId option:selected").val(),
        CP_WorkTypeId: $("#CP_WorkTypeId option:selected").val(),
        CddAbout: $('textarea#CddAbout').val(),
        CPAvatar: Avatar
    };
    $.ajax({
        type: "POST",
        url: '/Candidate/UpdateProfile',
        data: candidateDTO,
        success: function (result) {
            $('.avatarlinkclass').attr("src", Avatar);
            $.notify(result.message, result.status);
        },
        error: function (ex) {
            $.notify("Cập nhật không thành công!", "error");
            return false;
        }
    });
}

//update profile candidate
//$("#valid-update-candidate-contact").validate({
$("#valid-update-candidate-contact").validate({
    rules: {
        CddPhone: {
            required: true,
            regex: /(^\(\+\84\)\s\d{3}\s\d{3}$)|(^\d{10}$)|(^\d{4}\s\d{3}\s\d{3}$)|(^\(\+\84\)\d{3}\d{3}$)/,
        },
        CddFacebook: {
            required: true
        },
        CddGoogle: {
            required: true
        },
        CddEmail: {
            required: true,
            email: true
            //regex: /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
        },
        CddAddress: {
            required: true
        }
        //RJ_SalaryId: {
        //    selectRequired: null
        //}
    },
    messages: {
        CddPhone: {
            regex: "Hãy nhập đúng định dạng điện thoại"
        },
        CddFacebook: {
        },
        CddGoogle: {
        },
        CddEmail: {
            email: "Hãy nhập đúng định dạng email"
        },
        CddAddress: {
        }
    },
    submitHandler: function (form) {
        UpdateContactAndSocialCandidate();
    }
});

function UpdateContactAndSocialCandidate() {
    var candidateDTO = {
        CandidateId: $('#IdCandidate').val(),
        CddEmail: $('#CddEmail').val(),
        CddPhone: $('#CddPhone').val(),
        CddFacebook: $('#CddFacebook').val(),
        CddGoogle: $('#CddGoogle').val(),
        CddAddress: $('#CddAddress').val(),
        Cdd_CityId: $('#Cdd_CityId').val(),
        Cdd_DistrictId: $('#Cdd_DistrictId').val(),
        Cdd_WardId: $('#Cdd_WardId').val()
    };
    $.ajax({
        type: "POST",
        url: '/Candidate/UpdateSocialAndContact',
        data: candidateDTO,
        success: function (result) {
            $.notify(result.message, result.status);
        },
        error: function (ex) {
            $.notify("Cập nhật không thành công!", "error");
            return false;
        }
    });
}


//Login
$("#__AjaxAntiForgeryForm").validate({
    rules: {
        Password: {
            required: true,
            minlength: 6
        },
        Email: {
            required: true,
            email: true
        }
    },
    messages: {
        Password: {
            required: "Mật khẩu không được để trống",
            minlength: "Mật khẩu có độ dài tối thiểu là 6 ký tự"
        },
        Email: {
            email: "Hãy nhập đúng định dạng email"
        }
    },
    submitHandler: function (form) {
        Login();
    }
});

//Login
function Login() {
    var model = {
        Email: $('#EmailLogin').val(),
        Password: $('#PassLogin').val(),
        RememberMe: $('#RememberMe').is(":checked")
    };
    var type = $(".logintype.active").attr('id');
    var checkTM = true;
    if (type == undefined) {
        checkTM = false;
        $.notify("Hãy chọn người dùng bạn muốn đăng nhập!", "error");
    }
    else if (model.Email == '' || model.Password == '') {
        checkTM = false;
        $.notify("Hãy nhập đủ thông tin", "error");
    }
    if (checkTM) {
        $('#Login')
            .after('<img src="/assets/client/images/loader.gif" class="loader" />')
            .attr('disabled', 'disabled');
        $.ajax({
            type: "POST",
            url: '/Account/Login',
            data: AddAntiForgeryToken({ type: type, model: model }),
            success: function (result) {
                $('#__AjaxAntiForgeryForm img.loader').fadeOut('slow', function () { $(this).remove(); });
                $.notify(result.message, result.status);
                if (result.status == "success") {
                    window.setTimeout(function () { location.reload(); }, 300);
                } else {
                    $('#Login').removeAttr("disabled");
                }
            },
            error: function (ex) {
                $('#Login').removeAttr("disabled");
                $('#__AjaxAntiForgeryForm img.loader').fadeOut('slow', function () { $(this).remove(); });
                $.notify("Đăng nhập không thành công!", "error");
                return false;
            }
        });
    }
}

//Register

$("#registerform").validate({
    rules: {
        UserName: {
            required: true,
            minlength: 6,
            maxlength: 30
        },
        PassRegister: {
            required: true,
            minlength: 6
        },
        ConfirmPassword: {
            equalTo: "#PassRegister"
        },
        EmailRegister: {
            required: true,
            email: true
        },
        PhoneRegister: {
            required: true,
            regex: /(^\(\+\84\)\s\d{3}\s\d{3}$)|(^\d{10}$)|(^\d{4}\s\d{3}\s\d{3}$)|(^\(\+\84\)\d{3}\d{3}$)/,
        }
    },
    messages: {
        UserName: {
            required: "Tên đăng ký không được để trống",
            minlength: "Tên đăng ký có độ dài tối thiểu là 6 ký tự",
            maxlength: "Tên đăng ký có đội dài tối đa là 30 ký tự"
        },
        PassRegister: {
            required: "Mật khẩu không được để trống",
            minlength: "Mật khẩu có độ dài tối thiểu là 6 ký tự"
        },
        ConfirmPassword: {
            equalTo: "Mật khẩu không trùng khớp"
        },
        PhoneRegister: {
            regex: "Hãy nhập đúng định dạng điện thoại"
        },
        EmailRegister: {
            email: "Hãy nhập đúng định dạng email"
        }
    },
    submitHandler: function (form) {
        Register();
    }
});
//Register
function Register() {
    var model = {
        UserName: $('#UserNameRegister').val(),
        Phone: $('#PhoneRegister').val(),
        Email: $('#EmailRegister').val(),
        Password: $('#PassRegister').val(),
        ConfirmPassword: $('#ConfirmPassRegister').val(),
    };
    var type = $(".registertype.active").attr('id');
    var checkTM = true;
    if (type == undefined) {
        checkTM = false;
        $.notify("Hãy chọn người dùng bạn muốn đăng nhập!", "error");
    }
    else if (model.Email == '' || model.Phone == '' || model.UserName == '' || model.Password == '' || model.ConfirmPassword == '') {
        checkTM = false;
        $.notify("Hãy nhập đủ thông tin", "error");
    }
    if (checkTM) {
        $('#Register')
            .after('<img src="/assets/client/images/loader.gif" class="loader" />');
        $.ajax({
            type: "POST",
            url: '/Account/Register',
            data: AddAntiForgeryToken({ type: type, model: model }),
            success: function (result) {
                $('#registerform img.loader').fadeOut('slow', function () { $(this).remove(); });
                $.notify(result.message, result.status);
                if (result.status == "success") {
                    $('#registerform')[0].reset();
                    window.setTimeout(function () { location.reload(); }, 500);
                }
            },
            error: function (ex) {
                $('#registerform img.loader').fadeOut('slow', function () { $(this).remove(); });
                $.notify("Đăng ký không thành công!", "error");
                return false;
            }
        });
    }
}

//To accept ValidateAntiForgeryToken in controller
AddAntiForgeryToken = function (data) {
    data.__RequestVerificationToken = $('#__AjaxAntiForgeryForm input[name=__RequestVerificationToken]').val();
    return data;
};


//Change password Candidate
$("#ChangePassword").validate({
    rules: {
        OddPass: {
            required: true,
            minlength: 6
        },
        NewPass: {
            required: true,
            minlength: 6
        },
        ConfirmPass: {
            equalTo: "#NewPass"
        }
    },
    messages: {
        OddPass: {
            required: "Mật khẩu không được để trống",
            minlength: "Mật khẩu có độ dài tối thiểu là 6 ký tự"
        },
        NewPass: {
            required: "Mật khẩu không được để trống",
            minlength: "Mật khẩu có độ dài tối thiểu là 6 ký tự"
        },
        ConfirmPass: {
            equalTo: "Mật khẩu xác nhận không trùng khớp"
        }
    },
    submitHandler: function (form) {
        var OddPass = $("#OddPass").val();
        var NewPass = $("#NewPass").val();
        var ConfirmPass = $("#ConfirmPass").val();
        $.ajax({
            type: "POST",
            url: '/Candidate/ChangePasswordCdd',
            data: { OddPass: OddPass, NewPass: NewPass, ConfirmPass: ConfirmPass },
            success: function (result) {
                $.notify(result.message, result.status);
                $('#ChangePassword')[0].reset();
            },
            error: function (ex) {
                $.notify("Đổi không thành công!", "error");
                return false;
            }
        });
    }
});

//Change password Recruit
$("#ChangePasswordRC").validate({
    rules: {
        OddPass: {
            required: true,
            minlength: 6
        },
        NewPass: {
            required: true,
            minlength: 6
        },
        ConfirmPass: {
            equalTo: "#NewPass"
        }
    },
    messages: {
        OddPass: {
            required: "Mật khẩu không được để trống",
            minlength: "Mật khẩu có độ dài tối thiểu là 6 ký tự"
        },
        NewPass: {
            required: "Mật khẩu không được để trống",
            minlength: "Mật khẩu có độ dài tối thiểu là 6 ký tự"
        },
        ConfirmPass: {
            equalTo: "Mật khẩu xác nhận không trùng khớp"
        }
    },
    submitHandler: function (form) {
        var OddPass = $("#OddPass").val();
        var NewPass = $("#NewPass").val();
        var ConfirmPass = $("#ConfirmPass").val();
        $.ajax({
            type: "POST",
            url: '/Recruit/ChangePasswordRC',
            data: { OddPass: OddPass, NewPass: NewPass, ConfirmPass: ConfirmPass },
            success: function (result) {
                $.notify(result.message, result.status);
                $('#ChangePasswordRC')[0].reset();
            },
            error: function (ex) {
                $.notify("Đổi không thành công!", "error");
                return false;
            }
        });
    }
});
var IDJob;
//delete job
$('.deletejob').click(function () {
    IDJob = $(this).attr('id').substring(2);
    $('.title-job').text($('#titlejob' + idJob).text());
});

$('.btndeletejob').click(function () {
    $.ajax({
        type: "POST",
        url: '/RecruitJob/DeleteJob',
        data: { idJob: IDJob },
        success: function (result) {
            $('#exampleModalCenter').css('display', 'none');
            $('.modal-backdrop.show').css('opacity', '0');
            if (result) {
                $.notify("Xóa thành công!", "success");
                window.setTimeout(function () { location.reload(); }, 500);
            } else {
                $.notify("Xóa không thành công!", "error");
                return false;
            }
        },
        error: function (ex) {
            $.notify("Xóa không thành công!", "error");
            return false;
        }
    });
});