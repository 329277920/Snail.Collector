function parse() {
   var uri = 'https://www.baidu.com/s?ie=utf-8&mod=1&isbd=1&isid=A3574D8728F71060&ie=utf-8&f=8&rsv_bp=1&rsv_idx=1&tn=baidu&wd=%E5%BE%AE%E4%BF%A1%E6%94%AF%E4%BB%98&oq=%25E5%25BE%25AE%25E4%25BF%25A1%25E6%2594%25AF%25E4%25BB%2598&rsv_pq=d9656bbc00001bcf&rsv_t=1f1c62NHHAdtjLqcfibF7JqdcnE%2BJ4%2BU5VQLmO61qpgbJfRuTdEtVzz%2BT94&rqlang=cn&rsv_enter=0&bs=%E5%BE%AE%E4%BF%A1%E6%94%AF%E4%BB%98&rsv_sid=undefined&_ss=1&clist=&hsug=&f4s=1&csor=0&_cr1=29300';

    http.headers.add("User-Agent","Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.84 Safari/537.36");
    
    http.headers.add("Accept","text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8"); 
    http.headers.add("Accept-Encoding","gzip"); 
    http.headers.add("Accept-Language","zh-CN,zh;q=0.9"); 
    http.headers.add("Cookie","BAIDUID=A3574DAB2C73846C05B8F890B5C8728F:FG=1; BIDUPSID=A3574DAB2C73846C05B8F890B5C8728F; PSTM=1496991929; __cfduid=d79da0670f3af24f0a82eaf94e568637a1502099550; MSA_WH=414_736; ispeed=1; BD_UPN=12314353; ispeed_lsm=2; H_PS_PSSID=25355_1444_19037_21103_17001_25178_20929; BD_HOME=0; BD_CK_SAM=1; PSINO=7; H_PS_645EC=1f1c62NHHAdtjLqcfibF7JqdcnE%2BJ4%2BU5VQLmO61qpgbJfRuTdEtVzz%2BT94; WWW_ST=1513663533788");
    // 自定义请求头
    // http.headers.add("client","Snail_Collector");

    // 获取文档
    // var doc = http.get(uri).toHtml();
    
    var doc = http.get(uri).toHtml();
    // doc.removeAttr("style","class").removeTag("style","script","noscript");
    
    doc.find("div.c-container").each(function(div){
    	log.debug(div.find("h3").innerText);
    	log.debug(div.find("h3>a").attr("href"));
    	// log.debug("-----------------------------------");
    });
    
    // log.debug(doc.outerHTML);

    // 返回成功
    return 1;
}