// 截取文件名
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

String.prototype.toUri = function (baseUri) {
    baseUri = baseUri == undefined ? "" : baseUri;
    return host.getUri(this + "", baseUri);
}


// 扩展string，移除所有样式class
String.prototype.removeClass = function () {
    return this.replace(/class=['"]?[a-zA-Z0-9]*['"]?/ig, "");
}

// 扩展string，移除所有超链接
String.prototype.removeLink = function () {
    return this.replace(/<\/?a.*?>/ig, "");
}

// 字符串转json
String.prototype.toJson = function () {
    return JSON.parse(this);
}