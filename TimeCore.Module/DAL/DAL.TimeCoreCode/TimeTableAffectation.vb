Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.Xpo.Metadata
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Reflection
Imports DevExpress.Persistent.Base
Imports DevExpress.XtraScheduler

Namespace DAL.TimeCore
    <DefaultClassOptions()>
    Partial Public Class TimeTableAffectation
        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub
        Public Overrides Sub AfterConstruction()
            MyBase.AfterConstruction()
        End Sub
        <Association("WorkShiftReferencesTimeTableAffectation"), Aggregated()>
        Public ReadOnly Property WorkShifts() As XPCollection(Of WorkShift)
            Get
                Return GetCollection(Of WorkShift)(NameOf(WorkShifts))
            End Get
        End Property
        'Public Function GetRecurenceInfos() As Dictionary(Of TimeSlot, Dictionary(Of Guid, String))
        '    Return Me.TimeTable.GetRecurenceInfos(Me.StartDay, Me.EndDay)
        'End Function


        Public Function GetWorkShifts() As Dictionary(Of WorkTimeSlot, Dictionary(Of Guid, WorkShift))
            Dim tss As List(Of WorkTimeSlot) = (From q In Me.TimeTable.TimeTableDetails
                                                Where q.TimeSlot IsNot Nothing
                                                Select ts = q.TimeSlot Distinct
                                                Order By ts.TimeStart).ToList()
            Dim result As Dictionary(Of WorkTimeSlot, Dictionary(Of Guid, WorkShift)) =
                tss.ToDictionary(Function(q) q, Function(q) Me.GetWorkShifts(q))

            'For Each q In tss
            '    Me.GetWorkShifts(q)
            'Next

            Return result
        End Function
        Public Shared Function GetNextWeekday(ByVal start As Date, ByVal day As DayOfWeek) As Date
            ' The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            Dim daysToAdd As Integer = (CInt(day) - CInt(start.DayOfWeek) + 7) Mod 7
            Return start.AddDays(daysToAdd)
        End Function
        'Public Shared Function DayOfWeek2WeekDays(dw As DayOfWeek) As WeekDays
        '    If dw = DayOfWeek.Sunday Then Return WeekDays.Sunday

        '    Return DirectCast(Convert.ToInt32(2 ^ Convert.ToInt32(dw)), WeekDays)
        'End Function
        'Public Shared Function WeekDays2DayOfWeek(dw As WeekDays) As DayOfWeek
        '    Select Case dw
        '        Case WeekDays.Monday
        '            Return DayOfWeek.Monday
        '        Case WeekDays.Tuesday
        '            Return DayOfWeek.Tuesday
        '        Case WeekDays.Wednesday
        '            Return DayOfWeek.Wednesday
        '        Case WeekDays.Thursday
        '            Return DayOfWeek.Thursday
        '        Case WeekDays.Friday
        '            Return DayOfWeek.Friday
        '        Case WeekDays.Saturday
        '            Return DayOfWeek.Saturday
        '        Case WeekDays.Sunday
        '            Return DayOfWeek.Sunday

        '    End Select

        '    Return WeekDays.EveryDay
        'End Function

        'Public Shared Function DayNum2WeekDays(ByVal dayNum As Integer, fdw As DayOfWeek) As WeekDays
        '    Dim dn As Integer = dayNum Mod 7
        '    If dn = 1 Then Return DayOfWeek2WeekDays(fdw)
        '    If fdw = DayOfWeek.Sunday Then
        '        Return DayOfWeek2WeekDays(DirectCast(dn - 1, DayOfWeek))
        '    End If

        '    Dim f As Integer = Convert.ToInt32(fdw)
        '    'If f = 0 Then
        '    '    f = 7
        '    'End If
        '    dn = dn + f
        '    dn = dn Mod 7
        '    If dn = 1 Then
        '        Return WeekDays.Sunday
        '    ElseIf dn = 0 Then
        '        Return WeekDays.Saturday

        '        'dn = Math.Abs(dn - 7)
        '    End If

        '    Return DayOfWeek2WeekDays(DirectCast(dn - 1, DayOfWeek))
        'End Function


        Public Function GetWorkShifts(ts As WorkTimeSlot) As Dictionary(Of Guid, WorkShift)
            'Dim ts As TimeSlot = detail.TimeSlot
            If ts Is Nothing Then Return Nothing
            Dim startDate As Date = Me.StartDay.Date
            Dim endDate As Date = Me.EndDay.Date
            Dim result As New Dictionary(Of Guid, WorkShift)

            Dim details = From q In Me.TimeTable.TimeTableDetails
                          Where q.TimeSlot IsNot Nothing AndAlso q.TimeSlot.Oid = ts.Oid
                          Order By q.DayNum

            Select Case Me.TimeTable.Kind
                Case TimeTableKind.Weeks
                    Dim dic As New Dictionary(Of Integer, Date)
                    For i As Integer = 1 To Me.TimeTable.Frequency
                        dic.Add(i, startDate.AddDays((i - 1) * 7))
                    Next
                    For Each kv In dic
                        Dim fdw As DayOfWeek = DayOfWeek.Sunday
                        Dim firstDayOfWeek As Integer = Convert.ToInt32(Me.TimeTable.FirstDayOfWeek)
                        If firstDayOfWeek = 0 Then
                            firstDayOfWeek = 7
                        End If
                        Dim wDays As List(Of WeekDays) =
                            (From q In details
                             Where q.WeekNum = kv.Key
                             Select dn = q.WeekDay).ToList()

                        If wDays.Any() Then
                            Dim wDay As WeekDays = wDays.FirstOrDefault

                            For i As Integer = 1 To wDays.Count - 1
                                wDay = wDay Or wDays(i)
                            Next
                            Dim ws As New WorkShift(Me.Session)
                            Dim days2Add As Integer = 0
                            If kv.Key > 1 Then
                                Dim nextWeekFirstDay As Date = GetNextWeekday(startDate, DayOfWeek.Monday)
                                If nextWeekFirstDay = startDate Then
                                    days2Add = (kv.Key - 1) * 7
                                Else
                                    nextWeekFirstDay = nextWeekFirstDay.AddDays(((kv.Key - 1) * 7) - 7)
                                    days2Add = nextWeekFirstDay.Subtract(startDate).TotalDays
                                End If
                            End If
                            ws.SetUp(ts, Me, days2Add, wDay)

                            result.Add(ws.Oid, ws)
                        End If
                    Next
                Case TimeTableKind.Days
                    Dim ws As New WorkShift(Me.Session)
                    ws.SetUp(ts, Me)
                    result.Add(ws.Oid, ws)

            End Select

            Return result
        End Function


    End Class

End Namespace
