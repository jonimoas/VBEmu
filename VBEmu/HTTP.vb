Imports System.IO
Imports System.Net
Imports System.Web.Script.Serialization
Module HTTP
    Dim TGDBdevelopers As String = Nothing
    Dim TGDBgenres As String = Nothing
    Function updateMetaDataRAWG(ByVal g As Game)
        Dim myReq As HttpWebRequest
        Dim myResp As HttpWebResponse
        Dim myReader As StreamReader
        Dim stringResult As String
        Dim sanitizedName = g.getName()
        If g.getName().indexOf("(") > 0 Then
            sanitizedName = g.getName().Substring(0, g.getName().indexOf("("))
        End If
        myReq = HttpWebRequest.Create("https://api.rawg.io/api/games?key=" + My.Settings.RAWGToken + "&search=" + sanitizedName)
        myReq.Method = "GET"
        myResp = myReq.GetResponse()
        myReader = New System.IO.StreamReader(myResp.GetResponseStream())
        stringResult = myReader.ReadToEnd()
        Dim gameID As Integer
        Dim jsonResult As Object = New JavaScriptSerializer().Deserialize(Of Object)(stringResult)
        gameID = jsonResult("results")(0)("id")
        myReq = HttpWebRequest.Create("https://api.rawg.io/api/games/" + gameID.ToString() + "?key=" + My.Settings.RAWGToken + "&search=" + g.getName())
        myReq.Method = "GET"
        myResp = myReq.GetResponse()
        myReader = New System.IO.StreamReader(myResp.GetResponseStream())
        stringResult = myReader.ReadToEnd()
        jsonResult = New JavaScriptSerializer().Deserialize(Of Object)(stringResult)
        Dim descriptionText = jsonResult("description_raw")
        Dim genre = jsonResult("genres")(0)("name")
        Dim name = jsonResult("name")
        Dim developer = jsonResult("developers")(0)("name")
        Dim image = jsonResult("background_image")
        Return New Game(g.getpath(), name, image, genre, developer, descriptionText, g.getId())
    End Function
    Function updateMetaDataTheGamesDB(ByVal g As Game)
        downloadDevelopersGenresTGDB()
        Dim myReq As HttpWebRequest
        Dim myResp As HttpWebResponse
        Dim myReader As StreamReader
        Dim stringResult As String
        Dim sanitizedName = g.getName()
        If g.getName().indexOf("(") > 0 Then
            sanitizedName = g.getName().Substring(0, g.getName().indexOf("("))
        End If
        myReq = HttpWebRequest.Create("https://api.thegamesdb.net/v1.1/Games/ByGameName?apikey=" + My.Settings.TheGamesDBToken + "&include=boxart&fields=overview,genres&name=" + sanitizedName)
        myReq.Method = "GET"
        myResp = myReq.GetResponse()
        myReader = New System.IO.StreamReader(myResp.GetResponseStream())
        stringResult = myReader.ReadToEnd()
        Dim jsonResult As Object = New JavaScriptSerializer().Deserialize(Of Object)(stringResult)
        Dim gameID = jsonResult("data")("games")(0)("id")
        Dim descriptionText = jsonResult("data")("games")(0)("overview")
        Dim name = jsonResult("data")("games")(0)("game_title")
        Dim image = jsonResult("include")("boxart")("base_url")("medium") + jsonResult("include")("boxart")("data")(gameID)(0)("filename")
        Dim genreJson As Object = New JavaScriptSerializer().Deserialize(Of Object)(TGDBgenres)
        Dim genre = Nothing
        Dim developer = Nothing
        Try
            genre = genreJson("data")("genres")(jsonResult("data")("games")(0)("genres")(0))("name")
        Catch
            genre = Nothing
        End Try
        Dim developerJson As Object = New JavaScriptSerializer().Deserialize(Of Object)(TGDBdevelopers)
        Try
            developer = developerJson("data")("developers")(jsonResult("data")("games")(0)("developers")(0))("name")
        Catch
            developer = Nothing
        End Try
        Return New Game(g.getpath(), name, image, genre, developer, descriptionText, g.getId())
    End Function
    Sub downloadDevelopersGenresTGDB()
        If TGDBdevelopers = Nothing Then
            Dim myReq As HttpWebRequest
            Dim myResp As HttpWebResponse
            Dim myReader As StreamReader
            myReq = HttpWebRequest.Create("https://api.thegamesdb.net/v1/Developers?apikey=" + My.Settings.TheGamesDBToken)
            myReq.Method = "GET"
            myResp = myReq.GetResponse()
            myReader = New System.IO.StreamReader(myResp.GetResponseStream())
            TGDBdevelopers = myReader.ReadToEnd()
            System.Console.WriteLine(TGDBdevelopers)
        End If
        If TGDBgenres = Nothing Then
            Dim myReq As HttpWebRequest
            Dim myResp As HttpWebResponse
            Dim myReader As StreamReader
            myReq = HttpWebRequest.Create("https://api.thegamesdb.net/v1/Genres?apikey=" + My.Settings.TheGamesDBToken)
            myReq.Method = "GET"
            myResp = myReq.GetResponse()
            myReader = New System.IO.StreamReader(myResp.GetResponseStream())
            TGDBgenres = myReader.ReadToEnd()
            System.Console.WriteLine(TGDBgenres)
        End If
    End Sub
    Function downloadImage(ByVal g As Game, ByVal s As gameSystem)
        Dim Client As New WebClient
        Client.DownloadFile(g.getImage(), s.getPath() + "/images/" + g.getName().ToString().Replace(":", "-").Replace("/", "-") + "-image.jpg")
        Client.Dispose()
        Return New Game(g.getpath(), g.getName(), "./images/" + g.getName().ToString().Replace(":", "-").Replace("/", "-") + "-image.jpg", g.getGenre(), g.getDeveloper(), g.getDescription(), g.getId())
    End Function
End Module
