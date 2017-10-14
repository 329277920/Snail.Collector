// 定义异步回调方法原型
var asyncCallBack = lib.System.Action(core.Snail.Collector.Modules.Core.JSCallBackEventArgs);

// 定义通用集合
// var MyArray = core.Snail.Collector.Modules.Core.JSArray;


// 扩展string
String.prototype.removeClass = function () {
    return this.replace(/class=['"]?[a-zA-Z0-9]*['"]?/ig, "");
}

// 扩展string
String.prototype.removeScript = function () {
    return this.replace(/class=['"]?[a-zA-Z0-9]*['"]?/ig, "");
}