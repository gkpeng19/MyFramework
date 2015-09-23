var lastShade = null;
function initTraveImgShade(selector, color) {
    $(selector).each(function () {
        if (!$(this).attr("data-event")) {
            $(this).attr("data-event", 1);
            $(this).bind("mouseover", function () {
                if (lastShade) {
                    lastShade.remove();
                }

                var sdiv = $("<div></div>");
                sdiv.css({
                    position: 'absolute',
                    top: 0,
                    left: 0,
                    backgroundColor: color,
                    opacity: 0.6,
                    zIndex: 300
                }).height($(this).height()).width($(this).width()).show().appendTo("body");
                var top = $(this).offset().top;
                var left = $(this).offset().left;
                sdiv[0].style.left = left + "px";
                sdiv[0].style.top = top + "px";

                lastShade = sdiv;
            });
        }
    });
}
