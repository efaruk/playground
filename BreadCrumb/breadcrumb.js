var apiUrl = 'http://www.currentdomain.com/api/xhr';
var debugging = true;
var whiteList = [ 'google.com', 'raygun.io', 'elmah.io', 'airbrake.io', 'errorception.com' ];
var goldFinch = new Object();
goldFinch.tempOpen = XMLHttpRequest.prototype.open;
goldFinch.tempSend = XMLHttpRequest.prototype.send;
goldFinch.self = false;
goldFinch.callback = function () {
	try {
		if (CheckWhiteList(this.url)) return;
		var log = [this.method, this.url, this.data, getStackTrace()];
		if (debugging) alert(log);
		var xhr = new XMLHttpRequest();
		xhr.onreadystatechange = function () {
			// You should change here how you whish
			if (this.readyState == 4 ) { // When complete
				// Log to console
				console.log(this.status);
			}
		}
		this.self = true;
		xhr.open('post', apiUrl);
		xhr.send(log)
		this.self = false;
	}
	catch(err) {
		this.self = false;
		if (debugging) alert(err);
		console.error(err);
	}
	finally {
		this.self = false;
	}
}

XMLHttpRequest.prototype.open = function(a,b) {
  if (goldFinch.self) return;
  if (!a) var a='';
  if (!b) var b='';
  goldFinch.tempOpen.apply(this, arguments);
  goldFinch.method = a;  
  goldFinch.url = b;
  if (a.toLowerCase() == 'get' || a.toLowerCase() == 'head') {
    var data = b.split('?');
    goldFinch.data = data[1];
  }
}

XMLHttpRequest.prototype.send = function(a,b) {
  if (goldFinch.self) return;
  if (!a) var a='';
  if (!b) var b='';
  goldFinch.tempSend.apply(this, arguments);
  var method = goldFinch.method.toLowerCase();
  if(method != 'get' || method != 'head') {
	goldFinch.data = a;
  }
  goldFinch.callback();
}

function CheckWhiteList(url) {
	var white = false;
	var u = ExtractDomain(url)
	for (i = 0; i < whiteList.length; i++) {
		var item = whiteList[i];
		if (u.indexOf(item) !=-1) {
			white = true;
			break;
		}
	}
	return white;
}

function ExtractDomain(url) {
    var domain;
    //find & remove protocol
    if (url.indexOf("://") > -1) {
        domain = url.split('/')[2];
    }
    else {
        domain = url.split('/')[0];
    }
    //find & remove port number
    domain = domain.split(':')[0];
    return domain;
}

function getStackTrace() { 
  var e = new Error();
  var st = e.stack.replace('Error', '');
  return st;
}