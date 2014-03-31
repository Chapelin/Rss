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
var gfs = Grid(mongoose.connection.db, mongoose.mongo);
var entreMod = require('./Entrees.js');


var temp = require("./Utils.js");
var SetContentJson = temp.SetContentJson;
var SendList = temp.SendList;
var SendOne = temp.SendOne;
var HandleError = temp.HandleError;
var SetContentIco = temp.SetContentIco;


var categorieSchema = new Schema(
{
	Id : Schema.Types.ObjectId,
	Description : String
});

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



var faviconSchema = new Schema(
{
	Id : Schema.Types.ObjectId,
	SourceId : Schema.Types.ObjectId,
	GridFSId : Schema.Types.ObjectId
});


var Categorie = mongoose.model("Categorie",categorieSchema,"Categories");
var Source = mongoose.model("Source",sourceSchema,"Sources");
var Favicon = mongoose.model("Favicon",faviconSchema,"Favicon");




db.on('error', console.error.bind(console, 'connection error:'));

app.use(cors());
entreMod(app, mongoose,numberList);
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


app.get("/Categories/GetAll" ,function(req,res)
{
	Categorie.find().exec(function(err,result)
	{
		if(err)
			HandleError(err,res);
		else
			SendList(res,result);
	});
});

app.get("/Favicons/GetIcoBySource/:id" ,function(req,res)
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
	Favicon.findOne({SourceId : id}).exec(function(err,result)
	{

		if(err)
			HandleError(err,res);
		else
		{
			if(result)
			{
				var readstream = gfs.createReadStream({_id: result.GridFSId});
				SetContentIco(res);
				readstream.pipe(res);
			}
			else
			{
				console.log("Pas de favicon pour "+id);
				res.end();
			}

		}
	});
});

app.listen(5555);

