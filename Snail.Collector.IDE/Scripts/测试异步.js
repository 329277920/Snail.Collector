function run() {
   
   //var uri = "http://192.168.10.82:9999/default/get";
   
   var uri = "http://192.168.10.82:9999/default/getasync";    
    stat.reg(1,uri);

    

    for(var i=0;i<1;i++){         
   
          // 获取文档
          var doc = http.get(uri).toString();
          
          stat.success(1);
    }
   

    // 返回成功
    return 1;
}