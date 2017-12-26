function parse() {
    var mobile = "13058037012";
    var spus = new Array();
    spus.push(5780808,2862407,2319964,2383467,7952636);
    var spus_Sale = new Array();
    spus_Sale.push(4875788,2862407,9324403,2728308,2533460);

    // var baseUri = "http://devapi.manjd.com/api/";
    var baseUri = "http://localhost:56695/";
           
    http.headers.add("shopid","6");
    http.headers.add("sharesource","1");    

    for(var i=0;i<1000;i++){
         var rest = http.get(baseUri + "goods/m/getcate").toJson();    
         log.debug("获取分类:" + rest.code);
    }   
}