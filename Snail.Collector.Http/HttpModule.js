var postFormFunc = lib.System.Func(lib.System.String, lib.System.Object, lib.System.Object);

http.postForm = new postFormFunc(function (uri, data) {
    return http.post(uri, data != undefined ? data.toForm() : "");
});