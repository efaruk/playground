var apiUrl = 'http://www.currentdomain.com/api/xhr';
var debugging = true;
var whiteList = [ 'google.com', 'raygun.io', 'elmah.io', 'airbrake.io', 'errorception.com' ];
var bird = new Object();
bird.tempOpen = XMLHttpRequest.prototype.open;
bird.tempSend = XMLHttpRequest.prototype.send;
bird.tempHeader = XMLHttpRequest.prototype.setRequestHeader;
bird.headers = [];
bird.self = false;
bird.callback = function () {
	try {
		if (CheckWhiteList(this.url)) return;
		if (!this.headers) { this.headers = []; }
		var header = '';
		for (var key in this.headers) {
		  if (key === 'length' || !this.headers.hasOwnProperty(key)) continue;
		  var value = this.headers[key];
		  header = header + key + ':' + value + ',';
		}
		var log = [this.method, this.url, this.data, getStackTrace(), header];
		if (debugging) alert(log);
		var xhr = new XMLHttpRequest();
		xhr.onreadystatechange = function () {
			// You should change here how you whish
			if (debugging) {
				// Log to console
				console.log(this.readyState);
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
  if (bird.self) return;
  if (!a) var a='';
  if (!b) var b='';
  bird.tempOpen.apply(this, arguments);
  bird.method = a;  
  bird.url = b;
  if (a.toLowerCase() == 'get' || a.toLowerCase() == 'head') {
    var data = b.split('?');
    bird.data = data[1];
  }
}

XMLHttpRequest.prototype.send = function(a,b) {
  if (bird.self) return;
  if (!a) var a='';
  if (!b) var b='';
  bird.tempSend.apply(this, arguments);
  var method = bird.method.toLowerCase();
  if(method != 'get' || method != 'head') {
	bird.data = a;
  }
  bird.callback();
}

XMLHttpRequest.prototype.setRequestHeader = function(a,b) {
  if (!a) var a='';
  if (!b) var b='';
  if(!this.headers) {
    this.headers = [];
  }
  bird.tempHeader.apply(this, arguments);
  if (!bird.self) {
	this.headers[a] = b;
	bird.headers = this.headers;
  }
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
