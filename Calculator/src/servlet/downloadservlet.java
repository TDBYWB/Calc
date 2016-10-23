package servlet;

import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import dao.Data;

/**
 * Servlet implementation class downloadservlet
 */
@WebServlet("/downloadservlet")
public class downloadservlet extends HttpServlet {
	private static final long serialVersionUID = 1L;

	/**
	 * @see HttpServlet#HttpServlet()
	 */
	public downloadservlet() {
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
		String count = request.getParameter("count");
		int level = Integer.parseInt(request.getParameter("level"));
		Data data = new Data();
		int flag = data.produceFile(count, level);
		if (flag == 0) {
			String filename = "equation.txt";
			response.setContentType(getServletContext().getMimeType(filename));
			// 设置Content-Disposition
			response.setHeader("Content-Disposition", "attachment;filename="
					+ filename);
			// 读取目标文件，通过response将目标文件写到客户端
			// 获取目标文件的绝对路径
			String fullFileName = "D:\\workspace\\Calculator\\src\\equation.txt";
			// 读取文件
			InputStream in = new FileInputStream(fullFileName);
			OutputStream out = response.getOutputStream();

			// 写文件
			int b;
			while ((b = in.read()) != -1) {
				out.write(b);
			}
			in.close();
			out.close();
			response.setStatus(HttpServletResponse.SC_MOVED_PERMANENTLY);
			String newLocn = "download.jsp";
			response.setHeader("Location", newLocn);
		} else {
			request.setAttribute("download", "false");
			request.getRequestDispatcher("index.jsp")
					.forward(request, response);
		}
	}

}
