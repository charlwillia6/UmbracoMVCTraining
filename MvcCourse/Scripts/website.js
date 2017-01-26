$(function () {
    $('#doc-history').click(function () {    
        var id = $(this).attr('data-doc-id');

        $.get("/umbraco/api/DocumentationHistory/Versions/" + id,
            function (data) {
                $(data).each(function (i, item) {
                    var d = new Date(item.publishDate);
             
                    $("#doc-history-list")
                        .append("<dt><a class=\"revision-history\" data-version='" + item.versionId  + "'>" + item.name + "</a><dt>")
                        .append("<dd><small>" + d.getUTCDate() + "/" + (d.getUTCMonth() +1) + " - " + d.getFullYear() + "</small></dd>");
                });
                bindRollbackClickEvents();
            });

        return false;
    });
});

function bindRollbackClickEvents() {

    $('.revision-history').click(function () {
        alert('revision clicked');
        //get version from data attribute of clicked item
        var version = $(this).data("version");

        // maybe add some kind of are you sure you want to roll back to some kind of older version ?
        $.get("/umbraco/api/DocumentationHistory/PublishVersion/?version=" + version,
            function (data) {      
                // we've triggered the roll back to the previous version
                // we now need to reload the page, the url might have changed 
                window.location.href = data;
            });
    });
}
