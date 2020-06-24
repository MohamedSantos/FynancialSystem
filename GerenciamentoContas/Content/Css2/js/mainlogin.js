/* Animação de elementos */
AOS.init({
    duration: 800,
    easing: 'slide',
    once: false
  });
  /* fim animação */

/*********Tela Login***********/

const inputs = document.querySelectorAll(".input");

function addcl(){
	let parent = this.parentNode.parentNode;
	parent.classList.add("focus");
}

function remcl(){
	let parent = this.parentNode.parentNode;
	if(this.value == ""){
		parent.classList.remove("focus");
	}
}


inputs.forEach(input => {
	input.addEventListener("focus", addcl);
	input.addEventListener("blur", remcl);
});



/* Inicio validação form */

const form = document.getElementById('form');
const username = document.getElementById('username');
const password = document.getElementById('password');


form.addEventListener('submit', e => {
	e.preventDefault();
	
	checkInputs();
});

function checkInputs() {
	// trim to remove the whitespaces
	const usernameValue = username.value.trim();
	const passwordValue = password.value.trim();

	if(usernameValue === '') {
		setErrorFor(username, 'Insira um nome de usúario.');
	} else {
		setSuccessFor(username);
	}
	
	if(passwordValue === '') {
		setErrorFor(password, 'Insira sua senha de usúario.');
	} else {
		setSuccessFor(password);
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
