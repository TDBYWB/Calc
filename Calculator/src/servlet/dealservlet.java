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
 * Servlet implementation class dealservlet
 */
@WebServlet("/dealservlet")
public class dealservlet extends HttpServlet {
	private static final long serialVersionUID = 1L;

	/**
	 * @see HttpServlet#HttpServlet()
	 */
	public dealservlet() {
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
		String status = request.getParameter("status");
		HttpSession session = request.getSession();
		String uname = (String) session.getAttribute("uname");
		if (status.compareTo("5") == 0) {
			session.removeAttribute("uname");
			response.sendRedirect("index.jsp");
		} else if (status.compareTo("4") == 0) {
			response.sendRedirect("updatepass.jsp");
		} else if (status.compareTo("6") == 0) {
			response.sendRedirect("download.jsp");
		} else if (status.compareTo("7") == 0) {
			response.sendRedirect("history.jsp");
		} else {
			Data data = new Data();
			int flag = data.produceExam(uname, Integer.parseInt(status));
			if (flag == 1) {
				request.getRequestDispatcher("table.jsp").forward(request,
						response);
			}
		}
	}

}
