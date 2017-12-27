function run() {    

    // 加载资源
    source.importFile(1,"c:\\账户.txt");
    
    // 加载统计
    stat.reg(1,"用户获取验证码接口");
    stat.reg(2,"用户登录接口");
   
    var baseUri = "http://devapi.manjd.com/api/";
        
    // var baseUri = "http://192.168.10.82:9020/";
           
    http.headers.add("nonce","1.0.0");
    http.headers.add("market","appstore");
    http.headers.add("appversion","ios-25");
    
    for(var i=0;i<1;i++)
    {        
        var mobile = source.next(1).userName;
        // console.writeLine(source.next(1).userName);   
        
        stat.start(1);
        var rest = http.postJson(baseUri + "user/v1/app/getcode",{ mobile:mobile,smstype:1 }).toJson();    
        
        if(!rest || rest.code != 1){
              stat.error(1);             
              continue;
        }          
        stat.success(1);         
        
        stat.start(2);
        rest = http.postJson(baseUri + "user/v1/app/login",{mobile: mobile,smscode: "258456"}).toJson();
        if(!rest || rest.code != 1){
              stat.error(2);
              continue;
        }    
        stat.success(2);
        
        host.sleep(1000);
    }   
    return 1;
}   

