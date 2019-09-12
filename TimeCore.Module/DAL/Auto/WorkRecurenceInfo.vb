Imports System.ComponentModel
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Xml

Namespace DAL.TimeCore

    Public Class WorkRecurenceInfo
        Implements IRecurrenceInfo
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Public Sub New(id As Guid, startDay As Date, startTime As TimeSpan, endDay As Date, endTime As TimeSpan, periodicity As Integer, weekDays As WeekDays)
            Me.New(id, TimeTableKind.Weeks, startDay, startTime, endDay, endTime, periodicity)
            Me.WeekDays = weekDays
        End Sub

        Public Sub New(id As Guid, startOn As Date, endOn As Date, periodicity As Integer, weekDays As WeekDays)
            Me.New(id, TimeTableKind.Weeks, startOn, endOn, periodicity)
            Me.WeekDays = weekDays
        End Sub
        Public Sub New(id As Guid, startDay As Date, startTime As TimeSpan, endDay As Date, endTime As TimeSpan, periodicity As Integer)
            Me.New(id, TimeTableKind.Days, startDay, startTime, endDay, endTime, periodicity)
        End Sub
        Public Sub New(id As Guid, startOn As Date, endOn As Date, periodicity As Integer)
            Me.New(id, TimeTableKind.Days, startOn, endOn, periodicity)
        End Sub
        Private Sub New(id As Guid, kind As TimeTableKind, periodicity As Integer)
            Me.Id = id
            Me.AllDay = False
            Me.Type = DirectCast(Convert.ToInt32(kind), RecurrenceType)
            Me.Periodicity = periodicity
            Me.Range = RecurrenceRange.EndByDate
        End Sub

        Public Sub New(id As Guid, kind As TimeTableKind, startOn As Date, endOn As Date, periodicity As Integer)
            Me.New(id, kind, periodicity)
            Me.BaseStart = startOn
            Me.BaseEnd = endOn
        End Sub

        Private Sub New(id As Guid, kind As TimeTableKind, startDay As Date, startTime As TimeSpan, endDay As Date, endTime As TimeSpan, periodicity As Integer)
            Me.Id = id
            Me.AllDay = False
            Me.Type = DirectCast(Convert.ToInt32(kind), RecurrenceType)
            Me.Periodicity = periodicity
            Me.Range = RecurrenceRange.EndByDate
            Me.StartTime = startTime
            Me.BaseStart = startDay.Date.Add(startTime)
            Me.BaseEnd = endDay.Date.Add(startTime)
            If endTime <= startTime Then
                endTime = endTime.Add(TimeSpan.FromDays(1))
            End If
            Me.EndTime = endTime
            'Me.Duration = endTime.Subtract(startTime)
            'Me.Duration = Me.BaseEnd.Subtract(Me.BaseStart)
            'Me.StartOn = Me.BaseStart
            'Me.EndOn = Me.BaseEnd
        End Sub


        Public ReadOnly Property Id As Object Implements IRecurrenceInfo.Id
        Public ReadOnly Property StartOn As Date
            Get
                Return Me.BaseStart
            End Get
        End Property
        Public ReadOnly Property EndOn As Date
            Get
                Return Me.BaseEnd
            End Get
        End Property

        Public ReadOnly Property StartDay As Date
            Get
                Return Me.BaseEnd.Date
            End Get
        End Property
        Public ReadOnly Property EndDay As Date
            Get
                Return Me.BaseEnd.Date
            End Get
        End Property
        Public ReadOnly Property StartTime As TimeSpan
        Public ReadOnly Property EndTime As TimeSpan
        '    Get
        '        If Me.Duration > TimeSpan.FromDays(1) Then
        '            Return Me.Duration.Subtract(TimeSpan.FromDays(1)).Add(Me.StartTime)
        '        End If
        '        Return Me.Duration.Add(Me.StartTime)
        '    End Get
        'End Property


        Public Property Type As RecurrenceType Implements IRecurrenceInfo.Type

        Private Property BaseStart As Date Implements IRecurrenceInfo.Start

        Private Property BaseEnd As Date Implements IRecurrenceInfo.End

        Private ReadOnly Property TimeZoneId As String Implements IRecurrenceInfo.TimeZoneId

        Private Property Duration As TimeSpan Implements IRecurrenceInfo.Duration

        Private Property Range As RecurrenceRange Implements IRecurrenceInfo.Range

        Private Property AllDay As Boolean Implements IRecurrenceInfo.AllDay





        Private Property OccurrenceCount As Integer Implements IRecurrenceInfo.OccurrenceCount




#Region "Month"
        Private Property DayNumber As Integer Implements IRecurrenceInfo.DayNumber
        Private Property WeekOfMonth As WeekOfMonth Implements IRecurrenceInfo.WeekOfMonth
        Private Property Month As Integer Implements IRecurrenceInfo.Month
#End Region

        Private Property FirstDayOfWeek As DayOfWeek Implements IRecurrenceInfo.FirstDayOfWeek


        Public Property Periodicity As Integer Implements IRecurrenceInfo.Periodicity


        Public Property WeekDays As WeekDays Implements IRecurrenceInfo.WeekDays



#Region "Ignored"

        Private Sub Assign(src As IRecurrenceInfo, assignId As Boolean) Implements IRecurrenceInfo.Assign
            Throw New NotImplementedException()
        End Sub

        Private Sub Assign(src As IRecurrenceInfo) Implements IRecurrenceInfo.Assign
            Throw New NotImplementedException()
        End Sub

        Private Sub Reset(type As RecurrenceType) Implements IRecurrenceInfo.Reset
            Throw New NotImplementedException()
        End Sub

        Private Sub BaseFromXml(val As String) Implements IRecurrenceInfo.FromXml
            Dim ri As IRecurrenceInfo = RecurrenceInfoXmlPersistenceHelper.ObjectFromXml(val)
            Throw New NotImplementedException()
        End Sub

        Private Sub FromXml(val As String, dateTimeSavingMode As DateTimeSavingMode) Implements IRecurrenceInfo.FromXml
            Throw New NotImplementedException()
        End Sub
#End Region

        Public Shared Function FromXml(val As String) As WorkRecurenceInfo
            Dim ri As IRecurrenceInfo = RecurrenceInfoXmlPersistenceHelper.ObjectFromXml(val)
            If TypeOf ri.Id IsNot Guid Then Return Nothing
            Select Case ri.Type
                Case RecurrenceType.Daily
                    Return New WorkRecurenceInfo(
                        DirectCast(ri.Id, Guid),
                        ri.Start, ri.End,
                        ri.Periodicity)

                Case RecurrenceType.Weekly
                    Return New WorkRecurenceInfo(
                        DirectCast(ri.Id, Guid),
                        ri.Start, ri.End,
                        ri.Periodicity,
                        ri.WeekDays)
            End Select
            Return Nothing
        End Function

        Public Function ToXml() As String Implements IRecurrenceInfo.ToXml
            Dim result As String = New RecurrenceInfoXmlPersistenceHelper(Me).ToXml()
            result = result.Replace("DayNumber=""0"" ", "")
            result = result.Replace("WeekOfMonth=""0"" ", "")
            result = result.Replace("WeekDays=""0"" ", "")
            result = result.Replace("Month=""0"" ", "")
            result = result.Replace("OccurrenceCount=""0"" ", "")
            result = result.Replace("FirstDayOfWeek=""0"" ", "")
            '<RecurrenceInfo Start="04/10/2018 08:00:00" End="04/28/2018 08:00:00" DayNumber="0" WeekOfMonth="0" WeekDays="0" Id="0cd518d1-2130-4c74-ba9b-3d2e6b0a38f5" Month="0" OccurrenceCount="0" Periodicity="2" Range="2" FirstDayOfWeek="0" Version="1" />
            Return result
        End Function
    End Class
End Namespace
