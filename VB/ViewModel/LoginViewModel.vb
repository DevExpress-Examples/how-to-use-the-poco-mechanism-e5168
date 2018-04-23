Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Xpf.Mvvm
Imports DevExpress.Xpf.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Mvvm.POCO

Namespace Example.ViewModel
	<POCOViewModel> _
	Public Class LoginViewModel
		Protected Sub New()
		End Sub
		Public Shared Function Create() As LoginViewModel
			Return ViewModelSource.Create(Function() New LoginViewModel())
		End Function

		Private privateUserName As String
		Public Overridable Property UserName() As String
			Get
				Return privateUserName
			End Get
			Set(ByVal value As String)
				privateUserName = value
			End Set
		End Property
		Private privateError As String
        Public Overridable Property LoginError() As String
            Get
                Return privateError
            End Get
            Set(ByVal value As String)
                privateError = value
            End Set
        End Property
		Protected Overridable ReadOnly Property MessageBoxService() As IMessageBoxService
			Get
				Return Nothing
			End Get
		End Property

		Protected Sub OnUserNameChanged()
            Me.RaiseCanExecuteChanged(Sub(x) x.Login())
		End Sub
		Public Sub Login()
			allowValidate = True
			Validate()
			If (Not CanLogin()) Then
				ShowErrorMessage()
                Me.RaiseCanExecuteChanged(Sub(x) x.Login())
			Else
				ShowOK()
			End If
		End Sub
		Public Function CanLogin() As Boolean
			If (Not allowValidate) Then
				Return True
			End If
			Validate()
            Return String.IsNullOrEmpty(privateError)
		End Function
		Private allowValidate As Boolean = False
		Private Sub Validate()
			If String.IsNullOrEmpty(UserName) Then
                LoginError = "UserName cannot be empty"
			Else
                LoginError = Nothing
			End If
		End Sub
		Private Sub ShowErrorMessage()
            If (Not String.IsNullOrEmpty(privateError)) Then
                MessageBoxService.Show(privateError)
            End If
		End Sub
		Private Sub ShowOK()
			MessageBoxService.Show("OK")
		End Sub
	End Class
End Namespace
