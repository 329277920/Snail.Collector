function run() {

    var testUri = "https://api.manjd.com/PMSApi/api/Product/GetStockSkuListBySkuID";
    http.postJson(testUri,{areaID:13565,skuID:[102687002]});
    

    return 1;

    var uri = 'http://192.168.10.82:6000/user/13058037010';
    
    http.headers.add("token","xxxx");
    // 获取文档
    var user = http.get(uri).toJson();
    
	console.writeLine(user.userName)
   // 返回成功
    return 1;
}