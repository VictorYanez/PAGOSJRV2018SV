Imports System.Data.OracleClient
Imports System.Data

Public Class usuarios

    Dim cadena As String = " Data Source=orcl; User id=PAGOSJRV; password= Arkantos14"
    Dim con As New OracleConnection(cadena)

   
    Private Sub btningresar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btningresar.Click

        Dim sql As String = ("INSERT INTO usuario (cod_usuario, nom_usuario, password) values (SEQ_INC_CODUSUARIO.nextval, :nombre, :password)")
        Dim comando As New OracleCommand(sql, con) ' SE crea un nuevo comando de oracle y se le asigna la consulta a la base (sql) y la conexion(con)

        con.Open()


        For Each control As Control In Me.Controls
            If TypeOf control Is TextBox Then
                If control.Text = "" Then
                    MsgBox("Llene todos los cuadros de texto")
                    con.Close()
                    Exit Sub
                End If
            End If
            txtusuario.Focus()

        Next

        If txtclave.Text <> txtconfirma.Text Then
            MsgBox("clave o confirmacion de clave invalida")
            txtusuario.Text = ""
            txtclave.Text = ""
            txtconfirma.Text = ""
            txtusuario.Focus()
            con.Close()
            Exit Sub
        End If

        Try
            comando.Parameters.Clear()
            ' comando.Parameters.Add("codigo", a)
            comando.Parameters.Add("nombre", txtusuario.Text)
            comando.Parameters.Add("password", txtclave.Text)


            comando.ExecuteNonQuery()
            txtusuario.Text = ""
            txtclave.Text = ""
            txtconfirma.Text = ""
            txtusuario.Focus()
            MsgBox("Usuario y Clave Ingresada con éxito")
            'Dim lector As OracleDataReader = Nothing
            con.Close()
            'lector = comando.ExecuteReader

        Catch ex As Exception
            MessageBox.Show(ex.ToString) ' Se imprime el error por si falla la conexion a la base
            con.Close()

        End Try



    End Sub

    Private Sub btnsalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsalir.Click
        ingreso.Show()
        Me.Close()

    End Sub
End Class