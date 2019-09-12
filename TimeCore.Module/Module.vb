Imports Microsoft.VisualBasic
Imports System
Imports System.Text
Imports System.Linq
Imports DevExpress.ExpressApp
Imports System.ComponentModel
Imports DevExpress.ExpressApp.DC
Imports System.Collections.Generic
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.BaseImpl.PermissionPolicy
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp.Model.Core
Imports DevExpress.ExpressApp.Model.DomainLogics
Imports DevExpress.ExpressApp.Model.NodeGenerators
Imports DevExpress.ExpressApp.Xpo
Imports TimeCore.Module.DAL.TimeCore
Imports DevExpress.Xpo

' For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ModuleBase.
Public NotInheritable Class TimeCoreModule
    Inherits ModuleBase
    Public Sub New()
        InitializeComponent()
        BaseObject.OidInitializationMode = OidInitializationMode.AfterConstruction
    End Sub

    Public Overrides Function GetModuleUpdaters(ByVal objectSpace As IObjectSpace, ByVal versionFromDB As Version) As IEnumerable(Of ModuleUpdater)
        Dim updater As ModuleUpdater = New Updater(objectSpace, versionFromDB)
        Return New ModuleUpdater() {updater}
    End Function

    Public Overrides Sub Setup(application As XafApplication)
        MyBase.Setup(application)
        ' Manage various aspects of the application UI and behavior at the module level.
    End Sub
    Public Overrides Sub CustomizeTypesInfo(ByVal typesInfo As ITypesInfo)
        MyBase.CustomizeTypesInfo(typesInfo)
        CalculatedPersistentAliasHelper.CustomizeTypesInfo(typesInfo)
        'Dim xml As String = "<RecurrenceInfo Start=""04/10/2018 08:00:00"" End=""04/28/2018 08:00:00"" Id=""0cd518d1-2130-4c74-ba9b-3d2e6b0a38f5"" Periodicity=""2"" Range=""2"" Version=""1"" />"
        Dim xml As String = "<RecurrenceInfo Start=""04/10/2018 08:00:00"" End=""04/28/2018 08:00:00"" WeekDays=""6"" Id=""0cd518d1-2130-4c74-ba9b-3d2e6b0a38f5"" Range=""2"" Type=""1"" Version=""1"" />"
        'Dim ri2 As New RecurrenceInfo
        'ri2.FromXml(xml)

        Dim ri As WorkRecurenceInfo = WorkRecurenceInfo.FromXml(xml)
        'Dim calc As OccurrenceCalculator = OccurrenceCalculator.CreateInstance(ri)
        'calc.
        Dim xm = ri.ToXml()

        Dim x = 0
        'For i As Integer = 1 To 7
        '    Dim dayNum As Integer = i
        '    Dim d As TimeTableDetail = tt.TimeTableDetails.FirstOrDefault(Function(q) q.DayNum = dayNum)
        '    afc.TimeTable
        'Next


        'Dim s As New UnitOfWork()
    End Sub
End Class
