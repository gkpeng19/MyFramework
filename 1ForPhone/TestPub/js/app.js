/**
 * 演示程序当前的 “注册/登录” 等操作，是基于 “本地存储” 完成的
 * 当您要参考这个演示程序进行相关 app 的开发时，
 * 请注意将相关方法调整成 “基于服务端Service” 的实现。
 **/
(function($, owner) {
	/**
	 * 用户登录
	 **/
	owner.login = function(loginInfo, callback) {
		callback = callback || $.noop;
		loginInfo = loginInfo || {};
		loginInfo.account = loginInfo.account || '';
		loginInfo.password = loginInfo.password || '';
		if (loginInfo.account.length < 5) {
			return callback('账号最短为 5 个字符');
		}
		if (loginInfo.password.length < 6) {
			return callback('密码最短为 6 个字符');
		}
		
		mui.post(getUrl("/Phone/Login"),{uname:loginInfo.account,psw:loginInfo.password,ran:Math.random()},function(r){
			if(!r||r==0){
				return callback('用户名或密码错误');
			}
			else{
				return owner.createState(r, loginInfo.account, callback);
			}
		});
	};

	owner.createState = function(uid, name, callback) {
		var state = owner.getState();
		state.uid=uid;
		state.account = name;
		state.token = "token123456789";
		owner.setState(state);
		return callback();
	};

	/**
	 * 新用户注册
	 **/
	owner.reg = function(regInfo, callback) {
		callback = callback || $.noop;
		regInfo = regInfo || {};
		regInfo.account = regInfo.account || '';
		regInfo.password = regInfo.password || '';
		if (regInfo.account.length < 5) {
			return callback('用户名最短需要 5 个字符');
		}
		if (regInfo.password.length < 6) {
			return callback('密码最短需要 6 个字符');
		}
		if (!checkEmail(regInfo.email)) {
			return callback('邮箱地址不合法');
		}
		
		mui.post(getUrl("/Phone/Register"),{uname:regInfo.account,psw:regInfo.password,email:regInfo.email,ran:Math.random()},function(r){
			if(r.ResultID==0){
				callback(r.Error);
			}
			else{
				//var users = [];
				//regInfo.UID=r.ResultID;
				//users.push(regInfo);
				//localStorage.setItem('$users', JSON.stringify(users));
		 		callback();
			}
		});
	};

	/**
	 * 获取当前状态
	 **/
	owner.getState = function() {
		var stateText = localStorage.getItem('$state') || "{}";
		return JSON.parse(stateText);
	};

	/**
	 * 设置当前状态
	 **/
	owner.setState = function(state) {
		state = state || {};
		localStorage.setItem('$state', JSON.stringify(state));
		//var settings = owner.getSettings();
		//settings.gestures = '';
		//owner.setSettings(settings);
	};

	var checkEmail = function(email) {
		email = email || '';
		return (email.length > 3 && email.indexOf('@') > -1);
	};

	/**
	 * 找回密码
	 **/
	owner.forgetPassword = function(email, callback) {
		callback = callback || $.noop;
		if (!checkEmail(email)) {
			return callback('邮箱地址不合法');
		}
		return callback(null, '新的随机密码已经发送到您的邮箱，请查收邮件。');
	};

	/**
	 * 获取应用本地配置
	 **/
	owner.setSettings = function(settings) {
		settings = settings || {};
		localStorage.setItem('$settings', JSON.stringify(settings));
	}

	/**
	 * 设置应用本地配置
	 **/
	owner.getSettings = function() {
		var settingsText = localStorage.getItem('$settings') || "{}";
		return JSON.parse(settingsText);
	}
	
	owner.checkUpdate=function(callback){
		var w= plus.nativeUI.showWaiting("检测更新中...");
		plus.runtime.getProperty(plus.runtime.appid,function(inf){
        	wgtVer=inf.version;
        	$.getJSON("/Phone/CheckUpdate",{pversion:wgtVer,ran:Math.random()},function(r){
        		w.close();
        		if(r.updated==1){
        			plus.nativeUI.toast('检测到新版本，正在下载');
        			owner.installUpdate(r.path);
        		}
        		else{
        			plus.nativeUI.toast("无新版本可更新");
        		}
        	});
    	});
	}
	
	owner.installUpdate=function(path){
		if(mui.os.android){
			var dtask = plus.downloader.createDownload( url, {}, function ( d, status ) {
    			if ( status == 200 ) { // 下载成功
    				plus.runtime.install(d.filename);
    			} else {//下载失败
    				plus.nativeUI.toast("新版本下载失败");
    			}
			});
			dtask.start(); 
		}
		else if(mui.os.ios){
			// HelloH5应用在appstore的地址
			var url='itms-apps://itunes.apple.com/cn/app/hello-h5+/id682211190?l=zh&mt=8';
			plus.runtime.openURL(url);
		}
	}
	
	owner.autoUpdate=function(){
		plus.runtime.getProperty(plus.runtime.appid,function(inf){
        	wgtVer=inf.version;
        	$.getJSON(getUrl("/Phone/CheckUpdate"),{pversion:wgtVer,ran:Math.random()},function(r){
        		if(r.updated==1){
        			plus.nativeUI.confirm("检测到新版本，是否更新？",function(e){
        				if(e.index==0){
        					if(mui.os.android){
								var dtask = plus.downloader.createDownload( r.path, {}, function ( d, status ) {
					    			if ( status == 200 ) { // 下载成功
					    				plus.runtime.install(d.filename);
					    			}
								});
								dtask.start(); 
							}
							else if(mui.os.ios){
								// HelloH5应用在appstore的地址
								var url='itms-apps://itunes.apple.com/cn/app/hello-h5+/id682211190?l=zh&mt=8';
								plus.runtime.openURL(url);
							}
        				}
        			},"自动更新提示",['确定','取消']);
        		}
        	});
        });
	}
}(mui, window.app = {}));