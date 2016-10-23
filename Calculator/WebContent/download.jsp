<%@ page contentType="text/html;charset=utf-8"%>
<%@ page language="java"%>
<!DOCTYPE html>
<html class="no-js">
    
    <head>
        <title>下载</title>
        <!-- Bootstrap -->
        <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" media="screen">
        <link href="bootstrap/css/bootstrap-responsive.min.css" rel="stylesheet" media="screen">
        <link href="vendors/easypiechart/jquery.easy-pie-chart.css" rel="stylesheet" media="screen">
        <link href="assets/styles.css" rel="stylesheet" media="screen">
        
        <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
        <!--[if lt IE 9]>
            <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
        <![endif]-->
        <script src="vendors/modernizr-2.6.2-respond-1.1.0.min.js"></script>
         <%
  String download=(String)request.getAttribute("download");
  if(download != null){ %>
   <script>alert("题目文件文件生成成功！");</script>
  <%} %>
   <%
     HttpSession Session = request.getSession();
     String str = (String)Session.getAttribute("uname");
     if(str == null){ 
     response.sendRedirect("index.jsp");
     }%> 
   <script language="JavaScript">
   javascript:window.history.forward(1);
</script>
    </head>
    
     <body background="./images/4.jpg">
      <form name="dealform" action="dealservlet" method="post">
       <input type="hidden" name="status" value="0"/>
        <div class="navbar navbar-fixed-top">
            <div class="navbar-inner">
                <div class="container-fluid">
                    <a class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse"> <span class="icon-bar"></span>
                     <span class="icon-bar"></span>
                     <span class="icon-bar"></span>
                    </a>
                    <a class="brand" href="main.jsp">主页</a>
                    <div class="nav-collapse collapse">
                        <ul class="nav pull-right">
                            <li class="dropdown">
                                <a href="#" role="button" class="dropdown-toggle" data-toggle="dropdown"> <i class="icon-user"></i> ${uname} <i class="caret"></i>
                                </a>
                                <ul class="dropdown-menu">
                                    <li>
                                         <a href='javascript:document.dealform.submit();' onclick="javascript:document.dealform.status.value='4'">修改密码</a>
                                    </li>
                                    <li class="divider"></li>
                                    <li>
                                     <a href='javascript:document.dealform.submit();' onclick="javascript:document.dealform.status.value='5'">注销</a>
                                    </li>    
                                </ul>
                            </li>
                        </ul>
                   <ul class="nav">
                            <li class="dropdown">
                                <a data-toggle="dropdown" class="dropdown-toggle">进入练习<b class="caret"></b>
                                </a>
                             
                               <ul class="dropdown-menu" id="menu1">
                                    <li>
                                        <a href='javascript:document.dealform.submit();' onclick="javascript:document.dealform.status.value='1'">简单</a>
                                     </li>
                                     <li>
                                       <a href='javascript:document.dealform.submit();' onclick="javascript:document.dealform.status.value='2'">适中</a>
                                     </li>
                                     <li>
                                        <a href='javascript:document.dealform.submit();' onclick="javascript:document.dealform.status.value='3'">困难</a>
                                     </li>
                                </ul>
                             
                            </li>
                               <li>
                                 <a href='javascript:document.dealform.submit();' onclick="javascript:document.dealform.status.value='6'">生成题目文件</a>
                              
                                </li>
                                <li >
                                <a href='javascript:document.dealform.submit();' onclick="javascript:document.dealform.status.value='7'">查看历史记录</a>
                                                
                                </li>   
                        </ul>
                               </div>
                    <!--/.nav-collapse -->
                </div>
            </div>
        </div>
       </form>
        <div class="container-fluid">
            <div class="row-fluid">
                <div class="span5" id="sidebar">
                </div>
                <div class="span3" id="content">
                      <!-- morris stacked chart --
                        <!-- block -->
                        <div class="block">
                            <div class="navbar navbar-inner block-header">
                                <div class="muted pull-left">生成题目</div>
                            </div>
                            <div class="block-content collapse in">
                                     <form name="downloadform" action="downloadservlet" method="post" >
                                      <fieldset>
                                        <div class="control-group warning">
                                          <label class="control-label" >输入题目数量</label>
                                          <div class="controls">
                                            <input type="text" name="count">
                                          </div>
                                        </div>
                                        <div class="control-group success">
                                          <label class="control-label" for="selectError">选择题目难度</label>
                                          <div class="controls">
                                            <select name="level">
                                              <option value="1">简单</option>
                                              <option value="2">适中</option>
                                              <option value="3">困难</option>
                                            </select>
                                          </div>
                                        </div> 
                                        <div style="margin:0 auto;text-align:center;">
                                          <button type="submit" class="btn btn-primary">下载文件</button>
                                          <button type="reset" class="btn">重&nbsp;&nbsp;置</button>
                                        </div>                       
                                      </fieldset>
                                       
                                    </form>
                            </div>
                        </div>
                        <!-- /block -->
                    </div>
                </div>
            </div>
        <!--/.fluid-container-->
        <script src="vendors/jquery-1.9.1.min.js"></script>
        <script src="bootstrap/js/bootstrap.min.js"></script>
        <script src="vendors/easypiechart/jquery.easy-pie-chart.js"></script>
        <script src="assets/scripts.js"></script>
        <script>
        $(function() {
            // Easy pie charts
            $('.chart').easyPieChart({animate: 1000});
        });
        </script>
    </body>

</html>