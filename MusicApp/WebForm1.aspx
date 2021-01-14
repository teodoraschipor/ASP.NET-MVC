<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadAudio.cs" Inherits="MusicApp.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload Audio</title>  
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">  
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">  
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>  
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>  
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.16/css/dataTables.bootstrap4.min.css" />  
    <script src="https://cdn.datatables.net/1.10.16/js/jquery.dataTables.min.js" type="text/javascript"></script>  
    <script src="https://cdn.datatables.net/1.10.16/js/dataTables.bootstrap4.min.js" type="text/javascript"></script>  
    <script type="text/javascript">  
        $(document).ready(function () {  
            $("#GridView1").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();  
        });  
    </script>  
</head>
<body>
   <form id="form1" runat="server">  
        <div class="container py-3">  
            <h2 class="text-center text-uppercase">How to upload audio in database and play using asp.net</h2>  
            <div class="card" style="margin-bottom: 10px;">  
                <div class="card-header bg-primary text-white">  
                    <h5>Upload Audio</h5>  
                </div>  
                <div class="card-body">  
                    <button type="button" class="btn btn-primary btn-sm rounded-0" data-toggle="modal" data-target="#myAudio">  
                        <i class="fa fa-plus-circle"></i>Add New  
                    </button>  
                    <div class="modal fade" id="myAudio">  
                        <div class="modal-dialog">  
                            <div class="modal-content">  
                                <div class="modal-header">  
                                    <h4 class="modal-title">Add New Audio</h4>  
                                    <button type="button" class="close" data-dismiss="modal">×</button>  
                                </div>  
                                <div class="modal-body">  
                                    <div class="row">  
                                        <div class="col-md-12">  
                                            <div class="form-group">  
                                                <label>Choose Audio:</label>  
                                                <div class="input-group">  
                                                    <div class="custom-file">  
                                                        <asp:FileUpload ID="FileUpload1" CssClass="custom-file-input" runat="server" />  
                                                        <label class="custom-file-label"></label>  
                                                    </div>  
                                                    <div class="input-group-append">  
                                                        <asp:Button ID="btnUpload" CssClass="btn btn-outline-secondary" runat="server" Text="Upload" OnClick="btnUpload_Click" />  
                                                    </div>  
                                                </div>  
                                                <asp:Label ID="lblMessage" runat="server"></asp:Label>  
                                            </div>  
                                        </div>  
                                    </div>  
                                </div>  
                                <div class="modal-footer">  
                                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>  
                                </div>  
                            </div>  
                        </div>  
                    </div>  
                </div>  
                <asp:GridView ID="GridView1" ShowHeaderWhenEmpty="true" HeaderStyle-CssClass="bg-primary text-white" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered">  
                    <EmptyDataTemplate>  
                        <div class="text-center">No Data Found <strong>Upload New Audio</strong></div>  
                    </EmptyDataTemplate>  
                    <Columns>  
                        <asp:BoundField HeaderText="ID" DataField="ID" />  
                        <asp:BoundField HeaderText="Name" DataField="Name" />  
                        <asp:BoundField HeaderText="Audio" DataField="Audio_Path" />  
                        <asp:TemplateField HeaderText="Play">  
                            <ItemTemplate>  
                                <audio controls>  
                                    <source src='<%#Eval("Audio_Path")%>' type="audio/ogg">  
                                </audio>  
                            </ItemTemplate>  
                        </asp:TemplateField>  
                    </Columns>  
                </asp:GridView>  
            </div>  
        </div>  
    </form>  

</body>
</html>
