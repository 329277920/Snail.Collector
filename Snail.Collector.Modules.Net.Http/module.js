// 定义类型
var a_s = lib.System.Action(lib.System.String);
var f_s_s = lib.System.Func(lib.System.String, lib.System.String);
var a_s_call = lib.System.Action(lib.System.String, lib.System.Object);

// 定义导出方法
http.getString = new a_s_call(function (uri, callBack) {
    http._innerGetString(uri, new a_s(callBack));
});


http.getString2 = new a_s_call(function (uri, callBack) {
    var innerCb = new f_s_s(function (content) {
        var result = callBack(content);
        // 数据类型转换
        return result == 1 ? "cnf" : "lqy";
    });
    http._innerGetString(uri, innerCb);
});