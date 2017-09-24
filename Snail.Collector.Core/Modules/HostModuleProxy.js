function using(module) {
    host.import(module);
}

function infoAsync(message, callBack) {
    var cb = new Action(callBack);
    host.InfoAsync(message, cb);
}

function info(message) {    
    host.Info(message);
}