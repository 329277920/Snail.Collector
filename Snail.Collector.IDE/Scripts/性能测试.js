function run() {
    // var uri = 'http://devapi.manjd.com/api/shop/m/info';    
    // var uri = "http://192.168.10.82:9020/shop/m/test";
    // var uri = "http://myapi.manjd.com/common/m/test";
    var uri = "http://192.168.10.75:6290/";   // netcore
    // var uri = "http://192.168.10.75:6291/api/values";
    // var uri = "http://192.168.10.75:6292/api/values";  // netcore 
    // var uri = "http://192.168.10.75:9293/api/values";
    stat.reg(1,uri);
    
    for(var i=0;i<1;i++){        
        // var data = http.get(uri).toString();         
        http.get(uri);
        // console.writeLine(data.toString());     
        stat.success(1);
        host.sleep(1);
    }       
}