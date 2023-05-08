$(function () {
    var l = abp.localization.getResource('LocalTest');

    var inputAction = function (requestData, dataTableSettings) {
        var listNumber = $("#listNumber").html();
        return {
            searchValue: listNumber,
        };
    };
    var dataTable = $('#SpecificationDetailTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(localTest.materialSpecificationList.materialSpecificationDetail.getListById, inputAction),
            columnDefs: [
                //{
                //    title: l('Actions'),
                //    rowAction: {
                //        items:
                //            [
                //                {
                //                    text: l('Delete'),
                //                    confirmMessage: function (data) {
                //                        return l('MaterialDeletionConfirmationMessage',
                //                            data.record.name
                //                        );
                //                    },
                //                    action: function (data) {
                //                        localTest.materialSpecificationList.materialSpecificationDetail
                //                            .delete(data.record.id)
                //                            .then(function () {
                //                                abp.notify.info(
                //                                    l('SuccessfullyDeleted')
                //                                );
                //                                dataTable.ajax.reload();
                //                            });
                //                    }
                //                }
                //            ]
                //    }
                //},
                //{
                //    title: l('Name'),
                //    data: "name"
                //},
                //{
                //    title: l('ProjectName'),
                //    data: "projectName"
                //},
                {
                    title: l('MaterialNumber'),
                    data: "materialNumber"
                },
                {
                    title: l('ProductPlatform'),
                    data: "productPlatform"
                },
                {
                    title: l('ProductSystem'),
                    data: "productSystem"
                },
                {
                    title: l('ComponentPosition'),
                    data: "componentPosition"
                },
                {
                    title: l('MaterialName'),
                    data: "materialName"
                },
                {
                    title: l('DrawingLength'),
                    data: "drawingLength"
                },
                {
                    title: l('DrawingWidth'),
                    data: "drawingWidth"
                },
                {
                    title: l('DrawingThickness'),
                    data: "drawingThickness"
                },
                {
                    title: l('MaterialCount'),
                    data: "materialCount"
                },
                {
                    title: l('MaterialUnit'),
                    data: "materialUnit"
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
});