function run() {    

    // 测试参数
    var mobile = "13058037023";
    var spus = new Array();
    spus.push(5780808,2862407,2319964,2383467,7952636);
    var spus_Sale = new Array();
    spus_Sale.push(4875788,2862407,9324403,2728308,2533460);

    var baseUri = "http://devapi.manjd.com/api/";
    // var baseUri = "http://192.168.10.82:9020/";
    // var baseUri = "http://devapi.manjd.com/api/";
    // var baseUri= "http://checkapi.manjd.com/api/";
           
    http.headers.add("nonce","1.0.0");
    http.headers.add("market","appstore");
    http.headers.add("appversion","ios-25");
    http.headers.add("source","h5");

    var rest = http.postJson(baseUri + "user/v1/app/getcode",{ mobile:mobile,smstype:1 }).toJson();    
    console.writeLine("发送验证码:" + rest.code);

    // return 1;

    rest = http.postJson(baseUri + "user/v1/app/login",{mobile: mobile,smscode: "258456"}).toJson();
    console.writeLine("用户登录:" + rest.code + ",token:" + rest.data.token);
    http.headers.add("token",rest.data.token);
    
    // return 1;
       
    rest = http.postJson(baseUri + "shop/v1/app/create").toJson();
    console.writeLine("创建店铺:" + rest.code + ",msg:" + rest.message);

    rest = http.postJson(baseUri + "shop/v1/app/edit", { modType:2,content:"" }).toJson();
    console.writeLine("修改店铺描述:" + rest.code + ",msg:" + rest.message);

    rest = http.postJson(baseUri + "shop/v1/app/edit", { modType:1,content:"好店铺1" }).toJson();
    console.writeLine("修改店铺名称:" + rest.code + ",msg:" + rest.message);

    rest = http.get(baseUri + "shop/v1/app/stat").toJson();
    console.writeLine("获取店铺统计信息:" + rest.code + ",商品总数:" + rest.data.totalgoods);   

    rest = http.postFile(baseUri + "shop/v1/app/setLogo","c:\\Logo.jpg").toJson();
    console.writeLine("设置店铺logo:" + rest.code + ",地址:" + rest.data);   

    rest = http.postFile(baseUri + "shop/v1/app/setSignPic","c:\\Logo.jpg").toJson();
    console.writeLine("设置店招图:" + rest.code + ",地址:" + rest.data);  
    
    rest = http.get(rest.data).toFile("c:\\Logo111.jpg");
    console.writeLine("下载店招图:" + rest); 
    
    rest = http.get(baseUri + "shop/v1/app/getSignPics").toJson();
    console.writeLine("获取默认店招图:" + rest.code + ",data:" + rest.data);      
    
     rest = http.get(baseUri + "shop/v1/app/getShop").toJson();
    console.writeLine("获取店铺基础信息:" + rest.code + ",storeName:" + rest.data.storename+ ",logopic:" + rest.data.logopic+"描述:" + rest.data.description);  
    
    spus.each(function(spuId){
    	rest = http.postJson(baseUri + "shop/v1/app/editGoods",{ spuId:spuId,actionType:1 }).toJson();
        console.writeLine("关注:" + rest.code);     
    })
    
    rest = http.postJson(baseUri + "shop/v1/app/goods",{ type:1,pageIndex:1,pageSize:20 }).toJson();
    console.writeLine("获取关注列表:" + rest.code + ",count:" + rest.data.total);    
    
    spus.each(function(spuId){
    	rest = http.postJson(baseUri + "shop/v1/app/editGoods",{ spuId:spuId,actionType:3 }).toJson();
        console.writeLine("取消关注:" + rest.code);     
    });      
    
    spus_Sale.each(function(spuId){
    	rest = http.postJson(baseUri + "shop/v1/app/editGoods",{ spuId:spuId,actionType:2 }).toJson();
        console.writeLine("推荐:" + rest.code);     
    });
    
    rest = http.postJson(baseUri + "shop/v1/app/goods",{ type:2,pageIndex:1,pageSize:20 }).toJson();
    console.writeLine("获取推荐列表:" + rest.code + ",count:" + rest.data.total);                 
    
    spus_Sale.each(function(spuId){
    	rest = http.postJson(baseUri + "shop/v1/app/editGoods",{ spuId:spuId,actionType:4 }).toJson();
        console.writeLine("取消推荐:" + rest.code);     
    });     
}   

