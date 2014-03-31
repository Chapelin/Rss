var Utils = require("./Utils.js");

module.exports = function(app, mongoose)
{

	var categorieSchema = new mongoose.Schema(
	{
		Id : mongoose.Schema.Types.ObjectId,
		Description : String
	});

	var Categorie = mongoose.model("Categorie",categorieSchema,"Categories");

	app.get("/Categories/GetAll" ,function(req,res)
	{
		Categorie.find().exec(function(err,result)
		{
			if(err)
				Utils.HandleError(err,res);
			else
				Utils.SendList(res,result);
		});
	});
}