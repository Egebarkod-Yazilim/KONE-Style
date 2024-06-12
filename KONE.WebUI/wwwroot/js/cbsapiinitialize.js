$(document).ready(function () {

    // Harita oluşturma
    var map = L.map('map').setView([39.9334, 32.8597], 10);


    //L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    //    attribution: '© <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
    //}).addTo(map);
    //var map = L.map('map').setView([-41.2858, 174.78682], 14);
    mapLink =
        '<a href="http://www.esri.com/">Esri</a>';
    wholink =
        'i-cubed, USDA, USGS, AEX, GeoEye, Getmapping, Aerogrid, IGN, IGP, UPR-EGP, and the GIS User Community';
    googleHybrid = L.tileLayer('http://{s}.google.com/vt/lyrs=s,h&x={x}&y={y}&z={z}', {
        maxZoom: 20,
        subdomains: ['mt0', 'mt1', 'mt2', 'mt3']
    }).addTo(map);




    var geojsonLayer;
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    var attributionControl = document.querySelector('.leaflet-control-attribution.leaflet-control');
    if (attributionControl) {
        attributionControl.textContent = 'KONFRUT AG KONE Parsel Sorgulama Ekranı';
    }


    $('#selectedProvinceId').change(function () {


        toastr.info('Bu işlem biraz vakit alabilir. Sistemde olmayan veriler Tapu kadastro parsel sorgulama sistmeinden çekilirken, olan veriler sistemden çekilmektedir. İşlem bitince ilçeler dolacaktır.', 'Veriler Çekiliyor!');

        var selectElement1 = $('#selectedDistrictId');
        selectElement1.empty();
        var selectElement2 = $('#selectedVillageId');
        selectElement2.empty();

        var selectedId = $(this).val();
        // Fetch city data from API
        $.ajax({
            url: '/Home/GetProvinceCoordinates', // API endpoint
            data: { provinceId: selectedId },
            method: 'GET',
            success: function (response) {
                // Clear previous layers if any
                if (geojsonLayer && map.hasLayer(geojsonLayer)) {
                    map.removeLayer(geojsonLayer);
                }

                // Draw polygons
                var geojsonFeature = response;

                geojsonLayer = L.geoJSON(geojsonFeature, {
                    style: function (feature) {
                        console.log(feature);
                        return {
                            fillColor: '#6118ff', // Change fill color as needed
                            weight: 1,
                            opacity: 1,
                            color: 'yellow',
                            fillOpacity: 0.7
                        };
                    }
                }).addTo(map);

                // Zoom to fit the bounds of the drawn polygon
                map.fitBounds(geojsonLayer.getBounds());


            },
            error: function (error) {
                toastr.warning('İl kordinatları getirilemedi.', 'İşlem Başarısız!');

                console.error('Error fetching city data:', error);
            }
        });


        $.ajax({
            url: '/Home/DistrictList', // Replace with your API endpoint
            data: { provinceId: selectedId },
            method: 'GET',
            success: function (response) {
                console.log(response);

                var selectElement = $('#selectedDistrictId');
                selectElement.empty();

                var features = response.features; // Veri dizisi

                // Her bir öğe için döngü
                features.forEach(function (feature) {
                    var id = feature.properties.id; // İlçe ID'si
                    var text = feature.properties.text; // İlçe adı

                    // Eğer ilçe adı null değilse, dropdown listesine ekle
                    if (text !== null) {
                        var option = $('<option></option>').attr('value', id).text(text);
                        selectElement.append(option);
                    }
                });

                toastr.success('İlçeler getirildi. Lütfen ilçe seçerek ilerleyiniz.', 'İşlem Başarılı!');
            },
            error: function (error) {
                toastr.warning('İlçeler getirilemedi.', 'İşlem Başarısız!');
                console.error('Error fetching city data:', error);
            }
        });
    });



    $('#selectedDistrictId').change(function () {
        toastr.info('Bu işlem biraz vakit alabilir. Sistemde olmayan veriler Tapu kadastro parsel sorgulama sistmeinden çekilirken, olan veriler sistemden çekilmektedir. İşlem bitince mahalleler dolacaktır.', 'Veriler Çekiliyor!');

        var selectElement = $('#selectedVillageId');
        selectElement.empty();

        var selectedId = $(this).val();
        // Fetch city data from API
        $.ajax({
            url: '/Home/GetDistrictCoordinates', // API endpoint
            data: { districtId: selectedId },
            method: 'GET',
            success: function (response) {
                // Clear previous layers if any
                if (geojsonLayer && map.hasLayer(geojsonLayer)) {
                    map.removeLayer(geojsonLayer);
                }

                // Draw polygons
                var geojsonFeature = response;

                geojsonLayer = L.geoJSON(geojsonFeature, {
                    style: function (feature) {
                        console.log(feature);
                        return {
                            fillColor: '#6118ff', // Change fill color as needed
                            weight: 1,
                            opacity: 1,
                            color: 'yellow',
                            fillOpacity: 0.7
                        };
                    }
                }).addTo(map);

                // Zoom to fit the bounds of the drawn polygon
                map.fitBounds(geojsonLayer.getBounds());
            },
            error: function (error) {
                toastr.warning('İlçe Kordinatları getirilemedi.', 'İşlem Başarısız!');

                console.error('Error fetching city data:', error);
            }
        });


        $.ajax({
            url: '/Home/VillageList', // Replace with your API endpoint
            data: { districtId: selectedId },
            method: 'GET',
            success: function (response) {
                console.log(response);

                var selectElement = $('#selectedVillageId');
                selectElement.empty();

                var features = response.features; // Veri dizisi

                // Her bir öğe için döngü
                features.forEach(function (feature) {
                    var id = feature.properties.id; // İlçe ID'si
                    var text = feature.properties.text; // İlçe adı

                    // Eğer ilçe adı null değilse, dropdown listesine ekle
                    if (text !== null) {
                        var option = $('<option></option>').attr('value', id).text(text);
                        selectElement.append(option);
                    }
                });

                toastr.success('Mahalleler getirildi. Lütfen ilçe seçerek ilerleyiniz.', 'İşlem Başarılı!');
            },
            error: function (error) {
                toastr.warning('Mahalleler getirilemedi.', 'İşlem Başarısız!');

                console.error('Error fetching city data:', error);
            }
        });
    });



    $('#selectedVillageId').change(function () {
        var selectedId = $(this).val();
        // Fetch city data from API
        $.ajax({
            url: '/Home/GetVillageCoordinates', // API endpoint
            data: { villageId: selectedId },
            method: 'GET',
            success: function (response) {
                // Clear previous layers if any
                if (geojsonLayer && map.hasLayer(geojsonLayer)) {
                    map.removeLayer(geojsonLayer);
                }

                // Draw polygons
                var geojsonFeature = response;

                geojsonLayer = L.geoJSON(geojsonFeature, {
                    style: function (feature) {
                        console.log(feature);
                        return {
                            fillColor: '#6118ff', // Change fill color as needed
                            weight: 1,
                            opacity: 1,
                            color: 'yellow',
                            fillOpacity: 0.7
                        };
                    }
                }).addTo(map);

                // Zoom to fit the bounds of the drawn polygon
                map.fitBounds(geojsonLayer.getBounds());
            },
            error: function (error) {
                toastr.warning('Mahalle/Köy Kordinatları getirilemedi.', 'İşlem Başarısız!');
                console.error('Error fetching city data:', error);
            }
        });
    });

    $('#submitparcelformbutton').click(function (event) {
        console.log("tiklandi.");
        var villageId = $('#selectedVillageId').val();
        var parcelNo = $('#ParcelNo').val();
        var landNo = $('#LandNo').val();

        console.log(landNo);
        $.ajax({
            url: '/Home/GetParcelCoordinates', // API endpoint
            data: { villageId: villageId, land: landNo, parcel: parcelNo },
            method: 'GET',
            success: function (response) {
                console.log(response);

                if (response.resultStatus === 1) {
                    toastr.warning('Parsel alanı bulunamadı.', 'İşlem Başarısız!');

                } else {
                    // Clear previous layers if any
                    if (geojsonLayer && map.hasLayer(geojsonLayer)) {
                        map.removeLayer(geojsonLayer);
                    }

                    // Draw polygons
                    var geojsonFeature = response.data;

                    geojsonLayer = L.geoJSON(geojsonFeature, {
                        style: function (feature) {
                            console.log(feature);
                            return {
                                fillColor: '#6118ff', // Change fill color as needed
                                weight: 1,
                                opacity: 1,
                                color: 'yellow',
                                fillOpacity: 0.7
                            };
                        },
                        onEachFeature: function (feature, layer) {
                            var bounds = layer.getBounds(); // Katmanın sınırlarını al
                            var center = bounds.getCenter(); // Katmanın merkezini hesapla

                            // Metni oluştur
                            var text = feature.properties.ozet;

                            // Metni haritaya ekle
                            L.marker(center, {
                                icon: L.divIcon({
                                    className: 'text-label text-white',
                                    html: '<div>' + text + '</div>',
                                    iconSize: [100, 40]
                                })
                            }).addTo(map);
                        }
                    }).addTo(map);

                    // Zoom to fit the bounds of the drawn polygon
                    map.fitBounds(geojsonLayer.getBounds());

                    toastr.success('Ada/Parsel sorgusu başarıyla gerçekleştirilmiştir.', 'İşlem Başarılı!');

                }


            },
            error: function (error) {
                toastr.warning('Parsel alanı bulunamadı.', 'İşlem Başarısız!');

                console.error('Error fetching city data:', error);
            }
        });
    });
});