;#DropDB TimeCoreEasyTest

#Application TimeCore.Win
#Application TimeCore.Web

*FillForm
 User Name = Admin
*Action Log In

#IfDef TimeCore.Win

*Action About...
 
*Action OK

#EndIf

#IfDef TimeCore.Web
*Action Navigation(My Details)
#EndIf
