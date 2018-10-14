var table = $("#tbServicoPrestado").dataTable({
    "columnDefs": [
        {
            "targets": 6,
            "orderable": false,
            "className": "center"
        }
    ],
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
    }
});

$(document).ready(function() {
    $("#camadaefeitos").hide();

    $("#ocultar").click(function(event) {
        event.preventDefault();
        $("#camadaefeitos").hide("slow");
    });

    $("#mostrar").click(function(event) {
        event.preventDefault();
        $("#camadaefeitos").show("slow");
    });

    CarregarFornecedores();
    CarregarClientes();
    CarregarTiposServicos();

    $('#formServicoPrestado').validate({
        rules: {

            idDataAtendimento: {
                required: true,
                date: true
            },
            idValor: {
                required: true,
                number: true
            },
            IdDescricao: {
                required: true
            }
        }
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



});

function Remover(row, id) {

    var idRemover = parseInt(id);
    var linha = row.closest('tr');

    $.ajax({
        url: "ServicoPrestado/RemoverServicoPrestado",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ id: idRemover }),
        success: function (data) {
            if (data) {

                        table.dataTable().fnDeleteRow(linha, null, true);
                    
            }
        },
        complete: function (data) { toastr.success('Registro removido com sucesso!') }
    });
}

$("#btnSalvar").click(function () {

    if ($("#formServicoPrestado").valid()) {

        var servicoPrestadoViewModel = {
         DescricaoServico: $("#IdDescricao").val(),
         DataAtendimento: $("#idDataAtendimento").val(),
         ValorServico: parseFloat($("#idValor").val()),
         ClienteId: parseInt($("#ddlClientes").val()),
         FornecedorId: parseInt($("#ddlFornecedores").val()),
         TipoServicoId: parseInt($("#ddlTiposServicos").val())
        };

        $.ajax({
            type: "POST",
            url: "ServicoPrestado/InserirServicoPrestado",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(servicoPrestadoViewModel),
            success: function (response) {

                    table.dataTable().fnAddData([
                        response.DataAtendimentoddMMyyyy,
                        response.DescricaoServico,
                        $("#ddlTiposServicos option:selected").text(),
                        $("#ddlClientes option:selected").text(),
                        $("#ddlFornecedores option:selected").text(),
                        (parseFloat($("#idValor").val()).toFixed(2).replace('.', ',')),
                        '<td style="text-align: center"><span class="glyphicon glyphicon-trash" style="cursor: pointer;text-align: center;" onclick="Remover(this,' + response.Id +')" id="' + response.Id+'"></span></td>']);
            },
            complete: function (response) { toastr.success('Registro salvo com sucesso!') }
        });
    };
   
});


function CarregarFornecedores() {
    $.ajax({
        type: "GET",
        url: "ServicoPrestado/ListarFornecedores",
        dataType: "json",
        success: function(data) {
            for (var i = 0; i < data.length; i++) {
                $("#ddlFornecedores").append("<option value=" + data[i].Id + ">" + data[i].Nome + "</option > ");
            }
        }, complete: function (data) {
            // Handle the complete event
        }
    });
}

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
