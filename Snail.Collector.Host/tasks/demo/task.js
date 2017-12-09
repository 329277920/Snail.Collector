function config() {    
    return {
        taskId: 1,
        taskName: "测试Http",
        parallel: 4,
        url: "xxxx.com",
        script: "task.js"
    };
}

function parse(url) {
    // 建立10个任务
    for (var i = 1; i <= 10; i++) {
        host.newTask(i + ".html", "task_item.js");
    }
    return 1;
}  