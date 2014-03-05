var express = require("express");
var app = express();
app.get('/', function(request, response)
{
	response.end("Hello ! ");
});


app.listen(5555);


