/*----------TriageApp DataGrid------------------------------*/
$("#app_dg").datagrid({
    singleSelect: true, toolbar: '#dg_toolbar', pagination: true,
    rowStyler: function (index, row) {
        if (row.StatusID == 1) {
            return "color:green;"
        }
        else {
            return "color:red;"
        }
    }
});
$("#app_dg").datagrid("getPager").html("");

loadDataGrid(1);

function loadDataGrid(pageindex) {
    $("#app_dg").datagrid("loading");
    var search = $.getJsonContext('#dg_toolbar');
    search.testrunid = currTPID;
    search.pageindex_c = pageindex;
    search.pagesize_c = 20;
    $.post("/Home/LoadTriageApps", search, function (r) {
        $("#app_dg").datagrid({ data: $.jsonToJsResult(r.data) });
        $("#app_dg").datagrid("getPager").html("<div class='pager'>" + r.pager + "</div>");
        $("#app_dg").datagrid("loaded");
    });
}

function appStatusFormatter(value, row, index) {
    if (value == 1) {
        return "<a href='" + row.LogPath + "' target='_blank' style='color:green;'>Pass</a>";
    }
    else if (value == 2) {
        return "<a href='" + row.LogPath + "' target='_blank' style='color:red;'>Fail</a>";
    }
    return "<span>Fail</span>";
}

function appActionFormatter(value, row, index) {
    return '<span class="triagebtn" onclick="triageApp(' + currTPID + ',' + row.ApplicationID + ',' + row.OSID + ')"></span>';
}

/*-----------TriageAppCase DataGrid---------------------------*/
$("#table_triage").datagrid({ singleSelect: true });

function triageApp(testrunid, applicationid, osid) {
    $.post("/Home/LoadAppCaseRunDetail", { testrunid: testrunid, applicationid: applicationid, osid: osid }, function (r) {
        var jsons = $.jsonToJsResult(r);
        $("#div_triage").show();
        $("#table_triage").datagrid({ data: jsons });
        $.layerDom("TestCaseStatus", "#div_triage");
    });
}

function testCaseStatusFormatter(value, row, index) {
    if (value == 1) {
        return "<a href='#' style='color:green;'>Pass</a>";
    }
    return "<a href='#' style='color:red;' onclick='triageTestCase()'>Triage</a>";
}

/*------------Do Triage For Case------------------------------------------*/
function triageTestCase() {

}

$("#cbx_bugtypes").change(function () {
    alert(1);
});

$("#cbx_bugtypes").combobox({
    valueField: 'value', textField: 'value', data:
        [{ value: 'Threshold Bug' }, { value: 'WinSE Bug' }, { value: 'App Issue' }, { value: 'Script Issue' }, { value: 'N/A' }],
    onSelect: function (item) {
        if (item.value != "N/A") {
            $("#txt_bugid").show();
        }
        else {
            $("#txt_bugid").hide();
        }
    }
});


