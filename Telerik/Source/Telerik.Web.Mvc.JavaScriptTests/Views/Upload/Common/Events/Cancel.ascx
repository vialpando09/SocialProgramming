<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<script type="text/javascript">

    test("cancel fired when clicking cancel", 1, function() {
        uploadInstance = createUpload({ onCancel:
            function(e) {
                ok(true);
            }
        });

        simulateFileSelect();
        $(".t-cancel", uploadInstance.wrapper).trigger("click");
    });

    test("cancel event arguments contain list of files", function() {
        var files = false;
        uploadInstance = createUpload({ onCancel:
            function(e) {
                files = e.files;
            }
        });

        simulateFileSelect();
        $(".t-cancel", uploadInstance.wrapper).trigger("click");

        assertSelectedFile(files);
    });

    test("cancelling an upload should fire complete event", 1, function() {
        uploadInstance = createUpload({ onComplete:
            function(e) {
                ok(true);
            }
        });

        simulateFileSelect();
        $(".t-cancel", uploadInstance.wrapper).trigger("click");
    });

</script>