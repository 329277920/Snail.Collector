﻿// 截取地址中的文件名
String.prototype.subFileName = function () {
    if (!this) {
        return this;
    }
    var name = this.substring(this.lastIndexOf("/") + 1);
    var idx = name.indexOf("?");
    if (idx >= 0) {
        name = name.substring(0, idx);
    }
    return name;
}

// 转换完整地址
String.prototype.toUri = function (baseUri) {
    baseUri = baseUri === undefined ? "" : baseUri;
    return host.getUri(this + "", baseUri);
}


// 字符串转json
String.prototype.toJson = function () {
    return JSON.parse(this);
}


String.prototype.format = function (args) {
    if (!arguments || !arguments.length || arguments.length <= 0) {
        return this;
    }
    var result = this;
    if (arguments.length === 1 && typeof (args) === "object") {
        for (var key in args) {
            if (args[key] !== undefined) {
                var reg = new RegExp("({" + key + "})", "g");
                result = result.replace(reg, args[key]);
            }
        }
        return result;
    }
    for (var i = 0; i < arguments.length; i++) {
        if (arguments[i] !== undefined) {           
            var regItem = new RegExp("({)" + i + "(})", "g");
            result = result.replace(regItem, arguments[i]);
        }
    }
    return result;
}
