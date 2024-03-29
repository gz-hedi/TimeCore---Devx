﻿Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.ExpressApp
Imports System.ComponentModel
Imports DevExpress.ExpressApp.Web
Imports System.Collections.Generic
Imports DevExpress.ExpressApp.Xpo
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Security.ClientServer

' For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Web.WebApplication
Partial Public Class TimeCoreAspNetApplication
    Inherits WebApplication
    Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule
    Private module2 As DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule
    Private module3 As TimeCore.Module.TimeCoreModule
    Private module4 As TimeCore.Module.Web.TimeCoreAspNetModule

    Private securityModule1 As DevExpress.ExpressApp.Security.SecurityModule
    Private securityStrategyComplex1 As DevExpress.ExpressApp.Security.SecurityStrategyComplex
    Private authenticationStandard1 As DevExpress.ExpressApp.Security.AuthenticationStandard
    Private objectsModule As DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule
    Private chartModule As DevExpress.ExpressApp.Chart.ChartModule
    Private chartAspNetModule As DevExpress.ExpressApp.Chart.Web.ChartAspNetModule
    Private conditionalAppearanceModule As DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule
    Private dashboardsModule As DevExpress.ExpressApp.Dashboards.DashboardsModule
    Private dashboardsAspNetModule As DevExpress.ExpressApp.Dashboards.Web.DashboardsAspNetModule
    Private kpiModule As DevExpress.ExpressApp.Kpi.KpiModule
    Private schedulerModuleBase As DevExpress.ExpressApp.Scheduler.SchedulerModuleBase
    Private schedulerAspNetModule As DevExpress.ExpressApp.Scheduler.Web.SchedulerAspNetModule
    Private stateMachineModule As DevExpress.ExpressApp.StateMachine.StateMachineModule
    Private treeListEditorsModuleBase As DevExpress.ExpressApp.TreeListEditors.TreeListEditorsModuleBase
    Private treeListEditorsAspNetModule As DevExpress.ExpressApp.TreeListEditors.Web.TreeListEditorsAspNetModule
    Private validationModule As DevExpress.ExpressApp.Validation.ValidationModule
    Private validationAspNetModule As DevExpress.ExpressApp.Validation.Web.ValidationAspNetModule

#Region "Default XAF configuration options (https://www.devexpress.com/kb=T501418)"
    Shared Sub New()
		EnableMultipleBrowserTabsSupport = True
        DevExpress.ExpressApp.Web.Editors.ASPx.ASPxGridListEditor.AllowFilterControlHierarchy = True
        DevExpress.ExpressApp.Web.Editors.ASPx.ASPxGridListEditor.MaxFilterControlHierarchyDepth = 3
        DevExpress.ExpressApp.Web.Editors.ASPx.ASPxCriteriaPropertyEditor.AllowFilterControlHierarchyDefault = True
        DevExpress.ExpressApp.Web.Editors.ASPx.ASPxCriteriaPropertyEditor.MaxHierarchyDepthDefault = 3
		DevExpress.Persistent.Base.PasswordCryptographer.EnableRfc2898 = True
        DevExpress.Persistent.Base.PasswordCryptographer.SupportLegacySha512 = False
    End Sub
	Private Sub InitializeDefaults()
        LinkNewObjectToParentImmediately = False
        OptimizedControllersCreation = True
    End Sub
#End Region
    Public Sub New()
        InitializeComponent()
        InitializeDefaults()
    End Sub

    Protected Overrides Function CreateViewUrlManager() As IViewUrlManager
        Return New ViewUrlManager()
    End Function

    Protected Overrides Sub CreateDefaultObjectSpaceProvider(ByVal args As CreateCustomObjectSpaceProviderEventArgs)
        args.ObjectSpaceProvider = New SecuredObjectSpaceProvider(DirectCast(Me.Security, ISelectDataSecurityProvider), GetDataStoreProvider(args.ConnectionString, args.Connection), True)
        args.ObjectSpaceProviders.Add(New NonPersistentObjectSpaceProvider(TypesInfo, Nothing))
    End Sub
    Private Function GetDataStoreProvider(ByVal connectionString As String, ByVal connection As System.Data.IDbConnection) As IXpoDataStoreProvider
        Dim application As System.Web.HttpApplicationState = If((System.Web.HttpContext.Current IsNot Nothing), System.Web.HttpContext.Current.Application, Nothing)
        Dim dataStoreProvider As IXpoDataStoreProvider = Nothing
        If Not application Is Nothing AndAlso application("DataStoreProvider") IsNot Nothing Then
            dataStoreProvider = TryCast(application("DataStoreProvider"), IXpoDataStoreProvider)
        Else
		    dataStoreProvider = XPObjectSpaceProvider.GetDataStoreProvider(connectionString, connection, True)
            If Not application Is Nothing Then
                application("DataStoreProvider") = dataStoreProvider
            End If
        End If
        Return dataStoreProvider
    End Function
    Private Sub TimeCoreAspNetApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs) Handles MyBase.DatabaseVersionMismatch
#If EASYTEST Then
        e.Updater.Update()
        e.Handled = True
#Else
        If System.Diagnostics.Debugger.IsAttached Then
            e.Updater.Update()
            e.Handled = True
        Else
            Dim message As String = "The application cannot connect to the specified database, " & _
				"because the database doesn't exist, its version is older " & _
				"than that of the application or its schema does not match " & _
				"the ORM data model structure. To avoid this error, use one " & _
				"of the solutions from the https://www.devexpress.com/kb=T367835 KB Article."

            If e.CompatibilityError IsNot Nothing AndAlso e.CompatibilityError.Exception IsNot Nothing Then
                message &= Constants.vbCrLf & Constants.vbCrLf & "Inner exception: " & e.CompatibilityError.Exception.Message
            End If
            Throw New InvalidOperationException(message)
        End If
#End If
    End Sub
    Private Sub InitializeComponent()
        Me.module1 = New DevExpress.ExpressApp.SystemModule.SystemModule()
        Me.module2 = New DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule()
        Me.module3 = New TimeCore.Module.TimeCoreModule()
        Me.module4 = New TimeCore.Module.Web.TimeCoreAspNetModule()
        Me.securityModule1 = New DevExpress.ExpressApp.Security.SecurityModule()
        Me.securityStrategyComplex1 = New DevExpress.ExpressApp.Security.SecurityStrategyComplex()
        Me.securityStrategyComplex1.SupportNavigationPermissionsForTypes = False
        Me.authenticationStandard1 = New DevExpress.ExpressApp.Security.AuthenticationStandard()
        Me.objectsModule = New DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule()
        Me.chartModule = New DevExpress.ExpressApp.Chart.ChartModule()
        Me.chartAspNetModule = New DevExpress.ExpressApp.Chart.Web.ChartAspNetModule()
        Me.conditionalAppearanceModule = New DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule()
        Me.dashboardsModule = New DevExpress.ExpressApp.Dashboards.DashboardsModule()
        Me.dashboardsAspNetModule = New DevExpress.ExpressApp.Dashboards.Web.DashboardsAspNetModule()
        Me.kpiModule = New DevExpress.ExpressApp.Kpi.KpiModule()
        Me.schedulerModuleBase = New DevExpress.ExpressApp.Scheduler.SchedulerModuleBase()
        Me.schedulerAspNetModule = New DevExpress.ExpressApp.Scheduler.Web.SchedulerAspNetModule()
        Me.stateMachineModule = New DevExpress.ExpressApp.StateMachine.StateMachineModule()
        Me.treeListEditorsModuleBase = New DevExpress.ExpressApp.TreeListEditors.TreeListEditorsModuleBase()
        Me.treeListEditorsAspNetModule = New DevExpress.ExpressApp.TreeListEditors.Web.TreeListEditorsAspNetModule()
        Me.validationModule = New DevExpress.ExpressApp.Validation.ValidationModule()
        Me.validationAspNetModule = New DevExpress.ExpressApp.Validation.Web.ValidationAspNetModule()
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
        '
        ' stateMachineModule
        '
        Me.stateMachineModule.StateMachineStorageType = GetType(DevExpress.ExpressApp.StateMachine.Xpo.XpoStateMachine)
        ' 
        ' TimeCoreAspNetApplication
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
        Me.Modules.Add(Me.chartAspNetModule)
        Me.Modules.Add(Me.conditionalAppearanceModule)
        Me.Modules.Add(Me.dashboardsModule)
        Me.Modules.Add(Me.dashboardsAspNetModule)
        Me.Modules.Add(Me.kpiModule)
        Me.Modules.Add(Me.schedulerModuleBase)
        Me.Modules.Add(Me.schedulerAspNetModule)
        Me.Modules.Add(Me.stateMachineModule)
        Me.Modules.Add(Me.treeListEditorsModuleBase)
        Me.Modules.Add(Me.treeListEditorsAspNetModule)
        Me.Modules.Add(Me.validationModule)
        Me.Modules.Add(Me.validationAspNetModule)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
End Class

