function run() {

    source.importFile(1,"c:\\账户.txt");
    
    for(var i = 0;i<=500; i++)
    {
        var item = source.next(1);

        console.writeLine(item.userName);
    }       
     console.writeLine("OK");
   
    return 1;
}