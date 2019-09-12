Partial Class ViewController1

    <System.Diagnostics.DebuggerNonUserCode()>
    Public Sub New(ByVal Container As System.ComponentModel.IContainer)
        MyClass.New()

        'Required for Windows.Forms Class Composition Designer support
        Container.Add(Me)

    End Sub

    'Component overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.SimpleAction1 = New DevExpress.ExpressApp.Actions.SimpleAction(Me.components)
        '
        'SimpleAction1
        '
        Me.SimpleAction1.Caption = "Test"
        Me.SimpleAction1.ConfirmationMessage = Nothing
        Me.SimpleAction1.Id = "bec7621b-ff64-4465-8da9-39ec558269a4"
        Me.SimpleAction1.ToolTip = Nothing
        '
        'ViewController1
        '
        Me.Actions.Add(Me.SimpleAction1)

    End Sub

    Friend WithEvents SimpleAction1 As DevExpress.ExpressApp.Actions.SimpleAction
End Class
