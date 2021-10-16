$(document).ready(function () {
    $('#example').DataTable({
        "language": {
            "info": "Tổng số: _TOTAL_",
            "infoEmpty": "Tổng số: 0",
            "lengthMenu": "Hiển thị _MENU_ bản ghi",
            "emptyTable": "Không có dữ liệu",
            "infoFiltered": "(tìm kiếm từ _MAX_ bản ghi)",
            "infoPostFix": "",
            "thousands": ",",
            "loadingRecords": "Loading...",
            "processing": "Processing...",
            "search": "Tìm kiếm:",
            "zeroRecords": "Không có kết quả tìm kiếm",
            "paginate": {
                "first": "Đầu tiên",
                "last": "Trước",
                "next": "Sau",
                "previous": "Cuối cùng"
            },
            "aria": {
                "sortAscending": ": activate to sort column ascending",
                "sortDescending": ": activate to sort column descending"
            }
        }
    });
});