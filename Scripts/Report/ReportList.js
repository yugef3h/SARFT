function ReportList() { }
var totalPage;
var totalRecords;
var pageSize = 10;
var pageIndex = 1;
var url = "";
var data = "";
ReportList.prototype = {
    //查询 
    search: function Search() {
        Sub();
    },
    saveReport: function SaveReport(ConfigJson) {
        jQuery.ajax({
            url: "/Report/AlterReport",
            type: "POST",
            dataType: "json",
            async: true,
            data: { ConfigJson: ConfigJson },
            success: function (resultInfo) {
                if (resultInfo.Result > 0) {
                    alert(resultInfo.Desc);
                    window.location.href = "/Report/Index";
                }
                else {
                    alert(resultInfo.Desc);
                }
            }
        });
    }
}
function Sub() {
    selectPage(1, pageSize, true);
}
function selectPage(n, pageSize, first) {
    url = "/Report/ReportList";
    data = { pageIndex: n, pageSize: pageSize };
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
                for (var i = 0; i < da.Data.length; i++) {
                    html += "<tr>";
                    html += "<td style=\"cursor:pointer;\" class='am-sim'>" + da.Data[i].ReportName + "</td>";
                    html += "<td>" + da.Data[i].Remark + "</td>";
                    //html += "<td> <div class=\"am-btn-toolbar\"><div class=\"am-btn-group am-btn-group-xs\"> ";
                    //html += "<button type='button' class='am-btn am-btn-default am-btn-xs am-text-secondary' onclick='updateReport(" + da.Data[i].Id + ")'><span class='am-icon-pencil-square-o'></span> 编辑</button> ";
                    //html += "<button type='button' class='am-btn am-btn-default am-btn-xs am-text-danger am-hide-sm-only' onclick='delReport(" + da.Data[i].Id + ")'><span class='am-icon-trash-o'></span> 删除</button></div></div></td>";
                    //html += "</div></div></td>";
                    html += "<td><button type='button' class='am-btn am-btn-default am-btn-xs am-text-secondary' onclick='updateReport(" + da.Data[i].Id + ")'><span class='am-icon-pencil-square-o'></span> 编辑</button> </td>";
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
//删除违章
function delReport(obj) {
    $('#delteReport').modal({
        relatedTarget: this,
        onConfirm: function (e) {
            jQuery.ajax({
                async: false,
                url: "/Report/DelReport",
                type: "POST",
                dataType: "json",
                data: { id: obj },
                success: function (resultInfo) {
                    alert(resultInfo.Desc);
                    selectPage(pageIndex, pageSize, $(".carid").val(), $("#selectedid").val(), true);
                }
            });
        },
        onCancel: function (e) {

        }
    })
    $("#delteReport").show();
}
//编辑违章
function updateReport(id) {
    window.location.href = "/AlterReport/" + id;
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
                selectPage(n, pageSize, $(".carid").val(), $("#selectedid").val(), true);
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
//导出Excel
function method() {
    window.location.href = "/Flow/flowex?sim=" + $(".carid").val() + "&state=" + $("#selectedid").val() + "";
    selectPage(1, pageSize, $(".carid").val(), $("#selectedid").val(), true);
}

var ReportList = new ReportList();