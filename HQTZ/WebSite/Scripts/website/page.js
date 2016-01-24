var _shades = [];
var lastShade = null;
var lastTarget = null;
var targetImgIndex = 0;
function initTraveImgShade(selector, color, func) {
    $(selector).each(function () {
        if (!$(this).attr("data-event")) {
            $(this).attr("data-event", 1);
            $(this).bind("mouseover", function () {
                if (lastShade) {
                    lastShade.hide();
                    $(lastTarget).parent().find(".traveTitle").css("visibility", "visible");
                }

                var idx = $(this).attr("data-index");
                if (idx) {
                    lastShade = _shades[idx];
                    lastShade.show();
                    lastTarget = this;
                    $(this).parent().find(".traveTitle").css("visibility", "hidden");
                }
                else {
                    var sdiv = $("<div class='dc_content'></div>");
                    if (func) {
                        func(this, sdiv);
                    }

                    sdiv.css({
                        position: 'absolute',
                        top: $(this).offset().top,
                        left: $(this).offset().left,
                        backgroundColor: color,
                        opacity: 0.8,
                        zIndex: 300
                    }).height($(this).height() - 20).width($(this).width() - 20).show().appendTo("body");

                    $(this).attr("data-index", targetImgIndex);

                    $(this).parent().find(".traveTitle").css("visibility", "hidden");

                    _shades.push(sdiv);

                    ++targetImgIndex;

                    lastShade = sdiv;
                    lastTarget = this;
                }
            });
        }
    });
}

function initBottomFlow() {
    var mtop = $(window).height() - 41 - 3;
    $(".bottomflow").css({ top: mtop });

    var isFlow = false;
    $(window).scroll(function () {
        setTimeout(function () {
            var alltop = $(document).scrollTop() + $(window).height();
            if (alltop + 80 > $(document).height()) {
                if (isFlow) {
                    return;
                }
                isFlow = true;

                $(".flow_ctx").show();
                $(".bottomflow").animate({ top: $(window).height() - $(".bottomflow").height() }, 500);
                $(".flow_tab_icon").removeClass("fa-angle-double-down").addClass("fa-angle-double-up");
            }
            else {
                if (isFlow) {
                    $(".flow_ctx").hide();
                    $(".bottomflow").css({ top: mtop });
                    $(".flow_tab_icon").removeClass("fa-angle-double-up").addClass("fa-angle-double-down");
                    isFlow = false;
                }
            }
        }, 300);
    });

    $(".flow_tab").bind("click", function () {
        if (!isFlow) {
            isFlow = true;
            $(".flow_ctx").show();
            $(".bottomflow").animate({ top: $(window).height() - $(".bottomflow").height() }, 500);
            $(".flow_tab_icon").removeClass("fa-angle-double-down").addClass("fa-angle-double-up");
        }
        else {
            $(".bottomflow").animate({ top: mtop }, 500);
            $(".flow_ctx").hide(500);
            $(".flow_tab_icon").removeClass("fa-angle-double-up").addClass("fa-angle-double-down");
            isFlow = false;
        }
    });
}

function IsValidEmail(email) {
    var reg = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;
    return reg.test(email);
}

function LoginOut() {
    $.confirm("确定要退出系统吗？", function () {
        location.href = "/Base/LoginOut";
    });
}

function toShop(src, targetUrl) {
    if (!targetUrl) {
        targetUrl = '/';
    }
    $.ajaxSetup({ async: false });
    $.get("/Member/LoadCurrUserInfo", { ran: Math.random() }, function (r) {
        var url = null;
        if (r.Phone.length > 0) {
            var ppp = "su=" + r.Phone + "&sp=" + r.Pwd;
            ppp = new Base64().encode(ppp);
            url = "http://123.57.153.47:8099/ExAccount/Login?ppp=" + ppp + "&returnUrl=" + targetUrl;
        }
        else {
            url = "http://123.57.153.47:8099" + targetUrl;
        }

        if (src) {
            $(src).attr("href", url);
        }
        else {
            location.href = url;
        }
    });
    $.ajaxSetup({ async: true });
}

function initOrderHtml(orders) {
    var html = "";
    $(orders).each(function (i) {
        var planClass = "plan" + (i % 4 + 1);
        if (this.OStatus == 2 || this.OStatus == 4) {
            planClass += " planDisabled";
        }

        var status = "";
        if (this.OStatus == 0) {
            status = "<span class='label label-info'>待处理</span>";
        }
        else if (this.OStatus == 1) {
            status = "<span class='label label-success' style='background:#7db553;'>预订成功</span>";
        }
        else if (this.OStatus == 3) {
            status = "<span class='label label-success'>支付成功</span>";
        }
        else if (this.OStatus == 2) {
            status = "<span class='label label-warning'>预订失败</span>";
        }
        else if (this.OStatus == 4) {
            status = "<span class='label'>已退订</span>";
        }

        html += '<div class="plan ' + planClass + '"><div class="priceheader"><p>' + this.RoomName_G + "  " + status + '</p></div>' +
    '<div class="price">￥' + this.AllPrice_G + ' <span>/ 共 ' + this.BookDays_G + ' 天</span></div><ul>' +
        '<li>入住日期 <b>' + this.BookStartTime_G + '</b></li>' +
        '<li>退房日期 <b>' + this.BookEndTime_G + '</b></li>' +
        '<li>预订日期 <b>' + this.CreateOn_G + '</b> </li></ul>';

        if (this.CanCancelBook_G == 1) {
            html += ' <a class="signup cancelBook" href="javascript:void(0)" data-id="' + this.ID + '">&nbsp;退 订&nbsp;</a> ';
        }
        else {
            if (this.OStatus == 2) {//预订失败
                html += ' <a class="signup deleteBook" style="background:#f89406;" href="javascript:void(0)" onclick="deleteOrder(this,' + this.ID + ')">&nbsp;删 除&nbsp;</a> ';
                html += ' <a class="signup" style="background-color:white;color:#404a58;border:1px solid #c0c4cd;" href="javascript:void(0)" onclick="$.alert(\'' + this.Remark + '\')">查看详情</a> ';
            }
            else if (this.OStatus == 4) {
                html += ' <a class="signup" style="background-color:gray;">&nbsp;退 订&nbsp;</a> ';
            }
        }

        if (this.OStatus == 0) {
            html += ' <a class="signup cancelBook" href="javascript:void(0)" data-id="' + this.ID + '">&nbsp;退 订&nbsp;</a> ';
            html += ' <a class="signup remarkBook" style="background-color:white;color:#404a58;border:1px solid #c0c4cd;" href="javascript:void(0)" data-id="' + this.ID + '" data-remark="' + (this.Remark ? this.Remark : '') + '">修改备注</a> ';
        }
        else if (this.OStatus == 1 || this.OStatus == 3) {
            html += ' <a class="signup viewRemark" style="background-color:white;color:#404a58;border:1px solid #c0c4cd;" href="javascript:void(0)" data-id="' + this.ID + '" data-remark="' + (this.Remark ? this.Remark : '') + '">查看备注</a> ';
        }
        html += "</div>";
    });
    return html;
}

function deleteOrder(src, oid) {
    if (oid) {
        $.confirm("确定要删除该订单吗？", function () {
            $.post("/Member/DeleteOrder", { orderid: oid, ran: Math.random() }, function (r) {
                if (r == 1) {
                    $.success("删除订单成功。");
                    $(src).parent().remove();
                }
                else {
                    $.error("删除订单失败，请重试！");
                }
            });
        });
    }
}


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