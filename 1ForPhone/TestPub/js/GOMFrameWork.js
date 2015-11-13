function toJsResult(json) {
    var data = null;
    if (json.length != undefined) {
        if (json.length == 0 || json[0].Collection == undefined) {
            return json;
        }

        data = [];
        for (var i = 0; i < json.length; ++i) {
            var d = {};
            for (var item in json[i]) {
                if (typeof (json[i][item]) != "object") {
                    d[item] = json[i][item];
                }
            }
            for (var item in json[i].Collection) {
                d[item] = json[i].Collection[item].Value;
            }
            data[i] = d;
        }
    }
    else {
        if (json.Collection == undefined) {
            return json;
        }

        data = {};
        for (var item in json) {
            if (typeof (json[item]) != "object") {
                data[item] = json[item];
            }
        }
        for (var item in json.Collection) {
            data[item] = json.Collection[item].Value;
        }
    }

    return data;
}

function getUrl(url) {
	if(!url){
		return "";
	}
	if(url.indexOf('/')==0){ 
        return "http://123.57.153.47" + url;
        //return "http://localhost:55611"+url;
        //return "http://192.168.1.101:8073"+url;
    }
    return "http://123.57.153.47/" + url;
}

function initImages(containerId){
	var width=document.getElementById(containerId).clientWidth;
	var imgs= document.getElementById(containerId).getElementsByClassName("uimg");
	   for(var i=0;i<imgs.length;++i){
	    var src=imgs[i].getAttribute("_src");
	    if(src&&src.length>0){
	    	imgs[i].src=src+"?size="+width;
	    }
	}
}

(function($,doc){
	$.plusReady(function(){
		var tel=doc.getElementById("tel");
		tel.addEventListener("tap",function(){
			plus.device.dial("13126704233");
		},false);
	});
}(mui,document));
