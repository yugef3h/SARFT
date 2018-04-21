function BroadCastInfoList() { }
var totalPage;
var totalRecords;
var pageSize = 10;
var pageIndex = 1;
var TotalCount = 0;
var TotalCount2 = 0;
BroadCastInfoList.prototype = {
    //加载全部数据
    getInfoList: function GetInfoList() {
        Sub(1);
    }, //查询数据 
    interval: function Interval() {
        InterValue();
    },
}
//定时加载数据
function InterValue() {
    if (TotalCount2 > TotalCount) {
        TotalCount = TotalCount2;
    }
    jQuery.ajax({
        async: false,
        url: "/ReportDataWebApi/GetBroadCastInfoList",
        type: "POST",
        dataType: "json",
        data: { pageIndex: pageIndex, pageSize: pageSize, ReportType: 0, CastFlag: 0, Status: 7, start: "", end: "" },
        success: function (resultInfo) {
            var html = "";
            if (resultInfo.Data.TotalCount > TotalCount) {
                var count = resultInfo.Data.TotalCount - TotalCount;
                TotalCount = resultInfo.Data.TotalCount;
                notify(count);


            }
        }
    });
}
//查询
function Sub(index) {

    if (index == 1) {
        pageIndex = 1;
    }
    var ReportType = $("#ReportType").val();
    var CastFlag = $("#CastFlag").val();
    var Status = $("#SelStatus").val();
    var start = $("#start").val();
    var end = $("#end").val();
    selectPage(pageIndex, pageSize, ReportType, CastFlag, Status, start, end, true);
}
//定时查询
function Sub2() {


    pageIndex = 1;

    selectPage(pageIndex, pageSize, 0, 0, 7, "", "", true);
}

function selectPage(n, pageSize, ReportType, CastFlag, Status, start, end, first) {
    first = first == undefined ? true : first;
    jQuery.ajax({
        url: "/ReportDataWebApi/GetBroadCastInfoList",
        type: "POST",
        dataType: "json",
        async: false,
        data: { pageIndex: n, pageSize: pageSize, ReportType: ReportType, CastFlag: CastFlag, Status: Status, start: start, end: end },
        success: function (resultInfo) {
            var html = "";
            if (resultInfo.Result > 0) {
                log(resultInfo.Data.TotalCount, first);
                var da = resultInfo.Data;
                for (var i = 0; i < da.Data.length; i++) {
                    if (ReportType == 0 && CastFlag == 0 && Status == 7 && end == "" && start == "") {
                        TotalCount2 = resultInfo.Data.TotalCount;
                    }
                    html += "<tr>";
                    if (da.Data[i].CastFlag == 1) {
                        html += "<td><span class=\"am-icon-folder am-icon-sm\" style=\"color:tan;\"></span></td>";
                    }
                    else {
                        html += "<td><span class=\"am-icon-folder-open am-icon-sm\"></span></td>";
                    }
                    html += "<td><a href=\"javascript:void()\" class='am-add' title='" + da.Data[i].Id + "'>" + (da.Data[i].UserName == "" ? da.Data[i].Phone : da.Data[i].UserName) + "</a></td>";
                    html += "<td>&nbsp;" + (da.Data[i].UserCarLicense == null ? "" : da.Data[i].UserCarLicense) + "</td>";
                    html += "<td>" + da.Data[i].ReportTypeName + "</td>";
                    html += "<td>&nbsp;" + (da.Data[i].ReportCarLicense == null ? "" : da.Data[i].ReportCarLicense) + "</td>";
                    html += "<td><a onclick='ShowImg(" + JSON.stringify(da.Data[i].ImgURL) + ")'><img src=\"" + da.Data[i].ImgURL + "\" height=\"50\" width=\"65\"/></a></td>";
                    html += "<td>" + da.Data[i].ReportTime + "</td>";
                    //if (da.Data[i].Stars == 1) {
                    //    html += "<td>尚未评分</td>";
                    //}
                    //else if (da.Data[i].Stars == 2)
                    //{
                    //    html += "<td><span class=\"am-icon-star am-icon-sm\" style=\"color:forestgreen;\"></span></td>";
                    //}
                    //else if (da.Data[i].Stars == 3) {
                    //    html += "<td><span class=\"am-icon-star am-icon-sm\" style=\"color:forestgreen;\"></span><span class=\"am-icon-star am-icon-sm\" style=\"color:forestgreen;\"></span></td>";
                    //}
                    //else if (da.Data[i].Stars == 4) {
                    //    html += "<td><span class=\"am-icon-star am-icon-sm\" style=\"color:forestgreen;\"></span><span class=\"am-icon-star am-icon-sm\" style=\"color:forestgreen;\"></span><span class=\"am-icon-star am-icon-sm\" style=\"color:forestgreen;\"></span></td>";
                    //}
                    //else if (da.Data[i].Stars == 5) {
                    //    html += "<td><span class=\"am-icon-star am-icon-sm\" style=\"color:forestgreen;\"></span><span class=\"am-icon-star am-icon-sm\" style=\"color:forestgreen;\"></span><span class=\"am-icon-star am-icon-sm\" style=\"color:forestgreen;\"></span><span class=\"am-icon-star am-icon-sm\" style=\"color:forestgreen;\"></span></td>";
                    //}
                    //else {
                    //    html += "<td><span class=\"am-icon-star am-icon-sm\" style=\"color:forestgreen;\"></span><span class=\"am-icon-star am-icon-sm\" style=\"color:forestgreen;\"></span><span class=\"am-icon-star am-icon-sm\" style=\"color:forestgreen;\"></span><span class=\"am-icon-star am-icon-sm\" style=\"color:forestgreen;\"></span><span class=\"am-icon-star am-icon-sm\" style=\"color:forestgreen;\"></span></td>";
                    //}

                    html += "<td  class='am-add' title='" + da.Data[i].Id + "'><div class='am-icon-search' style=\"cursor:pointer;\"></td>";
                    html += "<td>  ";
                    if (da.Data[i].Status == 2) {
                        html += "<button type=\"button\" class=\"am-btn am-btn-primary am-btn-md am-radius\" onclick=\"updateReportData(" + da.Data[i].Id + ",1)\"> 编辑</button> ";
                        html += "<button type=\"button\" class=\"am-btn am-btn-danger  am-btn-md am-radius\" onclick=\"doRecoment(" + da.Data[i].Id + ")\"> 推荐</button></td>; ";
                    }
                    else if (da.Data[i].Status == 3 && da.Data[i].Stars <= 1) {
                        html += "<button type=\"button\" class=\"am-btn am-btn-primary am-btn-md am-radius\" onclick=\"updateReportData(" + da.Data[i].Id + ",1)\"> 编辑</button> ";
                        html += "<button type=\"button\" class=\"am-btn am-btn-danger  am-btn-md am-radius\" onclick=\"cancelRecoment(" + da.Data[i].Id + ")\"> 取消推荐</button></td>; ";
                    }
                    else {
                        html += "<button type=\"button\" class=\"am-btn am-btn-primary am-btn-md am-radius\" onclick=\"updateReportData(" + da.Data[i].Id + ",2)\"> 查看</button> </td>; ";
                    }

                    html += "</tr>";
                }
                $("#tables").html(html);

            } else {

                log(0, first);
                $("#tables").html("");
            }

            $(".am-add").click(function () {
                var ID = $(this).attr("title");
                jQuery.ajax({
                    async: false,
                    url: "/ReportDataWebApi/GetReportDataInfo",
                    type: "POST",
                    dataType: "json",
                    data: { ID: ID },
                    success: function (data) {
                        var da = data.Data;
                        var htmlcar = "";
                        htmlcar += "<tr>";
                        htmlcar += " <td>举报人：" + (da.UserName == "" ? da.Phone : da.UserName) + "</td>";
                        htmlcar += " <td>举报人车牌号：" + da.UserCarLicense + "</td>";
                        htmlcar += "</tr>";
                        htmlcar += "<tr>";
                        htmlcar += " <td>举报类型：" + da.ReportTypeName + "</td>";
                        htmlcar += " <td>被举报人车牌号：" + da.ReportCarLicense + "</td>";
                        htmlcar += "</tr>";
                        htmlcar += "<tr>";
                        htmlcar += " <td colspan=\"2\">上报时间：" + da.ReportTime + "</td>";
                        //   htmlcar += " <td>举报人姓名：" + da.Name + "</td>";
                        //if (da.Stars == 1) {
                        //    htmlcar += "<td>评分：尚未评分</td>";
                        //}
                        //else if (da.Stars == 2) {
                        //    htmlcar += "<td>评分：<span class=\"am-icon-star am-icon-sm\" style=\"color:forestgreen;\"></span></td>";
                        //}
                        //else if (da.Stars == 3) {
                        //    htmlcar += "<td>评分：<span class=\"am-icon-star am-icon-sm\" style=\"color:forestgreen;\"></span><span class=\"am-icon-star am-icon-sm\" style=\"color:forestgreen;\"></span></td>";
                        //}
                        //else if (da.Stars == 4) {
                        //    htmlcar += "<td>评分：<span class=\"am-icon-star am-icon-sm\" style=\"color:forestgreen;\"></span><span class=\"am-icon-star am-icon-sm\" style=\"color:forestgreen;\"></span><span class=\"am-icon-star am-icon-sm\" style=\"color:forestgreen;\"></span></td>";
                        //}
                        //else if (da.Stars == 5) {
                        //    htmlcar += "<td>评分：<span class=\"am-icon-star am-icon-sm\" style=\"color:forestgreen;\"></span><span class=\"am-icon-star am-icon-sm\" style=\"color:forestgreen;\"></span><span class=\"am-icon-star am-icon-sm\" style=\"color:forestgreen;\"></span><span class=\"am-icon-star am-icon-sm\" style=\"color:forestgreen;\"></span></td>";
                        //}
                        //else {
                        //    htmlcar += "<td>评分：<span class=\"am-icon-star am-icon-sm\" style=\"color:forestgreen;\"></span><span class=\"am-icon-star am-icon-sm\" style=\"color:forestgreen;\"></span><span class=\"am-icon-star am-icon-sm\" style=\"color:forestgreen;\"></span><span class=\"am-icon-star am-icon-sm\" style=\"color:forestgreen;\"></span><span class=\"am-icon-star am-icon-sm\" style=\"color:forestgreen;\"></span></td>";
                        //}
                        htmlcar += "</tr>";
                        htmlcar += "<tr>";
                        htmlcar += " <td colspan=\"2\">位置信息：" + da.TextLocation + "</td>";
                        htmlcar += "</tr>";
                        htmlcar += "<tr>";
                        htmlcar += " <td colspan=\"2\">信息说明：" + da.CusRemark + "</td>";
                        htmlcar += "</tr>";
                        htmlcar += "<tr>";
                        htmlcar += " <td colspan=\"2\">电台反馈：" + da.BroRemark + "</td>";
                        htmlcar += "</tr>";
                        $("#oinfo").html(htmlcar);
                        $("#doc-modal-2").modal('open');
                        //重新加载
                        Sub(2);
                    }
                });
            });
        }
    });
}

function log(TotalCount, first) {


    if (TotalCount >= 0) {
        totalRecords = TotalCount;
        totalPage = Math.ceil(totalRecords / pageSize);
        //debugger;
        // var pageNo = GetParameter("pno");
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
                // this.selectPage(n);
                pageIndex = n;
                var ReportType = $("#ReportType").val();
                var CastFlag = $("#CastFlag").val();
                var Status = $("#SelStatus").val();
                var start = $("#start").val();
                var end = $("#end").val();
                selectPage(pageIndex, pageSize, ReportType, CastFlag, Status, start, end, true);
                return false;
            }

            , lang: {
                firstPageText: '首页',
                firstPageTipText: '首页',
                lastPageText: '尾页',
                lastPageTipText: '尾页',
                prePageText: '上一页',
                prePageTipText: '上一页',
                nextPageText: '下一页',
                nextPageTipText: '下一页',
                totalPageBeforeText: '共',
                totalPageAfterText: '页',
                currPageBeforeText: '当前第',
                currPageAfterText: '页',
                totalInfoSplitStr: '/',
                totalRecordsBeforeText: '共',
                totalRecordsAfterText: '条数据',
                gopageBeforeText: '&nbsp;转到',
                gopageButtonOkText: '确定',
                gopageAfterText: '页',
                buttonTipBeforeText: '第',
                buttonTipAfterText: '页'
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
    var ReportType = $("#ReportType").val();
    var CastFlag = $("#CastFlag").val();
    var Status = $("#SelStatus").val();
    var start = $("#start").val();
    var end = $("#end").val();

    window.location.href = "/ReportDataWebApi/OutPutCards?ReportType=" + ReportType + "&CastFlag=" + CastFlag + "&Status=" + Status + "&start=" + start + "&end=" + end;
}
var BroadCastInfoList = new BroadCastInfoList();

