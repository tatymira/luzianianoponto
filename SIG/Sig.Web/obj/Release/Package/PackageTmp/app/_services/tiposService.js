
app.service('tiposService', function ($http, configService) {
    return {
        cadastrarTipo: cadastrarTipo,
        listarTipos: listarTipos,
        editarTipo: editarTipo,
        excluirTipo: excluirTipo
    }

    function cadastrarTipo(tipo) {
        jsonPostData = {
            tipo: tipo
        }
        var request = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            url: configService.urlApi + 'tipo',
            withCredentials: true,
            data: jsonPostData
        }
        return $http(request);
    }

    function listarTipos() {
        var request = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
            url: configService.urlApi + 'tipo',
            withCredentials: true
        }
        return $http(request);
    }

    function editarTipo(id) {
        var request = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
            url: configService.urlApi + 'tipo/editar',
            params: { id: id },
            withCredentials: true
        }
        return $http(request);
    }

    function excluirTipo(id) {
        var request = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
            url: configService.urlApi + 'tipo/excluir',
            params: { id: id },
            withCredentials: true
        }
        return $http(request);
    }

});