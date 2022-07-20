function PlaylistRemove(id) {
    $.ajax({
        type: "POST",
        url: "/Musik/PlaylistRemove",
        data: { id: id },
        success: function (data) {
            //console.log(data.ajax);
            var yeOkey = "A música com o ID " + id + " foi removida!";
            var noOkey = "Nenhuma música foi removida!";
            if (data.jax != "0") { $('#resultRemove').html(yeOkey); }
            else { $('#resultRemove').html(noOkey); }
        }
    });
};