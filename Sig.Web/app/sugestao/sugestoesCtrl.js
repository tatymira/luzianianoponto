
app.controller('sugestoesCtrl', function ($scope, sugestaoService) {

    var vm = this;
    var idModalLancar = 0;
    var idModalFinalizar = 0;

    vm.modalLancar = modalLancar;
    vm.modalFinalizar = modalFinalizar;
    vm.lancar = lancar;
    vm.finalizar = finalizar;

    function listarSugestoes() {
        vm.nomeEmpresa = localStorage.getItem("Empresa");
        sugestaoService.listarSugestoes(vm.nomeEmpresa)
        .then(function (obj) {
            vm.sugestoes = obj.data;
        });
    }

    function modalLancar(id) {
        idModalLancar = id
        $("#modalLancar").modal("show");
    }
    function modalFinalizar(id) {
        idModalFinalizar = id
        $("#modalFinalizar").modal("show");
    }

    function lancar() {
        sugestaoService.lancar(idModalLancar)
        .then(function (obj) {
            window.location.reload();
        });
    }

    function finalizar() {
        sugestaoService.finalizar(idModalFinalizar)
        .then(function (obj) {
            window.location.reload();
        });
    }

    listarSugestoes();
    
});

