<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:variable name="nomprog">Jonathan Rakotomalala</xsl:variable>
    <xsl:variable name="date">2024-09-24</xsl:variable>

<xsl:output method="html" indent="yes"/>
    <xsl:template match="/">
        <html>
            <head>
                <title>Page du patient</title>
                <link rel="stylesheet" type="text/css" href="nompatient.css"/>
            </head>
            <body>
                <div class="patient">
                    <xsl:apply-templates select="//patient"/>
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
    <xsl:template match="patient">
        <h1>Patient:</h1>
     <p>
            -nom: <xsl:value-of select="nom"/><br/>
            -prénom: <xsl:value-of select="prénom"/><br/>
            -adresse: <xsl:value-of select="adresse"/><br/>
     </p>
            <h1>Visite(s):</h1>
            <div class="visite">
                <p>
                    <xsl:apply-templates select="visite"/>
                </p>
            </div>
    </xsl:template>

    <xsl:template match="visite">
        -date: <xsl:value-of select="@date"/><br/>
        -intervenant: <xsl:value-of select="intervenant/nom"/>&#160;<xsl:value-of select="intervenant/prénom"/><br/>
        -acte: <xsl:value-of select="acte"/><br/>
    </xsl:template>
    
    <!-- Template pour les infos generales -->
    <xsl:template name="InfoDeBase">
        <xsl:param name="dateDuJour"/>
        <xsl:param name="nomProgrammeur"/>
        <p>Site mis à jour le <xsl:value-of select="$dateDuJour"/></p>
        <p>Programmeur : <xsl:value-of select="$nomProgrammeur"/></p>
    </xsl:template>
</xsl:stylesheet>