package servlet;

import java.io.IOException;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import dao.Data;

/**
 * Servlet implementation class regisservlet
 */
@WebServlet("/regisservlet")
public class regisservlet extends HttpServlet {
	private static final long serialVersionUID = 1L;

	/**
	 * @see HttpServlet#HttpServlet()
	 */
	public regisservlet() {
		super();
		// TODO Auto-generated constructor stub
	}

	/**
	 * @see HttpServlet#doGet(HttpServletRequest request, HttpServletResponse
	 *      response)
	 */
	protected void doGet(HttpServletRequest request,
			HttpServletResponse response) throws ServletException, IOException {
		// TODO Auto-generated method stub
	}

	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse
	 *      response)
	 */
	protected void doPost(HttpServletRequest request,
			HttpServletResponse response) throws ServletException, IOException {
		// TODO Auto-generated method stub
		String uname = request.getParameter("uname");
		String upass1 = request.getParameter("upass1");
		String upass2 = request.getParameter("upass2");
		Data data = new Data();
		int flag = data.regis(uname, upass1, upass2);
		if (flag == 1) {
			request.getSession().setAttribute("uname", uname);
			request.setAttribute("regissuccess", "true");
			request.getRequestDispatcher("index.jsp")
					.forward(request, response);
		} else {
			request.setAttribute("regiserror", "false");
			request.getRequestDispatcher("index.jsp")
					.forward(request, response);
		}
	}

}
