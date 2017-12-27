function run() {
   
   //var uri = "http://192.168.10.82:9999/default/get";
   
   var uri = "http://192.168.10.82:9999/async/readAsync";    
   stat.reg(1,uri);    
   
   for(var i=0;i<100;i++){ 
        stat.start(1);
        var doc = http.get(uri).toString();      
        stat.success(1);
    }      
}