var Utils = require("./Utils.js");
var Grid = require("gridfs-stream");

module.exports = function(app, mongoose)
{
	var ObjectId = mongoose.Types.ObjectId;
	var gfs = Grid(mongoose.connection.db, mongoose.mongo);
	var faviconSchema = new mongoose.Schema(
	{
		Id : mongoose.Schema.Types.ObjectId,
		SourceId : mongoose.Schema.Types.ObjectId,
		GridFSId : mongoose.Schema.Types.ObjectId
	});

	var Favicon = mongoose.model("Favicon",faviconSchema,"Favicon");



app.get("/Favicons/GetIcoBySource/:id" ,function(req,res)
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
	Favicon.findOne({SourceId : id}).exec(function(err,result)
	{

		if(err)
			Utils.HandleError(err,res);
		else
		{
			if(result)
			{
				res.header('Access-Control-Allow-Origin', "*")
				var readstream = gfs.createReadStream({_id: result.GridFSId});
				Utils.SetContentIco(res);
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

}