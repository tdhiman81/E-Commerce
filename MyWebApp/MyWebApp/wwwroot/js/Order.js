var dtable;
$(document).ready(function () {
    dtable = $('#myTable').DataTable({
        "ajax": {
            "url": "/Admin/Order/AllOrders"
        },
        "columns":
            [
                { "data": "name" },
                { "data": "phone" },
                { "data": "orderStatus" },
                { "data": "orderTotal" },
                {
                    "data": 'id',
                    "render": function (data) {
                        return `
                        <a href="/Admin/Order/AllOrders?id=${data}"><i class="bi bi-pencil-square"> </i></a>
                  ` }
                }

            ]
            
    });
});

function RemoveProduct(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dtable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}