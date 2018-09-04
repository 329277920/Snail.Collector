var doc = http.get(task.uri).toHtml().find("div.post");
doc.find("div.rights").remove();
doc.find("div#share").remove();
if (!doc.find("img").save({ type: "file", directory: "I:\\images\\demo" })) {
    task.error("未下载到图片");
}
var title = doc.find("div.title").find("h1").innerText;
if (!title) {
    task.error("未获取到标题");
}
var content = doc.find("div.article_content").innerHTML;
if (!content) {
    task.error("未获取到内容");
}
({ title: title, content: content }).save({ type: "content" });