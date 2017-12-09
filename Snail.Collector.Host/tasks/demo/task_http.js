function parse(uri) {    
     
    //http.getDoc(uri).find("li.has-sub-menu").each(function (li) {
    //    li.find("a").each(function (a) {
    //        host.debug(a.attr("href") + ":" + a.innerText);
    //    });
    //});

    //var strJson = "{ \"id\" : \"hehe\" }";

    //var obj = strJson.parseJSON();

    //host.debug(obj.id);

    //return;     

    // 测试下载文件
    var rest = http.getFile(
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/1.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/2.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/3.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/4.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/5.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/6.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/7.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/8.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/9.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/10.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/11.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/12.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/13.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/14.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/15.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/16.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/17.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/18.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/19.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/20.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/21.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/22.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/23.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/24.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/25.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/26.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/27.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/28.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/29.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/30.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/31.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/32.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/33.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/34.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/35.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/36.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/37.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/38.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/39.jpg" },
        { uri: "http://devimg.manjd.net/shop/20171102111226580.jpg", savePath: "F:/images/40.jpg" }
    );

    host.debug(rest);
}