Imports System.Data

Class MainWindow
    Dim soapcall As Soapy.TPReadOnlyDataServiceSoapClient = New Soapy.TPReadOnlyDataServiceSoapClient()
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Dim thetable As DataTable = soapcall.findEntities("petessoaptest", "purple").Tables(0)
        AvailableTablesBox.ItemsSource = thetable.DefaultView
        AvailableTablesBox.DisplayMemberPath = "EntityName"
        AvailableTablesBox.SelectedValuePath = "EntityName"
    End Sub

    Private Sub AvailableTablesBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles AvailableTablesBox.SelectionChanged
        Dim tablename As String = AvailableTablesBox.SelectedValue
        Dim theTable As DataTable = soapcall.getEntityData(tablename, "1=0", "petessoaptest", "purple").Tables(0)
        Dim headings As DataColumnCollection = theTable.Columns
        Dim headingNames As New List(Of String)
        For Each heading In headings
            headingNames.Add(heading.ToString)
        Next
        queryTextBox.Text = ""
        fieldNameBox.ItemsSource = headingNames
    End Sub

    Private Sub addFieldButton_Click(sender As Object, e As RoutedEventArgs) Handles addFieldButton.Click
        If queryTextBox.Text.Length = 0 Then
            queryTextBox.Text = fieldNameBox.SelectedValue + " "
        Else
            queryTextBox.Text = queryTextBox.Text + " " + fieldNameBox.SelectedValue + " "
        End If
        queryTextBox.Focus()
        queryTextBox.Select(queryTextBox.Text.Length, 0)
    End Sub

    Private Sub queryButton_Click(sender As Object, e As RoutedEventArgs) Handles queryButton.Click
        Dim theTable As DataTable = soapcall.getEntityData(AvailableTablesBox.SelectedValue, queryTextBox.Text, "petessoaptest", "purple").Tables(0)
        tableOutput.ItemsSource = theTable.DefaultView

    End Sub
End Class
