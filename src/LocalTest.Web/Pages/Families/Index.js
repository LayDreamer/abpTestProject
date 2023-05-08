$(function () {
    var l = abp.localization.getResource('LocalTest');

    /*LoadTable(null);*/

    var inputAction = function (requestData, dataTableSettings) {
        var ctl = $("#idtest").html();
        return {
            key: ctl,
        };
    };

    var dataTable = $('#FamiliesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(localTest.familyLibs.familyLib.getListById, inputAction),

            columnDefs: [
                //{
                //    title: l('Actions'),
                //    rowAction: {
                //        items:
                //            [
                //                {
                //                    text: l('Upload'),
                //                    action: function (data) {
                //                        uploadModal.open({ id: data.record.id });
                //                    }
                //                },
                //                {
                //                    text: l('Download'),
                //                    action: function (data) {
                //                        downloadModal.open({ id: data.record.id });
                //                    }
                //                },
                //                {
                //                    text: l('Delete'),
                //                    confirmMessage: function (data) {
                //                        return l(
                //                            'FamilyDeletionConfirmationMessage',
                //                            data.record.name
                //                        );
                //                    },
                //                    action: function (data) {
                //                        localTest.familyLibs.familyLib
                //                            .delete(data.record.id)
                //                            .then(function () {
                //                                abp.notify.info(
                //                                    l('SuccessfullyDeleted')
                //                                );
                //                                dataTable.ajax.reload();
                //                            });
                //                    }
                //                },
                //                {
                //                    text: l('Test'),
                //                    action: function (data) {
                //                        treeModal.open();
                //                    }
                //                }
                //            ]
                //    }
                //},

                {
                    title: l('Actions'),
                    rowAction: {
                        items: [
                            {
                                text: l('Download'),
                                action: function (data) {
                                    downloadModal.open({ id: data.record.id });
                                }
                            },]
                    }
                },
                {
                    rowAction: {
                        items: [
                            {
                                text: l('Detail'),
                                action: function (data) {
                                    testModal.open({ id: data.record.id });
                                }
                            },]
                    }
                },
                {
                    title: "<input align=\"center\" type=\"checkbox\" class=\"checkAll\" checked=\"checked\"  id=\"checkAll\"/>",
                    data: "",
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


    /*var createModal = new abp.ModalManager(abp.appPath + 'Families/CreateModal');*/
    var downloadModal = new abp.ModalManager(abp.appPath + 'FamilyLibs/DownloadModal');
    var testModal = new abp.ModalManager(abp.appPath + 'FamilyLibs/DetailModal');
    /*var uploadModal = new abp.ModalManager(abp.appPath + 'Families/UploadModal');*/

    downloadModal.onResult(function () {
        dataTable.ajax.reload();
    });
    testModal.onResult(function () {
        dataTable.ajax.reload();
    });

    //createModal.onResult(function () {
    //    dataTable.ajax.reload();
    //});

    //uploadModal.onResult(function () {
    //    dataTable.ajax.reload();
    //});

    $('#NewFamilyButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
    $('#jstree_demo_div').click(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

});

