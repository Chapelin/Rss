
$(document).ready(function () {
    console.log("ready §§");
    Appel("entree", "GetLast", "", AfficherDonnes);
    $(document).on("click", "#Cat", function () {
        Appel("Categorie", "Get", "", AfficherCategories);

    });
    $(document).on("click", "#DashBoard", function() {
        Appel("entree", "GetLast", "", AfficherDonnes);
    });
    $(document).on("click", "#Sources", function () {
        Appel("source", "Get", "", AfficherSource);
    });
    $(document).on("click", ".categorie", function () {
        var categid = $(this).attr('id');
        Appel("entree", "GetLastByCategorie", categid, AfficherDonnes);
    });
    $(document).on("click", ".source", function() {
        var sourceid = $(this).attr('id');
        Appel("entree", "GetBySourceID", sourceid, AfficherDonnes);
    });

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
        total += GenererBoutonCategorie(items[i]);
        total += '</div>';

        if (i % 6 === 5) {
            total += '</div>';
        }
    }
    $("#test").html(total);
}

function AfficherSource(data) {
    var items = [];
    console.log("Reponse ok");
    $.each(data, function (key, val) {
        items.push(val);
    });

    var total = '';
    for (var i = 0; i < items.length; i++) {
        if (i % 4 === 0) {
            total += '<div class="row">';
        }
        var totCate = [];
        $.each(items[i].Categories, function(key, val) {
            totCate.push(GenererBoutonCategorie(val));
        });
        console.log("totcat : " + totCate);
        total += '<div class="col-lg-3 source" id="'+items[i].Id+'">';
        total += '<h2>' + items[i].Description + '</h2>';
        total += totCate.join("");
        total += '</div>';

        if (i % 4 === 3) {
            total += '</div>';
        }
    }
    $("#test").html(total);
}


function AfficherDonnes(data) {
    var items = [];
    console.log("Reponse ok");
    $.each(data, function (key, val) {
        items.push(GenererContenuEntree(val));
    });
    
    
    $("#test").html(items.join(""));
}

function GenererBoutonCategorie(categorie) {
    return '<button type="button" class="btn btn-default categorie" id="' + categorie.Id + '">' + categorie.Description + '</button>';
}

function GenererContenuEntree(entree) {
    var date = new Date(entree.Date);
    var M = date.getMonth() + 1;
    var d = date.getDay();
    var h = date.getHours();
    var m = date.getMinutes();
    var s = date.getSeconds();
    var dateformatee = (d <= 9 ? '0' + d : d) + "/" + (M <= 9 ? '0' + M : M) + "/" + date.getFullYear() + " - " + (h <= 9 ? '0' + h : h) + ":" + (m <= 9 ? '0' + m : m) + ":" + (s <= 9 ? '0' + s : s);
    return '<div class="row"><div class="col-lg-12"><a href="' + entree.Link + '">' + dateformatee + ' - ' + entree.Titre + '</a></div></div>';
}