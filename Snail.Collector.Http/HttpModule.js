var postFunc = lib.System.Func(lib.System.String, lib.System.Object, lib.System.Object);

http.postForm = new postFunc(function (uri, data) {
    return http.post(uri, data != undefined ? unity.toForm(data) : "");
});

http.postJson = new postFunc(function (uri, data) {
    return http.post(uri, data != undefined ? data.toString() : "", "application/json");
});

http.putJson = new postFunc(function (uri, data) {
    return http.post(uri, data != undefined ? unity.toForm(data) : "", "application/json");
});