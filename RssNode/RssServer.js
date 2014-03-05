var express = require("express");
var app = express();
var mongoose = require('mongoose');
mongoose.connect("mongodb://localhost/test");
var db = mongoose.connection;
var Grid = require("gridfs-stream");
var Schema = mongoose.Schema;
var numberList = 20;


var entreeSchema = new Schema(
{
	Id : Schema.Types.ObjectId,
	Texte : String,
	Titre : String,
	Image : String,
	Link : String,
	UniqId : String,
	Date : Date,
	DateInsertion : Date,
	SourceId : Schema.Types.ObjectId
})

var categorieSchema = new Schema(
{
	Id : Schema.Types.ObjectId,
	Description : String
})

var sourceSchema = new Schema(
{
	Id : Schema.Types.ObjectId,
	URL : String,
	Description : String,
	DateAjout : Date,
	Delai : Number,
	Favicon : Boolean,
	CategoriesIds : [String]
})

var Entree = mongoose.model("Entree",entreeSchema,"Entrees");
var Categorie = mongoose.model("Categorie",categorieSchema,"Categories");
var Source = mongoose.model("Source",sourceSchema,"Sources");

var entites = [];
entites["Entrees"] = Entree;
entites["Categories"] = Categorie;
entites["Sources"] = Source;

db.on('error', console.error.bind(console, 'connection error:'));
app.get('/', function(request, response)
{
	response.end("Hello ! ");
});
app.get("/alive",function(req, res)
{
	var p = {};
	p.Nom = "toto";
	p.prenom = "tata";
	SetContentJson(res);
	res.end(JSON.stringify(p));

});
app.get('/one', function(req, res)
{
	Entree.findOne().exec(function(err,entree)
	{
		if(err)
			console.log("Error : " +err);
		else
		{
			console.log("Result : "+entree);
			SetContentJson(res);
			res.end(JSON.stringify(entree));
		}
	});
});

app.get("/:entite/GetLasts",function(req,res)
{
	console.log(req.params.entite);
	if(!entites[req.params.entite])
	{
		res.set(404);
		res.end();
		return;
	}
	var control = entites[req.params.entite];
	control.find().limit(numberList).exec(function(
		err,result)
	{

		if(err)
		{
			console.log(err);
		}
		else
		{
			SetContentJson(res);
			result.forEach(function(chunk)
			{
				console.log(JSON.stringify(chunk));
				res.write(JSON.stringify(chunk));
			});

			res.end();
		}
	});


});

app.listen(5555);


function SetContentJson(res)
{
	res.set({'Content-Type' : 'application/json'});
}

