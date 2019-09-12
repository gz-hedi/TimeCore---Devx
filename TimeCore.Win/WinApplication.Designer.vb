Imports Microsoft.VisualBasic
Imports System

Partial Public Class TimeCoreWindowsFormsApplication
	''' <summary> 
	''' Required designer variable.
	''' </summary>
	Private components As System.ComponentModel.IContainer = Nothing

	''' <summary> 
	''' Clean up any resources being used.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed otherwise, false.</param>
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
		Me.module1 = New DevExpress.ExpressApp.SystemModule.SystemModule()
        Me.module2 = New DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule()
		Me.module3 = New Global.TimeCore.Module.TimeCoreModule()
		Me.module4 = New Global.TimeCore.Module.Win.TimeCoreWindowsFormsModule()
        Me.securityModule1 = New DevExpress.ExpressApp.Security.SecurityModule()
        Me.securityStrategyComplex1 = New DevExpress.ExpressApp.Security.SecurityStrategyComplex()
        Me.securityStrategyComplex1.SupportNavigationPermissionsForTypes = False
        Me.objectsModule = New DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule()
        Me.chartModule = New DevExpress.ExpressApp.Chart.ChartModule()
        Me.chartWindowsFormsModule = New DevExpress.ExpressApp.Chart.Win.ChartWindowsFormsModule()
        Me.conditionalAppearanceModule = New DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule()
        Me.dashboardsModule = New DevExpress.ExpressApp.Dashboards.DashboardsModule()
        Me.dashboardsWindowsFormsModule = New DevExpress.ExpressApp.Dashboards.Win.DashboardsWindowsFormsModule()
        Me.kpiModule = New DevExpress.ExpressApp.Kpi.KpiModule()
        Me.schedulerModuleBase = New DevExpress.ExpressApp.Scheduler.SchedulerModuleBase()
        Me.schedulerWindowsFormsModule = New DevExpress.ExpressApp.Scheduler.Win.SchedulerWindowsFormsModule()
        Me.stateMachineModule = New DevExpress.ExpressApp.StateMachine.StateMachineModule()
        Me.treeListEditorsModuleBase = New DevExpress.ExpressApp.TreeListEditors.TreeListEditorsModuleBase()
        Me.treeListEditorsWindowsFormsModule = New DevExpress.ExpressApp.TreeListEditors.Win.TreeListEditorsWindowsFormsModule()
        Me.validationModule = New DevExpress.ExpressApp.Validation.ValidationModule()
        Me.validationWindowsFormsModule = New DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule()
        Me.authenticationStandard1 = New DevExpress.ExpressApp.Security.AuthenticationStandard()
		CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        ' 
        ' securityStrategyComplex1
        ' 
        Me.securityStrategyComplex1.Authentication = Me.authenticationStandard1
        Me.securityStrategyComplex1.RoleType = GetType(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyRole)
        Me.securityStrategyComplex1.UserType = GetType(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyUser)
        ' 
        ' securityModule1
        ' 
        Me.securityModule1.UserType = GetType(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyUser)
        ' 
        ' authenticationStandard1
        ' 
        Me.authenticationStandard1.LogonParametersType = GetType(DevExpress.ExpressApp.Security.AuthenticationStandardLogonParameters)
        '
        ' dashboardsModule
        '
        Me.dashboardsModule.DashboardDataType = GetType(DevExpress.Persistent.BaseImpl.DashboardData)
        Me.dashboardsWindowsFormsModule.DesignerFormStyle = DevExpress.XtraBars.Ribbon.RibbonFormStyle.Ribbon
        '
        ' stateMachineModule
        '
        Me.stateMachineModule.StateMachineStorageType = GetType(DevExpress.ExpressApp.StateMachine.Xpo.XpoStateMachine)
        ' 
		' TimeCoreWindowsFormsApplication
		' 
        Me.ApplicationName = "TimeCore"
        Me.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema
        Me.Modules.Add(Me.module1)
		Me.Modules.Add(Me.module2)
		Me.Modules.Add(Me.module3)
		Me.Modules.Add(Me.module4)
        Me.Modules.Add(Me.securityModule1)
        Me.Security = Me.securityStrategyComplex1
        Me.Modules.Add(Me.objectsModule)
        Me.Modules.Add(Me.chartModule)
        Me.Modules.Add(Me.chartWindowsFormsModule)
        Me.Modules.Add(Me.conditionalAppearanceModule)
        Me.Modules.Add(Me.dashboardsModule)
        Me.Modules.Add(Me.dashboardsWindowsFormsModule)
        Me.Modules.Add(Me.kpiModule)
        Me.Modules.Add(Me.schedulerModuleBase)
        Me.Modules.Add(Me.schedulerWindowsFormsModule)
        Me.Modules.Add(Me.stateMachineModule)
        Me.Modules.Add(Me.treeListEditorsModuleBase)
        Me.Modules.Add(Me.treeListEditorsWindowsFormsModule)
        Me.Modules.Add(Me.validationModule)
        Me.Modules.Add(Me.validationWindowsFormsModule)
        Me.UseOldTemplates = False

		CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

	End Sub

#End Region

	Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule
    Private module2 As DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule
	Private module3 As Global.TimeCore.Module.TimeCoreModule
    Private module4 As Global.TimeCore.Module.Win.TimeCoreWindowsFormsModule
    Private securityModule1 As DevExpress.ExpressApp.Security.SecurityModule
    Private securityStrategyComplex1 As DevExpress.ExpressApp.Security.SecurityStrategyComplex
    Private authenticationStandard1 As DevExpress.ExpressApp.Security.AuthenticationStandard
    Private objectsModule As DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule
    Private chartModule As DevExpress.ExpressApp.Chart.ChartModule
    Private chartWindowsFormsModule As DevExpress.ExpressApp.Chart.Win.ChartWindowsFormsModule
    Private conditionalAppearanceModule As DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule
    Private dashboardsModule As DevExpress.ExpressApp.Dashboards.DashboardsModule
    Private dashboardsWindowsFormsModule As DevExpress.ExpressApp.Dashboards.Win.DashboardsWindowsFormsModule
    Private kpiModule As DevExpress.ExpressApp.Kpi.KpiModule
    Private schedulerModuleBase As DevExpress.ExpressApp.Scheduler.SchedulerModuleBase
    Private schedulerWindowsFormsModule As DevExpress.ExpressApp.Scheduler.Win.SchedulerWindowsFormsModule
    Private stateMachineModule As DevExpress.ExpressApp.StateMachine.StateMachineModule
    Private treeListEditorsModuleBase As DevExpress.ExpressApp.TreeListEditors.TreeListEditorsModuleBase
    Private treeListEditorsWindowsFormsModule As DevExpress.ExpressApp.TreeListEditors.Win.TreeListEditorsWindowsFormsModule
    Private validationModule As DevExpress.ExpressApp.Validation.ValidationModule
    Private validationWindowsFormsModule As DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule
End Class
