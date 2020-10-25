app.service('linhasService', function ($http, configService) {

    return {
        listarLinhasEmpresa: listarLinhasEmpresa,
        pesquisaLinhas: pesquisaLinhas,
        cadastrarLinha: cadastrarLinha,
        getLinha: getLinha,
        excluirLinha: excluirLinha
    }

    function listarLinhasEmpresa(nomeEmpresa) {
        var request = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
            url: configService.urlApi + 'linha/listar-empresa',
            params: { nomeEmpresa: nomeEmpresa },
            withCredentials: true
        }
        return $http(request);
    }

    function pesquisaLinhas(filtro) {
        var request = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
            url: configService.urlApi + 'linha/pesquisar',
            params: { filtro: filtro },
            withCredentials: true
        }
        return $http(request);
    }

    function cadastrarLinha(_dadosLinha, _lstHorarios, _linhaTrajeto, _nomeEmpresa) {
        jsonPostData = {
            dadosLinha: _dadosLinha,
            lstHorarios: _lstHorarios,
            linhaTrajeto: _linhaTrajeto,
            nomeEmpresa: _nomeEmpresa
        }
        var request = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            url: configService.urlApi + 'linha',
            withCredentials: true,
            data: jsonPostData
        }
        return $http(request);
    }

    function getLinha(_id) {
        var request = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
            url: configService.urlApi + 'linha',
            params: { id: _id },
            withCredentials: true
        }
        return $http(request);
    }

    function excluirLinha(_id) {
        jsonPostData = {
            id: _id
        }
        var request = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            url: configService.urlApi + 'linha/excluir',
            withCredentials: true,
            data: jsonPostData
        }
        return $http(request);
    }


});