
app.controller('gerenciarCtrl', function ($scope, sugestaoService) {

    var vm = this;

    function contarNotificacoes() {
        vm.nomeEmpresa = localStorage.getItem("Empresa");
        sugestaoService.listarSugestoes(vm.nomeEmpresa)
        .then(function (obj) {
            if(obj.data.length > 0){
                vm.mostrarNotificacoes = true;
                vm.notificacoes = obj.data.length;
            }
        });
    }

    function main() {
        contarNotificacoes();
    }

    main();
});

