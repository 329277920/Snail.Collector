/// < path="JavaScript1.js" />
infoAsync("Hehe", function () {
    info("日志写入成功");
});

infoAsync2("Hehe2", function (success) {
    if (success === true)
    {
        info("日志写入成功2");
    }
    return "回调咯";
})

host.TestObj({ name: "chennanfei" });