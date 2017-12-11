function config() {    
    return {
        taskId: 1,
        taskName: "测试Http",
        parallel: 4,
        url: "xxxx.com",
        script: "task.js",
        storage: {
            driver: "mysql",
            connection: "server=localhost;database=1yyg;uid=root;pwd=chennanfei;"
        },
    };
}

function parse(url) {
    // 建立10个任务测试
    for (var i = 1; i <= 10; i++) {
        host.newTask(i + ".html", "task_item.js");
    }
    return 1;
}  