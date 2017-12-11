function parse(url) {
    if (!storage.add("news",{ title: url, content: url })) {
        return 0;
    }
    return 1;
}