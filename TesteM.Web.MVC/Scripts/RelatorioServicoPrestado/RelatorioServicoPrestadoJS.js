function DataTablesConfig() {

    var table = $("#tbServicoPrestadoRelatorio").dataTable({
        "pageLength": 10,
        "order": [[0, "asc"]],
        "language": {
            "sEmptyTable": "Nenhum registro encontrado",
            "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
            "sInfoFiltered": "(Filtrados de _MAX_ registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "_MENU_ resultados por página",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Pesquisar",
            "oPaginate": {
                "sNext": "Próximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Último"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        },
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'collection',
                text: 'Export',
                buttons: [
                    'copy',
                    'excel',
                    'csv',
                    'pdf',
                    'print'
                ]
            }
        ]
    });

    jQuery.extend(jQuery.validator.messages, {
        required: "Campo obrigatório!",
        remote: "Por favor, corrija este campo.",
        email: "Por favor, forneça um endereço eletrônico válido.",
        url: "Por favor, forneça uma URL válida.",
        date: "Por favor, forneça uma data válida.",
        dateISO: "Por favor, forneça uma data válida (ISO).",
        number: "Por favor, forneça um número válido.",
        digits: "Por favor, forneça somente dígitos.",
        creditcard: "Por favor, forneça um cartão de crédito válido.",
        equalTo: "Por favor, forneça o mesmo valor novamente.",
        accept: "Por favor, forneça um valor com uma extensão válida.",
        maxlength: jQuery.validator.format("Por favor, forneça não mais que {0} caracteres."),
        minlength: jQuery.validator.format("Por favor, forneça ao menos {0} caracteres."),
        rangelength: jQuery.validator.format("Por favor, forneça um valor entre {0} e {1} caracteres de comprimento."),
        range: jQuery.validator.format("Por favor, forneça um valor entre {0} e {1}."),
        max: jQuery.validator.format("Por favor, forneça um valor menor ou igual a {0}."),
        min: jQuery.validator.format("Por favor, forneça um valor maior ou igual a {0}.")
    });
}



$(document).ready(function() {

    CarregarClientes();
    CarregarTiposServicos();
    CarregarBairros();
    CarregarCidades();
    CarregarEstados();
    CarregarFornecedores();
    DataTablesConfig();

    jQuery.validator.addMethod("greaterThan",
        function (value, element, params) {

            if (!/Invalid|NaN/.test(new Date(value))) {
                return new Date(value) > new Date($(params).val());
            }

            return isNaN(value) && isNaN($(params).val())
                || (Number(value) > Number($(params).val()));
        }, 'O Periodo De deve ser Maior que o Periodo Até');


    //$("#idPeriodoAte").rules('add', { greaterThan: "#idPeriodoDe" });


    $("#formServicoPrestadoRelatorio").validate({
        rules: {
            idPeriodoAte: { greaterThan: "#idPeriodoDe" }
        }
    });

    jQuery.extend(jQuery.validator.messages,
        {
            required: "Campo obrigatório!",
            remote: "Por favor, corrija este campo.",
            email: "Por favor, forneça um endereço eletrônico válido.",
            url: "Por favor, forneça uma URL válida.",
            date: "Por favor, forneça uma data válida.",
            dateISO: "Por favor, forneça uma data válida (ISO).",
            number: "Por favor, forneça um número válido.",
            digits: "Por favor, forneça somente dígitos.",
            creditcard: "Por favor, forneça um cartão de crédito válido.",
            equalTo: "Por favor, forneça o mesmo valor novamente.",
            accept: "Por favor, forneça um valor com uma extensão válida.",
            maxlength: jQuery.validator.format("Por favor, forneça não mais que {0} caracteres."),
            minlength: jQuery.validator.format("Por favor, forneça ao menos {0} caracteres."),
            rangelength: jQuery.validator.format(
                "Por favor, forneça um valor entre {0} e {1} caracteres de comprimento."),
            range: jQuery.validator.format("Por favor, forneça um valor entre {0} e {1}."),
            max: jQuery.validator.format("Por favor, forneça um valor menor ou igual a {0}."),
            min: jQuery.validator.format("Por favor, forneça um valor maior ou igual a {0}.")
        });

    $(".glyphicon-trash").click(function() {
        var id = ($(this).attr("id"));
        var row = ($(this).parent().parent());

        $.ajax({
            url: "ServicoPrestado/RemoverServicoPrestado",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ id: id }),
            success: function(data) {
                if (data) {
                    row.fadeOut("slow",
                        function() {
                            table.dataTable().fnDeleteRow(row, null, true);
                        });

                }
            },
            complete: function(data) { toastr.success("Registro removido com sucesso!") }
        });
    });

});


$("#btnPesquisar").click(function() {

    if ($("#formServicoPrestadoRelatorio").valid()) {

        var filtroRelatorioViewModel = {};
        filtroRelatorioViewModel.PeriodoDe = $("#idPeriodoDe").val();
        filtroRelatorioViewModel.PeriodoAte = $("#idPeriodoAte").val();
        filtroRelatorioViewModel.ValorMinimo = parseFloat($("#idValorMinimo").val());
        filtroRelatorioViewModel.ValorMaximo = parseFloat($("#idValorMaximo").val());
        filtroRelatorioViewModel.ClienteId = parseInt($("#ddlClientes").val());
        filtroRelatorioViewModel.Bairro = $("#ddlBairro").val();
        filtroRelatorioViewModel.Cidade = $("#ddlCidade").val();
        filtroRelatorioViewModel.Estado = $("#ddlEstado").val();
        filtroRelatorioViewModel.FornecedorId = parseInt($("#ddlFornecedores").val());
        filtroRelatorioViewModel.TipoServicoId = parseInt($("#ddlTiposServicos").val());

        $.ajax({
            type: "POST",
            url: "RelatorioServicoPrestado/Pesquisar",
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            data: JSON.stringify(filtroRelatorioViewModel),
            success: function(response) {

                $("#DivTabela").html(response);
                DataTablesConfig();

            }
        });
    }

});


function CarregarClientes() {
    $.ajax({
        type: "GET",
        url: "ServicoPrestado/ListarClientes",
        dataType: "json",
        success: function(data) {
            for (var i = 0; i < data.length; i++) {
                $("#ddlClientes").append("<option value=" + data[i].Id + ">" + data[i].Nome + "</option > ");
            }
        }
    });
}


function CarregarFornecedores() {
    $.ajax({
        type: "GET",
        url: "ServicoPrestado/ListarFornecedores",
        dataType: "json",
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#ddlFornecedores").append("<option value=" + data[i].Id + ">" + data[i].Nome + "</option > ");
            }
        }, complete: function (data) {
            // Handle the complete event
        }
    });
}

function CarregarBairros() {
    $.ajax({
        type: "GET",
        url: "RelatorioServicoPrestado/ListarBairros",
        dataType: "json",
        success: function(data) {
            for (var i = 0; i < data.length; i++) {
                $("#ddlBairro").append("<option value=" + data[i].Bairro + ">" + data[i].Bairro + "</option > ");
            }
        }
    });
}

function CarregarCidades() {
    $.ajax({
        type: "GET",
        url: "RelatorioServicoPrestado/ListarCidades",
        dataType: "json",
        success: function(data) {
            for (var i = 0; i < data.length; i++) {
                $("#ddlCidade").append("<option value=" + data[i].Cidade + ">" + data[i].Cidade + "</option > ");
            }
        }
    });
}

function CarregarEstados() {
    $.ajax({
        type: "GET",
        url: "RelatorioServicoPrestado/ListarEstados",
        dataType: "json",
        success: function(data) {
            for (var i = 0; i < data.length; i++) {
                $("#ddlEstado").append("<option value=" + data[i].Estado + ">" + data[i].Estado + "</option > ");
            }
        }
    });
}


function CarregarTiposServicos() {
    $.ajax({
        type: "GET",
        url: "ServicoPrestado/ListarTiposServicos",
        dataType: "json",
        success: function(data) {
            for (var i = 0; i < data.length; i++) {
                $("#ddlTiposServicos").append("<option value=" + data[i].Id + ">" + data[i].Tipo + "</option > ");
            }
        }
    });
}