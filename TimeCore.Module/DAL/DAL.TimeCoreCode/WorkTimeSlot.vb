Imports System
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
    Partial Public Class WorkTimeSlot
        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub
        Public Overrides Sub AfterConstruction()
            MyBase.AfterConstruction()
        End Sub

        <Association("WorkShiftReferencesWorkTimeSlot"), Aggregated()>
        Public ReadOnly Property WorkShifts() As XPCollection(Of WorkShift)
            Get
                Return GetCollection(Of WorkShift)(NameOf(WorkShifts))
            End Get
        End Property

        Public ReadOnly Property TimeOfDayInterval As TimeOfDayInterval
            Get
                Return New TimeOfDayInterval(Me.TimeStart, Me.TimeEnd)
            End Get
        End Property

        Protected Overrides Sub OnChanged(propertyName As String, oldValue As Object, newValue As Object)
            MyBase.OnChanged(propertyName, oldValue, newValue)
            Select Case propertyName
                Case NameOf(TimeStart)
                    Dim newTime As TimeSpan = DirectCast(newValue, TimeSpan)

                    For Each ws In Me.WorkShifts
                        ws.SetStartTime(newTime)
                    Next
                Case NameOf(TimeEnd)
                    Dim newTime As TimeSpan = DirectCast(newValue, TimeSpan)

                    For Each ws In Me.WorkShifts
                        ws.SetEndTime(newTime)
                    Next
            End Select
        End Sub

    End Class

End Namespace
