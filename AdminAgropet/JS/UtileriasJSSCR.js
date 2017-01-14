function Cambiar_LabelEstado(checkbox) {
    checkbox.nextSibling.innerHTML = checkbox.checked ? '[Activo]' : '[Inactivo]';
}

function Cambiar_LabelEstadoPersonal(checkbox2) {
    checkbox2.nextSibling.innerHTML = checkbox2.checked ? '[La información será enviada a revisión]' : '[Si cambios de puesto.]';
} 

function solonumeros(e, field) {
    key = e.keyCode ? e.keyCode : e.which
    // backspace
    if (key == 8) return true
    // 0-9
    if (key > 47 && key < 58) {
        if (field.value == "") return true
        regexp = /.[0-9]{2}$/
        return !(regexp.test(field.value))
    }
    // .
    if (key == 46) {
        if (field.value == "") return true
        regexp = /^[0-9]+$/
        return regexp.test(field.value)
    }
    // other key
    return false

}

//Funcion que determina si al menos un item de un grid en un div(panel)  y confirma con el 
//usuario si se hace la operacion especificada, 
function checkItemsSeleccionados(idDiv) {

    var HaySeleccionados;
    var ValorARegresar;

    HaySeleccionados = false;
    var elementos = document.getElementById(idDiv).getElementsByTagName('*');
    for (i = 0; i < elementos.length; i++) {
        if (elementos[i].type == "checkbox" && elementos[i].checked)
            HaySeleccionados = true;
    }

    if (HaySeleccionados) {
        ValorARegresar = confirm('¿ Desea Activar / Des-activar los elementos seleccionados ?');
    }
    else {
        alert('No hay ningún elemento seleccionado, no se puede realizar la operación solicitada.');
        ValorARegresar = false;
    }

    return ValorARegresar;
}

//Funcion que cambia la tecla presionada Enter por Tab.
function ModifyEnterKeyPressAsTab() {
    if (window.event && window.event.keyCode == 13) {
        window.event.keyCode = 9;
    }
}

//Funcion para validar 
function Verificacion(Objeto, Tipo) {
    var LongitudValor = Objeto.value.length + 1
    var SubCadena = String.fromCharCode(window.event.keyCode).toUpperCase();
    // PARA ACENTOS
    if (window.event.keyCode == 180 ||
                  window.event.keyCode == 225 ||
                  window.event.keyCode == 233 ||
                  window.event.keyCode == 237 ||
                  window.event.keyCode == 243 ||
                  window.event.keyCode == 250 ||
                  window.event.keyCode == 193 ||
                  window.event.keyCode == 201 ||
                  window.event.keyCode == 205 ||
                  window.event.keyCode == 211 ||
                  window.event.keyCode == 218 ||
                  window.event.keyCode == 13) {
        window.event.keyCode = String.fromCharCode(window.event.keyCode).charCodeAt(0);
        return;
    }

    var Cadena = ""
    switch (Tipo) {
        case 1:  //Letras
            var cadStr = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ '
            break;
        case 2: //Números
            if (window.event.keyCode == 241) { window.event.keyCode = 0; return; }
            var cadStr = "0123456789"
            break;
        case 3:  //Letras y Números
            var cadStr = '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ '
            break;
        case 4:  //Decimales RCR
            if (window.event.keyCode == 241) { window.event.keyCode = 0; return; }
            var cadStr = '0123456789.'
            break;
        case 5:  //Hora
            if (window.event.keyCode == 241) { window.event.keyCode = 0; return; }
            var cadStr = '0123456789:'
            break;
        case 6:  //CURP y RFC
            var cadStr = '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ'
            break;
        case 7:  //Especial
            var cadStr = '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ- '
            break;
        case 8:  //Telefono
            var cadStr = '()0123456789-'
            break;
        case 9:  //Ffecha
            if (window.event.keyCode == 241) { window.event.keyCode = 0; return; }
            var cadStr = '0123456789/'
            break;
        case 10:  //DOMICILIO
            var cadStr = '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ#-. '
            break;
        case 11:  //NÚMEROS SIN CEROS
            if (window.event.keyCode == 241) { window.event.keyCode = 0; return; }
            var cadStr = '123456789'
            break;
        case 12: //CFG. 22-MAY-03 SOLO NÚMEROS O PALABRA 'ADMON' SOLO PARA AUTENTIFICACIÓN DE USUARIO
            if (window.event.keyCode == 241) { window.event.keyCode = 0; return; }
            var cadStr = '0123456789ADMON'
            break;
        case 13:  //OBSERVACIONES
            var cadStr = '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ#.;-:,()/ "$[]'
            break;
        case 14:  //Decimales RCR
            if (window.event.keyCode == 241) { window.event.keyCode = 0; return; }
            var cadStr = '0123456789.-'
            break;
        case 18:  //Dictamen
            var cadStr = '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-/()'
            break;
        case 19:  //SOLO LECTURA
            var cadStr = ''
            break;
        case 20:  //CARACTERES PARA NOMBRES DE ARCHIVOS
            var cadStr = '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ#-_.'
            break;
    }
    if (LongitudValor > 0) {
        for (i = 1; i <= cadStr.length; i++) {
            if ((cadStr.substring(i, i - 1) == SubCadena) || (window.event.keyCode == 209) || (window.event.keyCode == 241)) {
                Cadena = cadStr.substring(i, i - 1);
            }
        }
        if (Cadena.length == 0) {
            window.event.keyCode = 0
        }
        else {
            window.event.keyCode = String.fromCharCode(window.event.keyCode).toUpperCase().charCodeAt(0);
        }
        return;
    }
}


