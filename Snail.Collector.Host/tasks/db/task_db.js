function parse(uri) {

    var storage = require("storage");
    var config = {
        driver: "mysql",
        connection: "server=localhost;database=1yyg;uid=root;pwd=chennanfei;"
    };

    /* 测试写入 *********************************************************************/

    // 写入单条记录       
    var count = storage.insert(config, "news", { id: 1, title: "cnf", content: "heheeee1" });
    host.debug("insert:" + count);

    // 写入多条记录
    count = storage.insert(config, "news", { id: 2, title: "cnf", content: "heheeee2" },
        { id: 3, title: "cnf", content: "heheeee3" });
    host.debug("insert:" + count);

    // 写入多条记录
    var entities = new Array();
    entities.push({ id: 4, title: "cnf", content: "heheeee4" });
    entities.push({ id: 5, title: "cnf", content: "heheeee5" });
    entities.push({ id: 6, title: "cnf", content: "heheeee6" });
    count = storage.insert(config, "news", entities);
    host.debug("insert:" + count);

    /* 测试更新 *********************************************************************/

    // 更新单条记录
    count = storage.update(config, "news",
        { id: 1 },
        { title: "update1", content: "update111" });
    host.debug("update:" + count);

    // 更新多条记录
    count = storage.update(config, "news", { id: { "$p": "id" } },
        { id: 2, title: "update2", content: "update222" },
        { id: 3, title: "update3", content: "update333" }
    );
    host.debug("update:" + count);

    // 更新多条
    var entities = storage.select(config, "news", {
        id: { "$in": [4, 5, 6, 7] }
    });
    host.debug("select:" + entities.length);
    entities.each(function (item) { item.content = "pilianggengxin" });
    count = storage.update(config, "news", { id: { "$p": "id" } }, entities);
    host.debug("update:" + count);


    /* 测试查询 *********************************************************************/
    // 查询单条记录
    var content = storage.single(config, "news", { id: 1 }).content;
    host.debug("select:" + content);

    // 查询多条
    var items = storage.select(config, "news", { id: { "$in": [1, 2, 3, 40] } });
    host.debug("select:" + items.length);


    /* 测试删除 *********************************************************************/
    count = storage.delete(config, "news", { id: 10 });
    host.debug("delete:" + count);

    count = storage.delete(config, "news", { id: 1 });
    host.debug("delete:" + count);

    var entities = storage.select(config, "news", {
        id: { "$in": [4, 5, 6, 7] }
    });
    host.debug("select:" + entities.length);

    count = storage.delete(config, "news", { id: { "$p": "id" } }, entities);
    host.debug("delete:" + count);

    count = storage.delete(config, "news", { id: { "$in": [2, 3] } });
    host.debug("delete:" + count);
}