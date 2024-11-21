function soloLetras(event, lblText) {
    var regex = /^[a-zA-Z\sáéíóúÁÉÍÓÚñÑ]*$/;
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        $('#' + lblText).text("Solo se aceptan letras");
        $('#' + lblText).css({ "color": "red" });
        return false;
    } else {
        $('#' + lblText).text("");
    }
}

function userName(event, lblText) {
    var regex = /^[a-zA-Z]*$/;
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        $('#' + lblText).text("Solo se aceptan letras y sin espacios");
        $('#' + lblText).css({ "color": "red" });
        return false;
    } else {
        $('#' + lblText).text("");
    }
}

function validarCorreo(event, lblText) {
    var email = event.target.value;
    var caract = new RegExp(/^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/);

    if (caract.test(email) == false) {
        $('#' + lblText).text("Correo Erroneo");
        $('#' + lblText).css({ "color": "red" });
        return false;
    } else {
        $('#' + lblText).text("");
    }
}

function validarNumeros(event, lblText) {
    var numero = String.fromCharCode(event.which);
    var caract = new RegExp(/^[0-9]+$/);

    if (caract.test(numero) == false) {
        $('#' + lblText).text("Introduce solo números.");
        $('#' + lblText).css({ "color": "red" });
        event.preventDefault();
        return false;
    } else {
        $('#' + lblText).text("");
        return true;
    }
}

function validarCURP(event, lblText) {
    var curp = event.target.value;
    var caract = /^[A-Z]{4}[0-9]{6}[H,M][A-Z]{5}[A-Z0-9]{2}$/;

    if (caract.test(curp) == false) {
        $('#' + lblText).text("CURP no válida. Por favor, verifique su CURP.");
        $('#' + lblText).css({ "color": "red" });
        return false;
    } else {
        $('#' + lblText).text("");
    }
}

$("#password1, #password2").on("blur", function () {
    var password1 = $("#password1").val();
    var password2 = $("#password2").val();

    if (password1 === password2) {
        $("#lblErrorPassword").text("");
        $("#password1, #password2").removeClass("border-danger").toggleClass("border-success");

    } else {
        $("#password1, #password2").removeClass("border-success").toggleClass("border-danger");

        $("#lblErrorPassword").text("Las contraseñas no coinciden.");
        $("#lblErrorPassword").css({ "color": "red" });
    }
});



