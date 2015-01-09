function downloadReagents() {
    $.get("/Reagent/List", function (data) {
        var options = $("#reagentsSelect");
        options.find('option').remove();
        $.each(data, function () {
            options.append($("<option />").val(this.ID).text(this.Name));
        });
    });
}

$(function () {
    downloadReagents();

    $("#addReagent").click(function () {
        if ($('#reagentsSelect option').size() == 0) {
            alert("TUKŠS! Ko nu? Rimtāk ērzeli!");
            return;
        }

        $("#reagents").val(
            $("#reagents").val() + $("#reagentsSelect option:selected").val() + "\r\n");

        $("#reagentsDisplayNames").html(
            $("#reagentsDisplayNames").html() + $("#reagentsSelect option:selected").text() + "<br/>");

        $("#reagentsSelect option:selected").remove();
    });

    $("#startOverIGiveUp").click(function () {
        downloadReagents();
        $("#reagents").val("");
        $("#reagentsDisplayNames").html("");
    });
});