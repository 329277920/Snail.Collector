var doc = http.get("https://www.kqiwen.com/qiwen/16033.html").toHtml().find("div.post");
doc.find("div.rights").remove();
doc.find("div#share").remove();
var title = doc.find("div.title").find("h1").innerText;
debug.writeLine(doc.find("div.title").find("h1").innerText);
debug.writeLine(doc.find("div.article_content").innerHTML); 

