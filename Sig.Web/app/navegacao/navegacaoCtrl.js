
app.controller('navegacaoCtrl', function ($scope, navegacaoService) {

    var vm = this

    vm.validarLogin = validarLogin;
    vm.sairDoLogin = sairDoLogin;

    vm.cache = localStorage.getItem("Autenticação");
    vm.perfil = null;

    vm.route = window.location.hash;

    function validarLogin(nome, senha) {
        vm.usuarioLogado = nome;
        vm.autenticacaoInvalida = false;
        navegacaoService.validarLogin(nome, senha)
        .then(function (obj) {
            localStorage.setItem("Autenticação", obj.data);
            localStorage.setItem("Empresa", nome);
            if (obj.data.Validacao == true) {
                vm.cache = true;
                $('#abrirLogin').modal('hide')
                vm.perfil = obj.data.Perfil;
                console.log(vm.perfil)
                vm.usuario = null;
                if (obj.data.Perfil == "Administrador") {
                    window.location.href = '#/gerenciarAdm';
                    vm.perfil = obj.data.Perfil;
                } else {
                    window.location.href = '#/gerenciar';
                }
            } else {
                vm.cache = false;
                vm.autenticacaoInvalida = true;
            }
        });
    };

    function sairDoLogin() {
        vm.cache = localStorage.setItem("Autenticação", false);
        vm.cache = false;
        window.location.href = '#/';
    }


});

