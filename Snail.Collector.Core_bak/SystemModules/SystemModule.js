﻿// 定义异步回调方法原型
// var asyncCallBack = lib.System.Action(core.Snail.Collector.Modules.Core.JSCallBackEventArgs);

// 定义通用集合
// var MyArray = core.Snail.Collector.Modules.Core.JSArray;


// 扩展string，移除所有样式class
String.prototype.removeClass = function () {
    return this.replace(/class=['"]?[a-zA-Z0-9]*['"]?/ig, "");
}

// 扩展string，移除所有超链接
String.prototype.removeLink = function () {
    return this.replace(/<\/?a.*?>/ig, "");
}

// 引用某个模块
require = function (module) {
    return host.require(module);
}

// 字符串转json
String.prototype.parseJSON = function () {
    return JSON.parse(this);
}