function Playlist() {
    $.ajax({
        type: "POST",
        url: "/Musik/GetPlaylist",
        //url: "/Musik/SQLPlaylist",
        //async: true,
        success: function (data) {
            const app = new APlayer({
                container: document.getElementById('aplayer2'),
                autoplay: true,
                volume: 0.3,
                mutex: true,
                audio: data,
            });
        }
    });
};
Playlist();
