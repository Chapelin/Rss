
$(document).ready(function() {
    console.log("ready §§");
//    RequeteLast20("52f8be4f46485e02dc1edd2d");
    RequeteLast20("52f90d1031d5190e84aa8e65");
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
    $.each(data, function (key, val) {
        items.push(val);
    });
    
    var total = '';
    for (var i = 0; i < items.length; i++) {
        if(i%3===0) {
            total += '<div class="row">';
        }

        total += '<div class="col-lg-3">';
        total += '<h2>' + items[i].Titre+'</h2>';
        total += '<p>' + items[i].Texte+'</p>';
        total += '</div>';


        if(i%3===2) {
            total += '</div>';
        }
    }
    console.log("Total : " + total);
    $("#test").html(total);

}