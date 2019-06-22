
app.controller('cadastrarLinhasCtrl', function ($scope, $routeParams, linhasService, tiposService, paradasService) {

    var vm = this;
    var indice;
    var esquina = null;

    vm.lstHorarios = [];
    vm.linhaTrajeto = new Array();
    vm.lstparadas = new Array();

    vm.cadastrarLinha = cadastrarLinha;
    vm.addHorario = addHorario;
    vm.editarLinha = editarLinha;
    vm.resetarDesenho = resetarDesenho;
    vm.limparEscopo = limparEscopo;
    vm.excluirLinha = excluirLinha;
    vm.addHorarioNaLista = addHorarioNaLista;
    vm.limparHora = limparHora;
    vm.mostrarParadas = mostrarParadas;
    vm.aproveitarLinha = aproveitarLinha;


    var mymap = L.map('mapcriarlinha').setView([-16.24, -47.91], 13);
    L.tileLayer('http://{s}.google.com/vt/lyrs=s&x={x}&y={y}&z={z}', {
        maxZoom: 20,
        subdomains: ['mt0', 'mt1', 'mt2', 'mt3'],
        attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(mymap);


    var marker;
    var polyline;

    mymap.on('click', onMapClick);

    function onMapClick(e) {
        esquina = e.latlng;
        vm.linhaTrajeto.push(esquina);
        polyline = L.polyline(vm.linhaTrajeto,
            {
                color: 'orange',
                weight: 5
            }
            ).addTo(mymap);
    }

    function resetarDesenho() {
        for (i in mymap._layers) {
            if (mymap._layers[i]._path != undefined) {
                try {
                    mymap.removeLayer(mymap._layers[i]);
                }
                catch (e) {
                    console.log("problem with " + e + mymap._layers[i]);
                }
            }
        }
        vm.linhaTrajeto = [];
    };

    function listarTipos() {
        tiposService.listarTipos()
        .then(function (obj) {
            vm.tipos = obj.data;
        });
    };

    function editarLinha(id) {
        linhasService.getLinha(id)
        .then(function (obj) {
            for (x = 0; x < obj.data.LinhaMap.length; x++) {
                var e = {
                    latlng: { lat: obj.data.LinhaMap[x].Lat, lng: obj.data.LinhaMap[x].Lng }
                };
                onMapClick(e)
            }
            vm.linha = obj.data;
            vm.lstHorarios = obj.data.Horarios;
        });
    }

    function addHorarioNaLista() {
        var x = document.getElementById("myText");
    }

    function addHorario(HoraSaida) {
        var horario = {
            HoraSaida: HoraSaida
        }
        vm.lstHorarios.push(horario);
        delete vm.HoraSaida;
        $("#modalAddHorario").modal("hide");
    }

    function cadastrarLinha(dadosLinha) {
        vm.nomeEmpresa = localStorage.getItem("Empresa");
        dadosLinha.LinhaMap = null;
        linhasService.cadastrarLinha(dadosLinha, vm.lstHorarios, vm.linhaTrajeto, vm.nomeEmpresa)
        .then(function (obj) {
            window.location.href = '#/admLinhas';
        });
    }


    function excluirLinha() {
        linhasService.excluirLinha($routeParams.id)
        .then(function (obj) {
            $("#janelaDeConfirmacao").modal("hide");
            $("#exclusaoSuccess").modal("show");
            setInterval(function () {
                $("#exclusaoSuccess").modal("hide");
                setInterval(function () {
                    window.location.href = '#/admLinhas';
                }, 1000);
            }, 5000);
        });
    }

    function limparHora() {
        delete vm.HoraSaida;
    }

    function limparEscopo() {
        if (vm.linha != null) {
            if (vm.linha.Numero != null) {
                vm.linha.Numero = null;
            }
        }
        delete vm.linha;
        resetarDesenho();
        vm.lstHorarios = [];
    }

    function GetParadas(e) {
        marker = L.marker([e.latlng.lat, e.latlng.lng]);
        vm.lstparadas.push(marker);
        mymap.addLayer(marker);
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

    function listarLinhasEmpresa() {
        var nomeEmpresa = localStorage.getItem("Empresa");
        linhasService.listarLinhasEmpresa(nomeEmpresa)
        .then(function (obj) {
            console.log(obj);
            vm.linhasDaEmpresa = obj.data;
        });
    };

    function aproveitarLinha(linha) {
        resetarDesenho();
        linhasService.getLinha(linha.Id)
        .then(function (obj) {
            for (x = 0; x < obj.data.LinhaMap.length; x++) {
                var e = {
                    latlng: { lat: obj.data.LinhaMap[x].Lat, lng: obj.data.LinhaMap[x].Lng }
                };
                onMapClick(e)
            }
        });
    }

    function main() {
        listarLinhasEmpresa();
        listarTipos();
        $(document).ready(function () {
            $('#horasaida').mask('00:00');
            $('#horachegada').mask('00:00');
        });
        if ($routeParams.id != null)
            editarLinha($routeParams.id);

        var terminal = L.icon({
            iconUrl: 'assets/css/images/icon_init.png',

            iconSize: [30, 25],
            iconAnchor: [0, 25]
        });
        L.marker([-16.258870356597015, -47.95935451985088], { icon: terminal }).addTo(mymap);

    }

    main();


});