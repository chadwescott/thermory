function LumberTypeHistory(lumberTypeId) {
    var history;
    var postData = '{"lumberTypeId" : "' + lumberTypeId + '"}';

    $.ajax({
        type: 'POST',
        url: '/Inventory/GetLumberTypeHistory',
        contentType: 'application/json',
        dataType: 'json',
        data: postData,
        success: function (data, status, request) {
            console.log(lumberTypeId + ' Ok');
            console.log(data);
            console.log(status);
            console.log(request);
            history = data.records;
        },
        error: function (data, status, request) {
            console.log(lumberTypeId + ' Error');
            console.log(data);
            console.log(status);
            console.log(request);
        }
    });
}