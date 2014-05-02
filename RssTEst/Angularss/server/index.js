var serv = require('node-static');





var file = new serv.Server("../",{ cache: 3600 });


require('http').createServer(function(request, response)
{
	request.addListener('end', function()
	{
		file.serve(request,response);
	}).resume();
}).listen(5554);

