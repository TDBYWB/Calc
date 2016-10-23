package servlet;

import java.io.IOException;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import dao.Data;

/**
 * Servlet implementation class upservlet
 */
@WebServlet("/upservlet")
public class upservlet extends HttpServlet {
	private static final long serialVersionUID = 1L;

	/**
	 * @see HttpServlet#HttpServlet()
	 */
	public upservlet() {
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
		String uname = (String) request.getSession().getAttribute("uname");
		HttpSession session = request.getSession();
		String oripass = request.getParameter("oripass");
		String newpass1 = request.getParameter("newpass1");
		String newpass2 = request.getParameter("newpass2");
		Data data = new Data();
		int flag = data.updatePass(uname, oripass, newpass1);
		if (flag == 1) {
			session.removeAttribute("uname");
			request.setAttribute("success", "success");
			request.getRequestDispatcher("updatepass.jsp").forward(request,
					response);
		} else {
			if (oripass.length() <= 0 || newpass1.length() <= 0
					|| newpass2.length() <= 0)
				request.setAttribute("uperror", "nullup");
			else if (!newpass1.equals(newpass2))
				request.setAttribute("uperror", "difnewup");
			else
				request.setAttribute("uperror", "diforiup");
			request.getRequestDispatcher("updatepass.jsp").forward(request,
					response);
		}
	}

}
