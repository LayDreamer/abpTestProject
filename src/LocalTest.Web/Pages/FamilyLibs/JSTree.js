document.write("<script language = javascript src = 'https://cdnjs.cloudflare.com/ajax/libs/jstree/3.3.15/jstree.min.js'></script>");
$(function () {
    $('#jstree_demo_div').jstree({

        'plugins': ["search", "themes", "types", "state", "line", "contextmenu"],   //包含样式，选择框，图片等
        'types': {
            'default': {
                'icon': true // 默认图标,可以写路径名，但是必须将themes的icons打开，否则没有地方展示图标
            },
        },
        "contextmenu": {

            "items": {
                "create": null,
                "rename": null,
                "remove": null,
                "ccp": null,
                "新建子节点": {
                    "label": "新建子节点",
                    "action": function (data) {
                        const inst = data.instance;
                        const selectedNode = inst.get_node(data.selected);
                        dialog.show({ "title": "新建“" + selectedNode.text + "”的子菜单", /*url: "/accountmanage/createMenu?id=" + obj.id,*/ height: 280, width: 400 });
                    }
                },
                "删除节点": {
                    "label": "删除节点",
                    "action": function (data) {
                        var inst = jQuery.jstree.reference(data.reference),
                            obj = inst.get_node(data.reference);
                        if (confirm("确定要删除此菜单？删除后不可恢复。")) {
                            jQuery.get("/accountmanage/deleteMenu?id=" + obj.id, function (dat) {
                                if (dat == 1) {
                                    alert("删除菜单成功！");
                                    jQuery("#" + treeid).jstree("refresh");

                                } else {
                                    alert("删除菜单失败！");
                                }
                            });
                        }
                    }

                },
                "编辑节点": {
                    "label": "编辑节点",
                    "action": function (data) {
                        var inst = jQuery.jstree.reference(data.reference),
                            obj = inst.get_node(data.reference);
                        dialog.show({ "title": "编辑“" + obj.text + "”菜单", url: "/accountmanage/editMenu?id=" + obj.id, height: 280, width: 400 });
                    }
                }
            }
        },

        'core': {  //core主要功能是控制树的形状，单选多选等等

            'data': function (obj, callback) {
                var jsonstr = "[]";
                var jsonarray = eval('(' + jsonstr + ')');

                $.ajax({
                    type: 'GET',
                    url: "/FamilyLibs/Index?handler=TreeList",
                    async: false,
                    dataType: "json",
                    success: function (result) {

                        var arrays = result;
                        for (var i = 0; i < arrays.length; i++) {

                            var arr = {
                                "id": arrays[i].id,
                                "parent": arrays[i].parent == null ? "#" : arrays[i].parent,
                                "text": arrays[i].text
                            }
                            jsonarray.push(arr);
                        }
                    }
                });
                callback.call(this, jsonarray);
            },

            //填充数据,data需要识别格式,关键字为id, text,children,展示时显示的是text,传递的可以是id也可以是text

            'themes': {
                "icons": false,        //默认图标
                "theme": "classic",
                "dots": false,
                "stripes": false,        //增加条纹
            },        //关闭文件夹样式
            'dblclick_toggle': true,   //允许tree的双击展开,默认是true
            "multiple": false, // 单选
            'check_callback': function (operation, node, node_parent, node_position, more) {
                return true;
            },

        },
    }
    );

    $('#jstree_demo_div').on("select_node.jstree", function (event, data) {
        // 获取选中的节点
        const inst = data.instance;
        const selectedNode = inst.get_node(data.selected);

        $("#idtest").text(selectedNode.id);
    });
});

