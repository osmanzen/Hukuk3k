document.querySelector('#muvekkilDuzenle').addEventListener('submit', function (e) {
    var form = this;
    e.preventDefault();

    swal({
        title: "Güncellemeleri Onaylıyor musunuz?",
        text: "",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: 'İptal',
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Onaylıyorum",
        closeOnConfirm: false
    },
        function () {
            form.submit();
        });
});