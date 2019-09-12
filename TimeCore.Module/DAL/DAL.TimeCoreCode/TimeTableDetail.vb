Imports System
Imports DevExpress.Xpo
Imports DevExpress.Xpo.Metadata
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Reflection
Imports DevExpress.XtraScheduler

Namespace DAL.TimeCore

    Partial Public Class TimeTableDetail
        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub
        Public Overrides Sub AfterConstruction()
            MyBase.AfterConstruction()
        End Sub

        Public ReadOnly Property WeekNum As Integer
            Get
                Dim result As Integer = -1
                If Not Me.IsLoading AndAlso Not Me.IsSaving Then
                    Select Case Me.TimeTable.Kind
                        Case TimeTableKind.Weeks
                            result = Me.DayNum Mod 7
                            result = If(result = 0, 0, 1) + ((Me.DayNum - result) / 7)
                    End Select
                End If
                Return result
            End Get
        End Property
        'Public ReadOnly Property ModNum As Integer
        '    Get
        '        Dim result As Integer = -1
        '        If Not Me.IsLoading AndAlso Not Me.IsSaving Then
        '            Select Case Me.TimeTable.Kind
        '                Case TimeTableKind.Weeks
        '                    result = Me.DayNum Mod 7
        '                    If result = 0 Then
        '                        result = 7
        '                    End If
        '                    result = result + Convert.ToInt32(Me.TimeTable.FirstDayOfWeek) - 1
        '                    If result >= 7 Then
        '                        result -= 7
        '                    End If
        '                    'result = result Mod 7
        '                    'result = If(result = 0, 0, 1) + ((Me.DayNum - result) / 7)
        '            End Select
        '        End If
        '        Return result
        '    End Get
        'End Property

        Public ReadOnly Property WeekDay As WeekDays
            Get
                Dim result As WeekDays = WeekDays.EveryDay
                If Not Me.IsLoading AndAlso Not Me.IsSaving Then
                    Select Case Me.TimeTable.Kind
                        Case TimeTableKind.Weeks
                            Dim dwInt As Integer = Me.DayNum Mod 7
                            If dwInt = 0 Then
                                dwInt = 7
                            End If
                            dwInt = dwInt + Convert.ToInt32(Me.TimeTable.FirstDayOfWeek) - 1
                            If dwInt >= 7 Then
                                dwInt -= 7
                            End If
                            result = DayOfWeek2WeekDays(DirectCast(dwInt, DayOfWeek))
                    End Select
                End If
                Return result
            End Get
        End Property


        Public Shared Function DayOfWeek2WeekDays(dw As DayOfWeek) As WeekDays
            If dw = DayOfWeek.Sunday Then Return WeekDays.Sunday

            Return DirectCast(Convert.ToInt32(2 ^ Convert.ToInt32(dw)), WeekDays)
        End Function

        Public Shared Function DayNum2WeekDays(ByVal dayNum As Integer, fdw As DayOfWeek) As WeekDays
            Dim dn As Integer = dayNum Mod 7
            If dn = 1 Then Return DayOfWeek2WeekDays(fdw)
            If fdw = DayOfWeek.Sunday Then
                Return DayOfWeek2WeekDays(DirectCast(dn - 1, DayOfWeek))
            End If

            Dim f As Integer = Convert.ToInt32(fdw)
            'If f = 0 Then
            '    f = 7
            'End If
            dn = dn + f
            dn = dn Mod 7
            If dn = 1 Then
                Return WeekDays.Sunday
            ElseIf dn = 0 Then
                Return WeekDays.Saturday

                'dn = Math.Abs(dn - 7)
            End If

            Return DayOfWeek2WeekDays(DirectCast(dn - 1, DayOfWeek))
        End Function

    End Class

End Namespace
