var http = host.require('http');

http.getString("http://www.qq.com", function (html) {
    // host.debug(html);
    host.debug("1");
});
http.getString("http://www.qq.com", function (html) {
    // host.debug(html);
    host.debug("2");
});

http.getString2("http://www.qq.com", function (html) {
    return 1;
})

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