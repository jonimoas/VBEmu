Imports System.IO
Imports System.Net
Imports System.Security.Policy
Imports System.Web.Script.Serialization
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Module HTTP
    Function updateMetaData(ByVal g As Game)
        Dim myReq As HttpWebRequest
        Dim myResp As HttpWebResponse
        Dim myReader As StreamReader
        Dim stringResult As String
        myReq = HttpWebRequest.Create("https://api.rawg.io/api/games?key=" + My.Settings.RAWGToken + "&search=" + g.getName())
        myReq.Method = "GET"
        myResp = myReq.GetResponse()
        myReader = New System.IO.StreamReader(myResp.GetResponseStream())
        stringResult = myReader.ReadToEnd()
        System.Console.WriteLine(stringResult)
        Dim gameID As Integer
        Dim jsonResult As Object = New JavaScriptSerializer().Deserialize(Of Object)(stringResult)
        gameID = jsonResult("results")(0)("id")
        System.Console.WriteLine(gameID)
        myReq = HttpWebRequest.Create("https://api.rawg.io/api/games/" + gameID.ToString() + "?key=" + My.Settings.RAWGToken + "&search=" + g.getName())
        myReq.Method = "GET"
        myResp = myReq.GetResponse()
        myReader = New System.IO.StreamReader(myResp.GetResponseStream())
        stringResult = myReader.ReadToEnd()
        System.Console.WriteLine(stringResult)
        jsonResult = New JavaScriptSerializer().Deserialize(Of Object)(stringResult)
        Dim descriptionText = jsonResult("description_raw")
        Dim genre = jsonResult("genres")(0)("name")
        Dim name = jsonResult("name")
        Dim developer = jsonResult("developers")(0)("name")
        Dim image = jsonResult("background_image")
        Return New Game("./" + g.getName(), name, image, genre, developer, descriptionText, g.getId())
    End Function
End Module
