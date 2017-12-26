function run() {       
     
    // 配置存储
	storage.config({ provider:"mysql",connectionString:"server=localhost;database=1yyg;uid=root;pwd=chennanfei;" });    
 
    var table = "news";
 
    // 添加单条记录
    console.writeLine(storage.add(table,{ title:"new1",content:"content1" }));
    
    // 添加多条记录
    console.writeLine(storage.add(table,
    { title:"new2",content:"content2" },
    { title:"new2",content:"content3" }));
    
    // 查询单条记录
    var item = storage.single(table,{ title:"new1" });
    console.writeLine(item.id);
   
    
    // 查询多条记录
    item = storage.select(table,{ title:"new2" }).each(function(data){
    	console.writeLine(data.id);
    	data.title = "xxx100";
    });     
    
    // 更新多条
    console.writeLine("更新多条：" + storage.update(table,{ id:{"$p" : "id"}}, item));
    
    // 查询多条记录
    storage.select(table,{ title:"xxx100" }).each(function(data){
    	console.writeLine(data.id);    	 
    });     
    
    // 查询所有
    item = storage.select(table,{ id:{ "$gte":0 } });
    console.writeLine("数据条数:" + item.length);
    
    // 删除所有
    console.writeLine("删除所有:" + storage.delete(table, { id:{"$p" : "id"}}, item));

    // 返回成功
    return 1;
}