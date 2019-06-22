
app.controller('detalhamentoCtrl', function ($scope, $routeParams, linhasService, paradasService, sugestaoService) {

    var vm = this;
    var esquina = null;
    var id = $routeParams.id;

    vm.mostrarParadas = mostrarParadas;
    vm.enviarSugestao = enviarSugestao;

    vm.lstHorarios = [];
    vm.linhaTrajeto = new Array();
    vm.lstparadas = new Array();

    var mymap = L.map('mapdetalharlinha').setView([-16.24, -47.91], 13);
    L.tileLayer('http://{s}.google.com/vt/lyrs=s&x={x}&y={y}&z={z}', {
        maxZoom: 20,
        subdomains: ['mt0', 'mt1', 'mt2', 'mt3'],
        attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(mymap);


    var marker;
    var polyline;

    function GetLinha(e) {
        esquina = e.latlng;
        vm.linhaTrajeto.push(esquina);
        polyline = L.polyline(vm.linhaTrajeto,
            {
                color: 'orange',
                weight: 5
            }
            ).addTo(mymap);
    }

    function GetParadas(e) {
        marker = L.marker([e.latlng.lat, e.latlng.lng]);
        vm.lstparadas.push(marker);
        mymap.addLayer(marker);
    }

    function detalharLinha() {
        linhasService.getLinha(id)
        .then(function (obj) {
            vm.detalhes = obj.data;

            for (x = 0; x < obj.data.LinhaMap.length; x++) {
                var e = {
                    latlng: { lat: obj.data.LinhaMap[x].Lat, lng: obj.data.LinhaMap[x].Lng }
                };
                GetLinha(e)
            }
        });
    };

    function renderizarParadas() {
        paradasService.renderizarParadas()
        .then(function (obj) {
            for (x = 0; x < obj.data.length; x++) {
                var e = {
                    latlng: { lat: obj.data[x].Latitude, lng: obj.data[x].Longitude }
                };
                GetParadas(e)
            }
        });
    }

    function mostrarParadas(todasParadas) {
        if (todasParadas) {
            renderizarParadas();
        } else {
            for (i = 0; i < vm.lstparadas.length; i++) {
                if (vm.lstparadas[i] != null) {
                    mymap.removeLayer(vm.lstparadas[i]);
                    delete vm.lstparadas[i];
                }
            }
        }
    }

    function enviarSugestao(sugestao, empresaId, linhaId) {
        sugestaoService.enviarSugestao(sugestao, empresaId, linhaId)
        .then(function (obj) {

        });
    }

    function main() {
        detalharLinha();
        var terminal = L.icon({
            iconUrl: 'assets/css/images/icon_init.png',

            iconSize: [30, 25],
            iconAnchor: [0, 25]
        });
        L.marker([-16.258870356597015, -47.95935451985088], { icon: terminal }).addTo(mymap);

    };

    main();

});