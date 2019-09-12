Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.Xpo.Metadata
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Reflection
Imports DevExpress.Persistent.Base.General
Imports DevExpress.Persistent.Base

Namespace DAL.TimeCore
    <DefaultClassOptions()>
    Partial Public Class TimeTable
        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub
        Public Overrides Sub AfterConstruction()
            MyBase.AfterConstruction()
            Me.Kind = TimeTableKind.Weeks
            Me.Frequency = 1
            Me.FirstDayOfWeek = DayOfWeek.Monday

        End Sub

        Public Sub GenerateEmptyDetails()
            Dim totalDays As Integer = Me.Frequency
            If Me.Kind = TimeTableKind.Weeks Then
                totalDays *= 7
            End If
            For i As Integer = 1 To totalDays
                Dim d As New TimeTableDetail(Me.Session)
                d.TimeTable = Me
                Me.TimeTableDetails.Add(d)
                d.DayNum = i
            Next
        End Sub


        'Public Function GetRecurenceInfos(startDate As Date, endDate As Date) As Dictionary(Of TimeSlot, Dictionary(Of Guid, String))
        '    Dim result As Dictionary(Of TimeSlot, Dictionary(Of Guid, String)) =
        '        (From q In Me.TimeTableDetails
        '         Select ts = q.TimeSlot
        '         Order By ts.TimeStart).ToDictionary(Function(q) q, Function(q) Me.GetRecurenceInfos(q, startDate, endDate))
        '    Return result
        'End Function

        'Public Function GetRecurenceInfos(ts As TimeSlot, startDate As Date, endDate As Date) As Dictionary(Of Guid, String)
        '    Dim result As New Dictionary(Of Guid, String)

        '    Dim details = From q In Me.TimeTableDetails
        '                  Where q.TimeSlot.Oid = ts.Oid
        '                  Order By q.DayNum

        '    Select Case Me.Kind
        '        Case TimeTableKind.Weeks
        '            Dim dic As New Dictionary(Of Integer, Date)
        '            For i As Integer = 1 To Me.Frequency
        '                dic.Add(i, startDate.AddDays((i - 1) * 7))
        '            Next

        '            For Each kv In dic
        '                Dim fdw As DayOfWeek = DayOfWeek.Sunday
        '                Dim firstDayOfWeek As Integer = Convert.ToInt32(Me.FirstDayOfWeek)
        '                If firstDayOfWeek = 0 Then
        '                    firstDayOfWeek = 7
        '                End If
        '                Dim wDays As List(Of WeekDays) =
        '                    (From q In details
        '                     Where q.DayNum >= (1 + (7 * (kv.Key - 1))) AndAlso q.DayNum < (1 + (7 * kv.Key))
        '                     Select dn = q.DayNum Mod 7
        '                     Select If(dn = 0, WeekDays.Sunday, DirectCast(Convert.ToInt32(2 ^ dn), WeekDays))).ToList()

        '                Dim wDay As WeekDays = wDays.FirstOrDefault

        '                For i As Integer = 1 To wDays.Count - 1
        '                    wDay = wDay Or wDays(i)
        '                Next
        '                Dim id As Guid = Guid.NewGuid()
        '                Dim xml As String = "<RecurrenceInfo "
        '                xml &= "Start=""{0}"" " & kv.Value.Date.AddHours(ts.TimeStart.Hours).AddMinutes(ts.TimeStart.Minutes).ToString("MM/dd/yyyy HH:mm:ss")
        '                xml &= "End=""{0}"" " & endDate.Date.AddHours(ts.TimeStart.Hours).AddMinutes(ts.TimeStart.Minutes).ToString("MM/dd/yyyy HH:mm:ss")
        '                'WeekDays="2"
        '                xml &= String.Format("Id=""{0}"" ", id.ToString())
        '                xml &= String.Format("WeekDays=""{0}"" ", Convert.ToInt32(wDay))
        '                If Me.Frequency <> 1 Then
        '                    xml &= String.Format("Periodicity=""{0}"" ", Me.Frequency)
        '                End If

        '                xml &= "Range=""2"" "
        '                xml &= "Type=""1"" "
        '                xml &= "Version=""1"" />"
        '                result.Add(id, xml)
        '                '<RecurrenceInfo Start="04/10/2018 08:00:00" End="04/25/2018 08:00:00" WeekDays="60" Id="0cd518d1-2130-4c74-ba9b-3d2e6b0a38f5" Range="2" Type="1" Version="1" />
        '            Next
        '        Case TimeTableKind.Days
        '            Dim id As Guid = Guid.NewGuid()
        '            Dim xml As String = "<RecurrenceInfo "
        '            xml &= "Start=""{0}"" " & startDate.Date.AddHours(ts.TimeStart.Hours).AddMinutes(ts.TimeStart.Minutes).ToString("MM/dd/yyyy HH:mm:ss")
        '            xml &= "End=""{0}"" " & endDate.Date.AddHours(ts.TimeStart.Hours).AddMinutes(ts.TimeStart.Minutes).ToString("MM/dd/yyyy HH:mm:ss")
        '            'WeekDays="2"
        '            xml &= String.Format("Id=""{0}"" ", id.ToString())
        '            'xml &= String.Format("WeekDays=""{0}"" ", Convert.ToInt32(wDay))
        '            If Me.Frequency <> 1 Then
        '                xml &= String.Format("Periodicity=""{0}"" ", Me.Frequency)
        '            End If

        '            xml &= "Range=""2"" "
        '            xml &= "Version=""1"" />"
        '            result.Add(id, xml)

        '    End Select

        '    Return result
        'End Function


        'Public Function GetWorkRecurenceInfos(ts As TimeSlot, startDate As Date, endDate As Date) As Dictionary(Of Guid, WorkRecurenceInfo)
        '    startDate = startDate.Date
        '    endDate = endDate.Date
        '    Dim result As New Dictionary(Of Guid, WorkRecurenceInfo)

        '    Dim details = From q In Me.TimeTableDetails
        '                  Where q.TimeSlot.Oid = ts.Oid
        '                  Order By q.DayNum

        '    Select Case Me.Kind
        '        Case TimeTableKind.Weeks
        '            Dim dic As New Dictionary(Of Integer, Date)
        '            For i As Integer = 1 To Me.Frequency
        '                dic.Add(i, startDate.AddDays((i - 1) * 7))
        '            Next

        '            For Each kv In dic
        '                Dim fdw As DayOfWeek = DayOfWeek.Sunday
        '                Dim firstDayOfWeek As Integer = Convert.ToInt32(Me.FirstDayOfWeek)
        '                If firstDayOfWeek = 0 Then
        '                    firstDayOfWeek = 7
        '                End If
        '                Dim wDays As List(Of WeekDays) =
        '                    (From q In details
        '                     Where q.DayNum >= (1 + (7 * (kv.Key - 1))) AndAlso q.DayNum < (1 + (7 * kv.Key))
        '                     Select dn = q.DayNum Mod 7
        '                     Select If(dn = 0, WeekDays.Sunday, DirectCast(Convert.ToInt32(2 ^ dn), WeekDays))).ToList()

        '                Dim wDay As WeekDays = wDays.FirstOrDefault

        '                For i As Integer = 1 To wDays.Count - 1
        '                    wDay = wDay Or wDays(i)
        '                Next

        '                Dim id As Guid = Guid.NewGuid()

        '                Dim ri As New WorkRecurenceInfo(id, startDate, ts.TimeStart, endDate, ts.TimeEnd, Me.Frequency, wDay)

        '                result.Add(id, ri)
        '                '<RecurrenceInfo Start="04/10/2018 08:00:00" End="04/25/2018 08:00:00" WeekDays="60" Id="0cd518d1-2130-4c74-ba9b-3d2e6b0a38f5" Range="2" Type="1" Version="1" />
        '            Next
        '        Case TimeTableKind.Days
        '            Dim id As Guid = Guid.NewGuid()
        '            Dim ri As New WorkRecurenceInfo(id, startDate, ts.TimeStart, endDate, ts.TimeEnd, Me.Frequency)

        '            result.Add(id, ri)

        '    End Select

        '    Return result
        'End Function

        'Public Function GetWorkShifts(affectation As TimeTableAffectation) As Dictionary(Of TimeSlot, Dictionary(Of Guid, WorkShift))
        '    Dim result As Dictionary(Of TimeSlot, Dictionary(Of Guid, WorkShift)) =
        '        (From q In Me.TimeTableDetails
        '         Select ts = q.TimeSlot
        '         Order By ts.TimeStart).ToDictionary(Function(q) q, Function(q) Me.GetWorkShifts(q, affectation))
        '    Return result
        'End Function

        'Public Function GetWorkShifts(ts As TimeSlot, affectation As TimeTableAffectation) As Dictionary(Of Guid, WorkShift)
        '    'Dim ts As TimeSlot = detail.TimeSlot
        '    If ts Is Nothing Then Return Nothing
        '    Dim startDate As Date = affectation.StartDay.Date
        '    Dim endDate As Date = affectation.EndDay.Date
        '    Dim result As New Dictionary(Of Guid, WorkShift)

        '    Dim details = From q In Me.TimeTableDetails
        '                  Where q.TimeSlot.Oid = ts.Oid
        '                  Order By q.DayNum

        '    Select Case Me.Kind
        '        Case TimeTableKind.Weeks
        '            Dim dic As New Dictionary(Of Integer, Date)
        '            For i As Integer = 1 To Me.Frequency
        '                dic.Add(i, startDate.AddDays((i - 1) * 7))
        '            Next

        '            For Each kv In dic
        '                Dim fdw As DayOfWeek = DayOfWeek.Sunday
        '                Dim firstDayOfWeek As Integer = Convert.ToInt32(Me.FirstDayOfWeek)
        '                If firstDayOfWeek = 0 Then
        '                    firstDayOfWeek = 7
        '                End If
        '                Dim wDays As List(Of WeekDays) =
        '                    (From q In details
        '                     Where q.DayNum >= (1 + (7 * (kv.Key - 1))) AndAlso q.DayNum < (1 + (7 * kv.Key))
        '                     Select dn = q.DayNum Mod 7
        '                     Select If(dn = 0, WeekDays.Sunday, DirectCast(Convert.ToInt32(2 ^ dn), WeekDays))).ToList()

        '                Dim wDay As WeekDays = wDays.FirstOrDefault

        '                For i As Integer = 1 To wDays.Count - 1
        '                    wDay = wDay Or wDays(i)
        '                Next
        '                Dim ws As New WorkShift(Me.Session)

        '                ws.SetUp(ts, affectation, wDay)

        '                result.Add(ws.Oid, ws)
        '                '<RecurrenceInfo Start="04/10/2018 08:00:00" End="04/25/2018 08:00:00" WeekDays="60" Id="0cd518d1-2130-4c74-ba9b-3d2e6b0a38f5" Range="2" Type="1" Version="1" />
        '            Next
        '        Case TimeTableKind.Days
        '            Dim ws As New WorkShift(Me.Session)
        '            ws.SetUp(ts, affectation)
        '            result.Add(ws.Oid, ws)

        '    End Select

        '    Return result
        'End Function

    End Class

End Namespace
