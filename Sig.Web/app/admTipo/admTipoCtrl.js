
app.controller('admTipoCtrl', function ($scope, $rootScope, linhasService, tiposService) {

    var vm = this

    vm.cadastrarTipo = cadastrarTipo;
    vm.editarTipo = editarTipo;
    vm.limparEscopo = limparEscopo;
    vm.excluirTipo = excluirTipo;
    vm.modalExcluirTipo = modalExcluirTipo;

    function cadastrarTipo(tipo) {
        tiposService.cadastrarTipo(tipo)
        .then(function (obj) {
            $("#cadastrarTipo").modal("hide");
            limparEscopo();
            listarTipos();
            delete vm.tipo.descricao;
        });
    };

    function listarTipos() {
        tiposService.listarTipos()
        .then(function (obj) {
            vm.tipos = obj.data;
        });
    };

    function editarTipo(Id) {
        tiposService.editarTipo(Id)
        .then(function (obj) {
            delete Id;
            vm.tipo = obj.data;
            vm.editando = true;
            vm.tituloModal = "Editar Tipo";
            $("#cadastrarTipo").modal("show");
        });
    };

    function excluirTipo(Id) {
        tiposService.excluirTipo(Id)
        .then(function (obj) {
            delete Id;
            $("#janelaDeConfirmacao").modal("hide");
            $("#cadastrarTipo").modal("hide");
            if (obj.data == "") {
                limparEscopo();
                listarTipos();
            } else {
                vm.mensagemDeError = obj.data;
                $("#mensagemDeErro").modal("show");
            }

        });
    }

    function limparEscopo() {
        vm.tituloModal = "Cadastrar Tipo";
        delete vm.tipo;
    }

    function modalExcluirTipo(tipo) {
        vm.idTipoParaModal = tipo.Id;
        vm.nomeTipoParaModal = tipo.Descricao;
    }

    function main() {
        listarTipos();
        vm.tituloModal = "Cadastrar Tipo";
    }

    main();

});