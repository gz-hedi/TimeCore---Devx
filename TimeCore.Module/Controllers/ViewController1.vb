Imports Microsoft.VisualBasic
Imports System
Imports System.Linq
Imports System.Text
Imports DevExpress.ExpressApp
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Utils
Imports DevExpress.ExpressApp.Layout
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Templates
Imports DevExpress.Persistent.Validation
Imports DevExpress.ExpressApp.SystemModule
Imports DevExpress.ExpressApp.Model.NodeGenerators
Imports TimeCore.Module.DAL.TimeCore

' For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
Partial Public Class ViewController1
    Inherits ViewController
    Public Sub New()
        InitializeComponent()
        ' Target required Views (via the TargetXXX properties) and create their Actions.
    End Sub
    Protected Overrides Sub OnActivated()
        MyBase.OnActivated()
        ' Perform various tasks depending on the target View.
    End Sub
    Protected Overrides Sub OnViewControlsCreated()
        MyBase.OnViewControlsCreated()
        ' Access and customize the target View control.
    End Sub
    Protected Overrides Sub OnDeactivated()
        ' Unsubscribe from previously subscribed events and release other references and resources.
        MyBase.OnDeactivated()
    End Sub

    Private Sub SimpleAction1_Execute(sender As Object, e As SimpleActionExecuteEventArgs) Handles SimpleAction1.Execute
        Dim os As IObjectSpace = Application.CreateObjectSpace()
        Dim ts As WorkTimeSlot = os.CreateObject(Of WorkTimeSlot)
        ts.TimeStart = TimeSpan.FromHours(8)
        ts.TimeEnd = TimeSpan.FromHours(12)

        Dim ts2 As WorkTimeSlot = os.CreateObject(Of WorkTimeSlot)
        ts2.TimeStart = TimeSpan.FromHours(14)
        ts2.TimeEnd = TimeSpan.FromHours(18)

        Dim tt As TimeTable = os.CreateObject(Of TimeTable)
        tt.Code = "TT2"
        tt.Kind = TimeTableKind.Weeks
        tt.Frequency = 2
        tt.FirstDayOfWeek = DayOfWeek.Monday

        tt.GenerateEmptyDetails()

        For i As Integer = 1 To 5
            Dim dayNum As Integer = i
            Dim d As TimeTableDetail = tt.TimeTableDetails.FirstOrDefault(Function(q) q.DayNum = dayNum)
            d.TimeSlot = ts

        Next
        For i As Integer = 8 To 13
            Dim dayNum As Integer = i
            Dim d As TimeTableDetail = tt.TimeTableDetails.FirstOrDefault(Function(q) q.DayNum = dayNum)
            d.TimeSlot = ts2

        Next
        Dim u As DAL.TimeCore.User = os.CreateObject(Of DAL.TimeCore.User)
        u.Code = "HEDI"

        Dim afc As TimeTableAffectation = os.CreateObject(Of TimeTableAffectation)

        afc.User = u
        afc.TimeTable = tt

        afc.StartDay = New Date(2019, 10, 1)
        afc.EndDay = New Date(2020, 1, 1).AddDays(-1)
        Dim l = afc.GetWorkShifts()


        os.CommitChanges()

    End Sub

    Private Sub PopupActionShowCompatibleWorkShifts_CustomizePopupWindowParams(sender As Object, e As CustomizePopupWindowParamsEventArgs)

    End Sub
End Class
