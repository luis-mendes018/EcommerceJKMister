var nextButton = document.querySelector(".carousel-control-next");
var prevButton = document.querySelector(".carousel-control-prev");

nextButton.addEventListener("click", function () {
    $('#carouselStore').carousel('next');
});

prevButton.addEventListener("click", function () {
    $('#carouselStore').carousel('prev');
});



// consulta cep
function limpa_formulário_cep() {
    //Limpa valores do formulário de cep.
    document.getElementById('Endereco1').value = ("");
    document.getElementById('bairro').value = ("");
    document.getElementById('cidade').value = ("");
    document.getElementById('Estado').value = ("");
}

function meu_callback(conteudo) {
    if (!("erro" in conteudo)) {
        //Atualiza os campos com os valores.
        document.getElementById('Endereco1').value = (conteudo.logradouro);
        document.getElementById('bairro').value = (conteudo.bairro);
        document.getElementById('cidade').value = (conteudo.localidade);
        document.getElementById('Estado').value = (conteudo.uf);

    } //end if.
    else {
        //CEP não Encontrado.
        limpa_formulário_cep();
        alert("CEP não encontrado.");
    }
}

function pesquisacep(valor) {

    //Nova variável "cep" somente com dígitos.
    var cep = valor.replace(/\D/g, '');

    //Verifica se campo cep possui valor informado.
    if (cep != "") {

        //Expressão regular para validar o CEP.
        var validacep = /^[0-9]{8}$/;

        //Valida o formato do CEP.
        if (validacep.test(cep)) {

            //Preenche os campos com "..." enquanto consulta webservice.
            document.getElementById('Endereco1').value = "...";
            document.getElementById('bairro').value = "...";
            document.getElementById('cidade').value = "...";
            document.getElementById('Estado').value = "...";
            //document.getElementById('ibge').value = "...";

            //Cria um elemento javascript.
            var script = document.createElement('script');

            //Sincroniza com o callback.
            script.src = 'https://viacep.com.br/ws/' + cep + '/json/?callback=meu_callback';

            //Insere script no documento e carrega o conteúdo.
            document.body.appendChild(script);

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
};


//Mostrar senha

function MostrarSenha() {
    var passwordInput = document.getElementById("Password");
    var currentType = passwordInput.getAttribute("type");

    if (currentType == "password") {
        passwordInput.setAttribute("type", "text");
    } else {
        passwordInput.setAttribute("type", "password");
    }
}


//Mostrar senha de confirmação

function MostrarSenhaConfirm() {
    var passwordInput = document.getElementById("PasswordConfirm");
    var currentType = passwordInput.getAttribute("type");

    if (currentType == "password") {
        passwordInput.setAttribute("type", "text");
    } else {
        passwordInput.setAttribute("type", "password");
    }
}



//Para alteração de senhas

function MostrarSenhaAtual() {
    var passwordInput = document.getElementById("PasswordNow");
    var currentType = passwordInput.getAttribute("type");

    if (currentType == "password") {
        passwordInput.setAttribute("type", "text");
    } else {
        passwordInput.setAttribute("type", "password");
    }
}


function MostrarNovaSenha() {
    var passwordInput = document.getElementById("PasswordNew");
    var currentType = passwordInput.getAttribute("type");

    if (currentType == "password") {
        passwordInput.setAttribute("type", "text");
    } else {
        passwordInput.setAttribute("type", "password");
    }
}

function MostrarNovaSenhaConfirm() {
    var passwordInput = document.getElementById("PasswordNewConfirm");
    var currentType = passwordInput.getAttribute("type");

    if (currentType == "password") {
        passwordInput.setAttribute("type", "text");
    } else {
        passwordInput.setAttribute("type", "password");
    }
}

//Área de administração

function MostrarSenhaLoginAdm() {
    var passwordInput = document.getElementById("PasswordAdm");
    var currentType = passwordInput.getAttribute("type");

    if (currentType == "password") {
        passwordInput.setAttribute("type", "text");
    } else {
        passwordInput.setAttribute("type", "password");
    }
}

function AdminMostrarSenhaAtual() {
    var passwordInput = document.getElementById("AdminPasswordNow");
    var currentType = passwordInput.getAttribute("type");

    if (currentType == "password") {
        passwordInput.setAttribute("type", "text");
    } else {
        passwordInput.setAttribute("type", "password");
    }
}

function AdminMostrarNovaSenha() {
    var passwordInput = document.getElementById("AdminPasswordNew");
    var currentType = passwordInput.getAttribute("type");

    if (currentType == "password") {
        passwordInput.setAttribute("type", "text");
    } else {
        passwordInput.setAttribute("type", "password");
    }
}

function AdminMostrarNovaSenhaConfirm() {
    var passwordInput = document.getElementById("AdminPasswordNewConfirm");
    var currentType = passwordInput.getAttribute("type");

    if (currentType == "password") {
        passwordInput.setAttribute("type", "text");
    } else {
        passwordInput.setAttribute("type", "password");
    }
}

function MostrarSenhaCreate() {
    var passwordInput = document.getElementById("Senha");
    var currentType = passwordInput.getAttribute("type");

    if (currentType == "password") {
        passwordInput.setAttribute("type", "text");
    } else {
        passwordInput.setAttribute("type", "password");
    }
}

function MostrarSenhaConfirmCreate() {
    var passwordInput = document.getElementById("ConfirmacaoSenha");
    var currentType = passwordInput.getAttribute("type");

    if (currentType == "password") {
        passwordInput.setAttribute("type", "text");
    } else {
        passwordInput.setAttribute("type", "password");
    }
}


//Copiar caminho da imagem. Admnistração
function copyToClipboard(text) {
    var input = document.createElement('input');
    input.setAttribute('value', text);
    document.body.appendChild(input);
    input.select();
    document.execCommand('copy');
    document.body.removeChild(input);
    alert('Caminho da imagem copiado para a área de transferência.');
}

