var apiUrl = 'http://www.example.com/api';
var debugging = true;
var goldFinch = new Object();
goldFinch.tempOpen = XMLHttpRequest.prototype.open;
goldFinch.tempSend = XMLHttpRequest.prototype.send;
goldFinch.self = false;
goldFinch.callback = function () {
	try {
		this.self = true;
		var log = [this.method, this.url, this.data];
		if (debugging) alert(log);
		var xhr = new XMLHttpRequest();
		xhr.onreadystatechange = function () {
			// You should change here how you whish
			if (this.readyState == 4 ) {
				// When complete
				// Log to console
				console.log(this.status);
			}
		}
		xhr.open('post', apiUrl);
		xhr.send(log)
	}
	catch(err) {
		if (!debugging) alert(err);
	}
	finally {
		this.self = false;
	}
}

function bough() {
	if (this.readyState == 4 ) {
		console.log(this.status);
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