Imports System.ComponentModel
Imports DevExpress.XtraScheduler

Public Class Class1
    Implements IRecurrenceInfo
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public ReadOnly Property Id As Object Implements IRecurrenceInfo.Id
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public ReadOnly Property TimeZoneId As String Implements IRecurrenceInfo.TimeZoneId
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public Property Start As Date Implements IRecurrenceInfo.Start
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Date)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property [End] As Date Implements IRecurrenceInfo.End
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Date)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property Duration As TimeSpan Implements IRecurrenceInfo.Duration
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As TimeSpan)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property AllDay As Boolean Implements IRecurrenceInfo.AllDay
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Boolean)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property Type As RecurrenceType Implements IRecurrenceInfo.Type
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As RecurrenceType)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property Range As RecurrenceRange Implements IRecurrenceInfo.Range
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As RecurrenceRange)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property OccurrenceCount As Integer Implements IRecurrenceInfo.OccurrenceCount
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Integer)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property DayNumber As Integer Implements IRecurrenceInfo.DayNumber
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Integer)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property FirstDayOfWeek As DayOfWeek Implements IRecurrenceInfo.FirstDayOfWeek
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As DayOfWeek)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property Month As Integer Implements IRecurrenceInfo.Month
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Integer)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property Periodicity As Integer Implements IRecurrenceInfo.Periodicity
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Integer)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property WeekDays As WeekDays Implements IRecurrenceInfo.WeekDays
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As WeekDays)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property WeekOfMonth As WeekOfMonth Implements IRecurrenceInfo.WeekOfMonth
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As WeekOfMonth)
            Throw New NotImplementedException()
        End Set
    End Property


    Public Sub Assign(src As IRecurrenceInfo, assignId As Boolean) Implements IRecurrenceInfo.Assign
        Throw New NotImplementedException()
    End Sub

    Public Sub Assign(src As IRecurrenceInfo) Implements IRecurrenceInfo.Assign
        Throw New NotImplementedException()
    End Sub

    Public Sub Reset(type As RecurrenceType) Implements IRecurrenceInfo.Reset
        Throw New NotImplementedException()
    End Sub

    Public Sub FromXml(val As String) Implements IRecurrenceInfo.FromXml
        Throw New NotImplementedException()
    End Sub

    Public Sub FromXml(val As String, dateTimeSavingMode As DateTimeSavingMode) Implements IRecurrenceInfo.FromXml
        Throw New NotImplementedException()
    End Sub

    Public Function ToXml() As String Implements IRecurrenceInfo.ToXml
        Throw New NotImplementedException()
    End Function
End Class
