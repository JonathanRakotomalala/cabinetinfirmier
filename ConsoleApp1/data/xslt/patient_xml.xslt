<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
                xmlns:act="http://www.univ-grenoble-alpes.fr/l3miage/actes"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:cab="http://www.univ-grenoble-alpes.fr/l3miage/medical">
    <xsl:output method="xml" indent="yes" encoding="UTF-8"/>

    <xsl:variable name ="actes" select ="document('../xml/actes.xml')/act:ngap/act:actes"/>
    <xsl:param name="nom"/>
    
    <xsl:template match="/">
        <patient>  
            <xsl:apply-templates select="//cab:patients/cab:patient[cab:nom=$nom]"/>
        </patient>
    </xsl:template>

    <xsl:template match="cab:patients/cab:patient">
        <xsl:if test="cab:nom=$nom">
            <nom>   
                <xsl:value-of select="cab:nom"/>
            </nom>
            <prénom>
                <xsl:value-of select="cab:prénom"/>
            </prénom>
            <sexe>
                <xsl:value-of select="cab:sexe"/>
            </sexe>
            <adresse>
                <xsl:apply-templates select="cab:adresse"/>
            </adresse>
            <visite>
                <xsl:attribute name="date"><xsl:value-of select="cab:visite/cab:date"/></xsl:attribute>
                <xsl:apply-templates select="cab:visite"/>
            </visite>
        </xsl:if>
    </xsl:template>

    <xsl:template match="cab:adresse">
        <rue>
            <xsl:value-of select="cab:rue"/>
        </rue>
        <codePostal>
            <xsl:value-of select="cab:codePostal"/>
        </codePostal>
        <ville>
            <xsl:value-of select="cab:ville"/>
        </ville>
    </xsl:template>
    
    <xsl:template match="cab:visite">
        <xsl:variable name="int" select="cab:intervenant"/>
        <xsl:variable name="ac" select="cab:acte/@id"/>
        <intervenant>
            <nom>
                <xsl:value-of select="/cab:cabinet/cab:infirmiers/cab:infirmier[cab:id=$int]/cab:nom"/>
            </nom>
            <prénom>
                <xsl:value-of select="/cab:cabinet/cab:infirmiers/cab:infirmier[cab:id=$int]/cab:prénom"/>
            </prénom>
        </intervenant>
        <acte>
            <xsl:value-of select="$actes//act:acte[@id=$ac]/text()"/>
        </acte>
    </xsl:template>
    
</xsl:stylesheet>
    
    