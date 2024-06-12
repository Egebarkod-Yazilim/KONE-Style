$(document).ready(function () {
    console.log("yüklendi");
    $('#currentCardAddressdatatable').DataTable(
        {
            language: {
                "url": "https://cdn.datatables.net/plug-ins/1.12.0/i18n/tr.json"
            }
        }
    );
    $('#currentCardAddressdatatable thead th').each(function () {
        var title = $(this).text();
        var classList = $(this).attr('class'); // Öðenin sýnýf listesini al

        if (classList && classList.indexOf('nonsearchable') !== -1) {
            $(this).text(title);
        } else {
            $(this).html('<input type="text" class="form-control rounded-pill" placeholder="' + title + '" />');

            $('input', this).on('keyup change', function () {
                if (datatable.column(i).search() !== this.value) {
                    datatable
                        .column(i)
                        .search(this.value)
                        .draw();
                }
            });
        }
    });

    //var datatable = $('#currentCardAddressdatatable').DataTable({
    //    "columns": [
    //        { "data": null, "orderable": false },
    //        { "data": 'id' },
    //        { "data": 'title'},
    //        { "data": 'countryId' },
    //        { "data": 'provinceId' },
    //        { "data": 'districtId' },
    //        { "data": 'villageId' },
    //    ],
    //    "columnDefs": [
    //        {
    //            "targets": [0],
    //            "render": function (data, type, row, meta) {
    //                return '<div class="form-check form-check-md d-flex align-items-center"><input class="form-check-input" type = "checkbox" value ="' + data.Id + '" id="checkebox-md"></div >';
    //            }
    //        },
    //        {
    //            "targets": [8],
    //            "render": function (data, type, row, meta) {
    //                return '<div class="hstack gap-2">' +
    //                    '<a aria-label="anchor" href="your_download_url/' + row.id + '" class="btn btn-icon btn-wave waves-effect waves-light btn-sm btn-success-light"><i class="ri-download-2-line"></i></a>' +
    //                    '<a aria-label="anchor" href="/CurrentCard/AddOrUpdateCurrentCard/' + row.id + '" class="btn btn-icon btn-wave waves-effect waves-light btn-sm btn-primary-light"><i class="ri-edit-line"></i></a>' +
    //                    '</div>';
    //            }
    //        },
    //    ],
    //    "orderCellsTop": true,
    //    "fixedHeader": true,
    //    "pageLength": 25,
    //    "pagingType": "full_numbers", // Tüm sayfalarý göster
    //    "lengthMenu": [[25, 50, 100, -1], [25, 50, 100, "Tümü"]],
    //    language: {
    //        "url": "https://cdn.datatables.net/plug-ins/1.12.0/i18n/tr.json"
    //    }
    //});

    $('#addlandnametocurrentcard').click(function(event){
        event.preventDefault();
        console.log("tiklandi");
        const url = '/CurrentCard/AddOrUpdateCurrentCardLandName/';
        const id = $(this).data('id');
        const currentcardid = $(this).data('currentcardid');
        const placeHolderDiv = $('#modalPlaceHolder');
        $.get(url, { id: id, currentcardid: currentcardid }).done(function (data) {
            placeHolderDiv.html(data);
            placeHolderDiv.find(".modal").modal('show');
        }).fail(function () {
            toastr.error("Bir hata meydana geldi.", "Hata!");
        });

        placeHolderDiv.on('click', '#saveaddorupdatecurrentcardlandname', function (event) {
            event.preventDefault();

            const form = $('#addorupdatecurrentcardlandnameform');

            const actionUrl = form.attr('action');

            const dataToSend = form.serialize();

            $.post(actionUrl, dataToSend)
                .done(function (data) {
                    if (data.resultStatus === 0) {
                        placeHolderDiv.find(".modal").modal('hide');
                        window.location.reload();
                    } else {
                        var html = '<ul>';
                        $.each(data.messages, function (index, message) {
                            html += '<li>' + message + '</li>';
                        });
                        html += '</ul>';

                        toastr.error(`${html}`, 'Hatalý Ýþlem!');
                    }
                })
                .fail(function (err) {
                    toastr.error(`${err.responseText}`, "Bir hata meydana geldi.");
                });

        });
    });

    $('.updatecurrentcardlandname').click(function (event) {
        event.preventDefault();
        
        const url = '/CurrentCard/AddOrUpdateCurrentCardLandName/';
        const id = $(this).data('id');
        const currentcardid = $(this).data('currentcardid');
        const placeHolderDiv = $('#modalPlaceHolder');
        $.get(url, { id: id, currentcardid: currentcardid }).done(function (data) {
            placeHolderDiv.html(data);
            placeHolderDiv.find(".modal").modal('show');
        }).fail(function () {
            toastr.error("Bir hata meydana geldi.", "Hata!");
        });

        placeHolderDiv.on('click', '#saveaddorupdatecurrentcardlandname', function (event) {
            event.preventDefault();

            const form = $('#addorupdatecurrentcardlandnameform');

            const actionUrl = form.attr('action');

            const dataToSend = form.serialize();

            $.post(actionUrl, dataToSend)
                .done(function (data) {
                    if (data.resultStatus === 0) {
                        placeHolderDiv.find(".modal").modal('hide');
                        window.location.reload();
                    } else {
                        var html = '<ul>';
                        $.each(data.messages, function (index, message) {
                            html += '<li>' + message + '</li>';
                        });
                        html += '</ul>';

                        toastr.error(`${html}`, 'Hatalý Ýþlem!');
                    }
                })
                .fail(function (err) {
                    toastr.error(`${err.responseText}`, "Bir hata meydana geldi.");
                });

        });
    });

   

});