
function SetContentJson(res)
{
	res.set({'Content-Type' : 'application/json'});
}


exports.SetContentIco = function (res)
{
	res.set({'Content-Type' : 'image/vnd.microsoft.icon'});
}

exports.SetCors = function(res)
{
	res.header('Access-Control-Allow-Origin', "*")
	res.header('Access-Control-Allow-Methods',"GET, POST, OPTIONS");
	res.header("Access-Control-Allow-Headers","X-Requested-With");
}

exports.SendList = function(res,result)
{
	SetContentJson(res);
	this.SetCors(res);
	var total = "[";
	result.forEach(function(chunk)
	{
		total+=(JSON.stringify(chunk));
		total+=",";
	})
	var ind = total.lastIndexOf(",");
	if(ind!=-1)
	{
		total = total.substring(0,ind);
	};
	total+="]";
	res.write(total);
	
	res.end();
	
};


exports.SendOne = function (res,result)
{
	SetContentJson(res);
	this.SetCors(res);
	res.write(JSON.stringify(result));

	res.end();
	
};


exports.HandleError = function (err,res)
{
	console.error("Erreur : "+err);
	res.end();
}

