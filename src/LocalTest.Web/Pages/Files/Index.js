$(function () {
    var l = abp.localization.getResource('LocalTest');
    var DOWNLOAD_ENDPOINT = "/download";
    var downloadForm = $("form#DownloadFile");
    downloadForm.submit(function (event) {
        event.preventDefault();
        var fileName = $("#fileName").val().trim();
        //var downloadWindow = window.open(DOWNLOAD_ENDPOINT + "/" + fileName, "_blank");
        //var url = "https://gyhyjysvn.chinayasha.com/svn/Public/appupdate/BDSautocad/FamilyLibCache/北理工四地块公寓项目/全户型/10墙顶收口线条-QM00.rfa";
        window.location.href = fileName;
        //window.location.href = url;
        //downloadWindow.focus();
    });
});
