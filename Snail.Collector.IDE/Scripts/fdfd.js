function run() {
   var item = { name:"陈南飞", age:20, time: "fdfs" };
   
   console.writeLine(item.toString());
   
   console.writeLine(item.toForm()); 
   
   http.postForm("http://www.qq.com",{ name:"cnf",age:12 });
}