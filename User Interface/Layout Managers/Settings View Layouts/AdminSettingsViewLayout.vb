Namespace UI

    Public Class AdminSettingsViewLayout
        Inherits LayoutManager

        Public Shared ReadOnly MINIMUM_SIZE As Size = New Size(0, 0)
        Public Shared ReadOnly CONDENSED_SIZE As Size = New Size(0, 0)

        ' Components Attributes

        ' Permissions Label
        Private _permissionsLabel_location As Point
        Private _permissionsLabel_size As Size

        ' Permissions Panel
        Private _permissionsPanel_location As Point
        Private _permissionsPanel_size As Size

        ' Can Open Data Files Check Box
        Private _canOpenDataFilesCheckBox_location As Point
        Private _canOpenDataFilesCheckBox_size As Size

        ' Can Modify Delay Codes Check Box
        Private _canModifyDelayCodesCheckBox_location As Point
        Private _canModifyDelayCodesCheckBox_size As Size

        ' Can Change Email Settings Check Box
        Private _canChangeEmailSettingsCheckBox_location As Point
        Private _canChangeEmailSettingsCheckBox_size As Size

        ' Can Reset Database Check Box
        Private _canResetDatabaseCheckBox_location As Point
        Private _canResetDatabaseCheckBox_size As Size


        ' Attributes
        Public Sub New()
            MyBase.New(MINIMUM_SIZE, CONDENSED_SIZE)

        End Sub

        Protected Overloads Overrides Sub computeLayout()

            ' Permissions Label
            Me._permissionsLabel_location = New Point(LOCATION_START_X, LOCATION_START_Y)
            Me._permissionsLabel_size = New Size(Me.Width - 2 * LOCATION_START_X, FIELDS_HEIGHT)

            ' Permissions Panel
            Me._permissionsPanel_location = New Point(LOCATION_START_X, Me.PermissionsLabel_Location.Y + Me.PermissionsLabel_Size.Height)
            Me._permissionsPanel_size = New Size(Me.Width - 2 * LOCATION_START_X, 4 * FIELDS_HEIGHT + 2 * SPACE_BETWEEN_CONTROLS_Y)

            Dim permissionCheckboxSize As New Size(Me.PermissionsPanel_Size.Width - 2 * SPACE_BETWEEN_CONTROLS_X, FIELDS_HEIGHT)

            ' Can Open Data Files Check Box (Inside permissions panel)
            Me._canOpenDataFilesCheckBox_location = New Point(SPACE_BETWEEN_CONTROLS_X, SPACE_BETWEEN_CONTROLS_Y)
            Me._canOpenDataFilesCheckBox_size = permissionCheckboxSize

            ' Can Modify Delay Codes Check Box (Inside permissions panel)
            Me._canModifyDelayCodesCheckBox_location = New Point(SPACE_BETWEEN_CONTROLS_X, Me.CanOpenDataFilesCheckBox_Location.Y + Me.CanOpenDataFilesCheckBox_Size.Height)
            Me._canModifyDelayCodesCheckBox_size = permissionCheckboxSize

            ' Can Change Email Settings Check Box (Inside permissions panel)
            Me._canChangeEmailSettingsCheckBox_location = New Point(SPACE_BETWEEN_CONTROLS_X, Me.CanModifyDelayCodesCheckBox_Location.Y + Me.CanModifyDelayCodesCheckBox_Size.Height)
            Me._canChangeEmailSettingsCheckBox_size = permissionCheckboxSize

            ' Can Reset Database Check Box (Inside permissions panel)
            Me._canResetDatabaseCheckBox_location = New Point(SPACE_BETWEEN_CONTROLS_X, Me.CanChangeEmailSettingsCheckBox_Location.Y + Me.CanChangeEmailSettingsCheckBox_Size.Height)
            Me._canResetDatabaseCheckBox_size = permissionCheckboxSize

        End Sub

        ' 
        ' Permissions Label
        ' 
        Public ReadOnly Property PermissionsLabel_Location As Point
            Get
                Return Me._permissionsLabel_location
            End Get
        End Property
        Public ReadOnly Property PermissionsLabel_Size As Size
            Get
                Return Me._permissionsLabel_size
            End Get
        End Property
        ' 
        ' Permissions Panel
        ' 
        Public ReadOnly Property PermissionsPanel_Location As Point
            Get
                Return Me._permissionsPanel_location
            End Get
        End Property
        Public ReadOnly Property PermissionsPanel_Size As Size
            Get
                Return Me._permissionsPanel_size
            End Get
        End Property
        ' 
        ' Can Open Data Files Check Box (Inside permissions panel)
        ' 
        Public ReadOnly Property CanOpenDataFilesCheckBox_Location As Point
            Get
                Return Me._canOpenDataFilesCheckBox_location
            End Get
        End Property
        Public ReadOnly Property CanOpenDataFilesCheckBox_Size As Size
            Get
                Return Me._canOpenDataFilesCheckBox_size
            End Get
        End Property
        ' 
        ' Can Modify Delay Codes Check Box (Inside permissions panel)
        ' 
        Public ReadOnly Property CanModifyDelayCodesCheckBox_Location As Point
            Get
                Return Me._canModifyDelayCodesCheckBox_location
            End Get
        End Property
        Public ReadOnly Property CanModifyDelayCodesCheckBox_Size As Size
            Get
                Return Me._canModifyDelayCodesCheckBox_size
            End Get
        End Property
        ' 
        ' Can Change Email Settings Check Box (Inside permissions panel)
        ' 
        Public ReadOnly Property CanChangeEmailSettingsCheckBox_Location As Point
            Get
                Return Me._canChangeEmailSettingsCheckBox_location
            End Get
        End Property
        Public ReadOnly Property CanChangeEmailSettingsCheckBox_Size As Size
            Get
                Return Me._canChangeEmailSettingsCheckBox_size
            End Get
        End Property
        ' 
        ' Can Reset Database Check Box (Inside permissions panel)
        ' 
        Public ReadOnly Property CanResetDatabaseCheckBox_Location As Point
            Get
                Return Me._canResetDatabaseCheckBox_location
            End Get
        End Property
        Public ReadOnly Property CanResetDatabaseCheckBox_Size As Size
            Get
                Return Me._canResetDatabaseCheckBox_size
            End Get
        End Property

    End Class
End Namespace
