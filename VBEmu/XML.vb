﻿Imports System.Collections.Specialized
Imports System.Xml
Imports System.IO

Module XML
    Function readGame(ByVal filename, ByVal sys)
        Main.ProgressBar1.Value = 0
        Dim files() As String = IO.Directory.GetFiles(System.IO.Directory.GetParent(filename).ToString)
        Dim gamelistpaths As New StringCollection()
        Dim games As New Collection
        Dim nometadatagames As New StringCollection()
        Dim id = 1
        Dim isrom As Boolean
        Dim ingamelist As Boolean
        Main.ProgressBar1.Maximum = files.Count() * 2
        Dim settings = New XmlReaderSettings()
        settings.ConformanceLevel = ConformanceLevel.Fragment
        Dim reader As XmlReader = XmlReader.Create(filename, settings)
        On Error Resume Next
        Do
            reader.ReadToFollowing("game")
            Dim doc As New XmlDocument
            doc.LoadXml("<game>" + reader.ReadInnerXml() + "</game>")
            Dim path = doc.GetElementsByTagName("path")(0).InnerText
            gamelistpaths.Add(path.Remove(0, 2).Trim)
            Dim name = doc.GetElementsByTagName("name")(0).InnerText
            Dim desc = doc.GetElementsByTagName("desc")(0).InnerText
            Dim image = doc.GetElementsByTagName("image")(0).InnerText
            Dim developer = doc.GetElementsByTagName("developer")(0).InnerText
            Dim genre = doc.GetElementsByTagName("genre")(0).InnerText
            If files.Contains(System.IO.Directory.GetParent(filename).ToString + "\" + path.Remove(0, 2).Trim) Then
                If Not name = "" Then
                    games.Add(New Game(path, name, image, genre, developer, desc, id))
                End If
            End If

            Main.ProgressBar1.PerformStep()
        Loop While Not reader.EOF
        id = id + 1
        For Each f In files
            isrom = False
            inGameList = False
            f = f.Remove(0, f.LastIndexOf("\") + 1).Trim
            For Each g In gamelistpaths
                If g = f Then
                    ingamelist = True
                End If
            Next
            If Not ingamelist Then
                For Each e In sys.getExtensions()
                    If f.EndsWith(e) And e <> "" Then
                        isrom = True
                    End If
                Next
                If isrom Then
                    nometadatagames.Add(f)
                End If
            End If
        Next
        For Each f In nometadatagames
            games.Add(New Game("./" + f, f, "", "", "", "", id))
            id = id + 1
            Main.ProgressBar1.PerformStep()
        Next
        Return games
    End Function
    Function readGamesystem(ByVal filename)
        Dim rawXML As String
        rawXML = My.Computer.FileSystem.ReadAllText(filename).Replace("&", "andsymbol")
        rawXML = rawXML.Replace("&amp;lt;!-- This is the EmulationStation Systems configuration file.", vbNullString)
        rawXML = rawXML.Replace("All systems must be contained within the &amp;lt;systemList&amp;gt; tag.--&amp;gt;", vbNullString)
        Dim consoles = New Collection
        Dim reader As XmlReader = XmlReader.Create(New StringReader(rawXML))
        Do
            reader.ReadToFollowing("name")
            Dim name = reader.ReadInnerXml()
            reader.ReadToFollowing("fullname")
            Dim fullname = reader.ReadInnerXml()
            reader.ReadToFollowing("path")
            Dim path = reader.ReadInnerXml()
            reader.ReadToFollowing("extension")
            Dim extension = reader.ReadInnerXml()
            reader.ReadToFollowing("command")
            Dim command = reader.ReadInnerXml()
            consoles.Add(New gameSystem(path, name, fullname, command.Replace("andsymbol", "&"), extension))
        Loop While reader.ReadToFollowing("system")
        Return consoles
    End Function
End Module
