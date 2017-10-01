function _init_module_func_http() {
    var http = new _ref_module_type_HttpModule();
    // 定义类型
    var a_s = lib.System.Action(lib.System.String);
    var a_s_call = lib.System.Action(lib.System.String, lib.System.Object);

    // 定义导出方法
    http.getString = new a_s_call(function (uri, callBack) {
        http._innerGetString(uri, new a_s(callBack));
    });

    return http;
}