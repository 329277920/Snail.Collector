var document = http.get(task.uri).toHtml();

document.find("div.list").find("li").each(function (li) {
    var uri = li.find("div.imgr").find("a").attr("href");
    if (uri) {
        task.add({ uri: uri, script: "Script/xxx/100-detail.js" });
    }
    // return false;
});
// return;
document.find("div.pagebar").find("a").each(function (a) {
    var uri = a.attr("href");
    if (uri) {
        task.add({ uri: uri, script: "Script/xxx/100-list.js" });
    }
});