from fractions import Fraction
import random
input0 = 1
operators = "+-*/="
space = " "
priorities = {"#": -1, "+": 0, "-": 0,"*":1,"/":1}
def produceQue():
    strque = []
    isOperand = []
    count = random.randint(1, 2)
    num = []
    den = []
    operand = []
    index = []
    numOfBrackets = 0
    for i in range(count + 1):
        num.append(random.randint(2, 4))
        if random.randint(1, 10) < 8:
            den.append(1)
        else:
            den.append(random.randint(1, 4))
            numOfBrackets = random.randint(1, count)
        operand.append(Fraction(num[i], den[i]))
        if i < count:
            index.append(random.randint(0, 3))
    start = []
    end = []
    for i in range(numOfBrackets):
        start.append(random.randint(1, count))
        end.append(random.randint(start[i]+1, count + 1))
    for i in range(len(start)):
        for j in range(len(end)):
            if start[i] == end[j]:
                start.pop(i)
                end.pop(j)
                start.append(-1)
                end.append(-1)
    j = 1
    for i in range(count + 1):
        strque.append(str(operand[i]))
        isOperand.append(i + 1)
        if i < count:
            strque.append(operators[index[i]])
            isOperand.append(0)
    for i in range(numOfBrackets):
        if start[i] != -1:
          left = isOperand.index(start[i])
          strque.insert(left, "(")
          isOperand.insert(left, -1)
        if end[i] != -1:
          right = isOperand.index(end[i])
          strque.insert(right+1, ")")
          isOperand.insert(right+1, -1)
    strque.insert(0, "")
    j = 1
    while j < len(strque):
        strque[0] += strque[j] + space
        j = j + 1
    return strque

def Compute(leftNum,rightNum,op):
    if op == 0:
        return Fraction(leftNum)+Fraction(rightNum)
    if op == 1:
        return Fraction(leftNum)-Fraction(rightNum)
    if op == 2:
        return Fraction(leftNum)*Fraction(rightNum)
    if op == 3:
        return Fraction(leftNum)/Fraction(rightNum)

def IsOperator(op):
    try:
        i = operators.index(str(op))
        bo = True
    except:
        bo = False
    finally:
        return bo

def IsLeftAssoc(op):
    if op == "+" or op == "-" or op == "*" or op == "/":
        return True
    else:
        return False


def PreOrderToPostOrder(expression):
    result = []
    operatorStack = []
    operatorStack.append("#")
    top = ""
    cur = ""
    tempChar = ""
    tempNum = ""
    i = 1
    while i < len(expression):
        cur = expression[i]
        top = operatorStack[-1]
        if cur == "(":
            operatorStack.append(cur)
        else:
            if(IsOperator(cur)):
                while IsOperator(top) and (IsLeftAssoc(cur) and priorities[cur] <= priorities[top]) or (not IsLeftAssoc(cur) and priorities[cur] < priorities[top]):
                   result.append(operatorStack.pop())
                   top = operatorStack[-1]
                operatorStack.append(cur)
            elif cur == ")":
                tempChar = operatorStack.pop()
                while len(operatorStack) > 0 and   tempChar != "(":
                    result.append(tempChar)
                    tempChar = operatorStack.pop()
            else:
                tempNum = cur
                result.append(tempNum)
        i = i + 1

    while len(operatorStack) > 0:
        cur = operatorStack.pop()
        if cur == "#":
            continue;
        if len(operatorStack) > 0:
            top = operatorStack[-1]
        result.append(cur)
    return result

def Calculate(expression):
    rpn = PreOrderToPostOrder(expression)
    operandStack = []
    left = ""
    right = ""
    while len(rpn) > 0:
        cur = rpn.pop(0)
        if IsOperator(cur):
            right = operandStack.pop()
            left = operandStack.pop()
            index = operators.index(cur)
            operandStack.append(Compute(left,right,index))
        else:
            operandStack.append(cur)
    return operandStack.pop()

while input0 == 1:
    print("请输入生成四则运算题的数目：")
    numOfQue = int(input())
    inputAnswer = []
    correctAnswer = []
    ques = []
    result =[]
    for i in range(numOfQue):
        result = produceQue()
        ques.append(result[0])
        print(result[0]+space+operators[4]+space+str(Calculate(result)))




