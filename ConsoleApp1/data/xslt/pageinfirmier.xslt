<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
                xmlns:act="http://www.univ-grenoble-alpes.fr/l3miage/actes"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:cab="http://www.univ-grenoble-alpes.fr/l3miage/medical">
    <xsl:param name ="destinedId"/>
    <xsl:output method="html"/>

    <!-- Variables temporaires  ; elles seront remplacées pour permettre 
   l'envoi de ces informations depuis le programme appelant vers la feuille xslt 
   sous forme de paramètres -->
    
    <xsl:variable name="nomprog">Jonathan Rakotomalala</xsl:variable>
    <xsl:variable name="date">2024-09-24</xsl:variable>
    <xsl:variable name ="actes" select ="document('actes.xml',/)/act:ngap/act:actes"/>

    
    
    <xsl:template match="/">
        <html>
            <head>
                <script>
                    function openFacture(prenom, nom, actes){
                    var width = 500;
                    var height = 300;
                    if (window.innerWidth) {
                    var left = (window.innerWidth-width)/2;
                    var top = (window.innerHeight-height)/2;
                    } else {
                    var left = (document.body.clientWidth-width) /2;
                    var top = (document.body.clientHeight-height) /2;
                    }
                    var factureWindow = window.open("", "facture", 'menubar=yes, scrollbars= yes, top='+ top +', left='+ left +', width='+ width +', height='+height +'');
                    var factureText ="Facture pour : " + prenom + " " + nom;
                    factureWindow.document.write(factureText) ;
                    }
                </script>
                <title>
                    Page du cabinet
                </title>
                <style>
                    body {background-color: skyblue;}
                    h1   {color: maroon;}
                    h2 {color: olive;}
                    p    {color: black;}
                    table, th,  td {
                    border: 1px solid white;
                    border-collapse: collapse;
                    }
                    ol{border:1px solid}
                    .infopage{margin-top:10px;border-top: 1px solid}
                    .intervention{width:600px;border: 1px solid;background-color:#e8decb}
                    .soins{border-bottom:1px solid}
                    .listesoins{background-color:#ebe7df}
                    input{width:580px;height:50px;margin-bottom:20px;margin-left:10px}
                    .titre{border-bottom:1px solid}
                </style>
            </head>
            <body>
                <div class="Page">
                    <div class="Messagedebut">
                        <h1>Bonjour <xsl:value-of select="//cab:infirmiers/cab:infirmier[cab:id=$destinedId]/cab:prénom"/>,<xsl:value-of select="$destinedId" /></h1>
                        <p>Aujourd'hui vous avez <xsl:value-of select="count(//cab:cabinet/cab:patients/cab:patient/cab:visite[cab:intervenant=$destinedId])"/> patients</p>
                    </div>
                    <div class="interventions">
                    <h2><span class="titre">Interventions</span></h2>
                    </div>
                    <xsl:apply-templates select="//cab:cabinet/cab:patients/cab:patient[cab:visite/cab:intervenant=$destinedId]"/>
                </div>
                <div class="infopage">
                    <xsl:call-template name="InfoDeBase">
                        <xsl:with-param name="dateDuJour" select = "$date" />
                        <xsl:with-param name="nomProgrammeur" select = "$nomprog" />
                    </xsl:call-template>
                </div>
            </body>
        </html>
    </xsl:template>
    
    <xsl:template match="cab:patient">
        <xsl:if test="cab:visite/cab:intervenant = $destinedId">
        <div class="intervention">
            <xsl:variable name="name" select="cab:nom"/>
            <xsl:variable name="fname" select="cab:prénom"/>
            <xsl:variable name="acts" select="cab:visite[cab:intervenant=$destinedId]//cab:acte/@id"/>
            <p>-Nom: <xsl:value-of select="cab:nom"/><br/>
                -Adresse: <xsl:value-of select="cab:adresse"/><br/>
                </p>
            <p><span class="soins" >-Soins:</span><br/>
                <xsl:apply-templates select="cab:visite[cab:intervenant=$destinedId]/cab:acte"/>
            </p>
            <!-- Création du bouton -->
            <xsl:element name="input">
                <xsl:attribute name="type">button</xsl:attribute>
                <xsl:attribute name="onclick">
                    openFacture('<xsl:value-of select="$fname"/>','<xsl:value-of select ="$name"/>','<xsl:value-of select ="$acts"/>')
                </xsl:attribute>
                <xsl:attribute name="value">facture</xsl:attribute>
            </xsl:element>
        </div>
        </xsl:if>
    </xsl:template>
    
    <xsl:template match="cab:acte">
        <div class="listesoins">
        <ol>
            <ul style="list-style-type:square">
                <li>Id: <xsl:value-of select="@id"/></li>
                <li>Type: <xsl:value-of select="$actes/act:acte/@type"/></li>
                <li>Clé: <xsl:value-of select="$actes/act:acte/@clé"/></li>
                <li>Coef: <xsl:value-of select="$actes/act:acte/@coef"/></li>
            </ul>
        </ol>
        </div>
    </xsl:template>

    <!-- Template pour les infos generales -->
    <xsl:template name="InfoDeBase">
        <xsl:param name="dateDuJour"/>
        <xsl:param name="nomProgrammeur"/>
        <p>Site mis à jour le <xsl:value-of select="$dateDuJour"/></p>
        <p>Programmeur : <xsl:value-of select="$nomProgrammeur"/></p>
    </xsl:template>

</xsl:stylesheet>
    
    