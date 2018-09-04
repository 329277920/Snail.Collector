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
                    task.error("保存文件失败,未指定存储路径");
                }
                if (!settings.baseUri) {
                    settings.baseUri = "";
                }
                var count = fileDowner.downFiles(settings.directory, this, settings.baseUri);
                if (count == -1) {
                    return false;
                }
                state.NewFileCount = count;
                return true;
            case "content":
                var content = this.toString();
                if (!content) {
                    task.error("保存内容失败,内容为空");
                }
                var count = task.content(this.toString());
                if (count == -1) {
                    return false;
                }
                state.NewContentCount = count;
                return true;
            case "task":
                if (!this.uri) {
                    task.error("保存任务失败,未指定uri");
                }
                if (!this.script) {
                    task.error("保存任务失败,未指定script");
                }
                var count = task.add(this);
                if (count == -1) {
                    return false;
                }
                state.NewTaskCount = count;
                return true;
            default:
                task.error("不支持的存储类型:" + settings.type);
                return false;
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
})();