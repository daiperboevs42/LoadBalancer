const app = require('express')();  
const http = require('http');
var https = require('https');
var cors = require('cors');
const server = http.createServer(app);
const bodyParser = require('body-parser');
const fs = require('fs')
const filePath = './monitordata.txt'

  let previousServer = 0;
  let currentServer = 0;
  //Insert/remove links to Instances here
  let arrayOfServers = ['apiprime.azurewebsites.net','apiprime3.azurewebsites.net'];

  //Creates monitor file
try {
  if (fs.existsSync(filePath)) {
    //file exists
  }else{
	fs.open('monitordata.txt', 'w', function (err, file) {
		if (err) throw err;
		console.log('Saved!');
	  });
  }
} catch(err) {
  console.error(err)
}


  // Middleware
  app.use(bodyParser.json());
  app.use(cors({
	  origin: '*',
	  methods: ["GET", "POST"]
  }));

app.get('/prime/:id', function(req, response) {
	const option = createOption ('' , 'GET', '/api/primes/' + req.params.id);
	const requestHttp = https.request(option, res => {
		console.log(`statusCode: ${res.statusCode}`)
		res.setEncoding('utf8');
		res.on('data', d => {
			console.log(d);
			response.send(d);
		})
	})

	requestHttp.on('error', error => {
		console.error(error);
		response.send(error);
	})
	
	requestHttp.end()
	fs.appendFile(filePath, ' Response: ' + Date.now()+'\n\n'
	, function (err) {
		if (err) throw err;
		console.log('Updated!');
	  });
});

app.post('/prime', function (req, response) {
	let change = req.body;
	console.log(req.body)
	console.log(req.body.before, req.body.after)
	const data = JSON.stringify({
		start: Number(change.before),
		end: Number(change.after)
	});
		
	const option = createOption ( data, 'POST', '/api/primes/');

	const requestHttp = https.request(option, res => {
		console.log(`statusCode: ${res.statusCode}`)
		res.setEncoding('utf8');
		res.on('data', d => {
			console.log(d);
			response.send(d);
		})
	})

	requestHttp.on('error', error => {
		console.error(error);
		response.send(error)
	})
	
	requestHttp.write(data)
	requestHttp.end()
	fs.appendFile(filePath, ' Response: ' + Date.now() +'\n\n'
	, function (err) {
		if (err) throw err;
		console.log('Updated!');
	  });
})

function showSelectedServer(){
	if (currentServer >= arrayOfServers.length) currentServer = 0;
		return arrayOfServers[currentServer++];
	}

function selectServer() {
    if (previousServer >= arrayOfServers.length) previousServer = 0;
		return arrayOfServers[previousServer++];
	} 
  
    function createOption(data, type, path){

		fs.appendFile(filePath, 
			'Server Instance: ' + showSelectedServer() + '\n Path: ' + path + '\n Timestamp: ' + Date.now() + '\n'
			, function (err) {
			if (err) throw err;
			console.log('Updated!');
		  });

		if(type === 'GET'){
			return options = {
			  hostname: selectServer(),
			  port: 443,
			  path: path,
			  method: type,
				  headers: {
					'Content-Type': 'application/json'
				  }
			}
		}
		return options = {
		  hostname: selectServer(),
		  port: 443,
		  path: path,
		  method: type,
		  headers: {
			'Content-Type': 'application/json',
			'Content-Length': data.length
		  }
		}
	} 

server.listen(4444, () => {
  console.log('Listening on port 4444');
});