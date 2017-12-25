var http = host.require('http');
var html = host.require('html');
var storage = host.require('storage');

var config = { target: "mysql", conString: "server=localhost;User Id=root;password=chennanfei;Database=1yyg", table: "news" };

function catchList(url) {
    http.getString(url, function (content) {
        try {
            var doc = html.load(content);
            var nodes = doc.css("#list>div.title>a");
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
            
            // doc.css("script").remove();
            // doc.css("[class]").removeClass();
            // host.debug(script.length);

            // return;

            var title = doc.css("#info>div.biaoti>h1").innerText;

            host.debug(title);

            return;

            // doc.css("td.content").css("[class]").removeClass();
            
            var info = doc.css("td.content").outerHTML.removeClass();

            // 去掉样式
            // info = info.replace(/class=['"]?[a-zA-Z0-9]*['"]?/ig,"");

            //var array = new Array();
            //array.push({ title: title, content: info });
            //array.push({ title: title, content: info });

            // info = info.replace(/<script.*?>.*?<\/script>/ig, "");

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
