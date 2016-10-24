Imports System.IO

Public Class frmStorage
    Dim Tokens As TokenList

    Private Sub frmStorage_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim bf As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
        Dim fs As New FileStream("C:\texts.txt", FileMode.Create)

        Tokens = Nothing
        Tokens = CType(bf.Deserialize(New System.IO.MemoryStream(My.Resources.TI83)), TokenList)

        bf.Serialize(fs, Tokens.TextList)

        fs.Close()
        fs = New FileStream("C:\paths.pth", FileMode.Create)

        bf.Serialize(fs, Tokens.PathList)

        fs.Close()
        fs = New FileStream("C:\bytes.byt", FileMode.Create)

        bf.Serialize(fs, Tokens.ByteList)

        fs.Close()

    End Sub
End Class