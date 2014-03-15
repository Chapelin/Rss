var express = require("express");
var app = express();
var mongoose = require('mongoose');
mongoose.connect("mongodb://localhost/test");
var db = mongoose.connection;
var Grid = require("gridfs-stream");
var Schema = mongoose.Schema;
var numberList = 3;
var ObjectId = require('mongoose').Types.ObjectId; 
var cors = require("cors");

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
});

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

var Entree = mongoose.model("Entree",entreeSchema,"Entrees");
var Categorie = mongoose.model("Categorie",categorieSchema,"Categories");
var Source = mongoose.model("Source",sourceSchema,"Sources");

var entites = [];
entites["Entrees"] = Entree;
entites["Categories"] = Categorie;
entites["Sources"] = Source;

db.on('error', console.error.bind(console, 'connection error:'));

app.use(cors());
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
			HandleError(err,res);
		else
		{
			console.log("Result : "+entree);
			SetContentJson(res);
			res.end(JSON.stringify(entree));
		}
	});
});

app.get("/Entrees/GetLastX",function(req,res)
{
	Entree.find().sort("-DateInsertion").limit(numberList).exec(function(err,result)
	{
		if(err)
			HandleError(err,res);
		else
			SendList(res,result);
	})
});

app.get("/Entrees/GetBySourceIdAll/:id",function(req,res)
{
	var id;
	try
	{
		id = new ObjectId(req.params.id);
	}
	catch(e)
	{
		HandleError(err,res);
	}
	if(id)
	{
		Entree.find({SourceId : id}).exec(function(err,result)
		{
			if(err)
				console.log("Error : " +err);
			else
				SendList(res,result);
		})
	}
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


app.get("/Entrees/GetBySourceIdLastX/:id",function(req,res)
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
		Entree.find({SourceId : id}).sort("-DateInsertion").limit(numberList).exec(function(err,result)
		{
			if(err)
				HandleError(err,res);
			else
				SendList(res,result);
		})
	}
});



app.get("/Entrees/GetByCategoriesIdLastX/:id",function(req,res)
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
	if(id)
	{
		
		Source.find( {CategoriesIds : id}).exec(function(err,data)
		{
			if(err)
				HandleError(err,res)
			else
			{
				Entree.find().where('SourceId').in(data).sort("-DateInsertion").limit(numberList).exec(function(err,result)
				{
					if(err)
					{
						HandleError(err,res);
					}
					else
						SendList(res,result);
				});
			}
		});
		
	};
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


app.listen(5555);


function SetContentJson(res)
{
	res.set({'Content-Type' : 'application/json'});
}



function SendList(res,result)
{
	SetContentJson(res);
	result.forEach(function(chunk)
	{
		res.write(JSON.stringify(chunk));
	})
	res.end();
	
};



function SendOne(res,result)
{
	SetContentJson(res);
	res.write(JSON.stringify(result));
	res.end();
	
};



function HandleError(err,res)
{
	console.error("Erreur : "+err);
	res.end();
}