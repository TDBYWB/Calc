<%@ page language="java" contentType="text/html; charset=UTF-8"%>
<%@ page language="java"%>
<%@ page import="java.sql.*" %>
<%@ page import="java.util.*" %>
<%@ page import="dao.Data" %>
<!DOCTYPE html>
<html>
    <head>
        <title>答题</title>
        <!-- Bootstrap -->
        <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" media="screen">
        <link href="bootstrap/css/bootstrap-responsive.min.css" rel="stylesheet" media="screen">
        <link href="assets/styles.css" rel="stylesheet" media="screen">
        <link href="assets/DT_bootstrap.css" rel="stylesheet" media="screen">
        
        <!--[if lte IE 8]><script language="javascript" type="text/javascript" src="vendors/flot/excanvas.min.js"></script><![endif]-->
        <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
        <!--[if lt IE 9]>
            <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
        <![endif]-->
        <script src="vendors/modernizr-2.6.2-respond-1.1.0.min.js"></script>
 <script type="text/javascript">
        function display(clock){
            var now=new Date();   //创建Date对象
            var year=now.getFullYear(); //获取年份
            var month=now.getMonth(); //获取月份
            var date=now.getDate();  //获取日期
            var day=now.getDay();  //获取星期
            var hour=now.getHours(); //获取小时
            var minu=now.getMinutes(); //获取分钟
            var sec=now.getSeconds(); //获取秒钟
            month=month+1;
            var arr_week=new Array("星期日","星期一","星期二","星期三","星期四","星期五","星期六");
            var week=arr_week[day];  //获取中文的星期
            var time=year+"年"+month+"月"+date+"日 "+week+" "+hour+":"+minu+":"+sec; 
          // var str = new Date().Format("yyyy-MM-dd hh:mm:ss"); 
            clock.innerHTML="当前时间："+time; //显示系统时间
           
        }
        window.onload=function(){
            window.setInterval("display(clock)", 1000);
        }
        </script> 
    </head>
     <%
     HttpSession Session = request.getSession();
     String str = (String)Session.getAttribute("uname");
     if(str == null){ 
     response.sendRedirect("index.jsp");
     }%> 
   <script language="JavaScript">
   javascript:window.history.forward(1);
</script>
    <body background="./images/3.jpg">
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
                        <!-- block -->


                         
                            <div class="block-content collapse in">
                                <div class="span12">
                                     <div class="muted pull-left" id = "clock"></div>
       <form name="answerform" action="answerservlet" method="post">
      <%
        PreparedStatement pr = null;
  	    ResultSet re = null;
  	    Connection conn = (Connection) new Data().getcon();
		
  	    String sql = "select id from exam where id=(select max(id) from exam)";
  	    pr  = (PreparedStatement) conn.prepareStatement(sql);
  	    re = pr.executeQuery();
  	    re.next();
  	    String id = re.getString(1);
  		sql="select q.qid,q.equation from que q where q.uname=? && q.id = ?";
  		pr  = (PreparedStatement) conn.prepareStatement(sql);
  		String uname = (String)session.getAttribute("uname");
  		pr.setString(1, uname);
  		pr.setString(2, id);
  	    re = pr.executeQuery();
  	    String qid,equation;
  	    int num = 1; %>
		<table class="table table-striped table-bordered" width="200">
        <tbody>
  	   <% while(re.next()){
               qid = re.getString(1);
               equation = re.getString(2);
               if(num % 2 == 1){%>
						  <tr class="success">
					 <%}%>
						          <td width="100">
						          <%=equation %>&nbsp;=&nbsp;<input type="text" name="ans" style="width:30px" />
						          </td>
						          <% if(num % 2 == 0){%>
						               </tr>
						                 <%}%>    
						           <%num++;}
  	                                    pr.close();
  	                                    re.close();
  	                                   conn.close();%>
						              </tbody>
						            </table>
						            <center>
						          <input type="hidden" name="id" value=<%=id %> />
						         <button class="btn btn-primary"  onclick="return confirm('您确定要提交吗？');">提交</button>
						         </center>
						            </form>
                                </div>
                            </div>
                    
                    </div>
        </div>
        <!--/.fluid-container-->

        <script src="vendors/jquery-1.9.1.js"></script>
        <script src="bootstrap/js/bootstrap.min.js"></script>
        <script src="vendors/datatables/js/jquery.dataTables.min.js"></script>
        <script src="assets/scripts.js"></script>
        <script src="assets/DT_bootstrap.js"></script>
        <script>
        </script>
    </body>

</html>>