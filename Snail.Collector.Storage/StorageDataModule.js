// 定义类型
var f_s_o_ar = lib.System.Func(
    lib.System.String, lib.System.Object, Array);

var f_s_o_o = lib.System.Func(
    lib.System.String, lib.System.Object, lib.System.Object);

// 查询多条记录
storage.select = new f_s_o_ar(function (table, filter) {
    var items = storage.InnerSelect(table, filter);
    if (!items) {
        return null;
    }
    var result = new Array();
    items.each(function (item) {
        result.push(JSON.parse(item));
    });
    return result;
});

// 查询单条记录
storage.single = new f_s_o_o(function (table, filter) {
    var item = storage.InnerSelectSingle(table, filter);
    if (!item) {
        return null;
    }
    return JSON.parse(item);
});
