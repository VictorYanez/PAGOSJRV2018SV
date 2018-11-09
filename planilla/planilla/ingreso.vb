Imports System.Data.OracleClient
Imports System.Data
Imports planilla.Usuario

Public Class ingreso
    REM Dim cadena As String = "Data Source=localhost:1521/XE; User id=PAGOSJRV; password=pagosjrv"
    Dim cadena As String = "Data Source=172.16.1.16:1521/RFID; User id=PAGOSJRV; password=pagosjrv"
    Dim con As New OracleConnection(cadena)

    Private Sub nusuario_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        con.Close()
        usuarios.Show()
        Me.Close()
    End Sub


    Private Sub btnaceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaceptar.Click


        Try
            ' Dim sql As String = "select * from usuario where nom_usuario=:nombre and password=:password" ' Esta es la consulta a la base
            'Dim comando As New OracleCommand(sql, con) ' SE crea un nuevo comando de oracle y se le asigna la consulta a la base (sql) y la conexion(con)
            Dim sql As String = "select * from usuario where nom_usuario=:NOMBRE and password=:password"
            Dim comando As New OracleCommand(sql, con) ' SE crea un nuevo comando de oracle y se le asigna la consulta a la base (sql) y la conexion(con)
            comando.Parameters.Add(":NOMBRE", txtusuario.Text.ToUpper)
            comando.Parameters.Add(":password", txtclave.Text.ToUpper)


            con.Open()
            Dim lector As OracleDataReader = Nothing

            lector = comando.ExecuteReader
            If lector.HasRows Then
                If lector.Read() Then
                    Dim a As Integer = Convert.ToInt32(lector(0).ToString)
                    Dim usu As String = lector(1).ToString()
                    codUsuario = a
                    nomUsuario = usu
                End If
                'MessageBox.Show("TODO BIEN")
                ' MessageBox.Show("Ha ingresado al sistema")
                con.Close()

                menuprin.Show()
                Me.Close()
            Else
                MessageBox.Show("CONTRASEÑA O USUARIO INCORRECTO")
                txtusuario.Text = ""
                txtclave.Text = ""
                txtusuario.Focus()
                con.Close()


            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString) ' Se imprime el error por si falla la conexion a la base
            con.Close()

        End Try

    End Sub

    Private Sub btnsalida_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsalida.Click
        Me.Close()
    End Sub

    Private Sub ingreso_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub txtusuario_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtusuario.KeyDown
        If e.KeyData = Keys.Enter Then
            txtclave.Focus()
        End If
    End Sub

    Private Sub txtclave_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtclave.KeyDown
        If e.KeyData = Keys.Enter Then

            Try
                ' Dim sql As String = "select * from usuario where nom_usuario=:nombre and password=:password" ' Esta es la consulta a la base
                'Dim comando As New OracleCommand(sql, con) ' SE crea un nuevo comando de oracle y se le asigna la consulta a la base (sql) y la conexion(con)
                Dim sql As String = "select * from usuario where nom_usuario=:NOMBRE and password=:password"
                Dim comando As New OracleCommand(sql, con) ' SE crea un nuevo comando de oracle y se le asigna la consulta a la base (sql) y la conexion(con)
                comando.Parameters.Add(":NOMBRE", txtusuario.Text.ToUpper)
                comando.Parameters.Add(":password", txtclave.Text.ToUpper)


                con.Open()
                Dim lector As OracleDataReader = Nothing

                lector = comando.ExecuteReader
                If lector.HasRows Then
                    If lector.Read() Then
                        Dim a As Integer = Convert.ToInt32(lector(0).ToString)
                        Dim usu As String = lector(1).ToString()
                        codUsuario = a
                        nomUsuario = usu
                    End If
                    'MessageBox.Show("TODO BIEN")
                    ' MessageBox.Show("Ha ingresado al sistema")
                    con.Close()


                    menuprin.Show()
                    Me.Close()
                Else
                    MessageBox.Show("CONTRASEÑA O USUARIO INCORRECTO")
                    txtusuario.Text = ""
                    txtclave.Text = ""
                    txtusuario.Focus()
                    con.Close()


                End If

            Catch ex As Exception
                MessageBox.Show(ex.ToString) ' Se imprime el error por si falla la conexion a la base
                con.Close()

            End Try
        End If
    End Sub
End Class
