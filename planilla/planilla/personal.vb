Imports System.Data.OracleClient
Imports System.Data
Imports planilla.Usuario
Imports planilla.TablaTemp
Imports System.Text
Imports System.IO


Public Class personal
    REM Dim cadena As String = "Data Source=localhost:1521/XE; User id=PAGOSJRV; password=pagosjrv"
    Dim cadena As String = "Data Source=172.16.1.16:1521/RFID; User id=PAGOSJRV; password=pagosjrv"
    Dim con As New OracleConnection(cadena)
    Dim duiproc As String
    Dim duipadron As String
    Dim temporal As TablaTemp() = (New TablaTemp(15) {}).Select(Function(element) New TablaTemp).ToArray()
    Dim contador As Integer
    Dim dt As New DataTable
    Dim countDUI As Integer = 0
    Dim eval As Boolean = False
    Dim nextval As Integer = Nothing ' HELPER PARA LLEVAR LA SECUENCIA
    Dim dtusuario As New DataTable



    Private Sub btnsalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsalir.Click
        If btnsalir.Text = "SALIR" Then
            btnsalir.Text = "DIGITE LA ULTIMA ACTA PARA SALIR"
            btnsalir.Hide()
            MENSAJESALIR.Show()
            MENSAJESALIR.Text = "DIGITE LA ULTIMA ACTA PARA SALIR"
        Else
            btnsalir.Text = "SALIR"
        End If

    End Sub

    Private Sub personal_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'If Convert.ToInt32(txtjrv.Text) <> 9422 Then
        '    MessageBox.Show("No puede salirse hasta que haya terminado la digitación", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    e.Cancel = True
        'End If
        'Proceso de cierre modificado 09/03/18
        If btnsalir.Text <> "DIGITE LA ULTIMA ACTA PARA SALIR" Then
            e.Cancel = True
            Exit Sub
        Else
            Dim a As Integer = MessageBox.Show("¿Ya digitó la última acta?", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            '    If a = vbNo Then
            '        e.Cancel = True
            '        Exit Sub
            '    End If
            'End If
            'menuprin.Show()
            '********* CREO QUE HACE LO MISMO SI SE APRETA SI o NO ASI QUE MODIFICARE EL ELSE
            If a = vbNo Then
                'e.Cancel = True
                'Exit Sub
            Else
                menuprin.Show()
            End If
        End If
    End Sub


    Private Sub personal_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtjrv.Enabled = False
        Button7.Text = "Cambiar Imagen Asamblea"
        MENSAJESALIR.Hide()
        txtnombre1.ReadOnly = True
        'txtnombre2.ReadOnly = True
        txtape1.ReadOnly = True
        'txtape2.ReadOnly = True
        contador = 0
        cbProcedencia.Enabled = False
        btningresar.Enabled = False


        dt.Columns.Add("DUI")
        dt.Columns.Add("Nombre1")
        'dt.Columns.Add("Nombre2")
        dt.Columns.Add("Apellido1")
        'dt.Columns.Add("Apellido2")
        dt.Columns.Add("JRV")
        dt.Columns.Add("Cod_Usuario")
        dt.Columns.Add("Cargo")
        dt.Columns.Add("Estado")
        dt.Columns.Add("Imagen")
        dt.Columns.Add("DuiPadron")
        dt.Columns.Add("Procedencia")


        'Tabla secundaria para mostrar datos en datagrid al usuario
        dtusuario.Columns.Add("DUI")
        dtusuario.Columns.Add("Nombre1")
        dtusuario.Columns.Add("Apellido1")
        dtusuario.Columns.Add("JRV")
        dtusuario.Columns.Add("Cargo")
        dtusuario.Columns.Add("Procedencia")




        '***********************************************************************************
        '************** METODO PARA CARGAR LA IMAGEN AL CARGAR EL FORMULARIO****************
        '***********************************************************************************
        '******************************************************************************************
        '****************************** METODO PARA CARGAR LA SIGUIENTE IMAGEN ********************
        '******************************************************************************************
        ' Se evalua al cargar el formulario cuales actas ya fueron digitadas por el mismo usuario para evitar repeticiones 09/03/18
        'Falta reiniciar la secuencia en base al estado 0 o 1 del estado de la imagen
        'Dim buscarRepetido As String = "select * from PERSONAL ORDER BY JRV ASC"
        'Dim comandoRepetido As New OracleCommand(buscarRepetido, con) ' SE crea un nuevo comando de oracle y se le asigna la consulta a la base (sql) y la conexion(con)
        'Dim repetido As Integer = nextval
        'Dim norepetida As Integer = 1
        'con.Open()
        'Dim lector As OracleDataReader = Nothing

        'lector = comandoRepetido.ExecuteReader
        'If lector.HasRows Then
        '    While lector.Read()
        '        norepetida = Convert.ToInt32(lector(5).ToString)
        '        If norepetida >= repetido Then
        '            If Convert.ToInt32(lector(7).ToString) <> codUsuario Then
        '                Exit While
        '            End If
        '        End If
        '    End While
        'End If
        'con.Close()
        'If nextval <> norepetida Then
        '    Do Until nextval = norepetida
        '        siguiente_acta() ' Primero se pide el valor de la siguiente
        '        txtjrv.Text = nextval
        '        mostrar_asamblea()
        '    Loop
        'End If


        siguiente_acta() ' Primero se pide el valor de la siguiente
        txtjrv.Text = nextval
        mostrarAlcaldes()
        '***********************************************************************************

        btnsalir.Text = "SALIR"
        cargar_combobox_procedencia()
        
    End Sub
    Private Sub cargar_combobox_procedencia()
        Try
            con.Close()
            Dim SQL As String = "select * from origen"
            Dim comando As New OracleCommand(SQL, con)
            Dim lector As OracleDataReader = Nothing
            con.Open()
            lector = comando.ExecuteReader
            If lector.HasRows Then
                Dim da As New OracleDataAdapter(comando)
                Dim ds As New DataSet
                da.Fill(ds)
                cbProcedencia.DisplayMember = "PROCEDENCIA"
                cbProcedencia.ValueMember = "ID_PROCEDE"
                cbProcedencia.DataSource = ds.Tables(0)
                con.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            con.Close()
        End Try
    End Sub

    Private Sub btnbuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbuscar.Click
        con.Close() ' SE DEBE DE HABILITAR EL BOTON INGRESAR Y EL INGRESAR SE DEBE DE DEHABILITAR CUANDO TERMINE SU ´ROPIA RUTINA
        Try

            Dim buscar As String = "select * from padronel where DUI = :dui"
            Dim comando As New OracleCommand(buscar, con) ' SE crea un nuevo comando de oracle y se le asigna la consulta a la base (sql) y la conexion(con)
            comando.Parameters.Add("dui", txtdui.Text)


            con.Open()
            Dim lector As OracleDataReader = Nothing

            lector = comando.ExecuteReader
            If lector.HasRows Then
                If lector.Read() Then
                    duipadron = lector(0).ToString
                    txtnombre1.Text = lector(1).ToString()
                    'txtnombre2.Text = lector(5).ToString()
                    txtape1.Text = lector(2).ToString()
                    'txtape2.Text = lector(3).ToString()
                End If
            Else
                If MessageBox.Show("No se encontró esta persona en el padrón, ¿Desea digitar de nuevo?, de lo contrario se deberá ingresar manualmente.", "No encontrado", MessageBoxButtons.YesNo) = DialogResult.No Then
                    txtnombre1.ReadOnly = False
                    'txtnombre2.ReadOnly = False
                    txtape1.ReadOnly = False
                    'txtape2.ReadOnly = False
                    txtnombre1.Focus()
                End If
            End If
            con.Close()

            Dim duip As String = "select * from origen where ID_DUI = :dui" 'Saca el DUI de la tabla procedencia
            Dim comando1 As New OracleCommand(duip, con) ' SE crea un nuevo comando de oracle y se le asigna la consulta a la base (sql) y la conexion(con)
            comando.Parameters.Add("dui", txtdui.Text)


            con.Open()
            Dim lector1 As OracleDataReader = Nothing

            lector1 = comando.ExecuteReader
            If lector1.HasRows Then
                If lector1.Read() Then
                    duiproc = lector1(0).ToString
                End If
            End If
            con.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error")
            con.Close()
        End Try
        btningresar.Enabled = True
        'con.Close()
    End Sub
    Dim rondaprincipio As Integer
    Private Sub btningresar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btningresar.Click

        If txtdui.Text <> "" Then
            If txtnombre1.Text <> "" Then
                If txtape1.Text <> "" Then
                    '**************************
                    'CONSULTAR DUI EN TABLA PERSONAL
                    '*********************************
                    con.Close()
                    Try
                        Dim busquedaRonda As String = "select * from IMAGEN where ID_IMAGEN = :imagen"
                        Dim comandoRonda As New OracleCommand(busquedaRonda, con) ' SE crea un nuevo comando de oracle y se le asigna la consulta a la base (sql) y la conexion(con)
                        comandoRonda.Parameters.Add(":imagen", Convert.ToInt32(txtjrv.Text))
                        con.Open()
                        Dim lectorRonda As OracleDataReader = Nothing
                        lectorRonda = comandoRonda.ExecuteReader
                        If lectorRonda.HasRows Then
                            If lectorRonda.Read() Then
                                rondaprincipio = Convert.ToInt32(lectorRonda(3).ToString)
                            End If
                        End If
                        con.Close()
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                        con.Close()
                        Exit Sub
                    End Try
                    If rondaprincipio = 0 Then
                        con.Close()
                        Dim consulta_personal As String = "select * from personal where DUI=:DUI"
                        Dim comandoPersonal As New OracleCommand(consulta_personal, con)
                        comandoPersonal.Parameters.Add(":DUI", txtdui.Text)
                        Dim lector As OracleDataReader = Nothing
                        con.Open()
                        lector = comandoPersonal.ExecuteReader
                        If lector.HasRows Then
                            MessageBox.Show("EL DUI: " & txtdui.Text & " YA ESTA EN LA BASE DE DATOS, PORFAVOR RELLENELO DE CEROS COMO INCONSISTENCIA")
                            txtdui.Enabled = True
                            txtdui.Text = ""
                            txtdui.Focus()
                            Exit Sub
                            con.Close()
                        End If
                        con.Close()
                    Else
                        con.Close()
                        Dim consulta_personal As String = "select * from personal2 where DUI=:DUI"
                        Dim comandoPersonal As New OracleCommand(consulta_personal, con)
                        comandoPersonal.Parameters.Add(":DUI", txtdui.Text)
                        Dim lector As OracleDataReader = Nothing
                        con.Open()
                        lector = comandoPersonal.ExecuteReader
                        If lector.HasRows Then
                            MessageBox.Show("EL DUI: " & txtdui.Text & " YA ESTA EN LA BASE DE DATOS, PORFAVOR RELLENELO DE CEROS COMO INCONSISTENCIA")
                            txtdui.Enabled = True
                            txtdui.Text = ""
                            txtdui.Focus()
                            btnbuscar.Enabled = False
                            Exit Sub
                            con.Close()
                        End If
                        con.Close()
                    End If
                    '**************************************************************


                    'Dim sql As String = ("INSERT INTO personal (DUIDI, NOMBRE1, NOMBRE2, APELLIDO1, APELLIDO2, JRV, ID_CARGO, COD_USUARIO, ID_ESTADO, ID_IMAGEN, DUI, ID_PROCEDE) VALUES (:DUIDI, :NOMBRE1, :NOMBRE2, :APELLIDO1, :APELLIDO2, :JRV, :ID_CARGO, :COD_USUARIO, :ID_ESTADO, :ID_IMAGEN, :DUI, :ID_PROCEDE)")
                    'Dim comando As New OracleCommand(sql, con) ' SE crea un nuevo comando de oracle y se le asigna la consulta a la base (sql) y la conexion(con)
                    Dim cargo As Boolean = False
                    Dim cargousuario As String
                    Dim duiprocedencia As String ' para guardar la letra asignada a cada partido
                    If cmbcargo.Text <> "" Then

                        If cmbcargo.SelectedItem.ToString = "Vigilante" Then
                            '*******************************************
                            'If cbProcedencia.SelectedItem = Nothing Then
                            '    MessageBox.Show("SELECCIONE LA PROCEDENCIA DE LA PERSONA")
                            '    Exit Sub
                            'Else

                            '    '*******************************************
                            '    Dim buscar As String = "select * from ORIGEN where ID_PROCEDE=:PARTIDO"
                            '    Dim comandobusqueda As New OracleCommand(buscar, con) ' SE crea un nuevo comando de oracle y se le asigna la consulta a la base (sql) y la conexion(con)
                            '    comandobusqueda.Parameters.Add(":partido", cbProcedencia.SelectedValue.ToString)
                            '    con.Open()
                            '    Dim lector As OracleDataReader = Nothing

                            '    lector = comandobusqueda.ExecuteReader
                            '    If lector.HasRows Then
                            '        If lector.Read() Then
                            '            duiprocedencia = lector(0).ToString
                            '        End If
                            '    End If
                            'If cbProcedencia.SelectedValue.ToString = "" Then
                            'MessageBox.Show("SELECCIONE LA PROCEDENCIA DE LA PERSONA")
                            'Exit Sub
                            'Else

                            duiprocedencia = cbProcedencia.SelectedValue.ToString
                            cargo = True

                            con.Close()

                            If duiprocedencia <> "" Then
                                For Each row As DataRow In dt.Rows
                                    If duiprocedencia = row(9).ToString Then
                                        MessageBox.Show("Ya existe una persona procedente del partido " & cbProcedencia.SelectedItem.ToString & ", por favor revisar los datos ingresados nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                        Exit Sub
                                    End If
                                Next
                            End If
                            '*****************************************************************
                            'If PARA COMPROBAR QUE EL EL COMBOBOX DE CARGO NO ESTE VACIO 
                            'End If
                            ' End If


                        End If


                        For Each row As DataRow In dt.Rows
                            If txtdui.Text <> "00000000-0" Then
                                If txtdui.Text = row(0).ToString And codUsuario = row(4).ToString Then
                                    MessageBox.Show("El DUI ingresado ya fue digitado anteriormente, íntente con otro documento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                    Exit Sub
                                End If
                            End If
                        Next

                        Dim contarVocal = 0
                        For Each row As DataRow In dt.Rows

                            Select Case cmbcargo.SelectedItem.ToString
                                Case "Presidente"
                                    If Convert.ToInt32(row(5).ToString) = 1 Then
                                        MessageBox.Show("Esta JRV ya tiene otra persona seleccionada como " & cmbcargo.SelectedItem.ToString & ", por favor seleccione el cargo correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                        Exit Sub
                                    End If
                                Case "Secretario"
                                    If Convert.ToInt32(row(5).ToString) = 2 Then
                                        MessageBox.Show("Esta JRV ya tiene otra persona seleccionada como " & cmbcargo.SelectedItem.ToString & ", por favor seleccione el cargo correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                        Exit Sub
                                    End If
                                Case "Vocal1"
                                    If Convert.ToInt32(row(5).ToString) = 3 Then
                                        contarVocal += 1
                                        If contarVocal = 3 Then
                                            MessageBox.Show("Esta JRV ya tiene otra persona seleccionada como " & cmbcargo.SelectedItem.ToString & ", por favor seleccione el cargo correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                            Exit Sub
                                        End If
                                    End If
                                Case "Vocal2"
                                    If Convert.ToInt32(row(5).ToString) = 3 Then
                                        contarVocal += 1
                                        If contarVocal = 3 Then
                                            MessageBox.Show("Esta JRV ya tiene otra persona seleccionada como " & cmbcargo.SelectedItem.ToString & ", por favor seleccione el cargo correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                            Exit Sub
                                        End If
                                    End If
                                Case "Vocal3"
                                    If Convert.ToInt32(row(5).ToString) = 3 Then
                                        contarVocal += 1
                                        If contarVocal = 3 Then
                                            MessageBox.Show("Esta JRV ya tiene otra persona seleccionada como " & cmbcargo.SelectedItem.ToString & ", por favor seleccione el cargo correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                            Exit Sub
                                        End If
                                    End If
                            End Select

                        Next

                        cargousuario = cmbcargo.SelectedItem.ToString 'Se carga en la variable el cargo seleccionado en el combobox para mostrarse en el datagrid

                        cbProcedencia.Enabled = True
                        eval = True



                        If txtnombre1.ReadOnly = True Then
                            Try
                                temporal(contador).Dui = txtdui.Text
                                temporal(contador).Nombre1 = txtnombre1.Text
                                'temporal(contador).Nombre2 = txtnombre2.Text
                                temporal(contador).Apellido1 = txtape1.Text
                                'temporal(contador).Apellido2 = txtape2.Text
                                temporal(contador).Jrv = Convert.ToInt32(txtjrv.Text)
                                Select Case cmbcargo.SelectedItem.ToString
                                    Case "Presidente"
                                        temporal(contador).Cargo = Convert.ToInt32(1)
                                    Case "Secretario"
                                        temporal(contador).Cargo = Convert.ToInt32(2)
                                    Case "Vocal1"
                                        temporal(contador).Cargo = Convert.ToInt32(3)
                                    Case "Vocal2"
                                        temporal(contador).Cargo = Convert.ToInt32(3)
                                    Case "Vocal3"
                                        temporal(contador).Cargo = Convert.ToInt32(3)
                                    Case "Vigilante"
                                        temporal(contador).Cargo = Convert.ToInt32(4)
                                    Case Else
                                        temporal(contador).Cargo = Convert.ToInt32(1)
                                End Select

                                temporal(contador).CodigoUsuario = codUsuario
                                temporal(contador).Estado = Convert.ToInt32(1)
                                temporal(contador).Imagen = Convert.ToInt32(1)
                                temporal(contador).DuiPadron = duipadron
                                If cargo = True Then
                                    temporal(contador).Procedencia = duiprocedencia
                                Else
                                    temporal(contador).Procedencia = ""
                                End If


                                'comando.ExecuteNonQuery()
                                'Dim lector As OracleDataReader = Nothing
                                con.Close()
                                'lector = comando.ExecuteReader
                                Dim jrvtexto = txtjrv.Text
                                For Each control As Control In Me.Controls
                                    If TypeOf control Is TextBox Then
                                        If control.Text <> "" Then
                                            control.Text = ""
                                        End If
                                    End If
                                Next
                                txtjrv.Text = jrvtexto
                                cmbcargo.SelectedIndex = -1
                                cbProcedencia.SelectedIndex = -1
                                ' MessageBox.Show("Datos ingresados con éxito", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information)

                            Catch ex As Exception
                                MessageBox.Show(ex.Message) ' Se imprime el error por si falla la conexion a la base
                                con.Close()

                            End Try
                        Else
                            Try
                                temporal(contador).Dui = txtdui.Text
                                temporal(contador).Nombre1 = txtnombre1.Text
                                'temporal(contador).Nombre2 = txtnombre2.Text
                                temporal(contador).Apellido1 = txtape1.Text
                                'temporal(contador).Apellido2 = txtape2.Text
                                temporal(contador).Jrv = Convert.ToInt32(txtjrv.Text)
                                Select Case cmbcargo.SelectedItem.ToString
                                    Case "Presidente"
                                        temporal(contador).Cargo = Convert.ToInt32(1)
                                    Case "Secretario"
                                        temporal(contador).Cargo = Convert.ToInt32(2)
                                    Case "Vocal1"
                                        temporal(contador).Cargo = Convert.ToInt32(3)
                                    Case "Vocal2"
                                        temporal(contador).Cargo = Convert.ToInt32(3)
                                    Case "Vocal3"
                                        temporal(contador).Cargo = Convert.ToInt32(3)
                                    Case "Vigilante"
                                        temporal(contador).Cargo = Convert.ToInt32(4)
                                    Case Else
                                        temporal(contador).Cargo = Convert.ToInt32(1)
                                End Select

                                temporal(contador).CodigoUsuario = codUsuario
                                temporal(contador).Estado = Convert.ToInt32(3)
                                temporal(contador).Imagen = Convert.ToInt32(1)
                                temporal(contador).DuiPadron = duipadron
                                If cargo = True Then
                                    temporal(contador).Procedencia = duiprocedencia
                                Else
                                    temporal(contador).Procedencia = ""
                                End If



                                'comando.ExecuteNonQuery()
                                'Dim lector As OracleDataReader = Nothing
                                con.Close()
                                'lector = comando.ExecuteReader
                                Dim jrvtexto = txtjrv.Text
                                For Each control As Control In Me.Controls
                                    If TypeOf control Is TextBox Then
                                        If control.Text <> "" Then
                                            control.Text = ""
                                        End If
                                    End If

                                Next
                                txtjrv.Text = jrvtexto

                                cmbcargo.SelectedIndex = -1
                                cbProcedencia.SelectedIndex = -1

                                '   MessageBox.Show("Datos ingresados con éxito", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                txtnombre1.ReadOnly = True
                                'txtnombre2.ReadOnly = True
                                txtape1.ReadOnly = True
                                'txtape2.ReadOnly = True


                            Catch ex As Exception
                                MessageBox.Show(ex.Message) ' Se imprime el error por si falla la conexion a la base
                                con.Close()

                            End Try
                        End If

                        Dim fila As DataRow = dt.NewRow()
                        fila("DUI") = temporal(contador).Dui
                        fila("Nombre1") = temporal(contador).Nombre1
                        'fila("Nombre2") = temporal(contador).Nombre2
                        fila("Apellido1") = temporal(contador).Apellido1
                        'fila("Apellido2") = temporal(contador).Apellido2
                        fila("JRV") = temporal(contador).Jrv
                        fila("Cod_Usuario") = temporal(contador).CodigoUsuario
                        fila("Cargo") = temporal(contador).Cargo
                        fila("Estado") = temporal(contador).Estado
                        fila("Imagen") = temporal(contador).Imagen
                        fila("DuiPadron") = temporal(contador).DuiPadron
                        fila("Procedencia") = temporal(contador).Procedencia
                        dt.Rows.Add(fila)

                        'dgvData.DataSource = dt
                        cargo = False
                        cbProcedencia.Enabled = False
                        txtdui.Focus()
                        eval = False
                        btningresar.Enabled = False

                        'Se mostrara en el datagrid solo la informacion que el digitador debe de ver
                        Dim filausuario As DataRow = dtusuario.NewRow()
                        filausuario("DUI") = temporal(contador).Dui
                        filausuario("Nombre1") = temporal(contador).Nombre1
                        filausuario("Apellido1") = temporal(contador).Apellido1
                        filausuario("JRV") = temporal(contador).Jrv
                        filausuario("Cargo") = cargousuario
                        filausuario("Procedencia") = temporal(contador).Procedencia
                        dtusuario.Rows.Add(filausuario)
                        dgvData.DataSource = dtusuario

                        contador += 1

                        '*****************************************************************
                        'If PARA COMPROBAR QUE EL EL COMBOBOX DE CARGO NO ESTE VACIO 
                    Else
                        MessageBox.Show("SELECCIONE EL CARGO DE LA PERSONA")
                    End If
                Else
                    MessageBox.Show("Rellene los campos necesarios antes de guardar")
                    Exit Sub
                End If
            Else
                MessageBox.Show("Rellene los campos necesarios antes de guardar")
                Exit Sub
            End If
        Else
            MessageBox.Show("Rellene los campos necesarios antes de guardar")
            Exit Sub
        End If
        ''**************************
        ''CONSULTAR DUI EN TABLA PERSONAL
        ''*********************************
        'con.Close()
        'Try
        '    Dim busquedaRonda As String = "select * from IMAGEN where ID_IMAGEN = :imagen"
        '    Dim comandoRonda As New OracleCommand(busquedaRonda, con) ' SE crea un nuevo comando de oracle y se le asigna la consulta a la base (sql) y la conexion(con)
        '    comandoRonda.Parameters.Add(":imagen", Convert.ToInt32(txtjrv.Text))
        '    con.Open()
        '    Dim lectorRonda As OracleDataReader = Nothing
        '    lectorRonda = comandoRonda.ExecuteReader
        '    If lectorRonda.HasRows Then
        '        If lectorRonda.Read() Then
        '            rondaprincipio = Convert.ToInt32(lectorRonda(3).ToString)
        '        End If
        '    End If
        '    con.Close()
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        '    con.Close()
        '    Exit Sub
        'End Try
        'If rondaprincipio = 0 Then
        '    con.Close()
        '    Dim consulta_personal As String = "select * from personal where DUI=:DUI"
        '    Dim comandoPersonal As New OracleCommand(consulta_personal, con)
        '    comandoPersonal.Parameters.Add(":DUI", txtdui.Text)
        '    Dim lector As OracleDataReader = Nothing
        '    con.Open()
        '    lector = comandoPersonal.ExecuteReader
        '    If lector.HasRows Then
        '        MessageBox.Show("EL DUI: " & txtdui.Text & " YA ESTA EN LA BASE DE DATOS, PORFAVOR RELLENELO DE CEROS COMO INCONSISTENCIA")
        '        txtdui.Enabled = True
        '        txtdui.Text = ""
        '        txtdui.Focus()
        '        Exit Sub
        '        con.Close()
        '    End If
        '    con.Close()
        'Else
        '    con.Close()
        '    Dim consulta_personal As String = "select * from personal2 where DUI=:DUI"
        '    Dim comandoPersonal As New OracleCommand(consulta_personal, con)
        '    comandoPersonal.Parameters.Add(":DUI", txtdui.Text)
        '    Dim lector As OracleDataReader = Nothing
        '    con.Open()
        '    lector = comandoPersonal.ExecuteReader
        '    If lector.HasRows Then
        '        MessageBox.Show("EL DUI: " & txtdui.Text & " YA ESTA EN LA BASE DE DATOS, PORFAVOR RELLENELO DE CEROS COMO INCONSISTENCIA")
        '        txtdui.Enabled = True
        '        txtdui.Text = ""
        '        txtdui.Focus()
        '        btnbuscar.Enabled = False
        '        Exit Sub
        '        con.Close()
        '    End If
        '    con.Close()
        'End If
        ''**************************************************************


        ''Dim sql As String = ("INSERT INTO personal (DUIDI, NOMBRE1, NOMBRE2, APELLIDO1, APELLIDO2, JRV, ID_CARGO, COD_USUARIO, ID_ESTADO, ID_IMAGEN, DUI, ID_PROCEDE) VALUES (:DUIDI, :NOMBRE1, :NOMBRE2, :APELLIDO1, :APELLIDO2, :JRV, :ID_CARGO, :COD_USUARIO, :ID_ESTADO, :ID_IMAGEN, :DUI, :ID_PROCEDE)")
        ''Dim comando As New OracleCommand(sql, con) ' SE crea un nuevo comando de oracle y se le asigna la consulta a la base (sql) y la conexion(con)
        'Dim cargo As Boolean = False
        'Dim cargousuario As String
        'Dim duiprocedencia As String ' para guardar la letra asignada a cada partido
        'If cmbcargo.Text <> "" Then

        '    If cmbcargo.SelectedItem.ToString = "Vigilante" Then
        '        '*******************************************
        '        'If cbProcedencia.SelectedItem = Nothing Then
        '        '    MessageBox.Show("SELECCIONE LA PROCEDENCIA DE LA PERSONA")
        '        '    Exit Sub
        '        'Else

        '        '    '*******************************************
        '        '    Dim buscar As String = "select * from ORIGEN where ID_PROCEDE=:PARTIDO"
        '        '    Dim comandobusqueda As New OracleCommand(buscar, con) ' SE crea un nuevo comando de oracle y se le asigna la consulta a la base (sql) y la conexion(con)
        '        '    comandobusqueda.Parameters.Add(":partido", cbProcedencia.SelectedValue.ToString)
        '        '    con.Open()
        '        '    Dim lector As OracleDataReader = Nothing

        '        '    lector = comandobusqueda.ExecuteReader
        '        '    If lector.HasRows Then
        '        '        If lector.Read() Then
        '        '            duiprocedencia = lector(0).ToString
        '        '        End If
        '        '    End If
        '        'If cbProcedencia.SelectedValue.ToString = "" Then
        '        'MessageBox.Show("SELECCIONE LA PROCEDENCIA DE LA PERSONA")
        '        'Exit Sub
        '        'Else

        '        duiprocedencia = cbProcedencia.SelectedValue.ToString
        '        cargo = True

        '        con.Close()

        '        If duiprocedencia <> "" Then
        '            For Each row As DataRow In dt.Rows
        '                If duiprocedencia = row(9).ToString Then
        '                    MessageBox.Show("Ya existe una persona procedente del partido " & cbProcedencia.SelectedItem.ToString & ", por favor revisar los datos ingresados nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '                    Exit Sub
        '                End If
        '            Next
        '        End If
        '        '*****************************************************************
        '        'If PARA COMPROBAR QUE EL EL COMBOBOX DE CARGO NO ESTE VACIO 
        '        'End If
        '        ' End If


        '    End If


        '    For Each row As DataRow In dt.Rows
        '        If txtdui.Text <> "00000000-0" Then
        '            If txtdui.Text = row(0).ToString And codUsuario = row(4).ToString Then
        '                MessageBox.Show("El DUI ingresado ya fue digitado anteriormente, íntente con otro documento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '                Exit Sub
        '            End If
        '        End If
        '    Next

        '    Dim contarVocal = 0
        '    For Each row As DataRow In dt.Rows

        '        Select Case cmbcargo.SelectedItem.ToString
        '            Case "Presidente"
        '                If Convert.ToInt32(row(5).ToString) = 1 Then
        '                    MessageBox.Show("Esta JRV ya tiene otra persona seleccionada como " & cmbcargo.SelectedItem.ToString & ", por favor seleccione el cargo correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '                    Exit Sub
        '                End If
        '            Case "Secretario"
        '                If Convert.ToInt32(row(5).ToString) = 2 Then
        '                    MessageBox.Show("Esta JRV ya tiene otra persona seleccionada como " & cmbcargo.SelectedItem.ToString & ", por favor seleccione el cargo correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '                    Exit Sub
        '                End If
        '            Case "Vocal1"
        '                If Convert.ToInt32(row(5).ToString) = 3 Then
        '                    contarVocal += 1
        '                    If contarVocal = 3 Then
        '                        MessageBox.Show("Esta JRV ya tiene otra persona seleccionada como " & cmbcargo.SelectedItem.ToString & ", por favor seleccione el cargo correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '                        Exit Sub
        '                    End If
        '                End If
        '            Case "Vocal2"
        '                If Convert.ToInt32(row(5).ToString) = 3 Then
        '                    contarVocal += 1
        '                    If contarVocal = 3 Then
        '                        MessageBox.Show("Esta JRV ya tiene otra persona seleccionada como " & cmbcargo.SelectedItem.ToString & ", por favor seleccione el cargo correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '                        Exit Sub
        '                    End If
        '                End If
        '            Case "Vocal3"
        '                If Convert.ToInt32(row(5).ToString) = 3 Then
        '                    contarVocal += 1
        '                    If contarVocal = 3 Then
        '                        MessageBox.Show("Esta JRV ya tiene otra persona seleccionada como " & cmbcargo.SelectedItem.ToString & ", por favor seleccione el cargo correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '                        Exit Sub
        '                    End If
        '                End If
        '        End Select

        '    Next

        '    cargousuario = cmbcargo.SelectedItem.ToString 'Se carga en la variable el cargo seleccionado en el combobox para mostrarse en el datagrid

        '    cbProcedencia.Enabled = True
        '    eval = True



        '    If txtnombre1.ReadOnly = True Then
        '        Try
        '            temporal(contador).Dui = txtdui.Text
        '            temporal(contador).Nombre1 = txtnombre1.Text
        '            'temporal(contador).Nombre2 = txtnombre2.Text
        '            temporal(contador).Apellido1 = txtape1.Text
        '            'temporal(contador).Apellido2 = txtape2.Text
        '            temporal(contador).Jrv = Convert.ToInt32(txtjrv.Text)
        '            Select Case cmbcargo.SelectedItem.ToString
        '                Case "Presidente"
        '                    temporal(contador).Cargo = Convert.ToInt32(1)
        '                Case "Secretario"
        '                    temporal(contador).Cargo = Convert.ToInt32(2)
        '                Case "Vocal1"
        '                    temporal(contador).Cargo = Convert.ToInt32(3)
        '                Case "Vocal2"
        '                    temporal(contador).Cargo = Convert.ToInt32(3)
        '                Case "Vocal3"
        '                    temporal(contador).Cargo = Convert.ToInt32(3)
        '                Case "Vigilante"
        '                    temporal(contador).Cargo = Convert.ToInt32(4)
        '                Case Else
        '                    temporal(contador).Cargo = Convert.ToInt32(1)
        '            End Select

        '            temporal(contador).CodigoUsuario = codUsuario
        '            temporal(contador).Estado = Convert.ToInt32(1)
        '            temporal(contador).Imagen = Convert.ToInt32(1)
        '            temporal(contador).DuiPadron = duipadron
        '            If cargo = True Then
        '                temporal(contador).Procedencia = duiprocedencia
        '            Else
        '                temporal(contador).Procedencia = ""
        '            End If


        '            'comando.ExecuteNonQuery()
        '            'Dim lector As OracleDataReader = Nothing
        '            con.Close()
        '            'lector = comando.ExecuteReader
        '            Dim jrvtexto = txtjrv.Text
        '            For Each control As Control In Me.Controls
        '                If TypeOf control Is TextBox Then
        '                    If control.Text <> "" Then
        '                        control.Text = ""
        '                    End If
        '                End If
        '            Next
        '            txtjrv.Text = jrvtexto
        '            cmbcargo.SelectedIndex = -1
        '            cbProcedencia.SelectedIndex = -1
        '            ' MessageBox.Show("Datos ingresados con éxito", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information)

        '        Catch ex As Exception
        '            MessageBox.Show(ex.Message) ' Se imprime el error por si falla la conexion a la base
        '            con.Close()

        '        End Try
        '    Else
        '        Try
        '            temporal(contador).Dui = txtdui.Text
        '            temporal(contador).Nombre1 = txtnombre1.Text
        '            'temporal(contador).Nombre2 = txtnombre2.Text
        '            temporal(contador).Apellido1 = txtape1.Text
        '            'temporal(contador).Apellido2 = txtape2.Text
        '            temporal(contador).Jrv = Convert.ToInt32(txtjrv.Text)
        '            Select Case cmbcargo.SelectedItem.ToString
        '                Case "Presidente"
        '                    temporal(contador).Cargo = Convert.ToInt32(1)
        '                Case "Secretario"
        '                    temporal(contador).Cargo = Convert.ToInt32(2)
        '                Case "Vocal1"
        '                    temporal(contador).Cargo = Convert.ToInt32(3)
        '                Case "Vocal2"
        '                    temporal(contador).Cargo = Convert.ToInt32(3)
        '                Case "Vocal3"
        '                    temporal(contador).Cargo = Convert.ToInt32(3)
        '                Case "Vigilante"
        '                    temporal(contador).Cargo = Convert.ToInt32(4)
        '                Case Else
        '                    temporal(contador).Cargo = Convert.ToInt32(1)
        '            End Select

        '            temporal(contador).CodigoUsuario = codUsuario
        '            temporal(contador).Estado = Convert.ToInt32(3)
        '            temporal(contador).Imagen = Convert.ToInt32(1)
        '            temporal(contador).DuiPadron = duipadron
        '            If cargo = True Then
        '                temporal(contador).Procedencia = duiprocedencia
        '            Else
        '                temporal(contador).Procedencia = ""
        '            End If



        '            'comando.ExecuteNonQuery()
        '            'Dim lector As OracleDataReader = Nothing
        '            con.Close()
        '            'lector = comando.ExecuteReader
        '            Dim jrvtexto = txtjrv.Text
        '            For Each control As Control In Me.Controls
        '                If TypeOf control Is TextBox Then
        '                    If control.Text <> "" Then
        '                        control.Text = ""
        '                    End If
        '                End If

        '            Next
        '            txtjrv.Text = jrvtexto

        '            cmbcargo.SelectedIndex = -1
        '            cbProcedencia.SelectedIndex = -1

        '            '   MessageBox.Show("Datos ingresados con éxito", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '            txtnombre1.ReadOnly = True
        '            'txtnombre2.ReadOnly = True
        '            txtape1.ReadOnly = True
        '            'txtape2.ReadOnly = True


        '        Catch ex As Exception
        '            MessageBox.Show(ex.Message) ' Se imprime el error por si falla la conexion a la base
        '            con.Close()

        '        End Try
        '    End If

        '    Dim fila As DataRow = dt.NewRow()
        '    fila("DUI") = temporal(contador).Dui
        '    fila("Nombre1") = temporal(contador).Nombre1
        '    'fila("Nombre2") = temporal(contador).Nombre2
        '    fila("Apellido1") = temporal(contador).Apellido1
        '    'fila("Apellido2") = temporal(contador).Apellido2
        '    fila("JRV") = temporal(contador).Jrv
        '    fila("Cod_Usuario") = temporal(contador).CodigoUsuario
        '    fila("Cargo") = temporal(contador).Cargo
        '    fila("Estado") = temporal(contador).Estado
        '    fila("Imagen") = temporal(contador).Imagen
        '    fila("DuiPadron") = temporal(contador).DuiPadron
        '    fila("Procedencia") = temporal(contador).Procedencia
        '    dt.Rows.Add(fila)

        '    'dgvData.DataSource = dt
        '    cargo = False
        '    cbProcedencia.Enabled = False
        '    txtdui.Focus()
        '    eval = False
        '    btningresar.Enabled = False

        '    'Se mostrara en el datagrid solo la informacion que el digitador debe de ver
        '    Dim filausuario As DataRow = dtusuario.NewRow()
        '    filausuario("DUI") = temporal(contador).Dui
        '    filausuario("Nombre1") = temporal(contador).Nombre1
        '    filausuario("Apellido1") = temporal(contador).Apellido1
        '    filausuario("JRV") = temporal(contador).Jrv
        '    filausuario("Cargo") = cargousuario
        '    filausuario("Procedencia") = temporal(contador).Procedencia
        '    dtusuario.Rows.Add(filausuario)
        '    dgvData.DataSource = dtusuario

        '    contador += 1

        '    '*****************************************************************
        '    'If PARA COMPROBAR QUE EL EL COMBOBOX DE CARGO NO ESTE VACIO 
        'Else
        '    MessageBox.Show("SELECCIONE EL CARGO DE LA PERSONA")
        'End If

    End Sub

    'Private Sub btnJRV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Select Case btnJRV.Text
    '        Case "Fijar JRV"
    '            txtjrv.ReadOnly = True
    '            btnJRV.Text = "Cambiar JRV"
    '        Case "Cambiar JRV"
    '            txtjrv.ReadOnly = False
    '            btnJRV.Text = "Fijar JRV"
    '    End Select
    'End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'Using cmd As New OracleCommand With {.Connection = con, .CommandText = "SELECT DUIDI, NOMBRE1, NOMBRE2, APELLIDO1, APELLIDO2, JRV, ID_CARGO FROM PERSONAL WHERE JRV = :jrv"}
        '    con.Open()
        '    cmd.Parameters.Add(":JRV", Convert.ToInt32(txtjrv.Text))
        '    Dim dr As OracleDataReader = cmd.ExecuteReader
        '    Dim dt As New DataTable
        '    dt.Load(dr)
        '    dgvData.DataSource = dt
        '    con.Close()
        'End Using

    End Sub


    Private Sub btnSi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSi.Click
        'Dim da As New OracleDataAdapter
        Dim ronda As Integer = 0
        'If dt.Rows.Count < 2 Then
        '    MessageBox.Show("No se ha ingresado ningún registro en la JRV", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Sub
        'End If

        Try
            Dim busquedaRonda As String = "select * from IMAGEN where ID_IMAGEN = :imagen"
            Dim comandoRonda As New OracleCommand(busquedaRonda, con) ' SE crea un nuevo comando de oracle y se le asigna la consulta a la base (sql) y la conexion(con)
            comandoRonda.Parameters.Add(":imagen", Convert.ToInt32(txtjrv.Text))

            con.Open()
            Dim lectorRonda As OracleDataReader = Nothing
            lectorRonda = comandoRonda.ExecuteReader
            If lectorRonda.HasRows Then
                If lectorRonda.Read() Then
                    ronda = Convert.ToInt32(lectorRonda(3).ToString)
                End If
            End If
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            con.Close()
            Exit Sub
        End Try

        If ronda = 0 Then
            Dim sql As String = ("INSERT INTO personal (DUIDI, NOMBRE1, APELLIDO1, JRV, ID_CARGO, COD_USUARIO, ID_ESTADO, ID_IMAGEN, DUI, ID_PROCEDE) VALUES (:DUIDI, :NOMBRE1, :APELLIDO1, :JRV, :ID_CARGO, :COD_USUARIO, :ID_ESTADO, :ID_IMAGEN, :DUI, :ID_PROCEDE)")
            'Dim sql As String = ("INSERT INTO personal (DUIDI, NOMBRE1, APELLIDO1, JRV, ID_CARGO, COD_USUARIO, ID_ESTADO, ID_IMAGEN, DUI, ID_PROCEDE) VALUES (:DUIDI, :NOMBRE1, :APELLIDO1, :JRV, :ID_CARGO, :COD_USUARIO, :ID_ESTADO, :ID_IMAGEN, :DUI, :ID_PROCEDE)")
            Dim comando As New OracleCommand(sql, con) ' SE crea un nuevo comando de oracle y se le asigna la consulta a la base (sql) y la conexion(con)
            Dim countInconsistencia As Integer = 1

            Dim st As String
            Dim stt As String
            Try
                con.Open()
                For Each row As DataRow In dt.Rows
                    comando.Parameters.Clear()
                    If row(0).ToString = "00000000-0" Then
                        Dim buscar As String = "select * from PERSONAL where ID_ESTADO = :estado"
                        Dim comando1 As New OracleCommand(buscar, con) ' SE crea un nuevo comando de oracle y se le asigna la consulta a la base (sql) y la conexion(con)
                        comando1.Parameters.Add(":estado", 3)
                        Dim lector As OracleDataReader = Nothing
                        lector = comando1.ExecuteReader
                        If lector.HasRows Then
                            While lector.Read()
                                countInconsistencia += 1
                            End While
                        End If

                        Dim constructor As New StringBuilder
                        Dim numero As Integer = countInconsistencia.ToString.Length
                        'constructor.Append("9")
                        constructor.Append("8")
                        For i As Integer = 1 To (8 - numero)
                            constructor.Append("0")
                        Next
                        constructor.Append(countInconsistencia.ToString)
                        stt = constructor.ToString
                        st = stt.Substring(0, 8) + "-" + stt.Substring(8)
                        comando.Parameters.Add(":DUIDI", st)
                    Else
                        '*****************
                        ''***ARREGLO DE dui repetido
                        'Dim sqldui As String = "select * from personal where DUI=:DUI"
                        'Dim comandodui As New OracleCommand(sqldui, con)
                        'comandodui.Parameters.Add(":DUI", row(8).ToString)
                        'Dim lector As OracleDataReader = Nothing
                        'lector = comandodui.ExecuteReader
                        'If lector.HasRows Then
                        '    MessageBox.Show("El DUI:" & row(0).ToString & " Ya existe en la base de datos, Marcar como inconsistencia")
                        '    Exit Sub
                        'Else
                        '    comando.Parameters.Add(":DUIDI", row(0).ToString)
                        'End If
                        comando.Parameters.Add(":DUIDI", row(0).ToString)
                    End If
                    comando.Parameters.Add(":NOMBRE1", row(1).ToString)
                    comando.Parameters.Add(":APELLIDO1", row(2).ToString)
                    comando.Parameters.Add(":JRV", Convert.ToInt32(row(3).ToString))
                    comando.Parameters.Add(":COD_USUARIO", row(4).ToString)
                    comando.Parameters.Add(":ID_CARGO", Convert.ToInt32(row(5).ToString))
                    comando.Parameters.Add(":ID_ESTADO", Convert.ToInt32(row(6).ToString))
                    comando.Parameters.Add(":ID_IMAGEN", Convert.ToInt32(row(7).ToString))
                    comando.Parameters.Add(":DUI", row(8).ToString)
                    comando.Parameters.Add(":ID_PROCEDE", row(9).ToString)
                    comando.ExecuteNonQuery()
                    '***********************
                    'Linea para remover la ultima entrada
                    dgvData.Rows.RemoveAt(0)
                    countInconsistencia = 1
                Next
                'Array.Clear(temporal, 0, temporal.Length)
                contador = 0
                dt.Rows.Clear()
                dtusuario.Rows.Clear()
                dgvData.DataSource = dtusuario
                txtdui.Focus()
                con.Close()

                Dim actualizar As String = "UPDATE IMAGEN SET ESTADO = :estado where ID_IMAGEN = :imagen"
                Dim comandoup As New OracleCommand(actualizar, con) ' SE crea un nuevo comando de oracle y se le asigna la consulta a la base (sql) y la conexion(con)
                con.Open()
                comandoup.Parameters.Add(":estado", 1)
                comandoup.Parameters.Add(":imagen", nextval)
                comandoup.ExecuteNonQuery()
                con.Close()
                MessageBox.Show("Datos guardados con éxito", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                con.Close()
                Exit Sub
            End Try
            '******** aqui se debe de hscer el reseteo, porque debe de detectar la ultima jrv

            If btnsalir.Text = "DIGITE LA ULTIMA ACTA PARA SALIR" Then
                '    menuprin.Show()
                Me.Close()
                Me.Dispose()
                menuprin.Show()
            Else
                '******************************************************************************************
                '****************************** METODO PARA CARGAR LA SIGUIENTE IMAGEN ********************
                '******************************************************************************************
                siguiente_acta() ' Primero se pide el valor de la siguiente
                txtjrv.Text = nextval
                mostrar_asamblea()


                '******************************************************************************************
            End If
        Else
            Dim sql As String = ("INSERT INTO personal2 (DUIDI, NOMBRE1, APELLIDO1, JRV, ID_CARGO, COD_USUARIO, ID_ESTADO, ID_IMAGEN, DUI, ID_PROCEDE) VALUES (:DUIDI, :NOMBRE1, :APELLIDO1, :JRV, :ID_CARGO, :COD_USUARIO, :ID_ESTADO, :ID_IMAGEN, :DUI, :ID_PROCEDE)")
            'Dim sql As String = ("INSERT INTO personal (DUIDI, NOMBRE1, APELLIDO1, JRV, ID_CARGO, COD_USUARIO, ID_ESTADO, ID_IMAGEN, DUI, ID_PROCEDE) VALUES (:DUIDI, :NOMBRE1, :APELLIDO1, :JRV, :ID_CARGO, :COD_USUARIO, :ID_ESTADO, :ID_IMAGEN, :DUI, :ID_PROCEDE)")
            Dim comando As New OracleCommand(sql, con) ' SE crea un nuevo comando de oracle y se le asigna la consulta a la base (sql) y la conexion(con)
            Dim countInconsistencia As Integer = 1

            Dim st As String
            Dim stt As String
            Try
                con.Open()
                For Each row As DataRow In dt.Rows
                    comando.Parameters.Clear()
                    If row(0).ToString = "00000000-0" Then
                        Dim buscar As String = "select * from PERSONAL2 where ID_ESTADO = :estado"
                        Dim comando1 As New OracleCommand(buscar, con) ' SE crea un nuevo comando de oracle y se le asigna la consulta a la base (sql) y la conexion(con)
                        comando1.Parameters.Add(":estado", 3)


                        Dim lector As OracleDataReader = Nothing

                        lector = comando1.ExecuteReader
                        If lector.HasRows Then
                            While lector.Read()
                                countInconsistencia += 1
                            End While
                        End If

                        Dim constructor As New StringBuilder
                        Dim numero As Integer = countInconsistencia.ToString.Length
                        constructor.Append("9")
                        For i As Integer = 1 To (8 - numero)
                            constructor.Append("0")
                        Next
                        constructor.Append(countInconsistencia.ToString)
                        stt = constructor.ToString
                        st = stt.Substring(0, 8) + "-" + stt.Substring(8)
                        comando.Parameters.Add(":DUIDI", st)
                    Else
                        '*****************
                        '***ARREGLO DE dui repetido
                        'Dim sqldui As String = "select * from personal2 where duidi=:DUI"
                        'Dim comandodui As New OracleCommand(sqldui, con)
                        'comandodui.Parameters.Add(":DUI", row(1).ToString)
                        'Dim lector As OracleDataReader = Nothing
                        'lector = comandodui.ExecuteReader
                        'If lector.HasRows Then
                        '    MessageBox.Show("El DUI:" & row(0).ToString & " Ya existe en la base de datos, Marcar como inconsistencia")
                        '    Exit Sub
                        'Else
                        '    comando.Parameters.Add(":DUIDI", row(0).ToString)
                        'End If
                        comando.Parameters.Add(":DUIDI", row(0).ToString)
                    End If
                    comando.Parameters.Add(":NOMBRE1", row(1).ToString)
                    comando.Parameters.Add(":APELLIDO1", row(2).ToString)
                    comando.Parameters.Add(":JRV", Convert.ToInt32(row(3).ToString))
                    comando.Parameters.Add(":COD_USUARIO", row(4).ToString)
                    comando.Parameters.Add(":ID_CARGO", Convert.ToInt32(row(5).ToString))
                    comando.Parameters.Add(":ID_ESTADO", Convert.ToInt32(row(6).ToString))
                    comando.Parameters.Add(":ID_IMAGEN", Convert.ToInt32(row(7).ToString))
                    comando.Parameters.Add(":DUI", row(8).ToString)
                    comando.Parameters.Add(":ID_PROCEDE", row(9).ToString)
                    comando.ExecuteNonQuery()
                    countInconsistencia = 1
                Next
                'Array.Clear(temporal, 0, temporal.Length)
                contador = 0
                dt.Rows.Clear()
                dtusuario.Rows.Clear()
                dgvData.DataSource = dtusuario
                txtdui.Focus()
                con.Close()

                Dim actualizar As String = "UPDATE IMAGEN SET ESTADO = :estado where ID_IMAGEN = :imagen"
                Dim comandoup As New OracleCommand(actualizar, con) ' SE crea un nuevo comando de oracle y se le asigna la consulta a la base (sql) y la conexion(con)
                con.Open()
                comandoup.Parameters.Add(":estado", 2)
                comandoup.Parameters.Add(":imagen", nextval)
                comandoup.ExecuteNonQuery()
                con.Close()
                MessageBox.Show("Datos guardados con éxito", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                con.Close()
                Exit Sub
            End Try




            If btnsalir.Text = "DIGITE LA ULTIMA ACTA PARA SALIR" Then
                Me.Close()
                Me.Dispose()
                menuprin.Show()

            Else
                '******************************************************************************************
                '****************************** METODO PARA CARGAR LA SIGUIENTE IMAGEN ********************
                '******************************************************************************************
                siguiente_acta() ' Primero se pide el valor de la siguiente
                txtjrv.Text = nextval
                mostrar_asamblea()

                'Dim buscarRepetido As String = "select * from PERSONAL ORDER BY JRV ASC"
                'Dim comandoRepetido As New OracleCommand(buscarRepetido, con) ' SE crea un nuevo comando de oracle y se le asigna la consulta a la base (sql) y la conexion(con)
                'Dim repetido As Integer = nextval
                'Dim norepetida As Integer = 1
                'con.Open()
                'Dim lector As OracleDataReader = Nothing

                'lector = comandoRepetido.ExecuteReader
                'If lector.HasRows Then
                '    While lector.Read()
                '        norepetida = Convert.ToInt32(lector(5).ToString)
                '        If norepetida >= repetido Then
                '            If Convert.ToInt32(lector(7).ToString) <> codUsuario Then
                '                Exit While
                '            End If
                '        End If
                '    End While
                'End If
                'con.Close()
                'If nextval <> norepetida Then
                '    Do Until nextval = norepetida
                '        siguiente_acta() ' Primero se pide el valor de la siguiente
                '        txtjrv.Text = nextval
                '        mostrar_asamblea()
                '    Loop
                'End If
                '******************************************************************************************
                End If
        End If

        'Dim lector As OracleDataReader = Nothing

        '    Try
        '        comando.Parameters.Clear()
        '        comando.Parameters.Add(":DUIDI", txtdui.Text)
        '        comando.Parameters.Add(":NOMBRE1", txtnombre1.Text)
        '        comando.Parameters.Add(":NOMBRE2", txtnombre2.Text)
        '        comando.Parameters.Add(":APELLIDO1", txtape1.Text)
        '        comando.Parameters.Add(":APELLIDO2", txtape2.Text)
        '        comando.Parameters.Add(":JRV", Convert.ToInt32(txtjrv.Text))
        '        Select Case cmbcargo.SelectedItem.ToString
        '            Case "Presidente"
        '                comando.Parameters.Add(":ID_CARGO", Convert.ToInt32(1))
        '            Case "Secretario"
        '                comando.Parameters.Add(":ID_CARGO", Convert.ToInt32(2))
        '            Case "Vocal1"
        '                comando.Parameters.Add(":ID_CARGO", Convert.ToInt32(3))
        '            Case "Vocal2"
        '                comando.Parameters.Add(":ID_CARGO", Convert.ToInt32(3))
        '            Case "Vocal3"
        '                comando.Parameters.Add(":ID_CARGO", Convert.ToInt32(3))
        '            Case "Vigilante"
        '                comando.Parameters.Add(":ID_CARGO", Convert.ToInt32(4))
        '            Case Else
        '                comando.Parameters.Add(":ID_CARGO", Convert.ToInt32(1))
        '        End Select

        '        comando.Parameters.Add(":COD_USUARIO", codUsuario)
        '        If rbCorrecto.Checked = True Then
        '            comando.Parameters.Add(":ID_ESTADO", Convert.ToInt32(1))
        '        Else
        '            comando.Parameters.Add(":ID_ESTADO", Convert.ToInt32(2))
        '        End If
        '        comando.Parameters.Add(":ID_IMAGEN", Convert.ToInt32(1))
        '        comando.Parameters.Add(":DUI", duipadron)
        '        comando.Parameters.Add(":ID_PROCEDE", duiprocedencia)


        '        comando.ExecuteNonQuery()
        '        'Dim lector As OracleDataReader = Nothing
        '        con.Close()
        '        'lector = comando.ExecuteReader
        '        Dim jrvtexto = txtjrv.Text
        '        For Each control As Control In Me.Controls
        '            If TypeOf control Is TextBox Then
        '                If control.Text <> "" Then
        '                    control.Text = ""
        '                End If
        '            End If
        '        Next
        '        txtjrv.Text = jrvtexto
        '        cmbcargo.SelectedIndex = -1
        '        MessageBox.Show("Datos ingresados con éxito", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information)

        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message) ' Se imprime el error por si falla la conexion a la base
        '        con.Close()

        '    End Try



        'Using cmd As New OracleCommand With {.Connection = con, .CommandText = "SELECT DUIDI, NOMBRE1, NOMBRE2, APELLIDO1, APELLIDO2, JRV, ID_CARGO FROM PERSONAL WHERE JRV = :jrv"}
        '    con.Open()
        '    cmd.Parameters.Add(":JRV", Convert.ToInt32(txtjrv.Text))
        '    Dim dr As OracleDataReader = cmd.ExecuteReader
        '    Dim dt As New DataTable
        '    dt.Load(dr)
        '    dgvData.DataSource = dt

        '    con.Close()
        'End Using
    End Sub

    Private Sub txtdui_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtdui.KeyDown
        If e.KeyData = Keys.Enter Then
            ' SE DEBE DE HABILITAR EL BOTON INGRESAR Y EL INGRESAR SE DEBE DE DEHABILITAR CUANDO TERMINE SU ´ROPIA RUTINA
            Try

                Dim buscar As String = "select * from padronel where DUI = :dui"
                Dim comando As New OracleCommand(buscar, con) ' SE crea un nuevo comando de oracle y se le asigna la consulta a la base (sql) y la conexion(con)
                comando.Parameters.Add("dui", txtdui.Text)


                con.Open()
                Dim lector As OracleDataReader = Nothing

                lector = comando.ExecuteReader
                If lector.HasRows Then
                    If lector.Read() Then
                        duipadron = lector(0).ToString
                        txtnombre1.Text = lector(1).ToString()
                        'txtnombre2.Text = lector(5).ToString()
                        txtape1.Text = lector(2).ToString()
                        'txtape2.Text = lector(3).ToString()
                    End If
                Else
                    If MessageBox.Show("No se encontró esta persona en el padrón, ¿Desea digitar de nuevo?, de lo contrario se deberá ingresar manualmente.", "No encontrado", MessageBoxButtons.YesNo) = DialogResult.No Then
                        txtnombre1.ReadOnly = False
                        'txtnombre2.ReadOnly = False
                        txtape1.ReadOnly = False
                        'txtape2.ReadOnly = False
                        txtnombre1.Focus()
                    End If
                End If
                con.Close()

                Dim duip As String = "select * from origen where ID_DUI = :dui" 'Saca el DUI de la tabla procedencia
                Dim comando1 As New OracleCommand(duip, con) ' SE crea un nuevo comando de oracle y se le asigna la consulta a la base (sql) y la conexion(con)
                comando.Parameters.Add("dui", txtdui.Text)


                con.Open()
                Dim lector1 As OracleDataReader = Nothing

                lector1 = comando.ExecuteReader
                If lector1.HasRows Then
                    If lector1.Read() Then
                        duiproc = lector1(0).ToString
                    End If
                End If
                con.Close()

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error")
                con.Close()
            End Try
            btningresar.Enabled = True

        End If
    End Sub

    Private Sub txtdui_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtdui.KeyPress
        If Not IsNumeric(e.KeyChar) And e.KeyChar <> vbBack Then
            e.Handled = True
        End If


    End Sub


    Private Sub txtdui_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdui.TextChanged
        Dim texto As String
        Dim st As String
        Dim primera As New StringBuilder
        If txtdui.TextLength = 8 And countDUI = 1 Then
            countDUI = 0
        End If
        If txtdui.TextLength = 9 And countDUI = 0 Then
            texto = txtdui.Text
            primera.Append(texto.Substring(0, 8)).Append("-").Append(texto.Substring(8))
            st = primera.ToString
            txtdui.Text = st
            txtdui.SelectionStart = txtdui.TextLength
        End If
        If txtdui.TextLength = 10 Then
            countDUI = 1
        End If
    End Sub


    Private Sub cmbcargo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcargo.SelectedIndexChanged
        If eval = False Then
            If cmbcargo.SelectedItem.ToString = "Vigilante" Then
                cbProcedencia.Enabled = True
                cargar_combobox_procedencia()
            Else
                'cbProcedencia.SelectedItem = Nothing
                cbProcedencia.Enabled = False

            End If
        End If
    End Sub








    '**********************************************************************************************************
    '************************************METODOS PARA CARGAR LA IMAGEN ***************************************
    '*********************************************************************************************************

    'Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
    '    siguiente_acta() ' Primero se pide el valor de la siguiente
    '    txtjrv.Text = nextval
    '    mostrar_asamblea()
    'End Sub
    Private Sub siguiente_acta()
        con.Close()
        Try
            Dim sql As String = "select image_seq.nextval from dual"
            Dim comando As New OracleCommand(sql, con)
            Dim lector As OracleDataReader = Nothing
            con.Open()
            lector = comando.ExecuteReader
            If lector.HasRows Then
                Do While lector.Read
                    nextval = lector.GetInt32(0)
                Loop
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            con.Close()
        End Try
    End Sub
    Private Sub mostrar_asamblea()
        Try
            con.Close()
            Dim sql As String = "select imagendip from imagen where id_imagen=:ID"
            Dim comando As New OracleCommand(sql, con)                             ' Se hace una consulta para ver si aun quedan actas por digitar
            comando.Parameters.Add(":ID", OracleType.VarChar, 30).Value = nextval
            Dim lector As OracleDataReader = Nothing
            con.Open()
            lector = comando.ExecuteReader                                          'Si la consulta devuelve tuplas, quiere decir que aun faltan actas por digitar
            If lector.HasRows Then
                con.Close()
                
                'Aqui Agregare la funcion para que mande la imagen siempre y cuando el estado sea menor que 3
                Dim sqlmenor As String = "select imagendip from imagen where id_imagen=:ID and estado < 2"
                Dim comandomenor As New OracleCommand(sqlmenor, con)
                comandomenor.Parameters.Add(":ID", OracleType.VarChar, 30).Value = nextval
                Dim lectormenor As OracleDataReader = Nothing
                con.Open()
                lectormenor = comandomenor.ExecuteReader
                If lectormenor.HasRows Then
                    con.Close()
                    
                    Dim extraer As Byte() = ExtraerImagen(nextval, "select imagendip from imagen where id_imagen=:ID")
                    Dim ms As New MemoryStream(extraer)
                    LOGO.Image = Image.FromStream(ms)
                Else
                    siguiente_acta() ' Primero se pide el valor de la siguiente
                    txtjrv.Text = nextval
                    mostrar_asamblea()
                End If

            Else
                MessageBox.Show("Todas las actas han sido Digitadas")
                Me.Hide()
                Me.Close()
                LOGO.Image = Nothing

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    ' Metodo para Extraer imagen de la base de datos
    Function ExtraerImagen(ByVal ID As Integer, ByVal sql As String)

        Dim comando As New OracleCommand(sql, con)
        comando.Parameters.Add(":ID", OracleType.VarChar, 30).Value = ID
        Dim lector As OracleDataReader
        con.Open()
        lector = comando.ExecuteReader
        If lector.HasRows Then
            Dim MyPhoto() As Byte = CType(comando.ExecuteScalar(), Byte())
            con.Close()
            Return MyPhoto
        Else
            Return Nothing
        End If
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'ROTAR DERECHA
        Dim bm As Bitmap = LOGO.Image
        bm.RotateFlip(RotateFlipType.Rotate270FlipNone)
        Me.LOGO.Image = bm
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        'ROTAR IZQUIERDA
        Dim bm As Bitmap = LOGO.Image
        bm.RotateFlip(RotateFlipType.Rotate90FlipNone)
        Me.LOGO.Image = bm
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        If Button7.Text = "Cambiar Imagen ALCALDES" Then
            mostrarAlcaldes()
            Button7.Text = "Cambiar Imagen Asamblea"
        ElseIf Button7.Text = "Cambiar Imagen Asamblea" Then
            mostrar_asamblea()
            Button7.Text = "Cambiar Imagen ALCALDES"
        End If

    End Sub

    Private Sub mostrarAlcaldes()
        Try
            con.Close()
            Dim sql As String = "select imagenalc from imagen where id_imagen=:ID"
            Dim comando As New OracleCommand(sql, con)                             ' Se hace una consulta para ver si aun quedan actas por digitar
            comando.Parameters.Add(":ID", OracleType.VarChar, 30).Value = nextval
            Dim lector As OracleDataReader = Nothing
            con.Open()
            lector = comando.ExecuteReader                                          'Si la consulta devuelve tuplas, quiere decir que aun faltan actas por digitar
            If lector.HasRows Then
                con.Close()

                'Aqui Agregare la funcion para que mande la imagen siempre y cuando el estado sea menor que 3
                Dim sqlmenor As String = "select imagenalc from imagen where id_imagen=:ID and estado < 2"
                Dim comandomenor As New OracleCommand(sqlmenor, con)
                comandomenor.Parameters.Add(":ID", OracleType.VarChar, 30).Value = nextval
                Dim lectormenor As OracleDataReader = Nothing
                con.Open()
                lectormenor = comandomenor.ExecuteReader
                If lectormenor.HasRows Then
                    con.Close()
                    
                    Dim extraer As Byte() = ExtraerImagen(nextval, "select imagenalc from imagen where id_imagen=:ID")
                    Dim ms As New MemoryStream(extraer)
                    LOGO.Image = Image.FromStream(ms)
                Else
                    siguiente_acta() ' Primero se pide el valor de la siguiente
                    txtjrv.Text = nextval
                    mostrar_asamblea()
                End If
            Else
                MessageBox.Show("Todas las actas han sido Digitadas")
                LOGO.Image = Nothing
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub



    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        btningresar.Enabled = True
        'For Each control As Control In Me.Controls
        '    If TypeOf control Is TextBox Then
        '        If control.Text <> "" Then
        '            control.Text = ""
        '        End If
        '    End If

        'Next
        dgvData.DataSource = Nothing
        txtdui.Text = ""
        txtnombre1.Text = ""
        txtape1.Text = ""


        eval = True
        cmbcargo.SelectedIndex = -1
        cbProcedencia.SelectedItem = Nothing
        cbProcedencia.Enabled = False
        contador = 0
        dt.Rows.Clear()
        dtusuario.Rows.Clear()
        dgvData.DataSource = dtusuario
        eval = False
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            LOGO.Height = LOGO.Height + 450
            LOGO.Width = LOGO.Width + 450
            LOGO.SizeMode = PictureBoxSizeMode.StretchImage
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            LOGO.Height = LOGO.Height - 450
            LOGO.Width = LOGO.Width - 450
            LOGO.SizeMode = PictureBoxSizeMode.StretchImage
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
End Class
