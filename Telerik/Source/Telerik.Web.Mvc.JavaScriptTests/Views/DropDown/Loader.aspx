<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTests" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>DropDown Rendering</h2>

    <script type="text/javascript">
        var loader;
        var component;
        var $component;
        var $t;
        var isCalled = false;

        //event handler
        function dataBinding(e) {
            e.data = $.extend({}, e.data, { Test: "test" });
            isCalled = true;
        }

    </script>

    <%= Html.Telerik().DropDownList()
            .Name("DDL")
            .ClientEvents(events => events.OnDataBinding("dataBinding"))
    %>

    <% Html.Telerik().ScriptRegistrar()
           .Scripts(scripts => scripts
               .Add("telerik.common.js")
               .Add("telerik.list.js")); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="TestContent" runat="server">

<script type="text/javascript">



        QUnit.testStart = function() {
            $t = $.telerik;

            $component = $('#DDL');
            component = $component.data('tDropDownList')
            component.onDataBinding = "dataBinding";
            loader = new $t.list.loader(component);
        }

        test('isAjax method should return component ajax method', function() {
            component.ajax = { selectUrl: "selectUrl" };

            var result = loader.isAjax();

            equal(result, component.ajax);
        });

        test('isAjax method should return component ws method', function() {
            component.ws = { selectUrl: "selectUrl" };

            var result = loader.isAjax();

            equal(result.selectUrl, component.ws.selectUrl);
        });

        test('ajaxRequest should raise dataBinding event', function() {
            isCalled = false;
            loader.ajaxRequest(function () { });
            ok(isCalled);
        });

        test('ajaxRequest should create ajaxOptions and pass them to jQuery ajax method', function() {
            var ajaxOptions;
            component.ajax = undefined;
            component.ws = { selectUrl: "testURL" };
            $.ajax = function (options) { ajaxOptions = options; }
            loader.ajaxRequest(function () { });

            equal(ajaxOptions.url, 'testURL');
            equal(ajaxOptions.type, 'POST');
            equal(ajaxOptions.dataType, 'text');
            equal(ajaxOptions.contentType, 'application/json; charset=utf-8');

            component.ajax = undefined;
        });

</script>

</asp:Content>