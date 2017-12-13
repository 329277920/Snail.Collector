function config() {

    // http.headers.add("Cookie", "qrsc=3; __jdv=122270672|direct|-|none|-|1512090325535; TrackID=19DoktNB1PKho8RIXBRAaiwX5WTHWWMF9Z-LxnUHwEFW2JW-3e1j2-CtBVgRuGgaIEfjaL2Uzqi9j-ZDOMUXZnGfcapwhKIW9oG0mGAUZOqg; pinId=c6rlWUbQzUVy7C4CDJaN2g; pin=nanfei_130_m; unick=nanfei_130; _tp=izfBjc0s1bkYQLNttaPFEg%3D%3D; _pst=nanfei_130_m; user-key=4a79dad7-ea42-421b-a6ff-95652497de69; cn=0; sid=388d984bced0fd1badd6ce6cf03da824; ipLocation=%u5E7F%u4E1C; areaId=19; ipLoc-djd=19-1607-47388-0; __jda=122270672.1496992929218399763410.1496992929.1513070136.1513076116.27; __jdb=122270672.2.1496992929218399763410|27.1513076116; __jdc=122270672; xtest=3079.cf6b6759; rkv=V0400; __jdu=1496992929218399763410; 3AB9D23F7A4B3C9B=VT7LZSIIPBKHW65QO6TID55SK36B2DCOHZE7BFDADUWWDG7HRP4U4JX3FONTGTZYHNOAVMQUHTTGHVCTCDKQ25ZY4Y");
    http.headers.add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Safari/537.36");
    http.headers.add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
    http.headers.add("Accept-Encoding", "gzip");

    return {
        taskId: 2,
        taskName: "jd",
        parallel: 4,
        url: "https://search.jd.com/Search?keyword=%E6%B2%99%E5%8F%91&enc=utf-8&wq=%E6%B2%99%E5%8F%91&pvid=dws9n1ri.44bmbn",
        script: "task_list.js",
        storage: {
            driver: "mysql",
            connection: "server=localhost;database=1yyg;uid=root;pwd=chennanfei;"
        },
        resource: {
            // 资源保存路径
            directory: "F:\\images",
            // 文件名生成规则
            generateName: 1,
            // 文件路径生成规则
            generatePath: 1,
        }
    };
}
