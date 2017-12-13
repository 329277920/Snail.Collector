function parse(uri) {       
    var doc = http.getDoc(uri);  
    var imgs = new Array();
    doc.find("li.gl-item").each(function (li) {
        var src = li.find("div.p-img").find("a").attr("href");
        newTask(src);
        //li.find("div.p-img").find("img").each(function (img) {
        //    if (img.attr("width") == "220") {
        //        var src = img.attr("src");
        //        if (!src) {
        //            src = img.attr("data-lazy-img");
        //        }
        //        if (src) {
        //            imgs.push({ url: "https:" + src, savePath: "F:\\images\\demo\\" + src.getFileName() });                    
        //        }
        //    }
        //});
        //host.debug(li.find("div.p-img").find("a").attr("href"));
    });
    // http.getFile(imgs);
    return 1;
}

function newTask(value) {
    if (value && value.indexOf('item.jd.com') >= 0) {
        host.newTask("https:" + value, "task_item.js");
    }
}
