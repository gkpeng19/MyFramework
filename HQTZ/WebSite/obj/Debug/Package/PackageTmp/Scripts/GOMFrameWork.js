var gExtends = { datagrid: {}, gtree: {} };

$.fn.extend({
    /*----DataGrid---------------------------*/
    datagrid: function (a, b, c, d, e) {
        if (typeof (a) == "object") {
            //datagrid 初始化
            var pageSize = 10;
            var table = null;
            if (!a) {
                table = new datatable(this, pageSize);
                gExtends.datagrid[this[0].id] = table;
            }
            else {
                if (a.psize) {
                    pageSize = a.psize;
                }

                table = gExtends.datagrid[this[0].id];
                if (!table) {
                    table = new datatable(this, pageSize);
                    gExtends.datagrid[this[0].id] = table;
                }

                if (a.multiple == false) {
                    table.multiple = false;
                }
            }

            if (a.url && a.url.length > 0) {
                table.initByUrl(a.url, a.search, 1);
            }
            else {
                if (!a.data || a.data.length == 0) {
                    return;
                }
                table.initByData(a.data, a.page, a.pagecount);
            }
        }
        else {
            //调用无参函数
            var grid = gExtends.datagrid[this[0].id];
            if (grid) {
                return grid[a](b, c, d, e);
            }
        }
    },
    /*----Pager---------------------------*/
    pager: function (pageindex, pagecount, onPageChanged, pagebutton) {
        if (!pagebutton) {
            pagebutton = 10;
        }

        var html = "";
        if (pageindex > 1) {
            html += "<a class='normal' href='javascript:void(0);' data-page='" + (parseInt(pageindex) - 1) + "'>«</a>";
        }

        if (pagecount <= pagebutton) {
            for (var i = 1; i <= pagecount; ++i) {
                if (pageindex == i) {
                    html += "<a class='current' href='javascript:void(0);' data-page='" + i + "'>" + i + "</a>";
                }
                else {
                    html += "<a class='normal' href='javascript:void(0);' data-page='" + i + "'>" + i + "</a>";
                }
            }
        }
        else {
            var lbutton = 0;
            var rbutton = 0;
            var mbutton = 0;
            if (pagebutton % 3 == 0) {
                mbutton = parseInt(pagebutton / 3);
                lbutton = rbutton = mbutton - 1;
            }
            else {
                mbutton = parseInt(pagebutton / 3) + 1;
                lbutton = parseInt((pagebutton - 2 - mbutton) / 2);
                rbutton = pagebutton - 2 - mbutton - lbutton;
            }

            var ignoreleft = false;
            var ignoreright = false;

            var mloffset = parseInt((mbutton - 1) / 2);
            var mlbuttonindex = 0;//中间部分左边第一个按钮数字
            if (pageindex <= lbutton) {
                mlbuttonindex = lbutton + 1;
                ignoreleft = true;
            }
            else if (pageindex > pagecount - rbutton) {
                mlbuttonindex = pagecount - rbutton - mbutton + 1;
                ignoreright = true;
            }
            else {
                if (pageindex - lbutton - mloffset <= 1) {
                    mlbuttonindex = lbutton + 1;
                    ignoreleft = true;
                }
                else if (pagecount - rbutton - mbutton + 1 + mloffset < pageindex) {
                    mlbuttonindex = pagecount - rbutton - mbutton + 1;
                    ignoreright = true;
                }
                else {
                    mlbuttonindex = pageindex - mloffset;
                }
            }

            for (var i = 1; i <= lbutton; ++i) {
                if (pageindex == i) {
                    html += "<a class='current' href='javascript:void(0);' data-page='" + i + "'>" + i + "</a>";
                }
                else {
                    html += "<a class='normal' href='javascript:void(0);' data-page='" + i + "'>" + i + "</a>";
                }
            }

            if (!ignoreleft) {
                html += "<a class='ignore' href='javascript:void(0);'>…</a>";
            }

            for (var i = mlbuttonindex; i < mlbuttonindex + mbutton; ++i) {
                if (pageindex == i) {
                    html += "<a class='current' href='javascript:void(0);' data-page='" + i + "'>" + i + "</a>";
                }
                else {
                    html += "<a class='normal' href='javascript:void(0);' data-page='" + i + "'>" + i + "</a>";
                }
            }

            if (!ignoreright) {
                html += "<a class='ignore' href='javascript:void(0);'>…</a>";
            }

            for (var i = pagecount - rbutton + 1; i <= pagecount; ++i) {
                if (pageindex == i) {
                    html += "<a class='current' href='javascript:void(0);' data-page='" + i + "'>" + i + "</a>";
                }
                else {
                    html += "<a class='normal' href='javascript:void(0);' data-page='" + i + "'>" + i + "</a>";
                }
            }
        }

        if (pageindex < pagecount) {
            html += "<a class='normal' href='javascript:void(0);' data-page='" + (parseInt(pageindex) + 1) + "'>»</a>";
        }

        $(this).html(html);
        if (onPageChanged) {
            $(this).find("a.normal").each(function () {
                $(this).bind("click", function () {
                    onPageChanged($(this).attr("data-page"));
                });
            });
        }
    }
});

/*-------DataTable-----------------------------------------------------*/
var datatable = function (table, pageSize) {
    this.datagrid = table;
    this.multiple = true;
    this.psize = pageSize;
    this.trIdentity = "_dtr_g";

    this.currIdentity = -1;

    var hers = [];
    this.datagrid.find("thead th,thead td").each(function (index) {
        var option = eval('(' + $(this).attr("data-option") + ')');
        if (option != undefined) {
            hers[index] = option;
        }
    });
    this.headers = hers;

    var dg = this;

    this.tool = {};
    this.tool.initThCbx = function () {
        /*------------初始化全选按钮功能开始------------*/
        var cbkTh = dg.datagrid.find("th.th-ckbox");
        cbkTh.html("<input type='checkbox' /><span class='lbl'></span>");
        cbkTh.find("input[type=checkbox]").bind("change", function () {
            if (this.checked) {
                table.find(".ckbox").each(function () {
                    if (!this.checked) {
                        this.click();
                    }
                });
            }
            else {
                table.find(".ckbox").each(function () {
                    if (this.checked) {
                        this.click();
                    }
                });
            }
        });
        /*------------初始化全选按钮功能结束------------*/
    };

    this.tool.bindCheckBoxEvent = function (cbxs, json) {
        var source = null;
        if (json.length > 0) {
            source = json;
        }
        else {
            source = [];
            source.push(json);
        }

        cbxs.each(function (index) {
            $(this).bind('click', function () {
                var ck = this;
                if (!dg.multiple && this.checked) {
                    var ckBefores = dg.datagrid.find("tr.info .ckbox");
                    if (ckBefores.length > 0) {
                        $(ckBefores).each(function () {
                            if (this != ck) {
                                this.checked = false;
                                $(this).trigger('change');
                            }
                        });
                    }
                }
                event.stopPropagation(); // 阻止事件冒泡
            });
            $(this).bind('change', function () {
                if (this.checked) {
                    $(this).parents("tr").addClass("info");

                    var target = source[index];
                    target.checked_g = true;

                    if (dg.selectChanged) {
                        dg.selectChanged(target);
                    }

                    //当所有都勾选时，勾选全选按钮
                    var isCheck = true;
                    dg.datagrid.find(".ckbox").each(function () {
                        if (!this.checked) {
                            isCheck = false;
                            return;
                        }
                    });
                    dg.datagrid.find('.th-ckbox input[type=checkbox]')[0].checked = isCheck;
                }
                else {
                    $(this).parents("tr").removeClass("info");

                    source[index].checked_g = false;
                    //当所有勾选取消时，取消勾选全选按钮
                    dg.datagrid.find(".ckbox").each(function () {
                        if (!this.checked) {
                            dg.datagrid.find('.th-ckbox input[type=checkbox]')[0].checked = false;
                            return;
                        }
                    });
                }
            });
        });
    };

    this.tool.initThCbx();

    return this;
}

datatable.prototype.onCustomEdit = null;
datatable.prototype.onCustomDelete = null;
datatable.prototype.onCustomPager = null;
datatable.prototype.selectChanged = null;

datatable.prototype.url = null;
datatable.prototype.search = null;
datatable.prototype.initByUrl = function (url, search, page) {
    this.datagrid.loading();
    //取消全选
    var ck = this.datagrid.find('.th-ckbox input[type=checkbox]');
    if (ck.length > 0) {
        ck[0].checked = false;
    }

    this.url = url;
    this.search = search;

    if (!search) {
        search = {};
    }
    else {
        if (typeof (search) != "object") {
            search = $(search).getContext();
        }
    }
    page = page ? page : 1;
    search.page_g = page;
    search.psize_g = this.psize;

    var dg = this;
    $.post(url, search, function (r) {
        dg.initByData($.toJsResult(r.Data), page, r.PageCount);

        dg.datagrid.loaded();
    });
};

datatable.prototype.initByData = function (data, page, pagecount) {
    this.datasource = data;
    this.length = data.length;
    this.maxIdentity = data.length - 1;

    if (!this.multiple) {/*不能多选时，移除全选按钮*/
        this.datagrid.find("th.th-ckbox span").remove();
    }

    this.datagrid.find("tbody").remove();

    var tbody = $("<tbody></tbody>");
    this.datagrid.append(tbody);

    var html = "";
    for (var j = 0; j < this.datasource.length; ++j) {
        html += this.initLineHtml(this.datasource[j], j);
    }
    tbody.html(html);

    //序列赋值
    tbody.find(".sequence").each(function (index) {
        $(this).text(index + 1);
    });

    //选中索引赋值
    var dg = this;
    tbody.find("tr").each(function () {
        $(this).bind('click', function () {
            var ckCurr = $(this).find(".ckbox");
            if (ckCurr.length > 0) {
                var ckBefores = dg.datagrid.find("tr.info .ckbox");
                if (ckBefores.length > 0) {
                    $(ckBefores).each(function () {
                        this.checked = false;
                        $(this).trigger('change');
                    });
                }
                ckCurr[0].checked = true;
                ckCurr.trigger('change');
            }
        });
    });

    //复选框勾选、取消勾选事件
    var olddsource = [];
    $(this.datasource).each(function () {
        olddsource.push(this);
    });
    dg.tool.bindCheckBoxEvent(tbody.find(".ckbox"), olddsource);

    //绑定编辑、删除事件
    tbody.find(".edit").each(function () {
        $(this)[0].onclick = function () {
            if (dg.onCustomEdit) {
                dg.onCustomEdit();
            }
        };
    });
    tbody.find(".delete").each(function () {
        $(this)[0].onclick = function () {
            if (dg.onCustomDelete) {
                dg.onCustomDelete();
            }
        };
    });

    tbody.find("td a,td input[type=button]").each(function () {
        var eve = this.onclick;
        if (eve) {
            this.onclick = null;
            $(this).bind('click', function () {
                dg.currIdentity = $(this).parents("tr")[0].id.split("_")[0];
                eve();
            });
        }
    });

    if (this.selectChanged && this.datasource.length > 0) {
        this.selectChanged(this.datasource[0]);
    }

    /*----Init Pager Start--------------------------*/
    this.datagrid.parent().find(".pager").remove();

    if (!page || !pagecount || page < 1 || pagecount < 2) {
        return;
    }
    var pagerDiv = $("<div class='pager'></div>");
    this.datagrid.parent().append(pagerDiv);
    var dg = this;
    pagerDiv.pager(page, pagecount, function (newpage) {
        if (dg.url) {
            dg.initByUrl(dg.url, dg.search, newpage);
        }

        if (dg.onCustomPager) {
            dg.onCustomPager(newpage);
        }
    });
    /*----Init Pager End--------------------------*/
};

datatable.prototype.initLineHtml = function (json, identity) {
    json[this.trIdentity] = identity;

    var html = "<tr id='" + identity + this.trIdentity + "' " + (json.checked_g ? "class='info'" : "") + ">";
    for (var h = 0; h < this.headers.length; ++h) {
        var option = this.headers[h];
        if (option.sequence) {
            html += "<td><span class='sequence'></span></td>";
        }
        else if (option.checkbox && !option.bind) {
            html += "<td><input type='checkbox' class='ckbox' " + (json.checked_g ? "checked='checked' " : '') + "/><span class='lbl'></span></td>";
        }
        else {
            var vhtm = "";
            if (option.edit || option.del) {
                if (option.edit) {
                    vhtm += ' <a href="javascript:void(0)" class="edit btn btn-minier btn-primary">编辑</a> ';
                }

                if (option.del) {
                    vhtm += ' <a href="javascript:void(0)"  class="delete btn btn-minier btn-danger">删除</a> ';
                }
            }
            else {
                if (option.bind) {
                    vhtm = json[option.bind];
                    if (vhtm == undefined) {
                        vhtm = "";
                    }

                    if (option.format) {
                        vhtm = option.format(vhtm);
                    }
                }

                if (option.checkbox) {
                    vhtm = '<input type="checkbox" disabled="disabled" ' + (vhtm == 1 ? 'checked="checked"' : '') + '/><span class="lbl"></span>';
                }
            }

            if (option.onload) {
                vhtm = option.onload(json, vhtm);
            }

            html += "<td>" + vhtm + "</td>";
        }
    }
    return html + "</tr>";
};

datatable.prototype.initDataGridLine = function (json) {
    this.maxIdentity++;
    json[this.trIdentity] = this.maxIdentity;
    var tr = $(this.initLineHtml(json, this.maxIdentity));

    //绑定复选框勾选、取消勾选事件
    this.tool.bindCheckBoxEvent(tr.find(".ckbox"), json);

    var dg = this;

    tr.bind('click', function () {
        var ckCurr = $(this).find(".ckbox");
        if (ckCurr.length > 0) {
            var ckBefores = dg.datagrid.find("tr.info .ckbox");
            if (ckBefores.length > 0) {
                $(ckBefores).each(function () {
                    this.checked = false;
                    $(this).trigger('change');
                });
            }
            ckCurr[0].checked = true;
            ckCurr.trigger('change');
        }
    });

    //绑定编辑、删除事件
    tr.find(".edit").each(function () {
        $(this)[0].onclick = function () {
            if (dg.onCustomEdit) {
                dg.onCustomEdit();
            }
        };
    });
    tr.find(".delete").each(function () {
        $(this)[0].onclick = function () {
            if (dg.onCustomDelete) {
                dg.onCustomDelete();
            }
        };
    });

    tr.find("a,input[type=button]").each(function () {
        var eve = this.onclick;
        if (eve) {
            this.onclick = null;
            $(this).bind('click', function () {
                dg.currIdentity = $(this).parents("tr")[0].id.split("_")[0];
                eve();
            });
        }
    });

    return tr;
};

//------------------------------------------------------------------------------------

datatable.prototype.getCurrData = function () {
    if (this.currIdentity == -1 || this.currIdentity > this.maxIdentity) {
        return null;
    }

    var index = $.binarySearch(this.datasource, this.trIdentity, this.currIdentity);
    if (index != -1) {
        return this.datasource[index];
    }
    return null;
};

datatable.prototype.addRow = function (json) {
    var tr = this.initDataGridLine(json);

    this.datagrid.find("tbody").append(tr);
    this.datasource[this.length] = json;
    this.length++;

    //序列赋值
    tr.find(".sequence").text(this.length);
};

datatable.prototype.insertRow = function (index, json) {
    var tr = this.initDataGridLine(json);

    //更新界面
    index = index < 0 ? 0 : index;
    index = index >= this.length ? (this.length - 1) : index;
    if (index == 0) {
        //在该元素前插入
        var identity = this.datasource[index][this.trIdentity];
        this.datagrid.find("#" + identity + this.trIdentity).before(tr);
    }
    else {
        //在该元素前的一个元素后插入
        var identity = this.datasource[index - 1][this.trIdentity];
        this.datagrid.find("#" + identity + this.trIdentity).after(tr);
    }

    //维护数据源
    for (var i = this.length - 1; i >= index; --i) {
        this.datasource[i + 1] = this.datasource[i];
    }
    this.datasource[index] = json;

    this.length++;

    //序列赋值
    tr.find(".sequence").text(index + 1);
};

datatable.prototype.removeCurrRow = function () {
    if (this.currIdentity == -1 || this.currIdentity > this.maxIdentity) {
        return;
    }
    //查找标识[this.trIdentity]为this.currIdentity的索引
    var index = $.binarySearch(this.datasource, this.trIdentity, this.currIdentity);
    if (index != -1) {
        this.removeRow(index);
    }
};

datatable.prototype.removeRow = function (index) {
    if (index < 0 || index >= this.length) {
        return;
    }
    var identity = this.datasource[index][this.trIdentity];
    this.datagrid.find("#" + identity + this.trIdentity).remove();
    for (var i = index; i < this.length; ++i) {
        this.datasource[i] = this.datasource[i + 1];
    }

    this.length--;

    this.currIdentity = -1;
};

datatable.prototype.updateRow = function (oldjson, newjson) {
    //查找标识[this.trIdentity]为this.currIdentity的索引
    var index = $.binarySearch(this.datasource, this.trIdentity, oldjson[this.trIdentity]);
    if (index == -1) {
        return;
    }

    this.insertRow(index, newjson);
    this.removeRow(index + 1);
};

datatable.prototype.getSelecteds = function () {
    var items = [];
    for (var i = 0; i < this.datasource.length; ++i) {
        var json = this.datasource[i];
        if (json && json.checked_g) {
            items.push(this.datasource[i]);
        }
    }
    return items;
};

datatable.prototype.refresh = function () {
    this.initByUrl(this.url, this.search, 1);
};

datatable.prototype.hasChildren = function () {
    if (this.length > 0) {
        return true;
    }
    return false;
};

//-----------------------------------------------------------------------------------

datatable.prototype.onEdit = function (event) {
    this.onCustomEdit = event;
};

datatable.prototype.onDelete = function (event) {
    this.onCustomDelete = event;
};

datatable.prototype.onPager = function (event) {
    this.onCustomPager = event;
};

datatable.prototype.onLoadFooter = function (func) {
    this.datagrid.find("tfoot").remove();

    if (func) {
        var footer = $("<tfoot></tfoot>");
        this.datagrid.append(footer);
        for (var i = 0; i < this.headers.length; ++i) {
            footer.append($("<td></td>"));
        }
        func(footer);
    }
};

datatable.prototype.onSelectChanged = function (func) {
    this.selectChanged = func;
}

/*-----树形插件---------------------------------------------------------------*/
$.fn.extend({
    gtree: function (a, b, c) {
        if (typeof (a) == "object") {
            var tree = new gtree(this);
            if (a.url && a.url.length > 0) {
                tree.initByUrl(a.url);
            }
            else if (a.data.length > 0) {
                tree.initByData(a.data);
            }

            if (a.checkbox == true) {
                tree.checkbox = true;
            }

            gExtends.gtree[this.selector] = tree;
        }
        else {
            var tree = gExtends.gtree[this.selector];
            if (tree) {
                return tree[a](b, c);
            }
        }
    }
});

var gtree = function (target) {
    this.nodes = null;

    this.checkbox = false;

    target.html("");
    target.attr("class", "jstree jstree-default");
    this.container = $('<ul class="jstree-container-ul jstree-children">');
    target.append(this.container);

    var tree = this;

    this.tool = {};
    this.tool.getNode = function (nid) {
        for (var t = 0; t < tree.nodes.length; ++t) {
            var cnode = tree.nodes[t];
            if (cnode) {
                var node = tree.tool.getNodeFromNode(nid, cnode);
                if (node) {
                    return node;
                }
            }
        }
        return null;
    };

    //nid:查找节点的id；nd:在哪个节点中查找(包含其子节点)
    this.tool.getNodeFromNode = function (nid, nd) {
        if (nd.id == nid) {
            return nd;
        }
        else if (nd.children.length > 0) {
            for (var j = 0; j < nd.children.length; ++j) {
                var cnode = nd.children[j];
                if (cnode) {
                    var node = tree.tool.getNodeFromNode(nid, cnode);
                    if (node) {
                        return node;
                    }
                }
            }
        }
        return null;
    };

    this.tool.initTreeNodeChildren_D = function (node) {
        if (!node) {
            return;
        }

        node.children_d = [];
        node.children_d.push(node.id);

        $(node.children).each(function () {
            if (this) {
                var ids = tree.tool.initTreeNodeChildren_D(this);
                for (var i = 0; i < ids.length; ++i) {
                    node.children_d.push(ids[i]);
                }
            }
        });

        return node.children_d;
    };

    this.tool.initTreeNodeHtml = function (node, isselected, isopen, isend) {
        if (!node) {
            return '';
        }

        var ischecked = false;
        if (node.checked_g && node.checked_g == true) {
            ischecked = true;
        }

        var html = '';
        var endclass = '';
        if (isend) {
            endclass = 'jstree-last';
        }

        var pid = node.parentid > 0 ? node.parentid : 0;
        if (node.children.length > 0) {
            if (isopen) {
                html += '<li id="' + node.id + '" data-pid="' + pid + '" class="jstree-node jstree-open ' + endclass + '">';
            }
            else {
                html += '<li id="' + node.id + '" data-pid="' + pid + '" class="jstree-node  jstree-closed ' + endclass + '">';
            }
        }
        else {
            html += '<li id="' + node.id + '" data-pid="' + pid + '" class="jstree-node jstree-leaf ' + endclass + '">';
        }


        html += '<i class="jstree-icon jstree-ocl"></i>';

        if (tree.checkbox == true) {
            html += "<input class='tree_ckbox' type='checkbox' " + (ischecked == true ? "checked='checked'" : "") + " /><span class='lbl'></span>";
        }

        if (isselected) {
            html += '<a class="jstree-anchor jstree-clicked" href="javascript:void(0)"><i class="jstree-icon jstree-themeicon"></i><span>' + node.text + '</span></a>';
        }
        else {
            html += '<a class="jstree-anchor" href="javascript:void(0)"><i class="jstree-icon jstree-themeicon"></i><span>' + node.text + '</span></a>';
        }

        if (node.children.length > 0 && isopen) {
            html += '<ul class="jstree-children">';
            for (var i = 0; i < node.children.length - 1; ++i) {
                html += tree.tool.initTreeNodeHtml(node.children[i], false, false, false);
            }
            html += tree.tool.initTreeNodeHtml(node.children[node.children.length - 1], false, false, true);
            html += "</ul>";
        }

        html += '</li>';
        return html;
    };

    this.tool.initTreeEvent = function () {
        tree.container.find(".jstree-open>.jstree-icon").each(function () {
            if (!$(this).attr("data-event")) {
                $(this).attr("data-event", 1);
                $(this).bind('click', function () {
                    //关闭
                    var li = $(this).parent();
                    var clickId = li.attr("id");
                    var islast = li.hasClass("jstree-last");
                    var node = tree.tool.getNode(clickId);
                    if (node) {
                        var isselected = false;
                        var oldId = 0;
                        if (li.find(".jstree-clicked").length > 0) {
                            isselected = true;
                            oldId = li.find(".jstree-clicked").parent().attr("id");
                        }

                        li.replaceWith(tree.tool.initTreeNodeHtml(node, isselected, false, islast));
                        tree.tool.initTreeEvent();

                        if (oldId != 0) {
                            if (oldId != clickId && tree.onSelectChanged) {
                                tree.onSelectChanged(tree.getSelected());
                            }
                        }
                    }
                });
            }
        });

        tree.container.find(".jstree-closed>.jstree-icon").each(function () {
            if (!$(this).attr("data-event")) {
                $(this).attr("data-event", 1);
                $(this).bind('click', function () {
                    //打开
                    var li = $(this).parent();
                    var islast = li.hasClass("jstree-last");
                    var node = tree.tool.getNode(li.attr("id"));
                    if (node) {
                        var isselected = false;
                        if (li.find(".jstree-clicked").length > 0) {
                            isselected = true;
                        }

                        li.replaceWith(tree.tool.initTreeNodeHtml(node, isselected, true, islast));
                        tree.tool.initTreeEvent();
                    }
                });
            }
        });

        tree.container.find(".jstree-anchor").each(function () {
            if (!$(this).attr("data-event")) {
                $(this).attr("data-event", 1);
                $(this).bind('click', function () {
                    if ($(this).hasClass("jstree-clicked")) {
                        return;
                    }

                    tree.container.find(".jstree-clicked").removeClass("jstree-clicked");
                    $(this).addClass("jstree-clicked");

                    if (tree.onSelectChanged) {
                        tree.onSelectChanged(tree.getSelected());
                    }
                });
            }
        });

        if (tree.checkbox) {
            tree.container.find(".tree_ckbox").each(function () {
                var ckbox = $(this);
                if (!ckbox.attr("data-event")) {
                    ckbox.attr("data-event", 1);
                    ckbox.bind('change', function () {
                        var id = ckbox.parent().attr("id");
                        var node = tree.tool.getNode(id)
                        node.checked_g = ckbox[0].checked;
                    });
                }
            });
        }
    };

    this.tool.getSelectsFromNode = function (node) {
        var sels = [];
        $(node.children).each(function () {
            if (this.checked_g) {
                sels.push(this);
            }

            var csels = tree.tool.getSelectsFromNode(this);
            $(csels).each(function () {
                sels.push(this);
            });
        });

        return sels;
    }
};

gtree.prototype.initByUrl = function (url) {
    var tree = this;
    $.post(url, { ran: Math.random() }, function (r) {
        tree.initByData(r);
    });
};

gtree.prototype.initByData = function (nodes) {
    var tree = this;
    this.nodes = nodes;
    var html = '';

    $(nodes).each(function (index) {
        var isfirst = false;
        var islast = false;
        tree.tool.initTreeNodeChildren_D(this);
        if (index == 0) {
            isfirst = true;
        }
        if (index == nodes.length - 1) {
            islast = true;
        }
        html += tree.tool.initTreeNodeHtml(this, isfirst, isfirst, islast);
    });

    tree.container.html(html);

    tree.tool.initTreeEvent();


    //选中改变事件发生(因为默认选中第一项)
    if (this.onSelectChanged) {
        this.onSelectChanged(this.getSelected());
    }
};

gtree.prototype.getSelected = function () {
    var clicked = this.container.find(".jstree-clicked");
    if (clicked.length > 0) {
        var id = clicked.parent().attr("id");
        return this.tool.getNode(id);
    }
    return null;
};

gtree.prototype.getSelecteds = function () {
    var me = this;
    var sels = [];
    $(this.nodes).each(function () {
        if (this.checked_g) {
            sels.push(this);
        }

        var csels = me.tool.getSelectsFromNode(this);
        $(csels).each(function () {
            sels.push(this);
        });
    });

    return sels;
};

gtree.prototype.selectChanged = function (event) {
    this.onSelectChanged = event;
};

gtree.prototype.addRootNode = function (node) {
    node.children = [];
    this.tool.initTreeNodeChildren_D(node);
    var isselected = false;
    if (this.nodes.length == 0) {
        isselected = true;
    }
    var html = this.tool.initTreeNodeHtml(node, isselected, false, true);
    this.container.parent().find("ul.jstree-container-ul>li:last-child").removeClass("jstree-last");
    this.container.append(html);
    this.nodes.push(node);
    this.tool.initTreeEvent();
}

gtree.prototype.addChildNode = function (node) {
    node.children = [];
    var snode = this.getSelected();
    snode.children.push(node);
    this.tool.initTreeNodeChildren_D(snode);
    this.tool.initTreeNodeChildren_D(node);

    $(".jstree-clicked").parent().replaceWith(this.tool.initTreeNodeHtml(snode, true, true, $(".jstree-clicked").parent().hasClass("jstree-last")));
    this.tool.initTreeEvent();
}

gtree.prototype.removeSelectNode = function () {
    var node = this.getSelected();
    if (node.children.length > 0) {
        $.error("请先删除子节点！");
        return false;
    }

    if (this._beforeDeleteNode) {
        $.ajaxSetup({ async: false });
        var r = this._beforeDeleteNode();
        $.ajaxSetup({ async: true });

        if (!r) {
            return false;
        }
    }

    var pid = node.parentid;
    var nextid = 0;
    if (pid > 0) {
        var pnode = this.tool.getNode(pid);
        nextid = pnode.id;

        //从数据源中移除
        var targetChildren = [];
        $(pnode.children).each(function () {
            if (this != node) {
                targetChildren.push(this);
            }
        });
        pnode.children = targetChildren;
        this.tool.initTreeNodeChildren_D(pnode);
    }
    else {//根节点
        if (this.nodes[0] == node) {
            if (this.nodes[1]) {
                nextid = this.nodes[1].id;
            }
        }
        else {
            nextid = this.nodes[0].id;
        }

        //从数据源移除根节点
        var targetNodes = [];
        $(this.nodes).each(function () {
            if (this != node) {
                targetNodes.push(this);
            }
        });
        this.nodes = targetNodes;
    }


    var li = this.container.find(".jstree-clicked").parent();
    if (li.hasClass("jstree-last")) {
        li.prev().addClass("jstree-last");
    }
    li.remove();

    if (nextid != 0) {
        this.container.find("#" + nextid + ">a.jstree-anchor").click();
    }
    return true;
};

gtree.prototype.updateSelectNode = function (node) {
    var snode = this.getSelected();
    snode.text = node.text;
    this.container.find(".jstree-clicked span").text(node.text);
};

gtree.prototype.select = function (nodeid) {
    var boxs = $("#" + nodeid + " .tree_ckbox");
    if (boxs.length > 0) {
        if (!boxs[0].checked) {
            boxs[0].click();
        }
    }
    else {
        var node = this.tool.getNode(nodeid);
        node.checked_g = true;
    }
};

gtree.prototype.unSelect = function (nodeid) {
    var boxs = $("#" + nodeid + " .tree_ckbox");
    if (boxs.length > 0) {
        if (boxs[0].checked) {
            boxs[0].click();
        }
    }
    else {
        var node = this.tool.getNode(nodeid);
        node.checked_g = false;
    }
};

gtree.prototype.allUnSelect = function () {
    var me = this;
    var sels = this.getSelecteds();
    $(sels).each(function () {
        me.unSelect(this.id);
    });
};

gtree.prototype._beforeDeleteNode = null;
gtree.prototype.beforeDeleteNode = function (event) {
    this._beforeDeleteNode = event;
};

/*-----文件上传----------------------------------------------------*/
$.fn.extend({
    /*filter:image,video,application;uploadUrl:文件接收url;progressUrl:获取进度url;maxSize:最大文件大小;success:回调函数 */
    upload: function (args) {
        if (!args || !args.success) {
            return;
        }

        var me = this;
        if (me.attr("type") != "file") {
            return;
        }

        //if (args.filter) {
        //    me.attr("accept", args.filter + "/*");
        //}
        me.hide();
        var id = me.attr("id");
        var name = me.attr("name");
        me.attr("id", "file_" + id);
        me.attr("name", "upfile_g");
        var fInput = $('<input type="text" readonly="readonly" name="' + name + '" id="' + id + '" class="span11" />');
        var fSubmit = $('<label for="file_' + id + '" class="btn btn-primary btn-mini" style="margin-top:-10px;margin-left:-57px;">&nbsp;上&nbsp;传&nbsp;</label>');
        var fContainer = $("<span></span>");
        fContainer.append(fInput).append(fSubmit).insertAfter(me);

        if (!args.uploadUrl) {
            args.uploadUrl = "/Scripts/umeditor/net/fileUp.ashx?iseditor=0";
            args.progressUrl = "/Scripts/umeditor/net/fileUpProgress.ashx";
        }

        var progresskey = Math.random();
        args.uploadUrl = (args.uploadUrl + "&progresskey=" + progresskey);
        if (args.maxSize) {
            args.uploadUrl = args.uploadUrl + "&maxSize=" + args.maxSize;
        }
        if (args.filter) {
            args.uploadUrl = args.uploadUrl + "&filter=" + args.filter;
        }
        if (args.thumbs) {
            args.uploadUrl = args.uploadUrl + "&thumbs=" + args.thumbs;
        }

        me.on("change", function () {
            if ($(this).val().length == 0) {
                return;
            }

            $('<iframe name="up"  style="display:none;"></iframe>').insertBefore(me).on('load', function () {
                var r = this.contentWindow.document.body.innerHTML;

                if (r != "") {
                    var result = eval('(' + r + ')');
                    if (result.state != "1") {
                        args.isError = true;
                        me.parent().find(".remove").click();
                        $.alert(result.state);
                        return;
                    }
                    args.success(result);
                }

                $(this).unbind('load');
                $(this).remove();
            });

            var form = $('<form style="display:none;" target="up" method="post" action="' + args.uploadUrl + '" enctype="multipart/form-data"></form>');

            form.append(me);
            $("body").append(form);
            form[0].submit();

            //初始化进度条
            if (args.progressUrl && args.progressUrl.length > 0) {
                var pbar = $('<div class="progress progress-success progress-striped" style="height:4px;margin-bottom:0px;"><div class="bar" style=""></div></div>');
                pbar.css({
                    position: 'absolute',
                    top: 0,
                    left: 0,
                    zIndex: 20000000
                }).width(fInput.width() + 13).show().appendTo("body");
                var top = fInput.offset().top;
                var left = fInput.offset().left;
                pbar[0].style.left = left + "px";
                pbar[0].style.top = (top + 30) + "px";
                //更新进度条进度
                var progress = { length: 1.00, increment: 1.00, lastLength: 0.00, newIncre: 0.00 };
                var tInterval = setInterval(function () {
                    if (tInterval) {
                        progress.length += progress.increment;
                        progress.newIncre += progress.increment;
                        pbar.find(".bar").css({ width: progress.length + "%" });
                        if (progress.newIncre >= (100.00 - progress.lastLength) / 5) {
                            progress.newIncre = 0.00;
                            progress.lastLength = progress.length;
                            progress.increment = (100 - progress.length) / 100.00;
                        }
                    }
                }, 1000);

                var pInterval = setInterval(function () {
                    if (args.isError) {
                        clearInterval(pInterval);
                        pbar.remove();
                        return;
                    }
                    if (progress.length == 100) {
                        clearInterval(pInterval);
                        return;
                    }
                    $.get(args.progressUrl, { progresskey: progresskey, ran: Math.random() }, function (r) {
                        if (r >= progress.length) {
                            if (tInterval) {
                                clearInterval(tInterval);
                                tInterval = null;
                            }

                            if (r == 100) {
                                progress.length = 100;
                                var pTimeout = setTimeout(function () {
                                    clearTimeout(pTimeout);
                                    pbar.remove();
                                }, 1000);
                            }
                            pbar.find(".bar").css({ width: r + "%" });
                        }
                    });
                }, 500);
            }

            me.insertBefore(fContainer);
            form.remove();
        });
    }
});


/*-----Json数组的二分查找------------------------------------------------------*/
$.extend({
    binarySearch: function (array, key, keyv, start, end) {
        if (start == undefined || end == undefined) {
            start = 0;
            end = array.length - 1;
        }

        var length = end - start + 1;
        if (length <= 6) {
            for (var i = start; i <= end; ++i) {
                if (array[i][key] == keyv) {
                    return i;
                }
            }
            return -1;
        }
        else {
            var end_m = start + parseInt(length / 2);
            var index = $.binarySearch(array, key, keyv, start, end_m);
            if (index != -1) {
                return index;
            }
            else {
                return $.binarySearch(array, key, keyv, end_m + 1, end);
            }
        }
    }
});


/*------GOMFrameWork常用方法扩展-------------------------------------------------------------*/
//var temps = {};
$.fn.extend({
    bindEntity: function (json) {
        //Bind Value To UI
        this.find("input,select,textarea").each(function () {
            var name = $(this).attr("name");
            var tagName = $(this)[0].tagName;
            if (name != undefined) {
                var value = json[name];
                if (value != undefined) {
                    if (tagName == "TEXTAREA") {
                        if ($(this).prev().length > 0 && $(this).prev().hasClass("edui-body-container")) {
                            UM.getEditor(this.id).setContent(value);
                        }
                        else {
                            $(this).val(value);
                        }
                    }
                    else if ($(this).attr("type") == "checkbox") {
                        if (value == 1) {
                            this.checked = true;
                        }
                        else {
                            this.checked = false;
                        }
                    }
                    else {
                        if ($(this).attr("type") != "file") {
                            $(this).val(value);
                        }
                    }
                }
                else {
                    if (tagName == "SELECT") {
                        if ($(this).find("option").length > 0) {
                            $(this)[0].options[0].selected = true;
                            json[name] = $(this).val();
                            var exname = $(this).attr("data-exname");
                            if (exname != undefined && exname.length > 0) {
                                json[exname] = $(this).find("option:selected").text();
                            }
                        }
                    }
                    else if ($(this).attr("type") == "checkbox") {
                        this.checked = false;
                    }
                    else {
                        $(this).val("");
                        if (tagName == "TEXTAREA") {
                            if ($(this).prev().length > 0 && $(this).prev().hasClass("edui-body-container")) {
                                UM.getEditor(this.id).setContent("");
                            }
                        }
                    }
                }
            }
        });

        //temps[this.selector] = json;
    },
    getContext: function () {
        var newjson = {};

        //var oldjson = temps[this.selector];
        //for (var item in oldjson) {
        //    if (typeof (oldjson[item]) != "object") {
        //        newjson[item] = oldjson[item];
        //    }
        //}

        this.find("input,textarea").each(function () {
            var name = $(this).attr("name");
            if (name != undefined) {
                var tagType = $(this).attr("type");
                if (tagType == "checkbox") {
                    if (this.checked) {
                        newjson[name] = 1;
                    }
                    else {
                        newjson[name] = 0;
                    }
                }
                else if (tagType != "file") {
                    newjson[name] = $.trim($(this).val());
                }
            }
        });

        this.find("select").each(function () {
            var name = $(this).attr("name");
            if (name != undefined) {
                newjson[name] = $(this).val();
                var exname = $(this).attr("data-exname");
                if (exname != undefined && exname.length > 0) {
                    newjson[exname] = $(this).find("option:selected").text();
                }
            }
        });

        return newjson;
    }
});

$.extend({
    toJsResult: function (json) {
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
    },
    combineJson: function (oldjson, newjson, newjson2) {
        var json = {};
        for (var item in oldjson) {
            if (oldjson[item] && typeof (oldjson[item]) != "object") {
                json[item] = oldjson[item];
            }
        }
        for (var item in newjson) {
            if (newjson[item] && typeof (newjson[item]) != "object") {
                json[item] = newjson[item];
            }
        }

        if (newjson2) {
            for (var item in newjson2) {
                if (newjson2[item] && typeof (newjson2[item]) != "object") {
                    json[item] = newjson2[item];
                }
            }
        }

        return json;
    }
});

/*-------layer插件扩展-----------------------------------------------------------*/
$.extend({
    msg: function (msg) { layer.msg(msg, { icon: 0, time: 1000 }); },
    alert: function (msg) { layer.alert(msg, { icon: 0 }); },
    success: function (msg) { layer.alert(msg, { icon: 1 }); },
    error: function (msg) { layer.alert(msg, { icon: 2 }); },
    confirm: function (msg, yes, no) {
        layer.confirm(msg, { btn: ['确定', '取消'] /*按钮*/ }, function (index) {
            if (yes) {
                yes();
            }
            layer.close(index);
        }, function () {
            if (no) {
                no();
            }
        });
    }
});

$.fn.extend({
    open: function (title, yes, no, close, cancle, exoptions) {
        if (!title || (title.length && title.length == 0)) {
            title = false;
        }

        if (cancle != true) {
            cancle = false;
        }

        var options = {
            type: 1,
            title: title,
            content: this,
            yes: function (index) {
                if (yes) {
                    var v = yes(index);
                    if (v == undefined || v == true) {
                        layer.close(index);
                    }
                }
            },
            cancel: function (index) {
                if (no) {
                    var v = no(index);
                    if (v == undefined || v == true) {
                        layer.close(index);
                    }
                }
            }
        };
        if (close != undefined && close == false) {
            options.closeBtn = false;
        }

        var yesTxt = '确定';
        if (exoptions && exoptions.yesTxt) {
            yesTxt = exoptions.yesTxt;
            exoptions.yesTxt = undefined;
        }
        var noTxt = '取消';
        if (exoptions && exoptions.noTxt) {
            noTxt = exoptions.noTxt;
            exoptions.noTxt = undefined;
        }

        var btns = [];
        if (yes) {
            btns[0] = yesTxt;
        }
        if (cancle) {
            btns[btns.length] = noTxt;
        }

        var editorheight = 0;

        this.show();
        /*设置弹出层中的Editor开始*/
        this.find(".text_editor_hide").each(function () {
            var star = $(this).parent().find(".talentReqStar");
            if (!star.hasClass("starReady")) {
                UM.getEditor(this.id);
                var target = this;
                var timeout = setTimeout(function () {
                    clearTimeout(timeout);
                    var editor = $(target).prev();
                    var classnames = editor[0].className.split(' ');
                    for (var i = 0; i < classnames.length; ++i) {
                        if (classnames[i].indexOf("span") == 0) {
                            editor.removeClass(classnames[i]);
                            return;
                        }
                    }
                }, 50);
                star.remove();
                star.addClass("starReady");
                $(this).parent().append(star);
                editorheight += 20;
            }
        });
        /*设置弹出层中的Editor结束*/

        var height = this[0].clientHeight + (title == false ? 0 : 45) + 20 + editorheight;

        if (btns.length > 0) {
            options.btn = btns;
            height += 55;
        }

        options.area = [];
        if (this[0].scrollHeight > this[0].clientHeight) {
            options.area[0] = this[0].clientWidth + 20 + "px";
        }
        else {
            options.area[0] = this[0].clientWidth + "px";
        }
        options.area[1] = height + "px";

        if (options) {
            for (var it in exoptions) {
                if (exoptions[it]) {
                    options[it] = exoptions[it];
                }
            }
        }
        var win = layer.open(options);

        this.parent().parent().find(".layui-layer-btn").append("<span id='layer-error-msg'></span>");

        return win;
    },
    loading: function () {
        var tagName = this[0].tagName;
        var container = null;
        if (tagName == "TABLE") {
            container = this.parent();
        }
        else {
            container = this;
        }
        var top = container.offset().top;
        var left = container.offset().left;
        var width = container.width();
        var height = container.height();

        var ling = $('<div class="layui-layer-loading loading"><div class="layui-layer-content layui-layer-loading2"></div><span class="layui-layer-setwin"></span></div>');
        container.append(ling);
        ling[0].style.left = (left + width / 2) + "px";
        ling[0].style.top = (top + height / 2 - 32) + "px";
    },
    loaded: function () {
        var tagName = this[0].tagName;
        var container = null;
        if (tagName == "TABLE") {
            container = this.parent();
        }
        else {
            container = this;
        }
        container.find(".loading").remove();
    },
    shade: function () {/*在指定元素上创建遮罩*/
        var sdiv = $("<div></div>");
        sdiv.css({
            position: 'absolute',
            top: 0,
            left: 0,
            backgroundColor: "#808080",
            opacity: 0,
            zIndex: 20000000
        }).height(this.height()).width(this.width()).show().appendTo("body");
        var top = this.offset().top;
        var left = this.offset().left;
        sdiv[0].style.left = left + "px";
        sdiv[0].style.top = top + "px";
    }
});

var _pageParameters = { init: false };
$.extend({
    getUrlParam: function (pname) {
        if (_pageParameters.init) {
            return _pageParameters[pname];
        }

        var hrefs = location.href.split('?');
        if (hrefs.length > 1) {
            var params = hrefs[1].split('&');
            $(params).each(function () {
                var strs = this.split('=');
                if (strs.length > 1) {
                    _pageParameters[strs[0]] = strs[1];
                }
            });
        }

        _pageParameters.init = true;

        return _pageParameters[pname];
    }
});