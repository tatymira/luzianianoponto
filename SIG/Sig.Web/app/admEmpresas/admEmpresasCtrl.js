
app.controller('admEmpresasCtrl', function ($scope, $routeParams, empresaService) {

    var vm = this

    vm.editarEmpresa = editarEmpresa;
    vm.cadastrarEmpresa = cadastrarEmpresa;
    vm.limparEscopo = limparEscopo;
    vm.modalExcluirEmpresa = modalExcluirEmpresa;
    vm.excluirEmpresa = excluirEmpresa;
    vm.cadastrarSenha = cadastrarSenha;

    function cadastrarEmpresa(empresa) {
        empresaService.cadastrarEmpresa(empresa)
        .then(function (obj) {
            $("#cadastrarEmpresa").modal("hide");
            limparEscopo();
            listarEmpresas();
        });
    };

    function listarEmpresas() {
        empresaService.listarEmpresas()
        .then(function (obj) {
            vm.listEmpresas = obj.data;
        });
    };

    function editarEmpresa(id) {
        empresaService.editarEmpresa(id)
        .then(function (obj) {
            delete id;
            vm.empresa = obj.data;
            vm.tituloModal = "Editar Empresa";
            $("#cadastrarEmpresa").modal("show");
        });
    }

    function modalExcluirEmpresa(empresa) {
        vm.idEmpresaParaModal = empresa.Id
        vm.nomeEmpresaParaModal = empresa.Nome;
    }

    function excluirEmpresa(Id) {
        empresaService.excluirEmpresa(Id)
        .then(function (obj) {
            delete Id;
            $("#janelaDeConfirmacao").modal("hide");
            $("#cadastrarEmpresa").modal("hide");
            if (obj.data == "") {
                limparEscopo();
                listarEmpresas();
            } else {
                vm.mensagemDeError = obj.data;
                $("#mensagemDeErro").modal("show");
            }
        });
    };


    function cadastrarSenha(senha1, senha2) {
        var cnpj = $routeParams.id;
        empresaService.cadastrarSenha(senha1, senha2, cnpj)
        .then(function (obj) {
            $("#senhaRegistrada").modal("show");
            setTimeout(function () {
                $("#senhaRegistrada").modal("hide");
                setTimeout(function () {
                    window.location.href = '#/';
                }, 1000);
            }, 5000);
        });
    };


    function limparEscopo() {
        vm.tituloModal = "Cadastrar Empresa";
        vm.empresa = null;
    }

    function main() {
        vm.tituloModal = "Cadastrar Empresa";
        listarEmpresas();
    }

    main();

});