$(function () {
    var l = abp.localization.getResource('LocalTest');
    var inputAction = function (requestData, dataTableSettings) {
        var name = $("#SearchKey").val();
        return {
            searchValue: name,
        };
    };
    var dataTable = $('#ProjectsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            processing: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            deferRender: true,
            ajax: abp.libs.datatables.createAjax(localTest.materialSpecificationList.project.getListByInput, inputAction),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('LocalTest.Projects.Edit'), //CHECK for the PERMISSION
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    confirmMessage: function (data) {
                                        return l('ProjectDeletionConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    visible: abp.auth.isGranted('LocalTest.Projects.Delete'), //CHECK for the PERMISSION
                                    action: function (data) {
                                        localTest.materialSpecificationList.project
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
                    title: l('Status'),
                    data: "status"
                },
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('Code'),
                    data: "code"
                },

                {
                    title: l('Creator'),
                    data: "creator"
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


    var createModal = new abp.ModalManager(abp.appPath + 'Projects/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Projects/EditModal');



    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewProjectButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    $('#SearchButton').click(function (e) {
        dataTable.ajax.reload();
    });
});