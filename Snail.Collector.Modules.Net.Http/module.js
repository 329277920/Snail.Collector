var getStringDefine = lib.System.Func(lib.System.String);

http.getString = new funcToString(function () {
    return "Hello http!"; 
});

// System.Threading.Tasks.Task

var getStringDefine = lib.System.Func(lib.System.String, lib.System.Action(lib.System.String));

//var callBackHandler = new callBack(function (content) {
    
//});

http.getString = new getStringDefine(function (url, callBack) {
    var innerCallBack = new lib.System.Action(lib.System.String)(function (content) {
        if (callBack != null) {
            callBack(content);
        }
    });

    http.innerGetString(url, innerCallBack);
});