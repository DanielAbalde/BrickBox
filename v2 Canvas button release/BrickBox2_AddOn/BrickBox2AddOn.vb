Imports System.Collections.Generic

Imports Grasshopper.Kernel
Imports Grasshopper
Imports System.Windows.Forms
Imports System.Drawing
Imports Rhino
Imports System.Reflection

Public Class SetupBrickBox
    Inherits Grasshopper.Kernel.GH_AssemblyPriority

    Public Overrides Function PriorityLoad() As GH_LoadingInstruction

        AddHandler Instances.CanvasCreated, AddressOf AppendBrickBox

        Return GH_LoadingInstruction.Proceed
    End Function

    Private Sub AppendBrickBox(canvas As GUI.Canvas.GH_Canvas)
        RemoveHandler Instances.CanvasCreated, AddressOf AppendBrickBox
        Dim editorType As Type = GetType(Grasshopper.GUI.GH_DocumentEditor)
        Dim binding As BindingFlags = BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.GetField
        Dim field As FieldInfo = editorType.GetField("_CanvasToolbar", binding)
        Dim fieldInstance As Object = field.GetValue(Instances.DocumentEditor)
        Dim toolstrip As ToolStrip = TryCast(fieldInstance, ToolStrip)
        If toolstrip Is Nothing Then Exit Sub
        toolstrip.Items.Add(New System.Windows.Forms.ToolStripSeparator)
        toolstrip.Items.Add(New BrickBoxButton())
    End Sub

End Class

Public Class BrickBoxButton
    Inherits System.Windows.Forms.ToolStripSplitButton

    Private mSelectDefForm As FormSelectDefinition
    Public OptionForm As FormTabItemOptions
    Public ImageForm As FormItemImage

#Region "New"
    Sub New()
        Instantiate()
        Load()
        AddHandler Instances.ActiveCanvas.MouseDown, AddressOf DropBrickClick
    End Sub
    Private Sub Instantiate()
        MyBase.Name = "Brick Box"
        MyBase.Size = New Size(24, 24)
        MyBase.DropDownButtonWidth = 14
        MyBase.ImageScaling = ToolStripItemImageScaling.None
        MyBase.Image = My.Resources.icon24x24closed
        MyBase.ToolTipText = "Manages Grasshopper snippets"
        AddHandler MyBase.DropDownOpened, AddressOf DropOpen
        AddHandler MyBase.DropDownClosed, AddressOf DropClosed
        AddHandler Instances.DocumentEditor.Click, AddressOf ClickElsewhere

    End Sub
    Private Sub Load()
        MyBase.DropDownItems.Clear()
        For Each st As String In Grasshopper.Instances.Settings.EntryNames
            Dim split As String() = st.Split(".")
            If split.Length <> 3 Then Continue For
            If split(0) <> "BrickBox" Then Continue For
            Dim TabName As String = split(1)
            Dim ItemName As String = split(2)
            Dim ItemData As String = Grasshopper.Instances.Settings.GetValue(st, String.Empty)
            If ItemData <> String.Empty Then
                Dim ImgStr As String = Grasshopper.Instances.Settings.GetValue(st & ".Image", String.Empty)
                Dim ItemImage As Image = Nothing
                If ImgStr <> String.Empty Then ItemImage = ConvertStringToImage(ImgStr)
                AddItem(TabName, ItemName, ItemImage, ItemData, False)
            End If
        Next
    End Sub
#End Region

#Region "Add/Remove/Change"
    Public Function AddTab(TabName As String) As Boolean
        If MyBase.DropDownItems.ContainsKey(TabName) Then Return False
        MyBase.DropDownItems.Add(CreateTab(TabName))
        Return True
    End Function
    Public Function AddItem(TabName As String, ItemName As String, ItemImage As Image, ItemData As String, Save As Boolean) As Boolean
        Dim Item As New System.Windows.Forms.ToolStripMenuItem(ItemName, ItemImage)
        AddHandler Item.MouseDown, AddressOf ClickedItem
        AddHandler Item.MouseHover, AddressOf HoverItem
        AddHandler Item.MouseLeave, AddressOf LeaveItem
        Item.Name = ItemName
        Item.Tag = ItemData

        If MyBase.DropDownItems.ContainsKey(TabName) Then
            Dim tab As System.Windows.Forms.ToolStripMenuItem = MyBase.DropDownItems.Find(TabName, False)(0)
            If tab.DropDownItems.ContainsKey(ItemName) Then
                MessageBox.Show("A block already exists with that name, try another please.", "Invalid name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            Else
                tab.DropDownItems.Add(Item)
            End If
        Else
            Dim Tab As System.Windows.Forms.ToolStripMenuItem = CreateTab(TabName)
            Tab.DropDownItems.Add(Item)
            MyBase.DropDownItems.Add(Tab)
        End If

        If Save Then
            Dim KeyName As String = SetKeySettings(TabName, ItemName)
            Grasshopper.Instances.Settings.SetValue(KeyName, ItemData)
            Grasshopper.Instances.Settings.SetValue(KeyName & ".Image", ConvertImageToString(ItemImage))
            Grasshopper.Instances.Settings.WritePersistentSettings()
        End If

        Return True
    End Function

    Private Function CreateTab(Name As String) As System.Windows.Forms.ToolStripMenuItem
        Dim Tab As New System.Windows.Forms.ToolStripMenuItem(Name)
        Tab.Name = Name
        AddHandler Tab.MouseDown, AddressOf ClickedTab
        Return Tab
    End Function
    Private Function SetKeySettings(TabName As String, ItemName As String) As String
        Return String.Format("BrickBox.{0}.{1}", TabName, ItemName)
    End Function

    Public Sub RemoveTab(TabName As String)
        MyBase.DropDownItems.RemoveByKey(TabName)
        Dim tabs As New List(Of String)
        For Each st As String In Grasshopper.Instances.Settings.EntryNames
            Dim split As String() = st.Split(".")
            If split(0) <> "BrickBox" Then Continue For
            If split(1) <> TabName Then Continue For
            Grasshopper.Instances.Settings.DeleteValue(st)
        Next
    End Sub
    Public Sub RemoveItem(TabName As String, ItemName As String)
        Dim tab As System.Windows.Forms.ToolStripMenuItem = MyBase.DropDownItems.Find(TabName, False)(0)
        tab.DropDownItems.RemoveByKey(ItemName)
        For Each st As String In Grasshopper.Instances.Settings.EntryNames
            Dim split As String() = st.Split(".")
            If split(0) <> "BrickBox" Then Continue For
            If split(1) <> TabName Then Continue For
            If split(2) = ItemName Then
                Grasshopper.Instances.Settings.DeleteValue(st)
                Grasshopper.Instances.Settings.DeleteValue(st & ".Image")
                Exit For
            End If
        Next
    End Sub

    Public Sub ChangeTabName(PreviousName As String, NewName As String)
        Dim tab As System.Windows.Forms.ToolStripMenuItem = MyBase.DropDownItems.Find(PreviousName, False)(0)
        tab.Name = NewName
        tab.Text = NewName
        For Each st As String In Grasshopper.Instances.Settings.EntryNames
            Dim split As String() = st.Split(".")
            If split(0) <> "BrickBox" Then Continue For
            If split(1) <> PreviousName Then Continue For
            Dim val As String = Grasshopper.Instances.Settings.GetValue(st, String.Empty)
            Grasshopper.Instances.Settings.DeleteValue(st)
            st = st.Replace(PreviousName, NewName)
            Grasshopper.Instances.Settings.SetValue(st, val)
        Next

    End Sub
    Public Sub ChangeItemName(TabName As String, PreviousName As String, NewName As String)
        Dim tab As System.Windows.Forms.ToolStripMenuItem = MyBase.DropDownItems.Find(TabName, False)(0)
        Dim item As System.Windows.Forms.ToolStripMenuItem = tab.DropDownItems.Find(PreviousName, False)(0)
        item.Name = NewName
        item.Text = NewName
        For Each st As String In Grasshopper.Instances.Settings.EntryNames
            Dim split As String() = st.Split(".")
            If split(0) <> "BrickBox" Then Continue For
            If split(1) <> TabName Then Continue For
            If split(2) <> PreviousName Then Continue For
            Dim val As String = Grasshopper.Instances.Settings.GetValue(st, String.Empty)
            Dim valImg As String = Grasshopper.Instances.Settings.GetValue(st & ".Image", String.Empty)
            Grasshopper.Instances.Settings.DeleteValue(st)
            Grasshopper.Instances.Settings.DeleteValue(st & ".Image")
            st = st.Replace(PreviousName, NewName)
            Grasshopper.Instances.Settings.SetValue(st, val)
            Grasshopper.Instances.Settings.SetValue(st & ".Image", valImg)
            Exit For
        Next

    End Sub
#End Region


#Region "Click"
    Private Sub ClickedButton(sender As Object, e As EventArgs) Handles MyBase.ButtonClick
        If SelectDefForm Is Nothing Then SelectDefForm = New FormSelectDefinition(Me)
    End Sub
    Private Sub ClickedTab(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Right Then
            Dim tab As System.Windows.Forms.ToolStripMenuItem = CType(sender, System.Windows.Forms.ToolStripMenuItem)
            If OptionForm Is Nothing Then OptionForm = New FormTabItemOptions(Me, tab, False)
        End If
    End Sub

    Private DroppingItem As System.Windows.Forms.ToolStripMenuItem
    Private Sub ClickedItem(sender As Object, e As MouseEventArgs)
        Dim item As System.Windows.Forms.ToolStripMenuItem = CType(sender, System.Windows.Forms.ToolStripMenuItem)
        If e.Button = MouseButtons.Left Then
            MyBase.DropDown.Close()
            Dim parent As System.Windows.Forms.ToolStripMenuItem = TryCast(item.OwnerItem, System.Windows.Forms.ToolStripMenuItem)
            parent.DropDown.Close()
            Instances.ActiveCanvas.ActiveInteraction = New Grasshopper.GUI.Canvas.Interaction.GH_DumpInteraction(Instances.ActiveCanvas)
            DroppingItem = item
        ElseIf e.Button = MouseButtons.Right Then
            If OptionForm Is Nothing Then OptionForm = New FormTabItemOptions(Me, item, True)
        End If
    End Sub
    Private Sub DropBrickClick(sender As Object, e As MouseEventArgs)
        MyBase.DropDown.Close()
        If e.Button <> MouseButtons.Left Then Exit Sub
        If DroppingItem IsNot Nothing Then
            DropBrick(DroppingItem)
            DroppingItem = Nothing
        End If
    End Sub
    Private Sub HoverItem(sender As Object, e As EventArgs)
        Dim item As System.Windows.Forms.ToolStripMenuItem = CType(sender, System.Windows.Forms.ToolStripMenuItem)
        Dim tab As System.Windows.Forms.ToolStripMenuItem = TryCast(item.OwnerItem, System.Windows.Forms.ToolStripMenuItem)
        tab.DropDown.AutoClose = False
        If ImageForm Is Nothing Then
            ImageForm = New FormItemImage(Me, item)
        End If
    End Sub
    Private Sub LeaveItem(sender As Object, e As EventArgs)
        Dim item As System.Windows.Forms.ToolStripMenuItem = CType(sender, System.Windows.Forms.ToolStripMenuItem)
        Dim tab As System.Windows.Forms.ToolStripMenuItem = TryCast(item.OwnerItem, System.Windows.Forms.ToolStripMenuItem)
        tab.DropDown.AutoClose = True
        If ImageForm IsNot Nothing Then
            ImageForm.Dispose()
            ImageForm.Close()
            ImageForm = Nothing
        End If

    End Sub
    Private Sub DropOpen(sender As Object, e As EventArgs)
        MyBase.Image = My.Resources.icon24x24open
    End Sub
    Private Sub DropClosed(sender As Object, e As EventArgs)
        MyBase.Image = My.Resources.icon24x24closed
    End Sub
    Private Sub ClickElsewhere(sender As Object, e As EventArgs)
        MyBase.DropDown.Close(System.Windows.Forms.ToolStripDropDownCloseReason.AppClicked)
    End Sub
#End Region

#Region "Capture/Drop"
    Public Function CaptureObjects() As String
        Dim DocIO As New GH_DocumentIO(Document)
        DocIO.Copy(GH_ClipboardType.System, True)
        Return System.Text.Encoding.Default.GetString(System.Text.ASCIIEncoding.ASCII.GetBytes(System.Windows.Forms.Clipboard.GetText()))
    End Function
    Public Function CaptureImage(RhinoView As System.Windows.Forms.CheckState) As Bitmap

        Document.DeselectAll()

        Dim image As Bitmap = Nothing
        If (RhinoView = CheckState.Checked) Then
            image = Rhino.RhinoDoc.ActiveDoc.Views.ActiveView.CaptureToBitmap(False, False, False)
        Else
            Dim acanvas As Grasshopper.GUI.Canvas.GH_Canvas = Grasshopper.Instances.ActiveCanvas
            image = acanvas.GetCanvasScreenBuffer(Grasshopper.GUI.Canvas.GH_CanvasMode.Export)
        End If
        Instances.ActiveCanvas.Refresh()
        Return image
    End Function
    Public Sub DropBrick(Item As System.Windows.Forms.ToolStripMenuItem)
        Dim Doc As GH_Document = Nothing
        If Instances.ActiveCanvas.IsDocument Then
            Doc = Document
        Else
            Doc = Instances.DocumentServer.AddNewDocument()
            Instances.ActiveCanvas.Document = Doc
        End If

        Dim data As Byte() = System.Text.Encoding.Default.GetBytes(CStr(Item.Tag))
        Dim xml As String = System.Text.ASCIIEncoding.ASCII.GetString(data)
        System.Windows.Forms.Clipboard.SetText(xml)

        Dim DocIO As New GH_DocumentIO(Doc)
        DocIO.Paste(GH_ClipboardType.System)
        System.Windows.Forms.Clipboard.Clear()

        Dim Mypivot As PointF = Instances.ActiveCanvas.CursorCanvasPosition
        Dim mayorX As Int32 = Int32.MaxValue
        Dim mayorY As Int32 = Int32.MaxValue
        For Each obj As IGH_DocumentObject In DocIO.Document.Objects()
            ' If Not (obj.Name.Equals("Group")) Then
            Dim pivot As PointF = obj.Attributes.Pivot
            If (pivot.X < mayorX) Then mayorX = pivot.X
            If (pivot.Y < mayorY) Then mayorY = pivot.Y
            ' End If
        Next

        Dim offset As New Size(Mypivot.X - mayorX, Mypivot.Y - mayorY)
        DocIO.Document.TranslateObjects(offset, False)

        DocIO.Document.SelectAll()
        DocIO.Document.ExpireSolution()
        DocIO.Document.MutateAllIds()
        Doc.DeselectAll()
        Doc.MergeDocument(DocIO.Document)
        Dim objects As List(Of IGH_DocumentObject) = DocIO.Document.Objects
        Doc.UndoUtil.RecordAddObjectEvent("Paste", objects)
        Doc.ScheduleSolution(10)
    End Sub

#End Region


    Private Function ConvertImageToString(ItemImage As Image) As String
        Using memStream As New System.IO.MemoryStream
            ItemImage.Save(memStream, Imaging.ImageFormat.Png)
            Dim result As String = Convert.ToBase64String(memStream.ToArray())
            memStream.Close()
            Return result
        End Using
    End Function
    Private Function ConvertStringToImage(ImgStr As String) As Image
        Using memStream As New System.IO.MemoryStream(Convert.FromBase64String(ImgStr))
            Dim result As Image = Image.FromStream(memStream)
            memStream.Close()
            Return result
        End Using
    End Function

    Public ReadOnly Property Document As GH_Document
        Get
            Return Grasshopper.Instances.ActiveCanvas.Document
        End Get
    End Property
    Public Property SelectDefForm As FormSelectDefinition
        Get
            Return mSelectDefForm
        End Get
        Set(value As FormSelectDefinition)
            mSelectDefForm = value
            If value Is Nothing Then
                MyBase.Image = New Bitmap("X:\Grasshopper\Míos\BrickBox 2\icon24x24closed.png")
            Else
                MyBase.Image = New Bitmap("X:\Grasshopper\Míos\BrickBox 2\icon24x24open.png")
            End If
        End Set
    End Property
End Class


Public Class FormSelectDefinition
    Inherits System.Windows.Forms.Form

    Private BrickBoxOwner As BrickBoxButton
    Sub New(BBB As BrickBoxButton)
        BrickBoxOwner = BBB
        InitializeComponent()
        For Each Tab As System.Windows.Forms.ToolStripMenuItem In BBB.DropDownItems
            Me.ComboBox_TabNames.Items.Add(Tab.Name)
        Next
        Show()
    End Sub

    Private Sub Form_Closed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        BrickBoxOwner.SelectDefForm.Dispose()
        BrickBoxOwner.SelectDefForm = Nothing
    End Sub

    Private Sub Button_OK_Click(sender As Object, e As EventArgs) Handles Button_OK.Click
        If BrickBoxOwner.Document Is Nothing Then
            Close()
            Exit Sub
        End If
        If BrickBoxOwner.Document.SelectedCount = 0 Then
            MessageBox.Show("No selected component", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim TabName As String = Me.ComboBox_TabNames.Text
        If TabName = String.Empty Then Exit Sub
        Dim ItemName As String = Me.TextBox_DefName.Text
        If ItemName = String.Empty Then Exit Sub
        Dim ItemData As String = BrickBoxOwner.CaptureObjects()
        If ItemData = String.Empty Then Exit Sub
        Dim ItemImage As Image = BrickBoxOwner.CaptureImage(Me.CheckBox_View.CheckState)
        If BrickBoxOwner.AddItem(TabName, ItemName, ItemImage, ItemData, True) Then
            Close()
        End If
    End Sub

    Private Sub Button_Cancel_Click(sender As Object, e As EventArgs) Handles Button_Cancel.Click
        Close()
    End Sub

    Private FirstClick As Boolean = True
    Private Sub TextBox_DefName_Click(sender As Object, e As EventArgs) Handles TextBox_DefName.Click
        If FirstClick Then TextBox_DefName.SelectAll()
        FirstClick = False
    End Sub

#Region "Design"
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Button_OK = New System.Windows.Forms.Button()
        Me.Button_Cancel = New System.Windows.Forms.Button()
        Me.ComboBox_TabNames = New System.Windows.Forms.ComboBox()
        Me.TextBox_DefName = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CheckBox_View = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'Button_OK
        '
        Me.Button_OK.Location = New System.Drawing.Point(115, 65)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(123, 23)
        Me.Button_OK.TabIndex = 1
        Me.Button_OK.Text = "Done"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'Button_Cancel
        '
        Me.Button_Cancel.Location = New System.Drawing.Point(34, 65)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Button_Cancel.TabIndex = 2
        Me.Button_Cancel.Text = "Cancel"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'ComboBox_TabNames
        '
        Me.ComboBox_TabNames.FormattingEnabled = True
        Me.ComboBox_TabNames.Location = New System.Drawing.Point(12, 12)
        Me.ComboBox_TabNames.Name = "ComboBox_TabNames"
        Me.ComboBox_TabNames.Size = New System.Drawing.Size(226, 21)
        Me.ComboBox_TabNames.TabIndex = 5
        Me.ComboBox_TabNames.Text = "Tab name"
        Me.ToolTip1.SetToolTip(Me.ComboBox_TabNames, "Name of tab." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "You can select an existing or write a new one.")
        '
        'TextBox_DefName
        '
        Me.TextBox_DefName.Location = New System.Drawing.Point(12, 39)
        Me.TextBox_DefName.Name = "TextBox_DefName"
        Me.TextBox_DefName.Size = New System.Drawing.Size(226, 20)
        Me.TextBox_DefName.TabIndex = 6
        Me.TextBox_DefName.Text = "Brick name"
        Me.ToolTip1.SetToolTip(Me.TextBox_DefName, "Identifier name for the definition.")
        '
        'CheckBox_View
        '
        Me.CheckBox_View.AutoSize = True
        Me.CheckBox_View.Location = New System.Drawing.Point(13, 68)
        Me.CheckBox_View.Name = "CheckBox_View"
        Me.CheckBox_View.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox_View.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.CheckBox_View, "Check for capture Rhino view, otherwise GH canvas will ve captured")
        Me.CheckBox_View.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(250, 97)
        Me.ControlBox = False
        Me.Controls.Add(Me.CheckBox_View)
        Me.Controls.Add(Me.TextBox_DefName)
        Me.Controls.Add(Me.ComboBox_TabNames)
        Me.Controls.Add(Me.Button_Cancel)
        Me.Controls.Add(Me.Button_OK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Location = Grasshopper.Instances.ActiveCanvas.PointToScreen(New System.Drawing.Point(0, 0))
        Me.Text = "Add new brick"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button_OK As Button
    Friend WithEvents Button_Cancel As Button
    Friend WithEvents ComboBox_TabNames As ComboBox
    Friend WithEvents TextBox_DefName As TextBox
    Friend WithEvents CheckBox_View As CheckBox
    Friend WithEvents ToolTip1 As ToolTip
#End Region

End Class

Public Class FormTabItemOptions
    Inherits System.Windows.Forms.Form

    Private BrickBoxOwner As BrickBoxButton
    Private ItemOwner As System.Windows.Forms.ToolStripMenuItem

    Private IsItem As Boolean

    Sub New(BBB As BrickBoxButton, Item As System.Windows.Forms.ToolStripMenuItem, IsBrick As Boolean)
        IsItem = IsBrick
        BrickBoxOwner = BBB
        ItemOwner = Item
        InitializeComponent()
        If IsBrick Then
            Me.Text = "Brick options"
        Else
            Me.Text = "Tab options"
        End If

        Me.TextBox_Rename.Text = Item.Name
        Show()
    End Sub

    Private Sub Form_Closed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        BrickBoxOwner.OptionForm.Dispose()
        BrickBoxOwner.OptionForm = Nothing
    End Sub

    Private Sub Button_Done_Click(sender As Object, e As EventArgs) Handles Button_Done.Click
        If Me.TextBox_Rename.Text = "" Then Exit Sub
        If ItemOwner.Name <> TextBox_Rename.Text Then
            If IsItem Then
                Dim parent As System.Windows.Forms.ToolStripMenuItem = TryCast(ItemOwner.OwnerItem, System.Windows.Forms.ToolStripMenuItem)
                BrickBoxOwner.ChangeItemName(parent.Name, ItemOwner.Name, TextBox_Rename.Text)
            Else
                BrickBoxOwner.ChangeTabName(ItemOwner.Name, TextBox_Rename.Text)
            End If
        End If
        Close()

    End Sub

    Private Sub Button_Delete_Click(sender As Object, e As EventArgs) Handles Button_Delete.Click
        Try

            If IsItem Then
                If MessageBox.Show("Are you sure?", "Remove brick", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                    For Each tab As System.Windows.Forms.ToolStripMenuItem In BrickBoxOwner.DropDownItems
                        For i As Int32 = 0 To tab.DropDownItems.Count - 1
                            If tab.DropDownItems(i).Name = ItemOwner.Name Then
                                BrickBoxOwner.RemoveItem(tab.Name, ItemOwner.Name)
                                Exit For
                            End If
                        Next
                    Next
                    Close()
                End If
            Else
                If MessageBox.Show("Are you sure?", "Remove tab", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                    For i As Int32 = 0 To BrickBoxOwner.DropDownItems.Count - 1
                        If BrickBoxOwner.DropDownItems(i).Name = ItemOwner.Name Then
                            BrickBoxOwner.RemoveTab(ItemOwner.Name)
                            Exit For
                        End If
                    Next
                    Close()
                End If
            End If

        Catch ex As Exception
            RhinoApp.WriteLine(ex.ToString)
        End Try
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.TextBox_Rename = New System.Windows.Forms.TextBox()
        Me.Button_Delete = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Button_Done = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'TextBox_Rename
        '
        Me.TextBox_Rename.Location = New System.Drawing.Point(12, 12)
        Me.TextBox_Rename.Name = "TextBox_Rename"
        Me.TextBox_Rename.Size = New System.Drawing.Size(138, 20)
        Me.TextBox_Rename.TabIndex = 0
        Me.TextBox_Rename.Text = "NOMBRE"
        '
        'Button_Delete
        '
        Me.Button_Delete.Location = New System.Drawing.Point(12, 38)
        Me.Button_Delete.Name = "Button_Delete"
        Me.Button_Delete.Size = New System.Drawing.Size(57, 23)
        Me.Button_Delete.TabIndex = 1
        Me.Button_Delete.Text = "Remove"
        Me.ToolTip1.SetToolTip(Me.Button_Delete, "Remove this brick")
        Me.Button_Delete.UseVisualStyleBackColor = True
        '
        'Button_Done
        '
        Me.Button_Done.Location = New System.Drawing.Point(75, 38)
        Me.Button_Done.Name = "Button_Done"
        Me.Button_Done.Size = New System.Drawing.Size(75, 23)
        Me.Button_Done.TabIndex = 2
        Me.Button_Done.Text = "Done"
        Me.Button_Done.UseVisualStyleBackColor = True
        '
        'FormItemOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(160, 71)
        Me.Controls.Add(Me.Button_Done)
        Me.Controls.Add(Me.Button_Delete)
        Me.Controls.Add(Me.TextBox_Rename)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "FormItemOptions"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Location = Grasshopper.Instances.ActiveCanvas.PointToScreen(New System.Drawing.Point(0, 0))
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBox_Rename As TextBox
    Friend WithEvents Button_Delete As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Button_Done As Button
End Class

Public Class FormItemImage
    Inherits System.Windows.Forms.Form

    Sub New(BBB As BrickBoxButton, Item As System.Windows.Forms.ToolStripMenuItem)
        InitializeComponent()
        Dim loc As System.Drawing.Point = BBB.DropDown.Bounds.Location
        Dim img As Image = Item.Image
        Dim Ratio As Double = img.Height / img.Width
        Dim Max As Integer = loc.X - Grasshopper.Instances.DocumentEditor.Location.X - 10
        Dim w As Integer = Math.Min(Max, img.Width)
        Dim h As Integer = w * Ratio
        loc.X -= w
        Me.Location = loc
        Me.Size = New Size(w, h)
        Me.PictureBox_Item.Image = img
        Me.Show()

    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    Private Sub InitializeComponent()
        Me.PictureBox_Item = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox_Item, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox_Item
        '
        Me.PictureBox_Item.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox_Item.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox_Item.Name = "PictureBox_Item"
        Me.PictureBox_Item.Size = New System.Drawing.Size(284, 261)
        Me.PictureBox_Item.TabIndex = 0
        Me.PictureBox_Item.TabStop = False
        Me.PictureBox_Item.SizeMode = PictureBoxSizeMode.StretchImage
        '
        'Form_ItemImage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.ControlBox = False
        Me.Controls.Add(Me.PictureBox_Item)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.StartPosition = FormStartPosition.Manual
        Me.Name = "Form_ItemImage"
        CType(Me.PictureBox_Item, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PictureBox_Item As PictureBox
End Class
