var http = host.require('http');


var callBack = lib.System.Action(lib.System.String);

var innerCallBack = new callBack(function (content) {
    //if (callBack != null) {
    //    callBack(content);
    //}
    host.debug(http.toString());
});


var str = http.getString("http://www.qq.com", innerCallBack);

//host.debug(http.toString());