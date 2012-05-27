<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

  <%= Html.Telerik().Calendar()
          .Name("Calendar")
  %>
  
<script type="text/javascript">
    function getCalendar() {
        return $("#Calendar").data('tCalendar');
    }
</script>
</asp:Content>

<asp:Content ContentPlaceHolderID="TestContent" runat="server">

<script type="text/javascript">

    test('Value method should put in range viewedMonth if focusedDate is out of range', function () {
        var today = new Date();
        today.setDate(today.getDate() - 1);

        var calendar = getCalendar();
        calendar.maxDate = today;
        calendar.value(null);

        equal(+calendar.viewedMonth.year(), +today.getFullYear(), "viewedMonth is not defined correctly");
        equal(+calendar.viewedMonth.month(), +today.getMonth(), "viewedMonth is not defined correctly");
        equal(+calendar.viewedMonth.date(), 1, "viewedMonth is not defined correctly");
    });

    test('isInRange method should compare only date, not time part', function () {
        var min = new Date(1900,0,1);
        var max = new Date();
        var today = new Date();

        today.setHours(max.getHours() + 1);

        ok($.telerik.calendar.isInRange(today, min, max), "isInRange does not compare correctly");
    });
</script>

</asp:Content>