
//Codigo para mensaje de envio de correo en seccion contactanos
function alerta() {
    //let nombre = document.getElementById("nombre").value;
    //let email = document.getElementById('email').value;
    //let mensaje = document.getElementById('mensaje').value;

    //if (nombre !== "" && email !== "" && mensaje !== "") {
    //    alert("Mensaje enviado");
    //}
    //else {
    //    console.log("error");
    //}

    const btn = document.getElementById('btnEnviarForm');

    document.getElementById('formulario-para-contacto')
        .addEventListener('submit', function (event) {
            event.preventDefault();

            btn.value = 'Enviando...';

            const serviceID = 'default_service';
            const templateID = 'template_zbw7dxj';

            emailjs.sendForm(serviceID, templateID, this)
                .then(() => {
                    btn.value = 'Enviar Mensaje';
                    Swal.fire({
                        title: "¡Mensaje enviado!",
                        icon: "success"
                    });
                }, (err) => {
                    btn.value = 'Enviar Mensaje';
                    alert(JSON.stringify(err));
                });
        });

}
 //Enviar email de contacto


//Alertas personalizadas
