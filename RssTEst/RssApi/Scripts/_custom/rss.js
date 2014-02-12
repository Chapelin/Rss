
$(document).ready(function() {
    console.log("ready §§");
    RequeteLast20("52f8be4f46485e02dc1edd2d");
});
function RequeteLast20(source) {
    console.log("Lancement appel");
    $.getJSON(
        "api/entree/GetLast20bysourceid/"+source,
        AfficherDonnes
    );
}


function AfficherDonnes(data) {
    var items = [];
    console.log("Reponse ok");
    console.log(data);
    $.each(data, function(key, val) {
        items.push(val.Titre);
    });
    console.log("Test : "+$("#test"));
    $("#test").html(items.join("<br\>"));

}