/* Sticky nav menu */
window.addEventListener("scroll", function(){
  var header = document.querySelector("header");
  header.classList.toggle('sticky', window.scrollY > 0);
})
/* fim sticky */


/* Fade video scroll */
let video = document.querySelector('video');
window.addEventListener('scroll' , function(){
  let value = 1 + window.scrollY/-550;
  video.style.opacity = value;

})
/* fim Fade video scroll */


/* Animação de elementos */
AOS.init({
  duration: 800,
  easing: 'slide',
  once: false
});
/* fim animação */


/* scroll */
$("a").click(function () {
  console.log(this);
  $('html, body').animate({
      scrollTop: ($(this.hash).offset().top)
  }, 500)
})
/* fim scrool */

/* Mask CPF / Data de nascimento */
$(document).ready(function(){
  $("#cpf").mask("000.000.000-00")
  $("#telefone").mask("(00) 00000-0000")
  $("#data").mask("00/00/0000")
})
/* Fim Mask*/


/* Inicio validação form */

const form = document.getElementById('form');
const nome = document.getElementById('nome');
const cpf = document.getElementById('cpf');
const telefone = document.getElementById('telefone');
const email = document.getElementById('email');
const cep = document.getElementById('cep');
const rua = document.getElementById('rua');
const numero = document.getElementById('numero');
const complemento = document.getElementById('complemento');
const bairro = document.getElementById('bairro');
const cidade = document.getElementById('cidade');
const uf = document.getElementById('uf');
const data = document.getElementById('data');


/**************************************************/


form.addEventListener('submit', e => {
	e.preventDefault();
	
	checkInputs();
});

function checkInputs() {
	// trim to remove the whitespaces
  const nomeValue = nome.value.trim();
  const cpfValue = cpf.value.trim();
  const telefoneValue = telefone.value.trim();
  const emailValue = email.value.trim();
  const cepValue = cep.value.trim();
  const ruaValue = rua.value.trim();
  const numeroValue = numero.value.trim();
  const complementoValue = complemento.value.trim();
  const bairroValue = bairro.value.trim();
  const cidadeValue = cidade.value.trim();
  const ufValue = uf.value.trim();
  const dataValue = data.value.trim();
	
	if(nomeValue === '') {
		setErrorFor(nome, 'Preencha seu nome completo.');
	} else {
		setSuccessFor(nome);
  }
  
  if(cpfValue === '') {
		setErrorFor(cpf, 'Preencha seu CPF.');
	} else {
		setSuccessFor(cpf);
  }

  if(complementoValue === '') {
		setErrorFor(complemento, 'Acione o complemento.');
	} else {
		setSuccessFor(complemento);
  }

  if(numeroValue === '') {
		setErrorFor(numero, 'Preencha o número de sua casa.');
	} else {
		setSuccessFor(numero);
  }

  if(telefoneValue === '') {
		setErrorFor(telefone, 'Adicione seu telefone.');
	} else {
		setSuccessFor(telefone);
  }

  if(cepValue === '') {
		setErrorFor(cep, 'Preencha seu CEP.');
	} else {
		setSuccessFor(cep);
  }

  if(ruaValue === '') {
		setErrorFor(rua, 'Preencha sua Rua.');
	} else {
		setSuccessFor(rua);
  }

  if(cidadeValue === '') {
		setErrorFor(cidade, 'Preencha sua Cidade.');
	} else {
		setSuccessFor(cidade);
  }

  if(ufValue === '') {
		setErrorFor(uf, 'Preencha seu Estado.');
	} else {
		setSuccessFor(uf);
  }

  if(bairroValue === '') {
		setErrorFor(bairro, 'Preencha seu Bairro.');
	} else {
		setSuccessFor(bairro);
  }

  if(dataValue === '') {
		setErrorFor(data, 'Preencha sua data de nascimento.');
	} else {
		setSuccessFor(data);
  }
  
	if(emailValue === '') {
		setErrorFor(email, 'Preencha seu email.');
	} else if (!isEmail(emailValue)) {
		setErrorFor(email, 'Email invalido.');
	} else {
		setSuccessFor(email);
	}
}

function setErrorFor(input, message) {
	const formControl = input.parentElement;
	const small = formControl.querySelector('small');
	formControl.className = 'form-control-1 error';
	small.innerText = message;
}

function setSuccessFor(input) {
	const formControl = input.parentElement;
	formControl.className = 'form-control-1 success';
}
	
function isEmail(email) {
	return /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(email);
}

/***** FIM VALIDAÇÃO *******/
  