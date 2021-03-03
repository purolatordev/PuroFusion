(function (global, undefined) {
    global.Telerik = {
        rwUploadId: null,
        rnMessagesId: null,
        reId: null,
        uploadFile: function() {
            //invoke a postback so the file actually uploads so it can be handled, without the user having to click an additional Upload button
            __doPostBack("", "");
        },
        openUploadDialog: function() {
            $find(Telerik.rwUploadId).show();
        },
        OnClientValidationFailed: function (sender, args) {
            var errMessage = "The selected file is invalid. Please upload an MS Word document with an extension .doc, .docx or .rtf, or a .txt/.html/.htm file with HTML content!";
            var notification = $find(Telerik.rnMessagesId);
            notification.set_text(errMessage);
            notification.show();
            //clear the invalid file so the user can try again
            sender.deleteAllFileInputs();
        },
        setMarkdownContent: function () {
            //this function gets called from the server because this is where the markdown file is,
            //but the Markdown import is client-side functionality in RadEditor so the content has to be processed
            //so, to ensure the scripts execute when the controls are available, the Sys.Application.Load event is used
            Sys.Application.add_load(function() {
                try {
                    var converter = new Telerik.Web.UI.Editor.Markdown.Converter();
                    var editor = $find(Telerik.reId);
                    var content = editor.get_text();
                    editor.set_html(converter.makeHtml(content));
                }
                catch (ex) {
                    editor.set_html("");
                    var notification = $find(Telerik.rnMessagesId);
                    var errMessage = "There was an error during the import operation. Try simplifying the content.";
                    notification.set_text(errMessage);
                    notification.show();
                }
            });
        },
        toggleTrackChanges: function (sender, args) {
            //disable track changes for initial typing, but let the user enable them if they want to
            if (sender.get_enableTrackChanges()) {
                sender.fire("EnableTrackChangesOverride");
            }
        }
    };
})(window);
 

Telerik.Web.UI.Editor.CommandList["SaveAsDocx"] =
Telerik.Web.UI.Editor.CommandList["SaveAsRtf"] =
Telerik.Web.UI.Editor.CommandList["SaveAsMarkdown"] =
Telerik.Web.UI.Editor.CommandList["SaveAsPDF"] = function (commandName, editor, args) {
    __doPostBack("", commandName);
};
Telerik.Web.UI.Editor.CommandList["Open"] = function (commandName, editor, args) {
    Telerik.openUploadDialog();
};