var http = host.require('http');
var html = host.require('html');

function catchList(url) {
    http.getString(url, function (content) {
        try {
            var doc = html.load(content);
            var nodes = doc.css("#list").css("div.title>a");
            for (var i = 0; i < nodes.length; i++) {
                catchContent(nodes[i].attr("href"));
            }
        }
        catch (ex) {
            host.debug(ex.message);
        }
    });
}

function catchContent(url) {     
    http.getString(url, function (content) {
        try {
            var doc = html.load(content);
            var title = doc.css("#info").css("div.biaoti>h1").innerText;
            var info = doc.css("#content>table").innerText;
            // host.debug(title + " " + info);
            host.debug("OK");
        }
        catch (ex) {
            host.debug(ex.message);
        }
    });
}

catchList("http://www.qi-wen.com/ufo/");

host.debug("Complete");