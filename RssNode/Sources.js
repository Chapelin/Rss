var Utils = require("./Utils.js");


module.exports = function(app, mongoose)
{
	var ObjectId = mongoose.Types.ObjectId;

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


	var Source = mongoose.model("Source",sourceSchema,"Sources");


	app.get("/Sources/GetCategorieofId/:id",function(req,res)
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
	});



	app.get("/Sources/GetAll" ,function(req,res)
	{
		Source.find().exec(function(err,result)
		{
			if(err)
				Utils.HandleError(err,res);
			else
				Utils.SendList(res,result);
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
			Utils.HandleError(err,res)
		}
		Source.find( {CategoriesIds : id}).exec(function(err,data)
		{
			if(err)
				Utils.HandleError(err,res);
			else
				Utils.SendList(res,result);
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
			Utils.HandleError(err,res)
		}
		Source.findOne( {_id : id}).exec(function(err,data)
		{
			if(err)
				Utils.HandleError(err,res);
			else
				Utils.SendOne(res,data);
		});
	})
}