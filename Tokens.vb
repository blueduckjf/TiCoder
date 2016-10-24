

<Serializable()> Public Class TokenList
    Private _texts(255, 255) As String
    Private _paths(255, 255) As String
    Private _bytes As Hashtable
    Private _sig As String

    Public ReadOnly Property Signature() As String
        Get
            Return _sig
        End Get
    End Property
    Public ReadOnly Property TextList() As String(,)
        Get
            Return _texts
        End Get
    End Property
    Public ReadOnly Property PathList() As String(,)
        Get
            Return _paths
        End Get
    End Property
    Public ReadOnly Property ByteList() As Hashtable
        Get
            Return _bytes
        End Get
    End Property

    Public Sub New()

    End Sub

    Public Sub New(ByVal strTexts(,) As String, ByVal strPaths(,) As String, ByVal bytBytes As Hashtable, ByVal strSig As String)
        _texts = strTexts
        _paths = strPaths
        _bytes = bytBytes
        _sig = strSig
    End Sub


End Class
