package dao;

public class TreeNode {

	Object val;
	TreeNode left;
	TreeNode right;

	public TreeNode(Object val) {
		this.val = val;
	}

	/**
	 * 打印指定深度的树下节点
	 * 
	 * @param d
	 */
	public void print(int d) {
		StringBuilder printStrBuilder = new StringBuilder("");
		for (int i = 0; i < d; i++) {
			printStrBuilder.append("^ ");
		}
		printStrBuilder.append(val);
		System.out.println(printStrBuilder);

		if (left != null) {
			left.print(d + 1);
		}
		if (right != null) {
			right.print(d + 1);
		}
	}

	/**
	 * 打印整棵树
	 */
	public void printAll() {
		print(0);
	}
}
