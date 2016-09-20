Imports Grasshopper.Kernel

Public Class BrickBox2_AddOnInfo
    Inherits GH_AssemblyInfo

    Public Overrides ReadOnly Property Name() As String
        Get
            Return "BrickBox2AddOn"
        End Get
    End Property
    Public Overrides ReadOnly Property Icon As System.Drawing.Bitmap
        Get
            Return Nothing
        End Get
    End Property
    Public Overrides ReadOnly Property Description As String
        Get
            Return ""
        End Get
    End Property
    Public Overrides ReadOnly Property Id As System.Guid
        Get
            Return New System.Guid("a27f9433-99da-4666-b71c-db9af2915281")
        End Get
    End Property

    Public Overrides ReadOnly Property AuthorName As String
        Get
            Return "Daniel Abalde"
        End Get
    End Property
    Public Overrides ReadOnly Property AuthorContact As String
        Get
            Return "dga_3@hotmail.com"
        End Get
    End Property
End Class
