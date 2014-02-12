
$(document).ready(function () {
    console.log("ready §§");
    //    RequeteLast20("52f8be4f46485e02dc1edd2d");
    RequeteLast20("52f90d1031d5190e84aa8e65");
    console.log($("#Cat"));
    $(document).on("click", "#Cat", function () {
        RequeteCategories();

    });
    $(document).on("click", "#DashBoard", function () {
        RequeteLast20();
    })
    ;
    $(document).on("click", ".categorie", function () {
        var categid = $(this).attr('id');
        console.log(categid);
        RequeteLast20ByCategorie(categid);
    })
    ;
});
function RequeteLast20ByCategorie(categid) {
    console.log("Lancement appel RequeteLast20ByCategorie");
    $.getJSON(
        "api/entree/GetLast20ByCategorie/" + categid,
        AfficherDonnes
    );
}

function RequeteLast20(categorie) {
    console.log("Lancement appel RequeteLast20ById");
    $.getJSON(
        "api/entree/GetLast20bysourceid/" + source,
        AfficherDonnes
    );
}
function RequeteLast20() {
    console.log("Lancement appel RequeteLast20");
    $.getJSON(
        "api/entree/GetLast20",
        AfficherDonnes
    );
}

function RequeteCategories() {
    console.log("Lancement appel RequeteCategories");
    $.getJSON(
        "api/categorie/get",
        AfficherCategories
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