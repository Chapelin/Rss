var express = require('express')
  , cors = require('cors')
  , app = express();

app.use(cors());

app.get('/js', function(req, res, next){
  res.json({msg: 'This is CORS-enabled for all origins!'});
});
app.get('//js', function(req, res, next){
  res.json({msg: 'This is CORS-enabled for all origins!'});
});


app.listen(5555, function(){
  console.log('CORS-enabled web server listening on port 5555');
});