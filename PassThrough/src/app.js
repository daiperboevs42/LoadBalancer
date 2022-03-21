const app = require('express')();  
const http = require('http');
const io = require('socket.io')(http);
var https = require('https');
var cors = require('cors');
const server = http.createServer(app);
const bodyParser = require('body-parser');

  let previousServer = 0;
  let arrayOfServers = ['apiprime.azurewebsites.net','apiprime3.azurewebsites.net'];


  // Middleware
  app.use(bodyParser.json());
  app.use(cors({
	  origin: '*',
	  methods: ["GET", "POST"]
  }));

app.get('/prime/:id', function(req, response) {
	console.log("PeepeePoopoo")
	const option = createOption ('' , 'GET', '/api/primes/' + req.params.id);
	const requestHttp = https.request(option, res => {
		console.log(`statusCode: ${res.statusCode}`)
		res.setEncoding('utf8');
		res.on('data', d => {
			console.log(d);
			response.send(d);
			//socket.emit("isPrime", d);
		})
	})

	requestHttp.on('error', error => {
		console.error(error);
		response.send(error);
		//socket.emit("isPrime", 'Error');
	})

	requestHttp.end()
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
			//socket.emit("countPrime", d);
		})
	})

	requestHttp.on('error', error => {
		console.error(error);
		response.send(error)
		//socket.emit("countPrime", 'Error');
	})

	requestHttp.write(data)
	requestHttp.end()
})


function selectServer() {
    if (previousServer >= arrayOfServers.length) previousServer = 0;
		return arrayOfServers[previousServer++];
	} 
  
    function createOption(data, type, path){
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