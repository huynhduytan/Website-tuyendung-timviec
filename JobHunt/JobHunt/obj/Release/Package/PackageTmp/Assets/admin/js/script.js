var App ={checkLickPage: true}; $(document).ready(function(){
function Login() {
    var a = {
        Email: $("#EmailLogin").val(),
        Password: $("#PassLogin").val(),
        RememberMe: $("#RememberMe").is(":checked")
    }, b = !0;
    "" != a.Email && "" != a.Password || (b = !1, $.notify("Hãy nhập đủ thông tin", "error")), 
    b && $.ajax({
        type: "POST",
        url: "/AccountManage/Login",
        data: AddAntiForgeryToken({
            model: a
        }),
        success: function(a) {
            $.notify(a.message, a.status), "success" == a.status && window.setTimeout(function() {
                location.href = "/Admin/DashBoard";
            }, 300);
        },
        error: function(a) {
            return $.notify("Đăng nhập không thành công!", "error"), !1;
        }
    });
}

function UpdateProfileCandidate(a) {
    var b = !1, c = new FormData();
    Avatar = $("#avatarlink").attr("src");
    var d = {
        CddFullName: $("#CddFullName").val(),
        CddAge: $("#CddAge").val(),
        CddSex: $("#CddSex").val(),
        CP_ProfessionId: $("#CP_ProfessionId option:selected").val(),
        CddAbout: $("textarea#CddAbout").val(),
        CP_SalaryId: $("#CP_SalaryId option:selected").val(),
        CP_ExperienceId: $("#CP_ExperienceId option:selected").val(),
        CP_LevelId: $("#CP_LevelId option:selected").val(),
        CP_WorkTypeId: $("#CP_WorkTypeId option:selected").val(),
        CPExperience: $("#CPExperience").val(),
        CPStatus: $("#CPStatus option:selected").val(),
        CddPhone: $("#CddPhone").val(),
        CddEmail: $("#CddEmail").val(),
        CddFacebook: $("#CddFacebook").val(),
        CddGoogle: $("#CddGoogle").val(),
        Cdd_CityId: $("#Cdd_CityId option:selected").val(),
        Cdd_DistrictId: $("#Cdd_DistrictId option:selected").val(),
        Cdd_WardId: $("#Cdd_WardId option:selected").val(),
        CddAddress: $("#CddAddress").val(),
        CddDescribeCV: $("textarea#CddDescribeCV").val(),
        CPAvatar: Avatar
    };
    1 == a ? (d.CddUserName = $("#CddUserName").val(), d.CddPassword = $("#CddPassword").val()) : 2 == a && (d.CandidateId = $("#CandidateId").val());
    for (var e in d) c.append(e, d[e]);
    var f = $("#fileCV")[0].files;
    c.append("typeMember", "candidateregister"), c.append("fileCV", f[0]), 1 == a && void 0 == f[0] ? ($.notify("Hãy chọn file để tải lên", "error"), 
    b = !1) : b = !0, b && (1 == a ? $.ajax({
        type: "POST",
        url: "/Admin/AccountManage/Register",
        data: c,
        contentType: !1,
        processData: !1,
        success: function(a) {
            $.notify(a.message, a.status), "success" == a.status && $("#updatecandidate-form")[0].reset();
        },
        error: function(a) {
            return $.notify("Tạo không thành công!", "error"), !1;
        }
    }) : $.ajax({
        type: "POST",
        url: "/Admin/CandidateManagement/UpdateProfileCandidate",
        data: c,
        contentType: !1,
        processData: !1,
        success: function(a) {
            $.notify(a.message, a.status);
        },
        error: function(a) {
            return $.notify("Cập nhật không thành công!", "error"), !1;
        }
    }));
}

function dnChange() {
    if ($(this)[0].files.length > 0) {
        var a = $(this)[0].files[0].name;
        ValidateFile(a) ? $(".namefile").text(a) : $(".uploadfilecv").val("");
    }
}

function ValidateFile(a) {
    if ("" != a) {
        if (!a.toLowerCase().match(/(\.pdf|\.docx|\.doc)$/)) return $.notify("Vui lòng chọn file đúng định dạng", "error"), 
        !1;
    }
    return !0;
}

function SetValueForCandidate(a) {
    CandidateId = a.CandidateId, $("#namecddupdate").text(a.CddUserName), $("#CddFullName").val(a.CddFullName), 
    $("#CddUserName").val(a.CddUserName), $("#CddAge").val(a.CddAge), $("#CP_ProfessionId option[value=" + a.CP_ProfessionId + "]").attr("selected", "selected"), 
    $("#CddSex option[value=" + a.CddSex + "]").attr("selected", "selected"), $("#CP_SalaryId option[value=" + a.CP_SalaryId + "]").attr("selected", "selected"), 
    $("#CP_LevelId option[value=" + a.CP_LevelId + "]").attr("selected", "selected"), 
    $("#CP_WorkTypeId option[value=" + a.CP_WorkTypeId + "]").attr("selected", "selected"), 
    $("#CP_ExperienceId option[value=" + a.CP_ExperienceId + "]").attr("selected", "selected"), 
    $("textarea#CPExperience").val(a.CPExperience), $("textarea#CddAbout").val(a.CddAbout), 
    $("#CddPhone").val(a.CddPhone), $("#CddEmail").val(a.CddEmail), $("#CddFacebook").val(a.CddFacebook), 
    $("#CddGoogle").val(a.CddGoogle), $("#Cdd_CityId").val(a.Cdd_CityId), $("#CPStatus option[value=" + a.CPStatus + "]").attr("selected", "selected");
}

function CreateNew(a, b) {
    var c = {
        NTitle: $("#NTitle").val(),
        NQuote: $("#NQuote").val(),
        NDetail: $("#NDetail").summernote("code"),
        NAvatar: $("#avatarnew").attr("src"),
        NType: $("#NType").val(),
        N_CategoryId: $("#N_CategoryId").val(),
        Nstatus: $("#Nstatus").val()
    };
    2 == a && (c.NewsId = b), $.ajax({
        type: "POST",
        url: "/Admin/NewManagement/CUNew",
        data: {
            ndto: c,
            typePost: a
        },
        success: function(a) {
            $.notify(a.message, a.status), "success" == a.status && window.setTimeout(function() {
                location.reload();
            }, 500);
        },
        error: function(a) {
            return $.notify("Đăng không thành công!", "error"), !1;
        }
    });
}

function UpdateRecruit(a) {
    var b = new FormData(), c = $("#bg-recruit").css("background-image");
    c = c.replace("url(", "").replace(")", "").replace(/\"/gi, "");
    var d = {
        RIFullName: $("#RIFullName").val(),
        FoundedYear: $("#FounedYearString").val(),
        RI_CompanySizeId: $("#RI_CompanySizeId").val(),
        RI_ProfessionId: $("#RI_ProfessionId").val(),
        RIAbout: $("textarea#RIAbout").val(),
        RIStatus: $("#RIStatus").val(),
        RIPhone: $("#RIPhone").val(),
        RIEmail: $("#RIEmail").val(),
        RIWebsite: $("#RIWebsite").val(),
        RI_CityId: $("#RI_CityId").val(),
        RI_DistrictId: $("#Cdd_DistrictId").val(),
        RI_WardId: $("#Cdd_WardId").val(),
        RIAddress: $("#RIAddress").val(),
        RIAvatar: $("#avatarlink").attr("src"),
        RICoverImage: c,
        RILogo: $("#logocompany").attr("src")
    };
    1 == a ? (d.RIPassword = $("#RIPassword").val(), d.RIUserName = $("#RIUserName").val()) : d.RecruitId = $("#RecruitId").val();
    for (var e in d) b.append(e, d[e]);
    b.append("typeMember", "employerregister"), 2 == a ? $.ajax({
        type: "POST",
        url: "/Admin/RecruitManagement/UpdateRecruit",
        data: {
            rcdto: d,
            type: a
        },
        success: function(a) {
            $.notify(a.message, a.status);
        },
        error: function(a) {
            return $.notify("Cập nhật không thành công!", "error"), !1;
        }
    }) : $.ajax({
        type: "POST",
        url: "/Admin/AccountManage/Register",
        data: b,
        contentType: !1,
        processData: !1,
        success: function(a) {
            $.notify(a.message, a.status), "success" == a.status && $("#updatecandidate-form")[0].reset();
        },
        error: function(a) {
            return $.notify("Tạo không thành công!", "error"), !1;
        }
    });
}

function ChangeLogo() {
    if ($(this)[0].files.length > 0) {
        var a = (this.files[0].size / 1024 / 1024).toFixed(3);
        0 == ValidateExtentionImage($(this)[0].files[0].name) ? $.notify("Vui lòng chọn hình ảnh đúng định dạng .jpg,.png,.jpeg,.gif", "error") : parseInt(a) > 2 ? (checkTM = !1, 
        $.notify("Dung lượng tối đa là 2MB. Hãy thử lại", "error")) : GetLinkImageCover($(this), function(a) {
            $("#logocompany").attr("src", a);
        });
    }
}

function ValidateExtentionImage(a) {
    if ("" != a) {
        if (!a.toLowerCase().match(/(\.jpg|\.png|\.jpeg|\.gif)$/)) return !1;
    }
    return !0;
}

function GetLinkImageCover(a, b) {
    var c = $(a)[0].files, d = new FormData();
    d.append("file", c[0]), $.ajax({
        type: "POST",
        url: "/Common/UploadFileImage",
        contentType: !1,
        processData: !1,
        data: d,
        success: function(a) {
            BgRecruit = a, b(a);
        },
        error: function(a) {
            $.notify("Có lỗi " + a + " khi tải ảnh lên", "error");
        }
    });
}

function ChangeCover() {
    if ($(this)[0].files.length > 0) {
        var a = (this.files[0].size / 1024 / 1024).toFixed(3);
        0 == ValidateExtentionImage($(this)[0].files[0].name) ? $.notify("Vui lòng chọn hình ảnh đúng định dạng .jpg,.png,.jpeg,.gif", "error") : parseInt(a) > 2 ? (checkTM = !1, 
        $.notify("Dung lượng tối đa là 2MB. Hãy thử lại", "error")) : GetLinkImageCover($(this), function(a) {
            $("#bg-recruit").css("background-image", "url(" + a + ")");
        });
    }
}

function ChangeAvatar() {
    if ($(this)[0].files.length > 0) {
        var a = (this.files[0].size / 1024 / 1024).toFixed(3);
        0 == ValidateExtentionImage($(this)[0].files[0].name) ? $.notify("Vui lòng chọn hình ảnh đúng định dạng .jpg,.png,.jpeg,.gif", "error") : parseInt(a) > 2 ? (checkTM = !1, 
        $.notify("Dung lượng tối đa là 2MB. Hãy thử lại", "error")) : GetLinkImageAvatar($(this));
    }
}

function GetLinkImageAvatar(a) {
    var b = $(a)[0].files, c = new FormData();
    c.append("file", b[0]), $.ajax({
        type: "POST",
        url: "/Admin/DashBoard/UploadFileImage",
        contentType: !1,
        processData: !1,
        data: c,
        success: function(a) {
            $("#avatarlink").attr("src", a);
        },
        error: function(a) {
            alert("Có lỗi!");
        }
    });
}

function BookImage() {
    if ($(this)[0].files.length > 0) {
        var a = (this.files[0].size / 1024 / 1024).toFixed(3);
        0 == ValidateExtentionImage($(this)[0].files[0].name) ? $.notify("Vui lòng chọn hình ảnh đúng định dạng .jpg,.png,.jpeg,.gif", "error") : parseInt(a) > 2 ? (checkTM = !1, 
        $.notify("Dung lượng tối đa là 2MB. Hãy thử lại", "error")) : GetLinkImageCover($(this), function(a) {
            $("#bookimage").attr("src", a);
        });
    }
}

function BookAdvertisement(a) {
    var b = new FormData(), c = {
        AdStartDateString: $("#AdStartDateString").val(),
        AdEndDateString: $("#AdEndDateString").val(),
        AdPosition: $("#AdPosition").val(),
        AdStatus: $("#AdStatus").val(),
        AdName: $("#AdName").val(),
        AdPhone: $("#AdPhone").val(),
        AdEmail: $("#AdEmail").val(),
        AdAddress: $("#AdAddress").val(),
        AdLink: $("#AdLink").val(),
        AdImage: $("#bookimage").attr("src")
    };
    2 == a && (c.AdvertisementId = $("#AdvertisementId").val());
    for (var d in c) b.append(d, c[d]);
    b.append("typebook", parseInt(a)), $.ajax({
        type: "POST",
        url: "/Admin/Advertisement/UpdateAdvertisement",
        data: b,
        contentType: !1,
        processData: !1,
        success: function(a) {
            $.notify(a.message, a.status), "success" == a.status && window.setTimeout(function() {
                location.reload();
            }, 500);
        },
        error: function(a) {
            return $.notify("Tạo không thành công!", "error"), !1;
        }
    });
}

function UpdateJob(a) {
    var b = {
        RJTitle: $("#RJTitle").val(),
        RJPosition: $("#RJPosition").val(),
        RJAddress: $("#RJAddress").val(),
        RJAmount: $("#RJAmount").val(),
        RJ_ProfessionId: $("#RJ_ProfessionId").children("option:selected").val(),
        RJ_LevelId: $("#RJ_LevelId").children("option:selected").val(),
        RJ_Require: $("#RJ_Require").summernote("code"),
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
        RJ_Describe: $("#RJ_Describe").summernote("code"),
        RJBenefit: $("#RJBenefit").summernote("code")
    };
    2 == a && (b.RecruitJobId = $("#RecruitJobId").val()), $.ajax({
        type: "POST",
        url: "/Admin/RecruitJobManagement/PostANewJob",
        data: {
            recruitjobdto: b,
            typeFix: parseInt(a)
        },
        success: function(a) {
            $.notify(a.message, a.status), "success" == a.status && window.setTimeout(function() {
                location.reload();
            }, 500);
        },
        error: function(a) {
            return $.notify("Không thành công! Thử lại sau", "error"), !1;
        }
    });
}

function UpdateAccount(a) {
    var b = {
        WIUserName: $("#WIUserName").val(),
        WIEmail: $("#WIEmail").val(),
        WIPassword: $("#WIPassword").val(),
        WIFullName: $("#WIFullName").val(),
        WIPhoneNumber: $("#WIPhoneNumber").val(),
        WIAddress: $("#WIAddress").val(),
        WIType: $("#WIType").children("option:selected").val(),
        WIGender: $("#WIGender").children("option:selected").val(),
        WIBirthdayString: $("#WIBirthdayString").val(),
        WIStatus: $("#WIStatus").children("option:selected").val(),
        WIPosition: $("#WIPosition").children("option:selected").val()
    };
    2 == a && (b.WebmasterInfoId = $("#WebmasterInfoId").val(), b.WI_AspNetUserId = $("#WI_AspNetUserId").val()), 
    $.ajax({
        type: "POST",
        url: "/Admin/AccountManage/UpdateAccount",
        data: {
            accountdto: b,
            typeFix: parseInt(a)
        },
        success: function(a) {
            $.notify(a.message, a.status), "success" == a.status && window.setTimeout(function() {
                location.reload();
            }, 500);
        },
        error: function(a) {
            return $.notify("Không thành công! Thử lại sau", "error"), !1;
        }
    });
}

function ClickPQ(a) {
    ix = 0, $("#role_table .checkboxpq").removeClass("readonly"), $("#role_table .checkboxpq").prop("checked", !1);
    var b = a.attr("UserId");
    $.ajax({
        type: "POST",
        url: "/Admin/AccountManage/GetListDecentralization",
        data: {
            idUser: b
        },
        success: function(a) {
            $.each(a, function(a, b) {
                $("#check_" + b).prop("checked", !0);
            });
        },
        error: function(a) {
            return $.notify("Không thành công! Thử lại sau", "error"), !1;
        }
    });
}

var App = function(a) {
    a.Services = {};
};

jQuery.extend(jQuery.validator.messages, {
    required: "Không được để trống"
}), $.validator.addMethod("regex", function(a, b, c) {
    return this.optional(b) || c.test(a);
}, "Please check your input."), $.validator.addMethod("selectRequired", function(a, b, c) {
    return c != a;
}, "Hãy chọn một giá trị"), $("#__AjaxAntiForgeryForm").validate({
    rules: {
        Password: {
            required: !0,
            minlength: 6
        },
        Email: {
            required: !0,
            email: !0
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
    submitHandler: function(a) {
        Login();
    }
}), AddAntiForgeryToken = function(a) {
    return a.__RequestVerificationToken = $("#__AjaxAntiForgeryForm input[name=__RequestVerificationToken]").val(), 
    a;
};

var CandidateId;

$(".btn-update-cdd").click(function() {
    var a = parseInt($(this).attr("id").substring(3));
    $.ajax({
        type: "POST",
        url: "/Admin/CandidateManagement/GetInforCandidateById",
        data: {
            idCandidate: a
        },
        success: function(a) {
            SetValueForCandidate(a.resultObject);
        },
        error: function(a) {
            return $.notify("Lấy thông tin không thành công!", "error"), !1;
        }
    });
});

var idCandidate;

$(".btn-del-cdd").click(function() {
    idCandidate = parseInt($(this).attr("input-id-cdd"));
    var a = $(this).parent().siblings(".username")[0].innerText;
    $("#namedelete").text(a);
}), $("#btn-yes-delete-cdd").click(function() {
    $.ajax({
        type: "POST",
        url: "/Admin/CandidateManagement/DeleteCandidate",
        data: {
            idCandidate: idCandidate
        },
        success: function(a) {
            $.notify(a.message, a.status), $("#exampleModalCenter").css("display", "none"), 
            $(".modal-backdrop").remove(), "success" == a.status && window.setTimeout(function() {
                location.reload();
            }, 500);
        },
        error: function(a) {
            return $.notify("Lấy thông tin không thành công!", "error"), !1;
        }
    });
}), $("#updatecandidate-form").validate({
    rules: {
        CddUserName: {
            required: !0
        },
        CddFullName: {
            required: !0
        },
        CddAge: {
            required: !0
        },
        CddAbout: {
            required: !0
        },
        CPExperience: {
            required: !0
        },
        CddPhone: {
            required: !0
        },
        CddEmail: {
            required: !0
        },
        CddAddress: {
            required: !0
        },
        CddPassword: {
            required: !0,
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
    submitHandler: function(a) {
        UpdateProfileCandidate($(".updatecandidate").attr("id").substring(10));
    }
});

var $dn = $(".uploadfilecv");

$dn.change(dnChange), $("#CreateNew").validate({
    rules: {},
    messages: {},
    submitHandler: function(a) {
        CreateNew($(".updatenew").attr("id").substring(4), $("#IdNew").val());
    }
}), $("#image_uploads").on("change", function() {
    var a = $(this)[0].files, b = new FormData();
    b.append("file", a[0]), $.ajax({
        type: "POST",
        url: "/Admin/NewManagement/UploadFileImageNew",
        contentType: !1,
        processData: !1,
        data: b,
        success: function(a) {
            $("#avatarnew").attr("src", a), $(".imagenew img").css("display", "inline-block");
        },
        error: function(a) {
            alert("Có lỗi!");
        }
    });
});

var idNew;

$(".btn-del-new").click(function() {
    idNew = parseInt($(this).attr("input-id-new")), $("#namedelete").text($("#new" + idNew).text());
}), $("#btn-yes-delete-new").click(function() {
    $.ajax({
        type: "POST",
        url: "/Admin/NewManagement/DeleteNew",
        data: {
            idNew: idNew
        },
        success: function(a) {
            $.notify(a.message, a.status), $("#exampleModalCenter").css("display", "none"), 
            $(".modal-backdrop").remove(), "success" == a.status && window.setTimeout(function() {
                location.reload();
            }, 500);
        },
        error: function(a) {
            return $.notify("Lấy thông tin không thành công!", "error"), !1;
        }
    });
}), $("#updaterecruit-form").validate({
    rules: {
        RIFullName: {
            required: !0
        },
        FounedYearString: {
            required: !0
        },
        RIAbout: {
            required: !0
        },
        RIPhone: {
            required: !0
        },
        RIEmail: {
            required: !0
        },
        RIPassword: {
            required: !0,
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
            required: !0
        }
    },
    messages: {
        RIPassword: {
            minlength: "Mật khẩu ít nhất 6 ký tự"
        },
        RIPasswordConfirm: {
            equalTo: "Mật khẩu không trùng khớp"
        }
    },
    submitHandler: function(a) {
        UpdateRecruit($(".updaterecruit").attr("id").substring(8));
    }
});

var idRecruit;

$(".btn-del-rc").click(function() {
    idRecruit = parseInt($(this).attr("input-id-rc"));
    var a = $(this).parent().siblings(".username")[0].innerText;
    $("#namedelete").text(a);
}), $("#btn-yes-delete-rc").click(function() {
    $.ajax({
        type: "POST",
        url: "/Admin/RecruitManagement/DeleteRecruit",
        data: {
            idRecruit: idRecruit
        },
        success: function(a) {
            $.notify(a.message, a.status), $("#exampleModalCenter").css("display", "none"), 
            $(".modal-backdrop").remove(), "success" == a.status && window.setTimeout(function() {
                location.reload();
            }, 500);
        },
        error: function(a) {
            return $.notify("Lấy thông tin không thành công!", "error"), !1;
        }
    });
});

var BgRecruit;

$f2 = $("#updatelogo"), $f2.change(ChangeLogo);

var $f1 = $("#updatecover");

$f1.change(ChangeCover);

var $fa = $("#updateavatar");

$(".avatar-fix").click(function() {
    $(this).parent().next().click();
}), $fa.change(ChangeAvatar);

var $fbook = $("#image_uploads_book");

$fbook.change(BookImage), $("#advertisement-form").validate({
    rules: {
        AdStartDate: {
            required: !0
        },
        AdEndDate: {
            required: !0
        },
        AdPosition: {
            required: !0
        },
        AdName: {
            required: !0
        },
        AdPhone: {
            required: !0,
            number: !0,
            minlength: 10,
            maxlength: 11,
            regex: /(^\(\+\84\)\s\d{3}\s\d{3}$)|(^\d{10}$)|(^\d{4}\s\d{3}\s\d{3}$)|(^\(\+\84\)\d{3}\d{3}$)/
        },
        AdEmail: {
            required: !0,
            regex: /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
        },
        AdAddress: {
            required: !0
        },
        AdLink: {
            required: !0,
            regex: /^(http|https)?:\/\/[a-zA-Z0-9-\.]+\.[a-z]{2,4}/
        }
    },
    messages: {},
    submitHandler: function(a) {
        BookAdvertisement($(".updateadvertisement").attr("id").substring(14));
    }
}), $("#admin-updatepost-form").validate({
    rules: {
        RJTitle: {
            required: !0
        },
        RJPosition: {
            required: !0
        },
        RJ_LevelId: {
            required: !0
        },
        RJ_SalaryId: {
            required: !0
        },
        RJ_WorkTypeId: {
            required: !0
        },
        RJ_ExperienceId: {
            required: !0
        },
        RJAmount: {
            required: !0
        },
        RJExpirationDate: {
            required: !0
        },
        RJ_Describe: {
            required: !0
        },
        RJ_Require: {
            required: !0
        },
        RJBenefit: {
            required: !0
        },
        RJPhoneContact: {
            required: !0,
            number: !0,
            minlength: 10,
            maxlength: 11,
            regex: /(^\(\+\84\)\s\d{3}\s\d{3}$)|(^\d{10}$)|(^\d{4}\s\d{3}\s\d{3}$)|(^\(\+\84\)\d{3}\d{3}$)/
        },
        RJEmailContact: {
            required: !0,
            regex: /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
        },
        RJNameContact: {
            required: !0
        },
        RJCityId: {
            required: !0
        },
        RJDistrictId: {
            required: !0
        },
        RJ_WardId: {
            required: !0
        },
        RJ_WorkPlace: {
            required: !0
        }
    },
    messages: {
        RJEmailContact: {
            email: "Hãy nhập đúng định dạng email"
        },
        RJPhoneContact: {
            regex: "Hãy nhập đúng định dạng điện thoại"
        }
    },
    submitHandler: function(a) {
        UpdateJob($(".updatejob").attr("id").substring(8));
    }
});

var IDJob;

$(".btn-del-job").click(function() {
    IDJob = $(this).attr("input-id-job"), $("#namedelete").text($("#titlejob" + IDJob).text());
}), $(".btndeletejob").click(function() {
    $.ajax({
        type: "POST",
        url: "/Admin/RecruitJobManagement/DeleteJob",
        data: {
            idJob: IDJob
        },
        success: function(a) {
            if ($("#exampleModalCenter").css("display", "none"), $(".modal-backdrop.show").css("opacity", "0"), 
            !a) return $.notify("Xóa không thành công!", "error"), !1;
            $.notify("Xóa thành công!", "success"), window.setTimeout(function() {
                location.reload();
            }, 500);
        },
        error: function(a) {
            return $.notify("Xóa không thành công!", "error"), !1;
        }
    });
}), $("#admin-createaccount-form").validate({
    rules: {
        WIFullName: {
            required: !0
        },
        WIUserName: {
            required: !0
        },
        WIAddress: {
            required: !0
        },
        WIBirthDay: {
            required: !0
        },
        WIPhoneNumber: {
            required: !0,
            number: !0,
            minlength: 10,
            maxlength: 11,
            regex: /(^\(\+\84\)\s\d{3}\s\d{3}$)|(^\d{10}$)|(^\d{4}\s\d{3}\s\d{3}$)|(^\(\+\84\)\d{3}\d{3}$)/
        },
        WIEmail: {
            required: !0,
            regex: /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
        },
        WIPassword: {
            required: !0,
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
    submitHandler: function(a) {
        UpdateAccount($(".updateaccount").attr("id").substring(8));
    }
});

var ix = 0, idAspNetUser;

$(".userid").unbind().click(function() {
    var a = $(this);
    idAspNetUser = a.attr("userid"), ClickPQ(a);
}), $("#submitDecentralization").click(function() {
    var a = $("#role_table").find("input"), b = [];
    $.each(a, function() {
        this.checked && b.push(this.value);
    }), $.ajax({
        type: "POST",
        url: "/Admin/AccountManage/UpdateDecentralization",
        data: {
            idAspNetUser: idAspNetUser,
            listroles: b
        },
        success: function(a) {
            console.log(a), 1 == a ? $.notify("Phân quyền thành công", "success") : $.notify("Không thành công! Thử lại sau", "error");
        },
        error: function(a) {
            return $.notify("Không thành công! Thử lại sau", "error"), !1;
        }
    });
}), $("#admin-updatepass-form").validate({
    rules: {
        WIOldPassword: {
            required: !0,
            minlength: 6
        },
        WIPassword: {
            required: !0,
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
    submitHandler: function(a) {
        var b = $("#WIOldPassword").val(), c = $("#WIPassword").val(), d = $("#WI_AspNetUserId").val();
        $.ajax({
            type: "POST",
            url: "/Admin/AccountManage/ChangePassword",
            data: {
                idUser: d,
                oldPass: b,
                newPass: c
            },
            success: function(a) {
                $.notify(a.message, a.status), "success" == a.status && window.setTimeout(function() {
                    location.reload();
                }, 500);
            },
            error: function(a) {
                return $.notify("Không thành công! Thử lại sau", "error"), !1;
            }
        });
    }
});

var idwebmt;

$(".btn-del-webmt").click(function() {
    idwebmt = parseInt($(this).attr("input-id-webmt")), $("#namedelete").text($("#username" + idwebmt).text());
}), $("#btn-yes-delete-webmt").click(function() {
    $.ajax({
        type: "POST",
        url: "/Admin/AccountManage/DeleteAccount",
        data: {
            idwebmt: idwebmt
        },
        success: function(a) {
            $.notify(a.message, a.status), $("#exampleModalCenter").css("display", "none"), 
            $(".modal-backdrop").remove(), "success" == a.status && window.setTimeout(function() {
                location.reload();
            }, 500);
        },
        error: function(a) {
            return $.notify("Lấy thông tin không thành công!", "error"), !1;
        }
    });
});

var idad;

$(".btn-del-ad").click(function() {
    idad = parseInt($(this).attr("input-id-ad"));
}), $("#btn-yes-delete-ad").click(function() {
    $.ajax({
        type: "POST",
        url: "/Admin/Advertisement/DeleteAdvertisement",
        data: {
            idad: idad
        },
        success: function(a) {
            $.notify(a.message, a.status), $("#exampleModalCenter").css("display", "none"), 
            $(".modal-backdrop").remove(), "success" == a.status && window.setTimeout(function() {
                location.reload();
            }, 500);
        },
        error: function(a) {
            return $.notify("Lấy thông tin không thành công!", "error"), !1;
        }
    });
}), $(document).on("click", ".exportexcel--btn", function() {
    var a = $("#example_filter > label > input").val(), b = ($(this).attr("id"), getUrlParameter("status"));
    $(".valuesearch").val(a), $(".valuestatus").val(b), $("#ExportExcel").trigger("submit");
});

var getUrlParameter = function(a) {
    var b, c, d = window.location.search.substring(1), e = d.split("&");
    for (c = 0; c < e.length; c++) if (b = e[c].split("="), b[0] === a) return void 0 === b[1] || decodeURIComponent(b[1]);
};

$(document).ready(function() {
    $("#example").DataTable({
        language: {
            info: "Tổng số: _TOTAL_",
            infoEmpty: "Tổng số: 0",
            lengthMenu: "Hiển thị _MENU_ bản ghi",
            emptyTable: "Không có dữ liệu",
            infoFiltered: "(tìm kiếm từ _MAX_ bản ghi)",
            infoPostFix: "",
            thousands: ",",
            loadingRecords: "Loading...",
            processing: "Processing...",
            search: "Tìm kiếm:",
            zeroRecords: "Không có kết quả tìm kiếm",
            paginate: {
                first: "Đầu tiên",
                last: "Trước",
                next: "Sau",
                previous: "Cuối cùng"
            },
            aria: {
                sortAscending: ": activate to sort column ascending",
                sortDescending: ": activate to sort column descending"
            }
        }
    });
});
//# sourceMappingURL=script.js.map
});