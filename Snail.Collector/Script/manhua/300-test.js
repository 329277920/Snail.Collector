var directory = "D:\\Projects\\Snail.Collector\\Snail.Collector\\Script\\manhua\\300";

task.uri = "http://www.1kkk.com/manhua-kongbu/";

http.headers.add("Referer", task.uri);

var doc = http.get(task.uri).toHtml();

doc.find("ul.mh-list").find("li").each(function (li) {
    var href = li.find("div.mh-item-tip-detali").find("a").attr("href");
    if (href) {
        href = task.absoluteUri(href);        
        task.uri = href;
        return false;   
    }   
});

doc = http.get(task.uri).toHtml();

// 缩略图
doc.find("div.banner_detail_form").find("div.cover").find("img").save({
    type: "file",
    directory: directory
});

// 章节
doc.find("#chapterlistload").find("li").each(function (li) {
    var href = li.find("a").attr("href");
    if (href) {
        href = task.absoluteUri(href);
        task.uri = href;
        debug.writeLine(task.uri);
        return false;
    }   
});

// 内容
doc = http.get(task.uri).toHtml();

doc.find("head").find("script").each(function (script) {
    var src = script.attr("src");
    if (src) {
        eval(http.get(src).toString());
    }
    else {
        eval(script.innerText);
        return false;
    }     
});
debug.writeLine(DM5_MID);
//debug.writeLine(doc.find("#cp_image").length);
//doc.find("#cp_image").save({
//    type: "file",
//    directory: directory
//});


