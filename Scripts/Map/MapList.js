function MapList() { }
MapList.prototype = {
    //查询数据 
    interval: function Interval() {
        selectPage($("#reportid").val());
    }
}
var array = new Array();
var user = new Object();
function selectPage(id) {
    data = { id: id };
    jQuery.ajax({
        async: false,
        url: "/Map/Initial",
        type: "POST",
        dataType: "json",
        data: data,
        success: function (data) {
            var da = eval(data);
            if (da.Id > 0) {
                array = [];
                user = new Object();
                user.id = da.Id;
                user.UserName = da.UserName;
                user.Phone = da.Phone;
                user.UserCarLicense = da.UserCarLicense;
                user.Lon = da.Lon;
                user.Lat = da.Lat;
                user.TextLocation = da.TextLocation;
                user.ImgURL = da.ImgURL;
                user.ReportTime = da.ReportTime;
                user.Status = da.Status;
                user.Stars = da.Stars;
                user.CusRemark = da.CusRemark;
                user.ReportCarLicense = da.ReportCarLicense;
                user.ReportType = da.ReportType;
                user.BroRemark = da.BroRemark;
                array.push(user);
            } else {
            }
        }
    });
}
function GetParameter(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
var MapList = new MapList();