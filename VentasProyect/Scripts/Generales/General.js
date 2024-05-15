function CapitalizeText(input) {
    let texto = input.value.trim();
    let palabras = texto.split(' ');
    let textoCapitalizado = "";

    for (let i = 0; i < palabras.length; i++) {
        palabras[i] = palabras[i].charAt(0).toUpperCase() + palabras[i].slice(1).toLowerCase();
        textoCapitalizado += palabras[i] + " ";
    }

    textoCapitalizado = textoCapitalizado.replace(/ +/g, ' ');
    input.value = textoCapitalizado.trim();
}

function UpdateHiddenValue(select) {
    document.getElementById('ciu_id_hidden').value = parseInt(select.value);
}

function ValidateEmpy() {
    if (this.value == "" || this.value == null) {
        Swal.fire({
            title: 'Error!',
            text: 'El campo no puede estar vacio',
            icon: 'error',
            confirmButtonText: 'Cool'
        })
    }
}