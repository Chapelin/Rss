
$(document).ready(function () {
    console.log("ready §§");
    Appel("entree", "GetLast20", "", AfficherDonnes);
    $(document).on("click", "#Cat", function () {
        Appel("Categorie", "Get", "", AfficherCategories);

    });
    $(document).on("click", "#DashBoard", function () {
        Appel("entree", "GetLast20", "", AfficherDonnes);
    })
    ;
    $(document).on("click", ".categorie", function () {
        var categid = $(this).attr('id');
        Appel("entree", "GetLast20ByCategorie", categid, AfficherDonnes);
    })
    ;
});

function Appel(type, methode, valeur, callback) {
    var url = "api/"+type + "/" + methode;
    if (valeur != "")
        url += "/" + valeur;

    console.log("Appel " + url);
    $.getJSON(
        url,
        callback
    );
    
}

function AfficherCategories(data) {
    var items = [];
    console.log("Reponse ok");
    var total = '';
    $.each(data, function (key, val) {
        items.push(val);
    });
    for (var i = 0; i < items.length; i++) {
        if (i % 6 === 0) {
            total += '<div class="row">';
        }

        total += '<div class="col-lg-2" >';
        total += '<button type="button" class="btn btn-default categorie" id="'+items[i].Id+'">' + items[i].Description + '</button>';
        total += '</div>';

        if (i % 6 === 5) {
            total += '</div>';
        }
    }
    console.log($("#test"));
    $("#test").html(total);
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
        if(i%4===0) {
            total += '<div class="row">';
        }

        total += '<div class="col-lg-3">';
        total += '<h2>' + items[i].Titre+'</h2>';
        total += '<p>' + items[i].Texte+'</p>';
        total += '</div>';

        if(i%4===3) {
            total += '</div>';
        }
    }
    $("#test").html(total);

}