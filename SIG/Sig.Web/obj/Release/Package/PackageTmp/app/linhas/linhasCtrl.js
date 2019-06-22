
app.controller('linhasCtrl', function ($scope, $routeParams, linhasService) {

    var vm = this;
    var filtro = $routeParams.filtro;

    function listar() {
        linhasService.pesquisaLinhas(filtro)
        .then(function (obj) {
            vm.pesquisa = obj.data;
        });
    };

    function main() {
        listar();
    }

    main();

});