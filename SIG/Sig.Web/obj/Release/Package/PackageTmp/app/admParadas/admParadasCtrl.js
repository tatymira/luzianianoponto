
app.controller('admParadasCtrl', function ($scope, paradasService) {

    var vm = this;

    vm.salvarParadas = salvarParadas;

    vm.lstparadas = new Array();
    vm.qtdParadas = 0

    var mymap = L.map('mapaparadas').setView([-16.24, -47.91], 13);
    L.tileLayer('http://{s}.google.com/vt/lyrs=s&x={x}&y={y}&z={z}', {
        maxZoom: 20,
        subdomains: ['mt0', 'mt1', 'mt2', 'mt3'],
        attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(mymap);

    var marker;

    mymap.on('click', onMapClick);

    function onMapClick(e) {
        marker = L.marker([e.latlng.lat, e.latlng.lng]);
        vm.lstparadas.push(marker);
        mymap.addLayer(marker);
        vm.qtdParadas = vm.qtdParadas + 1;
        marker.on('click', deleteMarker);
    }

    function deleteMarker(e) {
        for (i = 0; i < vm.lstparadas.length; i++) {
            if (vm.lstparadas[i] != null) {
                if (vm.lstparadas[i]._latlng.lat == e.latlng.lat) {
                    if (vm.lstparadas[i]._latlng.lng == e.latlng.lng) {
                        vm.qtdParadas = vm.qtdParadas - 1;
                        mymap.removeLayer(vm.lstparadas[i]);
                        delete vm.lstparadas[i];
                    }
                }
            }
        }
    }

    function salvarParadas() {
        var listaParaSalvar = new Array();
        for (i = 0; i < vm.lstparadas.length; i++) {
            if (vm.lstparadas[i] != null) {
                listaParaSalvar.push({ latitude: vm.lstparadas[i]._latlng.lat, longitude: vm.lstparadas[i]._latlng.lng });
                mymap.removeLayer(vm.lstparadas[i]);
                vm.qtdParadas = vm.qtdParadas - 1;
            }
        }
        paradasService.cadastrarParadas(listaParaSalvar)
        .then(function (obj) {
            vm.lstparadas = [];
            renderizarParadas();
        });
    }

    function renderizarParadas() {
        paradasService.renderizarParadas()
        .then(function (obj) {
            vm.botaoDesabilitado = false;
            for (x = 0; x < obj.data.length; x++) {
                var e = {
                    latlng: { lat: obj.data[x].Latitude, lng: obj.data[x].Longitude }
                };
                onMapClick(e)
            }
        });
    }

    function main() {
        var terminal = L.icon({
            iconUrl: 'assets/css/images/icon_init.png',

            iconSize: [30, 25],
            iconAnchor: [0, 25]
        });
        L.marker([-16.258870356597015, -47.95935451985088], { icon: terminal }).addTo(mymap);

        renderizarParadas();
    }

    main();

});