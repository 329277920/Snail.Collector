function parse(uri) {
    var imgs = new Array();
    var doc = http.getDoc(uri);
    doc.find("#J-detail-pop-tpl-top-new>img").each(function (img) {
        var src = img.attr("src");
        if (src) {
            imgs.push({ url: src.getUri(), savePath: "F:\\images\\demo\\" + src.getFileName() });
        }
    });
    //imgs.each(function (img) {
    //    host.debug(img.url);
    //});
    http.getFile(imgs);
    return 1;
}



