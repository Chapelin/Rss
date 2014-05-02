var serv = require('node-static');

var file = new serv.Server("../");
console.log("Before");
require('http').createServer(function(request, response)
{
	request.addListener('end', function()
	{
		file.serve(request,response);
	}).resume();
}).listen(5554);

console.log("After");

