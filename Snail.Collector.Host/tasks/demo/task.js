function config() {    
    return {
        taskId: 1,
        taskName: "demo",
        parallel: 4,
        url: "https://sale.jd.com/act/BClHxZN1mRrb5P.html",
        script: "task.js",
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

function parse(url) {
    // 建立10个任务测试
    for (var i = 1; i <= 10; i++) {
        host.newTask(i + ".html", "task_http.js");
    }
    return 1;
}  