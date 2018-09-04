var document = http.get(task.uri).toHtml();

document.find("div.list").find("li").each(function (li) {
    var uri = li.find("div.imgr").find("a").attr("href");
    if (uri) {
        ({ uri: uri, script: "Script/xxx/100-detail.js" }).save({
            type: "task"
        });
    }
    return false;
});
document.find("div.pagebar").find("a").each(function (a) {
    var uri = a.attr("href");
    if (uri) {
        ({ uri: uri, script: "Script/xxx/100-list.js" }).save({
            type: "task"
        });
    }
});