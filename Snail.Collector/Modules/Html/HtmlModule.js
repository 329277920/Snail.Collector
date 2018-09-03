var saveFunc = lib.System.Func(lib.System.String, lib.System.Boolean);

http.save = new postFunc(function (uri, data) {
    return http.post(uri, data != undefined ? unity.toForm(data) : "");
});