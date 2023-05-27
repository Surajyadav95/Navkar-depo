<%@ Page Title="Resource Management | Item List" Language="VB" EnableEventValidation="false" MasterPageFile="~/Depo/Popup.master" AutoEventWireup="false" CodeFile="VesselList.aspx.vb" Inherits="Account_ItemList" Culture="en-GB"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" >
        function callparentfunction()
        {
            window.opener.document.getElementById("ContentPlaceHolder1_btnIndentItem1").click();
            self.close();
        }
    </script>
    <style type="text/css">
        .scrolling-table-container {
            height: 400px;
            overflow-y: auto;
            overflow-x: auto;
        }
</style>
       <div class="container" style="background-color:white">
           
             <div class="panel-body" >
                            <div class="form-group">
       <asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
      <div class="row">
                             <div class="col-sm-4  col-xs-6">
                                        <div class="form-group">
                                          <%--  <label class="control-label">
                                              Fax No.</label>--%>
                                          Search Text
                                               <asp:TextBox ID="txtItemCode" placeholder="Enter Vessel Name" Style="text-transform: uppercase;" class="form-control form-cascade-control"
                                                runat="server" ></asp:TextBox>
                                                
                                        </div>
                                    </div>
                             <div class="col-sm-2  col-xs-6">
                                        <div class="form-group" style="padding-top:23px">
                                           
                                            <asp:Button ID="btnOk" class="btn btn-primary btn-sm modal-submit"  OnClick="btnAdd_Click" 
                                                runat="server" Text="Search" />
                                            <asp:Label ID="Label1" Visible="false" runat="server" Text="0"></asp:Label>
                                        </div>
                                    </div>
                        </div>
                            

     <div class="row">
                                    <div class="col-lg-12 ">
                                     <div class="table-responsive  scrolling-table-container">
                                           
                                        <asp:GridView ID="grdVesselList"  AutoGenerateColumns="false"
                                            runat="server"  CssClass="table table-striped table-bordered table-hover" EmptyDataText="No records found!">

                                            <Columns>
                                                 <asp:TemplateField>
                                                    <ItemTemplate>
                                                         <asp:CheckBox runat="server" ID="chkitem" Checked='<%#Eval("CheckItem")%>' />
                                                        <asp:Label runat="server" ID="lblcode" Text='<%#Eval("VesselID")%>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="VesselName" HeaderStyle-Height="30" HeaderText="Vessel Name"></asp:BoundField>
                                               
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

          <div class="col-sm-2 col-xs-6">
             <div class="form-group">
                 <asp:Button ID="btnchoose" class="btn btn-primary btn-sm modal-submit" OnClick="OpenList"
                                                runat="server" Text="Choose" />
             </div>
         </div>
                                     </div>   
        </div>
                 </div>
          </div>
   <%-- <script type = "text/javascript">
        function SetTarget() {
            document.forms[0].target = "_blank";
           
        }
</script>--%>
    </asp:Content>


