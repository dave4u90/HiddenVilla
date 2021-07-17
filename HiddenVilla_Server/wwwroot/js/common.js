window.ShowToastr = (type, message) => {
    if (type === "success")
    {
        toastr.success(message, 'Operation Successful')
    }
    if (type === "error") {
        toastr.error(message, 'Operation Failed')
    }
}

window.ShowSwal = (type, message) => {
    if (type === "success") {
        Swal.fire('Operation Successful', message, 'success');
    }
    if (type === "error") {
        Swal.fire('Operation Failed', message, 'error');
    }
}

function ShowDeleteConfirmationModal() {
    $("#deleteConfirmationModal").modal('show');
}

function HideDeleteConfirmationModal() {
    $("#deleteConfirmationModal").modal('hide');
}
