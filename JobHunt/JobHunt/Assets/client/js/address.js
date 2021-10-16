var Controller = 'Common';
var GetListDistrictsByIdCity = 'GetListDistrictsByIdCity';
var GetListWardsByIdDistrict = 'GetListWardsByIdDistrict';
var GetListCities = 'GetListCities';


$(document).ready(function () {

    //get value of id city
    var typeAccount = parseInt($('#TypeAccount').val());
    if (typeAccount == 1) {
        var idCity = parseInt($('#Cdd_CityId').val());
        if (!isNaN(idCity)) {
            _getDistrict(idCity);
        }
    } else {
        var idCity = parseInt($('#RI_CityId').val());
        if (!isNaN(idCity)) {
            _getDistrictRecruit(idCity);
        }
    }

    //candidate
    //get value of id district
    var idDistrict = parseInt($('#DistrictIdGet').val());
    if (!isNaN(idDistrict)) {
        _getWard(idDistrict, function () {
            $('#Cdd_DistrictId > option[value="' + idDistrict + '"]').attr('selected', 'selected');
        });
    }
    //get value of id ward
    var idWard = parseInt($('#WardIdGet').val());
    if (!isNaN(idWard)) {
        _getWard(idDistrict, function () {
            $('#Cdd_WardId > option[value="' + idWard + '"]').attr('selected', 'selected');
        });
    }

    //recruit
    var idDistrictRecruit = parseInt($('#DistrictIdGet').val());
    if (!isNaN(idDistrict)) {
        _getWardRecruit(idDistrictRecruit, function () {
            $('#RI_DistrictId > option[value="' + idDistrict + '"]').attr('selected', 'selected');
        });
    }
    //get value of id ward
    var idWardRecruit = parseInt($('#WardIdGet').val());
    if (!isNaN(idWard)) {
        _getWardRecruit(idDistrictRecruit, function () {
            $('#RI_WardId > option[value="' + idWard + '"]').attr('selected', 'selected');
        });
    }

    //job
    var idCityJob = parseInt($('#RJCityId').val());
    if (!isNaN(idCityJob)) {
        _getDistrictJob(idCityJob);
    }
    //district job
    var idDistrictJob = parseInt($('#DistrictIdJobGet').val());
    if (!isNaN(idDistrictJob)) {
        _getWardJob(idDistrictJob, function () {
            $('#RJDistrictId > option[value="' + idDistrictJob + '"]').attr('selected', 'selected');
        });
    }
    //ward job
    var idWardJob = parseInt($('#WardIdJobGet').val());
    if (!isNaN(idWardJob)) {
        _getWardJob(idDistrictJob, function () {
            $('#RJ_WardId > option[value="' + idWardJob + '"]').attr('selected', 'selected');
        });
    }
});



//recruit
$("#RI_CityId").on('change', function () {
    var idTinh = $(this).val();
    if (idTinh !== undefined && idTinh !== '') {
        _getDistrictRecruit(idTinh);
    }
    else {
        var htmlHuyen = '';
        htmlHuyen += '<option value="">-- Chọn Quận/Huyện --</option>';
        $("#RI_DistrictId").html(htmlHuyen);

        var htmlXa = '';
        htmlXa += '<option value="">-- Chọn Phường/Xã --</option>';
        $("#RI_WardId").html(htmlXa);
    }
});
$("#RI_DistrictId").on('change', function () {
    var idHuyen = $(this).val();
    if (idHuyen !== undefined && idHuyen !== '') {
        var nameTinh = $("#RI_CityId option:selected").text();
        var nameHuyen = $("#RI_DistrictId option:selected").text();
        $('#RIAddress').val(nameHuyen + " - " + nameTinh);
        _getWardRecruit(idHuyen);
    }
    else {
        var html = '';
        html += '<option value="">-- Chọn Phường/Xã --</option>';
        $("#RI_WardId").html(html);
    }
});
$("#RI_WardId").on('change', function () {
    var nameTinh = $("#RI_CityId option:selected").text();
    var nameHuyen = $("#RI_DistrictId option:selected").text();
    var nameXa = $("#RI_WardId option:selected").text();
    $('#RIAddress').val(nameXa + " - " + nameHuyen + " - " + nameTinh);
});

//candidate
$("#Cdd_CityId").on('change', function () {
    var idTinh = $(this).val();
    if (idTinh !== undefined && idTinh !== '') {
        _getDistrict(idTinh);
    }
    else {
        var htmlHuyen = '';
        htmlHuyen += '<option value="">-- Chọn Quận/Huyện --</option>';
        $("#Cdd_DistrictId").html(htmlHuyen);

        var htmlXa = '';
        htmlXa += '<option value="">-- Chọn Phường/Xã --</option>';
        $("#Cdd_WardId").html(htmlXa);
    }
});

$("#Cdd_DistrictId").on('change', function () {
    var idHuyen = $(this).val();
    if (idHuyen !== undefined && idHuyen !== '') {
        var nameTinh = $("#Cdd_CityId option:selected").text();
        var nameHuyen = $("#Cdd_DistrictId option:selected").text();
        $('#CddAddress').val(nameHuyen + " - " + nameTinh);
        $('#RIAddress').val(nameHuyen + " - " + nameTinh);
        _getWard(idHuyen);
    }
    else {
        var html = '';
        html += '<option value="">-- Chọn Phường/Xã --</option>';
        $("#Cdd_WardId").html(html);
    }
});
$("#Cdd_WardId").on('change', function () {
    var nameTinh = $("#Cdd_CityId option:selected").text();
    var nameHuyen = $("#Cdd_DistrictId option:selected").text();
    var nameXa = $("#Cdd_WardId option:selected").text();
    $('#CddAddress').val(nameXa + " - " + nameHuyen + " - " + nameTinh);
});
//job
$("#RJCityId").on('change', function () {
    $('#RJ_WorkPlace').val("");
    var idTinh = $(this).val();
    if (idTinh !== undefined && idTinh !== '') {
        _getDistrictJob(idTinh);
    }
    else {
        var htmlHuyen = '';
        htmlHuyen += '<option value="">-- Chọn Quận/Huyện --</option>';
        $("#RJDistrictId").html(htmlHuyen);

        var htmlXa = '';
        htmlXa += '<option value="">-- Chọn Phường/Xã --</option>';
        $("#RJ_WardId").html(htmlXa);
    }
});
$("#RJ_WardId").on('change', function () {
    var nameTinh = $("#RJCityId option:selected").text();
    var nameHuyen = $("#RJDistrictId option:selected").text();
    var nameXa = $("#RJ_WardId option:selected").text();
    $('#RJ_WorkPlace').val(nameXa +" - "+nameHuyen + " - " + nameTinh);
});
$("#RJDistrictId").on('change', function () {
    var idHuyen = $(this).val();
    if (idHuyen !== undefined && idHuyen !== '') {
        var nameTinh = $("#RJCityId option:selected").text();
        var nameHuyen = $("#RJDistrictId option:selected").text();
        $('#RJ_WorkPlace').val(nameHuyen + " - " + nameTinh);
        _getWardJob(idHuyen);

    }
    else {
        var html = '';
        html += '<option value="">-- Chọn Phường/Xã --</option>';
        $("#RJ_WardId").html(html);
        $('#RJ_WorkPlace').val("");
    }
});


//recruit
function _getDistrictRecruit(idTinh, callback) {
    $.get("/" + Controller + "/" + GetListDistrictsByIdCity + "?idCity=" + idTinh, function (data) {
        if (data !== null && data !== undefined && data.length) {
            var html = '';
            html += '<option value="">-- Chọn Quận/Huyện --</option>';
            $.each(data, function (key, item) {
                html += '<option value=' + item.DistrictId + '>' + item.DistrictName + '</option>';
            });
            $("#RI_DistrictId").html(html);
            var htmlXa = '';
            htmlXa += '<option value="">-- Chọn Phường/Xã --</option>';
            $("#RI_WardId").html(htmlXa);
            if (callback)
                callback();
        }
    });
}
function _getWardRecruit(idHuyen, callback) {
    var nameTinh = $("#RI_CityId option:selected").text();
    var nameHuyen = $("#RI_DistrictId option:selected").text();
    $.get("/" + Controller + "/" + GetListWardsByIdDistrict + "?idDistrict=" + idHuyen, function (data) {
        if (data !== null && data !== undefined && data.length) {
            var html = '';
            html += '<option value="">-- Chọn Phường/Xã --</option>';
            $.each(data, function (key, item) {
                html += '<option value=' + item.WardId + '>' + item.WardName + '</option>';
            });
            $("#RI_WardId").html(html);
            if (callback)
                callback(function () {
                    $('#RIAddress').val(nameHuyen + " - " + nameTinh);
                });
        }
    });
}


//candidate
function _getDistrict(idTinh, callback) {
    $.get("/" + Controller + "/" + GetListDistrictsByIdCity + "?idCity=" + idTinh, function (data) {
        if (data !== null && data !== undefined && data.length) {
            var html = '';
            html += '<option value="">-- Chọn Quận/Huyện --</option>';
            $.each(data, function (key, item) {
                html += '<option value=' + item.DistrictId + '>' + item.DistrictName + '</option>';
            });
            $("#Cdd_DistrictId").html(html);
            var htmlXa = '';
            htmlXa += '<option value="">-- Chọn Phường/Xã --</option>';
            $("#Cdd_WardId").html(htmlXa);
            if (callback)
                callback();
        }
    });
}

function _getWard(idHuyen, callback) {
    var nameTinh = $("#Cdd_CityId option:selected").text();
    var nameHuyen = $("#Cdd_DistrictId option:selected").text();
    $.get("/" + Controller + "/" + GetListWardsByIdDistrict + "?idDistrict=" + idHuyen, function (data) {
        if (data !== null && data !== undefined && data.length) {
            var html = '';
            html += '<option value="">-- Chọn Phường/Xã --</option>';
            $.each(data, function (key, item) {
                html += '<option value=' + item.WardId + '>' + item.WardName + '</option>';
            });
            $("#Cdd_WardId").html(html);
            if (callback)
                callback(function () {
                    $('#CddAddress').val(nameHuyen + " - " + nameTinh);
                });
        }
    });
}


//job
function _getDistrictJob(idTinh, callback) {
    $.get("/" + Controller + "/" + GetListDistrictsByIdCity + "?idCity=" + idTinh, function (data) {
        if (data !== null && data !== undefined && data.length) {
            var html = '';
            html += '<option value="">-- Chọn Quận/Huyện --</option>';
            $.each(data, function (key, item) {
                html += '<option value=' + item.DistrictId + '>' + item.DistrictName + '</option>';
            });
            $("#RJDistrictId").html(html);
            var htmlXa = '';
            htmlXa += '<option value="">-- Chọn Phường/Xã --</option>';
            $("#RJ_WardId").html(htmlXa);
            if (callback)
                callback(function () {
                    $('#RJ_WorkPlace').val(nameHuyen + " - " + nameTinh);
                });
        }
    });
}

function _getWardJob(idHuyen, callback) {
    var nameTinh = $("#RJCityId option:selected").text();
    var nameHuyen = $("#RJDistrictId option:selected").text();
    $.get("/" + Controller + "/" + GetListWardsByIdDistrict + "?idDistrict=" + idHuyen, function (data) {
        if (data !== null && data !== undefined && data.length) {
            var html = '';
            html += '<option value="">-- Chọn Phường/Xã --</option>';
            $.each(data, function (key, item) {
                html += '<option value=' + item.WardId + '>' + item.WardName + '</option>';
            });
            $("#RJ_WardId").html(html);
            if (callback)
                callback(function () {
                    $('#RJ_WorkPlace').val(nameHuyen + " - " + nameTinh);
                });
        }
    });
}