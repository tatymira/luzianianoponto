app.service('paradasService', function ($http, configService) {

    return {
        cadastrarParadas: cadastrarParadas,
        renderizarParadas: renderizarParadas
    }

    function cadastrarParadas(lstparadas) {
        jsonPostData = {
            lstparadas: lstparadas
        }
        var request = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            url: configService.urlApi + 'parada',
            withCredentials: true,
            data: jsonPostData
        }
        return $http(request);
    }

    function renderizarParadas() {
        var request = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
            url: configService.urlApi + 'parada/renderizar',
            withCredentials: true
        }
        return $http(request);
    }


});