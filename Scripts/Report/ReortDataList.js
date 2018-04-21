function ReortDataList() { }
var totalPage;
var totalRecords;
var now = new Date();
var myyear = now.getYear();
var year = (myyear > 200) ? myyear : 1900 + myyear;
var pageSize = 10;
var pageIndex = 1;
var url = "";
var data = "";
ReortDataList.prototype = {
    //查询 
    search: function Search() {
        Sub();
    }
}
function Sub() {
    if ($(".end").val() != "") {
        if ($(".start").val() > $(".end").val()) {
            alert("开始时间不能大于结束时间");
            return false;
        }
    }
    selectPage(1, pageSize, $(".start").val(), $(".end").val(), $("#selectedidstatus").val(), $("#selectedidstars").val(), 0, 0, 0, $("#selectedid").val(), 1, true);
}

function getLocalTime(nS) {
    var s = nS.replace("/Date(", "").replace(")/", "");
    return new Date(parseInt(s) * 1000).toLocaleString().replace(/:\d{1,2}$/, ' ');

    //return new Date(parseInt(s.substring(0, 10)) * 1000).toLocaleString().replace(/:\d{1,2}$/, ' ');
    //return new Date(parseInt(s.substring(0, 10)) * 1000).toLocaleString().replace(/年|月/g, "-").replace(/日/g, " ");
}
function selectPage(n, pageSize, startTime, endTime, Status, Stars, CusFlag, CastFlag, HostFlag, ReportType, oderType, first) {
    url = "/Report/GetReortDataInfoList";
    data = { pageIndex: n, pageSize: pageSize, startTime: startTime, endTime: endTime, Status: Status, Stars: Stars, CusFlag: CusFlag, CastFlag: CastFlag, HostFlag: HostFlag, ReportType: ReportType, oderType: oderType };
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
                    html += "<td style=\"cursor:pointer;\" class='am-sim'>" + da.Data[i].UserName + "</td>";
                    html += "<td>" + da.Data[i].UserCarLicense + "</td>";
                    html += "<td>" + (da.Data[i].ReportTypeName == null ? "" : da.Data[i].ReportTypeName) + "</td>";
                    html += "<td>" + da.Data[i].ReportCarLicense + "</td>";
                    html += "<td>" + da.Data[i].TextLocation + "</td>";
                    html += "<td><a onclick='img(" + JSON.stringify(da.Data[i].ImgURL) + ")'><img src=" + da.Data[i].ImgURL + " style='width:60px; height:40px;'></a></td>";
                    html += "<td>" + da.Data[i].CusRemark + "</td>";
                    html += "<td>" + (da.Data[i].ReportTime == null ? "" : da.Data[i].ReportTime) + "</td>";
                    html += "<td>" + da.Data[i].BroRemark + "</td>";
                    html += "<td>" + (da.Data[i].ReportTime == null ? "" : da.Data[i].ReportTime) + "</td>";
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
                selectPage(n, pageSize, $(".start").val(), $(".end").val(), $("#selectedidstatus").val(), $("#selectedidstars").val(), 0, 0, 0, $("#selectedid").val(), 1, true);
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
    window.location.href = "/Report/Reportex?startTime=" + $(".start").val() + "&endTime=" + $(".end").val() + "&Status=" + $("#selectedidstatus").val() + "&Stars=" + $("#selectedidstars").val() + "&CusFlag=0&CastFlag=0&HostFlag=0&ReportType=" + $("#selectedid").val() + "&oderType=1";
    selectPage(n, pageSize, $(".start").val(), $(".end").val(), $("#selectedidstatus").val(), $("#selectedidstars").val(), 0, 0, 0, $("#selectedid").val(), 1, true);
}

var ReortDataList = new ReortDataList();