$(function () {

    var l = abp.localization.getResource('LocalTest');
    var inputAction = function (requestData, dataTableSettings) {
        var ctl = $("#idtest").html();
        var name = $("#SearchName").val();
        var code = $("#SearchCode").val();
        return {
            key: ctl, searchValue: name, searchCode: code,
        };
    };

    var dataTable = $('#FamiliesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[3, "asc"]],
            searching: false,
            scrollX: true,
            //stateSave: true,      //刷新不会丢失选项
            //autoWidth: false,           //自动宽度
            //processing: true,           //加载进度
            ajax: abp.libs.datatables.createAjax(localTest.familyLibs.familyLib.getListById, inputAction),
            columnDefs: [
                {
                    title: "<input type='checkbox' class='checkAll' id='checkAll'/>",
                    orderable: false,
                    render: function (data, type, full, meta) {
                        return "<input type='checkbox' class='checkchild' name='ckb-jobid' />";
                    },
                },

                {
                    rowAction: {
                        items: [
                            {
                                text: l('Detail'),
                                action: function (data) {
                                    /* testModal.open({ id: data.record.id });*/
                                    GetModuleInfo(data);
                                    SetImg(data);
                                    /*console.log(data);*/
                                }
                            },]
                    }
                },

                {
                    rowAction: {
                        title: l(''),
                        items: [
                            //{
                            //    text: l('Download'),
                            //    action:
                            //        function (data) {
                            //            GetSelectedNodes();
                            //        }
                            //},
                            {
                                text: l('Delete'),
                                confirmMessage: function (data) {
                                    return l(
                                        'FamilyDeletionConfirmationMessage',
                                        data.record.name
                                    );
                                },
                                visible: abp.auth.isGranted('LocalTest.FamilyLibs.Delete'),
                                action: function (data) {
                                    localTest.familyLibs.familyLib
                                        .delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(
                                                l('SuccessfullyDeleted')
                                            );
                                            dataTable.ajax.reload();
                                        });
                                }
                            },
                        ]
                    }
                },

                {
                    title: l('DisplayName'),
                    data: "displayName",
                },

                {
                    title: l('LastModificationTime'), data: "lastModificationTime",
                    render: function (data) {
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString(luxon.DateTime.DATETIME_SHORT);
                    }
                },
                {
                    title: l('CreationTime'), data: "creationTime",
                    render: function (data) {
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString(luxon.DateTime.DATETIME_SHORT);
                    }
                }

            ]
        })
    );

    var downloadModal = new abp.ModalManager(abp.appPath + 'FamilyLibs/DownloadModal');
    var testModal = new abp.ModalManager(abp.appPath + 'FamilyLibs/DetailModal');

    downloadModal.onResult(function () {
        dataTable.ajax.reload();
    });
    testModal.onResult(function () {
        dataTable.ajax.reload();
    });


    $('#jstree_demo_div').click(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
        document.getElementById('moduleCard').hidden = true;

    });

    $('#SearchButton').click(function (e) {
        dataTable.ajax.reload();
        document.getElementById('moduleCard').hidden = true;
    });

    $("#checkAll").click(function (e) {
        if (this.checked) {
            $(this).attr('checked', 'checked')
            $("input[name='ckb-jobid']").each(function () {
                this.checked = true;
            });
        } else {
            $(this).removeAttr('checked')
            $("input[name='ckb-jobid']").each(function () {
                this.checked = false;
            });
        }
    });

    $('#DownloadButton').click(function (e) {
        e.preventDefault();
        GetSelectedNodes();
    });



    //给tr或者td添加click事件
    //$("#treegridtable").on("click", "tr", function () {

    //    var data = $('#treegridtable').dataTable("getSelections");//获取值的对象数据
    //    console.log(data);
    //});


    //获取产品内部信息
    function GetModuleInfo(data) {
        document.getElementById('moduleCard').hidden = false;
        SetTreeGrid(data);
        window.scrollTo(0, document.documentElement.scrollHeight);//页面移动到底部                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
    }
    function SetTreeGrid(data) {
        $('#treegridtable').treegridData({
            id: 'id',
            parentColumn: 'parentId',
            type: "GET", //请求数据的ajax类型
            url: '/FamilyLibs/Index?handler=ModuleLists&id=' + data.record.id,
            ajaxParams: {}, //请求数据的ajax的data属性
            expandColumn: 0,//在哪一列上面显示展开按钮
            striped: true,   //是否各行渐变色
            bordered: true,  //是否显示边框
            expandAll: true,  //是否全部展开
            columns: [
                //{
                //    //field: 'selectItem',
                //    //checkbox: true
                //},
                {
                    title: '名称',
                    field: 'displayName',
                },
                {
                    title: '编码',
                    field: 'number',
                },
                {
                    title: '长',
                    field: 'length',
                },
                {
                    title: '宽',
                    field: 'width',
                },
                {
                    title: '高',
                    field: 'height',
                },
                {
                    title: '版本',
                    field: 'version',
                },
                {
                    title: '单位',
                    field: 'unit',
                },
                //{
                //    title: '地址',
                //    field: 'filePath',
                //}
            ],
            expanderExpandedClass: 'fa fa-chevron-down', // 展开的按钮的图标
            expanderCollapsedClass: 'fa fa-chevron-right' // 缩起的按钮的图标
        });
    }


    function SetImg(data) {
        /*var img = document.getElementsByTagName("img")[0];*/
        var img = document.getElementById("productImg");
        /*img.setAttribute("src", "https://gyhyjysvn.chinayasha.com/svn/Public/appupdate/BDSautocad/SVNImageCache/ProjectReview/2022-11-30-19-04-05.jpg");*/
        if (data.record.imagePath != null) {
            img.setAttribute("src", data.record.imagePath);
        }
    }

    function GetSelectedNodes() {
        var table = $("#FamiliesTable").dataTable();
        var trList = table.fnGetNodes();
        for (i = 0; i < trList.length; i++) {
            var trObj = trList[i];
            var childObj = trObj.firstElementChild.firstElementChild;
            if (childObj.checked) {
                var childreData = table.fnGetData(trObj);
                download(childreData.filePath);
            }
        }
    }

    ///同时下载多个文件
    function download(url) {
        const iframe = document.createElement("iframe");
        iframe.style.display = "none"; // 防止影响页面
        iframe.style.height = 0; // 防止影响页面
        iframe.src = url;
        document.body.appendChild(iframe); // 这一行必须，iframe挂在到dom树上才会发请求
        setTimeout(() => {
            iframe.remove();
        }, 5 * 60 * 1000);// 5分钟之后删除
    }
});

