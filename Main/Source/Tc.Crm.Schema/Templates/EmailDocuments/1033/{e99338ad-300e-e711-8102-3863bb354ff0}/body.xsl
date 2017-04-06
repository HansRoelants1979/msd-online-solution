﻿<?xml version="1.0" ?><xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0"><xsl:output method="text" indent="no"/><xsl:template match="/data"><![CDATA[<font face=Tahoma size=2>Dear&#160;</font>]]><xsl:choose><xsl:when test="contact/tc_salutation/@name"><xsl:value-of select="contact/tc_salutation/@name" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose><![CDATA[ <span style="font-family:Tahoma;font-size:small;"> </span><font face=Tahoma size=2>]]><xsl:choose><xsl:when test="contact/lastname"><xsl:value-of select="contact/lastname" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose><![CDATA[ ]]><xsl:choose><xsl:when test="account/name"><xsl:value-of select="account/name" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose><![CDATA[<span style="color:rgb(51, 51, 51);"> </span></font><div><font color="#333333" face=Tahoma size=2><br></font><div><font face=Tahoma size=2><span style="color:rgb(51, 51, 51);">Customer: &#160;]]><xsl:choose><xsl:when test="contact/fullname"><xsl:value-of select="contact/fullname" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose><![CDATA[ </span>]]><xsl:choose><xsl:when test="account/name"><xsl:value-of select="account/name" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose><![CDATA[<span style="color:rgb(51, 51, 51);"> </span><br style="color:rgb(51, 51, 51);"><span style="color:rgb(51, 51, 51);">Booking Reference: &#160;</span>]]><xsl:choose><xsl:when test="incident/tc_bookingreferencefreetext"><xsl:value-of select="incident/tc_bookingreferencefreetext" /></xsl:when><xsl:when test="incident/tc_bookingid/@name"><xsl:value-of select="incident/tc_bookingid/@name" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose><![CDATA[<span style="white-space:nowrap;"> </span><br style="color:rgb(51, 51, 51);"><br style="color:rgb(51, 51, 51);"></font><p style="margin-top:10px;margin-bottom:0px;padding:0px;color:rgb(51, 51, 51);"><font face=Tahoma size=2>Thanks for giving us your feedback.</font></p><p style="margin-top:10px;margin-bottom:0px;padding:0px;color:rgb(51, 51, 51);"><font face=Tahoma size=2>We're looking in to what has happened and will come back to you with an update as soon as possible.&#160;If there is anything else we can help you with then please contact us.</font></p><p style="margin-top:10px;margin-bottom:0px;padding:0px;color:rgb(51, 51, 51);"><font face=Tahoma size=2>Thanks for your patience whilst we look into your concerns.</font></p><p style="margin-top:10px;margin-bottom:0px;padding:0px;color:rgb(51, 51, 51);"><font face=Tahoma size=2>Kind regards</font></p><p style="margin-top:10px;margin-bottom:0px;padding:0px;color:rgb(51, 51, 51);"><font face=Tahoma size=2><img style="" src="http://cdn.thomascook.com/crm/email/logo/Cresta_Circle_logo.png"></font></p></div><div><font face=Tahoma size=2><span style="color:rgb(51, 51, 51);">Customer Relations</span><br style="color:rgb(51, 51, 51);"><span style="color:rgb(51, 51, 51);">Tel No: 01733 224 814</span><br style="color:rgb(51, 51, 51);"><span style="color:rgb(51, 51, 51);">Opening Hours: 9am-8pm Mon-Fri, 9am-5pm Sat.</span><br style="color:rgb(51, 51, 51);"><br style="color:rgb(51, 51, 51);"><span style="color:rgb(51, 51, 51);">Cresta</span><span style="color:rgb(51, 51, 51);">&#160;is a trading name of Thomas Cook Tour Operations Limited.</span><br style="color:rgb(51, 51, 51);"><span style="color:rgb(51, 51, 51);">Registered Office: The Thomas Cook Business Park, Coningsby Road, Peterborough, PE3 8SB</span><br style="color:rgb(51, 51, 51);"><span style="color:rgb(51, 51, 51);">Registered in England No. 3772199</span></font><font face=Tahoma size=2><br></font></div><font face="Tahoma, Verdana, Arial" size=2 style="display:inline;"></font></div>]]></xsl:template></xsl:stylesheet>