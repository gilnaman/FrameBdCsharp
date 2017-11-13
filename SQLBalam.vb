Imports MySql.Data.MySqlClient
Module SQLBalam
    'Public srv As String = 'My.Settings.Servidor    '"192.168.26.250" 'localhost
    Public conex As New MySqlConnection
    Public Sub conectar()
        Try
            conex.Close()
            conex.ConnectionString = "server=localhost;port=3309" & ";user=root" & ";password=admin" & ";database=seutc"
            conex.Open()
            'MsgBox("conexion exitosa")
        Catch ex As MySqlException
            MsgBox(ex.Message & ex.Number)
            'If ex.Number = 1042 Then
            MsgBox("No se puede conectar con el servidor" & vbCrLf & "Actualice los datos")
            'FrmDatosServidor.Show()
            'FrmLogin.Close()
            'End If
        End Try

    End Sub

    Public Sub SQLIDU(ByVal Sql As String)
        'Try
        conectar()
        Dim comando As New MySqlCommand(Sql, conex)
        comando.ExecuteNonQuery()
        conex.Close()
        'Catch ex As MySqlException




        'Select Case ex.Number
        '  Case 1062
        'MsgBox("El elemento que pretende crear ya existe", vbInformation, "ATENCIÓN")
        '   Case Else
        'MsgBox(ex.Message & " " & ex.Number)
        'End Select

        'End Try


    End Sub


    Public Sub DBInsert(ByVal Tabla As String, ByVal campval() As String)
        Try
            'conectar()
            Dim strinsert As String = "INSERT INTO " & Tabla & "("
            Dim contCampos As Integer = 0

            'Rutina para obtener los campos del vector enviado
            Dim campos As String = ""
            For i As Integer = 0 To (campval.Length - 1) Step 2
                campos = campos & campval(i) & ","
                contCampos += 1
            Next
            'Elimina la ultima coma
            campos = Left(campos, campos.Length - 1)


            'Rutina para obtener los valores de campo del vector enviado
            Dim valores As String = ""
            Dim ContValores As Integer = 0

            For i As Integer = 1 To (campval.Length - 1) Step 2
                valores = valores & campval(i) & ","
                ContValores += 1
            Next
            'Elimina la ultima coma
            valores = Left(valores, valores.Length - 1)


            strinsert = strinsert & campos & ") VALUES("
            strinsert = strinsert & valores & ")"

            If contCampos = ContValores Then
                MsgBox(strinsert)
            Else
                MsgBox("El numero de campos no coincide con el número de valores")

            End If



        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Public Function SQLSEL(ByVal sql As String) As DataSet
        Try
            conectar()

            Dim Adaptador As New MySqlDataAdapter(sql, conex)
            Dim RsDatos As New DataSet
            Adaptador.Fill(RsDatos, "datos")
            Return (RsDatos)
            conex.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Public Function SQLCOMBO(ByVal sql As String) As DataTable

        Try
            conectar()
            Dim Adap As New MySqlDataAdapter(sql, conex)
            Dim DT As New DataTable
            Adap.Fill(DT)
            Return DT
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Public Function SQLREADER(ByVal sql) As MySqlDataReader
        Try
            conectar()
            Dim comando As New MySqlCommand(sql, conex)
            Dim Dr As MySqlDataReader
            Dr = comando.ExecuteReader()
            Return Dr
            conex.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function



    Public Function Foliar(ByVal Tabla As String, ByVal Prefijo As String) As String

        Dim ult As Integer = 0

        ult = ObtenerUltimo(Tabla)

        Foliar = Prefijo & "" & Now.Year & "-" & ult + 1


    End Function

    'Esta funcion obtiene el ultimo folio que se haya almacenado en una tabla determinada
    'ENTRADA: Necesita el nombre de la tabla de donde obtendra el ultimo consecutivo
    'SALIDA: Devuelve el ultimo consecutivo que se insertó en la tabla

    Public Function ObtenerUltimo(ByVal Tabla As String) As Integer

        Dim strInstruccion As String = "SELECT MAX(consec) from " & Tabla
        Dim Ultimo As String = ""
        Dim AuxUltimo As Integer = 0
        conectar()
        Dim comando As New MySqlCommand(strInstruccion, conex)
        Ultimo = comando.ExecuteScalar.ToString

        If Ultimo <> "" Then
            AuxUltimo = Val(Ultimo)
        End If

        ObtenerUltimo = AuxUltimo
        conex.Close()



    End Function

    Public Function ObtenerDato(ByVal Tabla As String, condicion As String, ByVal CamposALeer() As String) As Array
        Dim dr As MySqlDataReader
        Dim nombre As String = ""
        Dim campos As String = ""
        Dim res() As String
        For i As Integer = 0 To CamposALeer.Length - 1
            campos = campos & CamposALeer(i)
            If i <> CamposALeer.Length - 1 Then
                campos = campos & ","
            End If
        Next

        Dim sql As String = "SELECT " & campos & _
              " FROM " & Tabla & _
              " WHERE " & condicion


        dr = SQLREADER(sql)

        ReDim res(CamposALeer.Length - 1)

        While dr.Read
            For i As Integer = 0 To res.Length - 1
                res(i) = dr(i)
            Next

        End While
        Return res

    End Function



    Public Function ObtenerColumna(ByVal Tabla As String, condicion As String, ByVal CamposALeer() As String) As Array
        Dim dr As MySqlDataReader
        Dim nombre As String = ""
        Dim campos As String = ""
        Dim res() As String
        For i As Integer = 0 To CamposALeer.Length - 1
            campos = campos & CamposALeer(i)
            If i <> CamposALeer.Length - 1 Then
                campos = campos & ","
            End If
        Next

        Dim sql As String = "SELECT " & campos & _
              " FROM " & Tabla & _
              " WHERE " & condicion


        dr = SQLREADER(sql)

        'ReDim res(CamposALeer.Length - 1)
        Dim registros As Integer = -1
        While dr.Read
            registros += 1
            ReDim Preserve res(registros)

            res(registros) = dr(0)
        End While
        Return res

    End Function


    Public Function ObtenerDato(sql As String) As String
        Dim dr As MySqlDataReader
        Dim res As String

        dr = SQLREADER(sql)

        While dr.Read

            'For i As Integer = 0 To res.Length - 1
            res = dr(0)
            ' Next
        End While
        Return res

    End Function




End Module
