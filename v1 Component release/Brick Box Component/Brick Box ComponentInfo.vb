Imports Grasshopper.Kernel

Public Class BrickBoxComponentInfo
    Inherits GH_AssemblyInfo

    Public Overrides ReadOnly Property Name() As String
        Get
            Return "BrickBox"
        End Get
    End Property
    Public Overrides ReadOnly Property Icon As System.Drawing.Bitmap
        Get
            'Return a 24x24 pixel bitmap to represent this GHA library.
            Return Nothing
        End Get
    End Property
    Public Overrides ReadOnly Property Description As String
        Get
            'Return a short string describing the purpose of this GHA library.
            Return ""
        End Get
    End Property
    Public Overrides ReadOnly Property Id As System.Guid
        Get
            Return New System.Guid("ec80e1fd-61e8-4762-8d04-95ee729ce409")
        End Get
    End Property

    Public Overrides ReadOnly Property AuthorName As String
        Get
            'Return a string identifying you or your company.
            Return "Daniel Abalde"
        End Get
    End Property
    Public Overrides ReadOnly Property AuthorContact As String
        Get
            'Return a string representing your preferred contact details.
            Return "dga_3@hotmail.com"
        End Get
    End Property
End Class
