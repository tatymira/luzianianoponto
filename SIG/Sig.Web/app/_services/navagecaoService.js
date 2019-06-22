app.service('navegacaoService', function ($http, configService) {

    return {
        validarLogin: _validarLogin
    }

    function _validarLogin(_nome, _senha) {
        var request = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
            url: configService.urlApi + 'navegacao/logar',
            params: { nome: _nome, senha: _senha },
            withCredentials: true
        }
        return $http(request);
    }


});