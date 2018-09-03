// 将对象保存为本地文件，目前只支持图片
// directory : 保存的主目录
Object.prototype.save = function (directory) {
    var imgs = new Array();
    var img = convertToLocalFile(directory, this);
    if (img) {
        imgs.push(img);
    }    
    if (this.each) {
        this.each(function (element) {
            img = convertToLocalFile(directory, element);
            if (img) {
                imgs.push(img);
            }
        });
    }   
    return http.getFiles(imgs);
}

function convertToLocalFile(directory, element) {
    if (element.attr) {
        var uri = element.attr("src");
        if (uri && (uri.indexOf("http://") >= 0 || uri.indexOf("https://") >= 0)) {
            var fileName = uri.subFileName();
            element.attr("src", fileName);
            var filePath = (directory + "/" + fileName);
            return { uri: uri, savePath: filePath };
        }                  
    }
    return null;
}


Date.prototype.format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}