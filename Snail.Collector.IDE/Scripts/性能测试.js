function run() {
    // var uri = 'http://devapi.manjd.com/api/shop/m/info';    
    // var uri = "http://192.168.10.82:9020/shop/m/test";
    // var uri = "http://myapi.manjd.com/common/m/test";
    var uri = "http://192.168.10.82:9020/shop/m/info";
    http.headers.add("shopid","6");
    http.headers.add("sharesource","1");   
    
    stat.reg(1,uri);
    
    for(var i=0;i<500;i++){        
        var data = http.get(uri).toJson();         
        // console.writeLine(data.toString());     
        if(!data || data.code != 1){   
            if(data){
                log.error(data.toString());
            }           
            else{
                log.error("空");
            }
            stat.error(1);
        }     
        else{
        	stat.success(1);
        }
        host.sleep(1);
    }       
}