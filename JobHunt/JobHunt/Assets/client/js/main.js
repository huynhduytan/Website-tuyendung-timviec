$(document).on('ready', function () {


    $('#btnsearchjob').click(function () {
        var cityId = $('#idcity').val();
    });

    //Validate extention of image
    function ValidateExtentionImage(extention) {
        if (extention != '') {
            var checkimg = extention.toLowerCase();
            if (!checkimg.match(/(\.jpg|\.png|\.JPG|\.PNG|\.jpeg|\.JPEG)$/)) {
                return false;
            }
        }
        return true;
    }


    $('#chooseAvatar').add("#avatarlink").click(function () {
        $("#updateavatar").click();
    });
    //upload avatar
    var $f1 = $("#updateavatar");
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
                GetLinkImage($(this));
            }
        }
    }
    $f1.change(ChangeAvatar);

    function GetLinkImage(btn) {
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
                Avatar = urlImage;
                $('#avatarlink').attr("src", urlImage);
            },
            error: function (err) {
                alert("Có lỗi!");
            }
        });
    }

    //register receive tin
    $('#chk_1').change(function () {
        if (this.checked) {
            $('.receivedropdown').css('display', 'flex');
        } else {
            $('.receivedropdown').css('display', 'none');
        }
    });
    $('#registerreceive').click(function () {
        var $email = $('#emailreceive');
        var $name = $('#namereceive');
        var checkFull = true;
        var checkRightInfo = false;
        //check info receive
        if ($email.length != 0 || $name.length != 0) {
            if ($email.val().length == 0 || $name.val().length == 0) {
                $.notify("Hãy nhập đầy đủ thông tin", "error");
                checkFull = false;
            } else {
                if (!isEmail($email.val())) {
                    $.notify("Email không đúng định dạng. Hãy nhập lại", "error");
                    checkFull = false;
                } else {
                    checkRightInfo = true;
                }
            }
        }

        //check option
        //check post
        var chk_1 = $('#chk_1:checkbox:checked').length > 0;
        //check new
        var chk_2 = $('#chk_2:checkbox:checked').length > 0;
        if (!chk_1 && !chk_2 && checkRightInfo) {
            $.notify("Hãy chọn bản tin điện tử mà bạn muốn", "error");
            checkFull = false;
        }
        if (checkFull) {
            var rrdto = {
                Email: $email.val(),
                Name: $name.val(),
                CheckNew: chk_2,
                CheckPost: chk_1,
                IdProfession: $('#idprofessionreceive').find(":selected").val()
            };
            console.log(rrdto);
            $.ajax({
                type: 'POST',
                url: '/Home/RegisterReceive',
                data: { rrdto: rrdto },
                success: function (result) {
                    console.log(result);
                    if (result) {
                        $.notify("Đăng ký thành công!", "success");
                    } else {
                        $.notify("Đã xảy ra lỗi trong quá trình đăng ký", "error");
                    }
                    $('#exampleModal').removeClass('show');
                    $('#exampleModal').css('display','none');
                    $(".modal-backdrop").removeClass("show");
                    $('.modal-backdrop').fadeOut('fast');
                },
                error: function (err) {
                    $.notify("Đã xảy ra lỗi trong quá trình đăng ký", "error");
                }
            });
        }
    });
    $('#closereceive').click(function () {
        $('#emailreceive').val('');
        $('#namereceive').val('');
        $('input:checkbox').removeAttr('checked');
        $('.receivedropdown').css('display', 'none');
    });
    function isEmail(email) {
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        return regex.test(email);
    }
});


//$('#updateprofileform').validate({
//    rules: {
//        "CddFullName": {
//            required: true,
//            //maxlength: 20
//        }
//    },
//    messages: {
//        "CddFullName": {
//            required: "Mã khách hàng không được để trống",
//            //maxlength: jQuery.validator.format("Không được nhập quá {0} kí tự.")
//        }
//    },
//    submitHandler: function (form) {
//        console.log("Hi");
//    }
//});

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
            $('#name-file-uploaded').text(NameFile);
        } else {
            $('.uploadfilecv').val("");
        }
        //}
    }
}
$dn.change(dnChange);
function ImportCV(btn) {
    //Lấy dữ liệu trên fileUpload
    var files = $(btn)[0].files;
    // Đối tượng formdata
    var formData = new FormData();
    formData.append('fileCV', files[0]);
    var Describe = $('#CddDescribeCV').val();
    formData.append('Describe', Describe);

    //add type
    $.ajax({
        type: 'POST',
        url: '/Candidate/UploadCV',
        contentType: false,
        processData: false,
        data: formData,
        success: function (resultReturn) {
            $('.uploadcv').val("");
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            $.notify(err, "error");
            return false;
        }
    });
}

$('#uploadcv').click(function () {
    //check TM
    var checkTM = true;
    //Lấy dữ liệu trên fileUpload
    var files = $('#fileCV')[0].files;
    var capacity = (files[0].size / 1024 / 1024).toFixed(3);
    if (parseInt(capacity) > 3) {
        checkTM = false;
        $.notify("Dung lượng tối đa là 3MB. Hãy thử lại", "error");
    }
    // Đối tượng formdata
    var formData = new FormData();
    formData.append('file', files[0]);
    var Describe = $('#CddDescribeCV').val();
    var Link = $('#CddDescribeCV').val();
    formData.append('Describe', Describe);
    formData.append('link', Link);
    if (checkTM) {
        //add type
        $.ajax({
            type: 'POST',
            url: '/Candidate/UploadCV',
            contentType: false,
            processData: false,
            data: formData,
            success: function (resultReturn) {
                $('.uploadcv').val("");
                $.notify(resultReturn.message, resultReturn.status);
                setTimeout(function () {
                    location.reload();
                }, 300);
            },
            error: function (xhr, status, p3, p4) {
                var err = "Error " + " " + status + " " + p3 + " " + p4;
                if (xhr.responseText && xhr.responseText[0] == "{")
                    err = JSON.parse(xhr.responseText).Message;
                $.notify(err, "error");
                return false;
            }
        });
    }
});


///save job
$('.fav-job').click(function () {
    var getPage = $('#pageCheck').val();
    var GetIdJob = $(this).attr('id');
    var postBy = '';
    var status = "";
    var idJob = parseInt(GetIdJob.substring(2));
    if (GetIdJob.indexOf("admin") > -1) {
        postBy = "admin";
    }
    if (this.className.indexOf("active") > -1) {
        status = "delete";
    } else {
        status = "save";
    }
    var sjdto = {
        SJ_RecruitJobId: idJob,
        status: status,
        postBy: postBy
    };
    $.ajax({
        type: 'POST',
        url: '/Candidate/SaveJob',
        data: { sjdto: sjdto },
        success: function (result) {
            $.notify(result.message, result.status);
            if (result.status == 'error') {
                $('#' + GetIdJob).removeClass('active');
            }
            if (getPage == 'savejob') {
                $('#savejob' + idJob).remove();
            }
            if (result.resultCode == 1) {
                $('.signin-popup-box').fadeIn('fast');
                $('html').addClass('no-scroll');
            }
            //setTimeout(function () {
            //    location.reload();
            //}, 300);
        },
        error: function (xhr, status, p3, p4) {
            $('#' + GetIdJob).removeClass('active');
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            $.notify(err, "error");
            return false;
        }
    });
});


$('#myModal').on('shown.bs.modal', function () {
    $('#myInput').trigger('focus');
});


var valueOfCV;

//Apply cv
$('#applyjob').click(function () {
    var UserID = $('#UserID').val();
    var JobID = $('#JobID').val();
    var PhoneUser = $('#PhoneCandidate').val();
    var CheckedCV = $("input[name='resumeApply']:checked").val();
    var checkCheckedUpFile = $('#optionsRadios0').is(':checked');
    var checkCheckedCV = $('#optionsRadios1').is(':checked');
    // Đối tượng formdata
    var formData = new FormData();
    //Gán dữ liệu cho form data
    formData.append('UserID', UserID);
    formData.append('JobID', JobID);
    formData.append('PhoneUser', PhoneUser);
    formData.append('CVOld', CheckedCV);
    if (valueOfCV != null)
        formData.append('file', valueOfCV[0]);

    //check thỏa mãn điều kiện
    var checkTM = true;
    if (PhoneUser == "") {
        checkTM = false;
        $.notify("Số điện thoại không được để trống", "error");
    }
    if (checkCheckedUpFile && valueOfCV == undefined) {
        checkTM = false;
        $.notify("Hãy chọn file để tải lên", "error");
    }
    if (checkTM) {
        $.ajax({
            type: 'POST',
            url: '/Candidate/PostResume',
            contentType: false,
            processData: false,
            data: formData,
            success: function (resultReturn) {
                $.notify(resultReturn.message, resultReturn.status);
            },
            error: function (xhr, status, p3, p4) {
                var err = "Error " + " " + status + " " + p3 + " " + p4;
                if (xhr.responseText && xhr.responseText[0] == "{")
                    err = JSON.parse(xhr.responseText).Message;
                $.notify(err, "error");
                return false;
            }
        });
    }
});


$('#fileCV').change(function () {
    $("#optionsRadios0").prop("checked", true);
    valueOfCV = $(this).get(0).files;
});


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
            callback(urlImage);
            //$('#bg-recruit').css('background-image', 'url(' + urlImage + ')');
        },
        error: function (err) {
            $.notify("Có lỗi " + err + " khi tải ảnh lên", "error");
        }
    });
}


//upload logo company
$('#updatelogo').change(function () {
    console.log(1232);
});

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


//click show popup
//var $boxes = $('#chk_1:checked');
//$boxes.each(function () {
//    alert("test");
//});

$('.deleteappliedjob').click(function () {
    var idPostResume = $(this).attr('id');
    $.ajax({
        type: 'POST',
        url: '/Candidate/DeleteAppliedJob',
        data: { id: parseInt(idPostResume) },
        success: function (result) {
            $.notify(result.message, result.status);
            if (result.resultCode == 1) {
                $('#applied' + idPostResume).remove();
            }
            //if (result.resultCode == 1) {
            //    $('.signin-popup-box').fadeIn('fast');
            //    $('html').addClass('no-scroll');
            //}
            //setTimeout(function () {
            //    location.reload();
            //}, 300);
        },
        error: function (xhr, status, p3, p4) {
            $('#' + GetIdJob).removeClass('active');
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            $.notify(err, "error");
            return false;
        }
    });
});