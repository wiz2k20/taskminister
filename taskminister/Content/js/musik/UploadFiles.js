function uploadFiles() {

    var fileUpload = $("#AddFile").get(0);
    var files = fileUpload.files; 

    //console.log("fileUpload: " + fileUpload);

    var songName = $('#AddName').val();
    var songArtist = $('#AddArtist').val();

    formData = new FormData();
    formData.append("file", files[0]);
    formData.append("name", songName);
    formData.append("artist", songArtist);
        
    $.ajax(
        {
            type: "POST",
            url: "/Musik/UploadMusik",
            data: formData,
            processData: false,
            contentType: false,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            success: function (data) {
                
                //console.log("ajax success");

            //    var today = new Date();
            //    var tmpHours = String(today.getHours()).padStart(2, '0');
            //    var tmpMin = String(today.getMinutes()).padStart(2, '0');
            //    var tmpSec = String(today.getSeconds()).padStart(2, '0');
            //    var time = tmpHours + ":" + tmpMin + ":" + tmpSec;

            //    var $edit = $("#logUpload");
            //    var curValue = $edit.val();
            //    var newValue = curValue + "\n" + time;
            //    $edit.val(newValue);
            },
            error: function (request, status, error) {
                alert(request.responseText);
            }
        });
}