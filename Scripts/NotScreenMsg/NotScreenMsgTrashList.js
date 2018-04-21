function NotScreenMsgTrashList() { }
var totalPage;
var totalRecords;
var now = new Date();
var myyear = now.getYear();
var year = (myyear > 200) ? myyear : 1900 + myyear;
var pageSize = 10;
var pageIndex = 1;
var url = "";
var data = "";
NotScreenMsgTrashList.prototype = {
    //查询 
    search: function Search() {
        Sub();
    },
    saveReport: function SaveReport(ID) {
        jQuery.ajax({
            url: "/NotScreenMsg/RecoverReport",
            type: "POST",
            dataType: "json",
            async: true,
            data: { ID: ID },
            success: function (resultInfo) {
                if (resultInfo.Result > 0) {
                    selectPage(pageIndex, pageSize, $(".start").val(), $(".end").val(), 0, 0, 0, 0, 0, 0, 1, true);
                    $(".pay").click();
                }
                else {
                    alert("获取数据失败");
                }
            }
        });
    }
}
function Sub() {
    if ($(".end").val() != "") {
        if ($(".start").val() > $(".end").val()) {
            alert("开始时间不能大于结束时间");
            return false;
        }
    }
    selectPage(1, pageSize, $(".start").val(), $(".end").val(), 0, 0, 0, 0, 0, 0, 1, true);
}
function selectPage(n, pageSize, startTime, endTime, Status, Stars, CusFlag, CastFlag, HostFlag, ReportType, oderType, first) {
    url = "/NotScreenMsg/GetReortDataInfoTrashList";
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
            var Names = "";
            if (resultInfo.Result > 0) {
                log(resultInfo.Data.TotalCount, first);
                var da = resultInfo.Data;
                for (var i = 0; i < da.Data.length; i++) {

                    if (n == 1 && i == 0 && startTime == "" && endTime == "") {
                        $("#hidcount").val(resultInfo.Data.TotalCount);
                    }
                    html += "<tr>";
                    if (da.Data[i].UserName == "") {
                        Names = da.Data[i].Phone
                    } else {
                        Names = da.Data[i].UserName
                    }
                    html += "<td style=\"cursor:pointer;\" class='am-sim'>" + Names + "</td>";
                    html += "<td>" + da.Data[i].UserCarLicense + "</td>";
                    html += "<td>" + da.Data[i].TextLocation + "</td>";
                    html += "<td><a onclick='img(" + JSON.stringify(da.Data[i].ImgURL) + "," + da.Data[i].Id + ")'><img src=" + da.Data[i].ImgURL + " style='width:60px; height:40px;' onmousewheel='return bigimg(this)'></a></td>";
                    html += "<td>" + (da.Data[i].ReportTime == null ? "" : da.Data[i].ReportTime) + "</td>";
                    html += "<td> <button type='button' class='am-btn am-btn-primary am-btn-md' onclick='updateReport(" + da.Data[i].Id + ")'>查看</button></td>";
                    html += "<td> <button type='button' class='am-btn am-btn-primary am-btn-md' onclick='RecoverReport(" + da.Data[i].Id + ")'>恢复</button></td>";
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
//点击图片
function img(obj, objid) {
    $("#img").html("<img src=" + obj + " onmousewheel='return bigimg(this)'>");
    $('#ProType-prompt').modal({
        relatedTarget: this,
        onConfirm: function (e) {
        },
        onCancel: function (e) {

        }
    })
    $("#ProType-prompt").show();
    //var ContentConfig = {
    //    "Id": objid,
    //};
    //var ConfigInfoJson = JSON.stringify(ContentConfig);
    //jQuery.ajax({
    //    url: "/NotScreenMsg/AlterReportCusFlag",
    //    type: "POST",
    //    dataType: "json",
    //    async: false,
    //    data: { ConfigJson: ConfigInfoJson },
    //    success: function (resultInfo) {
    //        if (resultInfo.Result > 0) {
    //            $("#img").html("<img src=" + obj + " style='width:100%; height:100%;'>");
    //            selectPage(pageIndex, pageSize, $(".start").val(), $(".end").val(), 0, 0, 0, 0, 0, 0, 1, true);
    //        }
    //        else {
    //            alert("获取数据失败");
    //        }
    //    }
    //});

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
                selectPage(n, pageSize, $(".start").val(), $(".end").val(), 0, 0, 0, 0, 0, 0, 1, true);
                return false;
            }


        }, first);
    }
}
//查看信息
function updateReport(obj) {
    var msg = document.getElementById("msgtips");
    msg.innerHTML = "";
    msg.style.display = "";
    jQuery.ajax({
        url: "/NotScreenMsg/GetReportInfo",
        type: "POST",
        dataType: "json",
        async: false,
        data: { ID: obj },
        success: function (resultInfo) {
            if (resultInfo != null) {
                $("#hidID").val(resultInfo.Id);
                if (resultInfo.UserName != "") {
                    $("#UserName").val(resultInfo.UserName);
                } else {
                    $("#UserName").val(resultInfo.Phone);
                }
                if (resultInfo.Status <= 1) {
                    $("#hidrecommend").show();
                } else {
                    $("#hidrecommend").hide();
                }
                $("#recommendid").val(resultInfo.Id);
                $("#imgs").html("<img src=" + resultInfo.ImgURL + " style='width:400px; height:400px;' onmousewheel='return bigimg(this)'>");
                $("#UserCarLicense").val(resultInfo.UserCarLicense);
                $("#ReportTime").val(resultInfo.ReportTime);
                $("#TextLocation").val(resultInfo.TextLocation);
                $("#CusRemark").val(resultInfo.CusRemark);
                $("#ReportCarLicense").val(resultInfo.ReportCarLicense);
                $("#selectedid").val(resultInfo.ReportType);
                $('#UserName').attr("disabled", true);
                $('#TextLocation').attr("disabled", "disabled");
                $('#UserCarLicense').attr("disabled", true);
                $('#ReportTime').attr("disabled", true);
                $("#js-selected").find("option[value='" + $("#selectedid").val() + "']").attr("selected", true);
                $(".pay").click();
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
var NotScreenMsgTrashList = new NotScreenMsgTrashList();