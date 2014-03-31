var express = require("express");
var app = express();
var mongoose = require('mongoose');
mongoose.connect("mongodb://localhost/test");
var db = mongoose.connection;
var Grid = require("gridfs-stream");
var Schema = mongoose.Schema;
var numberList = 20;
var ObjectId = require('mongoose').Types.ObjectId; 
var cors = require("cors");

var entreMod = require('./Entrees.js');
var categorieeMod = require('./Categories.js');
var favicMod = require("./Favicons.js");


var temp = require("./Utils.js");
var SetContentJson = temp.SetContentJson;
var SendList = temp.SendList;
var SendOne = temp.SendOne;
var HandleError = temp.HandleError;
var SetContentIco = temp.SetContentIco;




var sourceSchema = new Schema(
{
	Id : Schema.Types.ObjectId,
	URL : String,
	Description : String,
	DateAjout : Date,
	Delai : Number,
	Favicon : Boolean,
	CategoriesIds : [String]
});




var Source = mongoose.model("Source",sourceSchema,"Sources");





db.on('error', console.error.bind(console, 'connection error:'));

app.use(cors());
entreMod(app, mongoose,numberList);
categorieeMod(app,mongoose);
favicMod(app,mongoose);
app.get('/', function(request, response)
{
	response.end("Hello ! ");
});


app.get("/Sources/GetCategorieofId/:id",function(req,res)
{
	var id;
	try
	{
		id = new ObjectId(req.params.id);
	}
	catch(err)
	{
		HandleError(err,res);
	}
	if(id)
	{
		Source.find({_id : id}).select("CategoriesIds").exec(function(err,result)
		{
			if(err)
				HandleError(err,res);
			else
			{
				SendList(res,result);
			}
		})
	}
});



app.get("/Sources/GetAll" ,function(req,res)
{
	Source.find().exec(function(err,result)
	{
		if(err)
			HandleError(err,res);
		else
			SendList(res,result);
	});
});


app.get("/Sources/GetByCategorieId/:id" ,function(req,res)
{
	var id;
	try
	{
		id = new ObjectId(req.params.id);
	}
	catch(err)
	{
		HandleError(err,res)
	}
	Source.find( {CategoriesIds : id}).exec(function(err,data)
	{
		if(err)
			HandleError(err,res);
		else
			SendList(res,result);
	});
})

app.get("/Sources/GetById/:id" ,function(req,res)
{
	var id;
	try
	{
		id = new ObjectId(req.params.id);
	}
	catch(err)
	{
		HandleError(err,res)
	}
	Source.findOne( {_id : id}).exec(function(err,data)
	{
		if(err)
			HandleError(err,res);
		else
			SendOne(res,data);
	});
})



app.listen(5555);

