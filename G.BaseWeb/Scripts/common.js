var authoritiesMenus = null;
function applyAuthority(menuids) {
    $.ajaxSetup({ async: false });
    $.post("/Base/LoadAuthorities", { menuids: menuids }, function (r) {
        if (r.ID == 0) {
            location.href = "error.html";
        }
        else {
            authoritiesMenus = $.toJsResult(r.Menus);
        }
    });
    $.ajaxSetup({ async: true });

    $(function () {
        $(".btn-authority").hide();

        var canEdit = "false";
        var canDelete = "false";

        $(authoritiesMenus).each(function () {
            var btnid = this.btnid;
            var btnrole = this.btnrole;
            if (btnid) {
                $("#" + btnid).show();
            }
            else {
                switch (btnrole) {
                    case "treeaddroot":
                        $(".btn-add-treeroot").show();
                        break;
                    case "treeadd":
                        $(".btn-add-treechild").show();
                        break;
                    case "treeedit":
                        $(".btn-edit-treenode").show();
                        break;
                    case "treedelete":
                        $(".btn-del-treenode").show();
                        break;
                    case "tableadd":
                        $(".btn-table-add").show();
                        break;
                    case "tableedit":
                        canEdit = "true";
                        break;
                    case "tabledelete":
                        canDelete = "true";
                        break;
                    case "tablemovemore":
                        $(".btn-more-move").show();
                        break;
                }

                if (!canEdit && !canDelete) {
                    $(".th-operate").remove();
                }
                else {
                    $(".th-operate").attr("data-option", "{edit:" + canEdit + "del:" + canDelete + "}");
                }
            }
        });
    });
}