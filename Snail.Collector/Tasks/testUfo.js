var http = host.require('http');
var html = host.require('html');

http.getString("http://www.qi-wen.com/ufo/", getList);

function getList(content) {
    var pages = new Array();
    var doc = html.load(content);     
    var tags = doc.getElementById("list").getElementsByTagName("li", true).getElementsByClassName("title", true).getElementsByTagName("a");
    for (var i = 0; i < tags.length; i++) {
        host.debug(tags[i].getAttribute("href"));
        pages.push(tags[i].getAttribute("href"));
    }
}

function 