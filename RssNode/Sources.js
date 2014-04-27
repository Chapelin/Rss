var Utils = require("./Utils.js");
var mongoose = require('mongoose');


var sourceSchema = new mongoose.Schema(
{
	Id : mongoose.Schema.Types.ObjectId,
	URL : String,
	Description : String,
	DateAjout : Date,
	Delai : Number,
	Favicon : Boolean,
	CategoriesIds : [String]
});
var Source =  mongoose.model("Source",sourceSchema,"Sources");
exports.Source = Source;



exports.Register = function(app, mongoose)
{
	var ObjectId = mongoose.Types.ObjectId;

	

	this.GetCategorieofId = function(req,res)
	{
		var id;
		try
		{
			id = new ObjectId(req.params.id);
		}
		catch(err)
		{
			Utils.HandleError(err,res);
		}
		if(id)
		{
			Source.find({_id : id}).select("CategoriesIds").exec(function(err,result)
			{
				if(err)
					Utils.HandleError(err,res);
				else
				{
					Utils.SendList(res,result);
				}
			})
		}
	};

	this.GetAll = function(req,res)
	{
		Source.find().exec(function(err,result)
		{
			if(err)
				Utils.HandleError(err,res);
			else
				Utils.SendList(res,result);
		});
	};

	this.GetByCategorieId = function(req,res)
	{
		var id;
		try
		{
			id = new ObjectId(req.params.id);
		}
		catch(err)
		{
			Utils.HandleError(err,res)
		}
		Source.find( {CategoriesIds : id}).exec(function(err,data)
		{
			if(err)
				Utils.HandleError(err,res);
			else
				Utils.SendList(res,result);
		});
	};

	this.GetById = function(req,res)
	{
		var id;
		try
		{
			id = new ObjectId(req.params.id);
		}
		catch(err)
		{
			Utils.HandleError(err,res)
		}
		Source.findOne( {_id : id}).exec(function(err,data)
		{
			if(err)
				Utils.HandleError(err,res);
			else
				Utils.SendOne(res,data);
		});
	};

	app.get("/Sources/GetCategorieofId/:id",this.GetCategorieofId);
	app.get("//Sources/GetCategorieofId/:id",this.GetCategorieofId);


	app.get("/Sources/GetAll" ,this.GetAll);
	app.get("//Sources/GetAll" ,this.GetAll);

	app.get("/Sources/GetByCategorieId/:id" ,this.GetByCategorieId)
	app.get("//Sources/GetByCategorieId/:id" ,this.GetByCategorieId)

	app.get("/Sources/GetById/:id" ,this.GetById)
	app.get("//Sources/GetById/:id" ,this.GetById)
}