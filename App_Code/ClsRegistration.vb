Imports Microsoft.VisualBasic
Imports System.Data

Public Class ClsRegistration

    Public Sub countryList(ByVal DropDownCountryList As DropDownList)
        Dim obgclsdbOperation As New dbOperation

        'Dim sringConnection As String = ConfigurationManager.ConnectionStrings("SqlConnStringEMP").ConnectionString
        'Dim sringConnection1 As String = "Data Source=WELCOME-PC;Initial Catalog=EMP_PORTAL;Integrated Security=True;"
        'Dim con As New SqlConnection(sringConnection1)
        'con.Open()
        'Dim ds As New DataSet
        'Dim db As New SqlCommand()
        'db.Connection = conn
        'db.CommandType = CommandType.StoredProcedure

        Dim dt As DataTable = obgclsdbOperation.sub_GetDatatable("USP_getCountryList")


        DropDownCountryList.DataSource = dt.DefaultView
        DropDownCountryList.DataTextField = "CountryName"
        DropDownCountryList.DataValueField = "CountryID"
        DropDownCountryList.DataBind()

        
    End Sub

    Public Sub fillStateUsingCountryId(ByVal countryid As Integer, ByVal DropDownStateList As DropDownList)
        Dim obgclsdbOperation As New dbOperation


        Dim dt As DataTable = obgclsdbOperation.sub_GetDatatable("exec USP_GetStateList_CountryWise '" & Val(countryid) & "'")


        DropDownStateList.DataSource = dt.DefaultView
        DropDownStateList.DataTextField = "StateName"
        DropDownStateList.DataValueField = "StateID"
        DropDownStateList.DataBind()

    End Sub

    Public Sub CompanyList(ByVal DropDownCompanyList As DropDownList)
        Dim obgclsdbOperation As New dbOperation

        Dim dt As DataTable = obgclsdbOperation.sub_GetDatatable("USP_get_CompanyDetails")


        DropDownCompanyList.DataSource = dt.DefaultView
        DropDownCompanyList.DataTextField = "company_name"
        DropDownCompanyList.DataValueField = "company_id"
        DropDownCompanyList.DataBind()

    End Sub

    Public Sub DepartmentList(ByVal DropDownDepartmentList As DropDownList)
        Dim obgclsdbOperation As New dbOperation

        Dim dt As DataTable = obgclsdbOperation.sub_GetDatatable("USP_get_DepartmentList")


        DropDownDepartmentList.DataSource = dt.DefaultView
        DropDownDepartmentList.DataTextField = "department_name"
        DropDownDepartmentList.DataValueField = "department_id"
        DropDownDepartmentList.DataBind()

    End Sub


    Public Sub fillDesignationUsingDeptId(ByVal depatment_id As Integer, ByVal DropDownStateList As DropDownList)
        Dim obgclsdbOperation As New dbOperation


        Dim dt As DataTable = obgclsdbOperation.sub_GetDatatable("exec USP_get_DesignationList '" & Val(depatment_id) & "'")


        DropDownStateList.DataSource = dt.DefaultView
        DropDownStateList.DataTextField = "designation_name"
        DropDownStateList.DataValueField = "designation_id"
        DropDownStateList.DataBind()

    End Sub


    Public Sub UserTypeList(ByVal DropDownUserTypeList As DropDownList)
        Dim obgclsdbOperation As New dbOperation


        Dim dt As DataTable = obgclsdbOperation.sub_GetDatatable("USP_get_userTypeList")


        DropDownUserTypeList.DataSource = dt.DefaultView
        DropDownUserTypeList.DataTextField = "user_type"
        DropDownUserTypeList.DataValueField = "user_type_id"
        DropDownUserTypeList.DataBind()


    End Sub
End Class
