Imports System.ComponentModel
Imports DevExpress.ExpressApp.Data
Imports DevExpress.ExpressApp.DC
Imports TimeCore.Module.DAL.TimeCore

<DomainComponent>
Public Class WorkShiftLight
    <Browsable(False), Key>
    Public Property Id As Guid
    Public ReadOnly Property Code As String
    Public ReadOnly Property TimeStart As TimeSpan
    Public ReadOnly Property TimeEnd As TimeSpan

    'Public Property WorkShift As WorkShift

    Public Sub Init(ws As WorkTimeSlot)
        Me.Id = ws.Oid
        _Code = ws.Code
        _TimeStart = ws.TimeStart
        _TimeEnd = ws.TimeEnd

    End Sub
End Class
<DomainComponent>
Public Class WorkShiftLightsList
    Public ReadOnly Property Objects() As BindingList(Of WorkShiftLight)
    Public Sub New()
        Me.Objects = New BindingList(Of WorkShiftLight)()
    End Sub

End Class