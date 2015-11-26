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
        //return "http://123.57.153.47" + url;
        return "http://192.168.1.108:8073"+url;
        //return "http://10.2.8.35:8073"+url;
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

function openQQ() {
	if (plus.os.name == "Android") {
		var main = plus.android.runtimeMainActivity();
		var Intent = plus.android.importClass('android.content.Intent');
		var Uri = plus.android.importClass('android.net.Uri');
		var intent = new Intent(Intent.ACTION_VIEW, Uri.parse("mqqwpa://im/chat?chat_type=wpa&uin=1002934864"));
		main.startActivity(intent);
	}
	
	if (plus.os.name == "iOS") {
		plus.runtime.launchApplication({
			action: "mqq://im/chat?chat_type=wpa&uin=1002934864&version=1&src_type=web"
		}, function(e) {
			plus.nativeUI.confirm("检查到您未安装qq，请先到appstore搜索下载？", function(i) {
				if (i.index == 0) {
					iosAppstore("itunes.apple.com/cn/app/mqq/");
				}
			});
		});
	}
}


function initBottomBarFunc(doc){
	var msg=doc.getElementById("msg");
	if(msg){
		msg.addEventListener("tap",function(){
			if(!app.getState().uid){
				mui.openWindow({
					id: 'login',
					url:'login.html',
					waiting: {
						autoShow: false
					}
				});
			}
			else{
				if(plus.webview.currentWebview().id!="askList"){
					mui.openWindow({
						url:'askList.html',
						id:'askList',
						waiting: {
							autoShow: false
						}
					});
				}
			}
		},false);
	}
	
	var tel=doc.getElementById("tel");
	if(tel){
		tel.addEventListener("tap",function(){
			plus.device.dial("13126704233");
		},false);
	}

	var qq=doc.getElementById("qq");
	if(qq){
		qq.addEventListener("tap",function(){
			openQQ();
		},false);
	}
}


