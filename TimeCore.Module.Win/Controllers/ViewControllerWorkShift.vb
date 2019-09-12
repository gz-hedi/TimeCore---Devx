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
Imports DevExpress.ExpressApp.Scheduler.Win
Imports DevExpress.XtraScheduler
Imports System.Reflection
Imports DevExpress.XtraScheduler.Commands
Imports DevExpress.Persistent.Base.General
'Imports System.Windows.Forms

' For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
Partial Public Class ViewControllerWorkShift
    Inherits ObjectViewController(Of ListView, IRecurrentEvent)
    Public WithEvents ActionAddWorkShift As DevExpress.ExpressApp.Actions.SimpleAction
    Public WithEvents ActionRestoreRecurence As SimpleAction
    Public WithEvents PopupActionShowCompatibleWorkShifts As DevExpress.ExpressApp.Actions.PopupWindowShowAction
    Public Sub New()
        InitializeComponent()
        ' Target required Views (via the TargetXXX properties) and create their Actions.
        Me.ActionAddWorkShift = New SimpleAction(Me.components)
        Me.ActionAddWorkShift.Caption = "Add WorkShift"
        Me.ActionAddWorkShift.Category = "Edit"
        Me.ActionAddWorkShift.ConfirmationMessage = Nothing
        Me.ActionAddWorkShift.Id = "ActionAddWorkShift"
        Me.ActionAddWorkShift.TargetObjectType = GetType(WorkShift)
        Me.ActionAddWorkShift.TargetViewType = ViewType.ListView
        Me.ActionAddWorkShift.ToolTip = Nothing
        Me.ActionAddWorkShift.TypeOfView = GetType(ListView)
        Me.Actions.Add(Me.ActionAddWorkShift)
        Me.ActionAddWorkShift.Enabled.SetItemValue("sel", False)

        Me.ActionRestoreRecurence = New SimpleAction(Me.components)
        Me.ActionRestoreRecurence.Caption = "RestoreRecurence"
        Me.ActionRestoreRecurence.Category = "Edit"
        Me.ActionRestoreRecurence.ConfirmationMessage = Nothing
        Me.ActionRestoreRecurence.Id = "ActionRestoreRecurence"
        Me.ActionRestoreRecurence.TargetObjectType = GetType(WorkShift)
        Me.ActionRestoreRecurence.TargetViewType = ViewType.ListView
        Me.ActionRestoreRecurence.ToolTip = Nothing
        Me.ActionRestoreRecurence.TypeOfView = GetType(ListView)
        Me.Actions.Add(Me.ActionRestoreRecurence)
        Me.ActionRestoreRecurence.Enabled.SetItemValue("sel", False)

        Me.PopupActionShowCompatibleWorkShifts = New DevExpress.ExpressApp.Actions.PopupWindowShowAction(Me.components)
        Me.PopupActionShowCompatibleWorkShifts.IsModal = False
        Me.PopupActionShowCompatibleWorkShifts.AcceptButtonCaption = Nothing
        Me.PopupActionShowCompatibleWorkShifts.CancelButtonCaption = Nothing
        Me.PopupActionShowCompatibleWorkShifts.Caption = "Add WorkShifts"
        Me.PopupActionShowCompatibleWorkShifts.ConfirmationMessage = Nothing
        Me.PopupActionShowCompatibleWorkShifts.Id = "PopupActionShowCompatibleWorkShifts"
        Me.PopupActionShowCompatibleWorkShifts.ToolTip = Nothing
        Me.Actions.Add(Me.PopupActionShowCompatibleWorkShifts)
    End Sub

    Private Sub PopupActionShowCompatibleWorkShifts_CustomizePopupWindowParams(sender As Object, e As CustomizePopupWindowParamsEventArgs) Handles PopupActionShowCompatibleWorkShifts.CustomizePopupWindowParams
        Dim nonPersistentOS = Application.CreateObjectSpace(GetType(WorkShiftLightsList))
        Dim wsList As WorkShiftLightsList = nonPersistentOS.CreateObject(Of WorkShiftLightsList)()

        Dim wtss As List(Of WorkTimeSlot) = Me.ObjectSpace.GetObjects(Of WorkTimeSlot).ToList
        'Dim calc As OccurrenceCalculator = OccurrenceCalculator.CreateInstance(ri)

        Dim selectedWtss As List(Of WorkTimeSlot) =
                (From a In editor.Appointments.Items.OfType(Of Appointment), q In a.GetExceptions()
                 Where a.HasExceptions AndAlso q.Type = AppointmentType.ChangedOccurrence AndAlso editor.SelectedInterval.Contains(New TimeInterval(q.Start, q.End))
                 Select DirectCast(DirectCast(q.GetSourceObject(editor.SchedulerControl.DataStorage), WorkShift).RecurrencePattern, WorkShift).TimeSlot Distinct).ToList()

        selectedWtss.AddRange(From a In editor.SchedulerControl.DataStorage.GetAppointments(editor.SelectedInterval)
                              Where a.Type <> AppointmentType.Pattern
                              Select DirectCast(a.RecurrencePattern.GetSourceObject(editor.SchedulerControl.DataStorage), WorkShift).TimeSlot Distinct)

        selectedWtss = selectedWtss.Distinct.ToList()

        wtss = wtss.Except(selectedWtss).ToList()
        Dim t As New TimeOfDayInterval()
        't.
        wtss = (From a In wtss
                Where Not selectedWtss.Any(Function(q) q.TimeOfDayInterval.Contains(a.TimeStart) OrElse q.TimeOfDayInterval.Contains(a.TimeEnd) OrElse q.TimeOfDayInterval.IntersectsWithExcludingBounds(a.TimeOfDayInterval))).ToList()


        Dim x = 0
        'If deletedWorkShifts.Any() Then
        '    Me.ActionRestoreRecurence.Enabled.SetItemValue("sel", True)
        '    Me.ActionRestoreRecurence.Tag = deletedWorkShifts
        'Else
        '    Me.ActionRestoreRecurence.Enabled.SetItemValue("sel", False)
        '    Me.ActionRestoreRecurence.Tag = Nothing
        'End If


        For Each wts In wtss
            Dim wsLight As New WorkShiftLight
            wsLight.Init(wts)
            wsList.Objects.Add(wsLight)
        Next



        nonPersistentOS.CommitChanges()
        Dim dv As DetailView = Application.CreateDetailView(nonPersistentOS, wsList)
        dv.ViewEditMode = ViewEditMode.Edit
        dv.AllowNew.SetItemValue("x", False)
        dv.AllowDelete.SetItemValue("x", False)
        dv.AllowEdit.SetItemValue("x", False)
        e.View = dv
        e.DialogController.SaveOnAccept = False
        e.DialogController.CancelAction.Active("NothingToCancel") = False

        AddHandler e.DialogController.AcceptAction.Executed,
            Sub(s, a)
                Dim lv As ListView = DirectCast(dv.FindItem("Objects"), ListPropertyEditor).ListView

                Dim selected As List(Of WorkShiftLight) = lv.SelectedObjects.OfType(Of WorkShiftLight).ToList()

                '.SelectedObjects
                Dim z = 0
            End Sub

    End Sub

    Private Sub ActionRestoreRecurence_Execute(sender As Object, e As SimpleActionExecuteEventArgs) Handles ActionRestoreRecurence.Execute
        If DevExpress.XtraEditors.XtraMessageBox.Show("Restore?", "Deleted Occurences restoration.", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
            Dim deletedWorkShifts As List(Of WorkShift) = DirectCast(e.Action.Tag, List(Of WorkShift))
            Me.ObjectSpace.Delete(deletedWorkShifts)
            Me.ObjectSpace.CommitChanges()
            editor.SchedulerControl.RefreshData()
        End If
    End Sub

    Protected Overrides Sub OnActivated()
        MyBase.OnActivated()
        ' Perform various tasks depending on the target View.
    End Sub
    Dim editor As SchedulerListEditor
    'Protected _InnerControl As ISchedulerCommandTarget
    Protected Overrides Sub OnViewControlsCreated()
        MyBase.OnViewControlsCreated()
        ' Access and customize the target View control.
        editor = DirectCast(Me.View.Editor, SchedulerListEditor)
        If editor IsNot Nothing Then
            'Dim fi As FieldInfo = editor.SchedulerControl.GetType().GetField("innerControl", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
            '_InnerControl = DirectCast(fi.GetValue(editor.SchedulerControl), ISchedulerCommandTarget)
            editor.SchedulerControl.DataStorage.EnableReminders = False
            editor.SchedulerControl.OptionsCustomization.AllowAppointmentEdit = UsedAppointmentType.None
            'editor.SchedulerControl.OptionsCustomization.AllowAppointmentDelete = UsedAppointmentType.All
            editor.SchedulerControl.OptionsBehavior.RecurrentAppointmentDeleteAction = RecurrentAppointmentAction.Ask
            'editor.SchedulerControl.OptionsCustomization.AllowAppointmentDelete = UsedAppointmentType.All
            'AddHandler editor.SchedulerControl.AllowAppointmentDelete, AddressOf SchedulerControl_AllowAppointmentDelete

            'AddHandler editor.SchedulerControl.PopupMenuShowing, AddressOf SchedulerControl_PopupMenuShowing
            'AddHandler editor.SchedulerControl.SelectionChanged, AddressOf SchedulerControl_SelectionChanged
            AddHandler editor.SchedulerControl.DeleteRecurrentAppointmentFormShowing, AddressOf SchedulerControl_DeleteRecurrentAppointmentFormShowing
            Me.EnableAddWorkShiftAction(editor.SchedulerControl)

            'AddHandler editor.SchedulerControl.EditAppointmentFormShowing, AddressOf SchedulerControl_EditAppointmentFormShowing
            'AddHandler editor.SchedulerControl., AddressOf SchedulerControl_AllowAppointmentEdit
        End If
        'editor.SchedulerControl.DataStorage. = False

        Dim xxx = 0
        Dim x = 0
    End Sub

    Private Sub SchedulerControl_AllowAppointmentDelete(sender As Object, e As AppointmentOperationEventArgs)
        'e.Allow = False
        'If Not e.Recurring Then
        '    e.Appointment.Delete()
        'End If
    End Sub

    Private Sub SchedulerControl_DeleteRecurrentAppointmentFormShowing(sender As Object, e As DeleteRecurrentAppointmentFormEventArgs)
        If DevExpress.XtraEditors.XtraMessageBox.Show("Delete?", "Deleted Occurences.", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
            'e.QueryResult = RecurrentAppointmentAction.Occurrence
            e.DialogResult = System.Windows.Forms.DialogResult.OK
        Else
            e.DialogResult = System.Windows.Forms.DialogResult.Cancel

        End If
        e.QueryResult = RecurrentAppointmentAction.Occurrence
        e.Handled = True


        'e.DialogResult = System.Windows.Forms.DialogResult.Cancel
        'e.Handled = True
        e.Appointment.Delete()
        'editor.SchedulerControl.DeleteAppointment(e.Appointment)
        e.Handled = True
        Dim x = 0
        'e.Handled = True
        'e.QueryResult = RecurrentAppointmentAction.Ask
        'e.DialogResult = System.Windows.Forms.DialogResult.No
        'e.Appointment.Delete()
        'e.Appointment.Delete()
    End Sub

    Private Sub SchedulerControl_SelectionChanged(sender As Object, e As EventArgs)
        Dim sc As SchedulerControl = DirectCast(sender, SchedulerControl)
        Me.EnableAddWorkShiftAction(sc)
        If sc.SelectedInterval.Duration.TotalDays >= 1 Then
            'Dim l = Me.ObjectSpace.GetObjects(Of WorkShift)
            Dim deletedWorkShifts As List(Of WorkShift) =
                (From a In sc.DataStorage.Appointments.Items.OfType(Of Appointment), q In a.GetExceptions()
                 Where a.HasExceptions AndAlso q.Type = AppointmentType.DeletedOccurrence AndAlso sc.SelectedInterval.Contains(New TimeInterval(q.Start, q.End))
                 Select DirectCast(q.GetSourceObject(sc.DataStorage), WorkShift)).ToList()

            If deletedWorkShifts.Any() Then
                Me.ActionRestoreRecurence.Enabled.SetItemValue("sel", True)
                Me.ActionRestoreRecurence.Tag = deletedWorkShifts
            Else
                Me.ActionRestoreRecurence.Enabled.SetItemValue("sel", False)
                Me.ActionRestoreRecurence.Tag = Nothing
            End If
            Dim x = 0
        Else
            Me.ActionRestoreRecurence.Enabled.SetItemValue("sel", False)
            Me.ActionRestoreRecurence.Tag = Nothing
        End If

    End Sub

    Private Sub EnableAddWorkShiftAction(sc As SchedulerControl)
        'If sc.SelectedAppointments.Count = 0 AndAlso sc.SelectedInterval.Duration.TotalDays >= 1 Then
        If sc.SelectedInterval.Duration.TotalDays >= 1 Then
            Me.ActionAddWorkShift.Enabled.SetItemValue("sel", True)
        Else
            Me.ActionAddWorkShift.Enabled.SetItemValue("sel", False)
        End If
    End Sub

    Private Sub SchedulerControl_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs)
        Dim x = e
        Select Case e.Menu.Id
            Case SchedulerMenuItemId.AppointmentMenu
                For Each mi In e.Menu.Items.OfType(Of DevExpress.Utils.Menu.CommandPopupMenu(Of SchedulerMenuItemId))
                    If mi.Id <> SchedulerMenuItemId.DeleteAppointment Then
                        mi.Visible = False
                    End If
                Next
                For Each mi In e.Menu.Items.OfType(Of SchedulerMenuItem)
                    If mi.Id <> SchedulerMenuItemId.DeleteAppointment Then
                        mi.Visible = False
                    End If
                Next
                Dim miDel As New SchedulerMenuItem("Delete")

                e.Menu.Items.Insert(0, miDel)

                AddHandler miDel.Click, Sub(s, a)
                                            If DevExpress.XtraEditors.XtraMessageBox.Show("Delete?", "Delete Occurences.", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then

                                                Dim apt As Appointment = editor.SchedulerControl.SelectedAppointments.Item(0)
                                                Dim nApt As Appointment = apt.RecurrencePattern.CreateException(AppointmentType.DeletedOccurrence, apt.RecurrenceIndex)
                                                editor.SchedulerControl.DataStorage.Appointments.Add(nApt)
                                                'Dim ws As WorkShift = DirectCast(apt.RecurrencePattern.GetSourceObject(editor.SchedulerControl.DataStorage), WorkShift)
                                                'ws.CreateDeteted(apt)
                                                'apt.d
                                                Me.ObjectSpace.CommitChanges()
                                                editor.SchedulerControl.DataStorage.RefreshData()
                                                editor.SchedulerControl.RefreshData()
                                                editor.SchedulerControl.DataStorage.Appointments.ValidateDataSource()
                                                editor.SchedulerControl.Refresh()
                                                editor.Refresh()
                                                'Me.DeleteAppointment()
                                            End If

                                        End Sub

                e.Menu.Items.ElementAtOrDefault(1).BeginGroup = True
            Case SchedulerMenuItemId.DefaultMenu
                Dim miAdd As New SchedulerMenuItem("Add")

                e.Menu.Items.Insert(0, miAdd)

                'Dim miDel As New SchedulerMenuItem("Delete")

                'e.Menu.Items.Insert(0, miDel)

                'AddHandler miDel.Click, Sub(s, a) editor.SchedulerControl.SelectedAppointments.Item(0).Delete()

                'e.Menu.Items.ElementAtOrDefault(1).BeginGroup = True


        End Select


        'Return
        'Dim z As DevExpress.Utils.Menu.CommandPopupMenu(Of DevExpress.XtraScheduler.SchedulerMenuItemId)


    End Sub
    'Public Sub DeleteAppointment(ByVal apt As Appointment)
    '    Dim appointments As New AppointmentBaseCollection()
    '    appointments.Add(apt)
    '    Dim command As New Commands.DeleteAppointmentsQueryCommand(_InnerControl, appointments)
    '    command.Execute()
    'End Sub


    Protected Overrides Sub OnDeactivated()
        ' Unsubscribe from previously subscribed events and release other references and resources.
        MyBase.OnDeactivated()
    End Sub
End Class
