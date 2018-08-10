Module XML
    Function readGame(ByVal filename)
            Dim gamelist As XElement = XElement.Load(filename)
            Dim games As New Collection
            Dim e As InvalidOperationException
            Dim e2 As NullReferenceException
            For Each game In gamelist.Nodes
                Try
                    Try
                        games.Add(New Game(game.ElementsAfterSelf.First.Element("path").Value, game.ElementsAfterSelf.First.Element("name").Value, game.ElementsAfterSelf.First.Element("image").Value, game.ElementsAfterSelf.First.Element("genre").Value, game.ElementsAfterSelf.First.Element("developer").Value, game.ElementsAfterSelf.First.Element("desc").Value))
                    Catch e
                        Return games
                    End Try
                Catch e2
                End Try
            Next
            Return games
    End Function
    Function readGamesystem(ByVal filename)
        Dim rawXML As String
        rawXML = My.Computer.FileSystem.ReadAllText(filename).Replace("&", "andsymbol")
        rawXML = rawXML.Replace("&amp;lt;!-- This is the EmulationStation Systems configuration file.", vbNullString)
        rawXML = rawXML.Replace("All systems must be contained within the &amp;lt;systemList&amp;gt; tag.--&amp;gt;", vbNullString)
        Dim consolelist As XElement = XElement.Parse(rawXML)
        Dim consoles As New Collection
        Dim e As InvalidOperationException
        For Each console In consolelist.Nodes
            Try
                consoles.Add(New gameSystem(console.ElementsAfterSelf.First.Element("path").Value, console.ElementsAfterSelf.First.Element("name").Value, console.ElementsAfterSelf.First.Element("fullname").Value, console.ElementsAfterSelf.First.Element("command").Value.Replace("andsymbol", "&")))
            Catch e
                Return consoles
            End Try
        Next
        Return consoles
    End Function
End Module
