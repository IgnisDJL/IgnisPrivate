Imports System.Xml
Imports System.IO

Namespace XmlSettings

    Public Class Settings

        Public Const XPATH_TO_NODE = "/settings"
        Public Const NODE_NAME = "settings"

        Private Const LAST_UPDATE_ATTRIBUTE As String = "lastUpdate"

        Public Const ACTIVE_ATTRIBUTE As String = "active"
        Public Const IS_ACTIVE As String = "yes"
        Public Const IS_NOT_ACTIVE As String = "no"
        Public Const TRUE_VALUE As String = "yes"
        Public Const FALSE_VALUE As String = "no"

        Private xmlDoc As New XmlDocument()

        ''' <summary>Provide's access to the report node's content and attributes</summary>
        Public Reports As ReportsNode

        ''' <summary>Provides access to the usine node's content and attributes</summary>
        Public Usine As UsineNode

        ' #todo Redo this definition
        ''' <summary>Contains the permissions of the admin</summary>
        Public Admin As AdminNode

        ' Singleton instance
        Public Shared ReadOnly instance As New Settings

        Private _node As Xml.XmlNode
        Public ReadOnly Property NODE As Xml.XmlNode
            Get
                Return Me._node
            End Get
        End Property


        Private Sub New()

            Dim settingsFile As New IO.FileInfo(Constants.Paths.SETTINGS_FILE)
            If (Not settingsFile.Exists) Then

                Throw New Exception("Fichier de paramètre introuvable.")
            End If

            Me.xmlDoc.Load(Constants.Paths.SETTINGS_FILE)

            Me._node = Me.xmlDoc.SelectSingleNode(XPATH_TO_NODE)

            If (IsNothing(Me._node)) Then
                Me._node = Settings.create(Me.xmlDoc)
            End If

            Me.Usine = New UsineNode(Me.NODE, xmlDoc.SelectSingleNode(UsineNode.XPATH_TO_NODE))

            Me.Reports = New ReportsNode(Me.NODE, xmlDoc.SelectSingleNode(ReportsNode.XPATH_TO_NODE))

            Me.Admin = New AdminNode(Me.NODE, xmlDoc.SelectSingleNode(AdminNode.XPATH_TO_NODE))

            Me.save()
        End Sub


        Public Function wasUpdated() As Boolean

            Dim hasBeenUpdated As Boolean = True

            Dim settingsFile As New System.IO.FileInfo(Constants.Paths.SETTINGS_FILE)

            Dim lastWriteTime = settingsFile.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss")

            hasBeenUpdated = Not Me.LAST_UPDATE.ToString("yyyy-MM-dd HH:mm:ss").Equals(lastWriteTime)

            ' If the file was updated, change the lastUpdate attribute to the right value.
            If (hasBeenUpdated) Then
                Me.LAST_UPDATE = DateTime.Now
            End If

            Return hasBeenUpdated

        End Function


        Private Property LAST_UPDATE As Date
            Get
                Return Date.Parse(Me.NODE.Attributes.GetNamedItem(LAST_UPDATE_ATTRIBUTE).Value)
            End Get
            Set(value As Date)
                Me.NODE.Attributes.GetNamedItem(LAST_UPDATE_ATTRIBUTE).Value = value.ToString("yyyy-MM-dd HH:mm:ss")
                Me.xmlDoc.Save(Constants.Paths.SETTINGS_FILE)
            End Set
        End Property

        Public Sub save()

            Me.xmlDoc.Save(Constants.Paths.SETTINGS_FILE)

            Console.WriteLine("XMLSettings: Settings were saved")

        End Sub

        Public Shared Property LANGUAGE As Language = Constants.Settings.LANGUAGES(0)

        Private Shared Function create(document As Xml.XmlDocument) As Xml.XmlNode

            Dim node = document.CreateElement(NODE_NAME)
            document.AppendChild(node)

            Return node
        End Function

    End Class

End Namespace
