
// 测试通过
//var uri = "http://duanziwang.com/category/%E7%BB%8F%E5%85%B8%E6%AE%B5%E5%AD%90/";
//while (uri) {
//    var doc = http.get(uri).toHtml();
//    doc.find("article").each(function (item) {
//        debug.writeLine(item.find("div.post-content").innerText);
//    });
//    uri = doc.find("a.next").attr("href");
//    uri = "";
//}


var uri = "https://www.qiushibaike.com/text/";
while (uri) {
    var doc = http.get(uri).toHtml();    
    doc.find("div.item").find("a.text").each(function (item) {
        debug.writeLine(item.innerText);
       
    });
    uri = doc.find("a.next").attr("href");
    uri = "";
}


