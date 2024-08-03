

//Codigo para mensaje de envio de correo en seccion contactanos
function alerta() {
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
                    ResetInput('formulario-para-contacto');
                }, (err) => {
                    btn.value = 'Enviar Mensaje';
                    alert(JSON.stringify(err));
                });
        });

}
function ResetInput(form) {
    document.getElementById(form).reset();
}
//Alertas personalizadas eliminar
function DeleteRegister(id, route) {

    Swal.fire({
        title: "¿Eliminar registro?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, borrar"
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire({
                title: "¡Registro eliminado!",
                icon: "success",
                timer: 40000
            });
            window.location = route + id;
        }
    });
}

//Recuperar Password
function PasswordMail() {
    //const btn = document.getElementById('enviar');

    document.getElementById('form-login')
        .addEventListener('submit', function (event) {
            event.preventDefault();

            //btn.value = 'Sending...';

            const serviceID = 'default_service';
            const templateID = 'template_ievtj1e';

            emailjs.sendForm(serviceID, templateID, this)
                .then(() => {
                    //btn.value = 'Send Email';
                    Swal.fire({
                        title: "¡Mail enviado!",
                        text: "Revise su bandeja de entrada..",
                        icon: "success"
                    });
                }, (err) => {
                    //btn.value = 'Send Email';
                    alert(JSON.stringify(err));
                });
        });
    
}

//Alertas personalizadas confirmar
function enviarFormulario(e, form) {
    e.preventDefault();

    Swal.fire({
        title: "¡Éxito!",
        text: "Registro cargado correctamente!",
        icon: "success",
        showCancelButton: false,
        confirmButtonText: "Aceptar"
    }).then((result) => {
        if (result.isConfirmed) {
            const formulario = document.getElementById(form);
            formulario.submit();
        }
    })    
}
//Alertas personalizadas editar
function editarFormulario(e, form) {
    e.preventDefault();

    Swal.fire({
        title: "Advertencia",
        text: "¿Modificar registro?",
        icon: "question",
        showCancelButton: true,
        confirmButtonText: "Confirmar"
    }).then((result) => {
        if (result.isConfirmed) {
            const formulario = document.getElementById(form);
            formulario.submit();
        }
    })
}

/*"return confirm('¿Está seguro que desea eliminar este servicio?');"*/