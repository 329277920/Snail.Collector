function run() {
    // var uri = 'http://192.168.10.82:9998/verify/realname?name=林琼英1&idCard=430923198510150042&bankCard=622908363008922110&mobile=15573141373';
    
    // var uri = 'http://192.168.10.82:9998/verify/realname?name=林琼英1&idCard=430923198510150042&bankCard=622908363008922110&mobile=15573141373';
    // var uri = 'http://devsvc.manjd.com/third/verify/realname?name=林琼英&idCard=430923198510150042&bankCard=622908363008922110&mobile=15573141373';
    var uri = 'http://checksvc.manjd.com/third/verify/realname?name=林琼英&idCard=430923198510150042&bankCard=622908363008922110&mobile=15573141373';
    
    // var uri = 'http://checksvc.manjd.com/third/verify/realname?name=fdsfs&idCard=fsdfs&bankCard=fsd&mobile=fsd';
    
    console.writeLine(http.get(uri).toString());
   
    // 返回成功
    return 1;
}