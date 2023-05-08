$(function () {
    var l = abp.localization.getResource('LocalTest');
    var inputAction = function (requestData, dataTableSettings) {
        var name = $("#SearchKey").val();
        return {
            searchValue: name,
        };
    };
    var dataTable = $('#RequisitionListTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[2, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(localTest.factoryList.materialRequisition.getListByInput, inputAction),
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
                        items:
                            [
                                {
                                    text: l('Delete'),
                                    confirmMessage: function (data) {
                                        return l('MaterialRequisitionDeletionConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    visible: abp.auth.isGranted('LocalTest.FactoryMaterialRequisition.Delete'), //CHECK for the PERMISSION
                                    action: function (data) {
                                        localTest.factoryList.materialRequisition
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
                //{
                //    title: l('CreatorId'),
                //    data: "creatorId"
                //},
                {
                    title: l('Name'),
                    data: "name"
                },

                //{
                //    title: l('FilePath'),
                //    data: "filePath"
                //},

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
                },
            ]
        })
    );

    var createModal = new abp.ModalManager(abp.appPath + 'FactoryList/CreateModal');

    createModal.onResult(function () {
        dataTable.ajax.reload();
        abp.notify.success(l('Successfully'));
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

    $('#SearchButton').click(function (e) {
        dataTable.ajax.reload();
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

    function GetSelectedNodes() {
        var table = $("#RequisitionListTable").dataTable();
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