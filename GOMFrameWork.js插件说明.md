#����DataGrid�����API

##1������ʼ����
		$("#tableid").datagrid({ url:"", psize:10, search:"", mutiple:false });
		url : ��ȡ�������ӣ��践��CommonResult<T>���͵�JsonResult
		psize : ���PageSize, Ĭ��Ϊ10
		search : ��ѯ�������Ϊ"#divid","#formid",{ SItem:1, SItem2:"aa" ... }
		mutiple : ���������Ϊ��ѡ��ʱ��Ч��Ĭ��ֵΪtrue����ʾ���Զ�ѡ
		**���ϲ�����Ϊ��ѡ����

	2����ȡ��ǰ�����ݣ�
		var json = $("#tableid").datagrid("getCurrData");

	3��������У�׷�ӵ����
		$("#tableid").datagrid("addRow", json);

	4��������
		$("#tableid").datagrid("insertRow", index, json);
		index : ���뵽������

	5���Ƴ���ǰ��
		$("#tableid").datagrid("removeCurrRow");

	6���Ƴ���
		$("#tableid").datagrid("removeRow", index);
		index : Ҫ�Ƴ����е�����

	7��������
		$("#tableid").datagrid("updateRow", oldjson,newjson);
		oldjson : �ɵ�������
		newjson : �µ�������

	8����ø�ѡ��ѡ�е��У����ϣ�
		$("#tableid").datagrid("getSelecteds");

	9��ˢ�±��
		$("#tableid").datagrid("refresh");

	10���жϱ���Ƿ�Ϊ��
		$("#tableid").datagrid("hasChildren");

	11���õ������������
		$("#tableid").datagrid("getAll");

	12���õ������¹�������
		$("#tableid").datagrid("getUpdate");

	13��ע����༭�¼�
		$("#tableid").datagrid("onEdit",function(){ /*�Զ������*/ });

	14��ע����ɾ���¼�
		$("#tableid").datagrid("onDelete",function(){ /*�Զ������*/ });

	15��ע���񵼺�����ҳ���¼�
		$("#tableid").datagrid("onPager",function(newpage){ /*�Զ������*/ });
		newpage : �µ�PageIndex

	16��ע�����ʼ�� TFoot �¼�������ʵ���Զ��� TFoot
		$("#tableid").datagrid("onLoadFooter",function(footer){ /*�Զ������*/ });
		footer : ��� TFoot ����

	17��ע������ѡ�����仯�¼�
		$("#tableid").datagrid("onSelectChanged",function(json){ /*�Զ������*/ });
		json : ��ѡ�е�������


#����Tree�����API

##1��Tree��ʼ����
		$("#divid").gtree({ url: "", checkbox:false });
		url : ��ȡ�������ӣ��践��List<HtmlTreeNode>���͵�JsonResult����
		checkbox : ���ڵ�ǰ�Ƿ���ʾ��ѡ��
		**url Ϊ��ѡ������checkbox Ĭ��Ϊfalse����ѡ����

	2����õ�ǰѡ�нڵ�
		var node = $("#divid").gtree("getSelected");
	
	3����ø�ѡ��ѡ�Ľڵ㣨���ϣ�, ����checkbox=trueʱ��Ч
		var nodes = $("#divid").gtree("getSelecteds");

	4��ע�����ڵ�ѡ��仯�¼�
		$("#divid").gtree("selectChanged", function(node){ /*�Զ������*/ });
		node : ѡ�еĽڵ�

	5���������ڵ�
		$("#divid").gtree("addRootNode", node);
		node : ���ڵ�����

	6��������ǰѡ�нڵ��µ��ӽڵ�
		$("#divid").gtree("addChildNode", node);
		node : �ӽڵ�����
		
	7���Ƴ�ѡ�еĽڵ�
		$("#divid").gtree("removeSelectNode");
		
	8������ѡ�еĽڵ�
		$("#divid").gtree("updateSelectNode", newnode);
		newnode : �µĽڵ�����

	9����ѡĳ���ڵ㣬checkbox=true ʱ��Ч
		$("#divid").gtree("select", nodeid);
		nodeid : �ڵ�id
		
	10��ȡ����ѡĳ���ڵ㣬checkbox=true ʱ��Ч
		$("#divid").gtree("unSelect", nodeid);
		nodeid : �ڵ�id
		
	11��ȡ����ѡĳ���ڵ㣬checkbox=true ʱ��Ч
		$("#divid").gtree("allUnSelect");

	12��ע��ѡ�нڵ㱻�Ƴ�ǰ�¼�
		$("#divid").gtree("beforeDeleteNode", function(){ /*�Զ������*/  return true; });
		return true : ��ִ���Ƴ���return false : ����ִ���Ƴ�

#����uploader�ϴ����API

##1��uploader��ʼ����
		$("#fileid").upload({ filter:"", uploadUrl:"", progressUrl:"", maxSize:10, success:function(result){} });
		filter : �����ϴ����ļ����ͣ�image(ͼƬ), video(��Ƶ .mp4 .flv), application(�ĵ� .doc .docx .xls .xlsx .ppt .pptx")����ѡ����
		uploadUrl : �����ļ����ӣ���ѡ����
		progressUrl : ��ȡ�������ӣ���ѡ����
		maxSize : ����ϴ��ļ���СMB��ϵͳĬ��Ϊ10MB����ѡ����
		success : �ϴ��ɹ���Ļص�������result ��Ϣ���£� 
			result.name : �µ��ļ���
			result.url : �ļ�����
			result.path : �ļ�·��

#һЩJquery����չAPI

##1����Json��Div/Form
		$("#divid").bindEntity(json);

	2����Div/Form��ȡJson����
		$("#divid").getContext();

	3��������
		3.1��$.msg("msg") : ��Ϣ��û��ȷ����ť�����Զ���ʧ

		3.2��$.alert("msg") : ����

		3.3��$.success("msg") : �ɹ���ʾ

		3.4��$.error("msg") : ������ʾ
		
		3.5��$.confirm("msg",function(){ /*���ȷ��ʱ����*/ }, function(){ /*���ȡ��ʱ����*/ }) : ѡ����ʾ��

		3.6��$("#divid").open(title, yes, no, close, cancle) : ����ָ���Ĳ�
			title : ���������
			yes : function(){ /*���ȷ��ʱ����*/ }
			no : function(){ /*���ȡ��ʱ����*/ }
			close : Ĭ��Ϊtrue����ʾ�Ƿ���ʾ�������Ϸ��Ĺرհ�ť
			cancle : Ĭ��Ϊtrue����ʾ�Ƿ���ʾȡ����ť

		3.7��$.getUrlParam("pname") : ��url��ȡ����
				
		
		