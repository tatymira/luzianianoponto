
app.controller('admLinhasCtrl', function ($scope, linhasService, tiposService) {

    var vm = this;
    var indice;
    vm.lsthorarios = [];
    vm.linhaTrajeto = new Array();

    vm.listarTipos = listarTipos;

    function listarLinhasEmpresa() {
        var nomeEmpresa = localStorage.getItem("Empresa");
        linhasService.listarLinhasEmpresa(nomeEmpresa)
        .then(function (obj) {
            vm.listalinhas = obj.data;
        });
    };

    function listarTipos() {
        tiposService.listarTipos()
        .then(function (obj) {
            vm.tipos = obj.data;
        });
    };

    listarLinhasEmpresa();

});