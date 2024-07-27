
//Codigo para mensaje de envio de correo en seccion contactanos
function alerta() {
    let nombre = document.getElementById("nombre").value;
    let email = document.getElementById('email').value;
    let mensaje = document.getElementById('mensaje').value;

    if (nombre !== "" && email !== "" && mensaje !== "") {
        alert("Mensaje enviado");
    }
    else {
        console.log("error");
    }

    console.log(nombre);
    console.log(email);
    console.log(mensaje);

    //let nombre = "nuevo nombre";
    //console.log("hola");
    //alert(nombre);


}