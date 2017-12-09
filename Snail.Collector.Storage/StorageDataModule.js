// 定义类型
var f_o_s_o_ar = lib.System.Func(lib.System.Object,
    lib.System.String, lib.System.Object, Array);

var f_do_s_o_o = lib.System.Func(lib.System.Object,
    lib.System.String, lib.System.Object, lib.System.Object);

// 查询多条记录
storage.select = new f_o_s_o_ar(function (config, table, filter) {
    var items = storage.InnerSelect(config, table, filter);
    if (!items) {
        return null;
    }
    var result = new Array();
    items.each(function (item) {
        result.push(item.parseJSON());
    });
    return result;
});

// 查询单条记录
storage.single = new f_do_s_o_o(function (config, table, filter) {
    var item = storage.InnerSelectSingle(config, table, filter);
    if (!item) {
        return null;
    }
    return item.parseJSON();
});
