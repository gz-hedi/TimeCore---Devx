﻿Imports Microsoft.VisualBasic
Imports System

Partial Public Class TimeCoreWindowsFormsModule
	''' <summary> 
	''' Required designer variable.
	''' </summary>
	Private components As System.ComponentModel.IContainer = Nothing

	''' <summary> 
	''' Clean up any resources being used.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing AndAlso (Not components Is Nothing) Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

#Region "Component Designer generated code"

	''' <summary> 
	''' Required method for Designer support - do not modify 
	''' the contents of this method with the code editor.
	''' </summary>
	Private Sub InitializeComponent()
		'
		'TimeCoreWindowsFormsModule
		'
		Me.RequiredModuleTypes.Add(GetType(TimeCore.Module.TimeCoreModule))
        Me.RequiredModuleTypes.Add(GetType(DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule))
		Me.RequiredModuleTypes.Add(GetType(DevExpress.ExpressApp.Chart.Win.ChartWindowsFormsModule))
		Me.RequiredModuleTypes.Add(GetType(DevExpress.ExpressApp.Dashboards.Win.DashboardsWindowsFormsModule))
		Me.RequiredModuleTypes.Add(GetType(DevExpress.ExpressApp.Scheduler.Win.SchedulerWindowsFormsModule))
		Me.RequiredModuleTypes.Add(GetType(DevExpress.ExpressApp.TreeListEditors.Win.TreeListEditorsWindowsFormsModule))
		Me.RequiredModuleTypes.Add(GetType(DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule))
	End Sub

#End Region
End Class