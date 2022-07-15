
function uploadFiles(inputId) {
    var files = $('#' + inputId).prop("files");
    var songName = $('#AddName').val();
    var songArtist = $('#AddArtist').val();

    formData = new FormData();
    formData.append("xfile", files[0]);
    formData.append("xname", songName);
    formData.append("xartist", songArtist);

    startUpdatingProgressIndicator();

    $.ajax(
    {
        type: "POST",
        url: "/Musik",
        data: formData,
        processData: false,
        contentType: false,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
            $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (data) {
            stopUpdatingProgressIndicator();
            //console.log(data.dbInsert);
            //console.log(data.infoSize);

            var today = new Date();
            var tmpHours = String(today.getHours()).padStart(2, '0');
            var tmpMin = String(today.getMinutes()).padStart(2, '0');
            var tmpSec = String(today.getSeconds()).padStart(2, '0');
            var time = tmpHours + ":" + tmpMin + ":" + tmpSec;

            var $edit = $("#logUpload");
            var curValue = $edit.val();
            var newValue = curValue + "\n" + time;
            $edit.val(newValue);
        },
        error: function (request, status, error) {
          alert(request.responseText);
          stopUpdatingProgressIndicator();
        }
    });
}

var intervalId;
function startUpdatingProgressIndicator() {
  $("#pLabel").css('visibility', 'visible');
  intervalId = setInterval(
    function () {
      $.post("/Musik/Progress",
        function (pResult) {
          $("#pLabel").html("Status: " + pResult.status + "% - Uploaded: " + pResult.kbytes + "KB");
        }
      );
    },
    10
  );
}
  
function stopUpdatingProgressIndicator() {
  clearInterval(intervalId);
  $("#AddName").val('');
  $("#AddArtist").val('');

  var input = $("#files");
  input.replaceWith(input.val('').clone(true));
  
  setTimeout(hideStatus, 5000);
}

function hideStatus(){
  $("#pLabel").css('visibility', 'hidden'); 
}
