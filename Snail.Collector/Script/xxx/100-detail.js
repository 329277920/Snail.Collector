var doc = http.get(task.uri).toHtml().find("div.post");
doc.find("div.rights").remove();
doc.find("div#share").remove();
doc.find("img").save("I:\\images\\demo");
var title = doc.find("div.title").find("h1").innerText;
var content = doc.find("div.article_content").innerHTML;
task.content({ title: title, content: content }.toString());