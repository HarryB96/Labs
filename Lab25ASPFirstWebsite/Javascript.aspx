<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Javascript.aspx.cs" Inherits="Lab25ASPFirstWebsite.Javascript" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        var x = 0;
        function runSomeTestData() {
            x++;
            alert('The value of x is ' + x);
            var genius = confirm('Are you a computer geek');
            var name = prompt('Fine. Whats your name?');
            if (genius) {
                alert('Thanks, ' + name + ', I will come to you for advice');
            }
            else {
                alert('Thanks for your help')
            }
            console.log(genius);
            console.log(name);
        }
    </script>
    <button onclick="runSomeTestData()">Run Some Test Data</button>
    <div id ="test-data"></div>

</asp:Content>
