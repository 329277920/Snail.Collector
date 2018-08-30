http.headers.add("Cookie", "pgv_pvi=5816400896; RK=9uHiDLCKSF; pac_uid=1_1099191193; tvfe_boss_uuid=094c5c3f89f59faa; mobileUV=1_15f2dc75e82_6251f; ptcz=d3f5798fec2071307577a78109c6fcd173b452ac54f39fa41c248b8d7225e44f; pgv_pvid=4203130496; o_cookie=1099191193; gdt_original_refer=e.qq.com; gdt_original_full_refer=http%3A%2F%2Fe.qq.com%2F; __root_domain_v=.e.qq.com; _qddaz=QD.2xpugo.tvggwj.jkbu0961; gr_user_id=4176c947-6491-47e7-accc-d3fa25321ed8; pgv_gdtid=__tracestring__; ptui_loginuin=2918053662; pt2gguin=o2918053662; pgv_si=s4513403904; pgv_info=ssid=s7998616736; gdt_refer=e.qq.com; gdt_full_refer=https%3A%2F%2Fe.qq.com%2F; site_type=new; portalversion=new; verifysession=h016fa9177bab98acc507828092faac3860d226b4213505de32b13c4fe97878df9dfc3ea4849bc21ab0; hottagtype=header; _qpsvr_localtk=0.24613118892982677; ptisp=ctc; _qdda=3-1.2mxv1g; _qddab=3-2rv8hw.jlg4k5xd; gr_session_id_8751e4ce852fb210=9f10ed43-208f-4a4a-b1e8-700a3767448b; _qddamta_2852155024=3-0; atlas_platform=atlas; uin=o2918053662; skey=@ygsl7Oshz; dm_cookie=version=new&log_type=internal_click&ssid=s7998616736&pvid=4203130496&qq=2918053662&loadtime=3564&url=https%3A%2F%2Fe.qq.com%2Fads%2F&gdt_refer=e.qq.com&gdt_full_refer=https%3A%2F%2Fe.qq.com%2F&gdt_original_refer=e.qq.com&gdt_original_full_refer=http%3A%2F%2Fe.qq.com%2F&gdt_from=&uid=25553&hottag=atlas_https&hottagtype=header; hottag=atlas_https");
var response = http.get("https://e.qq.com/ec/api.php?mod=report&act=summary&owner=25553&advertiser_id=25553&unicode=true&g_tk=1778856671&orderid=&format=json&page=1&pagesize=10&sdate=2018-08-29&edate=2018-08-29&period=1&searchact=view_count%7Cvalid_click_count&dimension=view_count&time_rpt=0");
if (response.statusCode == 200) {
    var result = response.toJson();
    var message = ("花费:" + result.data.summary.cost + "\r\n");
    message += ("下载量:" + result.data.summary.download + "\r\n");
    message += ("曝光量:" + result.data.summary.view_count + "\r\n");
    message += ("安装量:" + result.data.summary.install_count + "\r\n");
    message += ("点击量:" + result.data.summary.valid_click_count + "\r\n");
    log.info(message);
}

http.headers.add("Cookie", "pgv_pvi=5816400896; RK=9uHiDLCKSF; pac_uid=1_1099191193; tvfe_boss_uuid=094c5c3f89f59faa; mobileUV=1_15f2dc75e82_6251f; ptcz=d3f5798fec2071307577a78109c6fcd173b452ac54f39fa41c248b8d7225e44f; pgv_pvid=4203130496; o_cookie=1099191193; gdt_original_refer=e.qq.com; gdt_original_full_refer=http%3A%2F%2Fe.qq.com%2F; __root_domain_v=.e.qq.com; _qddaz=QD.2xpugo.tvggwj.jkbu0961; gr_user_id=4176c947-6491-47e7-accc-d3fa25321ed8; pgv_gdtid=__tracestring__; pgv_si=s4513403904; pgv_info=ssid=s7998616736; gdt_refer=e.qq.com; gdt_full_refer=https%3A%2F%2Fe.qq.com%2F; site_type=new; portalversion=new; verifysession=h016fa9177bab98acc507828092faac3860d226b4213505de32b13c4fe97878df9dfc3ea4849bc21ab0; hottagtype=header; _qpsvr_localtk=0.24613118892982677; ptisp=ctc; atlas_platform=atlas; gr_session_id_8751e4ce852fb210=ff023002-07ea-447c-afbb-1a723fc1e339; _qddamta_2852155024=3-0; ptui_loginuin=2332325165; uin=o2332325165; skey=@2OSu0jWpd; pt2gguin=o2332325165; _qdda=3-1.2mxv1g; _qddab=3-wacrde.jlg88zul; dm_cookie=version=new&log_type=internal_click&ssid=s7998616736&pvid=4203130496&qq=2332325165&loadtime=3086&url=https%3A%2F%2Fe.qq.com%2Fads%2F&gdt_refer=e.qq.com&gdt_full_refer=https%3A%2F%2Fe.qq.com%2F&gdt_original_refer=e.qq.com&gdt_original_full_refer=http%3A%2F%2Fe.qq.com%2F&gdt_from=&uid=25552&hottag=atlas_https&hottagtype=header; hottag=atlas_https");
var response = http.get("https://e.qq.com/ec/api.php?mod=report&act=summary&owner=25552&advertiser_id=25552&unicode=true&g_tk=1788766355&orderid=&format=json&page=1&pagesize=10&sdate=2018-08-23&edate=2018-08-29&period=2&searchact=view_count%7Cvalid_click_count&dimension=view_count&time_rpt=0");
if (response.statusCode == 200) {
    var result = response.toJson();
    var message = ("花费:" + result.data.summary.cost + "\r\n");
    message += ("下载量:" + result.data.summary.download + "\r\n");
    message += ("曝光量:" + result.data.summary.view_count + "\r\n");
    message += ("安装量:" + result.data.summary.install_count + "\r\n");
    message += ("点击量:" + result.data.summary.valid_click_count + "\r\n");
    log.info(message);
}

http.headers.add("Cookie", "pgv_pvi=5816400896; RK=9uHiDLCKSF; pac_uid=1_1099191193; tvfe_boss_uuid=094c5c3f89f59faa; mobileUV=1_15f2dc75e82_6251f; ptcz=d3f5798fec2071307577a78109c6fcd173b452ac54f39fa41c248b8d7225e44f; pgv_pvid=4203130496; o_cookie=1099191193; gdt_original_refer=e.qq.com; gdt_original_full_refer=http%3A%2F%2Fe.qq.com%2F; __root_domain_v=.e.qq.com; _qddaz=QD.2xpugo.tvggwj.jkbu0961; gr_user_id=4176c947-6491-47e7-accc-d3fa25321ed8; pgv_gdtid=__tracestring__; pgv_si=s4513403904; pgv_info=ssid=s7998616736; gdt_refer=e.qq.com; gdt_full_refer=https%3A%2F%2Fe.qq.com%2F; site_type=new; portalversion=new; verifysession=h016fa9177bab98acc507828092faac3860d226b4213505de32b13c4fe97878df9dfc3ea4849bc21ab0; hottagtype=header; _qpsvr_localtk=0.24613118892982677; ptisp=ctc; atlas_platform=atlas; gr_session_id_8751e4ce852fb210=ff023002-07ea-447c-afbb-1a723fc1e339; _qddamta_2852155024=3-0; _qdda=3-1.2mxv1g; _qddab=3-wacrde.jlg88zul; ptui_loginuin=1440229741; uin=o1440229741; skey=@HzqbhUXud; pt2gguin=o1440229741; hottag=atlas_https; dm_cookie=version=new&log_type=internal_click&ssid=s7998616736&pvid=4203130496&qq=1440229741&loadtime=2568&url=https%3A%2F%2Fe.qq.com%2Fads%2F&gdt_refer=e.qq.com&gdt_full_refer=https%3A%2F%2Fe.qq.com%2F&gdt_original_refer=e.qq.com&gdt_original_full_refer=http%3A%2F%2Fe.qq.com%2F&gdt_from=&uid=38262&hottag=atlas_https&hottagtype=header");
var response = http.get("https://e.qq.com/ec/api.php?mod=report&act=summary&owner=38262&advertiser_id=38262&unicode=true&g_tk=2034882632&orderid=&format=json&page=1&pagesize=10&sdate=2018-08-29&edate=2018-08-29&period=3&searchact=view_count%7Cvalid_click_count&dimension=view_count&time_rpt=1");
if (response.statusCode == 200) {
    var result = response.toJson();
    var message = ("花费:" + result.data.summary.cost + "\r\n");
    message += ("下载量:" + result.data.summary.download + "\r\n");
    message += ("曝光量:" + result.data.summary.view_count + "\r\n");
    message += ("安装量:" + result.data.summary.install_count + "\r\n");
    message += ("点击量:" + result.data.summary.valid_click_count + "\r\n");
    log.info(message);
}