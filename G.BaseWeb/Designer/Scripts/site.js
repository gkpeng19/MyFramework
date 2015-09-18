google.load("visualization", "1", { packages: ["orgchart"] });

/*------JsonConfig----------------------------------------------------------*/
var jConfigs = {};
$.fn.extend({
    jconfig: function () {
        return jConfigs["#" + this[0].id];
    }
});

var jsonconfig = function (selector, id, root) {
    jConfigs[selector] = this;

    root = root ? root : '{}';
    root = root.length > 0 ? root : '{}';

    this.chartCon = $(selector);
    this.btn_addObject = $("<input type='button' value='添加对象' />");
    this.btn_addArray = $("<input type='button' value='添加数组' />");
    this.btn_removeNode = $("<input type='button' value='移除' style='display:none;' />");
    this.btn_removeCon = $("<input type='button' value='移除该对象' />");
    this.chartCon.append(this.btn_addObject).append(this.btn_addArray).append(this.btn_removeNode).append(this.btn_removeCon);

    var target = this;
    this.btn_addObject.bind('click', function () {
        if (!target.selectNode) {
            alert("请选中一个节点！");
            return;
        }

        $("#json_propertyName").open("输入名称", function () {
            var name = $.trim($("#txt_jsonPropertyName").val());
            $("#txt_jsonPropertyName").val('');
            if (name.length == 0) {
                alert("请维护属性名称！");
                return false;
            }

            target.AddNode(1, name + " {}");
        });
    });
    this.btn_addArray.bind('click', function () {
        if (!target.selectNode) {
            alert("请选中一个节点！");
            return;
        }

        $("#json_propertyName").open("输入名称", function () {
            var name = $.trim($("#txt_jsonPropertyName").val());
            $("#txt_jsonPropertyName").val('');
            if (name.length == 0) {
                alert("请维护属性名称！");
                return false;
            }

            target.AddNode(2, name + " []");
        });
    });
    this.btn_removeNode.bind('click', function () {
        if (!target.selectNode) {
            alert("请选中一个节点！");
            return;
        }

        target.RemoveSelectNode();
    });
    this.btn_removeCon.bind('click', function () {
        if (confirm("确定要移除整个对象吗？")) {
            target.chartCon.remove();
        }
    });

    this.chartCon.append("<div class='chart_div'></div>");

    this.data = new google.visualization.DataTable();
    this.data.addColumn('string', 'Name');
    this.data.addColumn('string', 'Manager');
    this.data.addColumn('string', 'title');

    this.data.addRows([
      [{ v: id, f: root }, '', '0_' + id + '_-1']
    ]);

    this.chart = new google.visualization.OrgChart(this.chartCon.find(".chart_div")[0]);
    this.chart.draw(this.data, { allowHtml: true, allowCollapse: true, size: "large" });

    this.bindNodeSelectEvent();

    return this;
};

jsonconfig.prototype.selectNode = null;//id,level,type,text
jsonconfig.prototype.bindNodeSelectEvent = function () {
    var target = this;
    this.chartCon.find(".google-visualization-orgchart-node").bind("click", function () {
        if (target.chartCon.find(".google-visualization-orgchart-nodesel").length == 0) {
            target.selectNode = null;
        }
        else {
            target.selectNode = {};
            var strs = $(this).attr("title").split('_');
            target.selectNode.id = strs[1];
            target.selectNode.level = parseInt(strs[0]);
            target.selectNode.type = parseInt(strs[2]);
            target.selectNode.text = $(this).text();

            if (target.selectNode.level == 0) {
                target.btn_addObject.show();
                target.btn_addArray.show();
                target.btn_removeNode.hide();
            }
            else if (target.selectNode.level == 1) {
                target.btn_addObject.hide();
                target.btn_addArray.hide();
                target.btn_removeNode.show();
            }
        }
    });
}

//type==-1：root，1：object，2：array
jsonconfig.prototype.AddNode = function (type, text) {
    var v = Math.random().toString().split('.')[1];
    this.data.addRows([[{ v: v, f: text }, this.selectNode.id, (this.selectNode.level + 1).toString() + '_' + v + '_' + type]]);
    this.chart.draw(this.data, { allowHtml: true, allowCollapse: true, size: "large" });
    this.bindNodeSelectEvent();
    this.selectNode = null;
};

jsonconfig.prototype.RemoveSelectNode = function () {
    var rows = eval('(' + this.data.toJSON() + ')').rows;
    for (var i = 0; i < rows.length; ++i) {
        var v = rows[i].c[0].v;
        if (this.selectNode.id == v) {
            this.data.removeRow(i);
            this.chart.draw(this.data, { allowHtml: true, allowCollapse: true, size: "large" });
            this.bindNodeSelectEvent();
            return;
        }
    }
};

/*-------FormConfig--------------------------------------------------------*/
var fConfig = {};
$.fn.extend({
    fconfig: function () {
        return fConfig["#" + this[0].id];
    }
});
var formconfig = function (selector, id, pkey, isshow, size, btns) {
    this.id = id;
    this.isshow = isshow;
    this.size = size;
    this.btns = btns;
    this.disabledurl = "";

    fConfig[selector] = this;

    var root = '{}';

    this.chartCon = $(selector);

    this.btn_input = $('<input type="button" value="添加 Input" data-type="0" />');
    this.btn_select = $('<input type="button" value="添加 Select" data-type="1" />');
    this.btn_datetime = $('<input type="button" value="添加 DateTime" data-type="2" />');
    this.btn_textarea = $('<input type="button" value="添加 TextArea" data-type="3" />');
    this.btn_cbx = $('<input type="button" value="添加 CheckBox" data-type="4" />');
    this.btn_editor = $('<input type="button" value="添加 HtmlEditor" data-type="5" />');
    this.btn_btns = $('<input type="button" value="编辑提交按钮"/>');
    this.btn_fmdisable = $('<input type="button" value="只读开关"/>');
    this.btn_removeNode = $("<input type='button' value='移除' style='display:none;' />");
    this.btn_removeCon = $("<input type='button' value='移除该对象' />");
    this.chartCon.append(this.btn_input).append(this.btn_select).append(this.btn_datetime).append(this.btn_textarea)
        .append(this.btn_cbx).append(this.btn_editor);

    if (this.isshow == 1) {
        this.chartCon.append(this.btn_btns).append(this.btn_fmdisable);
    }

    this.chartCon.append(this.btn_removeNode).append(this.btn_removeCon);

    var target = this;
    var click_func = function (type) {
        if (!target.selectNode) {
            alert("请选中一个节点！");
            return;
        }

        $("#form_propertyName").open("属性", function () {
            var name = $.trim($("#txt_formPropertyName").val());
            var showname = $.trim($("#txt_formShowName").val());
            if (name.length == 0) {
                alert("请维护属性名称！");
                return false;
            }
            if (showname.length == 0) {
                alert("请维护显示名称！");
                return false;
            }

            var csize = 0;
            try {
                csize = parseInt($.trim($("#txt_formCtrSize").val()));
            } catch (e) {
                alert("请维护控件尺寸！");
                return false;
            }

            /*初始化额外数据--------Start*/
            var extData = {};
            extData.showname = showname;
            extData.validates = [];
            extData.size = csize;
            $("#form_tab_validate input[type=checkbox]").each(function () {
                if (this.checked) {
                    var name = $(this).attr("name");
                    var validate = { type: name };
                    if (name == "lenvalidate") {
                        validate.minlen = $(this).attr("data-minlen");
                        validate.maxlen = $(this).attr("data-maxlen");
                    }
                    else if (name == "regvalidate") {
                        validate.regstr = $(this).attr("data-reg");
                    }
                    extData.validates.push(validate);

                    this.checked = false;
                }
            });
            /*初始化额外数据--------End*/

            $("#txt_formPropertyName").val('');
            $("#txt_formShowName").val('');
            $("#txt_formCtrSize").val('');

            target.AddNode(type, name, extData);
        });
    };
    this.ctrTypes = ['I', 'S', 'DT', 'TA', 'CB', 'HE', '', '', '', 'PK'];

    this.btn_input.bind('click', function () { click_func(0); });
    this.btn_select.bind('click', function () { click_func(1); });
    this.btn_datetime.bind('click', function () { click_func(2); });
    this.btn_textarea.bind('click', function () { click_func(3); });
    this.btn_cbx.bind('click', function () { click_func(4); });
    this.btn_editor.bind('click', function () { click_func(5); });

    if (this.isshow == 1) {
        this.btn_btns.bind("click", function () {
            $("#form_editbtns td .tdcontainer").remove();
            $("#form_editbtns td").append("<div class='tdcontainer'></div>");
            $("#form_editbtns .tdcontainer").append('<input type="button" value="添加" onclick="AddFormButtons(\'#form_editbtns .tdcontainer\')" />');

            $(target.btns).each(function () {
                var id = Math.random().toString().split('.')[1];
                $("#form_editbtns .tdcontainer").append('<input type="checkbox" id="' + id + '" checked="checked" data-txt="' + this.txt + '" data-action="' + this.action + '" /> <label for="' + id + '">' + this.txt + '</label>');
            });

            $("#form_editbtns").open("编辑表单提交按钮", function () {
                var btns = [];
                $("#form_editbtns .tdcontainer").find("input[type=checkbox]").each(function () {
                    if (this.checked) {
                        btns.push({ id: this.id, txt: $(this).attr("data-txt"), action: $(this).attr("data-action") });
                    }
                });

                target.btns = btns;
            });
        });

        this.btn_fmdisable.bind("click", function () {
            $("#form_disabled").open("设置只读", function () {
                var url = $.trim($("#fm_disabled_url").val());
                if (url.length == 0) {
                    alert("请输入只读Url！");
                    return false;
                }
                $("#fm_disabled_url").val("");
                target.disabledurl = url;
            });
        });
    }

    this.btn_removeNode.bind('click', function () {
        if (!target.selectNode) {
            alert("请选中一个节点！");
            return;
        }

        target.RemoveSelectNode();
    });

    this.beforeRemoveForm = null;
    this.btn_removeCon.bind('click', function () {
        if (confirm("确定要移除整个对象吗？")) {
            if (target.beforeRemoveForm) {
                if (target.beforeRemoveForm(target.id)) {
                    target.chartCon.remove();
                }
            }
            else {
                target.chartCon.remove();
            }
        }
    });

    this.chartCon.append("<div class='chart_div'></div>");

    this.data = new google.visualization.DataTable();
    this.data.addColumn('string', 'Name');
    this.data.addColumn('string', 'Manager');
    this.data.addColumn('string', 'title');

    var pid = Math.random().toString().split('.')[1];
    this.data.addRows([
      [{ v: id, f: root }, '', '0_' + id + '_-1'],
      [{ v: pid, f: pkey + ' {' + this.ctrTypes[9] + '}' }, id, '1_' + pid + '_9']
    ]);

    this.extData = { layer: 2, extdatas: [] };

    this.chart = new google.visualization.OrgChart(this.chartCon.find(".chart_div")[0]);
    this.chart.draw(this.data, { allowHtml: true, allowCollapse: true, size: "large" });

    this.bindNodeSelectEvent();

    return this;
};

formconfig.prototype.selectNode = null;//id,level,type,text
formconfig.prototype.bindNodeSelectEvent = function () {
    var target = this;
    this.chartCon.find(".google-visualization-orgchart-node").bind("click", function () {
        if (target.chartCon.find(".google-visualization-orgchart-nodesel").length == 0) {
            target.selectNode = null;
        }
        else {
            target.selectNode = {};
            var strs = $(this).attr("title").split('_');
            target.selectNode.id = strs[1];
            target.selectNode.level = parseInt(strs[0]);
            target.selectNode.type = parseInt(strs[2]);
            target.selectNode.text = $(this).text();

            if (target.selectNode.level == 0) {
                target.btn_input.show();
                target.btn_select.show();
                target.btn_datetime.show();
                target.btn_textarea.show();
                target.btn_cbx.show();
                target.btn_editor.show();
                target.btn_removeNode.hide();
            }
            else if (target.selectNode.level == 1) {
                target.btn_input.hide();
                target.btn_select.hide();
                target.btn_datetime.hide();
                target.btn_textarea.hide();
                target.btn_cbx.hide();
                target.btn_editor.hide();
                target.btn_removeNode.show();
            }
        }
    });
}

/*type==-1：root，0：Input，1：Select，2：DateTime，3：TextArea，9：PrimarkKey*/
formconfig.prototype.AddNode = function (type, text, exData) {
    var v = Math.random().toString().split('.')[1];
    this.data.addRows([[{ v: v, f: text + ' {' + this.ctrTypes[type] + '}' }, this.selectNode.id, (this.selectNode.level + 1).toString() + '_' + v + '_' + type]]);
    this.chart.draw(this.data, { allowHtml: true, allowCollapse: true, size: "large" });
    this.bindNodeSelectEvent();
    this.selectNode = null;

    this.extData.extdatas.push({ dataid: v, showname: exData.showname, size: exData.size, validates: exData.validates });
};

formconfig.prototype.RemoveSelectNode = function () {
    if (this.selectNode.type == 9) {
        alert("主键节点不能被移除！");
        return;
    }
    var rows = eval('(' + this.data.toJSON() + ')').rows;
    for (var i = 0; i < rows.length; ++i) {
        var v = rows[i].c[0].v;
        if (this.selectNode.id == v) {
            this.data.removeRow(i);
            this.chart.draw(this.data, { allowHtml: true, allowCollapse: true, size: "large" });
            this.bindNodeSelectEvent();

            for (var i = 0; i < this.extData.extdatas.length; ++i) {
                var item = this.extData.extdatas[i];
                if (item && item.dataid == v) {
                    this.extData.extdatas[i] = null;
                    break;
                }
            }
            return;
        }
    }
};

/*--------BindConfig----------------------------------------------------------------*/
var bConfig = {};
$.fn.extend({
    bconfig: function () {
        return bConfig["#" + this[0].id];
    }
});

//type==1：对象绑定到div，2：数组绑定到select控件
var bindconfig = function (jnodeid, fnodeid, jnodetext, fnodetext, jnodetype, fnodetype, selector, type) {
    this.type = type;

    bConfig[selector] = this;
    this.chartCon = $(selector);
    this.btn_remove = $('<input type="button" value="移除Binding" />');

    var target = this;
    this.btn_remove.bind("click", function () {
        if (confirm("确定要移除该绑定吗？")) {
            target.chartCon.remove();
        }
    });

    this.chartCon.append(this.btn_remove);

    this.chartCon.append("<div class='chart_div'></div>");

    this.data = new google.visualization.DataTable();
    this.data.addColumn('string', 'Name');
    this.data.addColumn('string', 'Manager');
    this.data.addColumn('string', 'title');

    this.data.addRows([
     [{ v: jnodeid, f: jnodetext }, '', jnodetype.toString()],
     [{ v: fnodeid, f: fnodetext }, jnodeid, fnodetype.toString()]
    ]);

    this.chart = new google.visualization.OrgChart(this.chartCon.find(".chart_div")[0]);
    this.chart.draw(this.data, { allowHtml: true, allowCollapse: true, size: "large" });

    return this;
}

/*-------重绘chart------------------------------------------------------------------*/
$.fn.extend({
    drawChart: function (rows) {
        var chart_div = $("<div class='chart_div'></div>");
        this.append(chart_div);
        chart_div[0].id = rows[0].c[2].v.split('_')[1];

        var data = new google.visualization.DataTable();
        data.addColumn('string', 'Name');
        data.addColumn('string', 'Manager');
        data.addColumn('string', 'title');

        var nrows = [];
        for (var i in rows) {
            nrows[nrows.length] = [rows[i].c[0], rows[i].c[1].v, rows[i].c[2].v];
        }
        data.addRows(nrows);

        var chart = new google.visualization.OrgChart(chart_div[0]);
        chart.draw(data, { allowHtml: true, allowCollapse: true, size: "large" });
    }
});