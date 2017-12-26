function parse() {
    var uri = 'https://www.94677.com/qirenyishi/'; 
    var save = "F:\\images\\Html-Demo\\";
    var doc = http.get(uri).toHtml();    
    var imgs = new Array();
    doc.find("div.c-box").find("li").each(function(li){
    	log.debug(li.find("h3>a").innerText);
    	log.debug(li.find("h3>a").attr("href").toUri(uri));
    	log.debug(li.find("img").attr("src").toUri());
    	var img = li.find("img").attr("src").toUri();
    	if(img){
    	    imgs.push({ uri: img,savePath: save + img.subFileName()});
    	}
    });    
    log.debug("下载文件:" + http.getFile(imgs));
    return 1;
}