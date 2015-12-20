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

function IsValidEmail(email) {
    var reg = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;
    return reg.test(email);
}