function f_throwTextSearcher(dropDown) {
    var valueSearcher = f_getHdnValue(dropDown);

    if ((dropDown.selectedIndex != valueSearcher.value) && (dropDown.selectedIndex > -1)) {
        valueSearcher.value = dropDown.selectedIndex;

        if (f_getHdnPostBack(dropDown)) {
            var btnSearcher = f_getFirerButton(dropDown);
            btnSearcher.click(btnSearcher);
        }
    }
}

function f_getFirerButton(attachedDropDown) {
    //var idBtnFirer = attachedDropDown.id.substr(0, attachedDropDown.id.length - 'ddlEntidadNegocio'.length) + 'btnEntidadNegocio';
    //return document.getElementById(idBtnFirer);
    return f_getObjetosCombo(attachedDropDown, 'btnEntidadNegocio');
    //return btnFirer;
}

function f_getHdnPostBack(attachedDropDown) {
    //var idHdnPostBack = attachedDropDown.id.substr(0, attachedDropDown.id.length - 'ddlEntidadNegocio'.length) + 'hdnDropDownPostBack';
    //var hdnPostBack = document.getElementById(idHdnPostBack);
    var hdnPostBack = f_getObjetosCombo(attachedDropDown, 'hdnDropDownPostBack');

    if (hdnPostBack != null)
        return hdnPostBack.value == '1' ? true : false;
    else
        return false;
}

function f_getHdnValue(attachedDropDown) {
    return f_getObjetosCombo(attachedDropDown, 'hdnDropDownValue');
    //var idHdnValue = attachedDropDown.id.substr(0, attachedDropDown.id.length - 'ddlEntidadNegocio'.length) + ;
    //return document.getElementById(idHdnValue);
    //return hdnValue;
}

function f_getObjetosCombo(attachedDropDown, nombreElem) {
    var idObjetoCombo = attachedDropDown.id.substr(0, attachedDropDown.id.length - 'ddlEntidadNegocio'.length) + nombreElem;
    return document.getElementById(idObjetoCombo);
}

function navegador() {
    var nav = 0;

    if (navigator.userAgent.toUpperCase().indexOf('FIREFOX') > -1) {
        nav = 1;
    }
    else if (navigator.userAgent.toUpperCase().indexOf('MSIE') > -1) {
        nav = 2;
    }
    else if (navigator.userAgent.toUpperCase().indexOf('CHROME') > -1) {
        nav = 3;
    }
    else if (navigator.userAgent.toUpperCase().indexOf('SAFARI') > -1) {
        nav = 4;
    }
    else if (navigator.userAgent.toUpperCase().indexOf('TRIDENT') > -1) {
        nav = 2;
    }

    return nav;
}
