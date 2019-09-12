Imports System.ComponentModel
Imports System.Xml
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.Base.General
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Xpo
Imports DevExpress.XtraScheduler

Namespace DAL.TimeCore
    <DefaultClassOptions()>
    Public Class WorkShift
        Inherits BaseObject
        Implements IRecurrentEvent

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub
        Public Overrides Sub AfterConstruction()
            MyBase.AfterConstruction()
            Me.AllDay = False
            _AppointmentId = Me.Oid
        End Sub

#Region "Helpers"
        Friend Sub SetAsDeleted(owner As WorkShift, apt As Appointment)
            Me.Type = Convert.ToInt32(AppointmentType.DeletedOccurrence)
            Me.RecurrencePattern = owner
            _RecurrenceInfoId = owner.RecurrenceInfoId
            Dim xml As String = "<RecurrenceInfo "
            xml &= String.Format("Id=""{0}"" ", owner.RecurrenceInfoId)
            If apt.RecurrenceIndex <> 0 Then
                xml &= String.Format("Index=""{0}"" ", apt.RecurrenceIndex)
            End If
            xml &= "/>"
            Me.RecurrenceInfoXml = xml
            Me.StartOn = apt.Start
            Me.EndOn = apt.End
            Me.WorkShiftDay = apt.Start.Date
        End Sub

        Public Function CreateDeteted(apt As Appointment) As WorkShift
            Dim o As New WorkShift(Me.Session)
            o.SetAsDeleted(Me, apt)
            Return o
        End Function
        Public Sub SetStartTime(time As TimeSpan)
            Me.StartOn = Me.WorkShiftDay.Add(time)
            Dim xmlDoc As New XmlDocument()
            xmlDoc.LoadXml(Me.RecurrenceInfoXml)
            xmlDoc.DocumentElement.Attributes("Start").Value = Me.StartOn.ToString("MM/dd/yyyy HH:mm:ss")
            xmlDoc.DocumentElement.Attributes("End").Value = Me.TimeTableAffectation.EndDay.Date.Add(time).ToString("MM/dd/yyyy HH:mm:ss")
            Me.RecurrenceInfoXml = xmlDoc.InnerXml()
        End Sub

        Public Sub SetEndTime(time As TimeSpan)
            Me.EndOn = Me.WorkShiftDay.Add(time)
        End Sub

        Public Sub FixTimeInteval(Optional daysToAdd As Integer = 0)
            If Me.TimeTableAffectation Is Nothing OrElse Me.TimeSlot Is Nothing Then Return
            Me.WorkShiftDay = Me.TimeTableAffectation.StartDay.Date.AddDays(daysToAdd)
            Me.StartOn = Me.WorkShiftDay.AddHours(Me.TimeSlot.TimeStart.Hours).AddMinutes(Me.TimeSlot.TimeStart.Minutes)
            Dim endTime As TimeSpan = Me.TimeSlot.TimeEnd
            If endTime <= Me.TimeSlot.TimeStart Then
                endTime = endTime.Add(TimeSpan.FromDays(1))
            End If
            Me.EndOn = Me.WorkShiftDay.AddHours(endTime.Hours).AddMinutes(endTime.Minutes)
        End Sub

        Public Sub SetUp(timeSlot As WorkTimeSlot, timeTableAffectation As TimeTableAffectation, Optional daysToAdd As Integer = 0, Optional weekDays As DevExpress.XtraScheduler.WeekDays? = Nothing)
            Me.TimeTableAffectation = timeTableAffectation
            Me.TimeSlot = timeSlot
            'Dim daysToAdd As Integer = 0
            'If weekNum.HasValue Then
            '    daysToAdd = dayNum.Value ' - 1
            'End If
            Me.FixTimeInteval(daysToAdd)
            _RecurrenceInfoId = Guid.NewGuid()
            _RecurrenceInfoKind = Me.TimeTableAffectation.TimeTable.Kind
            Dim ri As WorkRecurenceInfo = New WorkRecurenceInfo(_RecurrenceInfoId, _RecurrenceInfoKind, Me.StartOn, Me.TimeTableAffectation.EndDay.Date.AddHours(Me.TimeSlot.TimeStart.Hours).AddMinutes(Me.TimeSlot.TimeStart.Minutes), Me.TimeTableAffectation.TimeTable.Frequency)

            If weekDays.HasValue Then
                'ri.Type = DevExpress.XtraScheduler.RecurrenceType.Weekly
                ri.WeekDays = weekDays.Value
                _WeekDays = ri.WeekDays
            Else
                _WeekDays = Nothing
            End If
            _Periodicity = ri.Periodicity
            Me.Type = Convert.ToInt32(AppointmentType.Pattern)
            Me.RecurrenceInfoXml = ri.ToXml()
        End Sub
#End Region

#Region "Relations"

        Dim _TimeSlot As WorkTimeSlot
        <Association("WorkShiftReferencesWorkTimeSlot")>
        Public Property TimeSlot() As WorkTimeSlot
            Get
                Return _TimeSlot
            End Get
            Set(ByVal value As WorkTimeSlot)
                SetPropertyValue(Of WorkTimeSlot)(NameOf(TimeSlot), _TimeSlot, value)
            End Set
        End Property


        Private _TimeTableAffectation As TimeTableAffectation
        <Association("WorkShiftReferencesTimeTableAffectation")>
        Public Property TimeTableAffectation As TimeTableAffectation
            Get
                Return _TimeTableAffectation
            End Get
            Set(value As TimeTableAffectation)
                SetPropertyValue(Of TimeTableAffectation)(NameOf(TimeTableAffectation), _TimeTableAffectation, value)
                Me.FixTimeInteval()
            End Set
        End Property
#End Region

        Public Property Subject As String Implements IEvent.Subject
            Get
                Return Me.TimeSlot?.Code
            End Get
            Set(value As String)
            End Set
        End Property

        Public Property Description As String Implements IEvent.Description
            Get
                Return ""
            End Get
            Set(value As String)
            End Set
        End Property

        Public ReadOnly Property TimeStart() As TimeSpan
            Get
                Return Me.StartOn.Subtract(Me.StartOn.Date)
            End Get
        End Property

        Public ReadOnly Property TimeEnd() As TimeSpan
            Get
                Return Me.EndOn.Subtract(Me.EndOn.Date)
            End Get
        End Property

        Private _WorkShiftDay As Date
        <Browsable(False)>
        Public Property WorkShiftDay As Date
            Get
                'If Me.TimeSlot IsNot Nothing Then
                '    _StartOn = _StartOn.Add(Me.TimeSlot.TimeStart)
                'End If
                Return _WorkShiftDay
            End Get
            Set(value As Date)
                SetPropertyValue(Of Date)(NameOf(WorkShiftDay), _WorkShiftDay, value)
            End Set
        End Property

        Private _StartOn As Date
        <Browsable(False)>
        Public Property StartOn As Date Implements IEvent.StartOn
            Get
                'If Me.TimeSlot IsNot Nothing Then
                '    _StartOn = Me.WorkShiftDay.Add(Me.TimeSlot.TimeStart)
                'End If
                Return _StartOn
            End Get
            Set(value As Date)
                SetPropertyValue(Of Date)(NameOf(StartOn), _StartOn, value)
            End Set
        End Property


        Private _EndOn As Date
        <Browsable(False)>
        Public Property EndOn As Date Implements IEvent.EndOn
            Get
                'If Me.TimeSlot IsNot Nothing Then
                '    _EndOn = Me.WorkShiftDay.Add(Me.TimeSlot.TimeStart)
                'End If
                Return _EndOn
            End Get
            Set(value As Date)
                SetPropertyValue(Of Date)(NameOf(EndOn), _EndOn, value)
            End Set
        End Property

#Region "Useless"
        <Browsable(False)>
        Private Property AllDay As Boolean Implements IEvent.AllDay
        Private Property Location As String Implements IEvent.Location
        Private Property Label As Integer Implements IEvent.Label
        Private Property Status As Integer Implements IEvent.Status
        Private Property ResourceId As String Implements IEvent.ResourceId


        Private ReadOnly Property AppointmentId As Object Implements IEvent.AppointmentId

#End Region


        Public ReadOnly Property Kind As AppointmentType
            Get
                Return DirectCast(_Type, AppointmentType)
            End Get
        End Property



        Private _Type As Integer
        <Browsable(False)>
        Public Property Type As Integer Implements IEvent.Type
            Get
                'Pattern
                Return _Type
            End Get
            Set(value As Integer)
                SetPropertyValue(Of Integer)(NameOf(Type), _Type, value)
            End Set
        End Property




        <Persistent(NameOf(RecurrencePattern))>
        Private _RecurrencePattern As WorkShift
        <PersistentAlias(NameOf(_RecurrencePattern))>
        Public Property RecurrencePattern As IRecurrentEvent Implements IRecurrentEvent.RecurrencePattern
            Get
                Return _RecurrencePattern
            End Get
            Set(value As IRecurrentEvent)
                SetPropertyValue(Of IRecurrentEvent)(NameOf(RecurrencePattern), _RecurrencePattern, DirectCast(value, WorkShift))
            End Set
        End Property


        Private _RecurrenceInfoXml As String
        <Size(SizeAttribute.Unlimited)>
        Public Property RecurrenceInfoXml As String Implements IRecurrentEvent.RecurrenceInfoXml
            Get
                Return _RecurrenceInfoXml
            End Get
            Set(value As String)
                SetPropertyValue(Of String)(NameOf(RecurrenceInfoXml), _RecurrenceInfoXml, value)
            End Set
        End Property

        <Persistent(NameOf(OcurrenceIndex))>
        Private _OcurrenceIndex As Integer?
        <PersistentAlias(NameOf(_OcurrenceIndex))>
        Public ReadOnly Property OcurrenceIndex As Integer?
            Get
                Return _OcurrenceIndex
            End Get
        End Property

        <Persistent(NameOf(RecurrenceInfoId))>
        Private _RecurrenceInfoId As Guid?
        <PersistentAlias(NameOf(_RecurrenceInfoId))>
        Public ReadOnly Property RecurrenceInfoId As Guid?
            Get
                Return _RecurrenceInfoId
            End Get
        End Property

        <Persistent(NameOf(RecurrenceInfoKind))>
        Private _RecurrenceInfoKind As DevExpress.XtraScheduler.RecurrenceType?
        <PersistentAlias(NameOf(_RecurrenceInfoKind))>
        Public ReadOnly Property RecurrenceInfoKind As DevExpress.XtraScheduler.RecurrenceType?
            Get
                Return _RecurrenceInfoKind
            End Get
        End Property

        <Persistent(NameOf(Periodicity))>
        Private _Periodicity As Integer?
        <PersistentAlias(NameOf(_Periodicity))>
        Public ReadOnly Property Periodicity As Integer?
            Get
                Return _Periodicity
            End Get
        End Property

        <Persistent(NameOf(WeekDays))>
        Private _WeekDays As DevExpress.XtraScheduler.WeekDays?
        <PersistentAlias(NameOf(_WeekDays))>
        Public ReadOnly Property WeekDays As DevExpress.XtraScheduler.WeekDays?
            Get
                Return _WeekDays
            End Get
        End Property

        Protected Overrides Sub OnSaving()
            MyBase.OnSaving()
            If Me.Kind <> AppointmentType.Pattern Then
                If Me.Session.IsObjectMarkedDeleted(Me) Then

                End If

                _RecurrenceInfoId = DirectCast(Me.RecurrencePattern, WorkShift)?.RecurrenceInfoId
                Dim xmlDoc As New XmlDocument()
                xmlDoc.LoadXml(Me.RecurrenceInfoXml)
                _OcurrenceIndex = Convert.ToInt32(If(xmlDoc.DocumentElement.Attributes("Index")?.Value, "0"))
                Me.WorkShiftDay = Me.StartOn.Date
                Dim x = 0
            End If
        End Sub

    End Class

End Namespace

