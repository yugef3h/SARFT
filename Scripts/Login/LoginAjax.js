function LoginAjax() { }

LoginAjax.prototype = {
    //登录
    login: function Login(userName, password, url, b) {
        jQuery.ajax({
            url: "/Home/UserLogin",
            type: "POST",
            dataType: "json",
            async: false,
            data: { userName: userName, password: password, url: url, b: b },
            success: function (resultInfo) {
                if (resultInfo.Result == 0) {
                    jQuery('#msg_content').text(resultInfo.Desc);
                    jQuery('#msg').show();
                }
                else {
                    if (resultInfo.Result == 1) {
                        window.location.href = "/Admin/index";
                    }
                }
            }
        });
    },
    //注册Two
    registertwo: function Registertwo(UserName, ComTelephone, CompanyName, Password, ComOEMType, ComType) {
        jQuery.ajax({
            url: "/SystemWebApi/SaveRegister",
            type: "POST",
            dataType: "json",
            async: false,
            data: { UserName: UserName, ComTelephone: ComTelephone, CompanyName: CompanyName, Password: Password, ComOEMType: ComOEMType, ComType: ComType },
            success: function (resultInfo) {
                if (resultInfo.Result == 1) {
                    alert(resultInfo.Desc);
                    return false;
                }
                if (resultInfo.Result == 2) {
                    alert("注册成功");
                    $('#UserName').val("");

                    $('#ComTelephone').val("");

                    $('#CompanyName').val("");

                    $("#Password").val("");

                    $("#Password2").val("");
                    //window.location.href = "/System/SystemUserInfoList";
                }
                else {
                    alert(resultInfo.Desc);
                    return false;
                }
            }
        });
    },

    //重置密码
    reset: function Reset(UserName, forgetpass, Code) {
        jQuery.ajax({
            url: "/SystemWebApi/Reset",
            type: "POST",
            dataType: "json",
            async: false,
            data: { UserName: UserName, forgetpass: forgetpass, Code: Code },
            success: function (resultInfo) {
                alert(resultInfo.Desc);
                window.location.href = "/Home/index";
            }
        });
    },
    //注销
    logout: function Logout() {
        debugger;
        jQuery.ajax({
            url: "/Admin/Logout",
            type: "POST",
            async: false,
            dataType: "json",
            success: function (resultInfo) {
                window.location.href = "/Login";
            }
        });
    },
    //修改密码
    alterPassword: function AlterPassword(oldpassword, password) {
        jQuery.ajax({
            url: "/SystemWebApi/AlterPassword",
            type: "POST",
            async: false,
            dataType: "json",
            data: { oldpassword: oldpassword, password: password },
            success: function (resultInfo) {
                if (resultInfo.Result == 2) {
                    alert(resultInfo.Desc);
                }
                else {
                    alert(resultInfo.Desc);
                    $("#oldpassword").val("");
                    $("#oldpassword").focus();
                }
            }
        });
    },

    getStateName: function GetStateName(ID) {
        jQuery.ajax({
            url: "/SystemWebApi/GetStateName",
            type: "POST",
            dataType: "json",
            async: false,
            data: { ID: ID },
            success: function (resultInfo) {
                if (resultInfo.Result > 0) {
                    // $("#state" + pid).html(resultInfo.Desc);
                    if (resultInfo.Desc == undefined || resultInfo.Desc == "" || resultInfo.Desc == null) {
                        statelist.push(" ");
                    }
                    else {
                        statelist.push(resultInfo.Desc);
                    }


                }
            }
        });
    },


    getSysUserInfo: function GetSysUserInfo(ID) {
        jQuery.ajax({
            url: "/SystemWebApi/GetSysUserInfo",
            type: "POST",
            dataType: "json",
            async: false,
            data: { ID: ID },
            success: function (resultInfo) {
                if (resultInfo.Result > 0) {
                    var da = resultInfo.Data;
                    $("#UserName").val(da.UserName);
                    $("#ComType").val(da.ComType);
                    $("#ComOEMType").val(da.ComOEMType);
                    $("#Contact").val(da.Contact);
                    $("#TelePhone").val(da.TelePhone);
                    $("#Email").val(da.Email);
                    $("#CompanyId").val(da.CompanyId);
                    $("#ComTelephone").val(da.ComTelephone);
                    $("#ComAdress").val(da.ComAdress);
                    $("#Profile").html(da.Profile);
                    $(".pay").click();
                }
                else {
                    alert(resultInfo.Desc);
                }
            }
        });
    },
    //系统用户编辑
    saveSysUserContent: function SaveSysUserContent(ConfigInfoJson) {
        jQuery.ajax({
            url: "/SystemWebApi/SaveSysUserContent",
            type: "POST",
            async: false,
            dataType: "json",
            data: { ConfigInfoJson: ConfigInfoJson },
            success: function (resultInfo) {
                var $modal = $('#doc-modal-3');
                $modal.modal('close');
                //重新加载
                Sub(2);
                alert(resultInfo.Desc);
            }
        });
    },

}
var LoginAjax = new LoginAjax();