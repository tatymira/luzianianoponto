
app.service('inicioService', function ($http, configService) {
    return {
        pesquisaInicial: pesquisaInicial,
        listaralgo: listaralgo
    }

    function pesquisaInicial(pesquisa) {
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

    function listaralgo() {
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


});