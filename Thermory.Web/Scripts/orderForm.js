var EmptyGuid = '00000000-0000-0000-0000-000000000000';

function customerChanged() {
    var selectedCustomer = $('#customerId option:selected');

    var customerId = selectedCustomer.val();
    var name = customerId == EmptyGuid ? '' : selectedCustomer.text();

    $('#customerName').val(name);
    addressChanged(customerId);
    $('.addressSelector').hide();
    $('#' + customerId + 'AddressSelector').show();
}

function addressChanged(customerId) {
    var selectedAddress = $('#' + customerId + 'AddressId option:selected');

    var name = selectedAddress.val() == EmptyGuid ? '' : selectedAddress.text();
    var addressLine1 = selectedAddress.attr('addressLine1');
    var addressLine2 = selectedAddress.attr('addressLine2');

    $('#addressName').val(name);
    $('#addressLine1').val(addressLine1);
    $('#addressLine2').val(addressLine2);
}

function packagingTypeChanged() {
    var selectedPackagingType = $('#packagingTypeId option:selected');

    var packagingTypeName = selectedPackagingType.val() == EmptyGuid ? '' : selectedPackagingType.text();
    var weight = selectedPackagingType.attr('weight');

    $('#packagingTypeName').val(packagingTypeName);
    $('#packagingTypeWeight').val(weight);
}

function getCustomer() {
    var customerId = $('#customerId').val();
    var name = $('#customerName').val();

    return name == ''
        ? null
        : {
            Id: customerId,
            Name: name,
            Addresses: getAddress(customerId)
        };
}

function getAddress(customerId) {
    var addressId = $('#' + customerId +'AddressId').val();
    var name = $('#addressName').val();

    return name == ''
        ? null
        : [{
            Id: addressId,
            CustomerId: customerId,
            Name: name,
            AddressLine1: $('#addressLine1').val(),
            AddressLine2: $('#addressLine2').val()
        }];
}

function getPackagingType() {
    var name = $('#packagingTypeName').val();
    return name == ''
        ? null
        : {
            Id: $('#packagingTypeId').val(),
            Name: name,
            Weight: $('#packagingTypeWeight').val()
        };
}

function getInventory(prefix) {
    var products = new Array();
    var query = 'input[name=\'' + prefix + 'Qty\']';
    $(query).each(function () {
        var id = $(this).attr('id');
        var productId = $(this).attr('productId');
        var qty = $(this).val();

        if (qty > 0) {
            products.push(createProductLineItem(id, productId, qty));
        }
    });
    return products;
}

function createProductLineItem(id, productId, qty) {
    return {
        Id: id,
        ProductId: productId,
        Quantity: qty
    };
}