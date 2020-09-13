Imports System.Runtime.CompilerServices

Public Module Extensions
    <Extension()>
    Public Sub InvokeIfRequired(ByVal Control As Control, ByVal Method As [Delegate], ByVal ParamArray Parameters As Object())
        If Parameters Is Nothing OrElse
            Parameters.Length = 0 Then Parameters = Nothing 'If Parameters is null or has a length of zero then no parameters should be passed.
        If Control.InvokeRequired = True Then
            Control.Invoke(Method, Parameters)
        Else
            Method.DynamicInvoke(Parameters)
        End If
    End Sub
End Module
