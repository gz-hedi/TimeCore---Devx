﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Xpo.Metadata
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Reflection
Namespace DAL.TimeCore

    Partial Public Class Department
        Inherits DevExpress.Persistent.BaseImpl.BaseObject
        <Association("DepartmentUserReferencesDepartement"), Aggregated()>
        Public ReadOnly Property DepartmentUsers() As XPCollection(Of DepartmentUser)
            Get
                Return GetCollection(Of DepartmentUser)(NameOf(DepartmentUsers))
            End Get
        End Property
    End Class

End Namespace