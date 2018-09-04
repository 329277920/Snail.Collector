(function () {
    // 任意对象转Json字符串
    Object.prototype.toString = function () {
        return JSON.stringify(this);
    };

    // 扩展对象，增加持久化支持
    Object.prototype.save = function (settings) {
        if (!settings.type) {
            task.error("不支持的存储类型");
        }
        switch (settings.type) {
            case "file":
                if (!settings.directory) {
                    task.error("未指定存储路径");
                }
                if (!settings.baseUri) {
                    settings.baseUri = "";
                }
                return fileDowner.downFiles(settings.directory, this, settings.baseUri);
            case "content":
                return task.content(this.toString());
        }
    };

    // 文件下载器
    fileDowner = {
        downFiles: function (directory, element, baseUri) {
            var imgs = new Array();            
            var img = fileDowner._convertToLocalFile(directory, element, baseUri);
            if (img) {
                imgs.push(img);
            }
            if (element.each) {
                element.each(function (ele) {
                    img = fileDowner._convertToLocalFile(directory, ele, baseUri);
                    if (img) {
                        imgs.push(img);
                    }
                });
            }
            return http.getFiles(imgs);
        },
        _convertToLocalFile: function (directory, element, baseUri) {
            if (element.attr) {
                var uri = element.attr("src");             
                if (uri) {
                    // base64方式
                    if (uri.indexOf("data:image") >= 0) {

                    }
                    else {
                        var fileName = uri.subFileName();
                        element.attr("src", fileName);
                        var filePath = (directory + "/" + fileName);
                        return { uri: task.absoluteUri(uri, baseUri), savePath: filePath };
                    }
                }
            }
            return null;
        }
    };

    // 内容存储器

})();