function run() {
    // var uri = 'https://detail.tmall.com/item.htm?spm=a220m.1000858.1000725.20.65ad5cb2EojIUE&id=561067099846&skuId=3675770114922&areaId=440300&standard=1&user_id=2146022675&cat_id=2&is_b=1&rn=bb45738939e159e7978ac0c15c8826e9';
    var uri = 'http://192.168.10.82:9020/shop/m/test';
    // 自定义请求头
    http.headers.add("client", "Snail_Collector");
        
    // 获取文档
    // var doc = http.get(uri).toHtml();
    var doc = http.get(uri);
    
    return;
    
    console.writeLine(doc.find("script").length);
    
    doc.find("script").each(function(script){
    	 var str = script.innerHTML;
    	 if(str.indexOf('TShop.poc(\'buyshow\')') >=0){
    	 	 // var strJson = str.substring()
    	 	 var idx = str.indexOf("{\"api\":{\"");
    	 	 str = str.substring(idx);
    	 	 str = str.substring(0,str.lastIndexOf('}'));
    	 	 str = str.substring(0,str.lastIndexOf('}')+1);
    	 	 var obj = str.toJson();
    	 	 for(var item in obj.valItemInfo.skuMap){
    	 	 	  console.writeLine(obj.valItemInfo.skuMap[item].price);
    	 	 	  console.writeLine(obj.valItemInfo.skuMap[item].skuId);
    	 	 }    	   
    	 }
    });          
    	          
    // console.writeLine(doc.innerHTML);

    // 返回成功
    return 1;
}