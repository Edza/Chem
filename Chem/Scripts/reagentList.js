$(function () {
    $.get("../reagent/list", function (data) {
        var options = $("#reagentsSelect");
        options.find('option').remove();
        $.each(data, function () {
            options.append($("<option />").val(this.ID).text(this.Name));
        });
    });

    $("#addReagent").click(function () {
        $("#reagents").val($("#reagents").val() + "\r\n" + $("#reagentsSelect option:selected").val());
        $("#reagentsDisplayNames").text($("#reagentsDisplayNames").text() + $("#reagentsSelect option:selected").text());
    });
});