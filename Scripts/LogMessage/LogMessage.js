function LogMessage() { }
var totalPage;
var totalRecords;
var pageSize = 10;
var pageIndex = 1;
var url = "";
var data = "";
LogMessage.prototype = {
    //查询 
    search: function Search() {
        Sub();
    }
}
function Sub() {
    selectPage(1, pageSize, $("#selectedid").val(), true);
}
function selectPage(n, pageSize,OperationType, first) {
    url = "/LogMessage/LogMessageList";
    data = { pageIndex: n, pageSize: pageSize, OperationType: OperationType };
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
            debugger;
            if (da.Result > 0) {
                log(da.Data[0].TotalCount, first)
                for (var i = 0; i < da.Data.length; i++) {
                    html += "<tr>";
                    html += "<td >" + da.Data[i].UserName + "</td>";
                    html += "<td>" + da.Data[i].OperationType + "</td>";
                    html += "<td>" + da.Data[i].ReportData.UserName + "</td>";
                    html += "<td>" + da.Data[i].CreateTime + "</td>";
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
                selectPage(n, pageSize, $("#selectedid").val(), true);
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
var LogMessage = new LogMessage();