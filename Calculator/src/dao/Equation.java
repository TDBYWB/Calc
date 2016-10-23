package dao;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.LinkedList;
import java.util.Queue;
import java.util.Random;
import java.util.Stack;

public class Equation {

	public Equation() {
	}

	Random ran = new Random();
	HashMap<String, String> priorities = new HashMap<String, String>() {
		{
			put("#", "-1");
			put("+", "0");
			put("-", "0");
			put("*", "1");
			put("÷", "1");
		}
	};
	final String operators = "+-*÷=";
	final String space = " ";

	/*
	 * public void main(String args[]) throws Exception {
	 * System.out.println("请输入生成四则运算题的数目: "); Scanner sc = new
	 * Scanner(System.in); int numOfQue = sc.nextInt(); String[] ques = new
	 * String[numOfQue]; ArrayList que = new ArrayList(); for (int i = 0; i <
	 * numOfQue; i++) { que = produceQue(); ques[i] = (String) que.get(0);
	 * TreeNode s = suffixExpressionToTree(PreOrderToPostOrder(que));
	 * System.out.println(); System.out.println(ques[i] + operators.charAt(4) +
	 * space + Calculate(que));
	 * 
	 * }
	 * 
	 * }
	 */

	public ArrayList produceQue(int level) throws Exception {
		ArrayList str = new ArrayList();
		ArrayList isOperand = new ArrayList();
		int seed1, seed2, seed3 = 0;
		if (level == 1) {
			seed1 = 2;
			seed2 = 5;
			seed3 = 10;
		} else if (level == 2) {
			seed1 = 4;
			seed2 = 10;
			seed3 = 50;
		} else {
			seed1 = 8;
			seed2 = 50;
			seed3 = 100;
		}
		int count = ran.nextInt(seed1) + 1;
		int[] num = new int[count + 1];
		int[] den = new int[count + 1];
		String[] operand = new String[count + 1];
		int[] index = new int[count];
		int numOfBrackets = 0;
		if (count > 1)
			numOfBrackets = ran.nextInt(count);
		for (int i = 0; i < count + 1; i++) {
			num[i] = ran.nextInt(seed2) + 2;
			if (ran.nextInt(10) < 8)
				den[i] = 1;
			else {
				den[i] = ran.nextInt(seed3) + 1;
			}
			operand[i] = new Fraction(num[i], den[i]).ToString();
			if (i < count)
				index[i] = ran.nextInt(4);
		}
		ArrayList start = new ArrayList(numOfBrackets);
		ArrayList end = new ArrayList(numOfBrackets);
		for (int i = 0; i < numOfBrackets; i++) {
			start.add(ran.nextInt(count - 1) + 1);
			end.add(ran.nextInt(count - (int) start.get(i))
					+ (int) start.get(i) + 1);
		}
		for (int i = 0; i < numOfBrackets; i++)
			for (int j = 0; j < numOfBrackets; j++) {
				if (start.get(i).equals(end.get(j))) {
					start.set(i, -1);
					end.set(j, -1);
				}
			}
		for (int i = 0; i < count + 1; i++) {
			str.add(operand[i]);
			isOperand.add(i + 1);
			if (i < count) {
				str.add(operators.charAt(index[i]));
				isOperand.add(0);
			}
		}
		for (int i = 0; i < numOfBrackets; i++) {
			if ((int) start.get(i) != -1) {
				int left = isOperand.indexOf(start.get(i));
				str.add(left, "(");
				isOperand.add(left, 0);
			}
			if ((int) end.get(i) != -1) {
				int right = isOperand.indexOf(end.get(i)) + 1;
				str.add(right, ")");
				isOperand.add(right, 0);
			}
		}
		str.add(0, "");
		int j = 1;
		while (j < str.size()) {
			str.set(0, str.get(0).toString() + str.get(j).toString() + space);
			j = j + 1;
		}
		return str;
	}

	public boolean IsOperator(String op) {
		return operators.contains(op);
	}

	public boolean IsLeftAssoc(String op) {
		if (op.equals("+") || op.equals("-") || op.equals("*")
				|| op.equals("÷"))
			return true;
		else
			return false;
	}

	public String Compute(String leftNum, String rightNum, int op)
			throws Exception {
		Fraction result = new Fraction();
		switch (op) {
		case 0:
			return result.Add(leftNum, rightNum);
		case 1:
			rightNum = "-" + rightNum;
			return result.Add(leftNum, rightNum);
		case 2:
			return result.Multiply(leftNum, rightNum);
		case 3:
			rightNum = result.Inverse(new Fraction(rightNum)).ToString();
			return result.Multiply(leftNum, rightNum);
		}
		return null;
	}

	public Queue PreOrderToPostOrder(ArrayList expression) {
		Queue<String> result = new LinkedList<String>();
		Stack<String> operatorStack = new Stack<String>();
		operatorStack.push("#");
		String top, cur, tempChar, tempNum;
		for (int i = 1; i < expression.size(); i++) {
			cur = expression.get(i).toString();
			top = operatorStack.peek();
			if (cur == "(")
				operatorStack.push(cur);
			else {
				if (IsOperator(cur)) {
					while (IsOperator(top)
							&& ((IsLeftAssoc(cur) && priorities.get(cur)
									.compareTo(priorities.get(top)) <= 0) || (!IsLeftAssoc(cur) && priorities
									.get(cur).compareTo(priorities.get(top)) < 0))) {
						result.add(operatorStack.pop());
						top = operatorStack.peek();
					}
					operatorStack.push(cur);
				} else if (cur == ")") {
					while (operatorStack.size() > 0
							&& (tempChar = operatorStack.pop()) != "(") {
						result.add(tempChar);
					}
				} else {
					tempNum = cur;
					result.add(tempNum);
				}
			}
		}
		while (operatorStack.size() > 0) {
			cur = operatorStack.pop();
			if (cur == "#")
				continue;
			if (operatorStack.size() > 0)
				top = operatorStack.peek();
			result.add(cur);
		}
		return result;
	}

	public String Calculate(ArrayList expression) {
		Queue rpn = PreOrderToPostOrder(expression);
		Stack operandStack = new Stack<String>();
		try {
			String left, right, cur;
			while (rpn.size() > 0) {
				cur = (String) rpn.poll();
				int index = operators.indexOf(cur);
				if (index >= 0) {
					right = (String) operandStack.pop();
					left = (String) operandStack.pop();
					operandStack.push(Compute(left, right, index));
				} else {
					operandStack.push(cur);
				}
			}
		} catch (Exception e) {
			return null;
		}
		return (String) operandStack.pop();
	}

	public TreeNode suffixExpressionToTree(Queue suffixStr) {
		if (suffixStr.isEmpty())
			return null;
		// 用于临时存储节点的栈
		Object[] chs = suffixStr.toArray();
		Stack<TreeNode> stack = new Stack<TreeNode>();
		// 遍历所有字符，不是运算符的入栈，是运算符的，将栈中两个节点取出，合成一颗树然后入栈
		for (int i = 0; i < chs.length; i++) {
			if (IsOperator(chs[i].toString())) {
				if (stack.isEmpty() || stack.size() < 2) {
					System.err.println("输入的后缀表达式不正确");
					return null;
				}
				TreeNode root = new TreeNode(chs[i]);
				root.right = stack.pop();
				root.left = stack.pop();
				stack.push(root);
			} else {
				stack.push(new TreeNode(chs[i]));
			}
		}
		if (stack.isEmpty() || stack.size() > 1) {
			System.err.println("输入的后缀表达式不正确");
			return null;
		}
		// stack.pop().printAll();
		return stack.pop();

	}

	public boolean CompTree(TreeNode tree1, TreeNode tree2) {
		if (tree1 == null && tree2 == null)
			return true;

		if (tree1 != null && tree2 != null) {
			if (tree1.val.equals(tree2.val)) {
				if (tree1.val.equals("+") || tree1.val.equals("*")) {
					if (CompTree(tree1.left, tree2.left)
							&& CompTree(tree1.right, tree2.right)
							|| CompTree(tree1.right, tree2.left)
							&& CompTree(tree1.left, tree2.right)) {
						return true;
					}
				} else {
					if (CompTree(tree1.left, tree2.left)
							&& CompTree(tree1.right, tree2.right)) {
						return true;
					}
				}
			}
		}
		return false;
	}
}