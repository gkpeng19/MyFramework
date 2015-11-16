#关于DataGrid表格插件API

##1、表格初始化：
		$("#tableid").datagrid({ url:"", psize:10, search:"", mutiple:false });
		url : 获取数据链接，需返回CommonResult<T>类型的JsonResult
		psize : 表格PageSize, 默认为10
		search : 查询项，可设置为"#divid","#formid",{ SItem:1, SItem2:"aa" ... }
		mutiple : 当表格首列为复选框时有效，默认值为true，表示可以多选
		**以上参数均为可选参数

	2、获取当前行数据：
		var json = $("#tableid").datagrid("getCurrData");

	3、添加新行，追加到最后
		$("#tableid").datagrid("addRow", json);

	4、插入行
		$("#tableid").datagrid("insertRow", index, json);
		index : 插入到的索引

	5、移除当前行
		$("#tableid").datagrid("removeCurrRow");

	6、移除行
		$("#tableid").datagrid("removeRow", index);
		index : 要移除的行的索引

	7、更新行
		$("#tableid").datagrid("updateRow", oldjson,newjson);
		oldjson : 旧的行数据
		newjson : 新的行数据

	8、获得复选框选中的行（集合）
		$("#tableid").datagrid("getSelecteds");

	9、刷新表格
		$("#tableid").datagrid("refresh");

	10、判断表格是否为空
		$("#tableid").datagrid("hasChildren");

	11、得到表格所有数据
		$("#tableid").datagrid("getAll");

	12、得到表格更新过的数据
		$("#tableid").datagrid("getUpdate");

	13、注册表格编辑事件
		$("#tableid").datagrid("onEdit",function(){ /*自定义代码*/ });

	14、注册表格删除事件
		$("#tableid").datagrid("onDelete",function(){ /*自定义代码*/ });

	15、注册表格导航到新页码事件
		$("#tableid").datagrid("onPager",function(newpage){ /*自定义代码*/ });
		newpage : 新的PageIndex

	16、注册表格初始化 TFoot 事件，用于实现自定义 TFoot
		$("#tableid").datagrid("onLoadFooter",function(footer){ /*自定义代码*/ });
		footer : 表格 TFoot 对象

	17、注册表格行选择发生变化事件
		$("#tableid").datagrid("onSelectChanged",function(json){ /*自定义代码*/ });
		json : 新选中的行数据


#关于Tree树插件API

##1、Tree初始化：
		$("#divid").gtree({ url: "", checkbox:false });
		url : 获取数据链接，需返回List<HtmlTreeNode>类型的JsonResult数据
		checkbox : 树节点前是否显示复选框
		**url 为必选参数；checkbox 默认为false，可选参数

	2、获得当前选中节点
		var node = $("#divid").gtree("getSelected");
	
	3、获得复选框勾选的节点（集合）, 参数checkbox=true时有效
		var nodes = $("#divid").gtree("getSelecteds");

	4、注册树节点选择变化事件
		$("#divid").gtree("selectChanged", function(node){ /*自定义代码*/ });
		node : 选中的节点

	5、新增根节点
		$("#divid").gtree("addRootNode", node);
		node : 根节点数据

	6、新增当前选中节点下的子节点
		$("#divid").gtree("addChildNode", node);
		node : 子节点数据
		
	7、移除选中的节点
		$("#divid").gtree("removeSelectNode");
		
	8、更新选中的节点
		$("#divid").gtree("updateSelectNode", newnode);
		newnode : 新的节点数据

	9、勾选某个节点，checkbox=true 时有效
		$("#divid").gtree("select", nodeid);
		nodeid : 节点id
		
	10、取消勾选某个节点，checkbox=true 时有效
		$("#divid").gtree("unSelect", nodeid);
		nodeid : 节点id
		
	11、取消勾选某个节点，checkbox=true 时有效
		$("#divid").gtree("allUnSelect");

	12、注册选中节点被移除前事件
		$("#divid").gtree("beforeDeleteNode", function(){ /*自定义代码*/  return true; });
		return true : 会执行移除；return false : 不会执行移除

#关于uploader上传插件API

##1、uploader初始化：
		$("#fileid").upload({ filter:"", uploadUrl:"", progressUrl:"", maxSize:10, success:function(result){} });
		filter : 可以上传的文件类型，image(图片), video(视频 .mp4 .flv), application(文档 .doc .docx .xls .xlsx .ppt .pptx")，必选参数
		uploadUrl : 接收文件链接，可选参数
		progressUrl : 获取进度链接，可选参数
		maxSize : 最大上传文件大小MB，系统默认为10MB，可选参数
		success : 上传成功后的回调函数，result 信息如下： 
			result.name : 新的文件名
			result.url : 文件链接
			result.path : 文件路径

#一些Jquery的扩展API

##1、绑定Json到Div/Form
		$("#divid").bindEntity(json);

	2、从Div/Form获取Json数据
		$("#divid").getContext();

	3、弹出框
		3.1、$.msg("msg") : 消息框没有确定按钮，并自动消失

		3.2、$.alert("msg") : 警告

		3.3、$.success("msg") : 成功提示

		3.4、$.error("msg") : 错误提示
		
		3.5、$.confirm("msg",function(){ /*点击确定时发生*/ }, function(){ /*点击取消时发生*/ }) : 选择提示框

		3.6、$("#divid").open(title, yes, no, close, cancle) : 弹出指定的层
			title : 弹出框标题
			yes : function(){ /*点击确定时发生*/ }
			no : function(){ /*点击取消时发生*/ }
			close : 默认为true，表示是否显示窗口右上方的关闭按钮
			cancle : 默认为true，表示是否显示取消按钮

		3.7、$.getUrlParam("pname") : 从url获取参数
				
		
		