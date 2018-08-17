try {
    var str = null;
    str.substring(1, 1);   
    return 1;
} catch (err) {
    debug.writeLine(err.message);
    return 0;
}
