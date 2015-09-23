var _shades = [];
var lastShade = null;
var lastTarget = null;
function initTraveImgShade(selector, color, func) {
    $(selector).each(function (idx) {
        if (!$(this).attr("data-event")) {
            $(this).attr("data-event", 1);
            $(this).bind("mouseover", function () {
                if (lastShade) {
                    lastShade.hide();
                    $(lastTarget).parent().find(".traveTitle").css("visibility", "visible");
                }

                var dindex = $(this).attr("data-index");
                if (dindex) {
                    lastShade = _shades[dindex];
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
                    sdiv.attr("data-index", idx);

                    $(this).parent().find(".traveTitle").css("visibility", "hidden");

                    _shades.push(sdiv);

                    lastShade = sdiv;
                    lastTarget = this;
                }
            });
        }
    });
}