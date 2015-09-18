$("#cbx_hiddenForm").bind('click', function () {
    if (this.checked) {
        $("#form_tr_btns").hide();
        $("#form_btns input").remove();
        $("#form_btns label").remove();
    }
    else {
        $("#form_tr_btns").show();
    }
});

function ttAddCustomBtn() {
    $("#table_addcustombtn").open("新增自定义按钮", function () {
        var text = $.trim($("#table_cbtntext").val());
        var action = $.trim($("#table_cbtnaction").val());
        if (text.length == 0 || action.length == 0) {
            alert("请维护所有项！");
            return false;
        }

        $("#table_cbtntext").val("");
        $("#table_cbtnaction").val("");
        var bid = Math.random().toString().split('.')[1];
        $("#tt_custombtntd").append('<input type="checkbox" id="' + bid + '" checked="checked" data-text="' + text + '" data-action="' + action + '" /> <label for="' + bid + '">' + text + '</label>');
    });
}

var temptrees = {};
function AddTree() {
    $("#tree_addTree").open("添加树配置", function () {
        var nodeField = $.trim($("#tree_nodeField").val());
        var parentIdField = $.trim($("#tree_parentIdField").val());
        if (nodeField.length == 0) {
            alert("请维护节点名称对应字段！");
            return false;
        }

        if (parentIdField.length == 0) {
            alert("请维护上级节点ID对应字段！");
            return false;
        }

        var loadUrl = $.trim($("#tree_loadUrl").val());
        var saveUrl = $.trim($("#tree_saveUrl").val());
        var delUrl = $.trim($("#tree_delUrl").val());
        if (loadUrl.length == 0) {
            alert("请维护加载Url！");
            return false;
        }
        var tid = Math.random().toString().split('.')[1];

        AddTreeSubmit(tid, nodeField, parentIdField, loadUrl, saveUrl, delUrl, null);

        $("#tree_loadUrl").val("");
        $("#tree_saveUrl").val("");
        $("#tree_delUrl").val("");
    });
}

function AddTreeSubmit(tid, nodeField, parentIdField, loadUrl, saveUrl, delUrl, reftableid, defaultbtns) {
    if (!defaultbtns) {
        defaultbtns = ['addroot', 'add', 'edit', 'delete'];
    }

    /*添加树配置到界面---Start------*/
    var table = $('<table class="tree" id="tree_' + tid + '" cellpadding="0" cellspacing="0"><thead><tr><th>树控件</td></tr></thead></table>');
    var refBtn = $("<input type='button' value='关联到表格' />");
    refBtn.bind("click", function () {
        var stable = $("#div_forTable table.select");
        if (stable.length == 0) {
            alert("请选中要关联到的表格！");
            return;
        }

        $("#tree_settablefk").open("对应到Table中的外键名称", function () {
            var fkname = $.trim($("#table_fkname").val());
            if (fkname.length == 0) {
                alert("请设置外键名称！");
                return false;
            }

            var id = stable[0].id.split('_')[1];
            temptrees[tid]["reftableid"] = id;
            temptrees[tid]["reftablefk"] = fkname;
            alert("关联成功。");
        });
    });
    table.find("tr").append($("<th class='tablebtn'></th>").append(refBtn));

    var changeBtn = $("<input type='button' value='修改默认按钮' />");
    changeBtn.bind('click', function () {
        var dbtns = temptrees[tid].defaultbtns;
        $("#tree_editdefaultbtns input[type=checkbox]").each(function () {
            for (var i = 0; i < dbtns.length; ++i) {
                if (dbtns[i] == $(this).attr("data-role")) {
                    this.checked = true;
                    return;
                }
                else {
                    if (this.checked) {
                        this.checked = false;
                    }
                }
            }
        });
        $("#tree_editdefaultbtns").open("编辑树默认按钮", function () {
            temptrees[tid].defaultbtns = [];
            $("#tree_editdefaultbtns input[type=checkbox]").each(function () {
                if (this.checked) {
                    temptrees[tid].defaultbtns.push($(this).attr("data-role"));
                }
            });
        });
    });
    table.find("tr").append($("<th class='tablebtn'></th>").append(changeBtn));

    var customBtn = $("<input type='button' value='自定义按钮' />");
    customBtn.bind('click', function () {
        $("#tt_custombtntd input[type=checkbox]").each(function () {
            $(this).next().remove();
            $(this).remove();
        });

        var btns = temptrees[tid].custombtns;
        if (btns && btns.length > 0) {
            for (var i = 0; i < btns.length; ++i) {
                $("#tt_custombtntd").append('<input type="checkbox" id="tbtn' + i + '" checked="checked" data-text="' + btns[i].text + '" data-action="' + btns[i].action + '" /> <label for="tbtn' + i + '">' + btns[i].text + '</label>');
            }
        }

        $("#tt_custombtn").open("自定义树操作按钮", function () {
            temptrees[tid].custombtns = [];

            $("#tt_custombtntd input[type=checkbox]").each(function () {
                if (this.checked) {
                    temptrees[tid].custombtns.push({ id: Math.random().toString().split('.')[1], text: $(this).attr("data-text"), action: $(this).attr("data-action") });
                }
            });
        });
    });
    table.find("tr").append($("<th class='tablebtn'></th>").append(customBtn));

    var removeBtn = $("<input type='button' value='移除树' />");
    $(removeBtn).bind("click", function () {
        if (confirm("确定要移除该树控件吗？")) {
            for (var i in temptrees) {
                if (i == tid) {
                    temptrees[i] = undefined;
                    $("#tree_" + tid).remove();
                    return;
                }
            }
        }
    });
    table.find("tr").append($("<th class='tablebtn'></th>").append(removeBtn));

    $("#div_forTable").append(table);
    /*添加树配置到界面---End------*/

    temptrees[tid] = { id: tid, nodeField: nodeField, parentIdField: parentIdField, loadurl: loadUrl, saveurl: saveUrl, delurl: delUrl, defaultbtns: defaultbtns };
    if (reftableid) {
        temptrees[tid]["reftableid"] = reftableid;
    }
}

var layerTable = null;
function AddTable() {
    layerTable = layer.open({
        title: ['添加表格配置'],
        type: 2,
        area: ['700px', '530px'],
        fix: false, //不固定
        maxmin: false,
        content: 'AddTableConfig.html'
    });
}

var temptables = {};
function AddTableSubmit(tablejson, defaultbtns) {
    if (!defaultbtns) {
        defaultbtns = ['add'];
    }

    var tid = Math.random().toString().split('.')[1];
    var table = $('<table class="table" id="tbl_' + tid + '" cellpadding="0" cellspacing="0"><thead><tr></tr></thead></table>');
    for (var i in tablejson.headers) {
        table.find("tr").append("<th>" + tablejson.headers[i].header + "</th>");
    }

    var searchBtn = $("<input type='button' value='设置查询' />");
    searchBtn.bind("click", function () {
        $("#table_searchitemtd input[type=checkbox]").each(function () {
            $(this).next().remove();
            $(this).remove();
        });

        var sitems = temptables[tid].searchitems;
        if (sitems && sitems.length > 0) {
            for (var i = 0; i < sitems.length; ++i) {
                $("#table_searchitemtd").append('<input type="checkbox" id="sitem' + i + '" checked="checked" data-title="' + sitems[i].title + '" data-property="' + sitems[i].property + '" data-type="' + sitems[i].type + '" data-bindid="' + sitems[i].bindjsonid + '" /> <label for="sitem' + i + '">' + sitems[i].title + '</label>');
            }
        }

        $("#table_search").open("设置表格查询", function () {
            temptables[tid].searchitems = [];

            $("#table_searchitemtd input[type=checkbox]").each(function () {
                if (this.checked) {
                    temptables[tid].searchitems.push({ id: Math.random().toString().split('.')[1], title: $(this).attr("data-title"), property: $(this).attr("data-property"), type: $(this).attr("data-type"), bindjsonid: $(this).attr("data-bindid") });
                }
            });
            if (temptables[tid].searchitems.length > 0) {
                temptables[tid].searchid = Math.random().toString().split('.')[1];
            }
            else {
                temptables[tid].searchid = "0";
            }
        });
    });
    table.find("tr").append($("<th class='tablebtn'></th>").append(searchBtn));

    var changeBtn = $("<input type='button' value='修改默认按钮' />");
    changeBtn.bind('click', function () {
        if ($(".tree").length == 0) {
            $("#table_movemorespan").hide();
        }
        else {
            $("#table_movemorespan").show();
        }
        var dbtns = temptables[tid].defaultbtns;
        $("#table_editdefaultbtns input[type=checkbox]").each(function () {
            for (var i = 0; i < dbtns.length; ++i) {
                if (dbtns[i] == $(this).attr("data-role")) {
                    this.checked = true;
                    return;
                }
                else {
                    if (this.checked) {
                        this.checked = false;
                    }
                }
            }
        });
        $("#table_editdefaultbtns").open("编辑表格默认按钮", function () {
            temptables[tid].defaultbtns = [];
            $("#table_editdefaultbtns input[type=checkbox]").each(function () {
                if (this.checked) {
                    var role = $(this).attr("data-role");
                    if (role == "movemore") {
                        temptables[tid].transferurl = $(this).attr("data-turl");
                    }
                    temptables[tid].defaultbtns.push($(this).attr("data-role"));
                }
            });
        });
    });
    table.find("tr").append($("<th class='tablebtn'></th>").append(changeBtn));

    var customBtn = $("<input type='button' value='自定义按钮' />");
    customBtn.bind('click', function () {
        $("#tt_custombtntd input[type=checkbox]").each(function () {
            $(this).next().remove();
            $(this).remove();
        });

        var btns = temptables[tid].custombtns;
        if (btns && btns.length > 0) {
            for (var i = 0; i < btns.length; ++i) {
                $("#tt_custombtntd").append('<input type="checkbox" id="tbtn' + i + '" checked="checked" data-text="' + btns[i].text + '" data-action="' + btns[i].action + '" /> <label for="tbtn' + i + '">' + btns[i].text + '</label>');
            }
        }

        $("#tt_custombtn").open("自定义表格操作按钮", function () {
            temptables[tid].custombtns = [];

            $("#tt_custombtntd input[type=checkbox]").each(function () {
                if (this.checked) {
                    temptables[tid].custombtns.push({ id: Math.random().toString().split('.')[1], text: $(this).attr("data-text"), action: $(this).attr("data-action") });
                }
            });
        });
    });
    table.find("tr").append($("<th class='tablebtn'></th>").append(customBtn));

    var removeBtn = $("<input type='button' value='移除表格' />");
    $(removeBtn).bind("click", function () {
        if (confirm("确定要移除该表格吗？")) {
            temptables[tid] = undefined;
        }
    });
    table.find("tr").append($("<th class='tablebtn'></th>").append(removeBtn));

    $("#div_forTable").append(table);

    var tablejson = { id: tid, data: tablejson, searchid: "0", searchitems: [], defaultbtns: defaultbtns };
    temptables[tid] = tablejson;

    $("#div_forTable .table").each(function () {
        if (!$(this).attr("data-event")) {
            $(this).attr("data-event", 1);
            $(this).bind("click", function () {
                var target = this;
                $("#div_forTable .table").each(function () {
                    if (target != this) {
                        $(this).removeClass("select");
                    }
                    else {
                        if (!$(this).hasClass("select")) {
                            $(this).addClass("select");
                        }
                    }
                });
            });
        }
    });

    layer.close(layerTable);

    return tablejson;
}

function table_setMoveMoreInfo(src) {
    if (src.checked) {
        $("#table_settransferurl").open("设置批量转移提交Url", function () {
            var url = $.trim($("#table_transferurl").val());
            if (url.length == 0) {
                alert("请输入批量转移提交Url！");
                return false;
            }

            $("#table_transferurl").val("");

            $(src).attr("data-turl", url);
        });
    }
}

function TableAddSearch() {
    $("#table_searchitem").open("新增查询项", function () {
        var title = $.trim($("#table_sitemtitle").val());
        var property = $.trim($("#table_sitemproperty").val());
        var type = $("#table_sitemtype").val();
        var bindjsonid = $("#table_sitembindjsonid").val();
        if (title == 0 || property.length == 0 || (type == 2 && bindjsonid.length == 0)) {
            alert("请维护所有项！");
            return false;
        }

        $("#table_sitemtitle").val("");
        $("#table_sitemproperty").val("");
        $("#table_sitemtype").val(1);
        $("#table_sitembindjsonid").val("");
        $("#table_sitembindjsonid").parent().parent().hide();

        var id = Math.random().toString().split('.')[1];
        $("#table_searchitemtd").append('<input type="checkbox" id="' + id + '" checked="checked" data-title="' + title + '" data-property="' + property + '" data-type="' + type + '" data-bindid="' + bindjsonid + '" /> <label for="' + id + '">' + title + '</label>');
    });
}

function table_sitemtypechanged(src) {
    if ($(src).val() == 2) {
        $("#table_sitembindjsonid").val("");
        $(src).parent().parent().next().show();
    }
    else {
        $(src).parent().parent().next().hide();
    }
}

function table_sitembindjson() {
    $("#div_bindform .chart_div").remove();

    $("#div_forJson .chart").each(function () {
        var chart = jConfigs["#" + $(this)[0].id];
        var rows = eval('(' + chart.data.toJSON() + ')').rows;
        $("#div_bindform").drawChart(rows);

        $("#div_bindform .chart_div").each(function () {
            var target = this;
            $(this).bind('click', function () {
                $("#div_bindform .chart_div").each(function () {
                    if (target == this) {
                        $(this).addClass("selectchart");
                    }
                    else {
                        $(this).removeClass("selectchart");
                    }
                });
            });
        });
    });

    $("#div_bindform").open("绑定数据源配置", function () {
        if ($("#div_bindform .selectchart").length == 0 || $("#div_bindform .selectchart .google-visualization-orgchart-nodesel").length == 0) {
            $.alert("请选中数据源节点！");
            return false;
        }
        else {
            var title = $("#div_bindform .selectchart .google-visualization-orgchart-nodesel").attr("title");
            var strs = title.split('_');
            var type = strs[2];
            if (type != 2) {
                $.alert("请选中数组类型节点！");
                return false;
            }
            $("#table_sitembindjsonid").val(strs[1]);
        }
    });
}

function AddJsonData() {
    $("#json_objectName").open("设置Json对象名称", function () {
        var name = $.trim($("#txt_jsonObjectName").val());
        var url = $.trim($("#txt_jsonUrl").val());
        if (name.length == 0) {
            alert("请输入对象名称！");
            return false;
        }
        if (url.length == 0) {
            alert("请输入获取对象Url！");
            return false;
        }

        $("#txt_jsonObjectName").val("");
        $("#txt_jsonUrl").val("");
        var id = Math.random().toString().split('.')[1];

        var params = [];
        var paramstr = $("#txt_jsonParams").val();
        if (paramstr.length > 0) {
            paramstr = paramstr.split(",");
            $(paramstr).each(function () {
                params.push($.trim(this));
            });
        }

        AddJsonDataSubmit(id, url, name, params);
    });
}

function AddJsonDataSubmit(id, url, name, params) {
    var selector = "chart_" + id;
    var chardiv = $("<div id='" + selector + "' class='chart'></div>");
    chardiv.attr("data-url", url);
    $("#div_forJson").append(chardiv);
    var chart = new jsonconfig('#' + selector, id, name + " {}");
    chart.params = params;

    BindChartContentEvent();

    return chart;
}

function AddFormData() {
    $("#form_primaryKey").open("添加表单配置", function () {
        var name = $.trim($("#txt_formPrimaryKey").val());
        if (name.length == 0) {
            alert("请输入主键名称！");
            return false;
        }

        var size = 0;
        try {
            size = parseInt($.trim($("#txt_formSize").val()));
        }
        catch (e) {
            alert("请输入表单尺寸！");
            return false;
        }

        $("#txt_formPrimaryKey").val("");
        $("#txt_formSize").val(6);

        var id = Math.random().toString().split('.')[1];
        var isshow = $("#cbx_hiddenForm")[0].checked ? 0 : 1;

        var btns = [];
        $("#form_btns input[type=checkbox]").each(function () {
            if (this.checked) {
                btns.push({ id: this.id, txt: $(this).attr("data-txt"), action: $(this).attr("data-action") });
            }
        });
        AddFormDataSubmit(id, isshow, name, size, btns);
    });
}

function AddFormDataSubmit(id, isshow, name, size, btns) {
    var selector = "chart_" + id;
    $("#div_forForm").append($("<div id='" + selector + "' class='chart'></div>"));

    var form = new formconfig('#' + selector, id, name, isshow, size, btns);
    form.beforeRemoveForm = function (fid) {
        var existstable = false;
        //检查是否存在表格绑定到该Form
        $(temptables).each(function () {
            $(this.data.headers).each(function () {
                if (this.type == "2") {
                    $(this.actionbtns).each(function () {
                        if (this.formdataid == id) {
                            alert("删除失败，存在Table操作项绑定到该Form！");
                            existstable = true;
                        }
                    });
                }
            });
        });

        if (existstable) {
            return false;
        }

        //检查绑定
        $("#div_forBind .bindchart").each(function () {
            var bind = $(this).bconfig();
            if (bind.formid == id) {
                var yes = confirm("该Form存在数据绑定，是否同时删除该数据绑定？");
                if (yes) {
                    bind.btn_remove.click();
                }
            }
        });

        return true;
    };

    BindChartContentEvent();

    return form;
}

function AddFormButtons(btncontainerselector) {
    var btncontainer = null;
    if (!btncontainerselector) {
        btncontainer = $("#form_btns");
    }
    else {
        btncontainer = $(btncontainerselector);
    }

    $("#form_addbtns").open("添加按钮", function () {
        var txt = $.trim($("#form_addbtntext").val());
        var action = $.trim($("#form_addbtnaction").val());
        if (txt.length == 0 || action.length == 0) {
            alert("请维护所有内容！");
            return false;
        }

        $("#form_addbtntext").val("");
        $("#form_addbtnaction").val("");
        var id = Math.random().toString().split('.')[1];
        btncontainer.append('<input type="checkbox" id="' + id + '" checked="checked" data-txt="' + txt + '" data-action="' + action + '" /> <label for="' + id + '">' + txt + '</label>');
    });
}

function AddDataBind() {
    var jchart = $("#div_forJson .selectchart");
    if (jchart.length == 0) {
        alert("请选中数据源区域！");
        return;
    }
    var fchart = $("#div_forForm .selectchart");
    if (fchart.length == 0) {
        alert("请选中目标控件区域！");
        return;
    }

    jchart = $("#" + jchart[0].id).jconfig();
    fchart = $("#" + fchart[0].id).fconfig();
    if (!jchart.selectNode) {
        alert("请选中数据源区域节点！");
        return;
    }
    if (!fchart.selectNode) {
        alert("请选中目标控件区域节点！");
        return;
    }

    if ((jchart.selectNode.type == 1 || jchart.selectNode.type == -1) && fchart.selectNode.type == -1) {
        var id = Math.random().toString().split('.')[1];
        AddDataBindSubmit(id, jchart, fchart, 1);
    }
    else if (jchart.selectNode.type == 2 && fchart.selectNode.type == 1) {
        var id = Math.random().toString().split('.')[1];
        AddDataBindSubmit(id, jchart, fchart, 2);
    }
    else {
        alert("选中的节点类型不匹配！");
        return;
    }
}

function AddDataBindSubmit(id, jchart, fchart, type) {
    var selector = "chart_" + id;
    $("#div_forBind").append($("<div id='" + selector + "' class='bindchart'></div>"));
    //数组绑定到select控件
    var jnid = '', fnid = '', jntext = '', fntext = '', jntype = '', fntype = '';
    if (jchart) {
        jnid = jchart.selectNode.id;
        jntext = jchart.selectNode.text;
        jntype = jchart.selectNode.type;
    }
    if (fchart) {
        fnid = fchart.selectNode.id;
        fntext = fchart.selectNode.text;
        fntype = fchart.selectNode.type;
    }

    var chart = new bindconfig(jnid, fnid, jntext, fntext, jntype, fntype, "#" + selector, type);
    return chart;
}

function BindChartContentEvent() {
    $("#div_forJson .chart").each(function () {
        if (!this.onclick) {
            $(this).bind('click', function () {
                $("#div_forJson .chart").each(function () {
                    if ($(this).hasClass("selectchart")) {
                        $(this).removeClass("selectchart");
                    }
                });

                $(this).addClass("selectchart");
            });
        }
    });
    $("#div_forForm .chart").each(function () {
        if (!this.onclick) {
            $(this).bind('click', function () {
                $("#div_forForm .chart").each(function () {
                    if ($(this).hasClass("selectchart")) {
                        $(this).removeClass("selectchart");
                    }
                });

                $(this).addClass("selectchart");
            });
        }
    });
}

function Save() {
    //构造页面数据
    var content = { trees: [], tables: [], jsons: [], forms: [], binds: [], authorities: _authorities };

    for (var i in temptrees) {
        if (temptrees[i]) {
            content.trees.push(temptrees[i]);
        }
    }

    for (var i in temptables) {
        if (temptables[i]) {
            content.tables.push(temptables[i]);
        }
    }

    $("#div_forJson .chart").each(function () {
        var id = this.id.split('_')[1];
        var url = $(this).attr("data-url");
        var chart = $(this).jconfig();
        var data = JSON.parse(chart.data.toJSON()).rows;
        content.jsons.push({ id: id, url: url, data: data, parameters: chart.params });
    });

    $("#div_forForm .chart").each(function () {
        var id = this.id.split('_')[1];
        var chart = $(this).fconfig();
        var data = JSON.parse(chart.data.toJSON()).rows;
        content.forms.push({ id: id, isshow: chart.isshow, disabledurl: chart.disabledurl, size: chart.size, data: data, extdata: chart.extData, btns: chart.btns });
    });

    $("#div_forBind .bindchart").each(function () {
        var id = this.id.split('_')[1];
        var chart = $(this).bconfig();
        var data = JSON.parse(chart.data.toJSON()).rows;
        content.binds.push({ id: id, data: data, type: chart.type });
    });

    window.external.SaveToFile(JSON.stringify(content));
}

function InitUI(jsonstr) {
    var json = JSON.parse(jsonstr);
    //权限赋值
    if (json.authorities) {
        _authorities = json.authorities;
    }
    

    $(json.trees).each(function () {
        AddTreeSubmit(this.id, this.nodeField, this.parentIdField, this.loadurl, this.saveurl, this.delurl, this.reftableid, this.defaultbtns);
    });

    $(json.tables).each(function () {
        var json = AddTableSubmit(this.data, this.defaultbtns);
        json.searchid = this.searchid;
        json.searchitems = this.searchitems;
        json.transferurl = this.transferurl;
    });

    $(json.jsons).each(function () {
        var chart = AddJsonDataSubmit(this.id, this.url, "", this.parameters);
        var data = InitChartData(this.data);
        chart.data = data;
        chart.chart.draw(data, { allowHtml: true, allowCollapse: true, size: "large" });
        chart.bindNodeSelectEvent();
    });

    $(json.forms).each(function () {
        var chart = AddFormDataSubmit(this.id, this.isshow, '', this.size, this.btns);
        chart.disabledurl = this.disabledurl;
        chart.extData = this.extdata;
        var data = InitChartData(this.data);
        chart.data = data;
        chart.chart.draw(data, { allowHtml: true, allowCollapse: true, size: "large" });
        chart.bindNodeSelectEvent();
    });

    $(json.binds).each(function () {
        var data = InitChartData(this.data);
        var chart = AddDataBindSubmit(this.id, null, null, this.type);
        chart.data = data;
        chart.chart.draw(data, { allowHtml: true, allowCollapse: true, size: "large" });
    });
}

function InitChartData(rows) {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Name');
    data.addColumn('string', 'Manager');
    data.addColumn('string', 'title');

    var nrows = [];
    for (var i in rows) {
        nrows[nrows.length] = [rows[i].c[0], rows[i].c[1].v, rows[i].c[2].v];
    }
    data.addRows(nrows);
    return data;
}

$(function () {
    $("#validate_9").bind("click", function () {
        var ctr = this;
        if (this.checked) {
            $("#form_validateLength").open("维护最小最大长度", function () {
                var minlen = parseInt($.trim($("#txt_validateMinLength").val()));
                if (!minlen) {
                    minlen = '--';
                }
                var maxlen = parseInt($.trim($("#txt_validateMaxLength").val()));
                if (!maxlen) {
                    maxlen = '++';
                }

                $("#txt_validateMinLength").val("");
                $("#txt_validateMaxLength").val("");

                $(ctr).attr("data-minlen", minlen);
                $(ctr).attr("data-maxlen", maxlen);
            }, function () {
                ctr.checked = false;
            }, true, false);
        }
        else {
            $(this).removeAttr("data-minlen");
            $(this).removeAttr("data-maxlen");
        }
    });

    $("#validate_10").bind("click", function () {
        var ctr = this;
        if (this.checked) {
            $("#form_validateReg").open("维护正则表达式", function () {
                var regstr = $.trim($("#txt_validateRegString").val());
                $("#txt_validateRegString").val("");
                $(ctr).attr("data-reg", regstr);
            }, function () {
                ctr.checked = false;
            }, true, false);
        }
        else {
            $(this).removeAttr("data-reg");
        }
    });
});