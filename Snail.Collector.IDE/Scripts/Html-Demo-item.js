var content = "";
var imgs = new Array();
var info = {};

function parse() {    
    storage.config({
        provider:"mysql",
        connectionString:"server=localhost;database=1yyg;uid=root;pwd=chennanfei;CharSet=UTF8;"
    });
    var uri = 'https://www.94677.com/yule/26370.html'; 
    var pIdx = 0;
    var newUri = parseItem(uri , ++pIdx);
    while(newUri){
    	newUri = parseItem(newUri, ++pIdx);
    }    
    http.getFile(imgs);
    info.content = content;   
    log.debug(info.content);
    
    log.debug(storage.add("news",info));
}

function parseItem(uri,idx){        
    var doc = http.get(uri).toHtml();    
    var div = doc.find("div.newsnr");
    var pTitle = div.find("p.source");    
    if(idx == 1){
        info.title = div.children()[0].innerText;
        log.debug("title:" + div.children()[0].innerText);    
        log.debug("time:" + pTitle.children()[1].innerText);
    }   
    div.children()[3].find("img").each(function(img){
        var src = img.attr("src").toUri(uri);
        imgs.push({ uri:src, savePath: "F:\\images\\Html-Demo\\" + src.subFileName() });
    	img.attr("src",src.subFileName());
    });
    div.children()[3].removeAttr("style","class");
    
    content += div.children()[3].innerHTML;
    
    // 分页
    var pNext = "";
    doc.find("div.pag2").find("a").each(function(a){
    	if(a.innerText == "下一页"){
    	    var href = a.attr("href");
    	    if(href && href != "/"){
    	       pNext = a.attr("href").toUri(uri);
    	    }    		
    	}
    });
   
    return pNext;
}