$(function () {
    var l = abp.localization.getResource('LocalTest');
    var dataTable = $('#SpecificationTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[3, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(localTest.materialSpecificationList.materialSpecification.getList),
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
                                    DisplayDetail(data);
                                }
                            },]
                    }
                },
                {
                    rowAction: {
                        items:
                            [
                                //{
                                //    text: l('Edit'),
                                //    action: function (data) {
                                //        editModal.open({ id: data.record.id });
                                //    }
                                //},
                                {
                                    text: l('Delete'),
                                    confirmMessage: function (data) {
                                        return l('MaterialDeletionConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    visible: abp.auth.isGranted('LocalTest.MaterialSpecificationList.Delete'), //CHECK for the PERMISSION
                                    action: function (data) {
                                        localTest.materialSpecificationList.materialSpecification
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(
                                                    l('SuccessfullyDeleted')
                                                );
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },

                {
                    title: l('FileName'),
                    data: "fileName"
                },
                {
                    title: l('MaterialNumber'),
                    data: "materialNumber"
                },
                {
                    title: l('ProjectName'),
                    data: "projectName"
                },
                {
                    title: l('ProjectCode'),
                    data: "projectCode"
                },
                //{
                //    title: l('Version'),
                //    data: "version"
                //},
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

    var createModal = new abp.ModalManager(abp.appPath + 'MaterialSpecificationList/CreateModal');

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#CreateButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    $('#DownloadButton').click(function (e) {
        e.preventDefault();
        GetSelectedNodes();
    });
    $('#CloseButton').click(function (e) {
        e.preventDefault();
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


    function DisplayDetail(data) {
        document.getElementById('moduleCard').hidden = false;
        $("#listNumber").text(data.record.materialNumber);
        $('#SpecificationDetailTable').DataTable().ajax.reload();
        window.scrollTo(0, document.documentElement.scrollHeight);//页面移动到底部
        /*console.log(data.record.materialNumber);*/
        //$.ajax({
        //    url: '/FamilyLibs/Index?handler=ModuleLists&id=' + data.record.id, // 请求地址
        //    type: 'get', // 请求方式
        //    /* data: '', //携带到后端的参数*/
        //    contentType: 'application/json', // 传参格式（默认为表单） json为application/json
        //    dataType: 'json', //期望后端返回的数据类型
        //    success: function (res) {
        //        console.log('res', res);
        //    }, // 成功的回调函数 res就是后端响应回来的数据
        //    error: function (err) {
        //        console.log('err', err);
        //    } // 失败的回调函数
        //})
    }

    function GetSelectedNodes() {
        var table = $("#SpecificationTable").dataTable();
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