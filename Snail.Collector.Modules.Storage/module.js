// 定义类型
var a_o_o_o = lib.System.Action(lib.System.Object, lib.System.Object, lib.System.Object);


// 定义导出方法
storage.export = new a_o_o_o(function (config, data, callBack) {
    storage._innerExport(config, data, new asyncCallBack(callBack));
});
