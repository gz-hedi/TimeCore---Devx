Imports System.Linq
Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp
Imports TimeCore.Module.DAL.TimeCore

Public Class SampleData

    Public Shared Sub CreateSampleData(os As IObjectSpace)
        If os.FindObject(Of TimeTable)(CriteriaOperator.Parse("Code='TT2'")) IsNot Nothing Then
            Return
        End If

        Dim tt As TimeTable = os.CreateObject(Of TimeTable)
        tt.Code = "TT2"
        tt.Kind = TimeTableKind.Weeks
        tt.Frequency = 2
        tt.FirstDayOfWeek = DayOfWeek.Monday



        Dim ts As WorkTimeSlot = os.CreateObject(Of WorkTimeSlot)
        ts.TimeStart = TimeSpan.FromHours(8)
        ts.TimeEnd = TimeSpan.FromHours(12)

        Dim ts2 As WorkTimeSlot = os.CreateObject(Of WorkTimeSlot)
        ts2.TimeStart = TimeSpan.FromHours(14)
        ts2.TimeEnd = TimeSpan.FromHours(18)


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

        afc.StartDay = New Date(2019, 9, 1)
        afc.EndDay = New Date(2020, 1, 1).AddDays(-1)
        Dim l = afc.GetWorkShifts()


        os.CommitChanges()

    End Sub

End Class
