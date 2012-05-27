<%@ Page Title="Treeview / ClientAPI" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <%= Html.Telerik().TreeView()
            .Name("ClientSideTreeView")
            .ShowCheckBox(true)
            .Effects(fx => fx.Toggle()) %>

</asp:Content>


<asp:Content ContentPlaceHolderID="TestContent" runat="server">

<script type="text/javascript">

        var treeview;

        module("TreeView / ClientAPI", {
            setup: function() {
                treeview = $('#ClientSideTreeView').data('tTreeView');
            }
        });

        test('disable disables checkboxes', function() {
            treeview.bindTo([
                { Text: 'foo' }
            ]);

            treeview.disable('.t-item');

            ok($(':checkbox', treeview.element).is('[disabled]'));
        });

        test('enable enables checkboxes', function() {
            treeview.bindTo([
                { Text: 'foo', Enabled: false }
            ]);

            treeview.enable('.t-item');

            ok($(':checkbox', treeview.element).is(':not([disabled])'));
        });

        test('disable disables expand collapse icon', function() {
            treeview.bindTo([
                {
                    Text: 'foo', Expanded: false,
                    Items: [{ Text: 'bar' }]
                }
            ]);

            treeview.disable('.t-item');

            ok($('.t-item:first > div > .t-icon').hasClass('t-plus-disabled'));
            ok(!($('.t-item:first > div > .t-icon').hasClass('t-plus')));
        });

        test('enable enables expand collapse icon', function() {
            treeview.bindTo([
                {
                    Text: 'foo', Expanded: false, Enabled: false,
                    Items: [{ Text: 'bar' }]
                }
            ]);

            treeview.enable('.t-item');

            ok($('.t-item:first > div > .t-icon').hasClass('t-plus'));
            ok(!($('.t-item:first > div > .t-icon').hasClass('t-plus-disabled')));
        });

        test('checking node should set value of the checkbox input to true', function () {
            treeview.bindTo([
                { Text: 'foo' }
            ]);
            
            var checkbox = $(':checkbox', treeview.element);
            var item = checkbox.closest('.t-item');
            treeview.nodeCheck(item[0], true);

            equal(checkbox.attr('checked'), 'checked', 'value of the checkbox was not updated');
        });

        test('unchecking node should set value of the checkbox input to false', function () {
            treeview.bindTo([
                { Text: 'foo', Checked: true }
            ]);
            
            var checkbox = $(':checkbox', treeview.element);
            var item = checkbox.closest('.t-item');
            treeview.nodeCheck(item[0], false);

            ok(!checkbox.attr('checked'), 'value of the checkbox was not updated');
        });

        test('dynamically adding subnodes with checkboxes generates valid checkbox ids', function() {
            treeview.bindTo([
                { Text: 'foo', Checkable: false },
                { Text: 'bar', Checkable: false, Expanded: true, Items: [
                    { Text: 'baz', Checkable: false }
                ] }
            ]);
                
            treeview.dataBind($('.t-item:first', treeview.element), [{ Text: 'qux' }]);
            treeview.dataBind($('.t-item:last', treeview.element), [{ Text: 'qux' }]);

            var checkboxes = $(":checkbox");

            equal(checkboxes.eq(0).attr('name'), 'ClientSideTreeView_checkedNodes[0:0].Checked');
            equal(checkboxes.eq(1).attr('name'), 'ClientSideTreeView_checkedNodes[1:0:0].Checked');
        });

        test('nodeCheck() on non-visible items does not delete checkboxes', function() {
            treeview.bindTo([
                { Text: 'foo', Expanded: false, Items: [
                    { Text: 'bar', Checked: true }
                ] }
            ]);

            treeview.nodeCheck($('.t-item:last', treeview.element), false);

            equal($(":checkbox").length, 2);
        });

        test('bindTo() clears items, if necessary', function() {
            treeview.bindTo([]);

            equal($(treeview.element).find(".t-item").length, 0);
        });

        test('dataBind() removes subgroups', function() {
            treeview.bindTo([
                { Text: 'foo', Expanded: false, Items: [
                    { Text: 'bar', Checked: true }
                ] }
            ]);

            treeview.dataBind($(treeview.element).find(".t-item"), []);

            equal($(treeview.element).find(".t-item .t-group").length, 0);
        });

        test('dataBind() shows icon, if necessary', function() {
            treeview.bindTo([
                { Text: 'foo', Expanded: false }
            ]);

            treeview.dataBind($(treeview.element).find(".t-item"), [{ Text: 'bar', Checked: true }]);

            var rootItemIcon = $(treeview.element).find("> ul > li > div > .t-icon");

            equal($(treeview.element).find(".t-item .t-group").length, 1);
            equal(rootItemIcon.length, 1);
            ok(rootItemIcon.hasClass("t-minus"));
        });

        test('findByText() finds single item', function() {
            treeview.bindTo([
                { Text: 'foo' },
                { Text: 'bar' },
                { Text: 'baz' }
            ]);

            var result = treeview.findByText('foo'),
                items = $(treeview.element).find(".t-item");

            equal(result.length, 1);
            equal(result[0], items[0]);
        });

        test('findByText() does not search in substrings', function() {
            treeview.bindTo([
                { Text: 'foo' },
                { Text: 'bar' },
                { Text: 'baz' }
            ]);
            
            ok(!treeview.findByText('fo').length);
        });

        test('findByText() returns multiple items', function() {
            treeview.bindTo([
                { Text: 'foo' },
                { Text: 'bar' },
                { Text: 'foo' }
            ]);

            var result = treeview.findByText('foo'),
                items = $(treeview.element).find(".t-item");
            
            equals(result.length, 2);
            equals(result[0], items[0]);
            equals(result[1], items[2]);
        });

        test('findByValue() finds single item', function() {
            treeview.bindTo([
                { Text: 'foo', Value: 1 },
                { Text: 'bar', Value: 2 },
                { Text: 'baz', Value: 3 }
            ]);

            var result = treeview.findByValue(2),
                items = $(treeview.element).find(".t-item");

            equal(result.length, 1);
            equal(result[0], items[1]);
        });

        test('findByValue() returns multiple items', function() {
            treeview.bindTo([
                { Text: 'foo', Value: 1 },
                { Text: 'bar', Value: 2 },
                { Text: 'baz', Value: 1 }
            ]);

            var result = treeview.findByValue(1),
                items = $(treeview.element).find(".t-item");
            
            equals(result.length, 2);
            equals(result[0], items[0]);
            equals(result[1], items[2]);
        });

        test("append(nodeData, root) appends new node to empty parent", function() {
            treeview.bindTo([
                { Text: 'foo' }
            ]);

            var root = $(treeview.element).find(".t-item:first");

            var appendedNode = treeview.append({ Text: "bar" }, root);

            equals(root.find(".t-group").length, 1);
            equals(root.find(".t-group").css("display"), "block");
            equals(root.find(".t-item").length, 1);
            equals(appendedNode[0], root.find(".t-item")[0]);
            equals(treeview.getItemText(appendedNode), "bar");
            ok(appendedNode.find(">div").hasClass("t-bot"));
            ok(appendedNode.hasClass("t-last"));
            equals(root.find(">div>.t-minus.t-icon").length, 1);
            equals(root.find(".t-item .t-checkbox").length, 1);
        });

        test("append(nodeData, parentNode) appends new node to parent with existing group", function() {
            treeview.bindTo([
                { Text: "foo", Expanded: true, Items: [
                    { Text: "bar" }
                ] }
            ]);
            
            var parentNode = $(treeview.element).find(".t-item:first");

            var appendedNode = treeview.append({ Text: "bar" }, parentNode);

            equals(parentNode.find(".t-group").length, 1);
            equals(parentNode.find(".t-group").css("display"), "block");
            equals(parentNode.find(".t-item").length, 2);
            equals(appendedNode[0], parentNode.find(".t-item:last")[0]);
            ok(!appendedNode.prev(".t-item").hasClass("t-last"));
            equals(appendedNode.find(">div.t-bot").length, 1);
        });

        test("append(nodeData) appends new node to root", function() {
            treeview.bindTo([
                { Text: "foo" }
            ]);
            
            var treeViewElement = $(treeview.element),
                rootGroup = treeViewElement.find(".t-group");

            var appendedNode = treeview.append({ Text: "bar" });

            equals(rootGroup.length, 1);
            equals(treeViewElement.find("div").length, 2);
            equals(rootGroup.find("> .t-item").length, 2);
            ok(rootGroup.find("> .t-item").hasClass("t-first"));
        });

        test("append(nodeElement, parentNode) appends node to parent", function() {
            treeview.bindTo([
                { Text: "foo", Expanded: true, Items: [
                    { Text: "buzz" },
                    { Text: "bar" }
                ] },
                { Text: "baz" }
            ]);

            var rootGroup = $(treeview.element).find("> .t-group"),
                subNode = treeview.findByText("buzz"),
                newParent = treeview.findByText("baz");

            var appendedNode = treeview.append(subNode[0], newParent);

            equals(appendedNode[0], newParent.find(".t-item")[0]);
            equals(rootGroup.children().length, 2);
            equals(subNode.find(".t-bot").length, 1);
        });

        test("append(nodeElement) appends node to root", function() {
            treeview.bindTo([
                { Text: "foo", Expanded: true, Items: [
                    { Text: "bar" }
                ] }
            ]);

            var rootGroup = $(treeview.element).find("> .t-group"),
                subNode = treeview.findByText("bar");

            var appendedNode = treeview.append(subNode[0]);

            equals(subNode[0], appendedNode[0]);
            equals(rootGroup.children().length, 2);
            equals(rootGroup.find(".t-icon").length, 0);
            equals(rootGroup.find(".t-group").length, 0);
        });

        test("append() of single child to same parent does not remove it", function() {
            treeview.bindTo([
                { Text: "foo", Expanded: true, Items: [
                    { Text: "bar" }
                ] }
            ]);

            var rootGroup = $(treeview.element).find("> .t-group"),
                subNode = treeview.findByText("bar"),
                rootNode = treeview.findByText("foo");

            var appendedNode = treeview.append(subNode, rootNode);

            equals(subNode[0], appendedNode[0]);
            equals(rootGroup.children().length, 1);
            equals(rootNode.find(".t-group").length, 1);
            equals(rootNode.find(".t-group").children().length, 1);

            appendedNode = treeview.append(subNode[0], rootNode);

            equals(subNode[0], appendedNode[0]);
            equals(rootGroup.children().length, 1);
            equals(rootNode.find(".t-group").length, 1);
            equals(rootNode.find(".t-group").children().length, 1);
        });

        test("append() of array of items", function() {
            treeview.bindTo([]);

            var appendedNodes = treeview.append([
                    { Text: "foo" },
                    { Text: "bar" }
                ]);

            var group = $(treeview.element).find(".t-group");

            equals(group.length, 1);
            equals(group.children().length, 2);
        });
        test("insertAfter(nodeData, element) inserts new node after element", function() {
            treeview.bindTo([
                { Text: "foo", Expanded: true },
                { Text: "baz", Expanded: true }
            ]);

            var element = $(treeview.element),
                rootItem = element.find(".t-item:first"),
                rootGroup = element.find(".t-group");

            var insertedNode = treeview.insertAfter({ Text: "bar" }, rootItem);

            equals(rootGroup.find("> .t-item").length, 3);
            equals(rootGroup.find("> .t-item:contains('bar')").index(), 1);
            equals(insertedNode[0], rootGroup.find("> .t-item")[1]);
        });

        test("insertBefore(nodeData, element) inserts new node before element", function() {
            treeview.bindTo([
                { Text: "foo", Expanded: true },
                { Text: "baz", Expanded: true }
            ]);

            var element = $(treeview.element),
                rootItem = element.find(".t-item:last"),
                rootGroup = element.find(".t-group");

            var insertedNode = treeview.insertBefore({ Text: "bar" }, rootItem);

            equals(rootGroup.find("> .t-item").length, 3);
            equals(rootGroup.find("> .t-item:contains('bar')").index(), 1);
            equals(insertedNode[0], rootGroup.find("> .t-item")[1]);
        });

        test("insertBefore(nodeData, element) updates item classes", function() {
            treeview.bindTo([
                { Text: "foo" }
            ]);

            var element = $(treeview.element),
                rootItem = element.find(".t-item");

            var insertedNode = treeview.insertBefore({ Text: "bar" }, rootItem);

            ok(insertedNode.hasClass("t-first"));
            ok(!rootItem.hasClass("t-first"));
            ok(rootItem.hasClass("t-last"));
            ok(!rootItem.find(">div").hasClass("t-top"));
            ok(rootItem.find(">div").hasClass("t-bot"));
            equals(element.find("li").length, 2);
            equals(element.find("div").length, 2);
        });

        test("remove(node) removes nodes", function() {
            treeview.bindTo([
                { Text: "foo" },
                { Text: "bar" }
            ]);

            var element = $(treeview.element),
                rootItems = element.find(".t-item"),
                rootGroup = element.find(".t-group");

            treeview.remove(rootItems[0]);

            rootItems = element.find(".t-item");

            equals(rootItems.length, 1);
            equals(rootGroup.find(".t-in:contains('bar')").length, 1);
            equals(element.find("li").length, 1);
            equals(element.find("div").length, 1);
        });

        test("remove(node) updates classes", function() {
            treeview.bindTo([
                { Text: "foo", Items: [
                    { Text: "bar" }
                ] }
            ]);

            treeview.remove(treeview.findByText("bar"));

            var items = $(treeview.element).find(".t-item");

            equals(items.length, 1);
            equals(items.find(".t-group").length, 0);
        });

        test("remove('.t-item') removes all items, but not root list", function() {
            treeview.bindTo([
                { Text: "foo", Items: [
                    { Text: "bar" }
                ] }
            ]);

            treeview.remove(".t-item");

            var element = $(treeview.element);

            equals(element.find(".t-item").length, 0);
            ok(element.find("> .t-group").length);
            ok(!element.hasClass("t-item"));
        });

</script>

</asp:Content>