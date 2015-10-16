var _shades = [];
var lastShade = null;
var lastTarget = null;
function initTraveImgShade(selector, color, func) {
    var dindex = 0;
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
                }
                else {
                    var sdiv = $("<div class='dc_content'></div>");
                    if (func) {
                        func(this, sdiv);
                    }

                    sdiv.css({
                        position: 'absolute',
                        top: 0,
                        left: 0,
                        backgroundColor: color,
                        opacity: 0.8,
                        zIndex: 300
                    }).height($(this).height() - 20).width($(this).width() - 20).show().appendTo("body");
                    var top = $(this).offset().top;
                    var left = $(this).offset().left;
                    sdiv[0].style.left = left + "px";
                    sdiv[0].style.top = top + "px";
                    $(this).attr("data-index", dindex);

                    $(this).parent().find(".traveTitle").css("visibility", "hidden");

                    _shades.push(sdiv);

                    ++dindex;

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