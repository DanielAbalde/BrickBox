Imports Rhino
Imports Grasshopper
Imports Grasshopper.Kernel
Imports GH_IO
Imports GH_IO.Serialization
Imports Grasshopper.GUI
Imports Grasshopper.GUI.Canvas
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class Form1
    Inherits System.Windows.Forms.Form

    Public Caja As BrickBox
    Public MensajeForm As MensajeSeleccion
    Public Doc As GH_Document
    Public Comp As BrickBoxComp
    Private AskToSave As Boolean

    Sub New(_Comp As BrickBoxComp)
        InitializeComponent()
        Comp = _Comp
        Doc = _Comp.OnPingDocument()
        Me.Show()
        Caja = New BrickBox(Me)
        Caja.Seleccionado = Nothing
        AskToSave = True
    End Sub

#Region "Design"

    Private Const WCentralPanel As Integer = 300
    Private Const WTabsPanel As Integer = 160
    Private Const WMinPanel As Integer = 12

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.SplitContainer1.SplitterDistance = WMinPanel
    End Sub

    Private Sub Form1_ResizeEnd(sender As Object, e As EventArgs) Handles MyBase.ResizeEnd
        If (Convert.ToBoolean(Me.ButtOpenTabs.Tag)) Then
            Me.GroupTree.Size = New System.Drawing.Size(WTabsPanel - 8, Me.Height - 216)
            Me.TreeView1.Size = New System.Drawing.Size(WTabsPanel - 16, Me.Height - 233)
        End If
    End Sub

    Private Sub CloseBoxAtt()
        Me.Comp.IsOpen = False
        Me.Comp.ExpireSolution(True)
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

    Public components As System.ComponentModel.IContainer

    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.ButtOpenTabs = New System.Windows.Forms.Button()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer6 = New System.Windows.Forms.SplitContainer()
        Me.ButtFile = New System.Windows.Forms.Button()
        Me.SplitContainer7 = New System.Windows.Forms.SplitContainer()
        Me.ButtDel = New System.Windows.Forms.Button()
        Me.ButtAdd = New System.Windows.Forms.Button()
        Me.SplitContainer5 = New System.Windows.Forms.SplitContainer()
        Me.ButtCancel = New System.Windows.Forms.Button()
        Me.ButtAccept = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.SplitContainer4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        CType(Me.SplitContainer6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer6.Panel1.SuspendLayout()
        Me.SplitContainer6.Panel2.SuspendLayout()
        Me.SplitContainer6.SuspendLayout()
        CType(Me.SplitContainer7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer7.Panel1.SuspendLayout()
        Me.SplitContainer7.Panel2.SuspendLayout()
        Me.SplitContainer7.SuspendLayout()
        CType(Me.SplitContainer5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer5.Panel1.SuspendLayout()
        Me.SplitContainer5.Panel2.SuspendLayout()
        Me.SplitContainer5.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.IsSplitterFixed = True
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1MinSize = 12
        ' Me.SplitContainer1.Panel1.MinimumSize = New Size(12, 333)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ButtOpenTabs)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer1.Panel2MinSize = 68
        Me.SplitContainer1.Size = New System.Drawing.Size(352, 333)
        Me.SplitContainer1.SplitterDistance = 12
        Me.SplitContainer1.SplitterWidth = 1
        Me.SplitContainer1.TabIndex = 0
        '
        'ButtOpenTabs
        '
        Me.ButtOpenTabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtOpenTabs.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtOpenTabs.Location = New System.Drawing.Point(0, 0)
        Me.ButtOpenTabs.Margin = New System.Windows.Forms.Padding(0)
        Me.ButtOpenTabs.Name = "ButtOpenTabs"
        Me.ButtOpenTabs.Size = New System.Drawing.Size(12, 333)
        Me.ButtOpenTabs.TabIndex = 6
        Me.ButtOpenTabs.TabStop = False
        Me.ButtOpenTabs.Tag = "False"
        Me.ButtOpenTabs.Text = "<"
        Me.ToolTip1.SetToolTip(Me.ButtOpenTabs, "Show the tabs manager")
        Me.ButtOpenTabs.UseVisualStyleBackColor = False
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.TabControl1)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.SplitContainer4)
        Me.SplitContainer3.Panel2.Padding = New System.Windows.Forms.Padding(4, 0, 4, 6)
        Me.SplitContainer3.Size = New System.Drawing.Size(300, 333)
        Me.SplitContainer3.SplitterDistance = 300
        Me.SplitContainer3.SplitterWidth = 1
        Me.SplitContainer3.TabIndex = 0
        Me.SplitContainer3.IsSplitterFixed = True
        '
        'TabControl1
        '
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(300, 300)
        Me.TabControl1.TabIndex = 0
        Me.TabControl1.AllowDrop = True
        '
        'SplitContainer4
        '
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.Location = New System.Drawing.Point(4, 0)
        Me.SplitContainer4.Name = "SplitContainer4"
        Me.SplitContainer4.IsSplitterFixed = True
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.SplitContainer6)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.SplitContainer5)
        Me.SplitContainer4.Size = New System.Drawing.Size(292, 26)
        Me.SplitContainer4.SplitterDistance = 146
        Me.SplitContainer4.TabIndex = 0
        '
        'SplitContainer6
        '
        Me.SplitContainer6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer6.IsSplitterFixed = True
        Me.SplitContainer6.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer6.Name = "SplitContainer6"
        Me.SplitContainer6.IsSplitterFixed = True
        '
        'SplitContainer6.Panel1
        '
        Me.SplitContainer6.Panel1.Controls.Add(Me.ButtFile)
        Me.SplitContainer6.Panel1MinSize = 10
        '
        'SplitContainer6.Panel2
        '
        Me.SplitContainer6.Panel2.Controls.Add(Me.SplitContainer7)
        Me.SplitContainer6.Size = New System.Drawing.Size(146, 26)
        Me.SplitContainer6.SplitterDistance = 35
        Me.SplitContainer6.TabIndex = 0
        '
        'ButtFile
        '
        Me.ButtFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtFile.Location = New System.Drawing.Point(0, 0)
        Me.ButtFile.Name = "ButtFile"
        Me.ButtFile.Size = New System.Drawing.Size(30, 26)
        Me.ButtFile.TabIndex = 4
        Me.ButtFile.Text = "File"
        Me.ToolTip1.SetToolTip(Me.ButtFile, "Select the file")
        Me.ButtFile.UseVisualStyleBackColor = True
        '
        'SplitContainer7
        '
        Me.SplitContainer7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer7.IsSplitterFixed = True
        Me.SplitContainer7.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer7.Name = "SplitContainer7"
        Me.SplitContainer7.IsSplitterFixed = True
        '
        'SplitContainer7.Panel1
        '
        Me.SplitContainer7.Panel1.Controls.Add(Me.ButtDel)
        '
        'SplitContainer7.Panel2
        '
        Me.SplitContainer7.Panel2.Controls.Add(Me.ButtAdd)
        Me.SplitContainer7.Size = New System.Drawing.Size(112, 26)
        Me.SplitContainer7.SplitterDistance = 55
        Me.SplitContainer7.TabIndex = 0
        '
        'ButtDel
        '
        Me.ButtDel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtDel.Location = New System.Drawing.Point(0, 0)
        Me.ButtDel.Name = "ButtDel"
        Me.ButtDel.Size = New System.Drawing.Size(55, 26)
        Me.ButtDel.TabIndex = 3
        Me.ButtDel.Text = "Del"
        Me.ToolTip1.SetToolTip(Me.ButtDel, "Delete a brick of the box")
        Me.ButtDel.UseVisualStyleBackColor = True
        '
        'ButtAdd
        '
        Me.ButtAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtAdd.Location = New System.Drawing.Point(0, 0)
        Me.ButtAdd.Name = "ButtAdd"
        Me.ButtAdd.Size = New System.Drawing.Size(53, 26)
        Me.ButtAdd.TabIndex = 2
        Me.ButtAdd.Text = "Add"
        Me.ToolTip1.SetToolTip(Me.ButtAdd, "Add a new brick of components")
        Me.ButtAdd.UseVisualStyleBackColor = True
        '
        'SplitContainer5
        '
        Me.SplitContainer5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer5.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer5.Name = "SplitContainer5"
        Me.SplitContainer5.IsSplitterFixed = True
        '
        'SplitContainer5.Panel1
        '
        Me.SplitContainer5.Panel1.Controls.Add(Me.ButtCancel)
        '
        'SplitContainer5.Panel2
        '
        Me.SplitContainer5.Panel2.Controls.Add(Me.ButtAccept)
        Me.SplitContainer5.Size = New System.Drawing.Size(142, 26)
        Me.SplitContainer5.SplitterDistance = 68
        Me.SplitContainer5.TabIndex = 0
        '
        'ButtCancel
        '
        Me.ButtCancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtCancel.Location = New System.Drawing.Point(0, 0)
        Me.ButtCancel.Name = "ButtCancel"
        Me.ButtCancel.Size = New System.Drawing.Size(68, 26)
        Me.ButtCancel.TabIndex = 1
        Me.ButtCancel.Text = "Cancel"
        Me.ToolTip1.SetToolTip(Me.ButtCancel, "Close the box")
        Me.ButtCancel.UseVisualStyleBackColor = True
        '
        'ButtAccept
        '
        Me.ButtAccept.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtAccept.Location = New System.Drawing.Point(0, 0)
        Me.ButtAccept.Name = "ButtAccept"
        Me.ButtAccept.Size = New System.Drawing.Size(70, 26)
        Me.ButtAccept.TabIndex = 0
        Me.ButtAccept.Text = "Accept"
        Me.ToolTip1.SetToolTip(Me.ButtAccept, "Take a brick of components to the canvas")
        Me.ButtAccept.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = ""
        Me.OpenFileDialog1.Filter = "gh file|*.gh|ghx file|*.ghx"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(310, 333)
        Me.MinimumSize = New System.Drawing.Size(200, 214)
        Me.Controls.Add(Me.SplitContainer1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
        Me.Icon = My.Resources.Icon24x24
        Me.ShowInTaskbar = True
        Me.ShowIcon = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "    Brick Box "
        Me.Owner = Grasshopper.Instances.DocumentEditor
        Me.HelpButton = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.ResumeLayout(False)
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer4.ResumeLayout(False)
        Me.SplitContainer6.Panel1.ResumeLayout(False)
        Me.SplitContainer6.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer6.ResumeLayout(False)
        Me.SplitContainer7.Panel1.ResumeLayout(False)
        Me.SplitContainer7.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer7.ResumeLayout(False)
        Me.SplitContainer5.Panel1.ResumeLayout(False)
        Me.SplitContainer5.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer5.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents ButtOpenTabs As Button
    Friend WithEvents SplitContainer3 As SplitContainer
    Friend WithEvents SplitContainer4 As SplitContainer
    Friend WithEvents SplitContainer6 As SplitContainer
    Friend WithEvents ButtFile As Button
    Friend WithEvents SplitContainer7 As SplitContainer
    Friend WithEvents ButtDel As Button
    Friend WithEvents ButtAdd As Button
    Friend WithEvents SplitContainer5 As SplitContainer
    Friend WithEvents ButtCancel As Button
    Friend WithEvents ButtAccept As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
#End Region

#Region "Buttons"

    Private Sub ButtOpenTabs_MouseClick(sender As Object, e As MouseEventArgs) Handles ButtOpenTabs.MouseClick
        If (e.Button = MouseButtons.Left) Then
            Me.SuspendLayout()
            If Not (Convert.ToBoolean(Me.ButtOpenTabs.Tag)) Then
                Me.ButtOpenTabs.Tag = True
                Me.ButtOpenTabs.Dock = DockStyle.Right
                Me.ButtOpenTabs.Text = ">"
                Me.ButtOpenTabs.Size = New Size(WMinPanel, Me.ButtOpenTabs.Size.Height)
                Me.Location = New Point(Me.Location.X - WTabsPanel, Me.Location.Y)
                Me.Size = New Size(Me.Width + WTabsPanel, Me.Height)
                Me.Text = "                                                        Brick Box"
                Me.ToolTip1.SetToolTip(Me.ButtOpenTabs, "Hide the tabs manager")
                Me.AddTabsControls()
                Me.TabsToTree()
            Else
                Me.RemoveTabsControls()

                Me.ButtOpenTabs.Tag = False
                Me.ButtOpenTabs.Dock = DockStyle.Fill
                Me.ButtOpenTabs.Text = "<"
                Me.Location = New Point(Me.Location.X + WTabsPanel, Me.Location.Y)
                Me.Size = New Size(Me.Width - WTabsPanel, Me.Height)
                Me.Text = "   Brick Box"
                Me.SplitContainer1.SplitterDistance = WMinPanel
                Me.ToolTip1.SetToolTip(Me.ButtOpenTabs, "Show the tabs manager")
            End If
            Me.ResumeLayout()
        End If
    End Sub

    Private Sub ButtAccept_MouseClick(sender As Object, e As MouseEventArgs) Handles ButtAccept.MouseClick
        If (e.Button = MouseButtons.Left) Then
            AskToSave = False
            If (Caja.Seleccionado IsNot Nothing) Then
                Me.Close()
                Caja.Seleccionado.BrickToCanvas()
            Else
                Dim result As DialogResult = MessageBox.Show("No brick selected. You want to close the box?", "", MessageBoxButtons.YesNo)
                If (result = DialogResult.Yes) Then
                    Me.Close()
                Else
                    Exit Sub
                End If
            End If
        End If
    End Sub

    Private Sub ButtCancel_MouseClick(sender As Object, e As MouseEventArgs) Handles ButtCancel.MouseClick
        If (e.Button = MouseButtons.Left) Then
            AskToSave = True
            Me.Close()
        End If
    End Sub

    Private Sub ButtAdd_MouseClick(sender As Object, e As MouseEventArgs) Handles ButtAdd.MouseClick
        If (e.Button = MouseButtons.Left) Then
            If (Me.TabControl1.TabCount > 0) Then
                If (e.Button = MouseButtons.Left) AndAlso (MensajeForm Is Nothing) Then MensajeForm = New MensajeSeleccion(Me)
            Else
                MessageBox.Show("You need to have at least one tab.")
            End If
        End If
    End Sub

    Private Sub ButtDel_MouseClick(sender As Object, e As MouseEventArgs) Handles ButtDel.MouseClick
        If (e.Button = MouseButtons.Left) Then
            If (Caja.Seleccionado Is Nothing) Then
                MessageBox.Show("Nothing selected, nothing deleted.")
                Return
            End If
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete the """ & Caja.Seleccionado.Nombre & """ brick?", "", MessageBoxButtons.YesNo)
            If (result = DialogResult.Yes) Then Me.Caja.DeleteBrick()
        End If
    End Sub

    Private Sub ButtFile_MouseClick(sender As Object, e As MouseEventArgs) Handles ButtFile.MouseClick
        If (e.Button = MouseButtons.Left) Then
            If (Me.Caja.HaCambiado) Then
                Dim result As DialogResult = MessageBox.Show("Saves changes?", "", MessageBoxButtons.YesNo)
                If (result = DialogResult.Yes) Then Caja.SaveToFile()
            End If
            Caja.OpenNewFile()
            End If
    End Sub

    Private Sub Form1_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed

        If (Caja.Display IsNot Nothing) Then Caja.HidePreview()
        If (Me.Caja.HaCambiado) Then
            If (AskToSave) Then
                Dim result As DialogResult = MessageBox.Show("Saves changes?", "", MessageBoxButtons.YesNo)
                If (result = DialogResult.Yes) Then Caja.SaveToFile()
            Else
                Caja.SaveToFile()
            End If
        End If
        Me.Dispose()
        CloseBoxAtt()

    End Sub

    Private Sub Form1_HelpButtonClicked(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.HelpButtonClicked

        MessageBox.Show("Gh Brick Box is an UI that serves to manage pieces of definitions," & vbCrLf &
                        "geometry, or components with added parameters, all within a component." & vbCrLf &
                        "" & vbCrLf &
"Requires to have loaded a gh file (*.gh/*.ghx) to read and store binary data. Note that the file becomes unusable, because if you save the document from gh you will lose all data." & vbCrLf &
                        "" & vbCrLf &
"*How to use it*" & vbCrLf &
"First you need to have loaded a file to read or write BrickBox data. Use the |File button| to open a new file." & vbCrLf &
"When you open a file with BrickBox data, will appear tabs and bricks. You can browse, select the item you want and press |Accept button| to insert it to the canvas." & vbCrLf &
"Pressing right click on a brick, a window with the enlarge image is displayed. From this window you can change the name, tab or image of a brick." & vbCrLf &
"You can manage the tabs from the |left side button|, selecting an edit mode, filling text boxes and pressing accept to make changes." & vbCrLf &
"To add a new brick in the box, press the |Add button| and a pop-up window will appear. Select the components on the canvas which want to add," & vbCrLf &
"the image of the brick may be the screenshot of the canvas or rhino viewport. Add a name for the new brick in the text box and press done." & vbCrLf &
"" & vbCrLf &
"You can drag a brick or a tab from one box to another to add it to the target box pressing CTRL key while dragging. You can also drag to the box a gh file which contains BrickBox data for attaching all its contents to the target box." & vbCrLf &
        "Moving the mouse wheel And pressing the SHIFT key you can change the size Of the bricks." & vbCrLf &
        "" & vbCrLf &
"Right-clicking on the component, you can access to contact and development information.", "Help")
        e.Cancel = True
    End Sub
#End Region

#Region "Tab"
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents ListModesBox As System.Windows.Forms.ListBox
    Friend WithEvents AcceptButtTab As System.Windows.Forms.Button
    Friend WithEvents GroupControls As System.Windows.Forms.GroupBox
    Friend WithEvents GroupMode As System.Windows.Forms.GroupBox
    Friend WithEvents GroupTree As System.Windows.Forms.GroupBox
    Friend WithEvents Split1 As System.Windows.Forms.SplitContainer

    Private Sub AddTabsControls()

        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.ListModesBox = New System.Windows.Forms.ListBox()
        Me.GroupControls = New System.Windows.Forms.GroupBox()
        Me.GroupMode = New System.Windows.Forms.GroupBox()
        Me.AcceptButtTab = New System.Windows.Forms.Button()
        Me.Split1 = New System.Windows.Forms.SplitContainer()
        Me.GroupTree = New System.Windows.Forms.GroupBox()
        CType(Me.Split1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Split1.Panel1.SuspendLayout()
        Me.Split1.Panel2.SuspendLayout()
        Me.Split1.SuspendLayout()
        Me.GroupTree.SuspendLayout()
        Me.GroupMode.SuspendLayout()
        '
        'TreeView1
        '
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView1.BorderStyle = BorderStyle.None
        Me.TreeView1.BackColor = SystemColors.Control
        Me.TreeView1.Location = New System.Drawing.Point(4, 16)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.Size = New System.Drawing.Size(WTabsPanel - 16, Me.GroupTree.Height - 4)
        Me.TreeView1.TabIndex = 5
        '
        'ListModesBox
        '
        Me.ListModesBox.BackColor = SystemColors.Control
        Me.ListModesBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListModesBox.Dock = System.Windows.Forms.DockStyle.Left
        Me.ListModesBox.FormattingEnabled = True
        Me.ListModesBox.Items.AddRange(New Object() {"Add New tab", "Rename tab", "Delete tab"})
        Me.ListModesBox.Location = New System.Drawing.Point(4, 16)
        Me.ListModesBox.Name = "ListModesBox"
        Me.ListModesBox.Size = New System.Drawing.Size(140, 43)
        Me.ListModesBox.TabIndex = 0
        '
        'GroupControls
        '
        Me.GroupControls.Location = New System.Drawing.Point(4, 65)
        Me.GroupControls.Name = "GroupControls"
        Me.GroupControls.Size = New System.Drawing.Size(WTabsPanel - 8, 75)
        Me.GroupControls.TabIndex = 2
        Me.GroupControls.TabStop = False
        Me.GroupControls.Text = ""
        '
        'GroupMode
        '
        Me.GroupMode.Controls.Add(Me.ListModesBox)
        Me.GroupMode.Location = New System.Drawing.Point(4, 4)
        Me.GroupMode.MinimumSize = New System.Drawing.Size(0, 62)
        Me.GroupMode.Name = "GroupMode"
        Me.GroupMode.Size = New System.Drawing.Size(WTabsPanel - 8, 62)
        Me.GroupMode.TabIndex = 1
        Me.GroupMode.TabStop = False
        Me.GroupMode.Text = "Mode"
        '
        'GroupTree
        '
        Me.GroupTree.Controls.Add(Me.TreeView1)
        Me.GroupTree.Location = New System.Drawing.Point(4, 4)
        Me.GroupTree.Name = "GroupBox3"
        Me.GroupTree.Size = New System.Drawing.Size(WTabsPanel - 8, Me.Height - 220) '185
        Me.GroupTree.TabIndex = 1
        Me.GroupTree.TabStop = False
        Me.GroupTree.Text = "View"
        '
        'AcceptButtTab
        '
        Me.AcceptButtTab.Location = New System.Drawing.Point(4, 146)
        Me.AcceptButtTab.Name = "AcceptButtTab"
        Me.AcceptButtTab.Size = New System.Drawing.Size(WTabsPanel - 8, 23)
        Me.AcceptButtTab.TabIndex = 3
        Me.AcceptButtTab.Text = "Accept"
        Me.AcceptButtTab.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.Split1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Split1.ClientSize = New Size(WTabsPanel, Me.Height)
        Me.Split1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.Split1.Location = New System.Drawing.Point(0, 0)
        Me.Split1.Name = "Split1"
        Me.Split1.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.Split1.SplitterWidth = 1
        Me.Split1.Padding = New Padding(4)
        Me.Split1.IsSplitterFixed = True
        '
        'SplitContainer1.Panel1
        '
        Me.Split1.Panel1.Controls.Add(Me.AcceptButtTab)
        Me.Split1.Panel1.Controls.Add(Me.GroupMode)
        Me.Split1.Panel1.Controls.Add(Me.GroupControls)
        Me.Split1.Panel1.TabIndex = 0
        Me.Split1.Panel1.MinimumSize = New Size(WTabsPanel, 173)
        '
        'SplitContainer1.Panel2
        '
        Me.Split1.Panel2.Controls.Add(Me.GroupTree)
        Me.Split1.Size = New System.Drawing.Size(WTabsPanel, Me.Height - 173)
        Me.Split1.SplitterDistance = 173
        Me.Split1.Panel2.MinimumSize = New Size(WTabsPanel, 2)
        Me.Split1.Panel2.TabIndex = 2
        '
        'TabsManagerForm
        '
        Me.Controls.Add(Me.Split1)
        Me.Split1.Panel1.ResumeLayout(False)
        Me.Split1.Panel2.ResumeLayout(False)
        CType(Me.Split1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Split1.ResumeLayout(False)
        Me.GroupTree.ResumeLayout(False)
        Me.GroupMode.ResumeLayout(False)

    End Sub

    Private Sub RemoveTabsControls()
        Me.Controls.Remove(Me.Split1)
        Split1.Dispose()

    End Sub

    Private Mode As Integer

    Private Sub ListBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles ListModesBox.MouseClick
        If (Me.ListModesBox.GetSelected(0)) Then
            Mode = 1
            If (Me.GroupControls.Controls.Count > 0) Then Me.GroupControls.Controls.Clear()
            Me.GroupControls.Text = "Add New tab"
            Me.SetModeAddTabControls()

        ElseIf (Me.ListModesBox.GetSelected(1)) Then
            Mode = 2
            If (Me.GroupControls.Controls.Count > 0) Then Me.GroupControls.Controls.Clear()
            Me.GroupControls.Text = "Rename tab"
            Me.SetModeRenameTabControls()

        ElseIf (Me.ListModesBox.GetSelected(2)) Then
            Mode = 3
            If (Me.GroupControls.Controls.Count > 0) Then Me.GroupControls.Controls.Clear()
            Me.GroupControls.Text = "Delete tab"
            Me.SetModeDeleteTabControls()
        Else

        End If

    End Sub

    Friend WithEvents NameBox As System.Windows.Forms.TextBox
    Friend WithEvents TabsBox As System.Windows.Forms.ComboBox

    Private Sub SetModeAddTabControls()
        Me.GroupControls.Controls.Clear()
        NameBox = New System.Windows.Forms.TextBox()
        NameBox.Location = New System.Drawing.Point(6, 30)
        NameBox.Name = "NameBox"
        NameBox.Text = "Name"
        NameBox.Size = New System.Drawing.Size(140, 20)
        Me.GroupControls.Controls.Add(NameBox)
    End Sub

    Private Sub SetModeRenameTabControls()
        Me.GroupControls.Controls.Clear()

        NameBox = New System.Windows.Forms.TextBox()
        TabsBox = New System.Windows.Forms.ComboBox()

        TabsBox.FormattingEnabled = True
        For Each tab As TabPage In Me.TabControl1.TabPages
            If (tab IsNot Nothing) Then TabsBox.Items.Add(tab.Name)
        Next
        TabsBox.Location = New System.Drawing.Point(6, 19)
        TabsBox.Name = "TabsBox"
        TabsBox.Text = "Select tab"
        TabsBox.Size = New System.Drawing.Size(140, 21)
        TabsBox.TabIndex = 0

        NameBox.Location = New System.Drawing.Point(6, 46)
        NameBox.Name = "NameBox"
        NameBox.Text = "New name"
        NameBox.Size = New System.Drawing.Size(140, 20)
        NameBox.TabIndex = 1

        Me.GroupControls.Controls.Add(NameBox)
        Me.GroupControls.Controls.Add(TabsBox)
    End Sub

    Private Sub SetModeDeleteTabControls()
        Me.GroupControls.Controls.Clear()
        TabsBox = New System.Windows.Forms.ComboBox()
        TabsBox.FormattingEnabled = True
        For Each tab As TabPage In Me.TabControl1.TabPages
            TabsBox.Items.Add(tab.Name)
        Next
        TabsBox.Location = New System.Drawing.Point(6, 30)
        TabsBox.Name = "TabsBox"
        TabsBox.Text = "Select tab"
        TabsBox.Size = New System.Drawing.Size(140, 21)
        TabsBox.TabIndex = 0

        Me.GroupControls.Controls.Add(TabsBox)
    End Sub

    Public Sub RefreshTabsBox()
        TabsBox.Items.Clear()
        For Each tab As TabPage In Me.TabControl1.TabPages
            TabsBox.Items.Add(tab.Name)
        Next
        TabsBox.Text = ""
    End Sub

    Private Sub TextBox_RenameTab_Enter(sender As Object, e As EventArgs) Handles NameBox.Enter
        NameBox.Text = ""
    End Sub

    Private Sub TextBox_RenameTab_Leave(sender As Object, e As EventArgs) Handles NameBox.Leave
        If (NameBox.Text = Nothing) Then NameBox.Text = "New name"
    End Sub

    Private Sub Button1_MouseClick(sender As Object, e As MouseEventArgs) Handles AcceptButtTab.MouseClick

        Select Case Mode
            Case 1 'Add.
                Dim name As String = NameBox.Text
                If (name Is Nothing) Or (name = "Name") Then
                    MessageBox.Show("Type a name To add a New tab.")
                    Return
                End If
                Me.Caja.CreateNewTab(name)

            Case 2 'Rename.
                Dim index As Integer = TabsBox.SelectedIndex()
                If (index = -1) Then
                    MessageBox.Show("There Is no tab With this name.")
                    Return
                Else
                    'If (index < Me.TabControl1.TabPages.Count) Then
                    '    Me.TabControl1.TabPages.Item(index).Text = NameBox.Text
                    'End If
                    Dim name As String = NameBox.Text
                    Me.Caja.RenameTab(Me.TabControl1.TabPages.Item(index).Name, name)
                End If
            Case 3 'Delete.
                Dim index As Integer = TabsBox.SelectedIndex()
                If (index = -1) Then
                    MessageBox.Show("There Is no tab With this name.")
                    Return
                Else
                    'If (index < Me.TabControl1.TabPages.Count) Then
                    '    Dim page As TabPage = Me.TabControl1.TabPages.Item(index)
                    '    If (page IsNot Nothing) Then
                    '        Dim result As DialogResult = MessageBox.Show("Are you sure you want To delete the tab """ & page.Text & """ And all its content?", "", MessageBoxButtons.YesNo)
                    '        If (result = DialogResult.Yes) Then
                    '            Me.TabControl1.TabPages.Remove(page)
                    '            Me.SetModeDeleteTabControls()
                    '        End If
                    '    End If
                    'End If
                    Me.Caja.DeleteTab(Me.TabControl1.TabPages.Item(index).Name)
                End If
        End Select
        TabsToTree()

    End Sub

    Public Sub TabsToTree()

        If (Me.TreeView1.Nodes.Count > 0) Then Me.TreeView1.Nodes.Clear()

        'Si tiene alguna pestaña.
        If (Me.TabControl1.TabPages.Count > 0) Then
            'Por cada pestaña.
            For Each TAB As TabPage In Me.TabControl1.TabPages
                'Panel de pestaña.
                Dim panel As System.Windows.Forms.Panel = DirectCast(TAB.Controls(0), System.Windows.Forms.Panel)
                'Si panel de pestaña tiene pictureboxes.
                If (panel IsNot Nothing) AndAlso (panel.Controls.Count > 0) Then
                    Dim ramas As New List(Of System.Windows.Forms.TreeNode)
                    'Por cada picturebox
                    For i As Int32 = 0 To panel.Controls.Count - 1
                        Dim box As Brick = DirectCast(panel.Controls(i), Brick)
                        If (box IsNot Nothing) Then
                            Dim nodeChild As New TreeNode(box.Nombre)
                            nodeChild.Tag = 1
                            ramas.Add(nodeChild)
                        End If
                    Next
                    Dim NodeOrigen As New TreeNode(TAB.Text, ramas.ToArray())
                    NodeOrigen.Tag = 0
                    TreeView1.Nodes.Add(NodeOrigen)
                Else
                    Dim NodeOrigen As New TreeNode(TAB.Text)
                    NodeOrigen.Tag = 0
                    TreeView1.Nodes.Add(NodeOrigen)
                End If
            Next
            '  Me.TreeView1.ExpandAll()
        End If
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        If (CInt(e.Node.Tag) = 0) Then
            Me.TabControl1.SelectTab(e.Node.Text)
            Me.Caja.DeSelect()
        Else
            Dim tab As TabPage = Me.TabControl1.TabPages.Item(e.Node.Parent.Index)
            Me.Caja.DeSelect()
            Me.TabControl1.SelectTab(tab)

            For Each b As Brick In tab.Controls(0).Controls
                If (b.Nombre.Equals(e.Node.Text)) Then
                    Me.Caja.SelectMe(b)
                    Return
                    Exit For
                End If
            Next
        End If
    End Sub

#End Region

#Region "DragTabs"
    Private Function getHoverTabIndex() As Integer
        For i As Integer = 0 To TabControl1.TabPages.Count - 1
            If TabControl1.GetTabRect(i).Contains(TabControl1.PointToClient(Cursor.Position)) Then
                Return i
                Exit Function
            End If
        Next
        Return -1
    End Function

    Private Sub Tabs_MouseDown(Sender As Object, e As MouseEventArgs) Handles TabControl1.MouseDown
        If (e.Button = MouseButtons.Left) Then
            Dim keyboard As New Microsoft.VisualBasic.Devices.Keyboard
            If (keyboard.CtrlKeyDown) Then
                Dim index As Integer = getHoverTabIndex()
                If (index = -1) Then Exit Sub
                Dim tabToMove As TabPage = TabControl1.TabPages(index)
                TabControl1.DoDragDrop(tabToMove, DragDropEffects.Copy)
            End If
        End If
    End Sub

    Private Sub Tabs_DragEnter(sender As Object, e As DragEventArgs) Handles TabControl1.DragEnter
        e.Effect = DragDropEffects.All
    End Sub

    Private Sub Tabs_DragDrop(sender As Object, e As DragEventArgs) Handles TabControl1.DragDrop

        Dim replaceindex As Integer = getHoverTabIndex()
        If (replaceindex = -1) Then Exit Sub
        Dim tabToMove As TabPage = DirectCast(e.Data.GetData(GetType(TabPage)), TabPage)
        If (tabToMove Is Nothing) Then Exit Sub
        Dim tabToReplace As TabPage = TabControl1.TabPages(replaceindex)
        If (tabToMove.Parent.Equals(tabToReplace.Parent)) Then
            TabControl1.TabPages(tabToMove.TabIndex) = tabToReplace
            TabControl1.TabPages(replaceindex) = tabToMove
            tabToReplace.TabIndex = tabToMove.TabIndex
            tabToMove.TabIndex = replaceindex
        Else
            If (Caja.NombresPestañas.Contains(tabToMove.Text)) Then
                tabToMove.Text = tabToMove.Text & "*"
                tabToMove.Name = tabToMove.Name & "*"
                For Each b As Brick In tabToMove.Controls(0).Controls
                    b.Pestaña = tabToMove.Name
                Next
            End If
            For Each b As Brick In tabToMove.Controls(0).Controls
                If (Caja.NombresBloques.Contains(b.Nombre)) Then
                    b.Nombre = b.Nombre & "*"
                End If
                Caja.NombresBloques.Add(b.Nombre)
                b._form1 = Me
            Next
            Caja.NombresPestañas.Add(tabToMove.Name)
            tabToMove.TabIndex = TabControl1.TabCount
            TabControl1.TabPages.Add(tabToMove)
        End If
        TabControl1.SelectedTab = tabToMove
        TabControl1.Refresh()
        If (Convert.ToBoolean(ButtOpenTabs.Tag)) Then TabsToTree()
        Caja.HaCambiado = True
        Caja.Seleccionado = Nothing
    End Sub
#End Region

End Class

Public Class Brick
    Inherits System.Windows.Forms.PictureBox

    Public _form1 As Form1
    Public Nombre As String
    Public Pestaña As String
    Public Indice As IndexPair 'I = Index of tab, J = Index of brick.
    Public IMG As Bitmap
    Public Data As Byte()

#Region "New"

    Sub New(Parent As Form1, SuNombre As String, SuPestaña As String, Capture As CheckState)
        MyBase.New()
        _form1 = Parent
        Nombre = SuNombre
        Pestaña = SuPestaña
        Dim tab As TabPage = _form1.Caja.FindTab(SuPestaña)
        Indice = New IndexPair(tab.TabIndex, tab.Controls(0).Controls.Count)
        Data = CaptureData()
        If (Data Is Nothing) Then
            ' MessageBox.Show("Binary data could not be read.")
            Return
        End If
        IMG = CaptureImage(Capture)
        If (IMG Is Nothing) Then
            MessageBox.Show("Image could not be captured.")
        End If
        Me.PerformControl()

    End Sub

    Public Sub New(Parent As Form1, reader As GH_IReader)
        MyBase.New()
        _form1 = Parent
        Try
            Me.Pestaña = reader.GetString(NameOfTab, 0)
            Me.Indice.I = reader.GetInt32(IndexOfTab, 1)
            Me.Nombre = reader.GetString(NameOfBrick, 2)
            Me.Indice.J = reader.GetInt32(IndexOfBrick, 3)
            Me.Data = reader.GetByteArray(BinaryName, 4)
            Me.IMG = reader.GetDrawingBitmap(IMGName, 5)
        Catch ex0 As Exception
            Try
                Me.Pestaña = reader.GetString("Name Of tab", 0)
                Me.Indice.I = reader.GetInt32("Index Of tab", 1)
                Me.Nombre = reader.GetString("Name Of brick", 2)
                Me.Indice.J = reader.GetInt32("Index Of brick", 3)
                Me.Data = reader.GetByteArray(BinaryName, 4)
                Me.IMG = reader.GetDrawingBitmap(IMGName, 5)
            Catch ex As Exception
                MessageBox.Show("Could not read data from this file")
                Rhino.RhinoApp.WriteLine("Exception: " & ex.ToString())
            End Try
        End Try
        Me.PerformControl()
    End Sub

    Public Sub New(Parent As Form1, SuNombre As String, SuPestaña As String, SuIndice As IndexPair, SuIMG As Bitmap, SusDatos As Byte())
        MyBase.New()
        _form1 = Parent
        Nombre = SuNombre
        Pestaña = SuPestaña
        Indice = SuIndice
        IMG = SuIMG
        Data = SusDatos
        Me.PerformControl()
    End Sub

    Private Sub PerformControl()
        Me.Size = New Size(My.Settings.BrickSize, My.Settings.BrickSize)
        Me.Name = Nombre
        Me.Margin = New Padding(3)
        Me.SizeMode = PictureBoxSizeMode.Zoom
        Me.Image = IMG

        _form1.ToolTip1.SetToolTip(Me, Me.Nombre)
        AddHandler Me.MouseClick, AddressOf Me.SelectBrick
        AddHandler Me.MouseDown, AddressOf Me.DragBrick
    End Sub
#End Region

#Region "Capture"
    Private Function CaptureData()
        Dim IDs As New List(Of Guid)
        Dim _data As Byte() = Nothing

        For Each obj As IGH_DocumentObject In _form1.Doc.Objects()
            If (obj.Attributes.Selected) Then
                IDs.Add(obj.InstanceGuid)
            End If
        Next

        If (IDs.Count > 0) Then

            Dim DocIO As New GH_DocumentIO(_form1.Doc)
            DocIO.Copy(GH_ClipboardType.System, IDs)
            _data = System.Text.ASCIIEncoding.ASCII.GetBytes(System.Windows.Forms.Clipboard.GetText())
            System.Windows.Forms.Clipboard.Clear()
            _form1.Doc.ScheduleSolution(1)
        Else
            MessageBox.Show("No selected component.")
            _data = Nothing
        End If

        Return _data
    End Function

    Private Function CaptureImage(RhinoView As System.Windows.Forms.CheckState) As Bitmap

        _form1.Doc.DeselectAll()

        Dim image As Bitmap = Nothing
        If (RhinoView = CheckState.Checked) Then
            image = Rhino.RhinoDoc.ActiveDoc.Views.ActiveView.CaptureToBitmap(False, False, False)
        Else
            Dim acanvas As Grasshopper.GUI.Canvas.GH_Canvas = Grasshopper.Instances.ActiveCanvas
            image = acanvas.GetCanvasScreenBuffer(Grasshopper.GUI.Canvas.GH_CanvasMode.Export)
        End If

        Return image
    End Function

    Public Sub BrickToCanvas()
        If (Me Is Nothing) Then Return

        Dim Doc As GH_Document = _form1.Doc
        Dim xml As String = System.Text.ASCIIEncoding.ASCII.GetString(Me.Data)
        System.Windows.Forms.Clipboard.SetText(xml)

        Dim DocIO As New GH_DocumentIO(Doc)
        DocIO.Paste(GH_ClipboardType.System)
        System.Windows.Forms.Clipboard.Clear()

        Dim Mypivot As PointF = New System.Drawing.PointF(_form1.Comp.Attributes.Pivot.X + 75, _form1.Comp.Attributes.Pivot.Y + 75)
        Dim mayorX As Int32 = Int32.MaxValue
        Dim mayorY As Int32 = Int32.MaxValue
        For Each obj As IGH_DocumentObject In DocIO.Document.Objects()
            Dim pivot As PointF = obj.Attributes.Pivot
            If (pivot.X < mayorX) Then mayorX = pivot.X
            If (pivot.Y < mayorY) Then mayorY = pivot.Y
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

#Region "Sub"

    Public Sub SelectBrick(Sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        _form1.Caja.Seleccionado = Me
        If (e.Button = MouseButtons.Right) Then
            _form1.Caja.ShowMe(Me)
        End If
    End Sub

    Public Sub DeleteBrick()
        Dim bloque As Brick = _form1.Caja.FindBrick(Me.Nombre, Me.Pestaña)
        If (bloque Is Nothing) Then Exit Sub
        bloque.Dispose()
        _form1.Caja.Seleccionado = Nothing
        _form1.Caja.NombresBloques.RemoveAt(_form1.Caja.NombresBloques.IndexOf(bloque.Nombre))
        If (_form1.Caja.Display IsNot Nothing) Then _form1.Caja.HidePreview()
        If (Convert.ToBoolean(_form1.ButtOpenTabs.Tag)) Then _form1.TabsToTree()
        _form1.Caja.HaCambiado = True
    End Sub

    Public Function IsValid() As Boolean
        Return Me.Nombre IsNot Nothing AndAlso Me.Data IsNot Nothing AndAlso Me.IMG IsNot Nothing
    End Function

    Private Sub DragBrick(Sender As Object, e As MouseEventArgs)
        If (e.Button = MouseButtons.Left) Then
            Dim keyboard As New Microsoft.VisualBasic.Devices.Keyboard
            If (keyboard.CtrlKeyDown) Then
                Me.DoDragDrop(Sender, DragDropEffects.Copy)
            End If
        End If
    End Sub

#End Region

#Region "Serial"

    Private Const NameOfBrick As String = "Name of brick"
    Private Const NameOfTab As String = "Name of tab"
    Private Const IndexOfBrick As String = "Index of brick"
    Private Const IndexOfTab As String = "Index of tab"
    Private Const BinaryName As String = "Binary"
    Private Const IMGName As String = "IMG"

    Public Function Write(writer As GH_IWriter) As Boolean
        writer.SetString(NameOfTab, 0, Me.Pestaña)
        writer.SetInt32(IndexOfTab, 1, Me.Indice.I)
        writer.SetString(NameOfBrick, 2, Me.Nombre)
        writer.SetInt32(IndexOfBrick, 3, Me.Indice.J)
        writer.SetByteArray(BinaryName, 4, Me.Data)
        writer.SetDrawingBitmap(IMGName, 5, Me.IMG)
        Return True
    End Function

#End Region

End Class

Public Class BrickBox

    Public _Form1 As Form1
    Public File As String = My.Settings.FilePath
    Public NombresPestañas As New List(Of String)
    Public NombresBloques As New List(Of String)
    Private _Seleccionado As Brick
    Public Display As Preview
    Public HaCambiado As Boolean

    Sub New(Parent As Form1)
        _Form1 = Parent
        LoadFromFile(File)
        _Seleccionado = Nothing
        Display = Nothing
        HaCambiado = False
    End Sub

    Public Property Seleccionado As Brick
        Get
            Return _Seleccionado
        End Get
        Set(value As Brick)
            SelectMe(value)
        End Set
    End Property

#Region "BricksManager"

    Public Sub AddBrick(Bloque As Brick)

        If (NombresBloques.Contains(Bloque.Nombre)) Then
            'MessageBox.Show("Already exists a brick With name """ & Bloque.Nombre & """.", "")
            ' Exit Sub
            Bloque.Nombre = Bloque.Nombre & "*"

        End If
        Dim tab As TabPage = FindTab(Bloque.Pestaña)
        If (tab Is Nothing) Then
            MessageBox.Show("The brick """ & Bloque.Nombre & """ has a tab that does not exist in this box.")
            Exit Sub
        End If
        Bloque.Indice.J = tab.Controls(0).Controls.Count
        tab.Controls(0).Controls.Add(Bloque)
        NombresBloques.Add(Bloque.Nombre)
        If (Convert.ToBoolean(_Form1.ButtOpenTabs.Tag)) Then _Form1.TabsToTree()
        HaCambiado = True
    End Sub

    Public Sub ChangeTabOfBrick(Bloque As Brick, Pestaña As String)
        If (Bloque.Pestaña.Equals(Pestaña)) Then Exit Sub

        Dim tab As TabPage = FindTab(Pestaña)
        If (tab Is Nothing) Then
            MessageBox.Show("The tab """ & Pestaña & """ does not exist in this box.")
            Exit Sub
        End If
        Dim indexpair As New IndexPair(tab.TabIndex, tab.Controls(0).Controls.Count)
        Dim bloqueCopia As New Brick(_Form1, Bloque.Nombre, Pestaña, indexpair, Bloque.IMG, Bloque.Data)
        FindBrick(Bloque.Nombre, Bloque.Pestaña).Dispose()
        _Form1.TabControl1.SelectedTab = tab
        tab.Controls(0).Controls.Add(bloqueCopia)
        tab.Controls(0).Refresh()
        Seleccionado = bloqueCopia
        If (Convert.ToBoolean(_Form1.ButtOpenTabs.Tag)) Then _Form1.TabsToTree()
        HaCambiado = True
    End Sub

    Public Sub DeleteBrick()
        If (Seleccionado Is Nothing) Then Return
        If Not (NombresBloques.Contains(Seleccionado.Nombre)) Then
            MessageBox.Show("There is no brick with this name")
            Return
        End If
        If (Display IsNot Nothing) Then Display.Dispose()
        Seleccionado.DeleteBrick()
        Seleccionado = Nothing
        HaCambiado = True
    End Sub

    Private Sub VaciarCaja()

        Me.NombresBloques.Clear()
        Me.NombresPestañas.Clear()
        If (Seleccionado IsNot Nothing) Then
            If (Display IsNot Nothing) Then
                Display.Dispose()
                Display = Nothing
            End If
            Seleccionado.Dispose()
            Seleccionado = Nothing
        End If
        _Form1.TabControl1.TabPages.Clear()
        _Form1.Refresh()
    End Sub

    Public Sub SelectMe(Bloque As Brick)
        If (Bloque IsNot Nothing) Then
            DeSelect()
            Bloque.BorderStyle = BorderStyle.FixedSingle
            _Seleccionado = Bloque
            If (Display IsNot Nothing) Then Display.UpdateBrick(Bloque)
        Else
                _Seleccionado = Nothing
        End If
    End Sub

    Public Sub DeSelect()
        If (_Seleccionado IsNot Nothing) Then
            _Seleccionado.BorderStyle = BorderStyle.None
        End If
        _Seleccionado = Nothing

    End Sub

    Public Sub ShowMe(Bloque As Brick)
        If (Display Is Nothing) Then
            Display = New Preview(Bloque)
        Else
            Display.UpdateBrick(Bloque)
        End If
    End Sub

    Public Sub HidePreview()
        If (Display IsNot Nothing) Then
            Display.Dispose()
            Display = Nothing
        End If
    End Sub

    Private Sub DropBrick(sender As Object, e As DragEventArgs)

        Dim dragged As Brick = e.Data.GetData(GetType(Brick))
        If (dragged Is Nothing) Then
            Dim Files As String() = e.Data.GetData(DataFormats.FileDrop)
            If (Files IsNot Nothing) Then
                For Each file As String In Files
                    If (System.IO.Path.GetExtension(file).Equals(".gh")) Or (System.IO.Path.GetExtension(file).Equals(".ghx")) Then
                        AppendFile(file)
                    End If
                Next
            End If
            Exit Sub
        End If

        If (dragged._form1.Equals(Me._Form1)) Then
            Dim tab As TabPage = _Form1.TabControl1.SelectedTab

            For Each b As Brick In tab.Controls(0).Controls
                If (b.Bounds.Contains(tab.Controls(0).PointToClient(Cursor.Position))) AndAlso Not (dragged.Nombre.Equals(b.Nombre)) Then
                    Dim ind0 As Integer = dragged.Indice.J
                    Dim ind1 As Integer = b.Indice.J
                    If (ind0 < ind1) Then
                        tab.Controls(0).Controls.SetChildIndex(b, ind0)
                        tab.Controls(0).Controls.SetChildIndex(dragged, ind1)
                        b.Indice.J = ind0
                        dragged.Indice.J = ind1
                    Else
                        tab.Controls(0).Controls.SetChildIndex(dragged, ind1)
                        tab.Controls(0).Controls.SetChildIndex(b, ind0)
                        dragged.Indice.J = ind1
                        b.Indice.J = ind0
                    End If
                    tab.Refresh()
                    HaCambiado = True
                    Exit For
                End If
            Next

            Exit Sub
        End If
        If (NombresBloques.Contains(dragged.Nombre)) Then
            MessageBox.Show("This box allready contains a brick with this name.")
            Exit Sub
        End If
        Dim indexpair As New IndexPair(_Form1.TabControl1.SelectedTab.TabIndex, _Form1.TabControl1.SelectedTab.Controls(0).Controls.Count)
        Dim newbrick As New Brick(_Form1, dragged.Nombre, _Form1.TabControl1.SelectedTab.Name, indexpair, dragged.IMG, dragged.Data)
        Me.AddBrick(newbrick)

    End Sub

    Private Sub DropEnter(sender As Object, e As DragEventArgs)
        e.Effect = DragDropEffects.All
    End Sub

    Private Sub ResizeBricks(sender As Object, e As MouseEventArgs)
        Dim keyboard As New Microsoft.VisualBasic.Devices.Keyboard

        If (keyboard.ShiftKeyDown AndAlso e.Delta <> 0) Then
            For Each t As TabPage In _Form1.TabControl1.TabPages
                For Each b As Brick In t.Controls(0).Controls
                    Dim valor As Integer = Math.Max(10, b.Width + e.Delta * 0.05)
                    b.Size = New Size(valor, valor)
                    My.Settings.BrickSize = valor
                    Rhino.RhinoApp.WriteLine("BrickBox size =  " & valor & " px")
                Next
            Next
            _Form1.TabControl1.SelectedTab.Refresh()
            My.Settings.Save()
        End If

    End Sub
#End Region

#Region "TabsManager"

    Public Sub CreateNewTab(SuNombre As String)

        If (NombresPestañas.Contains(SuNombre)) Then
            MessageBox.Show("Already exists a tab with this name, try another")
            Return
        End If

        Dim Pestaña As New System.Windows.Forms.TabPage()

        Pestaña.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Pestaña.Name = SuNombre
        Pestaña.Text = SuNombre
        Pestaña.Padding = New System.Windows.Forms.Padding(3)
        Pestaña.TabIndex = _Form1.TabControl1.TabCount
        Pestaña.UseVisualStyleBackColor = True

        Dim panel As New System.Windows.Forms.FlowLayoutPanel()

        panel.AutoScroll = True
        panel.Dock = System.Windows.Forms.DockStyle.Fill
        panel.Location = New System.Drawing.Point(3, 3)
        panel.Name = "FlowLayoutPanel1"
        panel.Size = New System.Drawing.Size(284, 273)
        panel.TabIndex = 0
        panel.AllowDrop = True

        AddHandler panel.MouseClick, AddressOf Me.DeSelect
        AddHandler panel.MouseClick, AddressOf Me.HidePreview
        AddHandler panel.DragDrop, AddressOf Me.DropBrick
        AddHandler panel.DragEnter, AddressOf Me.DropEnter
        AddHandler panel.MouseWheel, AddressOf Me.ResizeBricks

        Pestaña.Controls.Add(panel)

        _Form1.TabControl1.TabPages.Add(Pestaña)
        Me.NombresPestañas.Add(SuNombre)
        HaCambiado = True
    End Sub

    Public Sub DeleteTab(SuNombre As String)

        If Not (NombresPestañas.Contains(SuNombre)) Then
            MessageBox.Show("There Is no tab with this name.")
            Return
        End If

        Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete the """ & SuNombre & """ tab and all its content?", "", MessageBoxButtons.YesNo)
        If (result = DialogResult.Yes) Then
            For Each tab As TabPage In _Form1.TabControl1.TabPages
                If (tab.Name.Equals(SuNombre)) Then
                    _Form1.TabControl1.TabPages.RemoveAt(tab.TabIndex)
                    Me.NombresPestañas.RemoveAt(Me.NombresPestañas.IndexOf(tab.Name))
                    tab.Dispose()
                    HaCambiado = True
                    Exit For
                End If
            Next
            _Form1.RefreshTabsBox()
        End If
    End Sub

    Public Sub RenameTab(SuNombre As String, NuevoNombre As String)

        If Not (NombresPestañas.Contains(SuNombre)) Then
            MessageBox.Show("There is no tab with this name.")
            Return
        End If
        If (NombresPestañas.Contains(NuevoNombre)) Then
            MessageBox.Show("Already exists a tab with this name, try another.")
            Return
        End If

        For Each tab As TabPage In _Form1.TabControl1.TabPages
            If (tab.Name.Equals(SuNombre)) Then
                _Form1.TabControl1.TabPages.Item(tab.TabIndex).Name = NuevoNombre
                _Form1.TabControl1.TabPages.Item(tab.TabIndex).Text = NuevoNombre
                Me.NombresPestañas(Me.NombresPestañas.IndexOf(tab.Name)) = NuevoNombre
                For Each b As Brick In tab.Controls(0).Controls
                    b.Pestaña = NuevoNombre
                Next
                HaCambiado = True
                Exit For
            End If
        Next
    End Sub


#End Region

#Region "Find"
    Public Function FindBrick(SuNombre As String, SuPestaña As String) As Brick
        Dim tab As TabPage = FindTab(SuPestaña)
        If (tab Is Nothing) Then
            MessageBox.Show("The brick """ & SuNombre & """ has a tab that does not exist in this box.")
            Return Nothing
            Exit Function
        End If
        Dim bloque As Brick = Nothing
        For Each b As Brick In tab.Controls(0).Controls
            If (b.Nombre.Equals(SuNombre)) Then
                bloque = b
                Exit For
            End If
        Next
        Return bloque
    End Function

    Public Function FindBrick(SuNombre As String) As Brick
        Dim bloque As Brick = Nothing
        Dim done As Boolean = False
        For Each tab As TabPage In _Form1.TabControl1.TabPages
            If (done) Then Exit For
            For Each b As Brick In tab.Controls(0).Controls
                If (b.Nombre.Equals(SuNombre)) Then
                    bloque = b
                    done = True
                    Exit For
                End If
            Next
        Next
        Return bloque
    End Function

    Public Function FindTab(SuNombre As String) As TabPage
        Dim thistab As TabPage = Nothing
        For Each tab As TabPage In _Form1.TabControl1.TabPages
            If (tab.Name.Equals(SuNombre)) Then
                thistab = tab
                Exit For
            End If
        Next
        Return thistab
    End Function
#End Region

#Region "Serial"
    Private Const NameBox As String = "BrickBox"

    Public Sub SaveToFile()

        If Not (HaCambiado) Then Return
        '0. Read file.
        Dim archive As New GH_Archive()
        Try
            archive.ReadFromFile(File)
        Catch ex As Exception
            MessageBox.Show("You need to load a file to save.")
            OpenNewFile()
            SaveToFile()
            Exit Sub
        End Try
        '1. Get root.
        Dim root As GH_Chunk = archive.GetRootNode()
        If (root Is Nothing) Then
            MessageBox.Show("File has no root")
            Return
        End If
        '2. Get/set BrickBox chunk.
        Dim RootBox As GH_Chunk = Nothing
        If (root.ChunkExists(NameBox)) Then
            root.RemoveChunk(NameBox)
            RootBox = root.CreateChunk(NameBox)
        Else
            RootBox = root.CreateChunk(NameBox)
        End If
        '3. Set tab chunk.
        For i As Int32 = 0 To _Form1.TabControl1.TabCount - 1
            Dim tab As TabPage = _Form1.TabControl1.TabPages.Item(i)
            Dim Chunktab As GH_Chunk = RootBox.CreateChunk(tab.Name)
            '4. Set brick chunk.
            Dim panel As FlowLayoutPanel = DirectCast(tab.Controls(0), FlowLayoutPanel)
            For j As Int32 = 0 To panel.Controls.Count - 1
                Dim b As Brick = DirectCast(panel.Controls(j), Brick)
                If (b Is Nothing) Then Continue For
                Dim chunkbrick As GH_Chunk = Chunktab.CreateChunk(b.Nombre)
                b.Write(chunkbrick)
            Next
        Next
        archive.WriteToFile(File, True, True)
        My.Settings.FilePath = File
        My.Settings.Save()
    End Sub

    Public Sub LoadFromFile(File As String)

        Me.VaciarCaja()

        '0. Read file.
        Dim archive As New GH_Archive()
        Try
            archive.ReadFromFile(File)
        Catch ex As Exception
            MessageBox.Show("Load a valid file.")
            OpenNewFile()
            Exit Sub
        End Try
        '1. Get root.
        Dim root As GH_Chunk = archive.GetRootNode()
        If (root Is Nothing) Then
            MessageBox.Show("File has no root")
            Return
        End If
        '2. Get BrickBox chunk.
        Dim RootBox As GH_Chunk = root.FindChunk(NameBox)
        If (RootBox Is Nothing) Then
            MessageBox.Show("The file is empty." & vbCrLf & "Begins creating a tab in order to add blocks, " & vbCrLf & "from the left side button.")
            Return
        End If
        '3. Get tab chunks.
        For i As Int32 = 0 To RootBox.ChunkCount - 1
            Dim ChunkTab As GH_Chunk = RootBox.Chunks(i)
            Me.CreateNewTab(ChunkTab.Name)
            '4. Get brick chunks.
            For j As Int32 = 0 To ChunkTab.ChunkCount - 1
                Dim chunkBrick As GH_Chunk = ChunkTab.Chunks(j)
                Dim b As New Brick(_Form1, chunkBrick)
                If (b.IsValid) Then Me.AddBrick(b)
            Next
        Next
        HaCambiado = False
    End Sub

    Public Sub AppendFile(File As String)
        '0. Read file.
        Dim archive As New GH_Archive()
        Try
            archive.ReadFromFile(File)
        Catch ex As Exception
            MessageBox.Show("This is not a valid file.")
        End Try
        '1. Get root.
        Dim root As GH_Chunk = archive.GetRootNode()
        If (root Is Nothing) Then
            MessageBox.Show("File has no root")
            Return
        End If
        '2. Get BrickBox chunk.
        Dim RootBox As GH_Chunk = root.FindChunk(NameBox)
        If (RootBox Is Nothing) Then
            MessageBox.Show("The file is empty, nothing to append.")
            Exit Sub
        End If
        '3. Get tab chunks.
        For i As Int32 = 0 To RootBox.ChunkCount - 1
            Dim ChunkTab As GH_Chunk = RootBox.Chunks(i)
            If Not (NombresPestañas.Contains(ChunkTab.Name)) Then Me.CreateNewTab(ChunkTab.Name)
            '4. Get brick chunks.
            For j As Int32 = 0 To ChunkTab.ChunkCount - 1
                Dim chunkBrick As GH_Chunk = ChunkTab.Chunks(j)
                Dim b As New Brick(_Form1, chunkBrick)
                If (NombresBloques.Contains(b.Nombre)) Then
                    b.Nombre = b.Nombre & "*"
                    b.Name = b.Name & "*"
                End If
                Me.AddBrick(b)
            Next
        Next
        HaCambiado = True
    End Sub

    Public Sub OpenNewFile()
        If (_Form1.OpenFileDialog1.ShowDialog() = DialogResult.OK) Then
            File = _Form1.OpenFileDialog1.FileName
            Try
                LoadFromFile(File)
            Catch ex As Exception
                MessageBox.Show("Something went wrong at load the file.")
                Rhino.RhinoApp.WriteLine(ex.ToString())
            End Try
            My.Settings.FilePath = File
            My.Settings.Save()
        End If
    End Sub
#End Region

End Class

#Region "OtherWindows"
Public Class Preview
    Inherits System.Windows.Forms.Form

    Private _Form1 As Form1
    Private ThisBrick As Brick

    Sub New(Bloque As Brick)
        _Form1 = Bloque._form1
        InitializeComponent()
        UpdateBrick(Bloque)
        Dim ratio As Double = Bloque.IMG.Width / Bloque.IMG.Height
        Me.Size = New System.Drawing.Size(_Form1.Height * ratio, _Form1.Height)
        Me.Location = New System.Drawing.Point(Bloque._form1.Location.X + Bloque._form1.Width, Bloque._form1.Location.Y)
        Me.Show()
    End Sub

#Region "Sub"

    Public Sub UpdateBrick(Bloque As Brick)
        ThisBrick = Bloque
        Me.TextBoxName.Text = Bloque.Nombre
        Me.ComboBoxTab.Items.Clear()
        For Each tab As String In _Form1.Caja.NombresPestañas
            Me.ComboBoxTab.Items.Add(tab)
        Next
        Me.ComboBoxTab.Text = ThisBrick.Pestaña
        Me.PictureBox1.Image = Bloque.IMG
        Me.PictureBox1.Refresh()
    End Sub

    Private Sub TextBoxName_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxName.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            If Not (Me.TextBoxName.Text.Equals(ThisBrick.Nombre)) Then
                Dim newname As String = Me.TextBoxName.Text
                Dim bloque As Brick = _Form1.Caja.FindBrick(ThisBrick.Nombre, ThisBrick.Pestaña)
                _Form1.Caja.NombresBloques(_Form1.Caja.NombresBloques.IndexOf(ThisBrick.Nombre)) = newname
                bloque.Nombre = newname
                _Form1.ToolTip1.SetToolTip(bloque, newname)
                bloque.Refresh()
                If (Convert.ToBoolean(_Form1.ButtOpenTabs.Tag)) Then _Form1.TabsToTree()
                _Form1.Caja.HaCambiado = True
            Else
                MessageBox.Show("Already exists a brick with this name.")
            End If
        End If
    End Sub

    Private Sub ComboBoxTab_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboBoxTab.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            Dim newtab As String = Me.ComboBoxTab.SelectedText
            If Not (Me.ComboBoxTab.SelectedText.Equals(ThisBrick.Pestaña)) AndAlso (_Form1.Caja.NombresPestañas.Contains(newtab)) Then
                Dim bloque As Brick = _Form1.Caja.FindBrick(ThisBrick.Nombre, ThisBrick.Pestaña)
                _Form1.Caja.ChangeTabOfBrick(ThisBrick, newtab)
            End If
        End If
    End Sub

    Private Sub ButtLoadPic_MouseClick(sender As Object, e As MouseEventArgs) Handles ButtLoadPic.MouseClick
        If (e.Button = MouseButtons.Left) Then
            Try
                If (Me.OpenFileDialog1.ShowDialog = DialogResult.OK) Then
                    Dim img As New Bitmap(Me.OpenFileDialog1.FileName)
                    Me.PictureBox1.Image = img
                    Me.ThisBrick.IMG = img
                    Dim bloque As Brick = _Form1.Caja.FindBrick(ThisBrick.Nombre, ThisBrick.Pestaña)
                    bloque.IMG = img
                    bloque.Image = img
                    bloque.Refresh()
                    _Form1.Caja.HaCambiado = True
                End If
            Catch ex As Exception
                MessageBox.Show("The selected file is not a valid image type.")
            End Try
        End If
    End Sub

    Private Sub Button1_MouseClick(sender As Object, e As MouseEventArgs) Handles Button1.MouseClick
        If (e.Button = MouseButtons.Left) Then
            Me.SaveFileDialog1.FileName = ThisBrick.Nombre & "_BrickImage"
            If (Me.SaveFileDialog1.ShowDialog = DialogResult.OK) Then
                Dim img As Bitmap = ThisBrick.Image
                img.Save(Me.SaveFileDialog1.FileName)
                MsgBox(Me.SaveFileDialog1.FileName)
            End If
        End If
    End Sub

    Private Sub Preview_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        ThisBrick._form1.Caja.Display = Nothing
    End Sub

    Private Sub BrickAtt_HelpButtonClicked(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.HelpButtonClicked
        MessageBox.Show("You can rename the brick typing in the text box a new name and press Enter." & vbCrLf &
"You can change the tab selecting a new tab in the tab box and press Enter." & vbCrLf &
"You can change the image by pressing the L button and loading a new image." & vbCrLf &
"You can save the current image of the brick by pressing the S button.", "Help")
        e.Cancel = True
    End Sub
#End Region

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
        Me.LabelName = New System.Windows.Forms.Label()
        Me.TextBoxName = New System.Windows.Forms.TextBox()
        Me.TabLabel = New System.Windows.Forms.Label()
        Me.ComboBoxTab = New System.Windows.Forms.ComboBox()
        Me.ButtLoadPic = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.SplitContainer4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        Me.SuspendLayout()
        '
        'LabelName
        '
        Me.LabelName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelName.Location = New System.Drawing.Point(0, 0)
        Me.LabelName.Name = "LabelName"
        Me.LabelName.Padding = New System.Windows.Forms.Padding(6, 0, 0, 8)
        Me.LabelName.Size = New System.Drawing.Size(51, 33)
        Me.LabelName.TabIndex = 0
        Me.LabelName.Text = "Name: "
        Me.LabelName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBoxName
        '
        Me.TextBoxName.BackColor = System.Drawing.SystemColors.Window
        Me.TextBoxName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxName.Dock = System.Windows.Forms.DockStyle.Top
        Me.TextBoxName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxName.Location = New System.Drawing.Point(3, 3)
        Me.TextBoxName.Name = "TextBoxName"
        Me.TextBoxName.Size = New System.Drawing.Size(162, 21)
        Me.TextBoxName.TabIndex = 0
        Me.TextBoxName.Text = ""
        Me.ToolTip1.SetToolTip(Me.TextBoxName, "Type a new name for this brick and press Enter")
        '
        'TabLabel
        '
        Me.TabLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabLabel.Location = New System.Drawing.Point(0, 0)
        Me.TabLabel.Name = "TabLabel"
        Me.TabLabel.Padding = New System.Windows.Forms.Padding(3, 0, 0, 8)
        Me.TabLabel.Size = New System.Drawing.Size(52, 33)
        Me.TabLabel.TabIndex = 0
        Me.TabLabel.Text = "Tab:"
        Me.TabLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ComboBoxTab
        '
        Me.ComboBoxTab.BackColor = System.Drawing.SystemColors.Window
        Me.ComboBoxTab.Dock = System.Windows.Forms.DockStyle.Top
        Me.ComboBoxTab.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxTab.FormattingEnabled = True
        Me.ComboBoxTab.Items.AddRange(New Object() {"sdfasdf", "asdf", "sdf"})
        Me.ComboBoxTab.Location = New System.Drawing.Point(0, 1)
        Me.ComboBoxTab.Name = "ComboBoxTab"
        Me.ComboBoxTab.Size = New System.Drawing.Size(151, 23)
        Me.ComboBoxTab.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.ComboBoxTab, "Change the tab of this brick and press Enter")
        '
        'ButtLoadPic
        '
        Me.ButtLoadPic.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtLoadPic.Location = New System.Drawing.Point(414, 295)
        Me.ButtLoadPic.MaximumSize = New System.Drawing.Size(20, 20)
        Me.ButtLoadPic.MinimumSize = New System.Drawing.Size(20, 20)
        Me.ButtLoadPic.Name = "ButtLoadPic"
        Me.ButtLoadPic.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtLoadPic.Size = New System.Drawing.Size(20, 20)
        Me.ButtLoadPic.TabIndex = 4
        Me.ButtLoadPic.TabStop = False
        Me.ButtLoadPic.Text = "L"
        Me.ToolTip1.SetToolTip(Me.ButtLoadPic, "Load a new image for this brick")
        Me.ButtLoadPic.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(388, 295)
        Me.Button1.MaximumSize = New System.Drawing.Size(20, 20)
        Me.Button1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(20, 20)
        Me.Button1.TabIndex = 3
        Me.Button1.TabStop = False
        Me.Button1.Text = "S"
        Me.ToolTip1.SetToolTip(Me.Button1, "Save current image")
        Me.Button1.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(440, 320)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = ""
        '
        'SaveFileDialog1
        '        '
        Me.SaveFileDialog1.OverwritePrompt = True
        Me.SaveFileDialog1.RestoreDirectory = True
        Me.SaveFileDialog1.Filter = "*.png|*.png|*.jpg|*.jpg|*.bmp|*.bmp|*.gif|*.gif|*.tiff|*.tiff"
        '     
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.SplitContainer1.SplitterWidth = 3
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ButtLoadPic)
        Me.SplitContainer1.Panel1.Controls.Add(Me.PictureBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(440, 357)
        Me.SplitContainer1.SplitterDistance = 320
        Me.SplitContainer1.TabIndex = 4
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.SplitContainer3)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer4)
        Me.SplitContainer2.Size = New System.Drawing.Size(440, 33)
        Me.SplitContainer2.SplitterDistance = 223
        Me.SplitContainer2.TabIndex = 0
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.IsSplitterFixed = True
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.LabelName)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.TextBoxName)
        Me.SplitContainer3.Panel2.Padding = New System.Windows.Forms.Padding(3, 3, 3, 6)
        Me.SplitContainer3.Size = New System.Drawing.Size(223, 33)
        Me.SplitContainer3.SplitterDistance = 51
        Me.SplitContainer3.TabIndex = 0
        '
        'SplitContainer4
        '
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.IsSplitterFixed = True
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer4.Name = "SplitContainer4"
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.TabLabel)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.ComboBoxTab)
        Me.SplitContainer4.Panel2.Padding = New System.Windows.Forms.Padding(0, 1, 6, 6)
        Me.SplitContainer4.Size = New System.Drawing.Size(213, 33)
        Me.SplitContainer4.SplitterDistance = 52
        Me.SplitContainer4.TabIndex = 0
        '
        'Preview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(440, 357)
        Me.MinimumSize = New System.Drawing.Size(200, 200)
        Me.StartPosition = FormStartPosition.Manual
        Me.Controls.Add(Me.SplitContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
        Me.Name = "BrickAtt"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = ""
        Me.HelpButton = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Owner = Grasshopper.Instances.DocumentEditor
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.Panel2.PerformLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.ResumeLayout(False)
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LabelName As Label
    Friend WithEvents TextBoxName As TextBox
    Friend WithEvents TabLabel As Label
    Friend WithEvents ComboBoxTab As ComboBox
    Friend WithEvents ButtLoadPic As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents SplitContainer3 As SplitContainer
    Friend WithEvents SplitContainer4 As SplitContainer
#End Region

End Class

Public Class MensajeSeleccion
    Inherits System.Windows.Forms.Form

    Public _Form1 As Form1
    Private Counter As New Integer

    Sub New(Parent As Form1)
        _Form1 = Parent
        InitializeComponent()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub Done_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim nombre As String = "No name"
        If (TextBox1.Text = Nothing) Or (TextBox1.Text = "") Or (TextBox1.Text = "Type a name") Then
            MessageBox.Show("Type a name please.")
            Return
        Else
            nombre = Me.TextBox1.Text

            If (_Form1.Caja.NombresBloques.Contains(nombre)) Then
                MessageBox.Show("The name """ & nombre & """ already exists, please choose another.")
                Return
            End If
        End If
        Timer1.Stop()
        Timer1.Dispose()
        Me.Close()
        Me.Dispose()

        Dim bloque As New Brick(_Form1, nombre, _Form1.TabControl1.SelectedTab.Name, Me.CheckBox1.CheckState)
        If (bloque.IsValid) Then _Form1.Caja.AddBrick(bloque)

    End Sub

#Region "Events"
    Private Sub TextBox1_Enter(sender As Object, e As EventArgs) Handles TextBox1.Enter
        Me.TextBox1.Text = ""
    End Sub

    Private Sub TextBox1_Leave(sender As Object, e As EventArgs) Handles TextBox1.Leave
        If (Me.TextBox1.Text = Nothing) Then Me.TextBox1.Text = "Type a name"
    End Sub

    Protected Overrides Sub OnHelpButtonClicked(e As System.ComponentModel.CancelEventArgs)
        Dim result As DialogResult = MessageBox.Show(
          "Select the group of components on the canvas that you want to store." & vbCrLf &
          "Make sure the image of the canvas satisfy you, will be its preview image." & vbCrLf &
          "Activate RhinoView to capture the viewport instead of canvas." & vbCrLf &
          "Type a name for this brick." & vbCrLf &
          "Click Done.")
        If (result = System.Windows.Forms.DialogResult.OK) Then
            e.Cancel = True
        End If

    End Sub

    Private Sub Mensaje_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        _Form1.MensajeForm = Nothing
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Dim puntos As String = ""
        If (Counter = 0) Then
            puntos = ""
            Counter += 1
        ElseIf (Counter = 1) Then
            puntos = "."
            Counter += 1
        ElseIf (Counter = 2) Then
            puntos = ".."
            Counter += 1
        Else
            puntos = "..."
            Counter = 0
        End If

        Me.Text = String.Format("{0}{1}", "Selecting", puntos)
    End Sub
#End Region

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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button1.Location = New System.Drawing.Point(144, 116)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Done"
        Me.Button1.UseVisualStyleBackColor = True
        Me.ToolTip1.SetToolTip(Me.Button1, "Bring components to the box")
        '
        'Label1
        '
        Me.Label1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
          Or System.Windows.Forms.AnchorStyles.Left) _
          Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(200, 20)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Select components on the canvas."
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Panel1.Controls.Add(Me.TextBox1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(12, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(190, 30)
        Me.Panel1.TabIndex = 2
        '
        'Timer1
        '
        Me.Timer1.Interval = 618
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(15, 120)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(77, 17)
        Me.CheckBox1.TabIndex = 3
        Me.CheckBox1.Text = "RhinoView"
        Me.CheckBox1.UseVisualStyleBackColor = True
        Me.ToolTip1.SetToolTip(Me.CheckBox1, "Capture the Rhino viewport instead of Grasshopper canvas")
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(12, 63)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(205, 20)
        Me.TextBox1.TabIndex = 0
        Me.TextBox1.Text = "Type a name"
        Me.ToolTip1.SetToolTip(Me.TextBox1, "Name for the new brick")
        '
        'Form1
        '
        Me.AcceptButton = Me.Button1
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(230, 150)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Button1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.HelpButton = True
        Me.Name = "MensajeAdd"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Location = New System.Drawing.Point(_Form1.Location.X + _Form1.Width / 2 - Me.Width / 2, _Form1.Location.Y + _Form1.Height / 2 - Me.Height / 2)
        Me.Text = "Selecting..."
        Me.Owner = Grasshopper.Instances.DocumentEditor
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents ToolTip1 As ToolTip
#End Region

End Class

Class ContactPopUp
    Inherits System.Windows.Forms.Form

    Sub New()
        InitializeComponent()
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
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(30, 32)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(0)
        Me.TextBox1.MinimumSize = New System.Drawing.Size(0, 30)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(131, 30)
        Me.TextBox1.TabIndex = 1
        Me.TextBox1.TabStop = False
        Me.TextBox1.Text = "dga_3@hotmail.com"
        '
        'TextBox2
        '
        Me.TextBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.Location = New System.Drawing.Point(105, 115)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(73, 13)
        Me.TextBox2.TabIndex = 3
        Me.TextBox2.Text = "- Daniel Abalde"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.Location = New System.Drawing.Point(39, 71)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(112, 16)
        Me.LinkLabel1.TabIndex = 2
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "GitHub repository"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(190, 140)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Form1"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Contact"
        Me.Owner = Grasshopper.Instances.DocumentEditor
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        System.Diagnostics.Process.Start(e.Link.LinkData.ToString())
    End Sub

    Private Sub ContactPopUp_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Dim link As New LinkLabel.Link()
        link.LinkData = "https://github.com/DanielAbalde/GhBrickBox"
        LinkLabel1.Links.Add(link)
    End Sub

End Class
#End Region

#Region "GH Component"
Public Class BrickBoxComp
    Inherits GH_Component

    Public IsOpen As Boolean

    Sub New()
        MyBase.New("Brick Box", "Brick Box", "Stores and retrieves bricks of grasshopper definitions", "Params", "Util")

    End Sub

    Protected Overrides ReadOnly Property Icon() As System.Drawing.Bitmap
        Get
            Return My.Resources.icon0
        End Get
    End Property

    Public Overrides ReadOnly Property ComponentGuid As Guid
        Get
            Return New Guid("{ec80e1fd-61e8-4762-8d04-95ee729ce409}")
        End Get
    End Property

    Public Overrides Sub CreateAttributes()
        m_attributes = New BrickBoxCompAtt(Me)
    End Sub

    Protected Overrides Sub RegisterInputParams(pManager As GH_InputParamManager)
    End Sub

    Protected Overrides Sub RegisterOutputParams(pManager As GH_OutputParamManager)
    End Sub

    Protected Overrides Sub SolveInstance(DA As IGH_DataAccess)
    End Sub

    Protected Overrides Sub AppendAdditionalComponentMenuItems(menu As ToolStripDropDown)
        MyBase.AppendAdditionalComponentMenuItems(menu)
        GH_DocumentObject.Menu_AppendItem(menu, "Contact", AddressOf Contact)
    End Sub

    Private PopUp As ContactPopUp

    Private Sub Contact(ByVal sender As Object, ByVal e As EventArgs)
        If (PopUp IsNot Nothing) Then PopUp.Dispose()
        PopUp = New ContactPopUp()
    End Sub

End Class

Public Class BrickBoxCompAtt
    Inherits GH_Attributes(Of BrickBoxComp)

    Public _Form1 As Form1

    Sub New(owner As BrickBoxComp)
        MyBase.New(owner)
        owner.IsOpen = False
    End Sub

    Public Overrides Function RespondToMouseDoubleClick(sender As GH_Canvas, e As GH_CanvasMouseEvent) As GH_ObjectResponse
        If e.Button = MouseButtons.Left AndAlso Me.Bounds.Contains(e.CanvasLocation) Then
            If Not (Owner.IsOpen) Then
                _Form1 = New Form1(MyBase.Owner)
                Owner.IsOpen = True
            Else
                If (_Form1 IsNot Nothing) Then
                    _Form1.Close()
                    _Form1.Dispose()
                End If
                Owner.IsOpen = False
            End If
            MyBase.Owner.ExpireSolution(True)
        End If
        Return MyBase.RespondToMouseDoubleClick(sender, e)
    End Function

#Region "Render"

    Protected Overrides Sub Layout()
        Me.Bounds = New RectangleF(Me.Pivot, New Size(60, 60))
    End Sub

    Protected Overrides Sub Render(canvas As GUI.Canvas.GH_Canvas, graphics As Graphics, channel As GUI.Canvas.GH_CanvasChannel)
        RenderImage(Owner.IsOpen, canvas, graphics)
    End Sub

    Protected Sub RenderImage(Open As Boolean, Canvas As GH_Canvas, graphics As System.Drawing.Graphics)
        Dim img As Bitmap = Nothing
        If (Open) Then
            If (Me.Selected) Then
                img = My.Resources.iconOpenSel
            Else
                img = My.Resources.iconOpenNoSel
            End If
        Else
            If (Me.Selected) Then
                img = My.Resources.iconClosedSel
            Else
                img = My.Resources.iconClosedNoSel
            End If
        End If

        Dim pt As New System.Drawing.Point(Pivot.X, Pivot.Y)
        graphics.DrawImage(img, pt)
    End Sub
#End Region

End Class
#End Region

