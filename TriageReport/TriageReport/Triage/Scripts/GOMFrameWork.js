var temps = {};
$.extend({
    bindValueToUI: function (selector, json, dvalue) {
        var updates = dvalue;
        //Init Default Values
        if (updates == null) {
            updates = {};
        }
        else {
            for (var item in updates) {
                json[item] = updates[item];
            }
        }
        json.updates = updates;

        //If exists primary key value, copy it to updates 
        var pkey = json["primarykey"];
        if (pkey != undefined) {
            var pk = pkey.toLowerCase();
            updates[pk] = json[pk];
        }
        else {
            if (json.id != undefined) {
                updates.id = json.id;
            }
        }

        //Bind Value To UI
        $(selector).find("input,select,textarea").each(function () {
            var name = $(this).attr("name");
            if (name != undefined) {
                var value = json[name];
                if (value != undefined) {
                    //if ($(this).hasClass("multiselect")) {
                    //    ReInitChosen($(this), value.split(","));
                    //}
                    //else {
                    $(this).val(value);
                    //}
                }
                else {
                    var tagName = $(this)[0].tagName;
                    if (tagName == "SELECT") {
                        if ($(this).find("option").length > 0) {
                            $(this)[0].options[0].selected = true;
                            json[name] = $(this).val();
                            json[$(this).attr("data-exname")] = $(this).find("option:selected").text();
                            updates[name] = $(this).val();
                        }
                    }
                    else {
                        $(this).val("");
                    }
                }
            }
        });

        temps[selector] = json;

        ////Regest ctr value chang event, for update data source
        //jselect.find("input,textarea").change(function () {
        //    var name = $(this).attr("name");
        //    if (name != undefined) {
        //        json[name] = $.trim($(this).val());
        //        updates[name] = $.trim($(this).val());
        //    }
        //});

        //jselect.find("select").change(function () {
        //    var name = $(this).attr("name");
        //    if (name != undefined) {
        //        //if ($(this).hasClass("multiselect")) {
        //        //    var value = $(this).val();
        //        //    if (value != null && value != undefined) {
        //        //        var v = "-1";
        //        //        var vstr = "";
        //        //        for (var i = 0; i < value.length; ++i) {
        //        //            v += "," + $(this).val()[i];
        //        //            alert("Warning:No set select text!");
        //        //        }
        //        //        json[name] = v + ",-1";
        //        //        updates[name] = v + ",-1";
        //        //    }
        //        //}
        //        //else {
        //        json[name] = $(this).val();
        //        json[$(this).attr("data-exname")] = $(this).find("option:selected").text();
        //        updates[name] = $(this).val();
        //        //}
        //    }
        //});
    },
    bindSearchToUI: function (selector, json) {
        //Bind value to UI
        $(selector).find("input,select,textarea").each(function () {
            var name = $(this).attr("name");
            if (name != undefined) {
                var value = json[name];
                if (value != undefined) {
                    //if ($(this).hasClass("multiselect")) {
                    //    ReInitChosen($(this), value.split(","));
                    //}
                    //else {
                    $(this).val(value);
                    //}
                }
                else {
                    var tagName = $(this)[0].tagName;
                    if (tagName == "SELECT") {
                        $(this)[0].options[0].selected = true;
                        json[name] = $(this).val();
                    }
                    else {
                        $(this).val("");
                    }
                }
            }
        });

        temps[selector] = json;

        ////Regist ctr value change event, for update datasource
        //jselect.find("input,textarea").change(function () {
        //    var name = $(this).attr("name");
        //    if (name != undefined) {
        //        json[name] = $.trim($(this).val());
        //    }
        //});
        //jselect.find("select").change(function () {
        //    var name = $(this).attr("name");
        //    if (name != undefined) {
        //        //if ($(this).hasClass("multiselect")) {
        //        //    var value = $(this).val();
        //        //    if (value != null && value != undefined) {
        //        //        var v = "-1";
        //        //        var vstr = "";
        //        //        for (var i = 0; i < value.length; ++i) {
        //        //            v += "," + $(this).val()[i];
        //        //            alert("Warning:No set select text!");
        //        //        }
        //        //        json[name] = v + ",-1";
        //        //    }
        //        //}
        //        //else {
        //        json[name] = $(this).val();
        //        //}
        //    }
        //});
    },
    jsonToJsResult: function (json) {
        var data = null;
        if (json.length != undefined) {
            data = [];

            for (var i = 0; i < json.length; ++i) {
                var d = {};
                for (var item in json[i]) {
                    if (typeof (json[i][item]) != "object") {
                        d[item.toLowerCase()] = json[i][item];
                    }
                }
                for (var item in json[i].Collection) {
                    d[item] = json[i].Collection[item].Value;
                }
                data[i] = d;
            }
        }
        else {
            data = {};
            for (var item in json) {
                if (typeof (json[item]) != "object") {
                    data[item.toLowerCase()] = json[item];
                }
            }
            for (var item in json.Collection) {
                data[item] = json.Collection[item].Value;
            }
        }

        return data;
    },
    getJsonContext: function (selector) {
        var json = temps[selector];
        $(selector).find("input,textarea").each(function () {
            var name = $(this).attr("name");
            if (name != undefined) {
                json[name] = $.trim($(this).val());
            }
        });

        $(selector).find("select").each(function () {
            var name = $(this).attr("name");
            if (name != undefined) {
                json[name] = $(this).val();
            }
        });

        return json;
    }
});