var express = require("express");
var app = express();
var mongoose = require('mongoose');
mongoose.connect("mongodb://localhost/test");
var db = mongoose.connection;

var numberList = 20;
var cors = require("cors");

var entreMod = require('./Entrees.js');
var categorieeMod = require('./Categories.js');
var favicMod = require("./Favicons.js");
var sourceMod = require("./Sources.js");


db.on('error', console.error.bind(console, 'connection error:'));

app.use(cors());

//Abonnements aux differents modules
entreMod(app, mongoose,numberList);
categorieeMod(app,mongoose);
favicMod(app,mongoose);
sourceMod.Register(app,mongoose);

app.options('*', function(req,res){
	res.header('Access-Control-Allow-Origin', '*');
	res.header('Access-Control-Allow-Methods', 'GET,PUT,POST,DELETE,OPTIONS');
	res.header('Access-Control-Allow-Headers', 'Content-Type, Authorization, Content-Length, X-Requested-With');
	res.send(200);
});

app.get('/', function(request, response)
{
	response.end("Hello ! ");
});




app.listen(5555);

