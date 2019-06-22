
app.service('empresaService', function ($http, $routeParams, configService) {
    return {
        cadastrarEmpresa: cadastrarEmpresa,
        listarEmpresas: listarEmpresas,
        editarEmpresa: editarEmpresa,
        excluirEmpresa: excluirEmpresa,
        cadastrarSenha: cadastrarSenha
    }

    function cadastrarEmpresa(empresa) {
        jsonPostData = {
            empresa: empresa
        }
        var request = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            url: configService.urlApi + 'empresa',
            withCredentials: true,
            data: jsonPostData
        }
        return $http(request);
    }

    function listarEmpresas() {
        var request = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
            url: configService.urlApi + 'empresa/listar',
            withCredentials: true
        }
        return $http(request);
    }

    function editarEmpresa(id) {
        var request = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
            url: configService.urlApi + 'empresa',
            params: { id: id },
            withCredentials: true
        }
        return $http(request);
    }

    function excluirEmpresa(id) {
        var request = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
            url: configService.urlApi + 'empresa/excluir',
            params: { id: id },
            withCredentials: true
        }
        return $http(request);
    }

    function cadastrarSenha(senha1, senha2, cnpj) {
        jsonPostData = {
            senha1: senha1,
            senha2: senha2,
            cnpj: cnpj
        }
        var request = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            url: configService.urlApi + 'empresa/cadastrar-senha',
            withCredentials: true,
            data: jsonPostData
        }
        return $http(request);
    }
    

});