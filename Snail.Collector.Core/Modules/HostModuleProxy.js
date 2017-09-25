
function info(message) {
    host.Info(message);
}
function using(module) {
    xHost.import(module);
}

function infoAsync(message, callBack) {
    var action = host.type("System.Action");   
    host.InfoAsync(message, new action(callBack));
}

function infoAsync2(message, callBack) {
    var func = host.type("System.Func");
    

    var stringType = host.type("System.String");
    var booleanType = host.type("System.Boolean");

    host.Test(func(booleanType, stringType));

    //var newCallBack = host.newObj(func(booleanType, stringType), function (success) { return "fds"; });
    //host.InfoAsync(message, newCallBack);

    //var List = host.type('System.Collections.Generic.List');
    //var DayOfWeek = host.type('System.DayOfWeek');
    //var week = host.newObj(List(DayOfWeek), 7);
    //week.Add(DayOfWeek.Sunday);
    //info(week[0].ToString());
}

