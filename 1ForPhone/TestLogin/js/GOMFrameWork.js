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

var _pageParameters = { init: false };
function getUrlParam(pname) {
    if (_pageParameters.init) {
        return _pageParameters[pname];
    }

    var hrefs = location.href.split('?');
    if (hrefs.length > 1) {
        var params = hrefs[1].split('&');
        for (var i=0;i<params.length;++i) {
        	var strs = params[i].split('=');
            if (strs.length > 1) {
                _pageParameters[strs[0]] = strs[1];
            }
    	}
    }

    _pageParameters.init = true;

    return _pageParameters[pname];
}

/*自定义方法*/

function getUrl(url) {
	if(!url){
		return "";
	}
	if(url.indexOf('/')>=0){
        //return "http://123.57.153.47" + url;
        return "http://10.2.8.184:8073"+url;
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
