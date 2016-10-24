Imports System
Imports java.util.zip

#Region " ENUMERATION "

Public Enum Action
    Compress = 0
    Decompress = 1
    AddEntries = 2
    RemoveEntries = 3
End Enum

#End Region

Public Class ZipUtility

#Region " EVENT HANDLERS "
    Public Event ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
    Public Event Completed(ByVal sender As Object, ByVal e As CompletedEventArgs)
#End Region

#Region " CLASS LEVEL VARIABLES "

    Dim _originalSize As Long
    Dim _bytesRead As Long
    Dim _percentage As Integer
    Dim _filesProcessed As Integer
    Dim _onePercent As Long
    Dim _cumulativePercent As Long

#End Region

#Region " PUBLIC METHODES "

#Region "    COMPRESS "

    ''' <summary>
    ''' Compresses files and folders into a zip file. 
    ''' </summary>
    ''' <param name="paths">A string-array of file and folder names(paths).</param>
    ''' <param name="zipFileName">A zip-file name.</param>
    Public Overloads Sub Compress(ByVal paths() As String, ByVal zipFileName As String)
        'ZIP FILE OUTPUT STREAM
        Dim zos As ZipOutputStream = Nothing
        Try
            If Array.IndexOf(paths, zipFileName) <> -1 Then
                Throw New Exception("Cannot copy a Compressed (zipped) file onto itself.")
            End If
            'OUTPUT STREAM THAT WILL WRITE OUT THE ZIP FILE, THIS 
            zos = New ZipOutputStream(New java.io.FileOutputStream(zipFileName))
            'SET THE CLASS LEVEL VARIABLES
            Me.ResetValues()
            Me.SetValues(paths)
            Me._onePercent = CLng(Me._originalSize / 100)
            'COMPRESS THE FILES
            Me.Compress(zos, paths)
        Catch ex As Exception
            'PASS THE EXCEPTION TO THE CALLING ROUTINE
            Throw ex
        Finally
            'ALL DONE, CLOSE THE ZIP FILE
            If Not zos Is Nothing Then
                zos.flush()
                zos.close()
            End If
        End Try
        Me.OnCompleted(Action.Compress, zipFileName)
    End Sub

#End Region

#Region "    DECOMPRESS "

    ''' <summary>
    ''' Decompresses zip files.
    ''' </summary>
    ''' <param name="zipFileName">A zip-file name.</param>
    ''' <param name="saveToDir">A directory where the decompressed file to be placed.</param>
    Public Sub Decompress(ByVal zipFileName As String, ByVal saveToDir As String)
        'ZIP FILE INPUT STREAM
        Dim zis As ZipInputStream = Nothing
        'FILE OUTPUT STREAM
        Dim fos As java.io.FileOutputStream = Nothing
        'HOLDS ZIP ENTRY. USED FOR EACH ZIP ENTRY
        Dim entry As ZipEntry
        'SET THE CLASS LEVEL VARIABLES
        Me.ResetValues()
        Me.SetValues(zipFileName)
        Me._onePercent = CLng(Me._originalSize / 100)
        Try
            'CREATE A FILE STREAM TO READ THE ZIP FILE TO THE FILE
            zis = New ZipInputStream(New java.io.FileInputStream(zipFileName))
            Do
                'GET NEXT ZIP ENTRY FROM THE STREAM
                entry = zis.getNextEntry
                'EXIT THE LOOP IF THERE ARE NO MORE ENTRIS
                If entry Is Nothing Then
                    Exit Do
                Else
                    Try
                        'CREATE AN EMPTY DIRECTORY
                        IO.Directory.CreateDirectory(IO.Path.Combine(saveToDir, _
                        IO.Path.GetDirectoryName(entry.getName.Replace("/", "\"))))
                        'CONTINUE IF THE ZIP ENTRY IS A FILE
                        If Not entry.isDirectory Then
                            'CREATE A FILE STREAM TO WRITE THE ZIP FILE TO THE FILE
                            fos = New java.io.FileOutputStream(IO.Path.Combine _
                            (saveToDir, entry.getName.Replace("/", "\")), False)
                            'LOOP THE INPUT FILE STREAM AND WRITE IT TO THE ZIP FILE
                            Me.CopyStream(zis, fos, IO.Path.GetFileName(entry.getName))
                        End If
                    Catch ex As Exception
                        'PASS THE EXCEPTION TO THE CALLING ROUTINE
                        Throw ex
                    Finally
                        'CLOSE THE FILE
                        If Not fos Is Nothing Then
                            fos.flush()
                            fos.close()
                        End If
                    End Try
                End If
            Loop
        Catch ex As Exception
            'PASS THE EXCEPTION TO THE CALLING ROUTINE
            Throw ex
        Finally
            'WE GOT ALL THE ZIP ENTRY NAMES, CLOSE THE ZIP FILE
            If Not zis Is Nothing Then
                zis.close()
            End If
        End Try
        Me.OnCompleted(Action.Decompress, zipFileName)
    End Sub

#End Region

#Region "    ADD ENTRIES "
    ''' <summary>
    ''' Creates a new zip file and writes all the entries from the specified 
    ''' zip file and the additional entries into the new zip file.
    ''' </summary>
    ''' <param name="zipFileName">A zip file from which all 
    ''' the entries should be copied to a new zip file.</param>
    ''' <param name="paths">A string-array of file and folder names(paths).</param>
    ''' <param name="newZipFile">A new zip file that will contain all 
    ''' the entries from the specified zip file and the added entries.</param>
    ''' <remarks></remarks>
    Public Sub AddEntries(ByVal zipFileName As String, ByVal paths() As String, ByVal newZipFile As String)
        'ZIP FILE INPUT STREAM
        Dim zis As ZipInputStream = Nothing
        'ZIP FILE OUTPUT STREAM
        Dim zos As ZipOutputStream = Nothing
        'HOLDS ZIP ENTRY. USED FOR EACH ZIP ENTRY
        Dim entry As ZipEntry
        'SET THE CLASS LEVEL VARIABLES
        Me.ResetValues()
        Me.SetValues(paths, zipFileName)
        Me._onePercent = CLng(Me._originalSize / 100)
        Try
            If zipFileName = newZipFile Then
                Throw New Exception("Cannot copy a Compressed (zipped) file onto itself.")
            End If
            'CREATE A FILE STREAM TO READ THE ZIP FILE TO THE FILE
            zis = New ZipInputStream(New java.io.FileInputStream(zipFileName))
            'OUTPUT STREAM THAT WILL WRITE OUT THE ZIP FILE, THIS 
            zos = New ZipOutputStream(New java.io.FileOutputStream(newZipFile))
            entry = zis.getNextEntry()
            While Not entry Is Nothing
                zos.putNextEntry(entry)
                If Not entry.isDirectory Then
                    Me.CopyStream(zis, zos, IO.Path.GetFileName(entry.getName))
                End If
                'CLOSE THE NEW ENTRY IN THE ZIP FILE
                zos.closeEntry()
                entry = zis.getNextEntry()
            End While
            'COMPRESS THE FILES
            Me.Compress(zos, paths)
        Catch ex As Exception
            'PASS THE EXCEPTION TO THE CALLING ROUTINE
            Throw ex
        Finally
            If Not zis Is Nothing Then
                zis.close()
            End If
            If Not zos Is Nothing Then
                zos.flush()
                zos.close()
            End If
        End Try
        Me.OnCompleted(Action.AddEntries, newZipFile)
    End Sub

#End Region

#Region "    REMOVE ENTRIES "

    ''' <summary>
    ''' Creates a new zip file and copies the desired entries 
    ''' from the specified zip file into the new zip file.
    ''' </summary>
    ''' <param name="zipFileName">A zip file from which the 
    ''' desired entries should be copied to a new zip file.</param>
    ''' <param name="entryNames">Zip-entry names that should 
    ''' be excluded of coping into a new zip file.</param>
    ''' <param name="newZipFile">A new zip file that will 
    ''' contain the desired zip-entries with their data.</param>
    Public Sub RemoveEntries(ByVal zipFileName As String, ByVal entryNames() As String, ByVal newZipFile As String)
        'ZIP FILE INPUT STREAM
        Dim zis As ZipInputStream = Nothing
        'ZIP FILE OUTPUT STREAM
        Dim zos As ZipOutputStream = Nothing
        'HOLDS ZIP ENTRY. USED FOR EACH ZIP ENTRY
        Dim entry As ZipEntry
        'SET THE CLASS LEVEL VARIABLES
        Me.ResetValues()
        Me.SetValues(zipFileName, entryNames)
        Me._onePercent = CLng(Me._originalSize / 100)
        Try
            If zipFileName = newZipFile Then
                Throw New Exception("Cannot copy a Compressed (zipped) file onto itself.")
            End If
            'CREATE A FILE STREAM TO READ THE ZIP FILE TO THE FILE
            zis = New ZipInputStream(New java.io.FileInputStream(zipFileName))
            'OUTPUT STREAM THAT WILL WRITE OUT THE ZIP FILE, THIS 
            zos = New ZipOutputStream(New java.io.FileOutputStream(newZipFile))
            entry = zis.getNextEntry()
            While Not entry Is Nothing
                If Array.IndexOf(entryNames, entry.getName) = -1 Then
                    zos.putNextEntry(entry)
                    If Not entry.isDirectory Then
                        Me.CopyStream(zis, zos, IO.Path.GetFileName(entry.getName))
                    End If
                    If Me._originalSize = Me._bytesRead Then
                        Exit While
                    End If
                    'CLOSE THE NEW ENTRY IN THE ZIP FILE
                    zos.closeEntry()
                End If
                entry = zis.getNextEntry()
            End While
        Catch ex As Exception
            'PASS THE EXCEPTION TO THE CALLING ROUTINE
            Throw ex
        Finally
            If Not zis Is Nothing Then
                zis.close()
            End If
            If Not zos Is Nothing Then
                zos.flush()
                zos.close()
            End If
        End Try
        Me.OnCompleted(Action.RemoveEntries, newZipFile)
    End Sub

#End Region

#Region "    GET ZIP ENTRIES "

    ''' <summary>Gets the entries of the zip file.</summary>
    ''' <param name="zipFileName">The zip file name.</param>
    Public Function GetEntries(ByVal zipFileName As String) As ZipEntry()
        'HOLD THE ENTRY NAMES
        Dim entrieList As New List(Of ZipEntry)
        Dim zf As ZipFile = Nothing
        Try
            'OPEN ZIP FILE
            zf = New ZipFile(zipFileName)
            Dim entries As ZipFile.ZipEntryEnum
            'CREATE ENTRY ENUMARATION
            entries = CType(zf.entries, ZipFile.ZipEntryEnum)
            While entries.hasMoreElements
                'FILL THE ENTRY LIST
                entrieList.Add(CType(entries.nextElement, ZipEntry))
            End While
        Catch ex As Exception
            'PASS THE EXCEPTION TO THE CALLING ROUTINE
            Throw ex
        Finally
            'WE GOT ALL THE ZIP ENTRY NAMES, CLOSE THE ZIP FILE
            If Not zf Is Nothing Then
                zf.close()
            End If
        End Try
        Return entrieList.ToArray
    End Function

#End Region

#Region "    IS DIRECTORY "

    ''' <summary>
    ''' Returns True if the entry is a directory otherwise returns False.
    ''' </summary>
    ''' <param name="entry">A zip entry.</param>
    Public Function IsDirectory(ByVal entry As ZipEntry) As Boolean
        Return entry.isDirectory
    End Function

#End Region

#Region "    FORMAT SIZE "

    ''' <summary>
    ''' Formats byte-size to bytes, KB, MB or GB.
    ''' </summary>
    ''' <param name="size">Size in bytes.</param>
    Public Function FormatSize(ByVal size As Long) As String
        Select Case True
            Case size < 1024
                Return size & " bytes"
            Case size < 1024 * 1024
                Return Me.NumberFormat(CDec(size / 1024)) & " KB"
            Case size < 1024 * 1024 * 1024
                Return Me.NumberFormat(CDec(size / (1024 * 1024))) & " MB"
            Case Else
                Return Me.NumberFormat(CDec(size / (1024 * 1024 * 1024))) & " GB"
        End Select
    End Function

#End Region

#End Region

#Region " PRIVATE METHODES "

    Private Overloads Sub Compress(ByVal zos As ZipOutputStream, ByVal paths() As String)
        'FILE INPUT STREAM
        Dim fis As java.io.FileInputStream = Nothing
        'OUR ZIP ENTRY, ONE FOR EACH FILE
        Dim myZipEntry As ZipEntry
        'TO HOLD ENTRY NAME FOR EACH ENTRY
        Dim entryName As String
        'LOOP EACH FILE NAME IN OUR ARRAY
        For Each fileName As String In paths
            For Each fsInfo As IO.FileSystemInfo In Me.GetFileSystemInfos(fileName)
                'CREATE ZIPENTRY NAME
                entryName = IO.Path.GetFileName(fileName)
                entryName = fsInfo.FullName.Substring(fsInfo.FullName.IndexOf(entryName))
                entryName = entryName.Replace("\"c, "/"c)
                Try
                    If (fsInfo.Attributes And IO.FileAttributes.Compressed) <> 0 Then
                        'CREATE A NEW ENTRY IN THE ZIP FILE, BASED ON THE FILE NAME
                        myZipEntry = New ZipEntry(entryName)
                        'SET IT TO STORED (ORIGINAL)
                        myZipEntry.setMethod(ZipEntry.STORED)
                        'STICK THE ENTRY IN THE ZIP FILE
                        zos.putNextEntry(myZipEntry)
                        'CREATE A FILE STREAM TO WRITE THE FILE TO THE ZIP
                        fis = New java.io.FileInputStream(fsInfo.FullName)
                        'LOOP THE INPUT FILE STREAM AND WRITE IT TO THE ZIP FILE
                        Me.CopyStream(fis, zos, fsInfo.Name)
                    ElseIf (fsInfo.Attributes And IO.FileAttributes.Directory) <> 0 Then
                        entryName &= "/"
                        'CREATE A NEW ENTRY IN THE ZIP FILE, BASED ON THE FILE NAME
                        myZipEntry = New ZipEntry(entryName)
                        'SET IT TO DEFLATE (COMPACT IT)
                        myZipEntry.setMethod(ZipEntry.DEFLATED)
                        'STICK THE ENTRY IN THE ZIP FILE
                        zos.putNextEntry(myZipEntry)
                    ElseIf (fsInfo.Attributes And IO.FileAttributes.Archive) <> 0 Then
                        'CREATE A NEW ENTRY IN THE ZIP FILE, BASED ON THE FILE NAME
                        myZipEntry = New ZipEntry(entryName)
                        'SET IT TO DEFLATE (COMPACT IT)
                        myZipEntry.setMethod(ZipEntry.DEFLATED)
                        'STICK THE ENTRY IN THE ZIP FILE
                        zos.putNextEntry(myZipEntry)
                        'CREATE A FILE STREAM TO WRITE THE FILE TO THE ZIP
                        fis = New java.io.FileInputStream(fsInfo.FullName)
                        'LOOP THE INPUT FILE STREAM AND WRITE IT TO THE ZIP FILE
                        Me.CopyStream(fis, zos, fsInfo.Name)
                    End If
                Catch ex As Exception
                    'PASS THE EXCEPTION TO THE CALLING ROUTINE
                    Throw ex
                Finally
                    'CLOSE THE NEW ENTRY IN THE ZIP FILE
                    zos.closeEntry()
                    'CLOSE THE FILE STREAM FOR THE FILE BEING ZIPPED
                    If Not fis Is Nothing Then
                        fis.close()
                    End If
                End Try
            Next
        Next
    End Sub

    ''' <summary>
    ''' Copies binary data from one stream to another.
    ''' </summary>
    ''' <param name="input">The source stream.</param>
    ''' <param name="output"> The destination stream.</param>
    Private Sub CopyStream(ByVal input As java.io.InputStream, ByVal output As java.io.OutputStream, ByVal fileName As String)
        'READ UP TO 5 KB AT A TIME
        Dim buffer(5119) As SByte
        'READ INTO THE BUFFER
        Dim bytesRead As Integer = input.read(buffer, 0, buffer.Length)
        While bytesRead > 0
            output.write(buffer, 0, bytesRead)
            Me._bytesRead += bytesRead
            bytesRead = input.read(buffer, 0, buffer.Length)
            If bytesRead = -1 Then
                Me._filesProcessed += 1
            End If
            If (Me._bytesRead - Me._cumulativePercent) >= Me._onePercent Then
                Me._cumulativePercent += Me._onePercent
                Me._percentage += 1
                Me.OnProgressChanged(fileName)
            End If
        End While
    End Sub

    Private Overloads Function GetFileSystemInfos(ByVal path As String) As List(Of IO.FileSystemInfo)
        Dim fsInfos As New List(Of IO.FileSystemInfo)
        If IO.File.Exists(path) Then
            Dim fInfo As New IO.FileInfo(path)
            If Not (fInfo.Attributes And IO.FileAttributes.Hidden) <> 0 Then
                fsInfos.Add(fInfo)
            End If
        ElseIf IO.Directory.Exists(path) Then
            Dim dInfo As New IO.DirectoryInfo(path)
            If Not (dInfo.Attributes And IO.FileAttributes.Hidden) <> 0 Then
                Me.GetFileSystemInfos(dInfo, fsInfos)
            End If
        End If
        Return fsInfos
    End Function

    Private Overloads Sub GetFileSystemInfos(ByVal dInfo As IO.DirectoryInfo, ByRef fsInfos As List(Of IO.FileSystemInfo))
        fsInfos.Add(dInfo)
        For Each fInfo As IO.FileInfo In dInfo.GetFiles
            If Not (fInfo.Attributes And IO.FileAttributes.Hidden) <> 0 Then
                fsInfos.Add(fInfo)
            End If
        Next
        For Each di As IO.DirectoryInfo In dInfo.GetDirectories
            If Not (di.Attributes And IO.FileAttributes.Hidden) <> 0 Then
                Me.GetFileSystemInfos(di, fsInfos)
            End If
        Next
    End Sub

    Private Overloads Sub SetValues(ByVal paths() As String)
        For Each path As String In paths
            If IO.File.Exists(path) Then
                Dim fi As New IO.FileInfo(path)
                If Not (fi.Attributes And IO.FileAttributes.Hidden) <> 0 Then
                    Me._originalSize += fi.Length
                End If
            ElseIf IO.Directory.Exists(path) Then
                Me.SetTotalSize(New IO.DirectoryInfo(path))
            End If
        Next
    End Sub

    Private Overloads Sub SetValues(ByVal paths() As String, ByVal zipFile As String)
        Me._originalSize = Me.GetEntriesSize(zipFile)
        Me.SetValues(paths)
    End Sub

    Private Overloads Sub SetValues(ByVal zipFile As String)
        Me._originalSize = Me.GetEntriesSize(zipFile)
    End Sub

    Private Overloads Sub SetValues(ByVal zipFile As String, ByVal entryNames() As String)
        Me._originalSize = Me.GetEntriesSize(zipFile, entryNames)
    End Sub

    Private Sub ResetValues()
        Me._originalSize = 0
        Me._bytesRead = 0
        Me._percentage = 0
        Me._filesProcessed = 0
        Me._onePercent = 0
        Me._cumulativePercent = 0
    End Sub

    Private Overloads Sub SetTotalSize(ByVal dInfo As IO.DirectoryInfo)
        For Each fInfo As IO.FileInfo In dInfo.GetFiles
            If Not (fInfo.Attributes And IO.FileAttributes.Hidden) <> 0 Then
                Me._originalSize += fInfo.Length
            End If
        Next
        For Each di As IO.DirectoryInfo In dInfo.GetDirectories
            Me.SetTotalSize(di)
        Next
    End Sub

    Private Function NumberFormat(ByVal dec As Decimal) As String
        'Cast to string.
        Dim dStr As String = CStr(dec)
        'Get the index of the decimal point.
        Dim index As Integer = dStr.IndexOf("."c)
        If index <> -1 Then
            'Remove the decimal point.
            dStr = dStr.Remove(index, 1)
        End If
        If dStr.Length < 3 Then
            If index = -1 Then
                'Set the index of the decimal point.
                index = dStr.Length
            End If
            'Add '0's to the end
            dStr = dStr.PadRight(3, "0"c)
        Else
            'Get required number of digits.
            dStr = dStr.Substring(0, 3)
        End If
        If index <> -1 AndAlso index < 3 Then
            'Insert the decimal point
            dStr = dStr.Insert(index, ".")
        End If
        Return dStr
    End Function

    Private Overloads Function GetEntriesSize(ByVal zipFile As String) As Long
        Dim zf As ZipFile = Nothing
        Dim mySize As Long
        Try
            'OPEN ZIP FILE
            zf = New ZipFile(zipFile)
            Dim entries As ZipFile.ZipEntryEnum
            'CREATE ENTRY ENUMARATION
            entries = CType(zf.entries, ZipFile.ZipEntryEnum)
            While entries.hasMoreElements
                'ADD THE ENTRY SIZE
                mySize += CType(entries.nextElement, ZipEntry).getSize
            End While
        Catch ex As Exception
            'PASS THE EXCEPTION TO THE CALLING ROUTINE
            Throw ex
        Finally
            'CLOSE THE ZIP FILE
            If Not zf Is Nothing Then
                zf.close()
            End If
        End Try
        Return mySize
    End Function

    Private Overloads Function GetEntriesSize(ByVal zipFile As String, ByVal entryNames() As String) As Long
        Dim zf As ZipFile = Nothing
        Dim entry As ZipEntry
        Dim mySize As Long
        Try
            'OPEN ZIP FILE
            zf = New ZipFile(zipFile)
            Dim entries As ZipFile.ZipEntryEnum
            'CREATE ENTRY ENUMARATION
            entries = CType(zf.entries, ZipFile.ZipEntryEnum)
            While entries.hasMoreElements
                entry = CType(entries.nextElement, ZipEntry)
                If Array.IndexOf(entryNames, entry.getName) = -1 Then
                    'ADD THE ENTRY SIZE
                    mySize += entry.getSize
                End If
            End While
        Catch ex As Exception
            'PASS THE EXCEPTION TO THE CALLING ROUTINE
            Throw ex
        Finally
            'CLOSE THE ZIP FILE
            If Not zf Is Nothing Then
                zf.close()
            End If
        End Try
        Return mySize
    End Function

#End Region

#Region " ON ZIP EVENT "

    Private Sub OnProgressChanged(ByVal fileName As String)
        RaiseEvent ProgressChanged(Me, New ProgressChangedEventArgs _
        (fileName, Me._filesProcessed, Me._percentage))
        Application.DoEvents()
    End Sub

    Private Sub OnCompleted(ByVal action As Action, ByVal zipFileName As String)
        Dim fi As New IO.FileInfo(zipFileName)
        RaiseEvent Completed(Me, New CompletedEventArgs(Action, fi.Length, _
        Me._filesProcessed, Me._originalSize, zipFileName))
        Application.DoEvents()
    End Sub

#End Region

#Region " EVENT ARGS "

    Public Class ProgressChangedEventArgs
        Inherits System.EventArgs
        Private _percentage As Integer
        Private _filesProcessed As Integer
        Private _fileName As String
        ''' <summary>
        ''' Gets the file name that is being processed.
        ''' </summary>
        ReadOnly Property FileName() As String
            Get
                Return Me._fileName
            End Get
        End Property
        ''' <summary>
        ''' Gets the count of processed files.
        ''' </summary>
        ReadOnly Property FilesProcessed() As Integer
            Get
                Return Me._filesProcessed
            End Get
        End Property
        ''' <summary>
        ''' Gets the percentage of the process that has been completed.
        ''' </summary>
        ReadOnly Property ProgressPercentage() As Integer
            Get
                Return Me._percentage
            End Get
        End Property

        Sub New(ByVal fileName As String, ByVal files_Processed As Integer, ByVal percentage As Integer)
            Me._fileName = fileName
            Me._filesProcessed = files_Processed
            Me._percentage = percentage
        End Sub
    End Class

    Public Class CompletedEventArgs
        Inherits System.EventArgs
        Private _action As Action
        Private _compressedSize As Long
        Private _fileCount As Integer
        Private _totalSize As Long
        Private _zipFileName As String
        ''' <summary>
        ''' Gets the action that is performed on the files.
        ''' </summary>
        ReadOnly Property Action() As Action
            Get
                Return Me._action
            End Get
        End Property
        ''' <summary>
        ''' Gets the compressed size of the zip file.
        ''' </summary>
        ReadOnly Property CompressedSize() As Long
            Get
                Return Me._compressedSize
            End Get
        End Property
        ''' <summary>
        ''' Gets the count of the compressed files in the archive.
        ''' </summary>
        ReadOnly Property FileCount() As Integer
            Get
                Return Me._fileCount
            End Get
        End Property
        ''' <summary>
        ''' Gets the uncompressed total size of the files.
        ''' </summary>
        ReadOnly Property OriginalSize() As Long
            Get
                Return Me._totalSize
            End Get
        End Property
        ''' <summary>
        ''' Gets the zip file name on which an action has been performed.
        ''' </summary>
        ReadOnly Property ZipFileName() As String
            Get
                Return Me._zipFileName
            End Get
        End Property

        Sub New(ByVal action As Action, ByVal compressedSize As Long, _
        ByVal fileCount As Integer, ByVal totalSize As Long, ByVal zFileName As String)
            Me._action = action
            Me._compressedSize = compressedSize
            Me._fileCount = fileCount
            Me._totalSize = totalSize
            Me._zipFileName = zFileName
        End Sub
    End Class

#End Region

End Class