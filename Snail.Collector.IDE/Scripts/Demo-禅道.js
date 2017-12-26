function parse() {   
        
    http.headers.add("User-Agent","Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.84 Safari/537.36");
    http.headers.add("Upgrade-Insecure-Requests","1"); 
    http.headers.add("Accept","text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8"); 
    http.headers.add("Accept-Encoding","gzip"); 
    http.headers.add("Accept-Language","zh-CN,zh;q=0.9"); 
    http.headers.add("Cookie","bugTree=show; lastProject=15; bugModule=0; preProductID=2; preBranch=0; lastProduct=2; qaBugOrder=openedDate_desc; lang=zh-cn; device=desktop; theme=default; windowHeight=732; windowWidth=1520; zentaosid=fuctmaqm65hq9aqvaodua293g0");        
    
    
    var uri = 'http://192.168.2.23/zentao/bug-view-792.html';
    var doc = http.get(uri).toHtml();
    // log.debug(http.get(uri).toString());
    var title = doc.find("#titlebar").find("strong[style]").innerText;
    log.debug("title:" + title);
        
    var divMain = doc.find("div.main").removeTag("script","style");   
    
    divMain.find("fieldset").each(function(field){
    	if(field.find("legend").innerText == "重现步骤"){
    		log.debug("重现步骤:" + field.find("div.content").removeAttr("onload","class").outerHTML);
    	}     
    });
         
    return 1;
}


 
