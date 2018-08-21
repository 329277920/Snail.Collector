var unity = {
    toForm: function (obj) {
        var form = "";
        var idx = 0;
        for (var key in obj) {
            if (typeof (obj[key]) == "function") {
                continue;
            }
            var value = obj[key] == undefined ? "" : encodeURIComponent(obj[key]);
            form += ("{2}{0}={1}".format(key, value, idx++ > 0 ? "&" : ""));
        }
        return form;
    }
};