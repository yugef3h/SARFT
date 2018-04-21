function UsersList() { }
var totalPage;
var totalRecords;
var pageSize = 10;
var pageIndex = 1;
var url = "";
var data = "";
UsersList.prototype = {
    //查询
    search: function Search() {
        Sub();
    },
    saveUser: function SaveUser(ConfigJson) {
        jQuery.ajax({
            url: "/Users/AlterUsers",
            type: "POST",
            dataType: "json",
            async: true,
            data: { ConfigJson: ConfigJson },
            success: function (resultInfo) {
                if (resultInfo.Result > 0) {
                    alert(resultInfo.Desc);
                    window.location.href = "/Users/Index";
                }
                else {
                    alert(resultInfo.Desc);
                }
            }
        });
    }
}
function Sub() {
    debugger;
    selectPage(1, pageSize, $("#userName").val(), $("#selectedid").val(), true);
}
function selectPage(n, pageSize, userName, RoleID, first) {
    url = "/Users/UsersList";
    data = { pageIndex: n, pageSize: pageSize, userName: userName, RoleID: RoleID };
    first = first == undefined ? false : first;
    jQuery.ajax({
        async: false,
        url: url,
        type: "POST",
        dataType: "json",
        data: data,
        success: function (data) {
            var html = "";
            var da = eval(data);
            if (da.TotalCount > 0) {
                log(da.TotalCount, first)
                var exprice;
                for (var i = 0; i < da.Data.length; i++) {
                    html += "<tr>";
                    html += "<td style=\"cursor:pointer;\" class='am-sim'>" + da.Data[i].UserName + "</td>";
                    html += "<td>" + da.Data[i].Phone + "</td>";
                    if (da.Data[i].RoleID == 1) {
                        exprice = "客服";
                    } else if (da.Data[i].RoleID == 2) {
                        exprice = "导播";
                    }
                    else if (da.Data[i].RoleID == 3) {
                        exprice = "主持人";
                    }
                    else if (da.Data[i].RoleID == 4) {
                        exprice = "管理员";
                    }
                    html += "<td class='table-author am-hide-sm-only'>" + exprice + "</td>";
                    html += "<td> <div class=\"am-btn-toolbar\"><div class=\"am-btn-group am-btn-group-xs\"> ";
                    html += "<button type='button' class='am-btn am-btn-default am-btn-xs am-text-secondary' onclick='updateUsers(" + da.Data[i].Id + ")'><span class='am-icon-pencil-square-o'></span> 编辑</button>";
                    html += "<button type='button' class='am-btn am-btn-default am-btn-xs am-text-danger am-hide-sm-only' onclick='delUsers(" + da.Data[i].Id + ")'><span class='am-icon-trash-o'></span> 删除</button>";
                    html += "<button type='button' class='am-btn am-btn-default am-btn-xs am-text-danger am-hide-sm-only' onclick='updatePwd(" + da.Data[i].Id + ")'><span class='am-icon-refresh'></span> 重置密码</button></div></div></td>";
                    html += "</div></div></td>";
                    html += "</tr>";
                }
                $("#tables").html(html);

            } else {
                log(0, first);
                $("#tables").html("");
                //alert("暂无数据");
                $.AMUI.progress.done();
            }
        }
    });
}
function log(TotalCount, first) {
    if (TotalCount >= 0) {
        totalRecords = TotalCount;
        totalPage = Math.ceil(totalRecords / pageSize);
        //debugger;  调试
        var pageNo = pageIndex;
        if (!pageNo) {
            pageNo = 1;
        }
        //生成分页
        //有些参数是可选的，比如lang，若不传有默认值
        kkpager.generPageHtml({
            pno: pageNo,
            //总页码
            total: totalPage,
            //总数据条数
            totalRecords: totalRecords,
            mode: 'click',//默认值是link，可选link或者click
            click: function (n) {
                //alert(n);
                // do something
                //手动选中按钮
                this.selectPage(n);
                pageIndex = n;
                selectPage(n, pageSize, $("#userName").val(), $("#selectedid").val(), true);
                return false;
            }


        }, first);
    }
}
function GetParameter(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
//删除用户
function delUsers(obj) {
    $('#delteUser').modal({
        relatedTarget: this,
        onConfirm: function (e) {
            jQuery.ajax({
                async: false,
                url: "/Users/DelUser",
                type: "POST",
                dataType: "json",
                data: { id: obj },
                success: function (resultInfo) {
                    alert(resultInfo.Desc);
                    selectPage(n, pageSize, $("#userName").val(), $("#selectedid").val(), true);
                }
            });
        },
        onCancel: function (e) {

        }
    })
    $("#delteUser").show();

}
//编辑用户
function updateUsers(id) {
    window.location.href = "/AlterUsersInfo/" + id;
}
//重置密码
function updatePwd(id) {
    $('#updateUser').modal({
        relatedTarget: this,
        onConfirm: function (e) {
            jQuery.ajax({
                async: false,
                url: "/Users/RefreshUser",
                type: "POST",
                dataType: "json",
                data: { id: id },
                success: function (resultInfo) {
                    alert(resultInfo.Desc);
                    selectPage(n, pageSize, $("#userName").val(), $("#selectedid").val(), true);
                }
            });
        },
        onCancel: function (e) {

        }
    })
    $("#updateUser").show();
}
function GetParameter(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
//导出Excel
function method() {
    window.location.href = "/Flow/flowex?sim=" + $(".carid").val() + "&state=" + $("#selectedid").val() + "";
    selectPage(1, pageSize, $(".carid").val(), $("#selectedid").val(), true);
}

var UsersList = new UsersList();