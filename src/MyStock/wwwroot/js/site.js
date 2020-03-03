$('#img').change(function() {
    $('#imgName').text(this.files[0].name);
    var reader = new FileReader();
    reader.onload = function (e) {
        $('#imgSrc').attr('src', e.target.result);
    }
    reader.readAsDataURL(this.files[0]);
})

/*TOOLTIPS: */
$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})

/* SHOW IMAGES: */
const showImgs = () => {
    var img = document.getElementById('show-img')
    if (img) {
        img.hidden = !img.hidden
    }
}

/* SHOW MODAL: */
function AjaxModal() {
	$(document).ready(function () {
		$(function () {
			$.ajaxSetup({ cache: false });

			$("a[data-modal]").on("click",
				function (e) {
					$('#myModalContent').load(this.href,
						function () {
							$('#myModal').modal({
									keyboard: true
								},
								'show');
							bindForm(this);
						});
					return false;
				});
		});

		function bindForm(dialog) {
			$('form', dialog).submit(function () {
				$.ajax({
					url: this.action,
					type: this.method,
                    data: $(this).serialize(),
					success: function (result) {
						if (result.success) {
                            $('#myModal').modal('hide');
							$('#AddressTarget').load(result.url); // Carrega o resultado HTML para a div demarcada
						} else {
							$('#myModalContent').html(result);
							bindForm(dialog);
						}
					}
				});
				return false;
			});
		}
	});
}

/* CONSULTA CEP */
function BuscaCep() {
    $(document).ready(function () {

        function limpa_formulário_cep() {
            // Limpa valores do formulário de cep.
            $("#Address_Street").val("");
            $("#Address_District").val("");
            $("#Address_City").val("");
            $("#Address_State").val("");
        }

        //Quando o campo cep perde o foco.
        $("#Address_Cep").blur(function () {

            //Nova variável "cep" somente com dígitos.
            var cep = $(this).val().replace(/\D/g, '');

            //Verifica se campo cep possui valor informado.
            if (cep != "") {

                //Expressão regular para validar o CEP.
                var validacep = /^[0-9]{8}$/;

                //Valida o formato do CEP.
                if (validacep.test(cep)) {

                    //Preenche os campos com "..." enquanto consulta webservice.
                    $("#Address_Street").val("...");
                    $("#Address_District").val("...");
                    $("#Address_City").val("...");
                    $("#Address_State").val("...");

                    //Consulta o webservice viacep.com.br/
                    $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?",
                        function (dados) {

                            if (!("erro" in dados)) {
                                //Atualiza os campos com os valores da consulta.
                                $("#Address_Street").val(dados.logradouro);
                                $("#Address_District").val(dados.bairro);
                                $("#Address_City").val(dados.localidade);
                                $("#Address_State").val(dados.uf);
                            } //end if.
                            else {
                                //CEP pesquisado não foi encontrado.
                                limpa_formulário_cep();
                                alert("CEP não encontrado.");
                            }
                        });
                } //end if.
                else {
                    //cep é inválido.
                    limpa_formulário_cep();
                    alert("Formato de CEP inválido.");
                }
            } //end if.
            else {
                //cep sem valor, limpa formulário.
                limpa_formulário_cep();
            }
        });
    });
}

$(document).ready(function () {
    $("#msg_box").fadeOut(2500);
});