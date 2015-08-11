var gExtends = { datagrid: {} };

$.fn.extend({
    /*----DataGrid---------------------------*/
    datagrid: function (a, b, c, d, e) {
        if (typeof (a) == "object") {
            //datagrid 初始化
            var table = new datatable(this, a.psize);
            gExtends.datagrid[this[0].id] = table;

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
var datatable = function (table, psize) {
    this.datagrid = table;
    this.psize = psize ? psize : 10;
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

    return this;
}

datatable.prototype.onCustomEdit = null;
datatable.prototype.onCustomDelete = null;
datatable.prototype.onCustomPager = null;

datatable.prototype.url = null;
datatable.prototype.search = null;
datatable.prototype.initByUrl = function (url, search, page) {
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
    search.page = page;
    search.psize = this.psize;
    var dg = this;
    $.post(url, search, function (r) {
        dg.initByData($.toJsResult(r.Data), page, r.PageCount);
    });
};

datatable.prototype.initByData = function (data, page, pagecount) {
    this.datasource = data;
    this.length = data.length;
    this.maxIdentity = data.length - 1;

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

    var dg = this;
    tbody.find("tr").each(function () {
        var identity = $(this)[0].id.split("_")[0];
        $(this).bind('click', function () {
            dg.currIdentity = identity;
        });
    });

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
        var event = this.onclick;
        if (event) {
            this.onclick = function () {
                var timeout = setTimeout(function () {
                    clearTimeout(timeout);
                    event();
                }, 50);
            };
        }
    });

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
    }, dg.psize);
    /*----Init Pager End--------------------------*/
};

datatable.prototype.initLineHtml = function (json, identity) {
    json[this.trIdentity] = identity;

    var html = "<tr id='" + identity + this.trIdentity + "'>";
    for (var h = 0; h < this.headers.length; ++h) {
        var option = this.headers[h];
        if (option.sequence) {
            html += "<td><span class='sequence'></span></td>";
        }
        else if (option.checkbox && !option.bind) {
            html += "<td><input type='checkbox' class='ckbox' /></td>";
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

                if (option.onload) {
                    vhtm = option.onload(vhtm);
                }
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

    tr.find("ckbox").bind('click', function () {
        json.checked = true;
    });

    var dg = this;
    var maxIdy = this.maxIdentity;
    tr.bind('click', function () { dg.currIdentity = maxIdy });

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
        var event = this.onclick;
        if (event) {
            this.onclick = function () {
                var timeout = setTimeout(function () {
                    clearTimeout(timeout);
                    event();
                }, 50);
            };
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
        $("#" + identity + this.trIdentity).before(tr);
    }
    else {
        //在该元素前的一个元素后插入
        var identity = this.datasource[index - 1][this.trIdentity];
        $("#" + identity + this.trIdentity).after(tr);
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

datatable.prototype.getSelected = function () {
    var items = [];
    for (var i = 0; i < this.datasource.length; ++i) {
        if (this.datasource[i].checked) {
            items.push(this.datasource[i]);
        }
    }
    return items;
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
                        $(this).val(value);
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
                if ($(this).attr("type") == "checkbox") {
                    if (this.checked) {
                        newjson[name] = 1;
                    }
                    else {
                        newjson[name] = 0;
                    }
                }
                else {
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
    updateJson: function (oldjson, newjson) {
        var json = {};
        for (var item in oldjson) {
            if (typeof (oldjson[item]) != "object") {
                json[item] = oldjson[item];
            }
        }

        for (var item in newjson) {
            if (typeof (newjson[item]) != "object") {
                var index = item.indexOf("_G");
                if (index > 0) {
                    var str = item.substring(0, index);
                    if (newjson[str]) {
                        json[item] = newjson[item];
                    }
                }
                else {
                    json[item] = newjson[item];
                }
            }
        }
        return json;
    }
});

/*-------layer插件扩展-----------------------------------------------------------*/
$.extend({
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
    open: function (title, yes, no, close, cancle) {
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
        if (close == false) {
            options.closeBtn = false;
        }
        var btns = [];
        if (yes) {
            btns[0] = "确定";
        }
        if (cancle) {
            btns[btns.length] = "取消";
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

        var height = this[0].clientHeight + 46 + 20 + editorheight;

        if (btns.length > 0) {
            options.btn = btns;
            height += 70;
        }

        options.area = [];
        if (this[0].scrollHeight > this[0].clientHeight) {
            options.area[0] = this[0].clientWidth + 20 + "px";
        }
        else {
            options.area[0] = this[0].clientWidth + "px";
        }
        options.area[1] = height + "px";

        var win = layer.open(options);

        this.parent().parent().find(".layui-layer-btn").append("<span id='layer-error-msg'></span>");

        return win;
    }
});
