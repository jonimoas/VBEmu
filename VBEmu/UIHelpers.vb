Module UIHelpers
    Public Function filter(ByVal genref, ByVal developerf, ByVal gamelist)
        Dim filteredgames = New Collection
        Dim gameControlList = New Collection
        For Each g In gamelist
            If g.getGenre() <> Nothing And g.getDeveloper() <> Nothing Then
                If (g.getGenre().trim = genref.trim Or genref.trim = "All") And (g.getDeveloper().trim = developerf.trim Or developerf = "All") Then
                    filteredgames.Add(g)
                End If
            End If
        Next
        For Each g In filteredgames
            gameControlList.Add(g.getName())
        Next
        Return gameControlList
    End Function
End Module
