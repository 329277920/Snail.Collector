function parse() {
   var uri = 'http://www.qq.com';

    // 自定义请求头
    http.headers.add("client","Snail_Collector");

    // 获取文档
    var doc = http.getDoc(uri);

    // 返回成功
    return 1;
}