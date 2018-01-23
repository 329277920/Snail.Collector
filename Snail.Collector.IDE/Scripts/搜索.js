function run() {
    var uri = 'http://192.168.10.82:9765/goods/v1/search';

    // 自定义请求头
    http.headers.add("client", "Snail_Collector");

    // 获取文档
    var result = http.postJson(uri,{
       keyword:"华为手机",
       page_start_index:1,
       page_end_index:100,
       user_id:1,
       store_id:1,
       request_time:'2015-01-01'
    }).toString();
    
	console.writeLine(result);

    // 返回成功
    return 1;
}