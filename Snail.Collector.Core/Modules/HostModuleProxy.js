
function info(message) {
    host.Info(message);
}
function using(module) {
    xHost.import(module);
}

function infoAsync(message, callBack) {
    var action = lib.System.Action;
    host.InfoAsync(message, new action(callBack));
}

function infoAsync2(message, callBack) {
    var func = lib.System.Func(lib.System.Boolean,lib.System.String);
         
    host.InfoAsync2("谢谢的你爱", new func(function (s) { return "fdsf"; }));

    //var newCallBack = host.newObj(func(booleanType, stringType), function (success) { return "fds"; });
    //host.InfoAsync(message, newCallBack);

    //var List = host.type('System.Collections.Generic.List');
    //var DayOfWeek = host.type('System.DayOfWeek');
    //var week = host.newObj(List(DayOfWeek), 7);
    //week.Add(DayOfWeek.Sunday);
    //info(week[0].ToString());
}

