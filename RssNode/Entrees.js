var Utils = require("./Utils.js");


module.exports = function(app, mongoose, numberList)
{
	var ObjectId = mongoose.Types.ObjectId;
	var entreeSchema = new mongoose.Schema(
	{
		Id : mongoose.Schema.Types.ObjectId,
		Texte : String,
		Titre : String,
		Image : String,
		Link : String,
		UniqId : String,
		Date : Date,
		DateInsertion : Date,
		SourceId : mongoose.Schema.Types.ObjectId
	});


	var Entree = mongoose.model("Entree",entreeSchema,"Entrees");


	app.get("/Entrees/GetLastX",function(req,res)
	{
		Entree.find().sort("-Date").limit(numberList).exec(function(err,result)
		{
			if(err)
				Utils.HandleError(err,res);
			else
				Utils.SendList(res,result);
		})
	});

	app.get("/Entrees/GetBySourceIdAll/:id",function(req,res)
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
			Entree.find({SourceId : id}).exec(function(err,result)
			{
				if(err)
					Utils.HandleError(err,res);
				else
					Utils.SendList(res,result);
			})
		}
	});



	app.get("/Entrees/GetByCatIdLastX/:id",function(req,res)
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
			Source.find({CategoriesIds : id}, { _id : 1}).exec(function(err,result)
			{
				if(err)
					console.log("Error : " +err);
				else
				{
					var listId= [];
					result.forEach(function(da)
					{
						listId.push(da._id);
					})
					console.log("Liste des id pour cette categorie : "+listId);
					Entree.find({SourceId : listId}).sort("-Date").limit(numberList).exec(function(err,result)
					{
						if(err)
							Utils.HandleError(err,res);
						else
							Utils.SendList(res,result);
					});
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
			Utils.HandleError(err,res);
		}
		if(id)
		{
			Entree.find({SourceId : id}).sort("-DateInsertion").limit(numberList).exec(function(err,result)
			{
				if(err)
					Utils.HandleError(err,res);
				else
					Utils.SendList(res,result);
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
			Utils.HandleError(err,res)
		}
		if(id)
		{

			Source.find( {CategoriesIds : id}).exec(function(err,data)
			{
				if(err)
					Utils.HandleError(err,res)
				else
				{
					Entree.find().where('SourceId').in(data).sort("-Date").limit(numberList).exec(function(err,result)
					{
						if(err)
						{
							Utils.HandleError(err,res);
						}
						else
							Utils.SendList(res,result);
					});
				}
			});

		};
	});

}