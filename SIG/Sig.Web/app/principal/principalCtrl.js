
app.controller('principalCtrl', function ($scope, $window, paradasService) {

    var vm = this;

    vm.lstparadas = new Array();
    vm.mensagemInicial = true;

    vm.tipoDePesquisa = tipoDePesquisa;
    vm.pesquisaInicial = pesquisaInicial;


    var mymap = L.map('mapprincipalparadas').setView([-16.24, -47.91], 13);
    L.tileLayer('http://{s}.google.com/vt/lyrs=s&x={x}&y={y}&z={z}', {
        maxZoom: 20,
        subdomains: ['mt0', 'mt1', 'mt2', 'mt3'],
        attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(mymap);
    //L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png', {
    //    attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
    //}).addTo(mymap);
    var marker;

    function onMapClick(e) {
        marker = L.marker([e.latlng.lat, e.latlng.lng]);
        vm.lstparadas.push(marker);
        mymap.addLayer(marker);
    }

    function renderizarParadas() {
        paradasService.renderizarParadas()
        .then(function (obj) {
            for (x = 0; x < obj.data.length; x++) {
                var e = {
                    latlng: { lat: obj.data[x].Latitude, lng: obj.data[x].Longitude }
                };
                onMapClick(e)
            }
        });
    }

    function tipoDePesquisa(tipo) {
        delete vm.filtro;
        vm.pesquisa = tipo;
        vm.mensagemInicial = false;
    }

    function pesquisaInicial(filtro, tipo) {
        if (filtro.Trajeto != null) {
            $window.location.href = "#/linhas/" + tipo +"&"+ filtro.Trajeto;
        } else if (filtro.Linha != null) {
            $window.location.href = "#/linhas/" + tipo +"&"+ filtro.Linha;
        } else {
            $window.location.href = "#/linhas/" + tipo + "&" + filtro.HorarioUm + "-" + filtro.HorarioDois;
        }

    }

    function main() {
        renderizarParadas();
        $(document).ready(function () {
            $('#horarioUm').mask('00:00');
            $('#horarioDois').mask('00:00');
        });
    }

    main();

});