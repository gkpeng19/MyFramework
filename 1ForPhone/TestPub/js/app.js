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
		
		mui.getJSON(getUrl("/Phone/Register"),{uname:regInfo.account,psw:regInfo.password,phone:regInfo.phone,yzm:regInfo.yzm,ran:Math.random()},function(r){
			r=r.ResultID;
			if(r==1){
				//var users = [];
				//regInfo.UID=r.ResultID;
				//users.push(regInfo);
				//localStorage.setItem('$users', JSON.stringify(users));
		 		callback();
			}
			else{
				var error=null;
				if(r==-1){
					error="用户名已存在！";
				}
				else if (r == -3) {
                    error="请获取短信验证码后重新输入！";
                }
                else if (r == -4) {
                    error="短信验证码输入错误！";
                }
                else {
                    error="注册失败，请重试！";
                }
                
                callback(error);
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
	 * 重置密码
	 **/
	owner.forgetPassword = function(phone,psw,yzm, callback) {
		$.getJSON(getUrl("/Phone/FindPsw"), { phone: phone, psw: psw, msgcode: yzm, ran: Math.random() }, function (r) {
            var r=r.r;
            if (r == 1) {
             	callback(1,"密码重置成功，请登录。");
            }
            else if (r == -1) {
              	callback(0,"请输入手机号码！");
            }
            else if (r == -2) {
                callback(0,"请获取短信验证码后重新输入！");
            }
            else if (r == -3) {
                callback(0,"短信验证码输入错误！");
            }
            else if (r == -4) {
                callback(0,"手机号码未注册！");
            }
            else if (r == 0) {
                callback(0,"重置密码失败，请重试！");
            }
        });
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
	
	owner.sendMessageCode=function(phone,type){
		if (phone.length == 0) {
			plus.nativeUI.toast("手机号码不能为空！");
            return;
        }
        if (!(/^[1][3,5,8][0-9]{9}$/g).test(phone)) {
        	plus.nativeUI.toast("请输入有效的手机号码！");
            return;
        }
        mui.getJSON(getUrl("/Phone/SendMsgCode"), { phone: phone, type: type, ran: Math.random() }, function (r) {
        	r=r.r;
            if (r == -1) {
            	plus.nativeUI.toast("手机号码输入错误！");
                return;
            }

            if (r == 9) {
            	plus.nativeUI.toast("该手机号码已被注册！");
            }
            else if (r == 1) {
                plus.nativeUI.toast("短信验证码已发送成功。");
            }
            else if (r == 0) {
            	plus.nativeUI.toast("短信验证码发送失败，请重试！");
            }
        });
	}
	
	owner.checkUpdate=function(callback){
		var w= plus.nativeUI.showWaiting("检测更新中...");
		plus.runtime.getProperty(plus.runtime.appid,function(inf){
        	wgtVer=inf.version;
        	mui.getJSON("/Phone/CheckUpdate",{pversion:wgtVer,ran:Math.random()},function(r){
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