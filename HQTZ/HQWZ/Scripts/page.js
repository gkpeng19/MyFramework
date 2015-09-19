var imgShades = [];
var lastShade = null;
function initTraveImgShade(selector, color) {
    var index = 0;
    $(selector).bind("mouseover", function () {
        if (lastShade) {
            lastShade.hide();
        }

        var dindex = $(this).attr("data-index");
        if (dindex) {
            imgShades[dindex].show();
            lastShade = imgShades[dindex];
        }
        else {
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

            imgShades.push(sdiv);

            $(this).attr("data-index", index);

            ++index;

            lastShade = sdiv;
        }
    });
}
