function ValidNum(e) {
    var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
    return ((tecla > 47 && tecla < 58));
}     

function mostrar_men(msg) {
    $('#div_Mensaje').html(msg);
    $('#div_Mensaje').showTopbarMessage({ background: "#580000", close: "2000", foreColor: "#FFFFFF", fontSize: "9pt" });
}
function requerido(elemento) {
    var longitud = $(elemento).val().trim().length;
    var v = $(elemento).val().trim();
    var bandera = (longitud > 0 && v != "0") ? true : false;
    var mensaje = "El campo " + $(elemento).attr("valida").split(",")[4] + " es requerido."; ;
    if (!bandera) {
        mostrar_men(mensaje)
        elemento.focus();
    }
    return bandera;
}
function numerico(elemento) {
    var v = $(elemento).val();
    var numeros = "0123456789";
    var mensaje = "El campo " + $(elemento).attr("valida").split(",")[4] + " solo puede contener numeros";
    for (i = 0; i < v.length; i++) {
        if (numeros.indexOf(v.charAt(i), 0) < 0) {
            mostrar_men(mensaje)
            elemento.focus();
            return false;
        }
    }
    return true;
}
function string(elemento) {
    var v = $(elemento).val();
    var letras = "abcdefghyjklmnñopqrstuvwxyz";
    var mensaje = "El campo " + $(elemento).attr("valida").split(",")[4] + " solo puede contener letras";
    v = v.toLowerCase();
    for (i = 0; i < v.length; i++) {
        if (letras.indexOf(v.charAt(i), 0) < 0) {
            mostrar_men(mensaje)
            elemento.focus();
            return false;
        }
    }
    return true;
}
function numdec(elemento) {
    var v = $(elemento).val();
    var mensaje = "El campo " + $(elemento).attr("valida").split(",")[4] + " debe contener un valor decimal";
    if (!/^([0-9])*[.]?[0-9]*$/.test(v)) {
        mostrar_men(mensaje)
        elemento.focus();
        return false;
    } else {
        return true;
    }
}
function tipo(elemento) {
    var bandera;
    var x = $(elemento).attr("valida").split(",")[1];
    switch (x) {
        case "num":
            bandera = numerico(elemento);
            break;
        case "decimal":
            bandera = numdec(elemento);
            break;
        case "text":
            bandera = string(elemento);
            break;
    }
    return bandera;
}
function longitud(elemento) {
    var min = parseInt($(elemento).attr("valida").split(",")[2]);
    var max = parseInt($(elemento).attr("valida").split(",")[3]);
    var bandera;
    var mensaje = "El campo " + $(elemento).attr("valida").split(",")[4] + " solo puede contener entre " + min + " y " + max + " caracteres.";
    var v = $(elemento).val();
    if (v.length >= min) {
        if (v.length <= max) {
            bandera = true;
        } else {
            mostrar_men(mensaje)
            elemento.focus();
            bandera = false;
        }
    } else {
        mostrar_men(mensaje)
        elemento.focus();
        bandera = false;
    }
    return bandera;
}
function valida() {
    var bandera = true;
    $("[type=text]:input,[type=checkbox]:input,select").each(function() {
        var elemento = this;
        var validacion = $(elemento).attr("valida");
        if (typeof validacion != "undefined") {
            var arrval = validacion.split(",");
            var x1 = (arrval[0] == "1") ? requerido(elemento) : true;
            if (!x1) {
                bandera = false;
                return false;
            }
            var x2 = (arrval[1] == "all") ? true : tipo(elemento);
            if (!x2) {
                bandera = false;
                return false;
            }
            var x3 = longitud(elemento);
            if (!x3) {
                bandera = false;
                return false;
            }
        }
    });
    return bandera;
}

//Funcion que selecciona o deselecciona todos los checkbox dentro de un div (panel)
function checkTodo(idDiv, idCheckBoxBase) {
    chkBase = document.getElementById(idCheckBoxBase);
    var elementos = document.getElementById(idDiv).getElementsByTagName('*');
    for (i = 0; i < elementos.length; i++) {
        if (elementos[i].type == "checkbox" && elementos[i].id != chkBase.id)
            elementos[i].checked = chkBase.checked
    }
}

//Obtiene el elemento select del componente combo según su nombre
function getDropDownUC(vps_NombreDropDown) {
    var vls_NombreDiv = vps_NombreDropDown + '_pnlDropDown';
    var vlo_Div = document.getElementById(vls_NombreDiv);

    if (vlo_Div != null) {
        var selects = vlo_Div.getElementsByTagName('select');

        if (selects.length > 0) {
            return selects[0];
        }
        else {
            return false;
        }
    }
    else
        return false;
}

//Obtiene el elemento select del componente combo según su nombre
function setTitleDropDownUC(vps_NombreDropDown, title) {
    var vlo_Select = getDropDownUC(vps_NombreDropDown);

    if (vlo_Select) {
        vlo_Select.title = title;
    }
}
