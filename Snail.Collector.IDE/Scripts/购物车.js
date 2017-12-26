function parse() {
  var baseUri = "http://devapi.manjd.com/api/";
           
    http.headers.add("nonce","1.0.0");
    http.headers.add("market","appstore");
    http.headers.add("appversion","ios-25");

    var rest = http.postJson(baseUri + "user/v1/app/getcode",{ mobile:"13058037012",smstype:1 }).toJson();
    log.debug("发送验证码:" + rest.code);

    rest = http.postJson(baseUri + "user/v1/app/login",{mobile: "13058037012",smscode: "258456"}).toJson();
    log.debug("用户登录:" + rest.code + ",token:" + rest.data.token);
    http.headers.add("token",rest.data.token);
       
    rest = http.postJson(baseUri + "shop/v1/app/create").toJson();
    log.debug("创建店铺:" + rest.code + ",msg:" + rest.message);

    rest = http.get(baseUri + "shop/v1/app/getShop").toJson();
    log.debug("获取店铺基础信息:" + rest.code + ",storeName:" + rest.data.storename);   

    rest = http.get(baseUri + "shop/v1/app/stat").toJson();
    log.debug("获取店铺统计信息:" + rest.code + ",商品总数:" + rest.data.totalgoods);   

    rest = http.postFile(baseUri + "shop/v1/app/setLogo","c:\\Logo.jpg").toJson();
    log.debug("设置店铺logo:" + rest.code + ",地址:" + rest.data);   

    rest = http.postFile(baseUri + "shop/v1/app/setSignPic","c:\\Logo.jpg").toJson();
    log.debug("设置店招图:" + rest.code + ",地址:" + rest.data);   

    rest = http.get(rest.data).toFile("c:\\Logo111.jpg");
    log.debug("下载店招图:" + rest);   
}