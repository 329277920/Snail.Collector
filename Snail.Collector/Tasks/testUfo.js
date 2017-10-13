var http = host.require('http');
var html = host.require('html');
var storage = host.require('storage');

var config = { target: "mysql", conString: "server=localhost;User Id=root;password=chennanfei;Database=1yyg", table: "news" };

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
    http.getString(url, function (content) {        try {
            var doc = html.load(content);
            var title = doc.css("#info").css("div.biaoti>h1").innerText;
            var info = doc.css("#content>table").outerHTML;
            //var array = new Array();
            //array.push({ title: title, content: info });
            //array.push({ title: title, content: info });

            info = info.replace(/<script.*?>.*?<\/script>/ig, "");

            storage.export(
                config,
                { title: title, content: info },
                function (args) {
                    if (!args.success) {
                        host.debug("callback : " + args.error.message);
                    }
                    else {
                        host.debug("callback : " + args.data);
                    }
                }                
            );          
        }
        catch (ex) {
            host.debug(ex.message);
        }
    });
}

// start
catchList("http://www.qi-wen.com/ufo/");
