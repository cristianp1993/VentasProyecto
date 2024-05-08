const loginButton = document.getElementById('login-button');
const usernameInput = document.getElementById('username');
const passwordInput = document.getElementById('password');
const togglePasswordVisibility = document.getElementById('togglePasswordVisibility');

usernameInput.addEventListener('change', function () {
    const usernameValue = usernameInput.value;
    const regexCorreo = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (regexCorreo.test(usernameValue)) {
        usernameInput.style.borderColor = 'green';
        usernameInput.style.borderWidth = '3px';

    } else {
        usernameInput.style.borderColor = 'red';
        usernameInput.style.borderWidth = '3px';
        console.log("El correo no es correcto")
    }
});

togglePasswordVisibility.addEventListener('click', function () {
    if (passwordInput.type === "password") {
        passwordInput.type = "text";
        togglePasswordVisibility.textContent = "visibility";
    } else {
        passwordInput.type = "password";
        togglePasswordVisibility.textContent = "visibility_off";
    }
});

loginButton.addEventListener('click', function (event) {
    event.preventDefault(); 

    const usernameValue = usernameInput.value.trim(); 
    const passwordValue = passwordInput.value.trim();

    if (usernameValue === '' || passwordValue === '') {
        usernameInput.style.borderColor = 'red';
        usernameInput.style.borderWidth = '3px';
        passwordInput.style.borderColor = 'red';
        passwordInput.style.borderWidth = '3px';
    } else {
        ValidateLogin();
    }
});


function ValidateLogin() {
    // Realizar la solicitud Fetch

    const data = {
        user: usernameInput.value,
        password: passwordInput.value
    };

    fetch('/Login/ValidateUser', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
    .then(response => response.text())
    .then(data => {
            
        if (data == "True") {
            window.location.href = '/Home/Index'; 
        } else {
            alert('Usuario o contraseña incorrectos.');
        }
    })
    .catch(error => {
        console.error('Error:', error);
        alert('Hubo un error al intentar iniciar sesión.');
    });
}


