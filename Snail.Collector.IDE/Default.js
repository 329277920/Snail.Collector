function run() {
    var uri = 'http://www.chennanfei.com';

    // 自定义请求头
    http.headers.add("client", "Snail_Collector");

    // 获取文档
    var doc = http.get(uri).toHtml();

    // 返回成功
    return 1;
}