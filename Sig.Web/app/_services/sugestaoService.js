app.service('sugestaoService', function ($http, configService) {

    return {
        enviarSugestao: enviarSugestao,
        listarSugestoes: listarSugestoes,
        lancar: lancar,
        finalizar: finalizar
    }

    function enviarSugestao(_sugestao, _empresaId, _linhaId) {
        jsonPostData = {
            sugestao: _sugestao,
            empresaId: _empresaId, 
            linhaId: _linhaId
        }
        var request = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            url: configService.urlApi + 'sugestao',
            withCredentials: true,
            data: jsonPostData
        }
        return $http(request);
    }

    function listarSugestoes(nomeEmpresa) {
        var request = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
            url: configService.urlApi + 'sugestoes/listar',
            params: { nomeEmpresa: nomeEmpresa},
            withCredentials: true
        }
        return $http(request);
    }

    function lancar(_idModal) {
        jsonPostData = {
            idModal: _idModal
        }
        var request = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            url: configService.urlApi + 'sugestao/lancar',
            withCredentials: true,
            data: jsonPostData
        }
        return $http(request);
    }

    function finalizar(_idSugestao) {
        jsonPostData = {
            idSugestao: _idSugestao
        }
        var request = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            url: configService.urlApi + 'sugestao/finalizar',
            withCredentials: true,
            data: jsonPostData
        }
        return $http(request);
    }

});