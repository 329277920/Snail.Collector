// 测试css选择器
var doc = http.get('https://toutiao.io/search?utf8=%E2%9C%93&q=JWT').toHtml();
doc.find("a[rel='external']").each(function (item) {
    debug.writeLine("title: " + item.attr("title"));
    debug.writeLine("href: " + item.attr("href"));
});

// 测试文件下载
var uri = 'http://blog.guoyb.com/2018/08/18/cpu-cores/?hmsr=toutiao.io&utm_medium=toutiao.io&utm_source=toutiao.io';
var doc = http.get(uri).toHtml();
var div = doc.find("div.content");
debug.writeLine(div.find("time").innerText);
debug.writeLine(div.find("h1").innerText); 
var savePath = "F:/images/demo";
var idx = 0;
var imgs = new Array();
div.find("div.entry").find("img").each(function (img) {
    var uri = img.attr("src");
    var fileName = uri.subFileName(uri);
    imgs.push({
        uri: uri,
        savePath: savePath + "/" + fileName
    });
    img.attr("src", fileName);
    debug.writeLine(fileName);
});
http.getFiles(imgs);

// 移除标签
div.removeTag("a", "script");
var content = div.find("div.entry").innerHTML;

return 1;


