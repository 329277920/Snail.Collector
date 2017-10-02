var http = host.require('http');
var html = host.require('html');

http.getString("http://www.qq.com", function (html) {
    // host.debug(html);
    // host.debug(html);
});



http.getString("http://www.qq.com", function (html) {
    // host.debug(html);
    host.debug("2");
});

http.getString2("http://www.qq.com", function (html) {
    return 1;
});

http.getJson("http://192.168.0.104:9010/Default/getuser?id=10", function (json) {
    host.debug(json.Age);
});
 



http.getString("http://neihanshequ.com/pic/", function (content) {   
    var doc = html.load(content);   
    var imgs = doc.getElementsByTagName("img");    
    if (imgs.Length > 0) {
        var k = imgs.Length;
        for (var i = 0; i < imgs.Length; i++) {
            var src = imgs[i].getAttribute("data-src");
            if (src) {
                var fileName = src.substr(src.lastIndexOf('/') + 1) + ".jpg";
                var task = http.downFile(src, "Images\\" + fileName, function (s) {
                    host.debug(--k);
                    if (k == 0) {
                        host.debug("Complete");
                    }
                });
            }
            else {
                k--;
            }
        }         
    }
});




//var proxy = lib.System.Action(lib.System.String);

//var callBack = new proxy(function (content) {
//    host.debug(content);
//});

//// http1.getString("http://www.qq.com", callBack);

//http.getString("http://www.qq.com", callBack);

//var proxy2 = lib.System.Func(lib.System.String, lib.System.Boolean);

//var callBack2 = new proxy2(function (content) {
//    host.debug(content);

//    return true;
//});

//http.getString("http://www.qq.com", callBack2);


//http.getStringProxy("http://www.qq.com", function (content) {
//    // host.debug(content);
//});