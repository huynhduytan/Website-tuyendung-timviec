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
////////////////////////////////////////end validate add supplier///////////////////////////
//function UpdateMember(type) {
//    $.ajax({
//        type: "POST",
//        url: '/Admin/JsonCommon/UpdateCustomer',
//        data: formData,
//        contentType: false,
//        processData: false,
//        success: function (result) {
//        },
//        error: function (ex) {
//        }
//    });
//}

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
    var checkTM = true;
    if (model.Email == '' || model.Password == '') {
        checkTM = false;
        $.notify("Hãy nhập đủ thông tin", "error");
    }
    if (checkTM) {
        //$('#Login')
        //    .after('<img src="/assets/client/images/loader.gif" class="loader" />')
        //    .attr('disabled', 'disabled');
        $.ajax({
            type: "POST",
            url: '/AccountManage/Login',
            data: AddAntiForgeryToken({ model: model }),
            success: function (result) {
                //$('#__AjaxAntiForgeryForm img.loader').fadeOut('slow', function () { $(this).remove(); });
                $.notify(result.message, result.status);
                if (result.status == "success") {
                    window.setTimeout(function () { location.href = "/Admin/DashBoard"; }, 300);
                } else {
                    //$('#Login').removeAttr("disabled");
                }
            },
            error: function (ex) {
                //$('#Login').removeAttr("disabled");
                //$('#__AjaxAntiForgeryFormAdmin img.loader').fadeOut('slow', function () { $(this).remove(); });
                $.notify("Đăng nhập không thành công!", "error");
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

var CandidateId;
//Update profile candidate
$('.btn-update-cdd').click(function () {
    var idCandidate = parseInt($(this).attr('id').substring(3));
    $.ajax({
        type: "POST",
        url: '/Admin/CandidateManagement/GetInforCandidateById',
        data: { idCandidate: idCandidate },
        success: function (result) {
            SetValueForCandidate(result.resultObject);
        },
        error: function (ex) {
            $.notify("Lấy thông tin không thành công!", "error");
            return false;
        }
    });
});

var idCandidate;
//Update profile candidate
$('.btn-del-cdd').click(function () {
    idCandidate = parseInt($(this).attr('input-id-cdd'));
    var username = $(this).parent().siblings(".username")[0].innerText;
    $('#namedelete').text(username);

});

$('#btn-yes-delete-cdd').click(function () {
    $.ajax({
        type: "POST",
        url: '/Admin/CandidateManagement/DeleteCandidate',
        data: { idCandidate: idCandidate },
        success: function (result) {
            $.notify(result.message, result.status);
            $('#exampleModalCenter').css('display', 'none');
            $('.modal-backdrop').remove();
            if (result.status == "success") {
                window.setTimeout(function () { location.reload(); }, 500);
            }
        },
        error: function (ex) {
            $.notify("Lấy thông tin không thành công!", "error");
            return false;
        }
    });
});

$("#updatecandidate-form").validate({
    rules: {
        CddUserName: {
            required: true
        },
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
        },
        CddPhone: {
            required: true
        },
        CddEmail: {
            required: true
        },
        CddAddress: {
            required: true
        },
        CddPassword: {
            required: true,
            minlength: 6
        },
        CddPasswordConfirm: {
            equalTo: "#CddPassword"
        },
        CP_ProfessionId: {
            selectRequired: ""
        },
        CP_SalaryId: {
            selectRequired: ""
        },
        CP_ExperienceId: {
            selectRequired: ""
        },
        CP_LevelId: {
            selectRequired: ""
        },
        CP_WorkTypeId: {
            selectRequired: ""
        },
        Cdd_CityId: {
            selectRequired: ""
        },
        Cdd_DistrictId: {
            selectRequired: ""
        },
        Cdd_WardId: {
            selectRequired: ""
        }
    },
    messages: {
        CddPassword: {
            minlength: "Mật khẩu ít nhất 6 ký tự"
        },
        CddPasswordConfirm: {
            equalTo: "Mật khẩu không trùng khớp"
        }
    },
    submitHandler: function (form) {
        //type 1: Thêm, type 2: Sửa
        var type = $('.updatecandidate').attr('id').substring(10);
        UpdateProfileCandidate(type);
    }
});

//update profile in /Candidate/CandidateProfile 
function UpdateProfileCandidate(type) {

    var checkTM = false;
    // Đối tượng formdata
    var formData = new FormData();

    Avatar = $('#avatarlink').attr('src');
    var cdddto = {
        CddFullName: $('#CddFullName').val(), //
        CddAge: $('#CddAge').val(), //
        CddSex: $('#CddSex').val(), //
        CP_ProfessionId: $("#CP_ProfessionId option:selected").val(), //
        CddAbout: $('textarea#CddAbout').val(), //
        CP_SalaryId: $("#CP_SalaryId option:selected").val(), //
        CP_ExperienceId: $("#CP_ExperienceId option:selected").val(), //
        CP_LevelId: $("#CP_LevelId option:selected").val(), //
        CP_WorkTypeId: $("#CP_WorkTypeId option:selected").val(), //
        CPExperience: $('#CPExperience').val(), //
        CPStatus: $("#CPStatus option:selected").val(), //

        CddPhone: $('#CddPhone').val(), //
        CddEmail: $('#CddEmail').val(), //
        CddFacebook: $('#CddFacebook').val(), //
        CddGoogle: $('#CddGoogle').val(), //

        Cdd_CityId: $("#Cdd_CityId option:selected").val(), //
        Cdd_DistrictId: $("#Cdd_DistrictId option:selected").val(), //
        Cdd_WardId: $("#Cdd_WardId option:selected").val(), //
        CddAddress: $('#CddAddress').val(), //
        CddDescribeCV: $('textarea#CddDescribeCV').val(), //
        CPAvatar: Avatar
    };
    if (type == 1) {
        cdddto.CddUserName = $('#CddUserName').val(); //
        cdddto.CddPassword = $('#CddPassword').val(); //
    } else if (type == 2) {
        cdddto.CandidateId = $('#CandidateId').val(); //
    }
    // Chuyển dữ liệu sang dạng dataform
    for (var item in cdddto) {
        formData.append(item, cdddto[item]);
    }
    //Lấy dữ liệu trên fileUpload
    var files = $('#fileCV')[0].files;

    formData.append("typeMember", "candidateregister");
    formData.append('fileCV', files[0]);
    if (type == 1 && files[0] == undefined) {
        $.notify("Hãy chọn file để tải lên", "error");
        checkTM = false;
    } else {
        checkTM = true;
    }

    if (checkTM) {
        if (type == 1) {
            $.ajax({
                type: "POST",
                url: '/Admin/AccountManage/Register',
                data: formData,
                contentType: false,
                processData: false,
                success: function (result) {
                    $.notify(result.message, result.status);
                    if (result.status == "success") {
                        $('#updatecandidate-form')[0].reset();
                    }
                },
                error: function (ex) {
                    $.notify("Tạo không thành công!", "error");
                    return false;
                }
            });
        } else {
            $.ajax({
                type: "POST",
                url: '/Admin/CandidateManagement/UpdateProfileCandidate',
                data: formData,
                contentType: false,
                processData: false,
                success: function (result) {
                    $.notify(result.message, result.status);
                    //if (result.status == "success") {
                    //    $('#updatecandidate-form')[0].reset();
                    //}
                },
                error: function (ex) {
                    $.notify("Cập nhật không thành công!", "error");
                    return false;
                }
            });
        }
    }
}

//upload cv
var $dn = $(".uploadfilecv");

function dnChange() {
    if ($(this)[0].files.length > 0) {
        //var dungluong = (this.files[0].size / 1024 / 1024).toFixed(3);
        //if (parseInt(dungluong) > 3) {
        //    alert("Ảnh có dụng lượng tối đa 3MB. Hãy chọn ảnh khác nhé bạn!");
        //} else {
        var NameFile = $(this)[0].files[0].name;
        if (ValidateFile(NameFile)) {
            $('.namefile').text(NameFile);
            //$('#name-file-uploaded').text(NameFile);
        } else {
            $('.uploadfilecv').val("");
        }
        //}
    }
}
$dn.change(dnChange);
//validate
function ValidateFile(extention) {
    if (extention != '') {
        var checkExtention = extention.toLowerCase();
        if (!checkExtention.match(/(\.pdf|\.docx|\.doc)$/)) {
            $.notify("Vui lòng chọn file đúng định dạng", "error");
            return false;
        }
    }
    return true;
}


//
function SetValueForCandidate(cdd) {
    CandidateId = cdd.CandidateId;
    $('#namecddupdate').text(cdd.CddUserName);
    $('#CddFullName').val(cdd.CddFullName);
    $('#CddUserName').val(cdd.CddUserName);
    $('#CddAge').val(cdd.CddAge);
    $('#CP_ProfessionId option[value=' + cdd.CP_ProfessionId + ']').attr('selected', 'selected');
    $('#CddSex option[value=' + cdd.CddSex + ']').attr('selected', 'selected');
    $('#CP_SalaryId option[value=' + cdd.CP_SalaryId + ']').attr('selected', 'selected');
    $('#CP_LevelId option[value=' + cdd.CP_LevelId + ']').attr('selected', 'selected');
    $('#CP_WorkTypeId option[value=' + cdd.CP_WorkTypeId + ']').attr('selected', 'selected');
    $('#CP_ExperienceId option[value=' + cdd.CP_ExperienceId + ']').attr('selected', 'selected');
    $("textarea#CPExperience").val(cdd.CPExperience);
    $("textarea#CddAbout").val(cdd.CddAbout);
    $('#CddPhone').val(cdd.CddPhone);
    $('#CddEmail').val(cdd.CddEmail);
    $('#CddFacebook').val(cdd.CddFacebook);
    $('#CddGoogle').val(cdd.CddGoogle);
    $('#Cdd_CityId').val(cdd.Cdd_CityId);
    $('#CPStatus option[value=' + cdd.CPStatus + ']').attr('selected', 'selected');
}


//Create new
$("#CreateNew").validate({

    rules: {
    },
    messages: {
    },
    submitHandler: function (form) {
        var type = $('.updatenew').attr('id').substring(4);
        var idNew = $('#IdNew').val();
        CreateNew(type, idNew);
    }
});
function CreateNew(type, id) {
    var newdto = {
        NTitle: $('#NTitle').val(),
        NQuote: $('#NQuote').val(),
        NDetail: $('#NDetail').summernote('code'),
        NAvatar: $('#avatarnew').attr('src'),
        NType: $('#NType').val(),
        N_CategoryId: $('#N_CategoryId').val(),
        Nstatus: $('#Nstatus').val()
    };
    if (type == 2) {
        newdto.NewsId = id;
    }
    $.ajax({
        type: "POST",
        url: '/Admin/NewManagement/CUNew',
        data: { ndto: newdto, typePost: type },
        success: function (result) {
            $.notify(result.message, result.status);
            if (result.status == "success") {
                window.setTimeout(function () { location.reload(); }, 500);
            }
        },
        error: function (ex) {
            $.notify("Đăng không thành công!", "error");
            return false;
        }
    });
}

//up avatar image new
$('#image_uploads').on('change', function () {
    //Lấy dữ liệu trên fileUpload   
    var files = $(this)[0].files;
    // Đối tượng formdata
    var formData = new FormData();
    formData.append('file', files[0]);
    $.ajax({
        type: 'POST',
        url: '/Admin/NewManagement/UploadFileImageNew',
        contentType: false,
        processData: false,
        data: formData,
        success: function (urlImage) {
            $("#avatarnew").attr("src", urlImage);
            $('.imagenew img').css('display', 'inline-block');
        },
        error: function (err) {
            alert("Có lỗi!");
        }
    });
});

//delete new
var idNew;
//Update profile candidate
$('.btn-del-new').click(function () {
    idNew = parseInt($(this).attr('input-id-new'));
    $('#namedelete').text($('#new' + idNew).text());
});

$('#btn-yes-delete-new').click(function () {
    $.ajax({
        type: "POST",
        url: '/Admin/NewManagement/DeleteNew',
        data: { idNew: idNew },
        success: function (result) {
            $.notify(result.message, result.status);
            $('#exampleModalCenter').css('display', 'none');
            $('.modal-backdrop').remove();
            if (result.status == "success") {
                window.setTimeout(function () { location.reload(); }, 500);
            }
        },
        error: function (ex) {
            $.notify("Lấy thông tin không thành công!", "error");
            return false;
        }
    });
});


//create new candidate
//update profile candidate
$("#updaterecruit-form").validate({
    rules: {
        RIFullName: {
            required: true
        },
        FounedYearString: {
            required: true
        },
        RIAbout: {
            required: true
        },
        RIPhone: {
            required: true
        },
        RIEmail: {
            required: true
        },
        RIPassword: {
            required: true,
            minlength: 6
        },
        RIPasswordConfirm: {
            equalTo: "#RIPassword"
        },
        RI_CityId: {
            selectRequired: ""
        },
        RI_ProfessionId: {
            selectRequired: ""
        },
        RI_CompanySizeId: {
            selectRequired: ""
        },
        RIAddress: {
            required: true
        }
        //RJ_SalaryId: {
        //    selectRequired: null
        //}
    },
    messages: {
        RIPassword: {
            minlength: "Mật khẩu ít nhất 6 ký tự"
        },
        RIPasswordConfirm: {
            equalTo: "Mật khẩu không trùng khớp"
        },
    },
    submitHandler: function (form) {
        //type 1: Thêm, type 2: Sửa
        var type = $('.updaterecruit').attr('id').substring(8);
        UpdateRecruit(type);
    }
});

//update profile in /Candidate/CandidateProfile 
function UpdateRecruit(type) {
    var formData = new FormData();
    var bg = $('#bg-recruit').css('background-image');
    bg = bg.replace('url(', '').replace(')', '').replace(/\"/gi, "");
    var rcdto = {
        RIFullName: $('#RIFullName').val(), //
        FoundedYear: $('#FounedYearString').val(), //
        RI_CompanySizeId: $('#RI_CompanySizeId').val(), //
        RI_ProfessionId: $('#RI_ProfessionId').val(), //
        RIAbout: $("textarea#RIAbout").val(), //
        RIStatus: $('#RIStatus').val(), //
        RIPhone: $("#RIPhone").val(), //
        RIEmail: $("#RIEmail").val(), //
        RIWebsite: $("#RIWebsite").val(), //
        RI_CityId: $("#RI_CityId").val(), //
        RI_DistrictId: $("#RI_DistrictId").val(), //
        RI_WardId: $('#RI_WardId').val(), //
        RIAddress: $("#RIAddress").val(), //
        RIAvatar: $('#avatarlink').attr('src'),
        RICoverImage: bg,
        RILogo: $('#logocompany').attr('src')
    };

    if (type == 1) {
        rcdto.RIPassword = $('#RIPassword').val();
        rcdto.RIUserName = $('#RIUserName').val();
    } else {
        rcdto.RecruitId = $('#RecruitId').val();

    }
    // Chuyển dữ liệu sang dạng dataform
    for (var item in rcdto) {
        formData.append(item, rcdto[item]);
    }
    //Lấy dữ liệu trên fileUpload

    formData.append("typeMember", "employerregister");

    if (type == 2) {
        $.ajax({
            type: "POST",
            url: '/Admin/RecruitManagement/UpdateRecruit',
            data: {
                rcdto: rcdto, type: type
            },
            success: function (result) {
                $.notify(result.message, result.status);
                //if (result.status == "success") {
                //    $('#updaterecruit-form')[0].reset();
                //}
            },
            error: function (ex) {
                $.notify("Cập nhật không thành công!", "error");
                return false;
            }
        });
    }
    else {
        $.ajax({
            type: "POST",
            url: '/Admin/AccountManage/Register',
            data: formData,
            contentType: false,
            processData: false,
            success: function (result) {
                $.notify(result.message, result.status);
                if (result.status == "success") {
                    $('#updatecandidate-form')[0].reset();
                }
            },
            error: function (ex) {
                $.notify("Tạo không thành công!", "error");
                return false;
            }
        });
    }

}

var idRecruit;
//Update profile candidate
$('.btn-del-rc').click(function () {
    idRecruit = parseInt($(this).attr('input-id-rc'));
    var username = $(this).parent().siblings(".username")[0].innerText;
    $('#namedelete').text(username);

});
$('#btn-yes-delete-rc').click(function () {
    $.ajax({
        type: "POST",
        url: '/Admin/RecruitManagement/DeleteRecruit',
        data: { idRecruit: idRecruit },
        success: function (result) {
            $.notify(result.message, result.status);
            $('#exampleModalCenter').css('display', 'none');
            $('.modal-backdrop').remove();
            if (result.status == "success") {
                window.setTimeout(function () { location.reload(); }, 500);
            }
        },
        error: function (ex) {
            $.notify("Lấy thông tin không thành công!", "error");
            return false;
        }
    });
});

//bg recruit
var BgRecruit;
$f2 = $('#updatelogo');
function ChangeLogo() {
    if ($(this)[0].files.length > 0) {
        var capacity = (this.files[0].size / 1024 / 1024).toFixed(3);
        var NameFile = $(this)[0].files[0].name;
        if (ValidateExtentionImage(NameFile) == false) {
            $.notify("Vui lòng chọn hình ảnh đúng định dạng .jpg,.png,.jpeg,.gif", "error");
        }
        else if (parseInt(capacity) > 2) {
            checkTM = false;
            $.notify("Dung lượng tối đa là 2MB. Hãy thử lại", "error");
        } else {
            GetLinkImageCover($(this), function (url) {
                $('#logocompany').attr("src", url);
            });
        }
    }
}
$f2.change(ChangeLogo);

//validate extetion image
function ValidateExtentionImage(extention) {
    if (extention != '') {
        var checkExtention = extention.toLowerCase();
        if (!checkExtention.match(/(\.jpg|\.png|\.jpeg|\.gif)$/)) {
            return false;
        }
    }
    return true;
}

function GetLinkImageCover(btn, callback) {
    //Lấy dữ liệu trên fileUpload   
    var files = $(btn)[0].files;
    // Đối tượng formdata
    var formData = new FormData();
    formData.append('file', files[0]);
    $.ajax({
        type: 'POST',
        url: '/Common/UploadFileImage',
        contentType: false,
        processData: false,
        data: formData,
        success: function (urlImage) {
            BgRecruit = urlImage;
            callback(urlImage);
            //$('#bg-recruit').css('background-image', 'url(' + urlImage + ')');
        },
        error: function (err) {
            $.notify("Có lỗi " + err + " khi tải ảnh lên", "error");
        }
    });
}

//upload avatar
var $f1 = $("#updatecover");
function ChangeCover() {
    if ($(this)[0].files.length > 0) {
        var capacity = (this.files[0].size / 1024 / 1024).toFixed(3);
        var NameFile = $(this)[0].files[0].name;
        if (ValidateExtentionImage(NameFile) == false) {
            $.notify("Vui lòng chọn hình ảnh đúng định dạng .jpg,.png,.jpeg,.gif", "error");
        }
        else if (parseInt(capacity) > 2) {
            checkTM = false;
            $.notify("Dung lượng tối đa là 2MB. Hãy thử lại", "error");
        } else {
            GetLinkImageCover($(this), function (url) {
                $('#bg-recruit').css('background-image', 'url(' + url + ')');
            });
        }
    }
}
$f1.change(ChangeCover);

///created by: 3F.Dat 19012019
//upload avatar
var $fa = $("#updateavatar");

$('.avatar-fix').click(function () {
    //$fa.change(ChangeAvatar);
    $(this).parent().next().click();
});
function ChangeAvatar() {
    if ($(this)[0].files.length > 0) {
        var capacity = (this.files[0].size / 1024 / 1024).toFixed(3);
        var NameFile = $(this)[0].files[0].name;
        if (ValidateExtentionImage(NameFile) == false) {
            $.notify("Vui lòng chọn hình ảnh đúng định dạng .jpg,.png,.jpeg,.gif", "error");
        }
        else if (parseInt(capacity) > 2) {
            checkTM = false;
            $.notify("Dung lượng tối đa là 2MB. Hãy thử lại", "error");
        } else {
            GetLinkImageAvatar($(this));
        }
    }
};
$fa.change(ChangeAvatar);

function GetLinkImageAvatar(btn) {
    //Lấy dữ liệu trên fileUpload   
    var files = $(btn)[0].files;
    // Đối tượng formdata
    var formData = new FormData();
    formData.append('file', files[0]);
    $.ajax({
        type: 'POST',
        url: '/Admin/DashBoard/UploadFileImage',
        contentType: false,
        processData: false,
        data: formData,
        success: function (urlImage) {
            $('#avatarlink').attr("src", urlImage);
        },
        error: function (err) {
            alert("Có lỗi!");
        }
    });
};

//book advert
//upload avatar
var $fbook = $("#image_uploads_book");
function BookImage() {
    if ($(this)[0].files.length > 0) {
        var capacity = (this.files[0].size / 1024 / 1024).toFixed(3);
        var NameFile = $(this)[0].files[0].name;
        if (ValidateExtentionImage(NameFile) == false) {
            $.notify("Vui lòng chọn hình ảnh đúng định dạng .jpg,.png,.jpeg,.gif", "error");
        }
        else if (parseInt(capacity) > 2) {
            checkTM = false;
            $.notify("Dung lượng tối đa là 2MB. Hãy thử lại", "error");
        } else {
            GetLinkImageCover($(this), function (url) {
                $('#bookimage').attr("src", url);
            });
        }
    }
}
$fbook.change(BookImage);

$("#advertisement-form").validate({
    rules: {
        AdStartDate: {
            required: true
        },
        AdEndDate: {
            required: true
        },
        AdPosition: {
            required: true
        },
        AdName: {
            required: true
        },
        AdPhone: {
            required: true,
            number: true,
            minlength: 10,
            maxlength: 11,
            regex: /(^\(\+\84\)\s\d{3}\s\d{3}$)|(^\d{10}$)|(^\d{4}\s\d{3}\s\d{3}$)|(^\(\+\84\)\d{3}\d{3}$)/,
        },
        AdEmail: {
            required: true,
            regex: /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
        },
        AdAddress: {
            required: true
        },
        AdLink: {
            required: true,
            regex: /^(http|https)?:\/\/[a-zA-Z0-9-\.]+\.[a-z]{2,4}/
        }
        //image_uploads: {
        //    required: true
        //}
    },
    messages: {
    },
    submitHandler: function (form) {
        //type 1: Thêm, type 2: Sửa
        var type = $('.updateadvertisement').attr('id').substring(14);
        BookAdvertisement(type);
    }
});
function BookAdvertisement(type) {
    var formData = new FormData();
    var addto = {
        AdStartDateString: $('#AdStartDateString').val(), //
        AdEndDateString: $('#AdEndDateString').val(), //
        AdPosition: $('#AdPosition').val(), //
        AdStatus: $('#AdStatus').val(), //
        AdName: $("#AdName").val(), //
        AdPhone: $('#AdPhone').val(), //
        AdEmail: $("#AdEmail").val(), //
        AdAddress: $("#AdAddress").val(), //
        AdLink: $("#AdLink").val(), //
        AdImage: $('#bookimage').attr('src') //
    };

    if (type == 2) {
        addto.AdvertisementId = $('#AdvertisementId').val();
    }
    // Chuyển dữ liệu sang dạng dataform
    for (var item in addto) {
        formData.append(item, addto[item]);
    }
    //Lấy dữ liệu trên fileUpload

    formData.append("typebook", parseInt(type));

    //if (type == 2) {
    //    $.ajax({
    //        type: "POST",
    //        url: '/Admin/AccountManage/BookNewAdvertisement',
    //        data: {
    //            rcdto: rcdto, type: type
    //        },
    //        success: function (result) {
    //            $.notify(result.message, result.status);
    //            //if (result.status == "success") {
    //            //    $('#updaterecruit-form')[0].reset();
    //            //}
    //        },
    //        error: function (ex) {
    //            $.notify("Cập nhật không thành công!", "error");
    //            return false;
    //        }
    //    });
    //}
    //else {
    $.ajax({
        type: "POST",
        url: '/Admin/Advertisement/UpdateAdvertisement',
        data: formData,
        contentType: false,
        processData: false,
        success: function (result) {
            $.notify(result.message, result.status);
            if (result.status == "success") {
                window.setTimeout(function () { location.reload(); }, 500);
            }
        },
        error: function (ex) {
            $.notify("Tạo không thành công!", "error");
            return false;
        }
    });
    //}
}

//post job
$("#admin-updatepost-form").validate({
    rules: {
        RJTitle: {
            required: true
        },
        RJPosition: {
            required: true
        },
        RJ_LevelId: {
            required: true
        },
        RJ_SalaryId: {
            required: true
        },
        RJ_WorkTypeId: {
            required: true
        },
        RJ_ExperienceId: {
            required: true
        },
        RJAmount: {
            required: true
        },
        RJExpirationDate: {
            required: true
        },
        RJ_Describe: {
            required: true
        },
        RJ_Require: {
            required: true
        },
        RJBenefit: {
            required: true
        },
        RJPhoneContact: {
            required: true,
            number: true,
            minlength: 10,
            maxlength: 11,
            regex: /(^\(\+\84\)\s\d{3}\s\d{3}$)|(^\d{10}$)|(^\d{4}\s\d{3}\s\d{3}$)|(^\(\+\84\)\d{3}\d{3}$)/,
        },
        RJEmailContact: {
            required: true,
            regex: /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
        },
        RJNameContact: {
            required: true
        },
        RJCityId: {
            required: true
        },
        RJDistrictId: {
            required: true
        },
        RJ_WardId: {
            required: true
        },
        RJ_WorkPlace: {
            required: true
        }
    },
    messages: {
        RJEmailContact: {
            email: "Hãy nhập đúng định dạng email"
        },
        RJPhoneContact: {
            regex: "Hãy nhập đúng định dạng điện thoại"
        },
    },
    submitHandler: function (form) {
        //type 1: Thêm, type 2: Sửa
        var type = $('.updatejob').attr('id').substring(8);
        UpdateJob(type);
    }
});
function UpdateJob(typeFix) {
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
        RJExpirationDateString: $("#RJExpirationDateString").val(),
        RJEmailContact: $("#RJEmailContact").val(),
        RJPhoneContact: $("#RJPhoneContact").val(),
        RJNameContact: $("#RJNameContact").val(),
        RJ_ExperienceId: $("#RJ_ExperienceId").children("option:selected").val(),
        RJType: $("#RJType").children("option:selected").val(),
        RJStatus: $("#RJStatus").children("option:selected").val(),
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
        url: '/Admin/RecruitJobManagement/PostANewJob',
        data: { recruitjobdto: recruitjobdto, typeFix: parseInt(typeFix) },
        success: function (result) {
            $.notify(result.message, result.status);
            if (result.status == "success") {
                window.setTimeout(function () { location.reload(); }, 500);
            }
        },
        error: function (ex) {
            $.notify("Không thành công! Thử lại sau", "error");
            return false;
        }
    });
}
var IDJob;
//click delete job
$('.btn-del-job').click(function () {
    IDJob = $(this).attr('input-id-job');
    $('#namedelete').text($('#titlejob' + IDJob).text());
});

$('.btndeletejob').click(function () {
    $.ajax({
        type: "POST",
        url: '/Admin/RecruitJobManagement/DeleteJob',
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

//create account
$("#admin-createaccount-form").validate({
    rules: {
        WIFullName: {
            required: true
        },
        WIUserName: {
            required: true
        },
        WIAddress: {
            required: true
        },
        WIBirthDay: {
            required: true
        },
        WIPhoneNumber: {
            required: true,
            number: true,
            minlength: 10,
            maxlength: 11,
            regex: /(^\(\+\84\)\s\d{3}\s\d{3}$)|(^\d{10}$)|(^\d{4}\s\d{3}\s\d{3}$)|(^\(\+\84\)\d{3}\d{3}$)/,
        },
        WIEmail: {
            required: true,
            regex: /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
        },
        WIPassword: {
            required: true,
            minlength: 6
        },
        WIConfirmPassword: {
            equalTo: "#WIPassword"
        }
    },
    messages: {
        RJEmailContact: {
            email: "Hãy nhập đúng định dạng email"
        },
        RJPhoneContact: {
            regex: "Hãy nhập đúng định dạng điện thoại"
        },
        WIPassword: {
            minlength: "Mật khẩu ít nhất 6 ký tự"
        },
        WIConfirmPassword: {
            equalTo: "Mật khẩu không trùng khớp"
        }
    },
    submitHandler: function (form) {
        //type 1: Thêm, type 2: Sửa
        var type = $('.updateaccount').attr('id').substring(8);
        UpdateAccount(type);
    }
});
function UpdateAccount(typeFix) {
    var accountdto = {
        WIUserName: $("#WIUserName").val(),
        WIEmail: $("#WIEmail").val(),
        WIPassword: $("#WIPassword").val(),
        WIFullName: $('#WIFullName').val(),
        WIPhoneNumber: $("#WIPhoneNumber").val(),
        WIAddress: $("#WIAddress").val(),
        WIType: $("#WIType").children("option:selected").val(),
        WIGender: $("#WIGender").children("option:selected").val(),
        WIBirthdayString: $('#WIBirthdayString').val(),
        WIStatus: $("#WIStatus").children("option:selected").val(),
        WIPosition: $("#WIPosition").children("option:selected").val()
    };
    //type: 1-thêm, 2-sửa
    if (typeFix == 2) {
        accountdto.WebmasterInfoId = $('#WebmasterInfoId').val();
        accountdto.WI_AspNetUserId = $('#WI_AspNetUserId').val();
    }
    $.ajax({
        type: "POST",
        url: '/Admin/AccountManage/UpdateAccount',
        data: { accountdto: accountdto, typeFix: parseInt(typeFix) },
        success: function (result) {
            $.notify(result.message, result.status);
            if (result.status == "success") {
                window.setTimeout(function () { location.reload(); }, 500);
            }
        },
        error: function (ex) {
            $.notify("Không thành công! Thử lại sau", "error");
            return false;
        }
    });
}


var ix = 0;
var idAspNetUser;
//phân quyền
$('.userid').unbind().click(function () {
    var thisbtn = $(this);
    //thisbtn.bind('click', ClickPQ(thisbtn));
    idAspNetUser = thisbtn.attr('userid');
    //thisbtn.off('click');
    ClickPQ(thisbtn);
    //thisbtn.unbind('click');
    //ix++;
    //if (ix == 1) {
    //    //ClickPQ(thisbtn);
    //    thisbtn.bind('click', ClickPQ(thisbtn));
    //}
});

function ClickPQ(btn) {
    //btn.on('click');
    ix = 0;
    $('#role_table .checkboxpq').removeClass('readonly');
    $('#role_table .checkboxpq').prop('checked', false);
    var idUser = btn.attr('UserId');
    $.ajax({
        type: "POST",
        url: '/Admin/AccountManage/GetListDecentralization',
        data: { idUser: idUser },
        success: function (result) {
            $.each(result, function (key, value) {
                $('#check_' + value).prop('checked', true);
            });
        },
        error: function (ex) {
            $.notify("Không thành công! Thử lại sau", "error");
            return false;
        }
    });
}

$("#submitDecentralization").click(function () {
    var roles = $('#role_table').find('input');
    var listroles = [];
    $.each(roles, function () {
        if (this.checked) {
            listroles.push(this.value);
        }
    });
    
    $.ajax({
        type: "POST",
        url: '/Admin/AccountManage/UpdateDecentralization',
        data: { idAspNetUser: idAspNetUser, listroles: listroles },
        success: function (result) {
            console.log(result);
            if (result == 1) {
                $.notify("Phân quyền thành công", "success");
            } else {
                $.notify("Không thành công! Thử lại sau", "error");
            }
        },
        error: function (ex) {
            $.notify("Không thành công! Thử lại sau", "error");
            return false;
        }
    });
});


//change password account
$("#admin-updatepass-form").validate({
    rules: {
        WIOldPassword: {
            required: true,
            minlength: 6
        },
        WIPassword: {
            required: true,
            minlength: 6
        },
        WIConfirmPassword: {
            equalTo: "#WIPassword"
        }
    },
    messages: {
        WIOldPassword: {
            minlength: "Mật khẩu ít nhất 6 ký tự"
        },
        WIPassword: {
            minlength: "Mật khẩu ít nhất 6 ký tự"
        },
        WIConfirmPassword: {
            equalTo: "Mật khẩu không trùng khớp"
        }
    },
    submitHandler: function (form) {
        var oldPass = $('#WIOldPassword').val();
        var newPass = $('#WIPassword').val();
        var idUser = $('#WI_AspNetUserId').val();
        $.ajax({
            type: "POST",
            url: '/Admin/AccountManage/ChangePassword',
            data: { idUser: idUser, oldPass: oldPass, newPass: newPass },
            success: function (result) {
                $.notify(result.message, result.status);
                if (result.status == "success") {
                    window.setTimeout(function () { location.reload(); }, 500);
                }
            },
            error: function (ex) {
                $.notify("Không thành công! Thử lại sau", "error");
                return false;
            }
        });
    }
});

//delete account
var idwebmt;
$('.btn-del-webmt').click(function () {
    idwebmt = parseInt($(this).attr('input-id-webmt'));
    $('#namedelete').text($('#username' + idwebmt).text());

});

$('#btn-yes-delete-webmt').click(function () {
    $.ajax({
        type: "POST",
        url: '/Admin/AccountManage/DeleteAccount',
        data: { idwebmt: idwebmt },
        success: function (result) {
            $.notify(result.message, result.status);
            $('#exampleModalCenter').css('display', 'none');
            $('.modal-backdrop').remove();
            if (result.status == "success") {
                window.setTimeout(function () { location.reload(); }, 500);
            }
        },
        error: function (ex) {
            $.notify("Lấy thông tin không thành công!", "error");
            return false;
        }
    });
});


//delete advertisment
var idad;
$('.btn-del-ad').click(function () {
    idad = parseInt($(this).attr('input-id-ad'));

});

$('#btn-yes-delete-ad').click(function () {
    $.ajax({
        type: "POST",
        url: '/Admin/Advertisement/DeleteAdvertisement',
        data: { idad: idad },
        success: function (result) {
            $.notify(result.message, result.status);
            $('#exampleModalCenter').css('display', 'none');
            $('.modal-backdrop').remove();
            if (result.status == "success") {
                window.setTimeout(function () { location.reload(); }, 500);
            }
        },
        error: function (ex) {
            $.notify("Lấy thông tin không thành công!", "error");
            return false;
        }
    });
});


//export excel
$(document).on('click', ".exportexcel--btn", function () {
    var keyWord = $('#example_filter > label > input').val();
    var controller = $(this).attr('id');
    var status = getUrlParameter('status');
    $('.valuesearch').val(keyWord);
    $('.valuestatus').val(status);
    $("#ExportExcel").trigger("submit");
});

var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
        }
    }
};