
function afficherFacture(prenom,nom,actes){
    var a="Facture pour : " + prenom + " " + nom;
    return a;
}
function openFacture (prenom, nom, actes) {
    var width = 500;
    var height = 300;
    if (window.innerWidth) {
        var left = (window.innerWidth-width) /2;
        var top = (window.innerHeight-height) /2;
    } else {
        var left = (document.body.clientWidth-width) /2;
        var top = (document.body.clientHeight-height) /2;
    }
    var factureWindow = window.open('file:///C:/Users/jon4t/RiderProjects/ConsoleApplication3/ConsoleApplication3/Properties/data/html/page1.html', 'facture', 'menubar=yes, scrollbars= yes, top='+top+', left='+left+', width='+width+', height='+height +'');
    var factureText = afficherFacture(prenom, nom, actes);
    factureWindow.document.write(factureText) ;
}