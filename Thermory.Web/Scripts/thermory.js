function rejectNonNumericText(e) {
    // Allow: backspace, delete, tab, escape, enter, minus and .
    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 189, 190]) !== -1 ||
        // Allow: Ctrl+A, Command+A
        (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
        // Allow: home, end, left, right, down, up
        (e.keyCode >= 35 && e.keyCode <= 40)) {
        // let it happen, don't do anything
        return;
    }
    // Ensure that it is a number and stop the keypress
    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
        e.preventDefault();
    }
}

function showCategoryForm(prefix) {
    $('#' + prefix + 'CategoryErrorPanel').hide();
    $('.' + prefix + 'CategoryForm').show();
    $('.' + prefix + 'CategoryView').hide();
}

function showCategoryView(prefix) {
    $('#' + prefix + 'CategoryErrorPanel').hide();
    $('.' + prefix + 'CategoryForm').hide();
    $('.' + prefix + 'CategoryView').show();
}

function enableNavTabByCssClasss(cssClass) {
    $('.' + cssClass).removeClass('disabled');
    $('.' + cssClass).find('a').attr('data-toggle', 'tab');
}

function disableNavTabByCssClasss(cssClass) {
    $('.' + cssClass).addClass('disabled');
    $('.' + cssClass).find('a').removeAttr('data-toggle');
}

function initiateViewMode(prefix) {
    $('.' + prefix + 'ErrorPanel').hide();
    enableNavTabByCssClasss(prefix + 'Tab');
    $('.' + prefix + 'View').show();
    $('.' + prefix + 'Form').hide();
}

function initiateEditMode(prefix) {
    disableNavTabByCssClasss(prefix + 'Tab');
    $('.' + prefix + 'View').hide();
    $('.' + prefix + 'Form').show();
}

function reduceFraction(numerator, denominator) {
    var gcd = greatestCommonDivisor(numerator, denominator);
    return [numerator / gcd, denominator / gcd];
}

function greatestCommonDivisor(a, b) {
    return b ? greatestCommonDivisor(b, a % b) : a;
};