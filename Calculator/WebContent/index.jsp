<%@ page contentType="text/html;charset=utf-8"%>
<%@ page language="java"%>


<!DOCTYPE HTML>
<html><head>
<title>登录注册</title> 
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<script type="text/javascript" src="js/jquery-1.9.0.min.js"></script>
<script type="text/javascript" src="images/login.js"></script>
<link rel="stylesheet" href="css/login2.css"  type="text/css" />
</head>

<script type="text/javascript">
function CheckLoginForm()
{
  if (document.loginform.uname.value==""){
	alert("请输入用户名！");
	document.loginform.uname.focus();
	return false;
  }
  if (document.loginform.upass.value==""){
	alert("请输入密码！");
	document.loginform.upass.focus();
	return false;
  }
}

function CheckRegisForm()
{
  if (document.regisform.uname.value==""){
	alert("请输入用户名！");
	document.regisform.uname.focus();
	return false;
  }
  if (document.regisform.upass1.value==""){
	alert("请输入密码！");
	document.regisform.upass1.focus();
	return false;
  }
  if (document.regisform.upass2.value==""){
	alert("请输入密码！");
	document.regisform.upass2.focus();
	return false;
  }
  if (document.regisform.upass1.value != document.regisform.upass2.value){
		alert("两次密码不一致！");
		document.regisform.upass1.focus();
		return false;
	  }
}
</script>

<%
  String error1=(String)request.getAttribute("loginerror");
  String error2=(String)request.getAttribute("regiserror");
  String success=(String)request.getAttribute("regissuccess");
  if(error1!=null){ %>
   <script>alert("用户名或密码错误");</script>
  <%} 
  if(error2!=null){ %>
  <script>alert("用户名已存在");
    document.regisform.uname.focus();
  </script>
 <%} 
  if(success!=null){ %>
  <script>alert("注册成功，请登录");</script>
 <%}
%>

<body>
 <script language="JavaScript">
   javascript:window.history.forward(1);
</script>
<h1>小学生四则运算系统</h1>
<div class="login">
    
    <div class="header">
        <div class="switch" id="switch"><a class="switch_btn_focus" id="switch_qlogin" href="javascript:void(0);" tabindex="7">快速登录</a>
			<a class="switch_btn" id="switch_login" href="javascript:void(0);" tabindex="8">快速注册</a><div class="switch_bottom" id="switch_bottom" style="position: absolute; width: 64px; left: 0px;"></div>
        </div>
    </div>    
  
    
    <div class="web_qr_login" id="web_qr_login" style="display: block; height: 235px;">    

            <!--登录-->
            <div class="web_login" id="web_login">
               
               
               <div class="login-box">
    
            
			<div class="login_form">
			<form name="loginform" action="loginservlet" method="post" onsubmit="return CheckLoginForm()">
                <div class="uinArea" id="uinArea">
                <label class="input-tips" for="u">帐号：</label>
                <div class="inputOuter" id="uArea">
                    
                    <input type="text" id="u" name="uname" class="inputstyle"/>
                </div>
                </div>
                <div class="pwdArea" id="pwdArea">
               <label class="input-tips" for="p">密码：</label> 
               <div class="inputOuter" id="pArea">
                    
                    <input type="password" id="p" name="upass" class="inputstyle"/>
                </div>
                </div>
               
                <div style="padding-left:50px;margin-top:20px;"><input type="submit" value="登 录" style="width:150px;" class="button_blue"/></div>
              </form>
           </div>
           
            	</div>
               
            </div>
            <!--登录end-->
  </div>

  <!--注册-->
    <div class="qlogin" id="qlogin" style="display: none; ">
   
    <div class="web_login"><form name="regisform" action="regisservlet" method="post" onsubmit="return CheckRegisForm()">
        <ul class="reg_form" id="reg-ul">
                <li>
                	
                    <label for="user"  class="input-tips2">用户名：</label>
                    <div class="inputOuter2">
                        <input type="text" id="user" name="uname" maxlength="16" class="inputstyle2"/>
                    </div>
                    
                </li>
                
                <li>
                <label for="passwd" class="input-tips2">密码：</label>
                    <div class="inputOuter2">
                        <input type="password" id="passwd"  name="upass1" maxlength="16" class="inputstyle2"/>
                    </div>
                    
                </li>
                <li>
                <label for="passwd2" class="input-tips2">确认密码：</label>
                    <div class="inputOuter2">
                        <input type="password" id="passwd" name="upass2" maxlength="16" class="inputstyle2" />
                    </div>
                    
                </li>
                
                <li>
                     <div style="padding-left:100px;margin-top:20px;"><input type="submit" value="注册" style="width:150px;" class="button_blue"/></div>
                </li>
            </ul></form>
    </div>
    </div>
    <!--注册end-->
</div>
</body></html>