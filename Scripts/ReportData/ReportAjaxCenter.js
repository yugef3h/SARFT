function ReportAjaxCenter() { }

ReportAjaxCenter.prototype = {
    getReportDataInfo: function GetReportDataInfo(ID) {
        jQuery.ajax({
            url: "/ReportDataWebApi/GetReportDataInfo",
            type: "POST",
            dataType: "json",
            async: false,
            data: { ID: ID },
            success: function (resultInfo) {
                if (resultInfo.Result > 0) {
                    var da = resultInfo.Data;
                    $("#UserName").html((da.UserName == "" ? da.Phone : da.UserName));
                    $("#UserCarLicense").html(da.UserCarLicense);
                    $("#ReportTime").html(da.ReportTime);
                    $("#TextLocation").val(da.TextLocation);
                    $("#SelReportType").val(da.ReportType);
                    $("#ReportCarLicense").val(da.ReportCarLicense);
                    $("#CusRemark").val(da.CusRemark);
                    $("#BroRemark").val(da.BroRemark);
                    $("#img2").html("<img src=" + da.ImgURL + " style='width:400px; height:400px;'>");
                    $(".pay").click();
                    //重新加载
                    Sub(2);
                }
                else {
                    alert(resultInfo.Desc);
                }
            }
        });
    }, saveCastReportDataContent: function SaveCastReportDataContent(ConfigInfoJson) {
        jQuery.ajax({
            url: "/ReportDataWebApi/SaveCastReportDataContent",
            type: "POST",
            async: false,
            dataType: "json",
            data: { ConfigInfoJson: ConfigInfoJson },
            success: function (resultInfo) {
                var $modal = $('#doc-modal-3');
                $modal.modal('close');
                //重新加载
                Sub(2);
                //alert(resultInfo.Desc);
            }
        });
    }, saveCastScores: function SaveCastScores(Id, star) {
        jQuery.ajax({
            url: "/ReportDataWebApi/SaveCastScores",
            type: "POST",
            async: false,
            dataType: "json",
            data: { Id: Id, star: star },
            success: function (resultInfo) {
                var $modal = $('#doc-modal-Star');
                $modal.modal('close');
                //重新加载
                Sub(2);
                //alert(resultInfo.Desc);
            }
        });
    }, saveRecomand: function SaveRecomand(Id) {
        jQuery.ajax({
            url: "/ReportDataWebApi/SaveRecomand",
            type: "POST",
            async: false,
            dataType: "json",
            data: { Id: Id},
            success: function (resultInfo) {
             
                //重新加载
                Sub(2);
              
            }
        });
    }, cancelRecomand: function CancelRecomand(Id) {
        jQuery.ajax({
            url: "/ReportDataWebApi/CancelRecomand",
            type: "POST",
            async: false,
            dataType: "json",
            data: { Id: Id},
            success: function (resultInfo) {
               
                //重新加载
                Sub(2);
               
            }
        });
    }
}
var ReportAjaxCenter = new ReportAjaxCenter();