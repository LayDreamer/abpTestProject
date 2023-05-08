$(function () {
    var l = abp.localization.getResource('LocalTest');
    var dataTable = $('#FamilyTreeTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(localTest.families.family.getList),
            columnDefs: [
                {
                    title: l('ProcductName'),
                    data: "procductName"
                },
                {
                    title: l('FileName'),
                    data: "fileName",
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
});