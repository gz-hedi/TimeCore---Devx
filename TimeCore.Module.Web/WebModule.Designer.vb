﻿Imports Microsoft.VisualBasic
Imports System

Partial Public Class TimeCoreAspNetModule
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
		'TimeCoreAspNetModule
		'
		Me.RequiredModuleTypes.Add(GetType(TimeCore.Module.TimeCoreModule))
        Me.RequiredModuleTypes.Add(GetType(DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule))
		Me.RequiredModuleTypes.Add(GetType(DevExpress.ExpressApp.Chart.Web.ChartAspNetModule))
		Me.RequiredModuleTypes.Add(GetType(DevExpress.ExpressApp.Dashboards.Web.DashboardsAspNetModule))
		Me.RequiredModuleTypes.Add(GetType(DevExpress.ExpressApp.Scheduler.Web.SchedulerAspNetModule))
		Me.RequiredModuleTypes.Add(GetType(DevExpress.ExpressApp.TreeListEditors.Web.TreeListEditorsAspNetModule))
		Me.RequiredModuleTypes.Add(GetType(DevExpress.ExpressApp.Validation.Web.ValidationAspNetModule))

	End Sub

#End Region
End Class