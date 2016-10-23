package dao;

import java.io.File;
import java.io.FileWriter;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;

public class Data {
	private Connection con = null;
	private PreparedStatement pr = null;
	private ResultSet re = null;

	public Data() {
	}

	public Connection getcon() {
		try {
			Class.forName("com.mysql.jdbc.Driver");
			DriverManager.registerDriver(new com.mysql.jdbc.Driver());
			String url = "jdbc:mysql://localhost:3306/Calc";
			con = (Connection) DriverManager.getConnection(url, "root",
					"594188");
		} catch (Exception e) {
			e.printStackTrace();
		}
		return con;
	}

	public int login(String uname, String upass) {
		Connection con = getcon();
		try {
			pr = con.prepareStatement("select uname,upass from users where uname=?&&upass=?");
			pr.setString(1, uname);
			pr.setString(2, upass);
			re = pr.executeQuery();
			if (re.next())
				return 1;
			pr.close();
			re.close();
			con.close();
		} catch (SQLException e) {
			return 0;
		}
		return 0;
	}

	public int produceQue(String uname, int id, int level) {
		Connection con = getcon();
		int flag = 0;
		try {
			ArrayList arr = new ArrayList();
			Equation equ = new Equation();
			arr = equ.produceQue(level);
			pr = con.prepareStatement("insert into que(uname,id,equation,correctans) values(?,?,?,?)");
			pr.setString(1, uname);
			pr.setInt(2, id);
			pr.setString(3, (String) arr.get(0));
			pr.setString(4, equ.Calculate(arr));
			flag = pr.executeUpdate();
			pr.close();
			con.close();
		} catch (Exception e) {
			return flag;
		}
		return flag;
	}

	public int produceExam(String uname, int level) {
		Connection con = getcon();
		int resu = 0;
		int id, flag;
		try {
			pr = con.prepareStatement("insert into exam(uname) values(?)");
			pr.setString(1, uname);
			flag = pr.executeUpdate();
			pr = con.prepareStatement("select id,flag from exam where uname=?");
			pr.setString(1, uname);
			re = pr.executeQuery();
			while (re.next()) {
				id = re.getInt(1);
				flag = re.getInt(2);
				if (flag == 0) {
					for (int i = 0; i < 10; i++) {
						if (produceQue(uname, id, level) == 0)
							i--;
					}
					pr = con.prepareStatement("update exam set flag=? where id=?");
					pr.setInt(1, 1);
					pr.setInt(2, id);
					resu = pr.executeUpdate();
				}
			}
			pr.close();
			con.close();
		} catch (Exception e) {
			return resu;
		}
		return resu;
	}

	public int updatePass(String name, String oldpassword, String newpassword) {
		Connection con = getcon();
		int flag = 0;
		try {
			pr = con.prepareStatement("update users set upass=? where uname=?&&upass=?");
			pr.setString(1, newpassword);
			pr.setString(2, name);
			pr.setString(3, oldpassword);
			flag = pr.executeUpdate();
			pr.close();
			con.close();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return flag;
	}

	public int regis(String uname, String upass1, String upass2) {
		Connection con = getcon();
		int flag = 0;
		if (upass1.compareTo(upass2) != 0)
			return flag;
		try {
			pr = con.prepareStatement("insert into users(uname,upass) values(?,?)");
			pr.setString(1, uname);
			pr.setString(2, upass1);
			flag = pr.executeUpdate();
			pr.close();
			con.close();
		} catch (SQLException e) {
			return flag;
		}
		return flag;
	}

	public int setStime(String date, String id) {
		Connection con = getcon();
		int flag = 0;
		try {
			pr = con.prepareStatement("update exam set stime=? where id=?");
			pr.setString(1, date);
			pr.setInt(2, Integer.parseInt(id));
			flag = pr.executeUpdate();
			pr.close();
			con.close();
		} catch (Exception e) {
			return flag;
		}
		return flag;
	}

	public int setEtime(String date, String id) {
		Connection con = getcon();
		int flag = 0;
		try {
			pr = con.prepareStatement("update exam set etime=?,flag=? where id=?");
			pr.setString(1, date);
			pr.setInt(2, 2);
			pr.setInt(3, Integer.parseInt(id));
			flag = pr.executeUpdate();
			pr.close();
			con.close();
		} catch (Exception e) {
			return flag;
		}
		return flag;
	}

	public int setAns(String[] ans, String id) {
		Connection con = getcon();
		int flag = 0;
		try {
			int qid = 0;
			int num = 0;
			/*
			 * System.out.println(id + "fdasfa"); for (int i = 0; i <
			 * ans.length; i++) System.out.println(ans[i] + "aaaaa");
			 */
			pr = con.prepareStatement("select qid from que where id=?");
			pr.setInt(1, Integer.parseInt(id));
			re = pr.executeQuery();
			while (re.next()) {
				qid = re.getInt(1);
				PreparedStatement pre = con
						.prepareStatement("update que set inputans=? where qid=?");
				pre.setString(1, ans[num]);
				pre.setInt(2, qid);
				flag = pre.executeUpdate();
				pre.close();
				num++;
			}
			pr.close();
			re.close();
			con.close();
		} catch (Exception e) {
			return flag;
		}
		return flag;
	}

	public int produceFile(String count, int level) {
		int flag = 0;
		try {
			ArrayList arr = new ArrayList();
			Equation equ = new Equation();

			File file = new File("../workspace/Calculator/src/equation.txt");

			if (!file.exists()) {
				file.createNewFile();
			}
			FileWriter out = new FileWriter(file);
			for (int i = 0; i < Integer.parseInt(count); i++) {
				arr = equ.produceQue(level);
				try {
					equ.Calculate(arr);
				} catch (Exception e) {
					i--;
				}
				String s = (String) arr.get(0) + " = ";
				if (level == 1) {
					s = String.format("%-20s", s);
				} else if (level == 2) {
					s = String.format("%-40s", s);
				} else {
					s = String.format("%-60s", s);
				}
				out.write(s);
				if (i % 5 == 4)
					out.write("\r\n");
			}
			out.close();

		} catch (Exception e) {
			flag = -1;
		}
		return flag;
	}

}
