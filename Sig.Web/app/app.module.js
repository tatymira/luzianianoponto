
var app = angular.module('Sig', ['ngRoute']);


app.config(function ($routeProvider) {

    $routeProvider
        .when('/', { templateUrl: '/app/principal/principal.html', controller: 'principalCtrl as vm' })
        .when('/linhas', { templateUrl: '/app/linhas/linhas.html', controller: 'linhasCtrl as vm' })
        .when('/linhas/:filtro', { templateUrl: '/app/linhas/linhas.html', controller: 'linhasCtrl as vm' })
        .when('/detalhamento/:id', { templateUrl: '/app/detalhamento/detalhamento.html', controller: 'detalhamentoCtrl as vm' })
        .when('/gerenciar', { templateUrl: '/app/gerenciar/gerenciar.html', controller: 'gerenciarCtrl as vm' })
        .when('/gerenciarAdm', { templateUrl: '/app/gerenciar/gerenciarAdm.html', controller: 'gerenciarCtrl as vm' })
        .when('/admLinhas', { templateUrl: '/app/admLinhas/admLinhas.html', controller: 'admLinhasCtrl as vm' })
        .when('/cadastrarLinhas', { templateUrl: '/app/admLinhas/cadastrarLinhas.html', controller: 'cadastrarLinhasCtrl as vm' })
        .when('/cadastrarLinhas/:id', { templateUrl: '/app/admLinhas/cadastrarLinhas.html', controller: 'cadastrarLinhasCtrl as vm' })
        .when('/admParadas', { templateUrl: '/app/admParadas/admParadas.html', controller: 'admParadasCtrl as vm'})
        .when('/admOnibus', { templateUrl: '/app/admOnibus/admOnibus.html', controller: 'admOnibusCtrl as vm' })
        .when('/admEmpresas', { templateUrl: '/app/admEmpresas/admEmpresas.html', controller: 'admEmpresasCtrl as vm' })
        .when('/admTipos', { templateUrl: '/app/admTipo/admTipo.html', controller: 'admTipoCtrl as vm' })
        .when('/admEmpresas/:id', { templateUrl: '/app/admEmpresas/cadastrarSenha.html', controller: 'admEmpresasCtrl as vm' })
        .when('/sugestoes', { templateUrl: '/app/sugestao/sugestoes.html', controller: 'sugestoesCtrl as vm' })
        .otherwise({ redirectTo: "/" });
});