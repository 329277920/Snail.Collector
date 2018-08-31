var date = "2018-08-30";

http.headers.add("Cookie", "pgv_pvi=5816400896; RK=9uHiDLCKSF; pac_uid=1_1099191193; tvfe_boss_uuid=094c5c3f89f59faa; mobileUV=1_15f2dc75e82_6251f; ptcz=d3f5798fec2071307577a78109c6fcd173b452ac54f39fa41c248b8d7225e44f; pgv_pvid=4203130496; o_cookie=1099191193; gdt_original_refer=e.qq.com; gdt_original_full_refer=http%3A%2F%2Fe.qq.com%2F; __root_domain_v=.e.qq.com; _qddaz=QD.2xpugo.tvggwj.jkbu0961; gr_user_id=4176c947-6491-47e7-accc-d3fa25321ed8; pgv_gdtid=__tracestring__; pgv_si=s3474590720; pgv_info=ssid=s6365925549; gdt_refer=e.qq.com; gdt_full_refer=https%3A%2F%2Fe.qq.com%2F; site_type=new; portalversion=new; gr_session_id_8751e4ce852fb210=147d4862-d3a3-4ef9-a8b2-0b4b3f9a8504; _qddamta_2852155024=3-0; hottagtype=header; _qpsvr_localtk=0.45921795699006673; ptisp=ctc; _qdda=3-1.2mxv1g; _qddab=3-a2a3zp.jlhqmrl3; atlas_platform=atlas; ptui_loginuin=2332325165; pt2gguin=o2332325165; uin=o2332325165; skey=@M5XtFjxrb; dm_cookie=version=new&log_type=internal_click&ssid=s6365925549&pvid=4203130496&qq=2332325165&loadtime=3214&url=https%3A%2F%2Fe.qq.com%2Fads%2F&gdt_refer=e.qq.com&gdt_full_refer=https%3A%2F%2Fe.qq.com%2F&gdt_original_refer=e.qq.com&gdt_original_full_refer=http%3A%2F%2Fe.qq.com%2F&gdt_from=&uid=25552&hottag=atlas_https&hottagtype=header; hottag=atlas_https");
var response = http.get("https://e.qq.com/ec/api.php?mod=report&act=summary&owner=25552&advertiser_id=25552&unicode=true&g_tk=892036015&orderid=&format=json&page=1&pagesize=10&sdate=" + date +"&edate=2018-08-30&period=3&searchact=view_count%7Cvalid_click_count&dimension=view_count&time_rpt=1");
if (response.statusCode == 200) {
    var result = response.toJson();
    var message = ("花费:" + result.data.summary.cost + "\r\n");
    message += ("下载量:" + result.data.summary.download + "\r\n");
    message += ("曝光量:" + result.data.summary.view_count + "\r\n");
    message += ("安装量:" + result.data.summary.install_count + "\r\n");
    message += ("点击量:" + result.data.summary.valid_click_count + "\r\n");
    log.info(message);
}

http.headers.add("Cookie", "pgv_pvi=5816400896; RK=9uHiDLCKSF; pac_uid=1_1099191193; tvfe_boss_uuid=094c5c3f89f59faa; mobileUV=1_15f2dc75e82_6251f; ptcz=d3f5798fec2071307577a78109c6fcd173b452ac54f39fa41c248b8d7225e44f; pgv_pvid=4203130496; o_cookie=1099191193; gdt_original_refer=e.qq.com; gdt_original_full_refer=http%3A%2F%2Fe.qq.com%2F; __root_domain_v=.e.qq.com; _qddaz=QD.2xpugo.tvggwj.jkbu0961; gr_user_id=4176c947-6491-47e7-accc-d3fa25321ed8; pgv_gdtid=__tracestring__; pgv_si=s3474590720; pgv_info=ssid=s6365925549; site_type=new; portalversion=new; gr_session_id_8751e4ce852fb210=c94da50e-8073-46d1-a537-30ba74abfcb1; _qdda=3-1.1; _qddab=3-qjs0hh.jlhrmsfr; _qddamta_2852155024=3-0; hottagtype=header; _qpsvr_localtk=0.43613617296369944; ptisp=ctc; ptui_loginuin=1440229741; uin=o1440229741; skey=@4hEZETlAr; pt2gguin=o1440229741; dm_cookie=version=new&log_type=url&ssid=s6365925549&pvid=4203130496&qq=1440229741&loadtime=3452&url=http%3A%2F%2Fe.qq.com%2Fads%2F&gdt_refer=&gdt_full_refer=&gdt_original_refer=e.qq.com&gdt_original_full_refer=http%3A%2F%2Fe.qq.com%2F&gdt_from=&uid=38262&hottag=atlas_https&hottagtype=header; hottag=atlas_https; atlas_platform=atlas");
var response = http.get("https://e.qq.com/ec/api.php?mod=report&act=summary&owner=38262&advertiser_id=38262&unicode=true&g_tk=1925889688&orderid=&format=json&page=1&pagesize=10&sdate=2018-08-30&edate=2018-08-30&period=3&searchact=view_count%7Cvalid_click_count&dimension=view_count&time_rpt=1");
if (response.statusCode == 200) {
    var result = response.toJson();
    var message = ("花费:" + result.data.summary.cost + "\r\n");
    message += ("下载量:" + result.data.summary.download + "\r\n");
    message += ("曝光量:" + result.data.summary.view_count + "\r\n");
    message += ("安装量:" + result.data.summary.install_count + "\r\n");
    message += ("点击量:" + result.data.summary.valid_click_count + "\r\n");
    log.info(message);
}

http.headers.add("Cookie", "pgv_pvi=5816400896; RK=9uHiDLCKSF; pac_uid=1_1099191193; tvfe_boss_uuid=094c5c3f89f59faa; mobileUV=1_15f2dc75e82_6251f; ptcz=d3f5798fec2071307577a78109c6fcd173b452ac54f39fa41c248b8d7225e44f; pgv_pvid=4203130496; o_cookie=1099191193; gdt_original_refer=e.qq.com; gdt_original_full_refer=http%3A%2F%2Fe.qq.com%2F; __root_domain_v=.e.qq.com; _qddaz=QD.2xpugo.tvggwj.jkbu0961; gr_user_id=4176c947-6491-47e7-accc-d3fa25321ed8; pgv_gdtid=__tracestring__; pgv_si=s3474590720; pgv_info=ssid=s6365925549; site_type=new; portalversion=new; gr_session_id_8751e4ce852fb210=c1a0e726-1806-4379-aad1-a0f9f89d9266; hottagtype=header; _qdda=3-1.1; _qddab=3-z2dwu8.jlht8ieg; _qpsvr_localtk=0.7798234579383738; _qddamta_2852155024=3-0; ptisp=ctc; ptui_loginuin=2918053662; uin=o2918053662; skey=@bGe8xejvK; pt2gguin=o2918053662; hottag=atlas_https; dm_cookie=version=new&log_type=url&ssid=s6365925549&pvid=4203130496&qq=2918053662&loadtime=2778&url=http%3A%2F%2Fe.qq.com%2Fads%2F&gdt_refer=&gdt_full_refer=&gdt_original_refer=e.qq.com&gdt_original_full_refer=http%3A%2F%2Fe.qq.com%2F&gdt_from=&uid=25553&hottag=atlas_https&hottagtype=header; atlas_platform=atlas");
var response = http.get("https://e.qq.com/ec/api.php?mod=report&act=summary&owner=25553&advertiser_id=25553&unicode=true&g_tk=1078395091&orderid=&format=json&page=1&pagesize=10&sdate=2018-08-30&edate=2018-08-30&period=3&searchact=view_count%7Cvalid_click_count&dimension=view_count&time_rpt=1");
if (response.statusCode == 200) {
    var result = response.toJson();
    var message = ("花费:" + result.data.summary.cost + "\r\n");
    message += ("下载量:" + result.data.summary.download + "\r\n");
    message += ("曝光量:" + result.data.summary.view_count + "\r\n");
    message += ("安装量:" + result.data.summary.install_count + "\r\n");
    message += ("点击量:" + result.data.summary.valid_click_count + "\r\n");
    log.info(message);
}