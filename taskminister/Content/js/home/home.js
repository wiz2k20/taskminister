
// Get Public IP
$.getJSON("https://api.ipify.org?format=json", function (data) {
    $("#infoIp").html(data.ip);
})

// Get browser
var nVer = navigator.appVersion;
var nAgt = navigator.userAgent;
var browserName = navigator.appName;
//var fullVersion = '' + parseFloat(navigator.appVersion);
//var majorVersion = parseInt(navigator.appVersion, 10);
var nameOffset, verOffset, ix;

var showCookie = navigator.cookieEnabled;
//$("#infoCookie").html(showCookie);
if(showCookie){
    $("#infoCookie").html("Sim");
}else{
    $("#infoCookie").html("Não");
}

// var showOnline = navigator.onLine;
// $("#infoOnline").html(showOnline);

var showMobile = navigator.userAgentData.mobile;
if(showMobile){
    $("#infoMobile").html("Sim");
}else{
    $("#infoMobile").html("Não");
}

if ((nAgt.indexOf("x64")) != -1) {
    var ark = "64 bits";
}else{
    var ark = "32 bits";
}

// In Opera, the true version is after "Opera" or after "Version"
if ((verOffset = nAgt.indexOf("Opera")) != -1) {
    browserName = "Opera";
    fullVersion = nAgt.substring(verOffset + 6);
    if ((verOffset = nAgt.indexOf("Version")) != -1)
        fullVersion = nAgt.substring(verOffset + 8);
}
// In MSIE, the true version is after "MSIE" in userAgent
else if ((verOffset = nAgt.indexOf("MSIE")) != -1) {
    browserName = "Microsoft Internet Explorer";
    fullVersion = nAgt.substring(verOffset + 5);
}
// In Chrome, the true version is after "Chrome" 
else if ((verOffset = nAgt.indexOf("Chrome")) != -1) {
    browserName = "Chrome";
    fullVersion = nAgt.substring(verOffset + 7);
}
// In Safari, the true version is after "Safari" or after "Version" 
else if ((verOffset = nAgt.indexOf("Safari")) != -1) {
    browserName = "Safari";
    fullVersion = nAgt.substring(verOffset + 7);
    if ((verOffset = nAgt.indexOf("Version")) != -1)
        fullVersion = nAgt.substring(verOffset + 8);
}
// In Firefox, the true version is after "Firefox" 
else if ((verOffset = nAgt.indexOf("Firefox")) != -1) {
    browserName = "Firefox";
    fullVersion = nAgt.substring(verOffset + 8);
}
// In most other browsers, "name/version" is at the end of userAgent 
else if ((nameOffset = nAgt.lastIndexOf(' ') + 1) <
    (verOffset = nAgt.lastIndexOf('/'))) {
    browserName = nAgt.substring(nameOffset, verOffset);
    fullVersion = nAgt.substring(verOffset + 1);
    if (browserName.toLowerCase() == browserName.toUpperCase()) {
        browserName = navigator.appName;
    }
}

var valueShowBrowser = browserName;
$("#infoBrowser").html(valueShowBrowser);

// Get system
var OSName = "Unknown OS";
if (navigator.appVersion.indexOf("Win") != -1) OSName = "Windows";
if (navigator.appVersion.indexOf("Mac") != -1) OSName = "MacOS";
if (navigator.appVersion.indexOf("X11") != -1) OSName = "UNIX";
if (navigator.appVersion.indexOf("Linux") != -1) OSName = "Linux";
$("#infoSystem").html(OSName + " ( " + ark + " )");

// Get screen size
var screenW = 640, screenH = 480;
if (parseInt(navigator.appVersion) > 3) {
    screenW = screen.width;
    screenH = screen.height;
}
else if (navigator.appName == "Netscape"
    && parseInt(navigator.appVersion) == 3
    && navigator.javaEnabled()
) {
    var jToolkit = java.awt.Toolkit.getDefaultToolkit();
    var jScreenSize = jToolkit.getScreenSize();
    screenW = jScreenSize.width;
    screenH = jScreenSize.height;
}
var valueShow = screenW + " x " + screenH;
$("#infoScreen").html(valueShow);