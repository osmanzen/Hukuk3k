document.querySelector('#davaekle').addEventListener('submit', function (e) {
    var form = this;
    e.preventDefault();

    swal({
        title: "Dava Eklemeyi Onaylıyor musunuz?",
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

var drpSehir = $("#SehirId");
var drpAdliye = $("#AdliyeId");
var drpDosya = $("#DosyaId");
var drpBirim = $("#BirimDaireId");
var drpDurum = $("#DavaDurumId");
var drpMuvekkil = $("#drpMuvekkil");


var sehirEkle = $('#sehirEkle');
var sehirDuzenle = $('#sehirDuzenle');
var sehirSil = $('#sehirSil');

var adliyeEkle = $('#adliyeEkle');
var adliyeDuzenle = $('#adliyeDuzenle');
var adliyeSil = $('#adliyeSil');

var dosyaEkle = $('#dosyaEkle');
var dosyaDuzenle = $('#dosyaDuzenle');
var dosyaSil = $('#dosyaSil');

var birimEkle = $('#birimEkle');
var birimDuzenle = $('#birimDuzenle');
var birimSil = $('#birimSil');

var durumEkle = $('#durumEkle');
var durumDuzenle = $('#durumDuzenle');
var durumSil = $('#durumSil');

var davaBaslikEkle = $('#davaBaslikEkle');
var davaBaslikDuzenle = $('#davaBaslikDuzenle');
var davaBaslikSil = $('#davaBaslikSil');

var sehirler = function () {
    drpSehir.empty().selectpicker('refresh');
    $.ajax({
        url: "/Admin/Dava/SehirGetir",
        type: "POST",
        success: function (response) {
            $.each(response, function (index, sehir) {
                drpSehir.append("<option value='" + sehir.SehirId + "' >" + sehir.SehirAdi + "</option>");
            });
            drpSehir.selectpicker('refresh');
            drpSehir.trigger("change");
            dosyaEkle.attr("disabled", true);
            birimEkle.attr("disabled", true);
        },
        failure: function (response) {
            alert("Eklenemedi");
        },
        error: function (response) {
            alert("Hata Oluştu");
        }
    });
};

var muvekkiller = function () {
    drpMuvekkil.empty().selectpicker('refresh');
    $.ajax({
        url: "/Admin/Muvekkil/AktifGetir",
        type: "POST",
        success: function (response) {
            $.each(response, function (index, kullanici) {
                drpMuvekkil.append("<option value='" + kullanici.TCKimlikNo + "' >" + kullanici.TCKimlikNo + " - " + kullanici.AdSoyad + "</option>");
            });
            drpMuvekkil.selectpicker('refresh');
        },
        failure: function (response) {
            alert("Eklenemedi");
        },
        error: function (response) {
            alert("Hata Oluştu");
        }
    });
};

drpSehir.change(function () {
    drpAdliye.empty().selectpicker('refresh');
    drpDosya.empty().selectpicker('refresh');
    drpBirim.empty().selectpicker('refresh');

    if (drpSehir.val() !== "-----") {
        $.ajax({
            url: "/Admin/Dava/AdliyeGetir",
            data: { id: drpSehir.val() },
            dataType: "JSON",
            type: "POST",
            success: function (response) {
                $.each(response, function (index, adliye) {
                    drpAdliye.append("<option value='" + adliye.AdliyeId + "' >" + adliye.AdliyeAdi + "</option>");
                });
                drpAdliye.selectpicker('refresh');
                drpAdliye.trigger("change");
                dosyaEkle.attr("disabled", true);
                birimEkle.attr("disabled", true);
            },
            failure: function (response) {
                alert("Eklenemedi");
            },
            error: function (response) {
                alert("Hata Oluştu");
            }
        });
    }
});

drpAdliye.change(function () {
    drpDosya.empty().selectpicker('refresh');
    drpBirim.empty().selectpicker('refresh');

    if (drpAdliye.val() !== null) {
        $.ajax({
            url: "/Admin/Dava/DavaTipiGetir",
            data: { id: drpAdliye.val() },
            dataType: "JSON",
            type: "POST",
            success: function (response) {
                $.each(response, function (index, davaTipi) {
                    drpDosya.append("<option value='" + davaTipi.DavaTipiId + "' >" + davaTipi.DavaTipiAdi + "</option>");
                });
                drpDosya.selectpicker('refresh');
                drpDosya.trigger("change");
                birimEkle.attr("disabled", true);
                dosyaEkle.attr("disabled", false);
            },
            failure: function (response) {
                alert("Eklenemedi");
            },
            error: function (response) {
                alert("Hata Oluştu");
            }
        });
    }
});

drpDosya.change(function () {
    drpBirim.empty().selectpicker('refresh');

    if (drpDosya.val() !== null) {
        $.ajax({
            url: "/Admin/Dava/BirimGetir",
            data: { id: drpDosya.val() },
            dataType: "JSON",
            type: "POST",
            success: function (response) {
                $.each(response, function (index, birim) {
                    drpBirim.append("<option value='" + birim.BirimDaireId + "' >" + birim.BirimDaireAdi + "</option>");
                });
                drpBirim.selectpicker('refresh');
                birimEkle.attr("disabled", false);
            },
            failure: function (response) {
                alert("Eklenemedi");
            },
            error: function (response) {
                alert("Hata Oluştu");
            }
        });
    }
});

$('#AcilisTarihi').bootstrapMaterialDatePicker({ lang: 'tr', weekStart: 1, time: false });

sehirEkle.click(function () {
    swal({
        title: "Şehir Ekle",
        type: "input",
        showCancelButton: true,
        closeOnConfirm: false,
        animation: "slide-from-bottom",
        inputPlaceholder: "Şehir Adı",
        cancelButtonText: 'İptal',
        confirmButtonText: "Kaydet"
    }, function (inputValue) {
        if (inputValue === false) return false;
        if (inputValue === "") {
            swal.showInputError("Boş geçilemez"); return false;
        }
        else {
            $.ajax({
                url: "/Admin/Dava/SehirEkle",
                data: { sehirAdi: inputValue },
                dataType: "JSON",
                type: "POST",
                success: function (sehir) {
                    sehirler();
                    swal("Başarılı!", sehir.SehirAdi + " Şehirlere Eklendi.", "success");
                }
            });

        }
    });
});

sehirDuzenle.click(function () {
    swal({
        title: "Şehir Adı Değiştir",
        type: "input",
        showCancelButton: true,
        closeOnConfirm: false,
        animation: "slide-from-bottom",
        inputPlaceholder: "Şehir Adı",
        cancelButtonText: 'İptal',
        confirmButtonText: "Güncelle"
    }, function (inputValue) {
        if (inputValue === false) return false;
        if (inputValue === "") {
            swal.showInputError("Boş geçilemez"); return false;
        }
        else {
            $.ajax({
                url: "/Admin/Dava/SehirGuncelle",
                data: { sehirAdi: inputValue, sehirId: drpSehir.val() },
                dataType: "JSON",
                type: "POST",
                success: function (sehir) {
                    sehirler();
                    swal("Başarılı!", sehir.SehirAdi + " Şehir Güncellendi.", "success");
                }
            });
        }
    });
});

sehirSil.click(function () {
    swal({
        title: "Seçilen Şehri Silmeyi Onaylıyor musunuz?",
        text: "",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: 'İptal',
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Onaylıyorum",
        closeOnConfirm: false
    },
        function () {
            $.ajax({
                url: "/Admin/Dava/SehirSil",
                data: { sehirId: drpSehir.val() },
                dataType: "JSON",
                type: "POST",
                success: function (baslik) {
                    swal("Başarılı!", " Şehir Silindi.", "success");
                    sehirler();
                }
            });
        });
});

adliyeEkle.click(function () {
    swal({
        title: "Adliye Ekle",
        type: "input",
        showCancelButton: true,
        closeOnConfirm: false,
        animation: "slide-from-bottom",
        inputPlaceholder: "Adliye Adı",
        cancelButtonText: 'İptal',
        confirmButtonText: "Kaydet"
    }, function (inputValue) {
        if (inputValue === false) return false;
        if (inputValue === "") {
            swal.showInputError("Boş geçilemez"); return false;
        }
        else {
            $.ajax({
                url: "/Admin/Dava/AdliyeEkle",
                data: { adliyeAdi: inputValue, sehirId: drpSehir.val() },
                dataType: "JSON",
                type: "POST",
                success: function (adliye) {
                    drpSehir.trigger("change");
                    swal("Başarılı!", adliye.AdliyeAdi + " Adliyelere Eklendi.", "success");
                }
            });

        }
    });
});

sehirDuzenle.click(function () {
    swal({
        title: "Adliye Adı Değiştir",
        type: "input",
        showCancelButton: true,
        closeOnConfirm: false,
        animation: "slide-from-bottom",
        inputPlaceholder: "Adliye Adı",
        cancelButtonText: 'İptal',
        confirmButtonText: "Güncelle"
    }, function (inputValue) {
        if (inputValue === false) return false;
        if (inputValue === "") {
            swal.showInputError("Boş geçilemez"); return false;
        }
        else {
            $.ajax({
                url: "/Admin/Dava/AdliyeGuncelle",
                data: { adliyeAdi: inputValue, adliyeId: drpAdliye.val() },
                dataType: "JSON",
                type: "POST",
                success: function (adliye) {
                    adliye();
                    swal("Başarılı!", adliye.AdliyeAdi + " Adliyesi Güncellendi.", "success");
                }
            });
        }
    });
});

sehirSil.click(function () {
    swal({
        title: "Seçilen Adliyeyi Silmeyi Onaylıyor musunuz?",
        text: "",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: 'İptal',
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Onaylıyorum",
        closeOnConfirm: false
    },
        function () {
            $.ajax({
                url: "/Admin/Dava/AdliyeSil",
                data: { adliyeId: drpAdliye.val() },
                dataType: "JSON",
                type: "POST",
                success: function (adliye) {
                    swal("Başarılı!", "Adliye Silindi.", "success");
                    adliye();
                }
            });
        });
});

dosyaEkle.click(function () {
    swal({
        title: "Dosya Türü Ekle",
        type: "input",
        showCancelButton: true,
        closeOnConfirm: false,
        animation: "slide-from-bottom",
        inputPlaceholder: "Dosya Türü",
        cancelButtonText: 'İptal',
        confirmButtonText: "Kaydet"
    }, function (inputValue) {
        if (inputValue === false) return false;
        if (inputValue === "") {
            swal.showInputError("Boş geçilemez"); return false;
        }
        else {
            $.ajax({
                url: "/Admin/Dava/DosyaTuruEkle",
                data: { tipAdi: inputValue, adliyeId: drpAdliye.val() },
                dataType: "JSON",
                type: "POST",
                success: function (dosyaTip) {
                    drpAdliye.trigger("change");
                    swal("Başarılı!", dosyaTip.DavaTipiAdi + " Dosya Türüne Eklendi", "success");
                }
            });
        }
    });
});

dosyaDuzenle.click(function () {
    swal({
        title: "Dosya Adı Değiştir",
        type: "input",
        showCancelButton: true,
        closeOnConfirm: false,
        animation: "slide-from-bottom",
        inputPlaceholder: "Dosya Adı",
        cancelButtonText: 'İptal',
        confirmButtonText: "Güncelle"
    }, function (inputValue) {
        if (inputValue === false) return false;
        if (inputValue === "") {
            swal.showInputError("Boş geçilemez"); return false;
        }
        else {
            $.ajax({
                url: "/Admin/Dava/DosyaTuruGuncelle",
                data: { tipAdi: inputValue, tipId: drpDosya.val() },
                dataType: "JSON",
                type: "POST",
                success: function (tip) {
                    drpAdliye.trigger("change");
                    swal("Başarılı!", tip.DavaTipiAdi + " Dosya Türü Güncellendi.", "success");
                }
            });
        }
    });
});

dosyaSil.click(function () {
    swal({
        title: "Seçilen Adliyeyi Silmeyi Onaylıyor musunuz?",
        text: "",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: 'İptal',
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Onaylıyorum",
        closeOnConfirm: false
    },
        function () {
            $.ajax({
                url: "/Admin/Dava/DosyaTuruSil",
                data: { tipId: drpDosya.val() },
                dataType: "JSON",
                type: "POST",
                success: function (tip) {
                    drpAdliye.trigger("change");
                    swal("Başarılı!", "Dosya Türü Silindi.", "success");
                }
            });
        });
});

var durumlar = function () {
    drpDurum.empty().selectpicker('refresh');
    $.ajax({
        url: "/Admin/Dava/DurumGetir",
        type: "POST",
        success: function (response) {
            $.each(response, function (index, durum) {
                drpDurum.append("<option value='" + durum.DavaDurumId + "' >" + durum.DavaDurumAdi + "</option>");
            });
            drpDurum.selectpicker('refresh');
        },
        failure: function (response) {
            alert("Eklenemedi");
        },
        error: function (response) {
            alert("Hata Oluştu");
        }
    });
};

durumEkle.click(function () {
    swal({
        title: "Dava Durumu Ekle",
        type: "input",
        showCancelButton: true,
        closeOnConfirm: false,
        animation: "slide-from-bottom",
        inputPlaceholder: "Dava Durumu...",
        cancelButtonText: 'İptal',
        confirmButtonText: "Kaydet"
    }, function (inputValue) {
        if (inputValue === false) return false;
        if (inputValue === "") {
            swal.showInputError("Boş geçilemez"); return false;
        }
        else {
            $.ajax({
                url: "/Admin/Dava/DavaDurumuEkle",
                data: { davaDurumu: inputValue },
                dataType: "JSON",
                type: "POST",
                success: function (davaDurumu) {
                    durumlar();
                    swal("Başarılı!", davaDurumu.DavaDurumAdi + " Dava Durumlarına Eklendi", "success");
                }
            });
        }
    });
});

durumDuzenle.click(function () {
    swal({
        title: "Durum Adı Değiştir",
        type: "input",
        showCancelButton: true,
        closeOnConfirm: false,
        animation: "slide-from-bottom",
        inputPlaceholder: "Durum Adı",
        cancelButtonText: 'İptal',
        confirmButtonText: "Güncelle"
    }, function (inputValue) {
        if (inputValue === false) return false;
        if (inputValue === "") {
            swal.showInputError("Boş geçilemez"); return false;
        }
        else {
            $.ajax({
                url: "/Admin/Dava/DavaDurumGuncelle",
                data: { durumAdi: inputValue, durumId: drpDurum.val() },
                dataType: "JSON",
                type: "POST",
                success: function (durum) {
                    durumlar();
                    swal("Başarılı!", durum.DavaDurumAdi + " Durum Güncellendi.", "success");
                }
            });
        }
    });
});

durumSil.click(function () {
    swal({
        title: "Seçilen Durumu Silmeyi Onaylıyor musunuz?",
        text: "",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: 'İptal',
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Onaylıyorum",
        closeOnConfirm: false
    },
        function () {
            $.ajax({
                url: "/Admin/Dava/DavaDurumSil",
                data: { durumId: drpDurum.val() },
                dataType: "JSON",
                type: "POST",
                success: function (baslik) {
                    swal("Başarılı!", " Durum Silindi.", "success");
                    durumlar();
                }
            });
        });
});

birimEkle.click(function () {
    swal({
        title: "Birim Ekle",
        type: "input",
        showCancelButton: true,
        closeOnConfirm: false,
        animation: "slide-from-bottom",
        inputPlaceholder: "Birim Adı...",
        cancelButtonText: 'İptal',
        confirmButtonText: "Kaydet"
    }, function (inputValue) {
        if (inputValue === false) return false;
        if (inputValue === "") {
            swal.showInputError("Boş geçilemez"); return false;
        }
        else {
            $.ajax({
                url: "/Admin/Dava/BirimEkle",
                data: { birimAdi: inputValue, dosyaId: drpDosya.val() },
                dataType: "JSON",
                type: "POST",
                success: function (birim) {
                    swal("Başarılı!", birim.BirimDaireAdi + " Dava Durumlarına Eklendi", "success");
                    drpDosya.trigger("change");
                }
            });
        }
    });
});

birimDuzenle.click(function () {
    swal({
        title: "Birim Adı Değiştir",
        type: "input",
        showCancelButton: true,
        closeOnConfirm: false,
        animation: "slide-from-bottom",
        inputPlaceholder: "Birim Adı",
        cancelButtonText: 'İptal',
        confirmButtonText: "Güncelle"
    }, function (inputValue) {
        if (inputValue === false) return false;
        if (inputValue === "") {
            swal.showInputError("Boş geçilemez"); return false;
        }
        else {
            $.ajax({
                url: "/Admin/Dava/BaslikGuncelle",
                data: { baslikAdi: inputValue, baslikId: drpBirim.val() },
                dataType: "JSON",
                type: "POST",
                success: function (birim) {
                    drpDosya.trigger("change");
                    swal("Başarılı!", birim.DavaBaslikAdi + " Başlık Güncellendi.", "success");
                }
            });
        }
    });
});

birimSil.click(function () {
    swal({
        title: "Seçilen Birimi Silmeyi Onaylıyor musunuz?",
        text: "",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: 'İptal',
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Onaylıyorum",
        closeOnConfirm: false
    },
        function () {
            $.ajax({
                url: "/Admin/Dava/BirimSil",
                data: { birimId: drpBirim.val() },
                dataType: "JSON",
                type: "POST",
                success: function (birim) {
                    drpDosya.trigger("change");
                    swal("Başarılı!", " Birim Silindi.", "success");
                }
            });
        });
});
