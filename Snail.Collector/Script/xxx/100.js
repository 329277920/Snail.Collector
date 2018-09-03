var uri = "http://www.ruanyifeng.com/blog/2017/12/blockchain-tutorial.html?hmsr=toutiao.io&utm_medium=toutiao.io&utm_source=toutiao.io";
var doc = http.get(uri).toHtml();
var content = doc.find("div#main-content");
content.find("img").save("F:/images/100");
log.info(content.outerHTML);
return;
task.add({
    uri: "https://www.kqiwen.com/list/3.html",
    script: "Script/宇宙探索/100-list.js"
});
