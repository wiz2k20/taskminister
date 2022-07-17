$(function () {

    //console.log("signalR nova pasta");
    //$.connection.hub.logging = true;

    var chat = $.connection.hubMusik;

    chat.client.progressbarbegin = function () {
        ProgressBarBegin();
    };

    chat.client.progressbarupdate = function (status, size) {
        ProgressBarUpdate(status, size);
    };

    chat.client.progressbarend = function () {
        ProgressBarEnd();
    };

    $.connection.hub.start().done(function () {
        $('#btnUploadZ').click(function () {
            chat.server.musikUpload();
        });
    });

    function ProgressBarBegin() {
        $("#pLabel").html("");
        $("#pLabel").css('visibility', 'visible');
    };

    function ProgressBarUpdate(status, size) {
        $("#pLabel").html("Status: " + status + "% - Uploaded: " + size + "KB");
    };

    function ProgressBarEnd() {

        $("#pLabel").html("Status: 100% - Upload finalizado!");

        $("#AddName").val('');
        $("#AddArtist").val('');

        var input = $("#files");
        input.replaceWith(input.val('').clone(true));

        setTimeout(hideStatus, 5000);

        function hideStatus() {
            $("#pLabel").css('visibility', 'hidden');
        }
    };

});