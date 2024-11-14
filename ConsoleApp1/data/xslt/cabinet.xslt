<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:cab="http://www.univ-grenoble-alpes.fr/l3miage/medical">
    <xsl:output method="html"/>

    <!-- Variables temporaires  ; elles seront remplacées pour permettre 
   l'envoi de ces informations depuis le programme appelant vers la feuille xslt 
   sous forme de paramètres -->
    <xsl:variable name="nomprog">Jonathan Rakotomalala</xsl:variable>
    <xsl:variable name="date">2024-09-24</xsl:variable>  
    
    
    <xsl:template match="/">
        <html>
            <head>
                <title>
                    Page du cabinet
                </title>
                <style>
                    body {background-color: maroon;}
                    h1   {color: red;}
                    h2 {color: #d4ac0d ;}
                    p    {color: silver;}
                    table, th,  td {
                    border: 1px solid white;
                    border-collapse: collapse;
                    }
                    th, td {
                    background-color: #d35400;
                    }
                </style>
            </head>
            <body>
                <div>
                    <h1>Page du cabinet</h1><br/>
                    <p>Il y a <xsl:value-of select="count(//cab:cabinet/cab:infirmiers/cab:infirmier)"/> infirmiers dans le cabinet</p>
                    <p>et <xsl:value-of select="count(//cab:cabinet/cab:patients/cab:patient)"/> patient(s)</p>
                    <h2>infirmiers dans le cabinet</h2>
                    <xsl:apply-templates select="//cab:cabinet/cab:infirmiers"/>
                    <h2>patients dans le cabinet</h2>
                    <xsl:apply-templates select="//cab:cabinet/cab:patients"/>
                    <h2>interventions</h2>
                    <table>
                        <thread>
                        <tr><th>Patient nom</th><th>Patient Prénom</th><th>date</th><th>Intervenant</th><th>Intervention</th><th>Type intervention</th></tr>
                        </thread>
                        <tbody>
                            <xsl:apply-templates select="//cab:cabinet/cab:patients/cab:patient/cab:visite "/>
                        </tbody>
                    </table>
                </div>
                <div>
                    <xsl:call-template name="InfoDeBase">
                        <xsl:with-param name="dateDuJour" select = "$date" />
                        <xsl:with-param name="nomProgrammeur" select = "$nomprog" />
                    </xsl:call-template>
                </div>                
            </body>
        </html>
    </xsl:template>
    
    <!--head tableau-->
    <xsl:template match="cab:cabinet/cab:patients|cab:cabinet/cab:infirmiers" >
        <table>
            <tr><th>Nom</th><th>Prénom</th></tr>
            <xsl:apply-templates select="cab:patient|cab:infirmier">
            </xsl:apply-templates>
        </table>
    </xsl:template>
    
    
    <!-- Template pour l'affichage des lignes -->
    <xsl:template match="cab:patient|cab:infirmier">
        <tr>
            <td><xsl:value-of select="cab:nom"/></td>
            <td><xsl:value-of select="cab:prenom"/></td>
        </tr>
    </xsl:template>


    <xsl:template match="cab:cabinet/cab:patients/cab:patient/cab:visite">
            <xsl:apply-templates select="cab:acte">
            </xsl:apply-templates>
    </xsl:template>
    
    <xsl:template match="cab:acte">
        <tr>
            <td><xsl:value-of select="../../cab:nom"/></td>
            <td><xsl:value-of select="../../cab:prenom"/></td>
            <td><xsl:value-of select="../cab:date"/></td>
            <td><xsl:value-of select="../cab:intervenant"/></td>
            <td><xsl:value-of select="@id"/></td>
            <td><xsl:value-of select="@type"/></td>
        </tr>
    </xsl:template>
    
    <!-- Template pour les infos generales -->
    <xsl:template name="InfoDeBase">
        <xsl:param name="dateDuJour"/>
        <xsl:param name="nomProgrammeur"/>
        <p>Site mis à jour le <xsl:value-of select="$dateDuJour"/></p>
        <p>Programmeur : <xsl:value-of select="$nomProgrammeur"/></p>
    </xsl:template>
    
</xsl:stylesheet>
    
    