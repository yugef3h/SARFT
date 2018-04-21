function HostManInfoList() { }
var totalPage;
var totalRecords;
var now = new Date();
var myyear = now.getYear();
var year = (myyear > 200) ? myyear : 1900 + myyear;
var pageSize = 10;
var pageIndex = 1;
var url = "";
var data = "";
var ReportType = 0;
HostManInfoList.prototype = {
    //查询 
    search: function Search() {
        Sub();
    },
    //查询数据 
    interval: function Interval() {
        InterValue($("#hidcount").val());
    }, saveReport: function SaveReport(Id, AwardRemark) {
        jQuery.ajax({
            url: "/ReportDataWebApi/GetRewards",
            type: "POST",
            dataType: "json",
            async: true,
            data: { Id: Id, AwardRemark: AwardRemark },
            success: function (resultInfo) {
                if (resultInfo.Result > 0) {
                    alert(resultInfo.Desc);
                    $(".Remark").click();
                    selectPage(1, pageSize, $(".start").val(), $(".end").val(), 8, 7, 0, 0, 0, 0, 1, true);
                }
                else {
                    alert(resultInfo.Desc);
                }
            }
        });
    }
}
//定时加载数据
function InterValue(count) {
    jQuery.ajax({
        async: false,
        url: "/ReportDataWebApi/GetHostManInfoList",
        type: "POST",
        dataType: "json",
        data: { pageIndex: 1, pageSize: 10, startTime: "", endTime: "", Status: 8, Stars: 7, CusFlag: 0, CastFlag: 0, HostFlag: 0, ReportType: 0, oderType: 1 },
        success: function (resultInfo) {
            if (resultInfo.Result > 0) {
                if (resultInfo.Data.TotalCount > count) {
                    $("#hidcount").val(resultInfo.Data.TotalCount);
                    notify(resultInfo.Data.TotalCount - count);
                }
            }
        }
    });
}
function Sub() {
    if ($(".end").val() != "") {
        if ($(".start").val() > $(".end").val()) {
            alert("开始时间不能大于结束时间");
            return false;
        }
    }
    selectPage(1, pageSize, $(".start").val(), $(".end").val(), 8, 7, 0, 0, 0, 0, 1, true);
}
function selectPage(n, pageSize, startTime, endTime, Status, Stars, CusFlag, CastFlag, HostFlag, ReportType, oderType, first) {
    if ($("#selectedid").val() != "" && $("#selectedid").val() != undefined) {
        ReportType = $("#selectedid").val();
    }
    url = "/ReportDataWebApi/GetHostManInfoList";
    data = { pageIndex: n, pageSize: pageSize, startTime: startTime, endTime: endTime, Status: Status, Stars: Stars, CusFlag: CusFlag, CastFlag: CastFlag, HostFlag: HostFlag, ReportType: ReportType, oderType: oderType };
    first = first == undefined ? false : first;
    jQuery.ajax({
        async: false,
        url: url,
        type: "POST",
        dataType: "json",
        data: data,
        success: function (resultInfo) {
            var html = "";
            if (resultInfo.Result > 0) {
                log(resultInfo.Data.TotalCount, first);
                var da = resultInfo.Data;
                for (var i = 0; i < da.Data.length; i++) {
                    if (n == 1 && i == 0 && ReportType == "" && startTime == "" && endTime == "") {
                        $("#hidcount").val(resultInfo.Data.TotalCount);
                    }
                    html += "<tr>";
                    if (da.Data[i].HostFlag == 1) {
                        html += "<td><span class=\"am-icon-folder am-icon-sm\" style=\"color:tan;\"></span></td>";
                    }
                    else {
                        html += "<td><span class=\"am-icon-folder-open am-icon-sm\"></span></td>";
                    }
                    html += "<td><a onclick='img(" + JSON.stringify(da.Data[i].ImgURL) + ")'><img src=\"" + da.Data[i].ImgURL + "\" height=\"50\" width=\"65\"/></a></td>";
                    html += "<td>&nbsp;" + (da.Data[i].UserCarLicense == null ? "" : da.Data[i].UserCarLicense) + "</td>";
                    html += "<td>" + da.Data[i].ReportTypeName + "</td>";
                    html += "<td>&nbsp;" + (da.Data[i].ReportCarLicense == null ? "" : da.Data[i].ReportCarLicense) + "</td>";

                    html += "<td class='table-author am-hide-sm-only'>" + da.Data[i].TextLocation + "</td>";
                    html += "<td class='table-author am-hide-sm-only'>" + da.Data[i].BroRemark + "</td>";
                    html += "<td>" + (da.Data[i].ReportTime == null ? "" : da.Data[i].ReportTime) + "</td>";
                    html += "<td>";
                    if (da.Data[i].Status == 3) {
                        html += "<button type='button' class='am-btn am-btn-primary am-btn-md am-radius' onclick='recommend(" + da.Data[i].Id + ")'>未播报</button>&nbsp;";
                    }
                    else if (da.Data[i].Status == 4) {
                        html += "<button type='button' class='am-btn am-btn-default am-btn-md am-radius'>已播报</button>&nbsp;";
                    }
                    if (da.Data[i].Stars == 1) {
                        html += "<button type='button' class='am-btn am-btn-primary am-btn-md am-radius' onclick='Rewards(" + da.Data[i].Id + ",1)'>未通知</button>&nbsp;";
                    }
                    else if (da.Data[i].Stars == 2) {
                        html += "<button type='button' class='am-btn am-btn-default am-btn-md am-radius' onclick='Rewards(" + da.Data[i].Id + ",2)'>已通知</button>&nbsp;";
                    }
                    html += "<button type='button' class='am-btn am-btn-primary am-btn-md am-radius' onclick='map(" + da.Data[i].Id + ")'>地图</button>&nbsp;";
                    html += "</td>";
                    html += "</tr>";
                }
                $("#tables").html(html);
            } else {
                log(0, first);
                $("#tables").html("");
                //alert("暂无数据");
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
                selectPage(n, pageSize, $(".start").val(), $(".end").val(), 8, 7, 0, 0, 0, 0, 1, true);
                return false;
            }


        }, first);
    }
}
//播报
function recommend(obj) {
    jQuery.ajax({
        url: "/ReportDataWebApi/GetRecommend",
        type: "POST",
        dataType: "json",
        async: false,
        data: { ID: obj },
        success: function (resultInfo) {
            if (resultInfo.Result > 0) {
                alert(resultInfo.Desc);
                selectPage(pageIndex, pageSize, $(".start").val(), $(".end").val(), 8, 7, 0, 0, 0, 0, 1, true);
            }
            else {
                alert(resultInfo.Desc);
            }
        }
    });
}
//奖励
function Rewards(obj, objid) {
    jQuery.ajax({
        url: "/ReportDataWebApi/GetNotify",
        type: "POST",
        dataType: "json",
        async: false,
        data: { ID: obj },
        success: function (resultInfo) {
            if (resultInfo != null) {
                $("#AwardRemark").val("");
                $("#hidID").val(resultInfo.Id);
                debugger;
                if (objid < 2) {
                    $("#Award").show();
                } else {
                    $("#Award").hide();
                    $("#AwardRemark").val(resultInfo.AwardRemark);
                }

                $(".Remark").click();
            }
            else {
                alert("获取数据失败");
            }
        }
    });
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

var HostManInfoList = new HostManInfoList();