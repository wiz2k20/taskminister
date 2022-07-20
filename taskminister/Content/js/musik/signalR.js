$(function () {

    //console.log("signalR ATIVO");
    //$.connection.hub.logging = true;

    var musikConnection = $.connection.hubMusik;

    // FUNCTION - Upload Song
    musikConnection.client.progressbarbegin = function () {
        ProgressBarBegin();
    };
    musikConnection.client.progressbarupdate = function (status, size) {
        ProgressBarUpdate(status, size);
    };
    musikConnection.client.progressbarend = function () {
        ProgressBarEnd();
    };

    // FUNCTION - List Of Songs
    musikConnection.client.showlistofsongs = function (lista) {
        //console.log("init ShowListOfSongs hub");
        //console.log("lista: " + lista);
        ShowListOfSongs(lista);
    };

    // HUB START
    $.connection.hub.start().done(function () {
        musikConnection.server.listOfSongs();
        $('#btnUploadZ').click(function () {
            musikConnection.server.musikUpload();
        });
    });

    // FUNCTION - List
    function ShowListOfSongs(lista) {
        var parsedData = JSON.parse(lista);
        //console.log(parsedData);

        $('#getInfoBtn').prop('disabled', 'true');
        $('#getInfoBtn').unbind('click');

        $('#playlistInfo-box').css('visibility', 'visible');

        $.each(parsedData, function (index, element) {
            var markup = "<tr>" +
                "<td>" + element.Name + "</td><td>" + element.Artist + "</td>" +
                "<td><a href='javascript:PlaylistRemove(" + element.Control + ");'>X</a></td>" +
                "</tr>";
            $('#playlistInfo tbody').append(markup);
        });
    };

    // FUNCTION - Upload
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