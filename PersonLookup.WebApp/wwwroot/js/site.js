// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function GetAllPeople() {
    $.get('/api/Person', {}, function (data) {
        var tblPeople = $("#tblPeople");
        $.each(data, function (index, item) {
            var tr = $("<tr></tr>");
            tr.html(("<td>" + item.id + "</td>")
                + " " + ("<td>" + item.name + "</td>"));
            tblPeople.append(tr);
        });
    });
}